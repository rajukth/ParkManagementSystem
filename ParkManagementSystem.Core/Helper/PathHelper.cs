namespace ParkManagementSystem.Infrastructure.Helper;
public static class PathHelper
{
    public static string GenerateRiderDeepLink(string project, string filePath)
    {
        return $"jetbrains://rd/navigate/reference?project={Uri.EscapeDataString(project)}&path={Uri.EscapeDataString(filePath)}";
    }

    public static string GetRelativePath(string fromPath, string toPath)
    {
        var fromUri = new Uri(fromPath);
        var toUri = new Uri(toPath);

        if (fromUri.Scheme != toUri.Scheme)
        {
            return toPath; // Paths have different schemes, cannot calculate relative path
        }

        var relativeUri = fromUri.MakeRelativeUri(toUri);
        var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

        if (string.IsNullOrEmpty(relativePath) || relativePath.StartsWith(".."))
        {
            return toPath; // Absolute path required, return the original path
        }

        return relativePath.Replace('/', Path.DirectorySeparatorChar);
    }
}