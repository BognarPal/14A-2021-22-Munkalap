using Microsoft.EntityFrameworkCore;
using munkalap.service.models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace munkalap.service
{
    public class ApplicationDbContext: DbContext
    {
        //public DbSet<Employee> Employees { get; set; }
        public DbSet<Failure> Failures { get; set; }

//TODO!!!! Paraméterbe kitenni!!!!
        public string ConnectionString { get; set; }
#if DEBUG
        = "Server=localhost;Database=WorkSheet;Uid=root;Pwd=;";
#endif

        public ApplicationDbContext()
        { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {
            //https://stackoverflow.com/questions/33127296/how-to-get-connectionstring-from-ef7-dbcontext
            var extenstion = options.FindExtension<MySqlOptionsExtension>();
            this.ConnectionString = extenstion.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasIndex(e => e.Name).IsUnique();

            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 1, Name="Béla" },
                new Employee() { Id = 2, Name="Géza" }
            );
        }

        public static ApplicationDbContext AppDbContext { get; set; }
    }
}
