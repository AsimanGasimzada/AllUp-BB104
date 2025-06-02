
namespace AllUp_BB104.Profiles;

public class AllUpProfile : Profile
{
    public AllUpProfile()
    {
        CreateMap<Product, ProductGetVM>().ForMember(vm => vm.MainImagePath, x => x.MapFrom(product => product.ProductImages.FirstOrDefault(x => x.IsMain == true).Path)).ReverseMap();
        CreateMap<Product, ProductCreateVM>().ReverseMap();




        CreateMap<Product, ProductUpdateVM>().ForMember(vm => vm.MainImagePath, x => x.MapFrom(product => product.ProductImages.FirstOrDefault(x => x.IsMain == true).Path))
            .ForMember(x => x.ProductImagePaths, x => x.MapFrom(x => x.ProductImages.Where(x => !x.IsMain).Select(x => x.Path)))
            .ForMember(x => x.ProductImageIds, x => x.MapFrom(x => x.ProductImages.Where(x => !x.IsMain).Select(x => x.Id)))
            .ForMember(x => x.TagIds, x => x.MapFrom(x => x.ProductTags.Select(x => x.TagId))).ReverseMap();

        CreateMap<Category, CategoryGetVM>().ReverseMap();
        CreateMap<Brand, BrandGetVM>().ReverseMap();
        CreateMap<Tag, TagGetVM>().ReverseMap();
    }
}
