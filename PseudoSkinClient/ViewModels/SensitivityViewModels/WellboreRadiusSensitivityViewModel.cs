using Prism.Mvvm;
using Prism.Commands;

namespace PseudoSkinClient.ViewModels.SensitivityViewModels
{
    public class WellboreRadiusSensitivityViewModel : BindableBase
    {
        public DelegateCommand CreatePseudoSkinCommand { get; }
    }
}
