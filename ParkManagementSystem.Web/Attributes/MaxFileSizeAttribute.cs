using System.ComponentModel.DataAnnotations;

namespace ParkManagementSystem.Web.Attributes;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxBytes;

    public MaxFileSizeAttribute(int maxBytes)
    {
        _maxBytes = maxBytes;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var files = value as IFormFileCollection;

        if (files == null || files.Count == 0)
            return ValidationResult.Success; // not required, so allow empty

        foreach (var file in files)
        {
            if (file.Length > _maxBytes)
            {
                return new ValidationResult($"Maximum allowed file size is {_maxBytes / 1024} KB.");
            }
        }

        return ValidationResult.Success;
    }
}