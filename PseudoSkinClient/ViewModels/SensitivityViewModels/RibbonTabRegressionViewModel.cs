using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PseudoSkinClient.ViewModels.SensitivityViewModels
{
    public class RibbonTabRegressionViewModel : BindableBase
    {
        public RibbonTabRegressionViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            AnisotropyCheckedCommand = new DelegateCommand<string>(CheckedAction);
            WellboreRadiusCheckedCommand = new DelegateCommand<string>(CheckedAction);
            PenetratioRatioCheckedCommand = new DelegateCommand<string>(CheckedAction);
            ZmCheckedCommand = new DelegateCommand<string>(CheckedAction);
        }
        private void CheckedAction(string parameter)
        {
            if (parameter == nameof(IsAnisotropy))
            {
                regionManager.RequestNavigate("ContentRegion", "AnisotropyRegressionView");
            }
            else if (parameter == nameof(IsWellboreRadius))
            {
                regionManager.RequestNavigate("ContentRegion", "WellboreRadiusRegressionView");
            }
            else if (parameter == nameof(IsPenetratioRatio))
            {
                regionManager.RequestNavigate("ContentRegion", "PenetrationRatioRegressionView");
            }
            else
            {
                regionManager.RequestNavigate("ContentRegion", "ZmRegressionView");

            }

        }
        public DelegateCommand<string> AnisotropyCheckedCommand { get; set; }
        public DelegateCommand<string> WellboreRadiusCheckedCommand { get; set; }
        public DelegateCommand<string> PenetratioRatioCheckedCommand { get; set; }
        public DelegateCommand<string> ZmCheckedCommand { get; set; }
        private string isZmValue;

        private bool isAnisotropy;
        public bool IsAnisotropy
        {
            get { return isAnisotropy; }
            set
            {
                SetProperty(ref isAnisotropy, value);

               
            }
        }
        private string isWellboreRadius;
        public string IsWellboreRadius
        {
            get { return isWellboreRadius; }
            set
            {
                SetProperty(ref isWellboreRadius, value);
               
            }
        }

        private string isPenetrationRatio;
        private readonly IRegionManager regionManager;

        public string IsPenetratioRatio
        {
            get { return isPenetrationRatio; }
            set
            {
                SetProperty(ref isPenetrationRatio, value);
               
            }
        }
        public string IsZmValue
        {
            get { return isZmValue; }
            set
            {
                SetProperty(ref isZmValue, value);
                
            }

        }
    }
}
