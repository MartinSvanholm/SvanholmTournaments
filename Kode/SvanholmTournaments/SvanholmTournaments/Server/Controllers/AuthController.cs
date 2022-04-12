using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SvanholmTournaments.Server.Services.AuthService;
using SvanholmTournaments.Shared.AuthenticationModels;
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

    [Authorize(Roles = "Admin")]
    [HttpGet("getallusers")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        try {
            return Ok(await _authService.GetAllUsers());
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticatedUserDTO>> Login(loginDTO loginDTO)
    {
        UserDTO userDTO = new UserDTO() { Username = loginDTO.Username, Password = loginDTO.Password};

        try {
            AuthenticatedUserDTO? authenticatedUserDTO = await _authService.Login(userDTO);

            if (authenticatedUserDTO == null)
                return BadRequest("Wrong username or password.");

            return Ok(authenticatedUserDTO);
        }
        catch (ArgumentException argumentExeption) {
            return BadRequest(argumentExeption.Message);
        }
        catch (Exception e) {
            return BadRequest($"Error: {e.Message}");
        }
    }

    [Authorize (Roles = "Admin")]
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

    [Authorize (Roles = "Admin")]
    [HttpPut("update")]
    public async Task<ActionResult<UserDTO>> UpdateUser(UserDTO userDTO)
    {
        try {
            await _authService.UpdateUser(userDTO);

            return Ok(userDTO);
        }
        catch (ArgumentException argumentExeption) {
            return BadRequest(argumentExeption.Message);
        }
        catch(Exception e) {
            return BadRequest($"Error: {e.Message}");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut ("delete")]
    public async Task<ActionResult> DeleteUser(UserDTO userDTO)
    {
        try {
            await _authService.DeleteUser(userDTO);

            return Ok("User deleted");
        }
        catch (ArgumentException ex) {
            return BadRequest(ex.Message);
        }
        catch (Exception e) {
            return BadRequest($"Error: {e.Message}");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("addRole")]
    public async Task<ActionResult> AddRoleToUser(string userName, string roleName)
    {
        try {
            await _authService.AddRoleToUser(userName, roleName);

            return Ok($"Role: {roleName} added to user: {userName}");
        }
        catch (ArgumentException ex) {
            return BadRequest(ex.Message);
        }
        catch (Exception e) {
            return BadRequest($"Error: {e.Message}");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("deleteRole")]
    public async Task<ActionResult> DeleteRoleFromUser(string userName, string roleName)
    {
        try {
            await _authService.RemoveRoleFromUser(userName, roleName);

            return Ok($"Role deleted from user");
        }
        catch (ArgumentException ex) {
            return BadRequest(ex.Message);
        }
        catch (Exception e) {
            return BadRequest($"Error: {e.Message}");
        }
    }
}