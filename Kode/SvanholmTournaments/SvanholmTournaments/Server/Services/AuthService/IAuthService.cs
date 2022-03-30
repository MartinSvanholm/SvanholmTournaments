using System.Security.Claims;
using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.DTOs.AuthenticationDTOs;

namespace SvanholmTournaments.Server.Services.AuthService;

public interface IAuthService
{
    Task AddRoleToUser(string userName, string roleName);
    string CreateToken(List<Claim> claims);
    Task<AuthenticatedUserDTO?> Login(UserDTO userDTO);
    Task RegisterUser(UserDTO userDto);
    Task RemoveRoleFromUser(string userName, string roleName);
    bool VerifyPasswordHash(UserDTO userDto, User user);
}