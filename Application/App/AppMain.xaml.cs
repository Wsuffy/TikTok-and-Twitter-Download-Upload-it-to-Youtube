using System.Windows;
using System.Windows.Controls;
using Application.Windows.Main;

namespace Application.App;

public partial class AppMain : System.Windows.Application
{
    private readonly MainWindowView _mainWindowView;
    
    public AppMain(MainWindowView mainWindowView)
    {
        _mainWindowView = mainWindowView;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _mainWindowView.Show();
        base.OnStartup(e);
    }
}