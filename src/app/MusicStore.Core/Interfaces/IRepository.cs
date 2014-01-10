using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MusicStore.Core.Interfaces
{
   public interface IRepository<T> : IDisposable
      where T : class
   {
      T Add(T entity);
      T Update(T entity);
      void Delete(T entity);
      T GetById(params object[] id);
       T GetById(int id);
      IEnumerable<T> Query(Expression<Func<T, bool>> criteria);
      void Save();
   }


}
