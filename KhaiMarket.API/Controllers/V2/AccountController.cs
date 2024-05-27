using System.Security.Claims;
using Asp.Versioning;
using KhaiMarket.API.Features.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KhaiMarket.API.Controllers.V2;


[ApiController]
[ApiVersion(2.0)]
[ApiVersion(1.0, Deprecated = true)]
public class AccountController : ApiController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RolesService _rolesService;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(
        SignInManager<IdentityUser> signInManager,
        RolesService rolesService,
        UserManager<IdentityUser> userManager
    )
    {
        _rolesService = rolesService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IResult> Logout([FromBody] object empty)
    {
        if (empty != null)
        {
            await _signInManager.SignOutAsync();
            return Results.Ok("You LogOut");
        }
        return Results.Unauthorized();
    }

    [HttpGet("getRoles")]
    [Authorize(Roles = "Admin")]
    public async Task<IResult> GetRoles()
    {
        var roles = await _rolesService.GetRolesAsync();
        return Results.Ok(roles);
    }

    [HttpGet("getUserRoles")]
    [Authorize(Roles = "Admin, Manager, User")]
    // [Authorize]
    public async Task<IResult> GetUserRoles()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (userEmail is null)
        {
            return Results.Ok(userEmail);
        }
        var userRoles = await _rolesService.GetUserRolesAsync(userEmail);
        if (userRoles.IsError)
        {
            return Results.BadRequest(userRoles.FirstError);
        }

        return Results.Ok(userRoles.Value);
    }

    [Authorize(Roles = "admin")]
    [HttpPost("addRoles")]
    public async Task<IResult> AddRoles([FromBody] string[] roles)
    {

        var addroles = await _rolesService.AddRolesAsync(roles);
        if (addroles.IsError)
        {
            return Results.BadRequest(addroles.FirstError);
        }

        return Results.Ok(roles);
    }

    [Authorize("admin")]
    [HttpPost("addUserRoles")]
    public async Task<IResult> AddUserRoles([FromBody] UserAndRoles user)
    {
        var addUserRoles = await _rolesService.AddUserRolesAsync(user.Email, user.Roles);

        if (addUserRoles.IsError)
        {
            return Results.BadRequest(addUserRoles.FirstError);
        }

        return Results.Ok(addUserRoles);
    }

    [HttpPost("setDefaultRole")]
    [Authorize]
    public async Task<IResult> SetDefaultRole()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var result = await _rolesService.SetDefaultRole(email!);

        return Results.Ok(result.Value);
    }

}
public class UserAndRoles
{
    public string Email { get; set; } = "";
    public string[] Roles { get; set; } = [];
}