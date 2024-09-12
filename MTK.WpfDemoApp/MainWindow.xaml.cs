using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MTK.WpfDemoApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IContainerExtension _container;
    private readonly IRegionManager _regionManager;

    public MainWindow(IContainerExtension container, IRegionManager regionManager)
    {
        _container = container;
        _regionManager = regionManager;
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        _regionManager.RequestNavigate("ContentRegion", "Index");
    }

    private void NavigateCounter(object sender, RoutedEventArgs e)
    {
        _regionManager.RequestNavigate("ContentRegion", "Counter");
    }
}