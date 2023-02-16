using PseudoSkinServices;
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

namespace PseudoSkinClient.ChartServices
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : Window
    {
        public ChatView(IAllService service)
        {
            InitializeComponent();
            Service = service;
            var chatArea = Service.ChartService;
            chatArea.ChatArea = myChatView.Plot;
            chatArea.PlotChat();

            myChatView.Refresh();
        }

        public IAllService Service { get; set; }
    }
}
