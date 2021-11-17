using munkalap.service.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace munkalap.service.repository
{
    public class FailureRepository: GenericRepository<Failure>
    {
        public FailureRepository(string fileName = "failure.csv"): base(fileName)
        {}        
    }
}
