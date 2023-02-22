using FluentNHibernate.Mapping;
using PseudoSkinDomain.Models;

namespace PseudoSkinDataAccess.Mappings
{
    public class ResultMap : ClassMap<Result>
    {
        public ResultMap()
        {
            Table("Results");
            Id(x => x.Id).GeneratedBy.GuidComb().Unique();
            Map(x => x.SensitivityValue);
            Map(x => x.PseudoskinValue);
            References(x => x.SensitivityResult);
            References(x => x.RegressionResult);
        }
    }
}
