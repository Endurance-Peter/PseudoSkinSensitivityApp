using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PseudoSkinClient.ChartServices;
using PseudoSkinClient.Views;
using PseudoSkinClient.Views.UserCrontrols;
using PseudoSkinClient.Views.UserCrontrols.RankingViews;
using PseudoSkinClient.Views.UserCrontrols.RegressionViews;
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
            regionManager.RegisterViewWithRegion<HomeView>("ContentRegion");
            regionManager.RegisterViewWithRegion<RibbonTabSensitivityView>("SensitivityRibbonTab");
            regionManager.RegisterViewWithRegion<RibbonTabRegressionView>("RegressionRibbonTab");
            regionManager.RegisterViewWithRegion<RankingRibbonView>("RankingRibbonRegion");

            regionManager.RegisterViewWithRegion<AnisotropyChartView>("AnisotropyChartRegion");
            regionManager.RegisterViewWithRegion<WellboreRadiusChartView>("ChartWellboreRegion");
            regionManager.RegisterViewWithRegion<PenRatioChartView>("ChartPenRatioRegion");
            regionManager.RegisterViewWithRegion<ZmChartView>("ChartZmRegion");

            regionManager.RegisterViewWithRegion<ScarterChartView>("ScarterChartRegion");
            regionManager.RegisterViewWithRegion<PieChartView>("PieChartRegion");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterScoped<IChartService, ChartService>();
            containerRegistry.RegisterForNavigation<RibbonView>("Sensitivity");
            containerRegistry.RegisterForNavigation<RibbonView>("regression");
            containerRegistry.RegisterForNavigation<RankingView>("RankingView");

            containerRegistry.RegisterForNavigation<AnisotropySensitivityView>("AnisotropySensitivityView");
            containerRegistry.RegisterForNavigation<PenetrationRatioSensitivityView>("PenetrationRatioSensitivityView");
            containerRegistry.RegisterForNavigation<WellboreRadiusSensitivityView>("WellboreRadiusSensitivityView");
            containerRegistry.RegisterForNavigation<ZmSensitivityView>("ZmSensitivityView");
            
            containerRegistry.RegisterForNavigation<AnisotropyRegressionView>("AnisotropyRegressionView");
            containerRegistry.RegisterForNavigation<PenetrationRatioRegressionView>("PenetrationRatioRegressionView");
            containerRegistry.RegisterForNavigation<WellboreRegressionView>("WellboreRadiusRegressionView");
            containerRegistry.RegisterForNavigation<ZmRegressionView>("ZmRegressionView");
            //containerRegistry.RegisterForNavigation<ChatView>("ChartView");

            //containerRegistry.RegisterForNavigation<RegressionView>("RegressionView");
            containerRegistry.RegisterForNavigation<CreatePseudoSkinView>("CreatePseudoSkinView");
           
        }
    }
}
