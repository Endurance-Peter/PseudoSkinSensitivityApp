using PseudoSkinServices.ChartServices;
using ScottPlot;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PseudoSkinClient.ChartServices
{
    public class ChartService : IChartService
    {
        private void SetTitleAndLebel()
        {
            ChatArea?.Title(Title);
            ChatArea.XLabel(XLabel);
            ChatArea.YLabel(YLabel);
        }
        public void AddScatterPlot(double[] xData, double[] yData)
        {
            
            ChatArea?.AddScatter(xData, yData, lineWidth: 2, markerSize: 5);
        }

        public void AddScatterPlot(double[] xData, double[] yData, string label)
        {
            SetTitleAndLebel();
            ChatArea?.AddScatter(xData, yData, label: label, lineWidth: 2, markerSize: 5);
        }

        public void AddPiePlot(double[] xData, double[] yData)
        {

            ChatArea?.AddPie(xData);
        }

        public void SaveChat(string fileName)
        {
            if (ChatArea != null) ChatArea.SaveFig(fileName);
        }

        public void SetAxisLimits(double xlBound, double xuBound, double ylBound, double yuBound)
        {
            ChatArea?.SetAxisLimits(xlBound, xuBound, ylBound, yuBound);
        }

        public void PlotChat()
        {
            SetTitleAndLebel();
            AddScatterPlot(XData, YData);
        }

        public string? XLabel { get; set; }
        public string? YLabel { get; set; }
        public string? Title { get; set; }
        public Plot? ChatArea { get; set; }
        public double[]? XData { get; set; }
        public double[]? YData { get; set; }
        public string[]? Labels { get; set; }
        public Dictionary<string, double[]> YArray { get; set; } = new Dictionary<string, double[]>();
    }
}
