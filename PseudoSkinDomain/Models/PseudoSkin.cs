using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinDomain.Models
{
    public class PseudoSkin : BaseEntity
    {
        public virtual void AddAnisotropy(SensitivityResult anisotropyResult)
        {
            anisotropyResult.PseudoSkin = this;
            _anisotropyResults.Add(anisotropyResult);
        }
        public virtual void AddPenetrationRatioResult(SensitivityResult penetrationRatioResults)
        {
            penetrationRatioResults.PseudoSkin = this;
            _penetrationRatioResults.Add(penetrationRatioResults);
        }
        public virtual void AddWellboreRadiusResult(SensitivityResult wellboreRadiusResults)
        {
            wellboreRadiusResults.PseudoSkin = this;
            _wellboreRadiusResults.Add(wellboreRadiusResults);
        }
        public virtual void AddZmResult(SensitivityResult distanceFromTopOfSandToMidOfPerforationResults)
        {
            distanceFromTopOfSandToMidOfPerforationResults.PseudoSkin = this;
            _distanceFromTopOfSandToMidOfPerforationResults.Add(distanceFromTopOfSandToMidOfPerforationResults);
        }

        public virtual void ClearAnisotropy()
        {
            _anisotropyResults.Clear();
        }
        public virtual void ClearPenetrationRatioResults()
        {
            _penetrationRatioResults.Clear();
        }
        public virtual void ClearWellboreRadiusResults()
        {
            _wellboreRadiusResults.Clear();
        }
        public virtual void ClearDistanceFromTopOfSandToMidOfPerforationResults()
        {
            _distanceFromTopOfSandToMidOfPerforationResults.Clear();
        }

        public virtual string Name { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual double HorizontalPermeability { get; set; }
        public virtual double VerticalPermeability { get; set; }
        public virtual double ReservoirThickness { get; set; }
        public virtual double WellboreRadius { get; set; }
        public virtual double LenghtOfPerforationInterval { get; set; }
        public virtual double DistanceFromTopOfSandToTopOfPerforation { get; set; }
        public virtual IEnumerable<SensitivityResult> AnisotropyResults => _anisotropyResults;
        private readonly ISet<SensitivityResult> _anisotropyResults = new HashSet<SensitivityResult>();
        public virtual IEnumerable<SensitivityResult> PenetrationRatioResults => _penetrationRatioResults;
        private readonly ISet<SensitivityResult> _penetrationRatioResults = new HashSet<SensitivityResult>();
        public virtual IEnumerable<SensitivityResult> WellboreRadiusResults => _wellboreRadiusResults;
        private readonly ISet<SensitivityResult> _wellboreRadiusResults = new HashSet<SensitivityResult>();
        public virtual IEnumerable<SensitivityResult> DistanceFromTopOfSandToMidOfPerforationResults => _distanceFromTopOfSandToMidOfPerforationResults;
        private readonly ISet<SensitivityResult> _distanceFromTopOfSandToMidOfPerforationResults = new HashSet<SensitivityResult>();
    }

    public class RegressionResult : BaseEntity
    {
        public virtual void AddAnisotropy(Result anisotropyResult)
        {
            anisotropyResult.RegressionResult = this;
            _anisotropyResults.Add(anisotropyResult);
        }
        public virtual void AddPenetrationRatioResult(Result penetrationRatioResults)
        {
            penetrationRatioResults.RegressionResult = this;
            _penetrationRatioResults.Add(penetrationRatioResults);
        }
        public virtual void AddWellboreRadiusResult(Result wellboreRadiusResults)
        {
            wellboreRadiusResults.RegressionResult = this;
            _wellboreRadiusResults.Add(wellboreRadiusResults);
        }
        public virtual void AddZmResult(Result distanceFromTopOfSandToMidOfPerforationResults)
        {
            distanceFromTopOfSandToMidOfPerforationResults.RegressionResult = this;
            _distanceFromTopOfSandToMidOfPerforationResults.Add(distanceFromTopOfSandToMidOfPerforationResults);
        }

        public virtual void ClearAnisotropy()
        {
            _anisotropyResults.Clear();
        }
        public virtual void ClearPenetrationRatioResults()
        {
            _penetrationRatioResults.Clear();
        }
        public virtual void ClearWellboreRadiusResults()
        {
            _wellboreRadiusResults.Clear();
        }
        public virtual void ClearDistanceFromTopOfSandToMidOfPerforationResults()
        {
            _distanceFromTopOfSandToMidOfPerforationResults.Clear();
        }
        public virtual double RegressionValue { get; set; }
        public virtual double RegressionIncreamentValue { get; set; }
        public virtual IEnumerable<Result> AnisotropyResults => _anisotropyResults;
        private readonly ISet<Result> _anisotropyResults = new HashSet<Result>();
        public virtual IEnumerable<Result> PenetrationRatioResults => _penetrationRatioResults;
        private readonly ISet<Result> _penetrationRatioResults = new HashSet<Result>();
        public virtual IEnumerable<Result> WellboreRadiusResults => _wellboreRadiusResults;
        private readonly ISet<Result> _wellboreRadiusResults = new HashSet<Result>();
        public virtual IEnumerable<Result> DistanceFromTopOfSandToMidOfPerforationResults => _distanceFromTopOfSandToMidOfPerforationResults;
        private readonly ISet<Result> _distanceFromTopOfSandToMidOfPerforationResults = new HashSet<Result>();
    }
}
