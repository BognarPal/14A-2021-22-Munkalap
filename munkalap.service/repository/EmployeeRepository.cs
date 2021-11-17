using Microsoft.EntityFrameworkCore;
using munkalap.service.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace munkalap.service.repository
{
    public class EmployeeRepository : GenericRepository<Employee>
    {
        public EmployeeRepository(ApplicationDbContext dbContext): base(dbContext)
        { }

        public EmployeeRepository(): base(ApplicationDbContext.AppDbContext)
        {}

        public override void Delete(Employee employee)
        {
            //dbContext.Set<Employee>().Remove(employee);
            employee.IsDeleted = true;
            this.Update(employee);
        }

        public IEnumerable<Employee> GetAll(bool withDeleted = false)
        {
            if (withDeleted)
                return base.GetAll();
            else
                return dbContext.Set<Employee>().Where(e => !e.IsDeleted).ToList();
        }

        public override IEnumerable<Employee> Search(Func<Employee, bool> filter)
        {
            return dbContext.Set<Employee>().Where(filter);
        }        
    }
}
