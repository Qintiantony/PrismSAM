using PrismSAM.Modules.SWP.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismSAM.Core;

namespace PrismSAM.Modules.SWP
{
    public class SWPModule : IModule
    {
        private readonly IRegionManager regionManager;

        public SWPModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            regionManager.RegisterViewWithRegion(RegionNames.SpectrumRegion, typeof(SpetrumView));
            regionManager.RegisterViewWithRegion(RegionNames.BS_TrackerRegion, typeof(BS_TrackerView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}