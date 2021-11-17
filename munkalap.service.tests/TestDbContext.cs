using Microsoft.EntityFrameworkCore;
using munkalap.service.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace munkalap.service.tests
{
    public class TestDbContext: ApplicationDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("worksheet"+ Guid.NewGuid().ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Failure>().HasData(
                new Failure() {
                    Id = 1, 
                    Issuer="Winch Eszter", 
                    Room = "103",
                    Description = "Hiba van...", 
                    IssueTimeStamp = new DateTime(2021, 10, 07, 13, 20, 59) },
                new
                {
                    Id = 2,
                    Issuer = "Kukor Ilona",
                    Room = "41",
                    Description = "Büdös van",
                    IssueTimeStamp = new DateTime(2021, 10, 07, 08, 18, 42),
                    AssignedId = 1,
                    AssignTimeStamp = new DateTime(2021, 10, 07, 08, 18, 43),
                    AssignComment = "Vigyél magaddal maszkot!"
                },                
                new 
                {
                    Id = 3,
                    Issuer = "Gipsz Jakab",
                    Room = "Étkező",
                    Description = "Elfogyott a sör..",
                    IssueTimeStamp = new DateTime(2021, 10, 07, 21, 22, 23),
                    AssignedId = 2,
                    AssignTimeStamp = new DateTime(2021, 10, 07, 21, 22, 24),
                    FinishComment = "Nem volt, nem is lesz",
                    WorkFinished = new DateTime(2021, 10, 07, 21, 22, 25)                    
                }
            );
        }

        public static TestDbContext GenerateTestDbContext()
        {
            var context = new TestDbContext();
            context.Database.EnsureCreated();
            return context;
        }
    }
}
