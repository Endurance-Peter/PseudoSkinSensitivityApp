using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinClient.ViewModels.SensitivityViewModels
{
    public class RibbonTabSensitivityViewModel : BindableBase
    {
        public RibbonTabSensitivityViewModel(IRegionManager regionManager)
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

                regionManager.RequestNavigate("ContentRegion", "AnisotropySensitivityView");
            }
        }
        private string isWellboreRadius;
        public string IsWellboreRadius
        {
            get { return isWellboreRadius; }
            set
            {
                SetProperty(ref isWellboreRadius, value);
                regionManager.RequestNavigate("ContentRegion", "WellboreRadiusSensitivityView");
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
                regionManager.RequestNavigate("ContentRegion", "PenetrationRatioSensitivityView");
            }
        }



        public string IsZmValue
        {
            get { return isZmValue; }
            set
            {
                SetProperty(ref isZmValue, value);
                regionManager.RequestNavigate("ContentRegion", "ZmSensitivityView");
            }

        }
    }
}
