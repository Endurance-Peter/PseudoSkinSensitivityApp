using Prism.Ioc;
using PseudoSkinClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PseudoSkinClient.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        private ShellViewModel _viewmodel;

        public ShellView(IContainerProvider containerProvider)
        {
            InitializeComponent();
            _viewmodel = containerProvider.Resolve<ShellViewModel>();
            DataContext = _viewmodel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //_viewmodel.OpenExplorerAction();
            explorerItems.Visibility = explorerItems.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
