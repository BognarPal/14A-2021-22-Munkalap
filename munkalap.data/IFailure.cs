using System;
using System.Collections.Generic;
using System.Text;

namespace munkalap.data
{
    public interface IFailure: IIdentity
    {
        string Issuer { get; set; }
        DateTime IssueTimeStamp { get; set; }
        string Room { get; set; }
        string Description { get; set; }
        IEmployee Assigned { get; set; }
        DateTime? AssignTimeStamp { get; set; }
        string AssignComment { get; set; }
        DateTime? WorkStarted { get; set; }
        DateTime? WorkFinished { get; set; }
        string FinishComment { get; set; }
        bool? IsChecked { get; set; }
    }
}
