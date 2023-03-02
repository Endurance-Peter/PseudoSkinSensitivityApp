using Prism.Mvvm;
using Prism.Regions;

namespace PseudoSkinClient.ViewModels.SensitivityViewModels
{
    public class RibbonTabRegressionViewModel : BindableBase
    {
        public RibbonTabRegressionViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        private string isZmValue;

        private bool isAnisotropy;
        public bool IsAnisotropy
        {
            get { return isAnisotropy; }
            set
            {
                SetProperty(ref isAnisotropy, value);

                regionManager.RequestNavigate("ContentRegion", "AnisotropyRegressionView");
            }
        }
        private string isWellboreRadius;
        public string IsWellboreRadius
        {
            get { return isWellboreRadius; }
            set
            {
                SetProperty(ref isWellboreRadius, value);
                regionManager.RequestNavigate("ContentRegion", "WellboreRadiusRegressionView");
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
                regionManager.RequestNavigate("ContentRegion", "PenetrationRatioRegressionView");
            }
        }
        public string IsZmValue
        {
            get { return isZmValue; }
            set
            {
                SetProperty(ref isZmValue, value);
                regionManager.RequestNavigate("ContentRegion", "ZmRegressionView");
            }

        }
    }
}
