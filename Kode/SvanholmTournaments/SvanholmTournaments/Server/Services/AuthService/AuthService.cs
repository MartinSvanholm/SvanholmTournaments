using Microsoft.IdentityModel.Tokens;
using SvanholmTournaments.Shared;
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

    public AuthService(IConfiguration configuration, IUserData userData, IRoleData roleData, IUserRolesData userRolesData)
    {
        _configuration = configuration;
        _userData = userData;
        _roleData = roleData;
        _userRolesData = userRolesData;
    }

    public async Task<AuthenticatedUserDTO?> Login(UserDTO userDTO)
    {
        User? user = await _userData.GetUserByUsername(userDTO.Username);

        if (user == null)
            return null;

        if (!VerifyPasswordHash(userDTO, user))
            return null;

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

        if (user.Roles != null) {
            foreach (Role role in user.Roles) {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }
        }

        AuthenticatedUserDTO authenticatedUserDTO = new AuthenticatedUserDTO {
            Username = userDTO.Username,
            AccessToken = CreateToken(claims)
        };

        return authenticatedUserDTO;
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
                await AddRoleToUser(user.Username, roleName);
            }
        }
        catch (Exception) {
            throw;
        }
    }

    /// <summary>
    /// Updates the user on the database, returns the user if successful.
    /// </summary>
    /// <param name="userDTO"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<UserDTO> UpdateUser(UserDTO userDTO)
    {
        if (! await CheckIfUserExist(userDTO)) {
            throw new ArgumentException("The user does not exist.");
        }

        User updatedUser = new();
        updatedUser.MapUser(userDTO);

        Printer.PrintUser(updatedUser);

        await _userData.UpdateUser(updatedUser);
        await _userRolesData.DeleteRolesForUser(updatedUser);

        foreach (Role role in updatedUser.Roles) {
            Role? tempRole = await _roleData.GetRoleByName(role.RoleName);

            if (tempRole == null) {
                throw new ArgumentException($"Role: {role.RoleName} does not exist");
            }
            else {
                await _userRolesData.InsertRoleForUser(updatedUser, tempRole.Id);
            }
        }

        return userDTO;
    }

    public async Task DeleteUser(UserDTO userDTO)
    {
        if (await CheckIfUserExist(userDTO)) {
            await _userData.DeleteUser(userDTO.Id);
        } else {
            throw new ArgumentException("The user does not exist.");
        }
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsers()
    {
        var users = await _userData.GetUsers();

        List<UserDTO> userDTOs = new List<UserDTO>();

        foreach (User user in users) {
            userDTOs.Add(new UserDTO().MapUserDTO(user));
        }

        return userDTOs;
    }

    /// <summary>
    /// Check if the user and role exists in the database, then insert the role for the user if they do.
    /// </summary>
    /// <param name="userDTO"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task AddRoleToUser(string userName, string roleName)
    {
        Role? role = await _roleData.GetRoleByName(roleName);

        if (role == null)
            throw new ArgumentException("Role does not exist.");

        User? user = await _userData.GetUserByUsername(userName);

        if (user == null)
            throw new ArgumentException("User does not exist.");

        await _userRolesData.InsertRoleForUser(user, role.Id);
    }

    public async Task RemoveRoleFromUser(string userName, string roleName)
    {
        Role? role = await _roleData.GetRoleByName(roleName);

        if (role == null)
            throw new ArgumentException("Role does not exist.");

        User? user = await _userData.GetUserByUsername(userName);

        if (user == null)
            throw new ArgumentException("User does not exist.");

        await _userRolesData.DeleteRolesForUser(user);
    }

    public bool VerifyPasswordHash(UserDTO userDto, User user)
    {
        using var hmac = new HMACSHA256(user.PasswordSalt);

        byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userDto.Password));
        return computedHash.SequenceEqual(user.PasswordHash);
    }

    public string CreateToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(120),
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

    /// <summary>
    /// Checks if the users username exists in the database.
    /// </summary>
    /// <param name="userDTO"></param>
    /// <returns>Returns true if the users username exist in the database, otherwise false.</returns>
    private async Task<bool> CheckIfUserExist(UserDTO userDTO)
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
    private async Task<bool> CheckIfRolesExists(List<string> roleNames)
    {
        IEnumerable<Role> roles = await _roleData.GetRoles();

        foreach (string roleName in roleNames) {
            foreach (Role role in roles) {
                if (role.RoleName == roleName)
                    return true;
            }
        }

        return false;
    }
}