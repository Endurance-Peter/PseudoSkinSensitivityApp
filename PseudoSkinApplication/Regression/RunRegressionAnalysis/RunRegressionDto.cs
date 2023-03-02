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
}