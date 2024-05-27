using ErrorOr;
using KhaiMarket.API.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Features.Account;

public class RolesService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppDbContext _appDbContext;

    public RolesService(RoleManager<IdentityRole> roleManager,
    UserManager<IdentityUser> userManager,
    AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
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

    public async Task<ErrorOr<List<string>>> AddRolesAsync(string[] roles)
    {
        var roleList = new List<string>();
        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
                roleList.Add(role);
            }
        }

        return roleList;
    }

    public async Task<ErrorOr<bool>> AddUserRolesAsync(string email, string[] roles)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Error.NotFound();
        }

        var existingRoles = _roleManager.Roles;

        var existingRolesNames = existingRoles.Select(x => x.Name);

        var rolesMatch = roles.Where(x => existingRolesNames.Contains(x));

        var addRoles = await _userManager.AddToRolesAsync(user, rolesMatch);
        await _appDbContext.SaveChangesAsync();
        return addRoles.Succeeded;

    }

    public async Task<ErrorOr<Updated>> SetDefaultRole(string email)
    {
        var currentUser = await _userManager.FindByEmailAsync(email);

        if (currentUser is null)
        {
            return Error.NotFound();
        }

        if (await _userManager.IsInRoleAsync(currentUser, "User"))
        {
            Error.Conflict();
        }

        _ = await _userManager.AddToRoleAsync(currentUser, "User");

        await _appDbContext.SaveChangesAsync();


        return Result.Updated;
    }

    private async Task<List<string>> ExistRolesAsync(string[] roles)
    {
        var roleList = new List<string>();
        foreach (var role in roles)
        {
            var rolesExist = await _roleManager.RoleExistsAsync(role);
            if (rolesExist)
            {
                roleList.Add(role);
            }
        }

        return roleList;
    }

}