using PseudoSkinServices.CalculatePseudoskin;
using PseudoSkinServices.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinApplication.RunSensititvity.SaveSensitivityResults
{
    public class SaveSensitivityResultCommand : Command
    {
        public override void Validation()
        {
            
        }

        public string PseudoskinName { get; set; }
        public double StartValue { get; set; }
        public double StopVlue { get; set; }
        public double StepVlue { get; set; }
        public SensititvityVariable SensititvityVariable { get; set; }
        public double[] ParameterValue { get; set; }
        public double[] PseudoskinValue { get; set; }
    }
}
