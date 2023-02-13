using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinServices.CalculatePseudoskin
{
    public class PseudoskinCalculation
    {

        public PseudoskinCalculation(double horizontalPermeability, double verticalPermeability, double wellboreRadius, double reservoirThickness,
            double distanceFromTopOfSandToTopOfOpenInterver, double perforationInterver)
        {
            _horizontalPermeability = horizontalPermeability;
            _verticalPermeability = verticalPermeability;
            _wellboreRadius = wellboreRadius;
            _y = distanceFromTopOfSandToTopOfOpenInterver;
            _reservoirThickness = reservoirThickness;
            _perforationInterver = perforationInterver;
        }

        public double Calculate()
        {
            _zm = _y + (_perforationInterver / 2);

            if (_y == 0)
            {
                _correctedWellboreRadius = _wellboreRadius;
            }
            else
            {
                _correctedWellboreRadius = _wellboreRadius * Math.Exp(0.2126 * ((_zm / _reservoirThickness) + 2.753));
            }

            var A = Math.Pow(((_reservoirThickness / _perforationInterver) - 1), 0.825);
            var B = Math.Log((_reservoirThickness * Math.Sqrt(_horizontalPermeability / _verticalPermeability)) + 7);
            var C = 0.49 + (0.1 * Math.Log((_reservoirThickness * Math.Sqrt(_horizontalPermeability / _verticalPermeability))));
            var D = Math.Log(_correctedWellboreRadius) - 1.95;

            var pseudoskin = 1.35 * (A * (B - (C * D)));

            return pseudoskin;
        }


        private double _zm;
        private double _wellboreRadius;
        private double _correctedWellboreRadius;
        private double _y;
        private double _horizontalPermeability;
        private double _verticalPermeability;
        private double _reservoirThickness;
        private double _perforationInterver;
    }
}
