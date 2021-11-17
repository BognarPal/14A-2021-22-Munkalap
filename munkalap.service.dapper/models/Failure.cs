using munkalap.data;
using munkalap.service.repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace munkalap.service.models
{
    public class Failure : IFailure
    {
        public int Id { get; set; }
        public string Issuer { get; set; }
        public DateTime IssueTimeStamp { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
        public IEmployee Assigned { get; set; }
        public DateTime? AssignTimeStamp { get; set; }
        public string AssignComment { get; set; }
        public DateTime? WorkStarted { get; set; }
        public DateTime? WorkFinished { get; set; }
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

        public int? AssignedEmployeeId
        {
            get
            {
                return Assigned == null ? null : (int?)Assigned.Id;
            }
            set
            {
                if (value == null)
                    Assigned = null;
                else
                    Assigned = new EmployeeRepository().GetById((int)value);
            }
        }

            
    }
}
