using Prism.Events;
using PseudoSkinApplication.Events;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PseudoSkinClient.Views.UserCrontrols
{
    /// <summary>
    /// Interaction logic for PseudoskinParameterView.xaml
    /// </summary>
    public partial class PseudoskinParameterView : UserControl
    {
        private readonly IEventAggregator eventAggregator;

        public PseudoskinParameterView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<OpenDetailsEvent>().Subscribe(OpenDetailsEventAction);
        }

        private void OpenDetailsEventAction()
        {
            this.Visibility = Visibility.Visible;
        }

        private void closeControl_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
