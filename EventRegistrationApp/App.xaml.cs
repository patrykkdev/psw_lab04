using System.Windows;

namespace EventRegistrationApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // globalna obsluga wyjatkow
            this.DispatcherUnhandledException += (sender, args) =>
            {
                MessageBox.Show($"Wystąpił nieoczekiwany błąd: {args.Exception.Message}", 
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Handled = true;
            };
        }
    }
}