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
    /// Interaction logic for DefaultChatView.xaml
    /// </summary>
    public partial class DefaultChatView : UserControl
    {
        private readonly IEventAggregator eventAggregator;

        public DefaultChatView(IEventAggregator eventAggregator, IChartService chartService)
        {
            InitializeComponent();

            this.eventAggregator = eventAggregator;
            ChartService = chartService;
            eventAggregator.GetEvent<ChartEvent>().Subscribe(ChartAction);
        }

        private void ChartAction()
        {
            myChatView.Plot.Clear();
            ChartService.ChatArea = myChatView.Plot;
            ChartService.PlotChat();

            myChatView.Refresh();
        }

        public IChartService ChartService { get; }
    }
}
