using PrismSAM.Modules.IQRecorder.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismSAM.Core;

namespace PrismSAM.Modules.IQRecorder
{
    public class IQRecorderModule : IModule
    {
        private readonly IRegionManager regionManager;

        public IQRecorderModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            regionManager.RegisterViewWithRegion(RegionNames.RecorderRegion, typeof(RecorderView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}