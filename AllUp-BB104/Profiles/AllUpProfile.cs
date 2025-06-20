
using AllUp_BB104.ViewModels.CommentViewModels;

namespace AllUp_BB104.Profiles;

public class AllUpProfile : Profile
{
    public AllUpProfile()
    {
        CreateMap<Product, ProductGetVM>().ForMember(vm => vm.MainImagePath, x => x.MapFrom(product => product.ProductImages.FirstOrDefault(x => x.IsMain == true).Path)).ReverseMap();

        CreateMap<Product, ProductDetailVM>()
            .ForMember(x => x.CategoryName, x => x.MapFrom(product => product.Category.Name))
            .ForMember(x => x.BrandName, x => x.MapFrom(product => product.Brand.Name))
            .ForMember(x => x.ProductImages, x => x.MapFrom(product => product.ProductImages.Select(x => x.Path)))
            .ForMember(x => x.TagNames, x => x.MapFrom(product => product.ProductTags.Select(x => x.Tag.Name)))
            .ReverseMap();



        CreateMap<Product, ProductCreateVM>().ReverseMap();




        CreateMap<Product, ProductUpdateVM>().ForMember(vm => vm.MainImagePath, x => x.MapFrom(product => product.ProductImages.FirstOrDefault(x => x.IsMain == true).Path))
            .ForMember(x => x.ProductImagePaths, x => x.MapFrom(x => x.ProductImages.Where(x => !x.IsMain).Select(x => x.Path)))
            .ForMember(x => x.ProductImageIds, x => x.MapFrom(x => x.ProductImages.Where(x => !x.IsMain).Select(x => x.Id)))
            .ForMember(x => x.TagIds, x => x.MapFrom(x => x.ProductTags.Select(x => x.TagId))).ReverseMap();

        CreateMap<Category, CategoryGetVM>().ReverseMap();
        CreateMap<Brand, BrandGetVM>().ReverseMap();
        CreateMap<Tag, TagGetVM>().ReverseMap();

        CreateMap<BasketItem, BasketItemGetVM>().ReverseMap();

        CreateMap<Comment,CommentGetVM>()
            .ForMember(x => x.AppUserName, x => x.MapFrom(comment => comment.AppUser.UserName))
            .ReverseMap();  

        CreateMap<Comment, CommentCreateVM>().ReverseMap();

    }
}
