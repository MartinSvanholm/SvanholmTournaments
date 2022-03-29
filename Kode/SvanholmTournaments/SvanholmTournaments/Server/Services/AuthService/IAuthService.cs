using System.Security.Claims;
using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.DTOs.AuthenticationDTOs;

namespace SvanholmTournaments.Server.Services.AuthService;

public interface IAuthService
{
    Task<bool> CheckIfRolesExists(List<string> roleNames);
    Task<bool> CheckIfUserExist(UserDTO userDTO);
    string CreateToken(List<Claim> claims);
    Task RegisterUser(UserDTO userDto);
    bool VerifyPasswordHash(UserDTO userDto, User user);
}