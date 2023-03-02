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
            SetParameters();
        }

        private void SetParameters()
        {
            _anisotropy = _horizontalPermeability / _verticalPermeability;
            _penetratioRatio = _reservoirThickness / _perforationInterver;

            if (_y == 0)
            {
                _correctedWellboreRadius = _wellboreRadius;
            }
            else
            {
                _zm = _y + (_perforationInterver / 2);
                _correctedWellboreRadius = _wellboreRadius * Math.Exp(0.2126 * ((_zm / _reservoirThickness) + 2.753));
            }
        }

        private void SetSensitivityParameter(double parameterValue, SensititvityVariable variable)
        {
            if(variable == SensititvityVariable.WellboreRadius)
            {
                _wellboreRadius = parameterValue;
                _correctedWellboreRadius = _wellboreRadius * Math.Exp(0.2126 * ((_zm / _reservoirThickness) + 2.753));
            }
            else if(variable == SensititvityVariable.DistanceFromTopOfSandToMidOfPerforation)
            {
                _zm = parameterValue;
                _correctedWellboreRadius = _wellboreRadius * Math.Exp(0.2126 * ((_zm / _reservoirThickness) + 2.753));
            }
        }

        public double Calculate()
        {
            double pseudoskin = CalculateAll();

            return pseudoskin;
        }
        public double Calculate(double parameterValue, SensititvityVariable variable)
        {
            switch(variable)
            {
                case SensititvityVariable.Anisotropy:
                    _anisotropy = 1 / parameterValue;
                    return CalculateAll();
                case SensititvityVariable.WellboreRadius:
                    SetSensitivityParameter(parameterValue,variable);
                    return CalculateAll();
                case SensititvityVariable.PenetrationRatio:
                    _penetratioRatio = 1/parameterValue;
                    return CalculateAll();
                case SensititvityVariable.DistanceFromTopOfSandToMidOfPerforation:
                    SetSensitivityParameter(parameterValue, variable);
                    return CalculateAll();
                default:
                    break;
            }

            return -1;
        }

        private double CalculateAll()
        {
            var A = Math.Pow((_penetratioRatio - 1), 0.825);
            var B = Math.Log((_reservoirThickness * Math.Sqrt(_anisotropy)) + 7);
            var C = 0.49 + (0.1 * Math.Log((_reservoirThickness * Math.Sqrt(_anisotropy))));
            var D = Math.Log(_correctedWellboreRadius);

            var pseudoskin = 1.35 * (A * (B - (C * D) - 1.95));
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
        private double _anisotropy;
        private double _penetratioRatio;
    }
}
