using SensititvityVariable = PseudoSkinServices.CalculatePseudoskin.SensititvityVariable;

namespace PseudoSkinApplication.RunSensititvity
{
    public static class EnumConversions
    {
        public static PseudoSkinDomain.Models.SensititvityVariable ConvertTo(this SensititvityVariable variable)
        {
            var result = new PseudoSkinDomain.Models.SensititvityVariable();
            switch (variable)
            {
                case SensititvityVariable.Anisotropy:
                    result = PseudoSkinDomain.Models.SensititvityVariable.Anisotropy;
                    break;
                case SensititvityVariable.PenetrationRatio:
                    result = PseudoSkinDomain.Models.SensititvityVariable.PenetrationRatio;
                    break;
                case SensititvityVariable.WellboreRadius:
                    result = PseudoSkinDomain.Models.SensititvityVariable.WellboreRadius;
                    break;
                case SensititvityVariable.DistanceFromTopOfSandToMidOfPerforation:
                    result = PseudoSkinDomain.Models.SensititvityVariable.DistanceFromTopOfSandToMidOfPerforation;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
