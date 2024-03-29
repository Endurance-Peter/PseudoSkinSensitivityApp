﻿using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PseudoSkinApplication.CreatePseudoskin;
using PseudoSkinApplication.Events;
using PseudoSkinDataAccess.UnitOfWorks;
using PseudoSkinDomain.Models;
using PseudoSkinServices.CalculatePseudoskin;
using PseudoSkinServices.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Result = PseudoSkinApplication.CreatePseudoskin.Result;

namespace PseudoSkinClient.ViewModels
{
    public class CreatePseudoSkinViewModel : BindableBase, IDialogAware
    {
        public string Title => "Create Pseudoskin";
        private string? name;
        public string? Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        private double horizontalPermeability;
        public double HorizontalPermeability
        {
            get { return horizontalPermeability; }
            set { SetProperty(ref horizontalPermeability, value); }
        }
        private double verticalPermeability;
        public double VerticalPermeability
        {
            get { return verticalPermeability; }
            set { SetProperty(ref verticalPermeability, value); }
        }
        private double reservoirThickness;
        public double ReservoirThickness
        {
            get { return reservoirThickness; }
            set { SetProperty(ref reservoirThickness, value); }
        }

        private double wellboreRadius;
        public double WellboreRadius
        {
            get { return wellboreRadius; }
            set { SetProperty(ref wellboreRadius, value); }
        }

        private double lenghtOfPerforationInterval;
        public double LenghtOfPerforationInterval
        {
            get { return lenghtOfPerforationInterval; }
            set { SetProperty(ref lenghtOfPerforationInterval, value); }
        }
        private double distanceFromTopOfSandToTopOfPerforation;
        public double DistanceFromTopOfSandToTopOfPerforation
        {
            get { return distanceFromTopOfSandToTopOfPerforation; }
            set { SetProperty(ref distanceFromTopOfSandToTopOfPerforation, value); }
        }

        private double pseudoskinResult;
        public double PseudoskinResult
        {
            get { return pseudoskinResult; }
            set { SetProperty(ref pseudoskinResult, value); }
        }
        private bool isValidated = false;
        private readonly IRegionManager regionManager;
        private readonly IMediator mediator;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEventAggregator eventAggregator;

        public bool IsValidated
        {
            get { return isValidated; }
            set { SetProperty(ref isValidated, value); }
        }

        public CreatePseudoSkinViewModel(IRegionManager regionManager, IMediator mediator, IUnitOfWork unitOfWork, IEventAggregator eventAggregator)
        {
            CalculateCommand = new DelegateCommand(CalculateAction, CanExecute);
            SaveCommand = new DelegateCommand(SaveAction);
            DetailsCommand = new DelegateCommand(DetailsAction);
            this.regionManager = regionManager;
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
            this.eventAggregator = eventAggregator;

            eventAggregator.GetEvent<ParameterResultEvent>().Subscribe(ParameterResultEventAction);
            eventAggregator.GetEvent<ClearParameterResultEvent>().Subscribe(ClearParameterResultEventAction);
        }

        private void ClearParameterResultEventAction()
        {
            Name = null;
            HorizontalPermeability = 0;
            VerticalPermeability = 0;
            ReservoirThickness = 0;
            WellboreRadius = 0;
            LenghtOfPerforationInterval = 0;
            DistanceFromTopOfSandToTopOfPerforation = 0;
            PseudoskinResult = 0;
        }

        private void ParameterResultEventAction(PseudoSkin pseudoskin)
        {
            Name = pseudoskin.Name;
            HorizontalPermeability = pseudoskin.HorizontalPermeability;
            VerticalPermeability = pseudoskin.VerticalPermeability;
            ReservoirThickness = pseudoskin.ReservoirThickness;
            WellboreRadius = pseudoskin.WellboreRadius;
            LenghtOfPerforationInterval = pseudoskin.LenghtOfPerforationInterval;
            DistanceFromTopOfSandToTopOfPerforation = pseudoskin.DistanceFromTopOfSandToTopOfPerforation;

            var pseudoskinCalulation = new PseudoskinCalculation(HorizontalPermeability, VerticalPermeability, WellboreRadius,
                               ReservoirThickness, DistanceFromTopOfSandToTopOfPerforation, LenghtOfPerforationInterval);

            PseudoskinResult = pseudoskinCalulation.Calculate();
        }

        private void DetailsAction()
        {
            regionManager.RequestNavigate("ContentResultRegion", "PseudoskinParameterView");

            eventAggregator.GetEvent<OpenDetailsEvent>().Publish();
        }

        private bool CanExecute()
        {
            return true;
        }

        private void SaveAction()
        {
            var command = new CreatePseudoskinCommand
            {
                Name = name,
                DistanceFromTopOfSandToTopOfPerforation = distanceFromTopOfSandToTopOfPerforation,
                DateCreated = DateTime.UtcNow,
                HorizontalPermeability = horizontalPermeability,
                VerticalPermeability = verticalPermeability,
                LenghtOfPerforationInterval = lenghtOfPerforationInterval,
                ReservoirThickness = reservoirThickness,
                WellboreRadius = wellboreRadius
            };
            var response = mediator.ExecuteCommand<CreatePseudoskinCommand, CreatePseudoskinCommandHandler, Result>(command, new CreatePseudoskinCommandHandler(unitOfWork));

            if(response.Exception != null)
            {
                MessageBox.Show($"{response.Exception.Message}");
            }
            else
            {
                MessageBox.Show("Saved Successfully");
                eventAggregator.GetEvent<CreatePseudoskinResultEvent>().Publish(response.Result);
            }
        }

        private void CalculateAction()
        {
            var pseudoskin = new PseudoskinCalculation(horizontalPermeability, verticalPermeability, wellboreRadius, 
                                reservoirThickness, distanceFromTopOfSandToTopOfPerforation, lenghtOfPerforationInterval);

            PseudoskinResult = pseudoskin.Calculate();
            CanExecute();
            CalculateCommand.RaiseCanExecuteChanged();
        }

        public DelegateCommand CalculateCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand DetailsCommand { get; set; }
        public ObservableCollection<Results> Results { get; set; } = new ObservableCollection<Results>();
        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
           
        }
    }

    public class Results
    {
        public int SerialNo { get; set; }
        public double ParameterValue { get; set; }
        public int PseudoskinValue { get; set; }
    }
}