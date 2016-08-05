using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ProjectPortfolio.Models;

namespace ProjectPortfolio.DAL
{
    public class PortfolioContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public PortfolioContext() : base("name=PortfolioContext")
        {
        }

        public DbSet<Project> AProjects { get; set; }
        public DbSet<Funder> Funders { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AProject>().HasMany(p => p.Files);
        }
    }
}
