using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinDomain.Models
{
    public class PseudoSkin : BaseEntity
    {
        public virtual void AddSensitivityResult(SensitivityResult sensitivityResult)
        {
            _sensitivityResults.Add(sensitivityResult);
        }
        public virtual void ClearSensitivityResults()
        {
            _sensitivityResults.Clear();
        }

        public virtual void AddRegressionResult(RegressionResult regressionResult)
        {
            _regressionResults.Add(regressionResult);
        }
        public virtual void ClearRegressionResults()
        {
            _regressionResults.Clear();
        }

        public virtual string Name { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual double HorizontalPermeability { get; set; }
        public virtual double VerticalPermeability { get; set; }
        public virtual double ReservoirThickness { get; set; }
        public virtual double WellboreRadius { get; set; }
        public virtual double LenghtOfPerforationInterval { get; set; }
        public virtual double DistanceFromTopOfSandToTopOfPerforation { get; set; }
        public virtual IEnumerable<SensitivityResult> SensitivityResults => _sensitivityResults;
        private readonly ISet<SensitivityResult> _sensitivityResults = new HashSet<SensitivityResult>();
        public virtual IEnumerable<RegressionResult> RegressionResults => _regressionResults;
        private readonly ISet<RegressionResult> _regressionResults = new HashSet<RegressionResult>();
    }

    public class RegressionResult : BaseEntity
    {
        public virtual void AddRegressionResult(Result result)
        {
            result.RegressionResult = this;
            _regressionResults.Add(result);
        }

        public virtual void ClearRegressionResults()
        {
            _regressionResults.Clear();
        }
        public virtual double RegressionValue { get; set; }
        public virtual double RegressionIncreamentValue { get; set; }
        public virtual SensititvityVariable SensititvityVariable { get; set; }
        public virtual PseudoSkin PseudoSkin { get; set; }
        public virtual IEnumerable<Result> RegressionResults => _regressionResults;
        private readonly ISet<Result> _regressionResults = new HashSet<Result>();
    }
}
