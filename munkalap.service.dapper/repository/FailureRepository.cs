using Dapper;
using munkalap.service.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace munkalap.service.repository
{
    public class FailureRepository : GenericRepository<Failure>
    {
        public override void Delete(Failure failure)
        {
            var sql = "delete from failures where id = @id";
            mySqlConnection.Execute(sql, failure);
        }

        public override IEnumerable<Failure> GetAll()
        {
            var sql = "select * from failures";
            return mySqlConnection.Query<Failure>(sql);
        }

        public override Failure GetById(int id)
        {
            var sql = "select * from failures where id = @id";
            var failure = mySqlConnection.Query<Failure>(sql, new { id = id }).FirstOrDefault();
            if (failure == null)
                throw new KeyNotFoundException();
            return failure;
        }

        public override IEnumerable<Failure> Search(Func<Failure, bool> filter)
        {
            return GetAll().Where(filter);
        }

        public override Failure Create(Failure failure)
        {
            var sql = "insert into failures (issuer, room, description) " +
                      "values(@issuer, @room, @description); " +
                      "select * from failures " +
                      "where id = LAST_INSERT_ID();";
            return mySqlConnection.Query<Failure>(sql, failure).FirstOrDefault();
        }

        public override Failure Update(Failure failure)
        {
            var sql = "update failures " +
                        " set issuer = @issuer, " +
                        " room = @room, " +
                        " description = @description, " +
                        " assignedEmployeeId = @assignedEmployeeId, " +
                        " assignComment = @assignComment, " +
                        " workStarted = @workStarted, " +
                        " workFinished = @workFinished, " +
                        " isChecked = @isChecked " +
                      " where id = @id; " +
                      " select * from failures " +
                      " where id = @id";
            return mySqlConnection.Query<Failure>(sql, failure).FirstOrDefault();
        }
    }
}
