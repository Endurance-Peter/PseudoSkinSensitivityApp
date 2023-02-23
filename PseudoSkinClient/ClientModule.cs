using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PseudoSkinClient.ChartServices;
using PseudoSkinClient.Views;
using PseudoSkinClient.Views.UserCrontrols;
using PseudoSkinClient.Views.UserCrontrols.SensitivityViews;
using PseudoSkinServices.ChartServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinClient
{
    public class ClientModule : IModule
    {
        private readonly IRegionManager regionManager;

        public ClientModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {

            regionManager.RegisterViewWithRegion<TabRibbonView>("TabRibbonRegion");
            regionManager.RegisterViewWithRegion<ExplorerView>("ExplorerRegion");
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterScoped<IChartService, ChartService>();
            containerRegistry.RegisterForNavigation<RibbonView>("Sensitivity");
            containerRegistry.RegisterForNavigation<RibbonView>("regression");
            containerRegistry.RegisterForNavigation<AnisotropySensitivityView>("AnisotropySensitivityView");
            containerRegistry.RegisterForNavigation<PenetrationRatioSensitivityView>("PenetrationRatioSensitivityView");
            containerRegistry.RegisterForNavigation<WellboreRadiusSensitivityView>("WellboreRadiusSensitivityView");
            containerRegistry.RegisterForNavigation<ZmSensitivityView>("ZmSensitivityView");
           
        }
    }
}
