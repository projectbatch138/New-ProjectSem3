
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.IReponsitory
{
    public class GenericReponsitory<T> : IGenericRepository<T> where T : class
    {
        protected DbContext _db { get; set; }
        protected DbSet<T> _table = null;
        public GenericReponsitory()
        {
            _db = new ApplicationDbContext();
            _table = _db.Set<T>();
        }
        public void Delete(object id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }

        public void Insert(T obj)
        {
            _table.Add(obj);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<T> SelectAll()
        {
            return _table.ToList();
        }

        public T SelectById(object id)
        {
            try
            {
                return _table.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public void Update(T obj)
        {
            _table.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
        }
    }
}