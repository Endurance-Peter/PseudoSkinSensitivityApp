using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using PseudoSkinApplication;
using PseudoSkinClient.ChartServices;
using PseudoSkinClient.ViewModels;
using PseudoSkinClient.ViewModels.RankingViewModels;
using PseudoSkinClient.ViewModels.RegressionViewModels;
using PseudoSkinClient.ViewModels.SensitivityViewModels;
using PseudoSkinClient.Views;
using PseudoSkinClient.Views.UserCrontrols;
using PseudoSkinClient.Views.UserCrontrols.RankingViews;
using PseudoSkinClient.Views.UserCrontrols.RegressionViews;
using PseudoSkinClient.Views.UserCrontrols.SensitivityViews;
using PseudoSkinDataAccess;
using PseudoSkinDataAccess.UnitOfWorks;
using PseudoSkinServices;
using PseudoSkinServices.ChartServices;
using PseudoSkinServices.Utility;
using System.Windows;

namespace PseudoSkinClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<ChatView>();

            containerRegistry.RegisterDialog<CreatePseudoSkinView>();
            containerRegistry.RegisterScoped<IMediator, Mediator>();
            containerRegistry.RegisterSingleton<SelectedPseudoskin>();
        }
        protected override Window CreateShell()
        {
            //return Container.Resolve<MainWindow>();
            return Container.Resolve<ShellView>();
        }
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();
            catalog.AddModule<DataAccessModule>();
            catalog.AddModule<ClientModule>();
            //catalog.AddModule<ServiceModule>();
            return catalog;
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<ExplorerView, ExplorerViewModel>();
            //ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
            //ViewModelLocationProvider.Register<ShellView, ShellViewModel>();
            ViewModelLocationProvider.Register<AnisotropySensitivityView, AnisotropySensitivityViewModel>();
            ViewModelLocationProvider.Register<PenetrationRatioSensitivityView, PenetrationRatioSensitivityViewModel>();
            ViewModelLocationProvider.Register<WellboreRadiusSensitivityView, WellboreRadiusSensitivityViewModel>();
            ViewModelLocationProvider.Register<ZmSensitivityView, ZmSensitivityViewModel>();
            
            ViewModelLocationProvider.Register<AnisotropyRegressionView, AnisotropyRegressionViewModel>();
            ViewModelLocationProvider.Register<PenetrationRatioRegressionView, PenetrationRatioRegressionViewModel>();
            ViewModelLocationProvider.Register<WellboreRegressionView, WellboreRadiusRegressionViewModel>();
            ViewModelLocationProvider.Register<ZmRegressionView, ZmRegressionViewModel>();


            ViewModelLocationProvider.Register<RibbonView, RibbonViewModel>();
            ViewModelLocationProvider.Register<TabRibbonView, TabRibbonViewModel>();
            ViewModelLocationProvider.Register<RibbonTabSensitivityView, RibbonTabSensitivityViewModel>();
            ViewModelLocationProvider.Register<RibbonTabRegressionView, RibbonTabRegressionViewModel>();

            ViewModelLocationProvider.Register<RankingRibbonView, RankingRiboonViewModel>();
            ViewModelLocationProvider.Register<RankingView, RankingViewModel>();

            ViewModelLocationProvider.Register<PseudoskinParameterView, PseudoskinParameterViewModel>();
        }
    }
}
