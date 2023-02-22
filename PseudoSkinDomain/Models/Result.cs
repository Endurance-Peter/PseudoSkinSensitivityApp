namespace PseudoSkinDomain.Models
{
    public class Result : BaseEntity
    {
        public virtual double SensitivityValue { get; set; }
        public virtual double PseudoskinValue { get; set; }
        public virtual SensitivityResult SensitivityResult { get; set; }
        public virtual RegressionResult RegressionResult { get; set; }
    }
}
