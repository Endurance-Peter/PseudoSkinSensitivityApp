using PseudoSkinServices.CalculatePseudoskin;
using System.Collections.Generic;

namespace PseudoSkinApplication.Regression.RunRegressionAnalysis
{
    public class RunRegressionDto
    {
        public double[] ResgresionPredictedPseudoskinValue { get; set; }
        public double[] ParameterRegressValues { get; set; }
        public double RSquareValue { get; set; }
    }

    public class RegressionDto
    {
        public double ResgresionPredictedPseudoskinValue { get; set; }
        public double SensititvitySkinValues { get; set; }
        public double ParameterValues { get; set; }
        public int SerialNo { get; set; }
    }

    public class RankingDto
    {
        public double ZmValue { get; set; }
        public double PenetrationRatioValue { get; set; }
        public double WellboreRadiusValue { get; set; }
        public double AnisotropyValue { get; set; }
        public double Value { get; set; }
        public int SerialNo { get; set; }
    }
}