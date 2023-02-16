using PseudoSkinClient.ChartServices;
using PseudoSkinServices;
using PseudoSkinServices.ChartServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinClient
{
    public class Service : IAllService
    {
        public Service(IServiceProvider serviceProvider)
        {
            ChartService = new ChartService();
            ServiceProvider = serviceProvider;
        }
        public IChartService ChartService { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
    }
}
