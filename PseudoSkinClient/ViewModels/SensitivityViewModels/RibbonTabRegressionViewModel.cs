using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PseudoSkinApplication.Events;
using System;

namespace PseudoSkinClient.ViewModels.SensitivityViewModels
{
    public class RibbonTabRegressionViewModel : BindableBase
    {
        public RibbonTabRegressionViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            AnisotropyCheckedCommand = new DelegateCommand<string>(CheckedAction);
            WellboreRadiusCheckedCommand = new DelegateCommand<string>(CheckedAction);
            PenetratioRatioCheckedCommand = new DelegateCommand<string>(CheckedAction);
            ZmCheckedCommand = new DelegateCommand<string>(CheckedAction);
            RegressCommand = new DelegateCommand(RegressAction);

        }

        private void RegressAction()
        {
            if(IsAnisotropy)
            {
                eventAggregator.GetEvent<RegressEvent>().Publish(nameof(IsAnisotropy));
            }
            else if(IsWellboreRadius)
            {
                eventAggregator.GetEvent<RegressEvent>().Publish(nameof(IsWellboreRadius));
            }
            else if (IsPenetratioRatio)
            {
                eventAggregator.GetEvent<RegressEvent>().Publish(nameof(IsPenetratioRatio));
            }
            else
            {
                eventAggregator.GetEvent<RegressEvent>().Publish(nameof(IsZmValue));
            }
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
        public DelegateCommand RegressCommand { get; set; }

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
        private readonly IEventAggregator eventAggregator;

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
