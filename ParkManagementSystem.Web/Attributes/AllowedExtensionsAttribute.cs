using System.ComponentModel.DataAnnotations;

namespace ParkManagementSystem.Web.Attributes;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var files = value as IFormFileCollection;

        if (files == null || files.Count == 0)
            return ValidationResult.Success;

        foreach (var file in files)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!_extensions.Contains(extension))
            {
                return new ValidationResult($"Only {string.Join(", ", _extensions)} files are allowed.");
            }
        }

        return ValidationResult.Success;
    }
}
