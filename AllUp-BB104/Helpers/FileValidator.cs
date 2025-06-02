namespace AllUp_BB104.Helpers;

public static class FileValidator
{
    public static string GetFileName(string fileName)
    {
        var fileExtension = Path.GetExtension(fileName);
        var newFileName = $"{Guid.NewGuid()}{fileExtension}";
        return newFileName;
    }

    public static string GetFilePath(string fileName, string folderName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", folderName, fileName);
        return filePath;
    }

    public static async Task<string> CreateFileAsync(this IFormFile file, string folderName)
    {
        var fileName = GetFileName(file.FileName);
        var filePath = GetFilePath(fileName, folderName);

        using (var stream = new FileStream(filePath, FileMode.Create))

            await file.CopyToAsync(stream).ContinueWith(_ => fileName);

        return fileName;
    }

    public static bool CheckSize(this IFormFile file, int maxSize)
                                                    => file.Length <= maxSize * 1024 * 1024;

    public static bool CheckType(this IFormFile file, string type = "image")
                                                    => file.ContentType.Contains(type);
}
