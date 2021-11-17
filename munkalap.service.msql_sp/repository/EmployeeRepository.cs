using munkalap.service.models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace munkalap.service.repository
{
    public class EmployeeRepository : GenericRepository<Employee>
    {
        public override Employee Create(Employee employee)
        {
            using (var cmd = new MySqlCommand("employee_Insert", mySqlConnection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_name", employee.Name);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return new Employee(reader);
                    else
                        throw new KeyNotFoundException();                    
                }
            }
        }

        public override void Delete(Employee employee)
        {
            Delete("employee_Delete", employee);
        }

        public override IEnumerable<Employee> GetAll()
        {
            return GetAll("employee_Select");           
        }

        public override Employee GetById(int id)
        {
            //GetById("employee_Select", id)

            //Itt nem tárolt eljárást hívunk, hanem SQL select-et
            using (var cmd = new MySqlCommand("Select * from employees where id = @id", mySqlConnection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return new Employee(reader);
                    else
                        throw new KeyNotFoundException();
                }
            }
        }

        public override IEnumerable<Employee> Search(Func<Employee, bool> filter)
        {
            throw new NotImplementedException();
        }

        public override Employee Update(Employee employee)
        {
            using (var cmd = new MySqlCommand("employee_Update", mySqlConnection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_id", employee.Id);
                cmd.Parameters.AddWithValue("_name", employee.Name);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        employee = new Employee(reader);
                    else
                        throw new KeyNotFoundException();
                }
            }
            return employee;
        }
    }
}
