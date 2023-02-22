using PseudoSkinServices.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoSkinApplication.CreatePseudoskin
{
    public class CreatePseudoskinCommand : Command
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public double HorizontalPermeability { get; set; }
        public double VerticalPermeability { get; set; }
        public double ReservoirThickness { get; set; }
        public double WellboreRadius { get; set; }
        public double LenghtOfPerforationInterval { get; set; }
        public double DistanceFromTopOfSandToTopOfPerforation { get; set; }

        public override void Validation()
        {
            
        }
    }
}
