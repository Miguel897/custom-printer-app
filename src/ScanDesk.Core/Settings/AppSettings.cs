using ScanDesk.Core.Scanning;

namespace ScanDesk.Core.Settings;

public sealed class AppSettings
{
    public string? ScannerUri { get; set; }

    public string? ScannerName { get; set; }

    public ScanOptions Scan { get; set; } = new();

    public string OutputFolder { get; set; } =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Scans");
}
