using Prism.Events;
using PseudoSkinApplication.Events;
using PseudoSkinServices.ChartServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PseudoSkinClient.ChartServices
{
    /// <summary>
    /// Interaction logic for PieChartView.xaml
    /// </summary>
    public partial class PieChartView : UserControl
    {
        private readonly IEventAggregator eventAggregator;

        public PieChartView(IEventAggregator eventAggregator, IChartService chartService)
        {
            InitializeComponent();
            this.eventAggregator = eventAggregator;
            ChartService = chartService;

            eventAggregator.GetEvent<PieChartEvent>().Subscribe(ChartAction);
        }

        private void ChartAction()
        {
            myChartView.Plot.Clear();
            ChartService.ChatArea = myChartView.Plot;
            var pie = ChartService.ChatArea.AddPie(ChartService.YData, true);
            pie.SliceLabels = ChartService.Labels;
            pie.ShowPercentages = true;
            ChartService.ChatArea.Legend();
            myChartView.Refresh();
        }

        public IChartService ChartService { get; }
    }
}
