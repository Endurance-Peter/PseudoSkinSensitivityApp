using PseudoSkinDomain.Models;
using PseudoSkinServices;
using PseudoSkinServices.CalculatePseudoskin;
using System;
using System.Linq;

namespace PseudoSkinApplication.RunSensititvity
{
    public class RunSensitivityHandler
    {
        //public RunSensitivityHandler(IAllService service)
        //{
        //    Service = service;
        //}
        //public IAllService Service { get; set; }

        public void RunSensitivity(IAllService service)
        {
            var skin = new PseudoSkin
            {
                HorizontalPermeability = 70,
                VerticalPermeability = 70,
                LenghtOfPerforationInterval = 40,
                ReservoirThickness = 200,
                WellboreRadius = 0.25,
                DistanceFromTopOfSandToTopOfPerforation = 80
            };

            var sensitivity = new RunSensitivityDto
            {
                StartValue = 70,
                StepVlue = 5,
                StopVlue = 200
            };

            var pseudoSkin = new PseudoskinCalculation();

            var items = GetNumberOfItems(sensitivity.StartValue, sensitivity.StepVlue, sensitivity.StopVlue);
            double[] y = new double[items];
            double[] x = new double[items];
            for (int i = 0; i < items; i++)
            {
                if(i!=0)
                {
                    x[i] = x[i-1] + sensitivity.StepVlue;
                    continue;
                }
                x[i] = sensitivity.StartValue;
            }

            for (int i = 0; i < x.Length; i++)
            {
                y[i] = pseudoSkin.Calculate(skin.HorizontalPermeability, x[i],
                        skin.WellboreRadius, skin.ReservoirThickness, skin.DistanceFromTopOfSandToTopOfPerforation, skin.LenghtOfPerforationInterval);
            }

            

            var chat = service.ChartService;

            chat.Title = "the chat area";
            chat.XLabel = "Anisotropy";
            chat.YLabel = "Effect of Pseudo Skin";
            chat.XData = x;
            chat.YData = y;

        }

        private int GetNumberOfItems(double start, double step, double stop)
        {
            return (int)Math.Ceiling((stop - start) / step) + 1;
        }

        
    }

    public class RunSensitivityDto
    {
        public int StartValue { get; set; }
        public int StopVlue { get; set; }
        public int StepVlue { get; set; }
        public SensititvityVariable SensititvityVariable { get; set; }
    }
}
