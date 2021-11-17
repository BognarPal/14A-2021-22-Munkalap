using munkalap.data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace munkalap.service.models
{
    public class Failure: IFailure
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string Issuer { get; set; }
        public DateTime IssueTimeStamp { get; set; } = DateTime.Now;
        [StringLength(30)]
        [Required]
        public string Room { get; set; }
        [StringLength(500)]
        [Required]
        public string Description { get; set; }
        
        public Employee Assigned { get; set; }
        public int? AssignedId { get; set; }

        public DateTime? AssignTimeStamp { get; set; }
        [StringLength(400)]
        public string AssignComment { get; set; }
        public DateTime? WorkStarted { get; set; }
        public DateTime? WorkFinished { get; set; }
        [StringLength(200)]
        public string FinishComment { get; set; }
        public bool? IsChecked { get; set; }

        public byte[] ImageStatus
        {
            get
            {
                if (WorkFinished == null)
                    return null;
                return System.IO.File.ReadAllBytes("finished.png");
            }
        }

        IEmployee IFailure.Assigned 
        {
            get
            {
                return Assigned;
            }
            set
            {
                Assigned = (Employee)value;
            }
        }
    }
}
