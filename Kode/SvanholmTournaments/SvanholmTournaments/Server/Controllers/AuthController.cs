using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SvanholmTournaments.Server.Services.AuthService;
using SvanholmTournaments.Shared.DTOs.AuthenticationDTOs;

namespace SvanholmTournaments.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(UserDTO request)
    {
        try {
            await _authService.RegisterUser(request);
            return Ok("User registered.");
        }
        catch (ArgumentException argumentExeption) {
            return BadRequest(argumentExeption.Message);
        }
        catch (Exception e) {
            return BadRequest($"Error: {e.Message}");
        }
    }
}