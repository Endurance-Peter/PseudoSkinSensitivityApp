using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PseudoSkinApplication.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinClient.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IDialogService dialogService;
        private readonly IEventAggregator eventAggregator;

        public MainWindowViewModel(IDialogService dialogService, IEventAggregator eventAggregator)
        {
            CreateSkinCommand = new DelegateCommand(CreateSkinAction);
            this.dialogService = dialogService;
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<ExplorerSelectedPseudoskinEvent>().Subscribe(SelectedPseudoskinAction);
        }
        private void CreateSkinAction()
        {
            dialogService.ShowDialog("CreatePseudoSkinView");
        }
        private void SelectedPseudoskinAction(string selectectedSkin)
        {
            selectedPseudoskin = selectectedSkin;
            eventAggregator.GetEvent<ExplorerSelectedToUpdateEvent>().Publish(selectedPseudoskin);
        }
        private string selectedPseudoskin;
        

        public DelegateCommand CreateSkinCommand { get; }
    }
}
