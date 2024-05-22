using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Features.Account;

public class RolesService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RolesService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<List<IdentityRole>> GetRolesAsync()
    {
        var roleList = _roleManager.Roles.ToListAsync();
        return await roleList;
    }

    public async Task<ErrorOr<List<string>>> GetUserRolesAsync(string emailId)
    {
        var user = await _userManager.FindByEmailAsync(emailId);

        if (user == null)
        {
            return Error.NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        return userRoles.ToList();
    }


}