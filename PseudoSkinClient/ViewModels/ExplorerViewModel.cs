using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PseudoSkinApplication;
using PseudoSkinApplication.CreatePseudoskin;
using PseudoSkinApplication.Events;
using PseudoSkinDataAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinClient.ViewModels
{
    public class ExplorerViewModel : BindableBase
    {
        private readonly IContainerProvider containerProvider;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEventAggregator eventAggregator;

        public ExplorerViewModel(IContainerProvider containerProvider, IUnitOfWork unitOfWork, IEventAggregator eventAggregator)
        {
            this.containerProvider = containerProvider;
            this.unitOfWork = unitOfWork;
            this.eventAggregator = eventAggregator;
            PupolateExplorerView();
            eventAggregator.GetEvent<CreatePseudoskinResultEvent>().Subscribe(AddNewCreatedSkinToListAction);
        }

        private void AddNewCreatedSkinToListAction(Result result)
        {
            FetchedPseudoskins.Add(result.Name);
            SelectedPseudoskin = result.Name;
        }

        private void PupolateExplorerView()
        {
            if (FetchedPseudoskins.Count == 0)
            {
                var fetchskins = unitOfWork.PseudoSkin.GetAll();
                var skinNames = fetchskins.Result.Select(x => x.Name);

                foreach (var name in skinNames)
                {
                    FetchedPseudoskins?.Add(name);
                }
                SelectedPseudoskin = FetchedPseudoskins[0];
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

                var pseudoskin = unitOfWork.PseudoSkin.GetByName(x => x.Name == selectedPseudoskin).Result;
                eventAggregator.GetEvent<ExplorerSelectedPseudoskinEvent>().Publish(selectedPseudoskin);
                eventAggregator.GetEvent<ParameterResultEvent>().Publish(pseudoskin);

                var selectedName = containerProvider.Resolve<SelectedPseudoskin>();
                selectedName.Name = selectedPseudoskin;
            }
        }
        public ObservableCollection<string> FetchedPseudoskins { get; set; } = new ObservableCollection<string>();

    }
}
