using Dapper;
using munkalap.service.models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace munkalap.service.repository
{
    public class EmployeeRepository : GenericRepository<Employee>
    {
        public override void Delete(Employee employee)
        {
            var sql = "update employees set isDeleted = 1 where id = @id";
            mySqlConnection.Execute(sql, employee);
        }

        public override IEnumerable<Employee> GetAll()
        {
            string sql = "select * from employees";
            return mySqlConnection.Query<Employee>(sql);
        }

        public override Employee GetById(int id)
        {
            string sql = "select * from employees where id = @id";
            var employee = mySqlConnection.Query<Employee>(sql, new { id = id }).FirstOrDefault();
            if (employee == null)
                throw new KeyNotFoundException();
            return employee;
        }

        public override IEnumerable<Employee> Search(Func<Employee, bool> filter)
        {
            throw new NotImplementedException();
        }

        public override Employee Create(Employee employee)
        {
            var sql = "insert into employees (name) values (@name);" +
                      "Select * from employees where id = LAST_INSERT_ID();";
            return mySqlConnection.Query<Employee>(sql, employee).FirstOrDefault();
        }

        public override Employee Update(Employee employee)
        {
            var sql = "update employees set name = @name where id = @id;" +
                      "Select* from employees where id = @id;";
            return mySqlConnection.Query<Employee>(sql, employee).FirstOrDefault();
        }
    }
}
