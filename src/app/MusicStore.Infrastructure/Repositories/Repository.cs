using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using MusicStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MusicStore.Infrastructure.Repositories
{

    public class Repository<T> : IRepository<T> where T : class
    {

        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet;
        private bool _disposed;

        public Repository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new InvalidOperationException("unitOfWork Context should not be null");
            DbContext = unitOfWork as DbContext;
            DbSet = DbContext.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet;
        }

        public virtual T Add(T entity)
        {
            DbEntityEntry<T> entityEntry = DbContext.Entry(entity);
            if (entityEntry.State != EntityState.Detached)
                entityEntry.State = EntityState.Added;
            else
                DbSet.Add(entity);
            return entity;
        }
        public virtual T Update(T entity)
        {
            DbEntityEntry<T> entityEntry = DbContext.Entry(entity);
            if (entityEntry.State == EntityState.Detached)
                DbSet.Add(entity);
            entityEntry.State = EntityState.Modified;
            return entity;
        }
        public virtual void Delete(T entity)
        {
            DbEntityEntry<T> entityEntry = DbContext.Entry(entity);
            if (entityEntry.State != EntityState.Detached)
                entityEntry.State = EntityState.Deleted;
            else
                DbSet.Attach(entity);
            DbSet.Remove(entity);

        }
        public virtual void Save()
        {
            DbContext.SaveChanges();
        }

        public virtual T GetById(params object[] id)
        {
            return DbSet.Find(id);
        }
        public virtual IEnumerable<T> Query(Expression<Func<T, bool>> criteria)
        {
            return DbSet.Where(criteria);
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

     

      

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (DbContext != null)
                    {
                        DbContext.Dispose();
                    }
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Repository()
        {
            Dispose(false);
        }
    }
}
