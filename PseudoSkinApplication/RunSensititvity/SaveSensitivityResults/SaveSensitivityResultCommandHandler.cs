using PseudoSkinDataAccess.UnitOfWorks;
using PseudoSkinDomain.Models;
using PseudoSkinServices.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinApplication.RunSensititvity.SaveSensitivityResults
{
    public class SaveSensitivityResultCommandHandler : CommandHandler<SaveSensitivityResultCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public SaveSensitivityResultCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public override async Task<bool> HandleAsync(SaveSensitivityResultCommand command)
        {
            var fetchPseudoskin = await unitOfWork.PseudoSkin.GetByName(x => x.Name == command.PseudoskinName);
            if (fetchPseudoskin == null) throw new ArgumentException($"{command.PseudoskinName} not found");

            var results = fetchPseudoskin.SensitivityResults.Where(x => x.SensititvityVariable == command.SensititvityVariable.ConvertTo());

            foreach (var result in results)
            {
                fetchPseudoskin.RemoveSensitivityResult(result);
            }

            for (int i = 0; i < command.ParameterValue.Length; i++)
            {
                var result = new SensitivityResult
                {
                    SensititvityVariable = command.SensititvityVariable.ConvertTo(),
                    StartValue = command.StartValue,
                    StepValue = command.StepVlue,
                    StopValue = command.StopVlue
                };

                result.AddResult(new Result
                {
                    PseudoskinValue = command.PseudoskinValue[i],
                    SensitivityValue = command.ParameterValue[i]
                });
                fetchPseudoskin.AddSensitivityResult(result);
            }

            unitOfWork.PseudoSkin.Update(fetchPseudoskin);
            var commitStatus = unitOfWork.SaveChangesAsync();

            return commitStatus.IsCompleted;
        }
    }
}
