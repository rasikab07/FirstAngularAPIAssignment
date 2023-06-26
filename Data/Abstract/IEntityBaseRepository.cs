using System;
using System.Linq;
using System.Linq.Expressions;
using AngularApiAssignment1.Models;

namespace AngularApiAssignment1.Data.Abstract
{
    public interface IEntityBaseRepository<T> where T : class, new()
    {
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetAll();
        int Count();
        T GetSingle(int id);
        T GetSingle(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
       
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        void Commit();

    }
}