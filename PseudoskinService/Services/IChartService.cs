using ScottPlot;
using System.Collections.Generic;

namespace PseudoSkinServices.ChartServices
{
    public interface IChartService
    {
        string XLabel { get; set; }
        string YLabel { get; set; }
        string Title { get; set; }
        double[] XData { get; set; }
        double[] YData { get; set; }
        string[] Labels { get; set; }
        Dictionary<string, double[]> YArray { get; set; } 
        Plot ChatArea { get; set; }
        void PlotChat();
        void SaveChat(string fileName);
        void SetAxisLimits(double xlBound, double xuBound, double ylBound, double yuBound);
        void AddScatterPlot(double[] xData, double[] yData, string label);
    }
}
