using munkalap.data;
using munkalap.service.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace munkalap.service.repository
{
    public class GenericRepository<T> where T: class, IIdentity
    {
        protected readonly string fileName;

        public GenericRepository(string fileName)
        {
            this.fileName = fileName;
        }

        public virtual T Create(T item)
        {
            var allItems = GetAll();
            int maxId = 0;
            if (allItems.Count() > 0)
                maxId = allItems.Max(e => e.Id);
            item.Id = maxId + 1;
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine(item);
                sw.Close();
            }
            return item;
        }

        public virtual T Update(T item)
        {
            var allItems = GetAll().ToList();

            RemoveItemFromList(allItems, item);
            allItems.Add(item);
            WriteItemsToFile(allItems);
            return item;
        }

        public virtual void Delete(T item)
        {
            var allItems = GetAll().ToList();
            RemoveItemFromList(allItems, item);
            WriteItemsToFile(allItems);
        }

        public virtual T GetById(int id)
        {
            var allItems = GetAll();
            return allItems.FirstOrDefault(i => i.Id == id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            var allItem = new List<T>();
            if (File.Exists(fileName))
            {
                File.ReadAllLines(fileName)
                    .ToList()
                    .ForEach(row =>
                    {
                        //https://stackoverflow.com/questions/731452/create-instance-of-generic-type-whose-constructor-requires-a-parameter
                        var instance = (T)Activator.CreateInstance(typeof(T), new object[] { row });
                        allItem.Add(instance);
                    });
            }
            return allItem;
        }

        private void WriteItemsToFile(List<T> items)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                items.ForEach(e => sw.WriteLine(e));
                sw.Close();
            }
        }

        private void RemoveItemFromList(List<T> allItems, T item)
        {
            var foundItem = allItems.FirstOrDefault(e => e.Id == item.Id);
            if (foundItem == null)
            {
                throw new KeyNotFoundException($"Id {item.Id} not found");
            }
            allItems.Remove(foundItem);
        }

        public IEnumerable<T> Search(Func<T, bool> filter)
        {
            return this.GetAll().Where(filter);
        }

    }
}
