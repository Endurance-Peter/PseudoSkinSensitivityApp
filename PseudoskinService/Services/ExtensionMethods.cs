using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinServices
{
    public static class ExtensionMethods
    {
        public static double ToDecimalPlace(this double value, int decimalPlace)
        {
            return Math.Round(value, decimalPlace);
        }
    }
}
