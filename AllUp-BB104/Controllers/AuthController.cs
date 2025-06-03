using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AllUp_BB104.Controllers;
public class AuthController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> CreateAdmin()
    {

        AppUser user = new()
        {
            Email = "Admin@gmail.com",
            UserName = "Admin",
            Fullname = "Admin admin"
        };

        AppUser user2 = new()
        {
            Email = "member@gmail.com",
            UserName = "Member",
            Fullname = "member"
        };

        var result = await _userManager.CreateAsync(user, "Admin123!");
        var result2 = await _userManager.CreateAsync(user2, "Member123!");

        if (!result.Succeeded)
            return Json(result);

        if(!result2.Succeeded)
            return Json(result2);


        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");

    }

    public async Task<IActionResult> Login1()
    {
        var user=await _userManager.FindByNameAsync("Admin");

        if (user == null)
        {
            return NotFound("User not found");
        }

        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Login2()
    {
        var user = await _userManager.FindByNameAsync("Member");

        if (user == null)
        {
            return NotFound("User not found");
        }

        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToAction("Index", "Home");
    }
}
