using Prism.Mvvm;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using System.Collections.ObjectModel;
using PseudoSkinApplication.RunSensititvity;
using PseudoSkinDataAccess.UnitOfWorks;
using PseudoSkinServices.Utility;
using Prism.Events;
using PseudoSkinApplication.Events;
using PseudoSkinServices;
using Prism.Services.Dialogs;
using Prism.Ioc;
using PseudoSkinServices.ChartServices;
using System.Windows;
using PseudoSkinServices.CalculatePseudoskin;
using PseudoSkinApplication;

namespace PseudoSkinClient.ViewModels.SensitivityViewModels
{
    public class AnisotropySensitivityViewModel : BindableBase
    {
        private readonly IDialogService dialogService;
        private readonly IChartService chartService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMediator mediator;
        private readonly IEventAggregator eventAggregator;
        private readonly IContainerProvider containerProvider;

        public AnisotropySensitivityViewModel(IDialogService dialogService, IChartService chartService,
                                IUnitOfWork unitOfWork, IMediator mediator, IEventAggregator eventAggregator, IContainerProvider containerProvider)
        {
            CalculateCommand = new DelegateCommand(CalculateAction);
            PlotCommand = new DelegateCommand(PlotAction);
            ExportCommand = new DelegateCommand(ExportToExcelAction);
            this.dialogService = dialogService;
            this.chartService = chartService;
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
            this.eventAggregator = eventAggregator;
            this.containerProvider = containerProvider;
            //eventAggregator.GetEvent<ExplorerSelectedToUpdateEvent>().Subscribe(SelectedPseudoskinAction);
        }

        private void ExportToExcelAction()
        {
            var excelService = new ExcelExportService();
            var pseudoskinresult = GetPseudoskinParameters();
            var parameterResults = GetParameterResults();
            excelService.ExportToExcel("INVESTIGATING THE IMPACT OF RESERVOIR AND WELL PARAMETERS", pseudoskinresult, parameterResults);
        }

        private Dictionary<string, object> GetPseudoskinParameters()
        {
            var parameter = unitOfWork.PseudoSkin.GetByName(x=>x.Name == selectedPseudoskin).Result;
            var results = new Dictionary<string, object>();
            results.Add(nameof(parameter.Name), parameter.Name);
            results.Add(nameof(parameter.DateCreated), parameter.DateCreated);
            results.Add(nameof(parameter.HorizontalPermeability), parameter.HorizontalPermeability);
            results.Add(nameof(parameter.VerticalPermeability), parameter.VerticalPermeability);
            results.Add(nameof(parameter.ReservoirThickness), parameter.ReservoirThickness);
            results.Add(nameof(parameter.WellboreRadius), parameter.WellboreRadius);
            results.Add(nameof(parameter.LenghtOfPerforationInterval), parameter.LenghtOfPerforationInterval);
            results.Add(nameof(parameter.DistanceFromTopOfSandToTopOfPerforation), parameter.DistanceFromTopOfSandToTopOfPerforation);

            return results;
        }

        private Dictionary<string, double[]> GetParameterResults()
        {
            var results = new Dictionary<string, double[]>();
            results.Add("Serial No.", Templates.Select(x => x.SerialNo).ToArray());
            results.Add("Anisotropy Value", Templates.Select(x => x.ParameterValue).ToArray());
            results.Add("Pseudoskin Value", Templates.Select(x => x.Pseudoskin).ToArray());

            return results;
        }

        private void PlotAction()
        {
            chartService.Title = "Anisotropy";
            chartService.XLabel = "Anisotropy Value";
            chartService.YLabel = "Pseudoskin Value";
            chartService.XData = Templates.Select(x => x.ParameterValue).ToArray();
            chartService.YData = Templates.Select(x => x.Pseudoskin).ToArray();

            dialogService.Show("ChatView");
        }

        private void CalculateAction()
        {
            var selectedPseudoskin = containerProvider.Resolve<SelectedPseudoskin>();
            var command = new RunSensitivityCommand
            {
                StartValue = startValue,
                StepVlue = stepValue,
                StopVlue = stopValue,
                SensititvityVariable = SensititvityVariable.Anisotropy,
                PseudoskinName = selectedPseudoskin.Name
            };

            if (selectedPseudoskin.Name == null)
            {
                MessageBox.Show("You haven't selected your Pseusoskin");
                return;
            }
            var response = mediator.ExecuteCommand<RunSensitivityCommand, RunSensitivityCommandHandler, List<RunSensitivityDto>>(command, new RunSensitivityCommandHandler(unitOfWork));

            SetItemSource(response.Result);
        }

        private void SetItemSource(List<RunSensitivityDto> runSensitivities)
        {
            Templates.Clear();
            foreach (var result in runSensitivities)
            {
                Templates.Add(new RunSensitivityDto
                {
                    SerialNo = result.SerialNo,
                    ParameterValue = result.ParameterValue.ToDecimalPlace(2),
                    Pseudoskin = result.Pseudoskin.ToDecimalPlace(2)
                });
            }
        }

        private void SelectedPseudoskinAction(string selectectedSkin)
        {
            SelectedPseudoskin = selectectedSkin;
        }
        private string selectedPseudoskin;
        public string SelectedPseudoskin
        {
            get { return selectedPseudoskin; }
            set { SetProperty(ref selectedPseudoskin, value); }
        }

        public ObservableCollection<RunSensitivityDto> Templates { get; set; } = new ObservableCollection<RunSensitivityDto>();

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
        public double StopValue
        {
            get { return stopValue; }
            set { SetProperty(ref stopValue, value); }
        }
        public DelegateCommand CalculateCommand { get; }
        public DelegateCommand PlotCommand { get; }
        public DelegateCommand ExportCommand { get; }
    }
}
