using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinDataAccess.Migrations.Scripts
{
    [Migration(202302221322)]
    public class Initial_Table : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("PseudoSkins")
                .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("DateCreated").AsDateTime().NotNullable()
                .WithColumn("VerticalPermeability").AsDouble().Nullable()
                .WithColumn("HorizontalPermeability").AsDouble().Nullable()
                .WithColumn("ReservoirThickness").AsDouble().Nullable()
                .WithColumn("WellboreRadius").AsDouble().Nullable()
                .WithColumn("DistanceFromTopOfSandToTopOfPerforation").AsDouble().Nullable()
                .WithColumn("LenghtOfPerforationInterval").AsDouble().Nullable();

            Create.Table("RegressionResults")
               .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
               .WithColumn("RegressionValue").AsDouble().NotNullable()
               .WithColumn("RegressionIncreamentValue").AsDouble().NotNullable()
               .WithColumn("SensititvityVariable").AsString().NotNullable()
               .WithColumn("Pseudoskin_Id").AsGuid().NotNullable().ForeignKey("FK_RegressionResult_Pseudoskin_Id", "PseudoSkins", "Id");

            Create.Table("SensitivityResults")
               .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
               .WithColumn("StartValue").AsDouble().NotNullable()
               .WithColumn("StepValue").AsDouble().NotNullable()
               .WithColumn("StopValue").AsDouble().NotNullable()
               .WithColumn("SensititvityVariable").AsString().NotNullable()
               .WithColumn("Pseudoskin_Id").AsGuid().NotNullable().ForeignKey("FK_SensitivityResult_Pseudoskin_Id", "PseudoSkins", "Id");

            Create.Table("Results")
               .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
               .WithColumn("SensitivityValue").AsDouble().NotNullable()
               .WithColumn("PseudoskinValue").AsDouble().NotNullable()
               .WithColumn("SensitivityResult_Id").AsGuid().Nullable().ForeignKey("FK_Result_SensitivityResult_Id", "SensitivityResults", "Id")
               .WithColumn("RegressionResult_Id").AsGuid().Nullable().ForeignKey("FK_Result_RegressionResult_Id", "RegressionResults", "Id");
        }
    }
}
