using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System.Net;
using AllUp_BB104.ViewModels.CloudinaryViewModels;

namespace AllUp_BB104.Services;


public class CloudinaryService
{
    private readonly IConfiguration _configuration;
    private readonly CloudinaryOptionsVM _optionsDto;
    private readonly Cloudinary _cloudinary = null!;

    public CloudinaryService(IConfiguration configuration)
    {
        _configuration = configuration;
        _optionsDto = _configuration.GetSection("CloudinarySettings").Get<CloudinaryOptionsVM>() ?? new();

        var myAccount = new Account { ApiKey = _optionsDto.APIKey, ApiSecret = _optionsDto.APISecret, Cloud = _optionsDto.CloudName };

        _cloudinary = new Cloudinary(myAccount);
        _cloudinary.Api.Secure = true;
    }

    public async Task<string> FileCreateAsync(IFormFile file)
    {
        string fileName = string.Concat(Guid.NewGuid(), file.FileName.Substring(file.FileName.LastIndexOf('.')));

        var uploadResult = new ImageUploadResult();
        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
                Folder = "AllUPBB104"
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }
        string url = uploadResult.SecureUrl.ToString();

        return url;
    }

    public async Task<bool> FileDeleteAsync(string filePath)
    {
        try
        {
            string publicIdWithExtension = filePath.Substring(filePath.LastIndexOf("AllUPBB104"));
            string publicId = publicIdWithExtension.Substring(0, publicIdWithExtension.LastIndexOf('.'));

            var deleteParams = new DelResParams()
            {
                PublicIds = new List<string> { publicId },
                Type = "upload",
                ResourceType = ResourceType.Image
            };
            var result = await _cloudinary.DeleteResourcesAsync(deleteParams);

            return result.StatusCode == HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
