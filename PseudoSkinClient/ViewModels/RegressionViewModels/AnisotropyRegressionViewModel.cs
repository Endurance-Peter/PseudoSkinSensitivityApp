using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using PseudoSkinApplication;
using PseudoSkinApplication.Events;
using PseudoSkinApplication.Regression.RunRegressionAnalysis;
using PseudoSkinDataAccess.UnitOfWorks;
using PseudoskinService.Services.Regression;
using PseudoSkinServices;
using PseudoSkinServices.ChartServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PseudoSkinClient.ViewModels.RegressionViewModels
{
    public class AnisotropyRegressionViewModel : BindableBase
    {
        private readonly IContainerProvider containerProvider;
        private readonly IEventAggregator eventAggregator;
        private readonly IChartService chartService;

        public AnisotropyRegressionViewModel(IContainerProvider containerProvider, IEventAggregator eventAggregator, IChartService chartService)
        {
            this.containerProvider = containerProvider;
            this.eventAggregator = eventAggregator;
            this.chartService = chartService;
            eventAggregator.GetEvent<RegressEvent>().Subscribe(RegressAction);
        }

        private void SetChartService(double[] x, double[] y, double[] y1)
        {
            chartService.Title = "Regressed Anisotropy";
            chartService.XLabel = "Anisotropy Value";
            chartService.YLabel = "Pseudoskin Value";
            chartService.XData = x;
            chartService.YData = y;
            chartService.Y1Data = y1;

            eventAggregator.GetEvent<AnisotropyChartEvent>().Publish();
        }
        private void RegressAction(string parameter)
        {
            if (parameter == ParameterVariables.ANISOTROPY)
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
                var sensitivity = fetchSeudoskin.Result.SensitivityResults.Where(x=> x.SensititvityVariable == PseudoSkinDomain.Models.SensititvityVariable.Anisotropy).SelectMany(x => x.Results);
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
        //public DelegateCommand RegressCommand { get; set; }

    }
}
