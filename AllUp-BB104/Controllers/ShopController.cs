using AllUp_BB104.Contexts;
using AllUp_BB104.ViewModels.CommentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AllUp_BB104.Controllers;
public class ShopController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    public ShopController(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IActionResult> Detail(int id)
    {
        var product = await _context.Products
            .Include(x => x.ProductTags)
            .ThenInclude(x => x.Tag)
            .Include(x => x.ProductImages)
            .Include(x => x.Category)
            .Include(x => x.Comments)
            .ThenInclude(x => x.AppUser)
            .Include(x => x.Brand).FirstOrDefaultAsync(x => x.Id == id);

        if (product is null)
            return NotFound();


        var vm = _mapper.Map<ProductDetailVM>(product);

        return View(vm);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostComment(CommentCreateVM vm)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("Detail", new { id = vm.ProductId });

        var existProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == vm.ProductId);


        if (existProduct is null)
            return NotFound();

        var user = await _userManager.GetUserAsync(User);

        if (user is null)
            return BadRequest();

        var comment = _mapper.Map<Comment>(vm);


        comment.AppUserId = user.Id;

        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();



        var comments = await _context.Comments.Where(x => x.ProductId == vm.ProductId).ToListAsync();

        int avarageRating = (int)Math.Ceiling(comments.Sum(x => x.Rating) / (decimal)comments.Count);

        existProduct.Rating = avarageRating;

        _context.Products.Update(existProduct);
        await _context.SaveChangesAsync();


        return RedirectToAction("Detail", new { id = vm.ProductId });
    }
}
