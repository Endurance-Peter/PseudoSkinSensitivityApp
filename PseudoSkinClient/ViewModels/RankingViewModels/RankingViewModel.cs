using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using PseudoSkinApplication;
using PseudoSkinApplication.Events;
using PseudoSkinApplication.Regression.RunRegressionAnalysis;
using PseudoSkinDataAccess.UnitOfWorks;
using PseudoSkinDomain.Models;
using PseudoSkinServices.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using PseudoSkinServices;
using PseudoSkinServices.ChartServices;
using System.Linq;

namespace PseudoSkinClient.ViewModels.RankingViewModels
{
    public class RankingViewModel : BindableBase
    {
        public RankingViewModel(IEventAggregator eventAggregator, IChartService chartService)
        {
            this.eventAggregator = eventAggregator;
            this.chartService = chartService;
            eventAggregator.GetEvent<RankingResultEvent>().Subscribe(SetParameterActions);
        }
        private void SetPieChartService(double[] y)
        {
            chartService.Title = "Impact of Parameters";
            chartService.YData = y;
            chartService.Labels= new [] { "Anisotropy","Wellbore Radius", "Penetration Ratio", "Zm Value"};

            eventAggregator.GetEvent<PieChartEvent>().Publish();
        }

        private void SetBarChartService(double[] x, double[] x1, double[] x2, double[] x3, double[] x4)
        {
            chartService.YArray.Clear();
            chartService.Title = "Impact of Parameters";
            chartService.XLabel = "Parameters";
            chartService.YLabel = "Pseudoskin value";
            chartService.XData = x;
            chartService.YArray.Add("Anisotropy", x1);
            chartService.YArray.Add("Wellbore Radius", x2);
            chartService.YArray.Add("Penetration Ratio", x3);
            chartService.YArray.Add("Zm Value", x4);
            chartService.Labels = new[] { "Anisotropy", "Wellbore Radius", "Penetration Ratio", "Zm Value" };

            eventAggregator.GetEvent<BarChartEvent>().Publish();
        }
        private void SetParameterActions(Dictionary<SensititvityVariable, RunRegressionDto> response)
        {
            if (response.Count != 4)
            {
                MessageBox.Show("Not all parameters have been run Sensitivity");
                return;
            }
            var anisotropyResults = response[SensititvityVariable.Anisotropy];
            var wellboreRadiusResults = response[SensititvityVariable.WellboreRadius];
            var penetratioRatioResults = response[SensititvityVariable.PenetrationRatio];
            var ZmResults = response[SensititvityVariable.DistanceFromTopOfSandToMidOfPerforation];
            Templates.Clear();
            for (int i = 0; i < anisotropyResults.ParameterRegressValues.Length; i++)
            {
                Templates.Add(new RankingDto
                {
                    SerialNo = i + 1,
                    Value = anisotropyResults.ParameterRegressValues[i].ToDecimalPlace(4),
                    AnisotropyValue = anisotropyResults.ResgresionPredictedPseudoskinValue[i].ToDecimalPlace(4),
                    PenetrationRatioValue = penetratioRatioResults.ResgresionPredictedPseudoskinValue[i].ToDecimalPlace(4),
                    WellboreRadiusValue = wellboreRadiusResults.ResgresionPredictedPseudoskinValue[i].ToDecimalPlace(4),
                    ZmValue = ZmResults.ResgresionPredictedPseudoskinValue[i].ToDecimalPlace(4)
                });
            }

            var anisotropyAverage = anisotropyResults.ResgresionPredictedPseudoskinValue.Average();
            var wellboreRadiusAverage = wellboreRadiusResults.ResgresionPredictedPseudoskinValue.Average();
            var PenetrationRatioAverage = penetratioRatioResults.ResgresionPredictedPseudoskinValue.Average();
            var ZmAverage = ZmResults.ResgresionPredictedPseudoskinValue.Average();

            SetBarChartService(anisotropyResults.ParameterRegressValues, anisotropyResults.ResgresionPredictedPseudoskinValue, wellboreRadiusResults.ResgresionPredictedPseudoskinValue,
                                penetratioRatioResults.ResgresionPredictedPseudoskinValue, ZmResults.ResgresionPredictedPseudoskinValue);
            SetPieChartService(new double[] { anisotropyAverage, wellboreRadiusAverage, PenetrationRatioAverage, ZmAverage });
        }
        
        private readonly IEventAggregator eventAggregator;
        private readonly IChartService chartService;

        public ObservableCollection<RankingDto> Templates { get; set; } = new ObservableCollection<RankingDto>();
    }


    public class RankingRiboonViewModel : BindableBase
    {
        public RankingRiboonViewModel(IUnitOfWork unitOfWork, IRegionManager regionManager, IEventAggregator eventAggregator, IMediator mediator, IContainerProvider containerProvider)
        {
            RegressAllCommand = new DelegateCommand(RegressAllAction);
            this.unitOfWork = unitOfWork;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.mediator = mediator;
            this.containerProvider = containerProvider;
        }
        public void RegressAllAction()
        {
            regionManager.RequestNavigate("ContentRegion", "RankingView");
            var selectedPseudoskin = containerProvider.Resolve<SelectedPseudoskin>();
            var command = new RunRegressionAnalysisCommand { PseudoskinName = selectedPseudoskin.Name, StartValue = StartValue, StepValue = StepValue, StopValue = StopValue };
            var response = mediator.ExecuteCommand<RunRegressionAnalysisCommand, RunRegressionAnalysisCommandHandler,
                                                Dictionary<SensititvityVariable, RunRegressionDto>>(command, new RunRegressionAnalysisCommandHandler(unitOfWork));

            eventAggregator.GetEvent<RankingResultEvent>().Publish(response.Result);

        }

        private double startValue;
        public double StartValue
        {
            get { return startValue; }
            set { SetProperty(ref startValue, value); }
        }
        private double stepValue;
        public double StepValue
        {
            get { return stepValue; }
            set { SetProperty(ref stepValue, value); }
        }

        private double stopValue;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly IMediator mediator;
        private readonly IContainerProvider containerProvider;

        public double StopValue
        {
            get { return stopValue; }
            set { SetProperty(ref stopValue, value); }
        }
        public DelegateCommand RegressAllCommand { get; set; }
        public ObservableCollection<RankingDto> Templates { get; set; } = new ObservableCollection<RankingDto>();

    }
}
