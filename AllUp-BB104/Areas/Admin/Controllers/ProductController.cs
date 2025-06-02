using AllUp_BB104.Contexts;
using AllUp_BB104.Helpers;
using AllUp_BB104.Models;
using AllUp_BB104.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AllUp_BB104.Areas.Admin.Controllers;
[Area("Admin")]
//[AutoValidateAntiforgeryToken]
public class ProductController : Controller
{

    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly CloudinaryService _cloudinaryService;

    public ProductController(AppDbContext context, IMapper mapper, CloudinaryService cloudinaryService)
    {
        _context = context;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<IActionResult> Index()
    {

        var products = await _context.Products.Include(x => x.ProductImages).ToListAsync();

        var vms = _mapper.Map<List<ProductGetVM>>(products);


        return View(vms);
    }

    public async Task<IActionResult> Create()
    {
        await _setViewBagItemsAsync();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateVM vm)
    {


        #region Validations

        if (!ModelState.IsValid)
        {
            await _setViewBagItemsAsync();
            return View(vm);
        }

        var isExistCategory = await _context.Categories.AnyAsync(c => c.Id == vm.CategoryId);

        if (!isExistCategory)
        {
            await _setViewBagItemsAsync();
            ModelState.AddModelError("CategoryId", "Category does not exist");
            return View(vm);
        }

        var isExistBrand = await _context.Brands.AnyAsync(b => b.Id == vm.BrandId);
        if (!isExistBrand)
        {
            await _setViewBagItemsAsync();
            ModelState.AddModelError("BrandId", "Brand does not exist");
            return View(vm);
        }

        foreach (var tagId in vm.TagIds)
        {
            var isExistTag = await _context.Tags.AnyAsync(x => x.Id == tagId);

            if (!isExistTag)
            {
                await _setViewBagItemsAsync();
                ModelState.AddModelError("TagIds", $"Tag with ID {tagId} does not exist");
                return View(vm);
            }
        }


        foreach (var image in vm.ProductImageFiles)
        {
            if (!image.CheckSize(2))
            {
                await _setViewBagItemsAsync();
                ModelState.AddModelError("ProductImageFiles", "Image size must be less than 2 MB");
                return View(vm);
            }
            if (!image.CheckType("image"))
            {
                await _setViewBagItemsAsync();
                ModelState.AddModelError("ProductImageFiles", "Image type must be an image");
                return View(vm);
            }
        }

        #endregion


        var product = _mapper.Map<Product>(vm);


        foreach (var tagId in vm.TagIds)
        {

            ProductTag productTag = new()
            {
                ProductId = product.Id,
                TagId = tagId,
                Product = product
            };

            product.ProductTags.Add(productTag);
        }


        foreach (var image in vm.ProductImageFiles)
        {

            string url = await _cloudinaryService.FileCreateAsync(image);

            ProductImage productImage = new()
            {
                Path = url,
                ProductId = product.Id,
                Product = product,
                IsMain = false
            };

            product.ProductImages.Add(productImage);

        }


        string mainImageUrl = await _cloudinaryService.FileCreateAsync(vm.MainImage);


        ProductImage mainProductImage = new()
        {
            ProductId = product.Id,
            Product = product,
            IsMain = true,
            Path = mainImageUrl
        };

        product.ProductImages.Add(mainProductImage);


        await _context.AddAsync(product);
        await _context.SaveChangesAsync();


        return RedirectToAction("Index");
    }

    private async Task _setViewBagItemsAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        var brands = await _context.Brands.ToListAsync();
        var tags = await _context.Tags.ToListAsync();

        var categoryVMs = _mapper.Map<List<CategoryGetVM>>(categories);
        var brandVMs = _mapper.Map<List<BrandGetVM>>(brands);
        var tagVMs = _mapper.Map<List<TagGetVM>>(tags);

        ViewBag.Categories = categoryVMs;
        ViewBag.Brands = brandVMs;
        ViewBag.Tags = tagVMs;
    }


    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == id);

        if (product is null)
            return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        foreach (var image in product.ProductImages)
        {
            await _cloudinaryService.FileDeleteAsync(image.Path);
        }

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Update(int id)
    {
        var product = _context.Products.Include(x => x.ProductImages).Include(x => x.ProductTags).FirstOrDefault(x => x.Id == id);

        if (product is null)
            return NotFound();

        var vm = _mapper.Map<ProductUpdateVM>(product);

        await _setViewBagItemsAsync();

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ProductUpdateVM vm)
    {

        #region Validations

        if (!ModelState.IsValid)
        {
            await _setViewBagItemsAsync();
            return View(vm);
        }

        var isExistCategory = await _context.Categories.AnyAsync(c => c.Id == vm.CategoryId);

        if (!isExistCategory)
        {
            await _setViewBagItemsAsync();
            ModelState.AddModelError("CategoryId", "Category does not exist");
            return View(vm);
        }

        var isExistBrand = await _context.Brands.AnyAsync(b => b.Id == vm.BrandId);
        if (!isExistBrand)
        {
            await _setViewBagItemsAsync();
            ModelState.AddModelError("BrandId", "Brand does not exist");
            return View(vm);
        }

        foreach (var tagId in vm.TagIds)
        {
            var isExistTag = await _context.Tags.AnyAsync(x => x.Id == tagId);

            if (!isExistTag)
            {
                await _setViewBagItemsAsync();
                ModelState.AddModelError("TagIds", $"Tag with ID {tagId} does not exist");
                return View(vm);
            }
        }


        foreach (var image in vm.ProductImageFiles)
        {
            if (!image.CheckSize(2))
            {
                await _setViewBagItemsAsync();
                ModelState.AddModelError("ProductImageFiles", "Image size must be less than 2 MB");
                return View(vm);
            }
            if (!image.CheckType("image"))
            {
                await _setViewBagItemsAsync();
                ModelState.AddModelError("ProductImageFiles", "Image type must be an image");
                return View(vm);
            }
        }

        if (vm.MainImage is not null)
        {
            if (!vm.MainImage.CheckSize(2))
            {
                await _setViewBagItemsAsync();
                ModelState.AddModelError("MainImage", "Image size must be less than 2 MB");
                return View(vm);
            }
            if (!vm.MainImage.CheckType("image"))
            {
                await _setViewBagItemsAsync();
                ModelState.AddModelError("MainImage", "Image type must be an image");
                return View(vm);
            }

        }

        #endregion


        var existProduct = await _context.Products.Include(x => x.ProductImages).Include(x => x.ProductTags).FirstOrDefaultAsync(x => x.Id == vm.Id);

        if (existProduct is null)
            return BadRequest();


        //existProduct.Name = vm.Name;
        //existProduct.Price = vm.Price;

        existProduct = _mapper.Map(vm, existProduct);

        if (vm.MainImage is { })
        {


            var existMainImage = existProduct.ProductImages.FirstOrDefault(x => x.IsMain);
            if (existMainImage is not null)
            {
                // Delete the old main image from Cloudinary
                await _cloudinaryService.FileDeleteAsync(existMainImage.Path);
                existProduct.ProductImages.Remove(existMainImage);
            }

            string mainImageUrl = await _cloudinaryService.FileCreateAsync(vm.MainImage);
            ProductImage mainProductImage = new()
            {
                ProductId = existProduct.Id,
                Product = existProduct,
                IsMain = true,
                Path = mainImageUrl
            };


            existProduct.ProductImages.Add(mainProductImage);

        }


        foreach (var image in vm.ProductImageFiles)
        {

            string url = await _cloudinaryService.FileCreateAsync(image);

            ProductImage productImage = new()
            {
                Path = url,
                ProductId = existProduct.Id,
                Product = existProduct,
                IsMain = false
            };

            existProduct.ProductImages.Add(productImage);

        }


        _context.ProductTags.RemoveRange(existProduct.ProductTags);
        existProduct.ProductTags.Clear();

        foreach (var tagId in vm.TagIds)
        {

            ProductTag productTag = new()
            {
                ProductId = existProduct.Id,
                TagId = tagId,
                Product = existProduct
            };

            existProduct.ProductTags.Add(productTag);
        }

        _context.Products.Update(existProduct);
        await _context.SaveChangesAsync();


        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteImage(int id)
    {
        var productImage = await _context.ProductImages.FirstOrDefaultAsync(x => x.Id == id);

        if (productImage is null )
            return NotFound();

        _context.Remove(productImage);
        await _context.SaveChangesAsync();

        await _cloudinaryService.FileDeleteAsync(productImage.Path);


        return RedirectToAction("Update", new { id = productImage.ProductId });

        //string returnUrl = Request.Headers["Referer"].ToString();

        //if(string.IsNullOrEmpty(returnUrl))
        //{
        //    return RedirectToAction("Index");
        //}

        //return Redirect(returnUrl); // Redirect to the previous page using the Referer header

    }
}
