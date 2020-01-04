using PrismSAM.Modules.SysInfo.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismSAM.Core;

namespace PrismSAM.Modules.SysInfo
{
    public class SysInfoModule : IModule
    {
        private readonly IRegionManager regionManager;

        public SysInfoModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            regionManager.RegisterViewWithRegion(RegionNames.ConnectionRegion, typeof(Connection));
            regionManager.RegisterViewWithRegion(RegionNames.InformationRegion, typeof(Information));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}