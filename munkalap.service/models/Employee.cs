using munkalap.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace munkalap.service.models
{
    public class Employee : IEmployee
    {
        public Employee()
        { }

        public Employee(string row)
        {
            var data = row.Split(';');
            //Todo hibakezelés???
            Id = int.Parse(data[0]);
            Name = data[1];
            IsDeleted = data[2] == "True";
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;

        public override string ToString()
        {
            return string.Format("{0};{1};{2}", Id, Name, IsDeleted);
            //return $"{Id};{Name};{IsDeleted}";
        }

    }   
}
