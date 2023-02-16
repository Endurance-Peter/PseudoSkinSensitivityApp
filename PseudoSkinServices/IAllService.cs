using PseudoSkinServices.ChartServices;
using System;

namespace PseudoSkinServices
{
    public interface IAllService
    {
        public IChartService ChartService { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
    }
}
