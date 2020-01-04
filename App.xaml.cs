using PrismSAM.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using PrismSAM.Modules.SysInfo;
using Infragistics.Themes;
using PrismSAM.Modules.SWP;
using PrismSAM.Modules.IQRecorder;

namespace PrismSAM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            ThemeManager.ApplicationTheme = new RoyalLightTheme();
            return Container.Resolve<MainWindow>();
            
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<SysInfoModule>();
            moduleCatalog.AddModule<SWPModule>();
            moduleCatalog.AddModule<IQRecorderModule>();
        }
    }
}
