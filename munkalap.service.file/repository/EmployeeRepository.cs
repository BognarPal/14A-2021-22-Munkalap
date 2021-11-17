using munkalap.service.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace munkalap.service.repository
{
    public class EmployeeRepository: GenericRepository<Employee>
    {
        public EmployeeRepository(string fileName = "employee.csv"): base(fileName)
        {}        

        public override void Delete(Employee employee)
        {
            employee.IsDeleted = true;
            Update(employee);
        }

        public IEnumerable<Employee> GetAll(bool withDeleted = false)
        {
            var employees = base.GetAll();
            return employees.Where(e => !e.IsDeleted || withDeleted);
        }        
    }
}
