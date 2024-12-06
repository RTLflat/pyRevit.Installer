using System.Reflection;
using System.Text.RegularExpressions;

namespace pyRevit.Installer.Utils;

internal static class ResourceUtils
{
    internal static IEnumerable<string> GetMatchingResourceNames(string wildcardPattern)
    {
        var embeddedAssembly = Assembly.GetExecutingAssembly();
        var resourceNames = embeddedAssembly.GetManifestResourceNames();

        string regexPattern = "^" + Regex.Escape(wildcardPattern).Replace("\\*", ".*") + "$";

        var matchingResources = resourceNames.Where(resource => Regex.IsMatch(resource, regexPattern));
        return matchingResources;
    }

    internal static void ExtractAndInstallResource(string embeddedInstallerName, string installDirectory)
    {
        var extractedInstallerPath = ExtractResourceToFile(embeddedInstallerName);

        if (extractedInstallerPath != null && File.Exists(extractedInstallerPath))
        {
            InstallUtils.RunInstaller(extractedInstallerPath, installDirectory);

            try
            {
                File.Delete(extractedInstallerPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete extracted installer file: {extractedInstallerPath}. Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Failed to extract the installer resource: {embeddedInstallerName}.");
        }
    }

    private static string? ExtractResourceToFile(string embeddedResourceName)
    {
        try
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            using var resourceStream = executingAssembly.GetManifestResourceStream(embeddedResourceName);
            if (resourceStream == null)
            {
                Console.WriteLine($"Resource {embeddedResourceName} not found.");
                return null;
            }

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(embeddedResourceName);
            string tempFilePath = Path.Combine(Path.GetTempPath(), $"{fileNameWithoutExtension}.exe");

            using var outputFileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
            resourceStream.CopyTo(outputFileStream);

            Console.WriteLine($"Extracted resource to: {tempFilePath}");
            return tempFilePath;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to extract resource {embeddedResourceName}. Error: {ex.Message}");
            return null;
        }
    }
}
