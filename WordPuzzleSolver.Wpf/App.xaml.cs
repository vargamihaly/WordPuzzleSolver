using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using WordPuzzleSolver.Wpf.ServiceCollectionExtensions;
using WordPuzzleSolver.Wpf.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace WordPuzzleSolver.Wpf
{
    public partial class App
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF5cWWJCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWH1ccHVUQmVfVUZ0WEM=");

            DispatcherUnhandledException += App_DispatcherUnhandledException;

            try
            {
                ServiceProvider = ConfigureServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources["Caption_Error"] as string, MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            
            InitializeComponent();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection()
                .ConfigureScrutor()
                .AddLogging(configure => configure.AddConsole().AddDebug());
            
            return services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        public IServiceProvider ServiceProvider { get; }

        public new static App Current => (App)Application.Current;

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var sb = new StringBuilder();
            if (e.Exception.InnerException != null)
            {
                sb.AppendLine(e.Exception.InnerException.ToString());
            }

            sb.AppendLine(e.Exception.ToString());

            if (e.Exception is ConfigurationException) return;
            e.Handled = true;
            MessageBox.Show(Resources["UnhandledException"] as string, Resources["Caption_Error"] as string, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
