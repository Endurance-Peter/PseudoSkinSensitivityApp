using Microsoft.EntityFrameworkCore;
using PseudoSkinDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinDataAccess
{
    public class DbContextStore : DbContext
    {
        public DbContextStore(DbContextOptions<DbContextStore> options) : base(options)
        {  }
        public DbSet<PseudoSkin> PseudoSkins { get; set; }
        public DbSet<RegressionResult> RegressionResults { get; set; }
        public DbSet<SensitivityResult> SensitivityResults { get; set; }
        public DbSet<Result> Results { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Server=.;DataBase=PseudoskinDb;Integrated Security=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
