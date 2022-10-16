using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_assignment.Models;
using Final_assignment.Repository;
using System.Data.Entity;
using System.Threading;
using System.Security.Policy;
using System.Collections;
using System.Data.Entity.Migrations;
using Final_assignment.Base;
using System.Security.Permissions;
using System.Linq.Expressions;

namespace Final_assignment.Repository
{
    public class FoodRepository<T> :IFoodRepository<T> where T : class
    {
        private FoodStoreContext _context = null;
        private DbSet<T> table = null;
        public FoodRepository()
        {
            this._context = new FoodStoreContext();
            table = _context.Set<T>();
        }
        public FoodRepository(FoodStoreContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return (IQueryable<T>)table;
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
            Save();
        }
        public void Update(T obj)
        {
            table.AddOrUpdate(obj);
            Save();
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public IQueryable<T> search(Expression<Func<T, bool>> query) {
            return table.Where(query);
                }
    }
}