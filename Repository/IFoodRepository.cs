using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Final_assignment.Repository
{
    public interface IFoodRepository <T> where T:class
    {
        IQueryable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
      IQueryable<T> search(Expression<Func<T, bool>>query);
       


    }
}
