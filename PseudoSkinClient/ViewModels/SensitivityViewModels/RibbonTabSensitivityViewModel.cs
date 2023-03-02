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
    public class RibbonTabSensitivityViewModel : BindableBase
    {
        public RibbonTabSensitivityViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            AnisotropyCheckedCommand = new DelegateCommand<string>(CheckedAction);
            WellboreRadiusCheckedCommand = new DelegateCommand<string>(CheckedAction);
            PenetratioRatioCheckedCommand = new DelegateCommand<string>(CheckedAction);
            ZmCheckedCommand = new DelegateCommand<string>(CheckedAction);
        }

        private void CheckedAction(string parameter)
        {
            if(parameter == nameof(IsAnisotropy))
            {
                regionManager.RequestNavigate("ContentRegion", "AnisotropySensitivityView");
            }
            else if(parameter == nameof(IsWellboreRadius))
            {
                regionManager.RequestNavigate("ContentRegion", "WellboreRadiusSensitivityView");
            }
            else if (parameter == nameof(IsPenetratioRatio))
            {
                regionManager.RequestNavigate("ContentRegion", "PenetrationRatioSensitivityView");
            }
            else
            {
                regionManager.RequestNavigate("ContentRegion", "ZmSensitivityView");

            }

        }

        public DelegateCommand<string> AnisotropyCheckedCommand { get; set; }
        public DelegateCommand<string> WellboreRadiusCheckedCommand { get; set; }
        public DelegateCommand<string> PenetratioRatioCheckedCommand { get; set; }
        public DelegateCommand<string> ZmCheckedCommand { get; set; }
        private bool isZmValue;

        private bool isAnisotropy;
        public bool IsAnisotropy
        {
            get { return isAnisotropy; }
            set
            {
                SetProperty(ref isAnisotropy, value);

            }
        }
        private bool isWellboreRadius;
        public bool IsWellboreRadius
        {
            get { return isWellboreRadius; }
            set
            {
                SetProperty(ref isWellboreRadius, value);
               
            }
        }

        private bool isPenetrationRatio;
        private readonly IRegionManager regionManager;

        public bool IsPenetratioRatio
        {
            get { return isPenetrationRatio; }
            set
            {
                SetProperty(ref isPenetrationRatio, value);
               
            }
        }



        public bool IsZmValue
        {
            get { return isZmValue; }
            set
            {
                SetProperty(ref isZmValue, value);
            }

        }
    }
}
