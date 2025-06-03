using AllUp_BB104.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AllUp_BB104.Controllers;
public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public HomeController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {

        var products = await _context.Products.Include(x=>x.ProductImages).Include(x=>x.ProductTags).ThenInclude(x=>x.Tag).ToListAsync();


        var vms = _mapper.Map<List<ProductGetVM>>(products);


        HomeVM homeVM = new()
        {
            Products = vms
        };


        return View(homeVM);
    }
}
