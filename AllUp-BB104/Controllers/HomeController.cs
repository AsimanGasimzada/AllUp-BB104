using Microsoft.AspNetCore.Mvc;

namespace AllUp_BB104.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
