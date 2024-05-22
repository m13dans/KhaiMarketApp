using Asp.Versioning;
using KhaiMarket.API.Features.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KhaiMarket.API.Controllers.V2;

[ApiController]

public class AccountController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RolesService _rolesService;

    public AccountController(SignInManager<IdentityUser> signInManager, RolesService rolesService)
    {
        _rolesService = rolesService;
        _signInManager = signInManager;
    }

    [HttpPost("account/logout")]
    [Authorize]
    public async Task<IResult> Logout([FromBody] object empty)
    {
        if (empty != null)
        {
            await _signInManager.SignOutAsync();
            return Results.Ok();
        }
        return Results.Unauthorized();
    }

    [HttpGet("account/getroles")]
    [Authorize(Roles = "Admin")]
    public async Task<IResult> GetRoles()
    {
        var roles = await _rolesService.GetRolesAsync();
        return Results.Ok(roles);
    }

    [HttpGet("account/getuserroles")]
    [Authorize(Roles = "Admin, Manager, User")]
    public async Task<IResult> GetUserRoles(string userEmail)
    {
        var userRoles = await _rolesService.GetUserRolesAsync(userEmail);
        if (userRoles.IsError)
        {
            return Results.Problem(userRoles.Errors.ToString());
        }

        return Results.Ok(userRoles.Value);
    }
}