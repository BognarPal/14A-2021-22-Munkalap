using Microsoft.EntityFrameworkCore;
using munkalap.service.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace munkalap.service.repository
{
    public class FailureRepository : GenericRepository<Failure>
    {
        public FailureRepository(ApplicationDbContext dbContext): base(dbContext)
        { }

        public FailureRepository(): base(ApplicationDbContext.AppDbContext)
        { }

        public override Failure Create(Failure failure)
        {
            failure.IssueTimeStamp = DateTime.Now;
            return base.Create(failure);
        }

        public override void Delete(Failure failure)
        {
            failure = GetById(failure.Id);
            dbContext.Set<Failure>().Remove(failure);
            dbContext.SaveChanges();
        }

        public override Failure GetById(int id)
        {
            var entry = dbContext.Set<Failure>()
                                 .Include(f => f.Assigned)
                                 .FirstOrDefault(e => e.Id == id);
            if (entry == null)
                throw new KeyNotFoundException();
            return entry;
        }

        public override IEnumerable<Failure> GetAll()
        {
            return dbContext.Set<Failure>().Include(f => f.Assigned);
        }

        public override IEnumerable<Failure> Search(Func<Failure, bool> filter)
        {
            return dbContext.Set<Failure>()
                            .Include(f => f.Assigned)
                            .Where(filter);
        }

        public Failure Assign(Failure failure)
        {
            failure.AssignTimeStamp = DateTime.Now;
            return Update(failure);
        }

        public Failure Start(Failure failure)
        {
            failure.WorkStarted = DateTime.Now;
            return Update(failure);
        }

        public Failure Finish(Failure failure)
        {
            failure.WorkFinished = DateTime.Now;
            return Update(failure);
        }

        public Failure Check(Failure failure)
        {
            failure.IsChecked = true;
            return Update(failure);
        }

    }
}
