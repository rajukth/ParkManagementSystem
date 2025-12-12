namespace ParkManagementSystem.Web.Helper;

public static class ImageHelper
{
    /// <summary>
    /// Uploads an image to wwwroot folder and returns the relative path
    /// </summary>
    /// <param name="file">The uploaded IFormFile</param>
    /// <param name="folder">Subfolder under wwwroot (e.g., "uploads/org")</param>
    /// <param name="env">IWebHostEnvironment</param>
    /// <returns>Relative path to saved image</returns>
    public static async Task<string?> UploadImageAsync(IFormFile file, string folder, IWebHostEnvironment env)
    {
        if (file == null || file.Length == 0)
            return null;

        // Ensure folder exists
        var uploadFolder = Path.Combine(env.WebRootPath, folder);
        if (!Directory.Exists(uploadFolder))
            Directory.CreateDirectory(uploadFolder);

        // Generate unique file name
        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";

        var filePath = Path.Combine(uploadFolder, fileName);

        // Save the file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Return relative path for usage in HTML
        var relativePath = Path.Combine("/", folder.Replace("\\", "/"), fileName);
        return relativePath;
    }

    /// <summary>
    /// Deletes a file from wwwroot folder
    /// </summary>
    public static void DeleteImage(string relativePath, IWebHostEnvironment env)
    {
        if (string.IsNullOrEmpty(relativePath))
            return;

        var filePath = Path.Combine(env.WebRootPath, relativePath.TrimStart('/').Replace("/", "\\"));
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
    }