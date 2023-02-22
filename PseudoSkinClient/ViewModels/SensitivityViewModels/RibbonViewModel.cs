using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinClient.ViewModels.SensitivityViewModels
{
    public class RibbonViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        public RibbonViewModel(IRegionManager regionManager)
        {
            AnisotropyCommand = new DelegateCommand(OpenAnisotropySensitivityViewAction);
            WellboreRadiusCommand = new DelegateCommand(OpenWellboreRadiusCommandSensitivityViewAction);
            PenetrationRatioCommand = new DelegateCommand(OpenPenetrationRatioViewAction);
            ZmCommand = new DelegateCommand(OpenZmViewAction);
            this.regionManager = regionManager;
        }

        private void OpenZmViewAction()
        {
            regionManager.RequestNavigate("ContentRegion", "ZmSensitivityView");
        }

        private void OpenPenetrationRatioViewAction()
        {
            regionManager.RequestNavigate("ContentRegion", "PenetrationRatioSensitivityView");
        }

        private void OpenWellboreRadiusCommandSensitivityViewAction()
        {
            regionManager.RequestNavigate("ContentRegion", "WellboreRadiusSensitivityView");
        }

        private void OpenAnisotropySensitivityViewAction()
        {
            regionManager.RequestNavigate("ContentRegion", "AnisotropySensitivityView");
        }

        public DelegateCommand AnisotropyCommand { get; }
        public DelegateCommand WellboreRadiusCommand { get; }
        public DelegateCommand PenetrationRatioCommand { get; }
        public DelegateCommand ZmCommand { get; }
    }
}
