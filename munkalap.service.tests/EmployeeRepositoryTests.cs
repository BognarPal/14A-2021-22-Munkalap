using munkalap.service.models;
using munkalap.service.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace munkalap.service.tests
{
    public class EmployeeRepositoryTests
    {
        [Fact]
        //public void Id_1_Should_B�la()
        public void GetById()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                //Arrange
                var sut = new EmployeeRepository(context);

                //Act
                var employee = sut.GetById(1);

                //Assert
                Assert.Equal("B�la", employee.Name);
                Assert.False(employee.IsDeleted);
                Assert.Equal(1, employee.Id);
            }
        }

        [Fact]
        public void GetByIdNotExists()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                //Arrange
                var sut = new EmployeeRepository(context);

                //Act                

                //Assert
                Assert.Throws<KeyNotFoundException>(() => sut.GetById(3));
            }
        }

        [Fact]
        public void GetAll()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                //Arrange
                var sut = new EmployeeRepository(context);

                //Act
                var employees = sut.GetAll();

                //Assert
                Assert.Equal(2, employees.Count());
                Assert.Contains("G�za", employees.Select(e => e.Name));
            }
        }

        [Fact]
        public void Create()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                //Arrange
                var sut = new EmployeeRepository(context);
                var employee = new Employee()
                {
                    Name = "J�nos"
                };

                //Act                
                var createdEmployee = sut.Create(employee);
                var employeeById = sut.GetById(createdEmployee.Id);

                //Assert
                Assert.Equal(3, createdEmployee.Id);
                Assert.Equal("J�nos", createdEmployee.Name);
                Assert.False(createdEmployee.IsDeleted);
                Assert.Equal("J�nos", employeeById.Name);
            }
        }

        [Fact]
        public void Update()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                //Arrange
                var sut = new EmployeeRepository(context);
                var employee = sut.GetById(1);
                employee.Name = "Teszt Elek";

                //Act                
                var updatedEmployee = sut.Update(employee);
                var employeeById = sut.GetById(1);

                //Assert
                Assert.Equal("Teszt Elek", updatedEmployee.Name);
                Assert.Equal(1, updatedEmployee.Id);
                Assert.Equal("Teszt Elek", employeeById.Name);
            }
        }

        [Fact]
        public void Delete()
        {
            using (var context = TestDbContext.GenerateTestDbContext())
            {
                //Arrange
                var sut = new EmployeeRepository(context);
                var employee = sut.GetById(1);

                //Act                
                sut.Delete(employee);
                var employeeById = sut.GetById(1);

                //Assert
                Assert.True(employeeById.IsDeleted);
            }
        }
    }
}
