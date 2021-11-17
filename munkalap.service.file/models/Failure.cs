using munkalap.data;
using munkalap.service.repository;
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

        private int? employeeId
        {
            get
            {
                //if (Assigned == null)
                //    return null;
                //return Assigned.Id;
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

        public Failure()
        {}

        public Failure(string row)
        {
            var data = row.Split(';');
            Id = int.Parse(data[0]);
            Issuer = data[1];
            IssueTimeStamp = DateTime.Parse(data[2]);
            Room = data[3];
            Description = data[4];
            employeeId = string.IsNullOrEmpty(data[5]) ? null : (int?)int.Parse(data[5]);
            AssignTimeStamp = string.IsNullOrEmpty(data[6]) ? null : (DateTime?)DateTime.Parse(data[6]);
            AssignComment = data[7];
            WorkStarted = string.IsNullOrEmpty(data[8]) ? null : (DateTime?)DateTime.Parse(data[8]);
            WorkFinished = string.IsNullOrEmpty(data[9]) ? null : (DateTime?)DateTime.Parse(data[9]);
            FinishComment = data[10];
            IsChecked = string.IsNullOrEmpty(data[11]) ? null : (bool?)(data[11] == "True");
        }

        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}",
                                 Id,
                                 Issuer,
                                 IssueTimeStamp,
                                 Room,
                                 Description,
                                 employeeId == null ? "" : employeeId.ToString(),
                                 AssignTimeStamp == null ? "" : AssignTimeStamp.ToString(),
                                 AssignComment,
                                 WorkStarted == null ? "" : WorkStarted.ToString(),
                                 WorkFinished == null ? "" : WorkFinished.ToString(),
                                 FinishComment,
                                 IsChecked == null ? "" : IsChecked.ToString()
                                );
        }
    }
}
