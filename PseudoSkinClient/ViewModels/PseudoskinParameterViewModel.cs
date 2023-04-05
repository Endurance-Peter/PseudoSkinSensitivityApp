using Prism.Events;
using Prism.Mvvm;
using PseudoSkinApplication.Events;
using PseudoSkinDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinClient.ViewModels
{
    public class PseudoskinParameterViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;

        public PseudoskinParameterViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ParameterResultEvent>().Subscribe(ParameterResultEventAction);
            eventAggregator.GetEvent<ClearParameterResultEvent>().Subscribe(ClearParameterResultEventAction);
            this.eventAggregator = eventAggregator;
        }

        private void ClearParameterResultEventAction()
        {
            AnisotropyResult = 0;
            WellboreRadiusResult = 0;
            PenetrationRatioResult = 0;
            zmResult = 0;
        }

        private void ParameterResultEventAction(PseudoSkin pseudoskin)
        {
            AnisotropyResult = pseudoskin.HorizontalPermeability / pseudoskin.VerticalPermeability;
            WellboreRadiusResult = pseudoskin.WellboreRadius;
            PenetrationRatioResult = pseudoskin.ReservoirThickness / pseudoskin.LenghtOfPerforationInterval;
            ZmResult = pseudoskin.DistanceFromTopOfSandToTopOfPerforation + (pseudoskin.LenghtOfPerforationInterval / 2);
        }

        private double anisotropyResult;
        public double AnisotropyResult
        {
            get { return anisotropyResult; }
            set { SetProperty(ref anisotropyResult, value); }
        }

        private double wellboreRadiusResult;
        public double WellboreRadiusResult
        {
            get { return wellboreRadiusResult; }
            set { SetProperty(ref wellboreRadiusResult, value); }
        }

        private double penetrationRatioResult;
        public double PenetrationRatioResult
        {
            get { return penetrationRatioResult; }
            set { SetProperty(ref penetrationRatioResult, value); }
        }

        private double zmResult;
        public double ZmResult
        {
            get { return zmResult; }
            set { SetProperty(ref zmResult, value); }
        }
    }
}
