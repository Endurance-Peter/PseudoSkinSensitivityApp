using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using PseudoSkinApplication;
using PseudoSkinApplication.CreatePseudoskin;
using PseudoSkinApplication.Events;
using PseudoSkinDataAccess.UnitOfWorks;
using PseudoSkinDomain.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PseudoSkinClient.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;
        private IUnitOfWork unitOfWork;
        private PseudoSkin _pseudoskin;

        public ShellViewModel(IContainerProvider containerProvider, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.containerProvider = containerProvider;
            this.regionManager = regionManager;
            CreatePseudoskinCommand = new DelegateCommand(CreatePseudoskinAction);
            ViewDetailsCommand = new DelegateCommand(ViewDetailsAction);
            //OpenExplorerCommand = new DelegateCommand(OpenExplorerAction);
            this.eventAggregator = eventAggregator;
            //eventAggregator.GetEvent<CreatePseudoskinResultEvent>().Subscribe(AddNewCreatedSkinToListAction);
            eventAggregator.GetEvent<ExplorerSelectedPseudoskinEvent>().Subscribe(SelectedPseudoskinEventAction);
        }

        private void ViewDetailsAction()
        {
            unitOfWork = unitOfWork ?? containerProvider.Resolve<IUnitOfWork>();
            _pseudoskin = unitOfWork.PseudoSkin.GetByName(x => x.Name == CurrentSkinName).Result;
            
            regionManager.RequestNavigate("ContentRegion", "CreatePseudoSkinView");

            eventAggregator.GetEvent<ParameterResultEvent>().Publish(_pseudoskin);
        }

        private void SelectedPseudoskinEventAction(string selectedPseudoskin)
        {
            CurrentSkinName = selectedPseudoskin;
        }

        //public void OpenExplorerAction()
        //{
        //    unitOfWork = unitOfWork ?? containerProvider.Resolve<IUnitOfWork>();
        //    PupolateExplorerView();
        //}

        //private void AddNewCreatedSkinToListAction(Result result)
        //{
        //    FetchedPseudoskins.Add(result.Name);
        //}

        //private void PupolateExplorerView()
        //{
        //    if(FetchedPseudoskins.Count == 0)
        //    {
        //        var fetchskins = unitOfWork.PseudoSkin.GetAll();
        //        var skinNames = fetchskins.Result.Select(x => x.Name);

        //        foreach (var name in skinNames)
        //        {
        //            FetchedPseudoskins?.Add(name);
        //        }
        //        SelectedPseudoskin = FetchedPseudoskins[0];
        //    }
        //}

        //private string selectedPseudoskin;
        //public string SelectedPseudoskin
        //{
        //    get
        //    {
        //        return selectedPseudoskin;
        //    }
        //    set
        //    {
        //        SetProperty(ref selectedPseudoskin, value);
        //        var selectedName = containerProvider.Resolve<SelectedPseudoskin>();
        //        selectedName.Name = selectedPseudoskin;
        //        CurrentSkinName = selectedPseudoskin;
        //    }
        //}

        private string currentSkinName;
        public string CurrentSkinName
        {
            get { return currentSkinName; }
            set 
            { 
                SetProperty(ref currentSkinName, value); 
            }
        }
        //public ObservableCollection<string> FetchedPseudoskins { get; set; } = new ObservableCollection<string>();
        

        private void CreatePseudoskinAction()
        {
            regionManager.RequestNavigate("ContentRegion", "CreatePseudoSkinView");
            eventAggregator.GetEvent<ClearParameterResultEvent>().Publish();
        }

        private readonly IContainerProvider containerProvider;
        private readonly IRegionManager regionManager;
        public DelegateCommand CreatePseudoskinCommand { get; set; }
        public DelegateCommand OpenExplorerCommand { get; set; }
        public DelegateCommand ViewDetailsCommand { get; set; }
    }
}
