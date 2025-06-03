using AllUp_BB104.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AllUp_BB104.Controllers;
public class BasketController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public BasketController(AppDbContext context, UserManager<AppUser> userManager, IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IActionResult> AddToBasket(int id)
    {



        var product = await _context.Products.FindAsync(id);

        if (product is null)
            return NotFound();


        if (User.Identity?.IsAuthenticated ?? false)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return BadRequest();


            var existBasketItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.ProductId == id && x.AppUserId == user.Id);


            if (existBasketItem is not null)
            {
                existBasketItem.Count++;
            }
            else
            {
                BasketItem basketItem = new()
                {
                    Count = 1,
                    ProductId = id,
                    AppUserId = user.Id
                };

                await _context.BasketItems.AddAsync(basketItem);
            }


            await _context.SaveChangesAsync();
        }
        else
        {
            List<BasketItemGetVM> basketItems = await GetBasket();

            var existBasketItem = basketItems.FirstOrDefault(x => x.ProductId == id);

            if (existBasketItem is not null)
            {
                existBasketItem.Count++;
            }
            else
            {

                BasketItemGetVM newBasketItem = new()
                {
                    Count = 1,
                    ProductId = id
                };

                basketItems.Add(newBasketItem);
            }


            var json = JsonConvert.SerializeObject(basketItems);


            Response.Cookies.Append("basket", json);
        }



        return PartialView("_basketModalPartial");
    }


    public async Task<List<BasketItemGetVM>> GetBasket()
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                throw new Exception(); ;


            var basketItems = await _context.BasketItems.Where(x => x.AppUserId == user.Id).Include(x => x.Product).ThenInclude(x => x.ProductImages).ToListAsync();



            var vms = _mapper.Map<List<BasketItemGetVM>>(basketItems);

            return vms;
        }
        else
        {
            var json = Request.Cookies["basket"];


            var basketItems = JsonConvert.DeserializeObject<List<BasketItemGetVM>>(json) ?? new List<BasketItemGetVM>();

            foreach (var item in basketItems)
            {
                var product = await _context.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == item.ProductId);


                var vmProduct = _mapper.Map<ProductGetVM>(product);
                item.Product = vmProduct;
            }

            var vms = _mapper.Map<List<BasketItemGetVM>>(basketItems);



            return vms;
        }
    }


    public async Task<IActionResult> DeleteFromBasket(int productId)
    {
        var basketItems = await GetBasket();

        var existBasketItem = basketItems.FirstOrDefault(x => x.ProductId == productId);

        if (existBasketItem is null)
            return NotFound();

        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return BadRequest();

            _context.BasketItems.Where(x => x.ProductId == productId && x.AppUserId == user.Id).ExecuteDelete();
        }
        else
        {
            basketItems.Remove(existBasketItem);

            var json = JsonConvert.SerializeObject(basketItems);

            Response.Cookies.Append("basket", json);
        }

        string? returnUrl = Request.Headers["Referer"].ToString();

        if (string.IsNullOrEmpty(returnUrl))
            return RedirectToAction("Index");

        return Redirect(returnUrl);
    }

    public IActionResult Index()
    {
        return View();
    }


    public async Task<IActionResult> Decrease(int productId)
    {
        var basketItems = await GetBasket();


        var existBasketItem = basketItems.FirstOrDefault(x => x.ProductId == productId);

        if (existBasketItem is null)
            return NotFound();

        if (existBasketItem.Count <= 1)
            return PartialView("_BasketSectionPartial");


        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return BadRequest();

            var basketItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.ProductId == productId && x.AppUserId == user.Id);

            if (basketItem is null)
                return NotFound();

            basketItem.Count--;
            await _context.SaveChangesAsync();
        }
        else
        {
            existBasketItem.Count--;
            var json = JsonConvert.SerializeObject(basketItems);
            Response.Cookies.Append("basket", json);
        }


        return PartialView("_BasketSectionPartial");


    }



    public async Task<IActionResult> Increase(int productId)
    {
        var basketItems = await GetBasket();


        var existBasketItem = basketItems.FirstOrDefault(x => x.ProductId == productId);

        if (existBasketItem is null)
            return NotFound();

        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return BadRequest();

            var basketItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.ProductId == productId && x.AppUserId == user.Id);

            if (basketItem is null)
                return NotFound();

            basketItem.Count++;
            await _context.SaveChangesAsync();
        }
        else
        {
            existBasketItem.Count++;
            var json = JsonConvert.SerializeObject(basketItems);
            Response.Cookies.Append("basket", json);
        }


        return PartialView("_BasketSectionPartial");


    }


    public IActionResult GetBasketModal()
    {
        return PartialView("_basketModalPartial");
    }
}
