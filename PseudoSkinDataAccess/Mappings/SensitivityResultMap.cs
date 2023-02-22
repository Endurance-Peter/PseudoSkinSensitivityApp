using FluentNHibernate.Mapping;
using PseudoSkinDomain.Models;

namespace PseudoSkinDataAccess.Mappings
{
    public class SensitivityResultMap : ClassMap<SensitivityResult>
    {
        public SensitivityResultMap()
        {
            Table("SensitivityResults");
            Id(x => x.Id).GeneratedBy.GuidComb().Unique();
            Map(x => x.StartValue);
            Map(x => x.StepValue);
            Map(x => x.StopValue);
            Map(x => x.SensititvityVariable).CustomType<GenericEnumMapper<SensititvityVariable>>();
            References(x => x.PseudoSkin);
            HasMany(x => x.Results).Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
