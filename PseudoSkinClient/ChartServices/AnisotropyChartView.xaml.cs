﻿using Prism.Events;
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
    /// Interaction logic for AnisotropyChartView.xaml
    /// </summary>
    public partial class AnisotropyChartView : UserControl
    {
        private readonly IEventAggregator eventAggregator;

        public AnisotropyChartView(IEventAggregator eventAggregator, IChartService chartService)
        {
            InitializeComponent();

            this.eventAggregator = eventAggregator;
            ChartService = chartService;
            eventAggregator.GetEvent<AnisotropyChartEvent>().Subscribe(ChartAction);
        }

        private void ChartAction()
        {
            myChatView.Plot.Clear();
            ChartService.ChatArea = myChatView.Plot;

            foreach (var item in ChartService.YArray)
            {
                ChartService.AddScatterPlot(ChartService.XData, item.Value, item.Key);
            }

            ChartService.ChatArea.Legend(location: ScottPlot.Alignment.UpperRight);
            myChatView.Refresh();
        }

        public IChartService ChartService { get; }
    }
}
