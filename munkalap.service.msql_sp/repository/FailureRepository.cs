using munkalap.service.models;
using MySqlConnector;
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
            Delete("failure_Delete", failure);
        }

        public override IEnumerable<Failure> GetAll()
        {
            return GetAll("failure_Select");
        }

        public override Failure GetById(int id)
        {
            return GetById("failure_Select", id);
        }

        public override IEnumerable<Failure> Search(Func<Failure, bool> filter)
        {
            return GetAll().Where(filter);
        }

        public override Failure Create(Failure failure)
        {
            using (var cmd = new MySqlCommand("failure_Insert", mySqlConnection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_issuer", failure.Issuer);
                cmd.Parameters.AddWithValue("_room", failure.Room);
                cmd.Parameters.AddWithValue("_description", failure.Description);
                
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return new Failure(reader);
                    else
                        throw new KeyNotFoundException();
                }
            }
        }

        public override Failure Update(Failure failure)
        {
            using (var cmd = new MySqlCommand("failure_Update", mySqlConnection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_id", failure.Id);
                cmd.Parameters.AddWithValue("_issuer", failure.Issuer);
                cmd.Parameters.AddWithValue("_room", failure.Room);
                cmd.Parameters.AddWithValue("_description", failure.Description);
                cmd.Parameters.AddWithValue("_assignedEmployeeId", 
                    failure.Assigned == null ? null : (int?)failure.Assigned.Id);
                cmd.Parameters.AddWithValue("_assignComment", failure.AssignComment);
                cmd.Parameters.AddWithValue("_workStarted", failure.WorkStarted);
                cmd.Parameters.AddWithValue("_workFinished", failure.WorkFinished);
                cmd.Parameters.AddWithValue("_isChecked", failure.IsChecked);
                
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return new Failure(reader);
                    else
                        throw new KeyNotFoundException();
                }
            }
        }
    }
}
