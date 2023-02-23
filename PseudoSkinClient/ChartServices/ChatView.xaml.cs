using PseudoSkinServices.ChartServices;
using System.Windows;
using System.Windows.Controls;

namespace PseudoSkinClient.ChartServices
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : UserControl
    {
        public ChatView(IChartService service)
        {
            InitializeComponent();
            Service = service;
            Service.ChatArea = myChatView.Plot;
            Service.PlotChat();

            myChatView.Refresh();
            
        }

        public IChartService Service { get; set; }
        
    }
}
