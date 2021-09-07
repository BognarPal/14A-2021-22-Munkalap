using munkalap.service.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace munkalap.service.repository
{
    public class EmployeeRepository
    {
        private readonly string fileName;
        public EmployeeRepository(string fileName = "employee.csv")
        {
            this.fileName = fileName;
        }

        public Employee Create(Employee employee)
        {
            var allEmployees = GetAll(true);
            int maxId = 0;
            if (allEmployees.Count() > 0)
                maxId = allEmployees.Max(e => e.Id);
            employee.Id = maxId + 1;
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine(employee);                
                sw.Close();
                //sw.Dispose();
            }
            return employee;
        }

        public Employee Update(Employee employee)
        {
            var employees = GetAll(true).ToList();

            //var foundEmployee = employees.Where(e => e.Id == employee.Id).FirstOrDefault();
            var foundEmployee = employees.FirstOrDefault(e => e.Id == employee.Id);
            if (foundEmployee == null)
            {
                throw new KeyNotFoundException($"Id {employee.Id} not found");
            }
            employees.Remove(foundEmployee);
            employees.Add(employee);
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                employees.ForEach(e => sw.WriteLine(e));
                sw.Close();
            }
            return employee;
        }

        public void Delete(Employee employee)
        {
            employee.IsDeleted = true;
            Update(employee);
        }

        public IEnumerable<Employee> GetAll(bool withDeleted = false)
        {
            var employees = new List<Employee>();
            if (File.Exists(fileName))
            {
                File.ReadAllLines(fileName)
                    .ToList()
                    .ForEach(row => employees.Add(new Employee(row)));

                //string[] rows = File.ReadAllLines(fileName);
                //foreach (var row in rows)
                //{
                //    employees.Add(new Employee(row));
                //}
            }
            return employees.Where(e => !e.IsDeleted || withDeleted);
        }

        public Employee GetById(int id)
        {
            var employees = GetAll(true);
            return employees.FirstOrDefault(e => e.Id == id);
        }
    }
}
