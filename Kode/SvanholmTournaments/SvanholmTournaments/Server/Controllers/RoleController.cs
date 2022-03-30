using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.Data.RoleData;
using SvanholmTournaments.Shared.DTOs.AuthenticationDTOs;

namespace SvanholmTournaments.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleData _roleData;

    public RoleController(IRoleData roleData)
    {
        _roleData = roleData;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<ActionResult> CreateRole(string roleName)
    {
        try {
            await _roleData.InsertRole(roleName);
            return Ok("Role created.");
        }
        catch (Exception e) {
            return BadRequest($"Error: {e.Message}");
        }
    }
}
