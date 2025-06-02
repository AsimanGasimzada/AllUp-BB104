using Microsoft.AspNetCore.Mvc;

namespace AllUp_BB104.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
