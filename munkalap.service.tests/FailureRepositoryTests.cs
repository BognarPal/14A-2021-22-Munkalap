using munkalap.service.models;
using munkalap.service.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace munkalap.service.tests
{
    public class FailureRepositoryTests
    {
        [Fact]
        public void GetById()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new FailureRepository(context);

                var failure = sut.GetById(2);

                Assert.Equal(2, failure.Id);
                Assert.Equal("Kukor Ilona", failure.Issuer);
                Assert.Equal("41", failure.Room);
                Assert.Equal("Büdös van", failure.Description);
                Assert.Equal("Vigyél magaddal maszkot!", failure.AssignComment);
                Assert.Null(failure.IsChecked);
                Assert.Null(failure.WorkFinished);
                Assert.Equal("Béla", failure.Assigned.Name);
            }
        }

        [Fact]
        public void GetByIdNotExists()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new FailureRepository(context);

                Assert.Throws<KeyNotFoundException>( () => sut.GetById(5) );
            }
        }

        [Fact]
        public void GetAll()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new FailureRepository(context);

                var failures = sut.GetAll();

                Assert.Equal(3, failures.Count());
                Assert.Equal("Winch Eszter", failures.Single(f => f.Id == 1).Issuer);
                Assert.Equal("41", failures.Single(f => f.Id == 2).Room);
                Assert.Equal("Géza", failures.Single(f => f.Id == 3).Assigned.Name);
            }
        }

        [Fact]
        public void Create()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new FailureRepository(context);
                var failure = new Failure
                {
                    Issuer = "Trab Antal",
                    Description = "Teszt hiba",
                    Room = "101"
                };

                var createdFailure = sut.Create(failure);
                var failureById = sut.GetById(createdFailure.Id);

                Assert.Equal(4, createdFailure.Id);
                Assert.Equal("Trab Antal", createdFailure.Issuer);
                Assert.Equal("Teszt hiba", createdFailure.Description);
                Assert.Equal("101", createdFailure.Room);

                Assert.Equal("Trab Antal", failureById.Issuer);
                Assert.Equal("Teszt hiba", failureById.Description);
                Assert.Equal("101", failureById.Room);
                
                Assert.InRange((DateTime.Now - failureById.IssueTimeStamp).TotalSeconds, 0, 5);
            }
        }

        [Fact]
        public void Update_Assign()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new FailureRepository(context);                
                var failure = sut.GetById(1);                
                failure.Assigned = new EmployeeRepository(context).GetById(1);
                failure.AssignComment = "teszt megjegyzés";

                Failure updatedFailure = sut.Assign(failure);
                var failureById = sut.GetById(1);

                Assert.Equal("Béla", updatedFailure.Assigned.Name);
                Assert.Equal("teszt megjegyzés", updatedFailure.AssignComment);

                Assert.Equal("Béla", failureById.Assigned.Name);
                Assert.Equal("teszt megjegyzés", failureById.AssignComment);
                Assert.InRange((DateTime.Now - (DateTime)failureById.AssignTimeStamp).TotalSeconds, 0, 5);
            }
        }

        [Fact]
        public void Update_Finshed()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new FailureRepository(context);
                var failure = sut.GetById(2);
                failure.FinishComment = "Megvan";

                Failure updatedFailure = sut.Finish(failure);
                var failureById = sut.GetById(2);

                Assert.Equal("Megvan", updatedFailure.FinishComment);
                Assert.Equal("Megvan", failureById.FinishComment);
                Assert.InRange((DateTime.Now - (DateTime)failureById.WorkFinished).TotalSeconds, 0, 5);
            }
        }

        [Fact]
        public void Update_Check()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new FailureRepository(context);
                var failure = sut.GetById(3);

                Failure updatedFailure = sut.Check(failure);
                var failureById = sut.GetById(3);

                Assert.True(updatedFailure.IsChecked);
                Assert.True(failureById.IsChecked);
            }
        }

        [Fact]
        public void Delete()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                var sut = new FailureRepository(context);

                var failure = sut.GetById(1);
                sut.Delete(failure);

                Assert.Throws<KeyNotFoundException>(() => sut.GetById(1));
            }
        }
    }
}
