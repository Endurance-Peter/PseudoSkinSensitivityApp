using Prism.Mvvm;
using Prism.Commands;

namespace PseudoSkinClient.ViewModels.SensitivityViewModels
{
    public class ZmSensitivityViewModel : BindableBase
    {
        public DelegateCommand CreatePseudoSkinCommand { get; }
    }
}
