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
}