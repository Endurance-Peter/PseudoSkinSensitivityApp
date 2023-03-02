using PseudoSkinApplication.RunSensititvity;
using PseudoSkinDataAccess.UnitOfWorks;
using PseudoskinService.Services.Regression;
using PseudoSkinServices.CalculatePseudoskin;
using PseudoSkinServices.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinApplication.Regression.SaveRegressionResults
{
    public class SaveRegressionResultCommand : Command
    {
        public override void Validation()
        {
            
        }
        public string PseudoskinName { get; set; }
        public SensititvityVariable SensititvityVariable { get; set; }
        public double[] SensitivityParameterValue { get; set; }
        public double[] SensitivityPseudoskinValue { get; set; }
    }

    public class SaveRegressionResultCommandHandler : CommandHandler<SaveRegressionResultCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public SaveRegressionResultCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public override async Task<bool> HandleAsync(SaveRegressionResultCommand command)
        {

            var fetchPseudoskin = await unitOfWork.PseudoSkin.GetByName(x => x.Name == command.PseudoskinName);
            if (fetchPseudoskin == null) throw new ArgumentException($"{command.PseudoskinName} not found");

            var regressionResults = fetchPseudoskin.RegressionResults.Where(x => x.SensititvityVariable == command.SensititvityVariable.ConvertTo());

            foreach (var result in regressionResults)
            {
                fetchPseudoskin.RemoveRegressionResult(result);
            }

            var predictedPseudoskin = RegressionAnalysis.FindPredictedBestFits(command.SensitivityParameterValue, command.SensitivityPseudoskinValue);
            var rSquareValue = RegressionAnalysis.CalculateRSquared(command.SensitivityPseudoskinValue, predictedPseudoskin);

            foreach (var result in predictedPseudoskin)
            {
                fetchPseudoskin.AddRegressionResult(new PseudoSkinDomain.Models.RegressionResult
                {

                });
            }

            return true;
        }
    }
}
