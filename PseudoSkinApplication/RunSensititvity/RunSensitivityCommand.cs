//using PseudoSkinDomain.Models;
using PseudoSkinServices.CalculatePseudoskin;
using PseudoSkinServices.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinApplication.RunSensititvity
{
    public class RunSensitivityCommand : Command
    {
        public override void Validation()
        {
            
        }
        public string PseudoskinName { get; set; }
        public double StartValue { get; set; }
        public double StopVlue { get; set; }
        public double StepVlue { get; set; }
        public SensititvityVariable SensititvityVariable { get; set; }
    }
}
