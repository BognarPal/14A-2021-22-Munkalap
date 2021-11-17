using munkalap.data;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace munkalap.service.models
{
    public class Employee : IEmployee
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
    }
}
