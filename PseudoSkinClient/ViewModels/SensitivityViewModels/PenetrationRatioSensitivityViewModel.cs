using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;

namespace PseudoSkinClient.ViewModels.SensitivityViewModels
{
    public class PenetrationRatioSensitivityViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        public PenetrationRatioSensitivityViewModel(IRegionManager regionManager)
        {
            RunPenetrationRatioCommand = new DelegateCommand(CreatePseudoSkinAction);
            this.regionManager = regionManager;
        }

        private void CreatePseudoSkinAction()
        {
            regionManager.RequestNavigate("ContentRegion", "AnisotropySensitivityView");
        }
        public DelegateCommand RunPenetrationRatioCommand { get; }
    }
}
