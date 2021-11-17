using munkalap.data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace munkalap.service.models
{
    public class Employee : IEmployee
    {
        //[Key]
        public int Id { get; set; }
        [StringLength(100)]
        [Required]        
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

}
