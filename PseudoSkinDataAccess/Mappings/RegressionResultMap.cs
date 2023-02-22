using FluentNHibernate.Mapping;
using PseudoSkinDomain.Models;

namespace PseudoSkinDataAccess.Mappings
{
    public class RegressionResultMap : ClassMap<RegressionResult>
    {
        public RegressionResultMap()
        {
            Table("RegressionResults");
            Id(x => x.Id).GeneratedBy.GuidComb().Unique();
            Map(x => x.RegressionValue);
            Map(x => x.RegressionIncreamentValue);
            Map(x => x.SensititvityVariable).CustomType<GenericEnumMapper<SensititvityVariable>>();
            References(x => x.PseudoSkin);
            HasMany(x => x.RegressionResults).Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
