using System.Windows;

namespace CompositeObservableCollection
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow { DataContext = new MainWindowViewModel() };
            mainWindow.Show();
        }
    }
}
