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

    }
}
