using System;
using System.Collections.Generic;

namespace PseudoSkinDomain.Models
{
    public class SensitivityResult : BaseEntity
    {
        public virtual void AddResult(Result result)
        {
            result.SensitivityResult = this;
            _results.Add(result);
        }
        public virtual double StartValue { get; set; }
        public virtual double StepValue { get; set; }
        public virtual double StopValue { get; set; }
        public virtual SensititvityVariable SensititvityVariable { get; set; }
        public virtual PseudoSkin PseudoSkin { get; set; }
        public virtual IEnumerable<Result> Results => _results;
        private readonly ISet<Result> _results = new HashSet<Result>();
    }
}
