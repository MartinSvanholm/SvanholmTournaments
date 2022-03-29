using Microsoft.IdentityModel.Tokens;
using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.Data;
using SvanholmTournaments.Shared.Data.RoleData;
using SvanholmTournaments.Shared.Data.UserRolesData;
using SvanholmTournaments.Shared.DTOs.AuthenticationDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SvanholmTournaments.Server.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserData _userData;
    private readonly IRoleData _roleData;
    private readonly IUserRolesData _userRolesData;

    public AuthService(IConfiguration configuration, IUserData userData, IRoleData roleData)
    {
        _configuration = configuration;
        _userData = userData;
        _roleData = roleData;
    }

    /// <summary>
    /// Checks if the users username exists in the database.
    /// </summary>
    /// <param name="userDTO"></param>
    /// <returns>Returns true if the users username exist in the database, otherwise false.</returns>
    public async Task<bool> CheckIfUserExist(UserDTO userDTO)
    {
        IEnumerable<User> users = await _userData.GetUsers();

        foreach (User user in users) {

            if (user.Username == userDTO.Username)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if the role exists in the database.
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns>Returns true if the roles name exists in the database, otherwise false.</returns>
    public async Task<bool> CheckIfRolesExists(List<string> roleNames)
    {
        IEnumerable<Role> roles = await _roleData.GetRoles();

        foreach (string roleName in roleNames) {
            foreach (Role role in roles) {
                if (role.Name == roleName)
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Creates password salt and hash, and inserts the user into the database.
    /// </summary>
    /// <param name="userDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task RegisterUser(UserDTO userDto)
    {
        if (await CheckIfUserExist(userDto)) {
            throw new ArgumentException("The username is allready taken.");
        }

        if (!await CheckIfRolesExists(userDto.Roles)) {
            throw new ArgumentException("Role does not exist");
        }

        User user = new User(userDto.FirstName, userDto.LastName, userDto.Username, DateTime.Now);
        CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        try {
            await _userData.InsertUser(user);

            foreach (string roleName in userDto.Roles) {
                Role? role = await _roleData.GetRoleByName(roleName);

                if (role != null) {
                    await _userRolesData.InsertRoleForUser(user, role.Id);
                }
                else {
                    throw new ArgumentException($"Role: {roleName} not found.");
                }
            }
        }
        catch (Exception) {
            throw;
        }
    }

    public bool VerifyPasswordHash(UserDTO userDto, User user)
    {
        using var hmac = new HMACSHA512(user.PasswordSalt);

        byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userDto.Password));
        Console.WriteLine(computedHash == user.PasswordHash);
        return computedHash.SequenceEqual(user.PasswordHash);
    }

    public string CreateToken(List<Claim> claims)
    {
        //List<Claim> claims = new()
        //{
        //    new Claim(ClaimTypes.Name, user.Username),
        //    new Claim(ClaimTypes.Role, "Admin")
        //};

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: cred);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA256();

        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
}