using Prism.Mvvm;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace PseudoSkinClient.ViewModels
{
    public class TabRibbonViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IDialogService dialogService;
        public TabRibbonViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            this.regionManager = regionManager;
            this.dialogService = dialogService;
            CreatePseudoSkinCommand = new DelegateCommand(CreatePseudoSkinAction);
            SensitivityAnalysisCommand = new DelegateCommand(SensitivityAnalysisAction);
            RegressionAnalysisCommand = new DelegateCommand(RegressionAnalysisAction);
        }

        private void RegressionAnalysisAction()
        {
            regionManager.RequestNavigate("RibbonRegion", "regression");
        }

        private void SensitivityAnalysisAction()
        {
            regionManager.RequestNavigate("RibbonRegion", "Sensitivity");
        }

        private void CreatePseudoSkinAction()
        {
            dialogService.ShowDialog("CreatePseudoSkinView");
        }

        public DelegateCommand CreatePseudoSkinCommand { get; }
        public DelegateCommand SensitivityAnalysisCommand { get; }
        public DelegateCommand RegressionAnalysisCommand { get; }
        public DelegateCommand RankingCommand { get; }

    }
}
