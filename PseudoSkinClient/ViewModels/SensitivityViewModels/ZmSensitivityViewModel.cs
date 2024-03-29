﻿using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using PseudoSkinDataAccess.UnitOfWorks;
using PseudoSkinServices.Utility;
using Prism.Events;
using PseudoSkinApplication.Events;
using PseudoSkinApplication.RunSensititvity;
using System.Collections.Generic;
using PseudoSkinServices;
using System.Collections.ObjectModel;
using PseudoSkinServices.ChartServices;
using Prism.Services.Dialogs;
using System.Linq;
using System.Windows;
using PseudoSkinServices.CalculatePseudoskin;
using PseudoSkinApplication;
using Prism.Ioc;

namespace PseudoSkinClient.ViewModels.SensitivityViewModels
{
    public class ZmSensitivityViewModel : BindableBase
    {
        private readonly IDialogService dialogService;
        private readonly IContainerProvider containerProvider;
        private readonly IChartService chartService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMediator mediator;
        private readonly IEventAggregator _eventAggregator;

        public ZmSensitivityViewModel(IDialogService dialogService, IContainerProvider containerProvider, IChartService chartService, IUnitOfWork unitOfWork, IMediator mediator, IEventAggregator eventAggregator)
        {
            CalculateCommand = new DelegateCommand(CalculateAction);
            PlotCommand = new DelegateCommand(PlotAction);
            this.dialogService = dialogService;
            this.containerProvider = containerProvider;
            this.chartService = chartService;
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
            this._eventAggregator = eventAggregator;
            //eventAggregator.GetEvent<ExplorerSelectedToUpdateEvent>().Subscribe(SelectedPseudoskinAction);
        }

        private void PlotAction()
        {
            chartService.Title = "Distance From Top Of Sand To Mid Of Perforation";
            chartService.XLabel = "Zm Value";
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
                SensititvityVariable = SensititvityVariable.DistanceFromTopOfSandToMidOfPerforation,
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
    }
}
