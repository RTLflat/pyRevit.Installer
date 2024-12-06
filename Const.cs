namespace pyRevit.Installer;

internal class Consts
{
    internal const string PyRevitRoot = @"C:\pyRevit-Master";
    internal const string EmbeddedInstallerPyrevit4 = "pyRevit.Installer.Resources.pyRevit_4*_signed.exe";
    internal const string EmbeddedInstallerPyrevit5 = "pyRevit.Installer.Resources.pyRevit_5*_signed.exe";
    internal const string Pyrevit4Exe = @"C:\pyRevit-Master\pyRevit-4\bin\pyrevit.exe";
    internal const string Pyrevit5Exe = @"C:\pyRevit-Master\pyRevit-5\bin\pyrevit.exe";
    internal static readonly int[] PyRevitFrameworkYears = [2020, 2021, 2022, 2023, 2024,];
    internal static readonly int[] PyRevitCoreYears = [2025,];
}
