using PseudoSkinApplication.RunSensititvity;
using PseudoSkinDataAccess.UnitOfWorks;
using PseudoSkinDomain.Models;
using PseudoskinService.Services.Regression;
using PseudoSkinServices.CalculatePseudoskin;
using PseudoSkinServices.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensititvityVariable = PseudoSkinDomain.Models.SensititvityVariable;

namespace PseudoSkinApplication.Regression.RunRegressionAnalysis
{
    public class RunRegressionAnalysisCommand : Command
    {
        public override void Validation()
        {
            
        }
        public string PseudoskinName { get; set; }
        public double  StartValue { get; set; }
        public double  StepValue { get; set; }
        public double  StopValue { get; set; }

    }

    public class RunRegressionAnalysisCommandHandler : CommandHandler<RunRegressionAnalysisCommand, Dictionary<SensititvityVariable,RunRegressionDto>>
    {
        private readonly IUnitOfWork unitOfWork;

        public RunRegressionAnalysisCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public override async Task<Dictionary<SensititvityVariable, RunRegressionDto>> HandleAsync(RunRegressionAnalysisCommand command)
        {
            var fetchPseudoskin = await unitOfWork.PseudoSkin.GetByName(x => x.Name == command.PseudoskinName);
            if (fetchPseudoskin == null) throw new ArgumentException($"{command.PseudoskinName} not found");

            var sensitivityResults = fetchPseudoskin.SensitivityResults.ToDictionary(x => x.SensititvityVariable, x=>x.Results);//GroupBy(x => x.SensititvityVariable);
            fetchPseudoskin.ClearRegressionResults();

            var regressionDto = new Dictionary<SensititvityVariable, RunRegressionDto>();
            var items = GetNumberOfItems(command.StartValue, command.StepValue, command.StopValue);
            double[] y = new double[items];
            double[] x = new double[items];
            for (int i = 0; i < items; i++)
            {
                if (i != 0)
                {
                    x[i] = x[i - 1] + command.StepValue;
                    continue;
                }
                x[i] = command.StartValue;
            }

            foreach (var sensitivityResult in sensitivityResults)
            {
                var regressionResult = new PseudoSkinDomain.Models.RegressionResult
                {
                    SensititvityVariable = sensitivityResult.Key,
                    RegressionValue = command.StartValue,
                    RegressionIncreamentValue = command.StepValue
                };

                var parameterValue = new List<double>();
                var pseudoskinValue = new List<double>();
                foreach (var value in sensitivityResult.Value)
                {
                    parameterValue.Add(value.SensitivityValue);
                    pseudoskinValue.Add(value.PseudoskinValue);
                }

                var degree = RegressionAnalysis.FindDegreeOfBestFit(parameterValue.ToArray(), pseudoskinValue.ToArray());
                var coefficients = RegressionAnalysis.FitPolynomial(parameterValue.ToArray(), pseudoskinValue.ToArray(), degree);
                var regressPseudoskinValue = RegressionAnalysis.EvaluatePolynomial(x, coefficients);
                var rSquareValue = RegressionAnalysis.CalculateRSquared(pseudoskinValue.ToArray(), regressPseudoskinValue);

                regressionDto.Add(sensitivityResult.Key, new RunRegressionDto
                {
                    ParameterRegressValues = x,
                    ResgresionPredictedPseudoskinValue = regressPseudoskinValue,
                    RSquareValue = rSquareValue
                });

                int i = 0;
                foreach (var regressionValue in regressPseudoskinValue)
                {
                    regressionResult.AddRegressionResult(new PseudoSkinDomain.Models.Result
                    {
                        PseudoskinValue = regressionValue,
                        SensitivityValue = x[i++]
                    });
                    fetchPseudoskin.AddRegressionResult(regressionResult);
                }
            }

            unitOfWork.PseudoSkin.Update(fetchPseudoskin);
            var commit = unitOfWork.SaveChangesAsync();
            return commit.IsCompleted ? regressionDto : throw new ArgumentException("Unable to save to database");
        }
        private int GetNumberOfItems(double start, double step, double stop)
        {
            return (int)Math.Ceiling((stop - start) / step) + 1;
        }
    }
}
