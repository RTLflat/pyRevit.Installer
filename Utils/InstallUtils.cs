using System.Diagnostics;

namespace pyRevit.Installer.Utils;

internal static class InstallUtils
{
    internal static void RunInstaller(string? installerFilePath, string? installationDirectory)
    {
        try
        {
            string cmdArguments = "/c " + "\"" + installerFilePath + "\"" + " /VERYSILENT /NORESTART /DIR=" + installationDirectory;

            var process = new Process
            {
                StartInfo = new()
                {
                    FileName = "cmd",
                    Arguments = cmdArguments,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            process.Start();
            string processOutput = process.StandardOutput.ReadToEnd();
            string processError = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Console.WriteLine($"Installation failed with error: {processError}");
            }
            else
            {
                Console.WriteLine($"pyRevit installed successfully at {installationDirectory}");
                Console.WriteLine();

                if (!string.IsNullOrWhiteSpace(processOutput))
                {
                    Console.WriteLine($"Process Output: {processOutput}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while running the installer: {ex.Message}");
        }
    }
}
