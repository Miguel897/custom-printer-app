using System.Windows;

namespace ScanDesk.App;

public partial class App : Application
{
    private Mutex? _singleInstance;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // One instance only: all instances would share the same working session folder.
        _singleInstance = new Mutex(initiallyOwned: true, @"Local\ScanDesk.SingleInstance", out var isFirst);
        if (!isFirst)
        {
            MessageBox.Show("ScanDesk is already running.", "ScanDesk", MessageBoxButton.OK, MessageBoxImage.Information);
            Shutdown();
            return;
        }

        DispatcherUnhandledException += (_, args) =>
        {
            MessageBox.Show($"Unexpected error:\n\n{args.Exception.Message}", "ScanDesk", MessageBoxButton.OK, MessageBoxImage.Error);
            args.Handled = true;
        };

        MainWindow = new MainWindow();
        MainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _singleInstance?.Dispose();
        base.OnExit(e);
    }
}
