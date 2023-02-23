using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using PseudoSkinClient.ChartServices;
using PseudoSkinClient.ViewModels;
using PseudoSkinClient.ViewModels.SensitivityViewModels;
using PseudoSkinClient.Views;
using PseudoSkinClient.Views.UserCrontrols;
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
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
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
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
            ViewModelLocationProvider.Register<AnisotropySensitivityView, AnisotropySensitivityViewModel>();
            ViewModelLocationProvider.Register<PenetrationRatioSensitivityView, PenetrationRatioSensitivityViewModel>();
            ViewModelLocationProvider.Register<RibbonView, RibbonViewModel>();
            ViewModelLocationProvider.Register<WellboreRadiusSensitivityView, WellboreRadiusSensitivityViewModel>();
            ViewModelLocationProvider.Register<ZmSensitivityView, ZmSensitivityViewModel>();
            ViewModelLocationProvider.Register<TabRibbonView, TabRibbonViewModel>();
        }
    }
}
