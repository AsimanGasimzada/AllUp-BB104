using AllUp_BB104.Contexts;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AllUp_BB104.Services;

public class LayoutService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LayoutService(UserManager<AppUser> userManager, AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BasketItemVM> GetBasketAsync()
    {
        if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user is null)
                throw new Exception(); ;


            var basketItems = await _context.BasketItems.Where(x => x.AppUserId == user.Id).Include(x => x.Product).ThenInclude(x => x.ProductImages).ToListAsync();



            var vms = _mapper.Map<List<BasketItemGetVM>>(basketItems);


            BasketItemVM vm = new()
            {
                BasketItems = vms,
                TotalPrice = vms.Sum(x => x.Product.Price * x.Count),
                Count = vms.Sum(x => x.Count)
            };

            return vm;
        }
        else
        {
            var json = _httpContextAccessor.HttpContext.Request.Cookies["basket"];


            var basketItems = JsonConvert.DeserializeObject<List<BasketItemGetVM>>(json) ?? new List<BasketItemGetVM>();

            foreach (var item in basketItems)
            {
                var product = await _context.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == item.ProductId);


                var vmProduct = _mapper.Map<ProductGetVM>(product);
                item.Product = vmProduct;
            }

            var vms = _mapper.Map<List<BasketItemGetVM>>(basketItems);

            BasketItemVM vm = new()
            {
                BasketItems = vms,
                TotalPrice = vms.Sum(x => x.Product.Price * x.Count),
                Count = vms.Sum(x => x.Count)
            };

            return vm;
        }
    }


    public async Task<Dictionary<string, string>> GetSettingsAsync()
    {
        var settings = await _context.Settings.ToDictionaryAsync(x => x.Key, x => x.Value);

        return settings;
    }
}
