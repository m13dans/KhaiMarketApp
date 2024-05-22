using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using KhaiMarket.API.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Features.Account;

public class RolesService
{
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RolesService(RoleManager<IdentityRole<int>> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<List<IdentityRole<int>>> GetRolesAsync()
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