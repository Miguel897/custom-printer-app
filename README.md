# ScanDesk

Windows desktop app to scan documents from network scanners that support **eSCL** (AirScan / Mopria),
such as the HP ENVY 6400 series. No HP software or scanner driver needed.

- Flatbed and document feeder (all pages of a feeder batch)
- Live PDF preview, page thumbnails, reorder (drag & drop), rotate, delete
- Manual two-sided scanning for feeders without duplex
- Save as PDF (JPEGs embedded without recompression) or JPEG files
- Unsaved pages survive a crash or close and are offered on the next start

See [docs/PRD.md](docs/PRD.md) for requirements and design.

## Requirements

- Windows 10/11 x64
- .NET 10 SDK to build (not needed to run the published exe)
- Microsoft Edge WebView2 Runtime for the PDF preview (preinstalled on Windows 10/11)

## Build, test, run

```powershell
dotnet build ScanDesk.slnx
dotnet test ScanDesk.slnx
dotnet run --project src/ScanDesk.App
```

## Publish a single exe

```powershell
dotnet publish src/ScanDesk.App -c Release -r win-x64 -o publish
```

Produces `publish/ScanDesk.exe` (~64 MB, self-contained). Copy it to any Windows PC on the same network as the scanner.

## First run on a new PC

- Windows Firewall asks whether ScanDesk may use the network: allow it on **private** networks so the
  scanner can be found automatically (mDNS). Without it, type the scanner's IP address and click **Connect**.
- Scans are saved to `Documents\Scans` by default; the last used folder is remembered.

## Where data is stored

| What | Where |
|---|---|
| Settings | `%APPDATA%\ScanDesk\settings.json` |
| Pages of the current document | `%LOCALAPPDATA%\ScanDesk\Session` |
| PDF preview browser profile | `%LOCALAPPDATA%\ScanDesk\WebView2` |

## Project layout

```
src/ScanDesk.Core          eSCL client, scan planning, discovery, document session, PDF export, settings
src/ScanDesk.App           WPF UI (MVVM, WebView2 preview)
tests/ScanDesk.Core.Tests  xUnit tests (fixtures captured from an HP ENVY 6400)
```
