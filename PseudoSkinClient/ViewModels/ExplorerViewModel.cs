using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly IEventAggregator eventAggregator;

        public ExplorerViewModel(IUnitOfWork unitOfWork, IEventAggregator eventAggregator)
        {
            this.unitOfWork = unitOfWork;
            this.eventAggregator = eventAggregator;
            PupolateExplorerView();
        }

        private void PupolateExplorerView()
        {
            var fetchskins = unitOfWork.PseudoSkin.GetAll();
            var skinNames = fetchskins.Result.Select(x => x.Name);

            foreach (var name in skinNames)
            {
                FetchedPseudoskins.Add(name);
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
                eventAggregator.GetEvent<ExplorerSelectedPseudoskinEvent>().Publish(selectedPseudoskin);
            }
        }
        public ObservableCollection<string> FetchedPseudoskins { get; set; } = new ObservableCollection<string>();

    }
}
