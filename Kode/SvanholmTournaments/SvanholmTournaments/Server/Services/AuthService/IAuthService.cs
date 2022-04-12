using System.Security.Claims;
using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.DTOs.AuthenticationDTOs;

namespace SvanholmTournaments.Server.Services.AuthService;

public interface IAuthService
{
    Task AddRoleToUser(string userName, string roleName);
    string CreateToken(List<Claim> claims);
    Task<IEnumerable<UserDTO>> GetAllUsers();
    Task<AuthenticatedUserDTO?> Login(UserDTO userDTO);
    Task RegisterUser(UserDTO userDto);
    Task<UserDTO> UpdateUser(UserDTO userDTO);
    Task DeleteUser(UserDTO userDTO);
    Task RemoveRoleFromUser(string userName, string roleName);
    bool VerifyPasswordHash(UserDTO userDto, User user);
}