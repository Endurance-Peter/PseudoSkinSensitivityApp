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

namespace PseudoSkinClient.ViewModels.SensitivityViewModels
{
    public class AnisotropySensitivityViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMediator mediator;
        private readonly IEventAggregator eventAggregator;

        public AnisotropySensitivityViewModel(IRegionManager regionManager,IUnitOfWork unitOfWork, IMediator mediator, IEventAggregator eventAggregator)
        {
            CalculateCommand = new DelegateCommand(CalculateAction);
            PlotCommand = new DelegateCommand(PlotAction);
            this.regionManager = regionManager;
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<ExplorerSelectedToUpdateEvent>().Subscribe(SelectedPseudoskinAction);
        }

        private void PlotAction()
        {
            
        }

        private void CalculateAction()
        {
            var command = new RunSensitivityCommand
            {
                StartValue = startValue,
                StepVlue = stepValue,
                StopVlue = stopValue,
                SensititvityVariable = PseudoSkinDomain.Models.SensititvityVariable.Anisotropy,
                PseudoskinName = SelectedPseudoskin
            };
            var response = mediator.ExecuteCommand<RunSensitivityCommand, RunSensitivityCommandHandler, List<RunSensitivityDto>>(command, new RunSensitivityCommandHandler(unitOfWork));

            SetItemSource(response.Result);
        }

        private void SetItemSource(List<RunSensitivityDto> runSensitivities)
        {
            foreach(var result in runSensitivities)
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
    }
}
