using ScottPlot;

namespace PseudoSkinServices.ChartServices
{
    public interface IChartService
    {
        public string XLabel { get; set; }
        public string YLabel { get; set; }
        public string Title { get; set; }
        public double[] XData { get; set; }
        public double[] YData { get; set; }
        public Plot ChatArea { get; set; }
        public void PlotChat();
        public void SaveChat(string fileName);
        public void SetAxisLimits(double xlBound, double xuBound, double ylBound, double yuBound);
    }
}
