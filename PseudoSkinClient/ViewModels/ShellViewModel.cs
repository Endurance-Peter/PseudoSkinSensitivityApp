using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using PseudoSkinApplication;
using PseudoSkinApplication.CreatePseudoskin;
using PseudoSkinApplication.Events;
using PseudoSkinDataAccess.UnitOfWorks;
using System.Collections.ObjectModel;
using System.Linq;

namespace PseudoSkinClient.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;
        private IUnitOfWork unitOfWork;

        public ShellViewModel(IContainerProvider containerProvider, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.containerProvider = containerProvider;
            this.regionManager = regionManager;
            CreatePseudoskinCommand = new DelegateCommand(CreatePseudoskinAction);
            //OpenExplorerCommand = new DelegateCommand(OpenExplorerAction);
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<CreatePseudoskinResultEvent>().Subscribe(AddNewCreatedSkinToListAction);
        }
        public void OpenExplorerAction()
        {
            unitOfWork = unitOfWork ?? containerProvider.Resolve<IUnitOfWork>();
            PupolateExplorerView();
        }

        private void AddNewCreatedSkinToListAction(Result result)
        {
            FetchedPseudoskins.Add(result.Name);
        }

        private void PupolateExplorerView()
        {
            if(FetchedPseudoskins.Count == 0)
            {
                var fetchskins = unitOfWork.PseudoSkin.GetAll();
                var skinNames = fetchskins.Result.Select(x => x.Name);

                foreach (var name in skinNames)
                {
                    FetchedPseudoskins?.Add(name);
                }
            }
        }

        private string selectedPseudoskin;
        public string SelectedPseudoskin
        {
            get
            {
                return selectedPseudoskin;
            }
            set
            {
                SetProperty(ref selectedPseudoskin, value);
                var selectedName = containerProvider.Resolve<SelectedPseudoskin>();
                selectedName.Name = selectedPseudoskin;
                //eventAggregator.GetEvent<ExplorerSelectedToUpdateEvent>().Publish(selectedPseudoskin);
            }
        }
        public ObservableCollection<string> FetchedPseudoskins { get; set; } = new ObservableCollection<string>();
        //private void SelectedPseudoskinAction(string selectectedSkin)
        //{
        //    selectedPseudoskin = selectectedSkin;
        //    eventAggregator.GetEvent<ExplorerSelectedToUpdateEvent>().Publish(selectedPseudoskin);
        //}

        private void CreatePseudoskinAction()
        {
            regionManager.RequestNavigate("ContentRegion", "CreatePseudoSkinView");
        }

        private readonly IContainerProvider containerProvider;
        private readonly IRegionManager regionManager;
        public DelegateCommand CreatePseudoskinCommand { get; set; }
        public DelegateCommand OpenExplorerCommand { get; set; }
    }
}
