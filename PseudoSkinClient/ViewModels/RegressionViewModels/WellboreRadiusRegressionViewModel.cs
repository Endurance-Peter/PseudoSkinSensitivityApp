using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PseudoSkinServices;
using Prism.Events;
using Prism.Ioc;
using PseudoSkinServices.ChartServices;
using PseudoSkinApplication.Events;
using PseudoSkinApplication;
using PseudoSkinDataAccess.UnitOfWorks;
using PseudoSkinApplication.Regression.RunRegressionAnalysis;
using PseudoskinService.Services.Regression;
using System.Collections.ObjectModel;
using System.Windows;

namespace PseudoSkinClient.ViewModels.RegressionViewModels
{
    public class WellboreRadiusRegressionViewModel : BindableBase
    {
        private readonly IContainerProvider containerProvider;
        private readonly IEventAggregator eventAggregator;
        private readonly IChartService chartService;

        public WellboreRadiusRegressionViewModel(IContainerProvider containerProvider, IEventAggregator eventAggregator, IChartService chartService)
        {
            this.containerProvider = containerProvider;
            this.eventAggregator = eventAggregator;
            this.chartService = chartService;
            eventAggregator.GetEvent<RegressEvent>().Subscribe(RegressAction);
        }

        private void SetChartService(double[] x, double[] y, double[] y1)
        {
            chartService.YArray.Clear();

            chartService.Title = "Regressed Wellbore Radius";
            chartService.XLabel = "Wellbore Radius Value";
            chartService.YLabel = "Pseudoskin Value";
            chartService.XData = x;
            chartService.YArray.Add("Sensitivity", y);
            chartService.YArray.Add("Regression", y1);

            eventAggregator.GetEvent<WellboreChartEvent>().Publish();
        }
        private void RegressAction(string parameter)
        {
            if (parameter == ParameterVariables.WELLBORE_RADIUS)
            {
                Templates.Clear();
                var selectedPseudoskin = containerProvider.Resolve<SelectedPseudoskin>();
                var unitOfWork = containerProvider.Resolve<IUnitOfWork>();
                if (selectedPseudoskin.Name == null)
                {
                    MessageBox.Show("You haven't selected your Pseusoskin");
                    return;
                }
                var fetchSeudoskin = unitOfWork.PseudoSkin.GetByName(x => x.Name == selectedPseudoskin.Name);

                var regressionDto = new RegressionDto();
                var sensitivity = fetchSeudoskin.Result.SensitivityResults.Where(x => x.SensititvityVariable == PseudoSkinDomain.Models.SensititvityVariable.WellboreRadius).SelectMany(x => x.Results).ToList();

                if (sensitivity.Count == 0)
                {
                    MessageBox.Show("You have not run Wellbore Radius sensitivity");
                    return;
                }
                var skinValue = sensitivity.Select(x => x.PseudoskinValue).ToArray();
                var paramValue = sensitivity.Select(x => x.SensitivityValue).ToArray();
                var resgresionPredictedPseudoskinValue = RegressionAnalysis.FindPredictedBestFits(paramValue, skinValue);
                for (int i = 0; i < paramValue.Length; i++)
                {
                    Templates.Add(new RegressionDto
                    {
                        SerialNo = i + 1,
                        ParameterValues = paramValue[i].ToDecimalPlace(2),
                        SensititvitySkinValues = skinValue[i].ToDecimalPlace(2),
                        ResgresionPredictedPseudoskinValue = resgresionPredictedPseudoskinValue[i].ToDecimalPlace(2)
                    });
                }
                SetChartService(paramValue, skinValue, resgresionPredictedPseudoskinValue);
            }
        }

        public ObservableCollection<RegressionDto> Templates { get; set; } = new ObservableCollection<RegressionDto>();
    }
}
