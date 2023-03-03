using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinApplication
{
    public class SelectedPseudoskin
    {
        public string Name { get; set; } 
    }

    public static class ParameterVariables
    {
        public const string ANISOTROPY = "IsAnisotropy";
        public const string WELLBORE_RADIUS = "IsWellboreRadius";
        public const string PENETRATIO_RATIO = "IsPenetratioRatio";
        public const string ZM = "IsZmValue";
    }
}
