using munkalap.data;
using munkalap.service.models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace munkalap.service.repository
{
    public abstract class GenericRepository<T>: IDisposable where T: class, IIdentity
    {
        protected readonly MySqlConnection mySqlConnection;

        public GenericRepository()
        {
            //TODO: Connection string -> külső paraméter legyen!!!
            mySqlConnection = new MySqlConnection("Server=localhost; Database=munkalap; Uid=root; Pwd=;");
            mySqlConnection.Open();
        }

        public void Dispose()
        {
            mySqlConnection.Dispose();
        }

        public abstract T Create(T item);

        public abstract T Update(T item);

        public abstract void Delete(T item);

        public abstract T GetById(int id);

        public abstract IEnumerable<T> GetAll();

        public abstract IEnumerable<T> Search(Func<T, bool> filter);


        protected void Delete(string storedProcedureName, T item)
        {
            using (var cmd = new MySqlCommand(storedProcedureName, mySqlConnection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_id", item.Id);
                cmd.ExecuteNonQuery();
            }
        }

        protected IEnumerable<T> GetAll(string storedProcedureName)
        {
            var items = new List<T>();
            using (var cmd = new MySqlCommand(storedProcedureName, mySqlConnection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_id", null);
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        items.Add(CreateInstance(reader));
            }
            return items;
        }

        protected T GetById(string storedProcedureName, int id)
        {
            using (var cmd = new MySqlCommand(storedProcedureName, mySqlConnection))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_id", id);
                using (var reader = cmd.ExecuteReader())
                    if (reader.Read())
                        return CreateInstance(reader);
                    else
                        throw new KeyNotFoundException();
            }
        }

        private T CreateInstance(MySqlDataReader reader)
        {
            //https://stackoverflow.com/questions/731452/create-instance-of-generic-type-whose-constructor-requires-a-parameter
            return (T)Activator.CreateInstance(typeof(T), new object[] { reader });
        }

    }
}
