using PseudoSkinDataAccess.UnitOfWorks;
using PseudoSkinDomain.Models;
using PseudoSkinServices;
using PseudoSkinServices.CalculatePseudoskin;
using PseudoSkinServices.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoSkinApplication.RunSensititvity
{
    public class RunSensitivityCommandHandler : CommandHandler<RunSensitivityCommand, List<RunSensitivityDto>>
    {
        private readonly IUnitOfWork unitOfWork;

        public RunSensitivityCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public override async Task<List<RunSensitivityDto>> HandleAsync(RunSensitivityCommand command)
        {
            var fetchPseudoskin = await unitOfWork.PseudoSkin.GetByName(x => x.Name == command.PseudoskinName);
            if(fetchPseudoskin== null) throw new ArgumentException($"{command.PseudoskinName} not found");

            var results = RunSensitivity(command, fetchPseudoskin);

            return SetValues(results);
        }

        private List<RunSensitivityDto> SetValues(Tuple<double[], double[]> results)
        {
            var dtos = new List<RunSensitivityDto>();
            for (int i = 0; i < results.Item1.Length; i++)
            {
                dtos.Add(new RunSensitivityDto
                {
                    SerialNo = i + 1,
                    ParameterValue = results.Item1[i],
                    Pseudoskin = results.Item2[i]
                });

            }

            return dtos;
        }

        private Tuple<double[],double[]> RunSensitivity(RunSensitivityCommand command, PseudoSkin skin)
        {
            
            var items = GetNumberOfItems(command.StartValue, command.StepVlue, command.StopVlue);
            double[] y = new double[items];
            double[] x = new double[items];
            for (int i = 0; i < items; i++)
            {
                if(i!=0)
                {
                    x[i] = x[i-1] + command.StepVlue;
                    continue;
                }
                x[i] = command.StartValue;
            }

            var pseudoSkin = new PseudoskinCalculation(skin.HorizontalPermeability, skin.VerticalPermeability,
                        skin.WellboreRadius, skin.ReservoirThickness, skin.DistanceFromTopOfSandToTopOfPerforation, skin.LenghtOfPerforationInterval);

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < x.Length; i++)
                {
                    y[i] = pseudoSkin.Calculate(x[i], (SensititvityVariable)j);
                }

            }

            //Itme1 :x----> ParameterValue, Item2: y-------> PseudoskinValue
            return Tuple.Create<double[], double[]>(x, y);
        }

        private int GetNumberOfItems(double start, double step, double stop)
        {
            return (int)Math.Ceiling((stop - start) / step) + 1;
        }

        
    }
}
