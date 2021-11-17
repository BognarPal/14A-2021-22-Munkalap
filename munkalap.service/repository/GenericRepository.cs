using Microsoft.EntityFrameworkCore;
using munkalap.data;
using munkalap.service.models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace munkalap.service.repository
{
    public abstract class GenericRepository<T> where T: class, IIdentity
    {
        protected readonly ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual T Create(T item)
        {
            dbContext.Set<T>().Add(item);
            dbContext.SaveChanges();
            return item;
        }

        public virtual T Update(T item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            dbContext.SaveChanges();
            return GetById(item.Id);
        }

        public abstract void Delete(T item);

        public virtual T GetById(int id)
        {
            var entry = dbContext.Set<T>().FirstOrDefault(e => e.Id == id);
            if (entry == null)
                throw new KeyNotFoundException();
            return entry;
        }

        public virtual IEnumerable<T> GetAll()
        {
            dbContext.Set<T>().Load();
            return dbContext.Set<T>().Local.ToList();
        }

        public abstract IEnumerable<T> Search(Func<T, bool> filter);

    }
}
