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

    [Authorize (Roles = "Admin")]
    [HttpGet ("getall")]
    public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles ()
    {
        try {
            IEnumerable<Role> roles = await _roleData.GetRoles();
            return Ok(roles);
        }
        catch (Exception e) {
            return BadRequest($"Error: {e.Message}");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<ActionResult> CreateRole(Role role)
    {
        try {
            await _roleData.InsertRole(role.RoleName);
            return Ok("Role created.");
        }
        catch (Exception e) {
            return BadRequest($"Error: {e.Message}");
        }
    }
}
