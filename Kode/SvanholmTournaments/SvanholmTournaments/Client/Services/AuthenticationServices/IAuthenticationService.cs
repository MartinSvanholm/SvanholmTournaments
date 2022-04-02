using SvanholmTournaments.Shared.DTOs.AuthenticationDTOs;

namespace SvanholmTournaments.Client.Services.AuthenticationServices;

public interface IAuthenticationService
{
    Task<AuthenticatedUserDTO> Login(loginDTO loginDTO);
    Task Logout();
}