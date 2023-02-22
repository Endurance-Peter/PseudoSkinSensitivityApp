using FluentNHibernate.Mapping;
using PseudoSkinDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinDataAccess.Mappings
{
    public class PseudoskinMap : ClassMap<PseudoSkin>
    {
        public PseudoskinMap()
        {
            Table("PseudoSkins");
            Id(x => x.Id).GeneratedBy.GuidComb().Unique();
            Map(x => x.Name);
            Map(x => x.DateCreated);
            Map(x => x.VerticalPermeability);
            Map(x => x.HorizontalPermeability);
            Map(x => x.ReservoirThickness);
            Map(x => x.WellboreRadius);
            Map(x => x.DistanceFromTopOfSandToTopOfPerforation);
            Map(x => x.LenghtOfPerforationInterval);
            HasMany(x => x.SensitivityResults).Cascade.AllDeleteOrphan().Inverse();
            HasMany(x => x.RegressionResults).Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
