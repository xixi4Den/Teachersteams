using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using Teachersteams.Domain;
using Teachersteams.Domain.Query;
using Teachersteams.Shared.Utilities;
using Group = Teachersteams.Domain.Entities.Group;

namespace Teachersteams.DataAccess
{
    public class UnitOfWork: BaseDisposable, IUnitOfWork
    {
        private readonly DbContext context;
        private readonly IDictionary<Type, object> repositories;

        protected UnitOfWork(DbContext context,
            IRepository<Group> groupRepository)
        {
            Contract.Requires<ArgumentNullException>(context != null, "context cannot be null");
            this.context = context;
            repositories = new Dictionary<Type,object>();

            RegisterRepository(groupRepository);
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public T Get<T>(Guid id) where T : Entity
        {
            return GetRepository<T>().Get(id);
        }

        public T GetSingleOrDefault<T>(BaseQueryParameters parameters) where T : Entity
        {
            return GetRepository<T>().GetSingleOrDefault(parameters);
        }

        public T GetFirstOrDefault<T>(BaseQueryParameters parameters) where T : Entity
        {
            return GetRepository<T>().GetFirstOrDefault(parameters);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return GetRepository<T>().GetAll();
        }

        public IEnumerable<T> GetAll<T>(BaseQueryParameters parameters) where T : Entity
        {
            return GetRepository<T>().GetAll(parameters);
        }

        public int Count<T>(BaseQueryParameters parameters) where T : Entity
        {
            return GetRepository<T>().Count(parameters);
        }

        public bool Any<T>(BaseQueryParameters parameters) where T : Entity
        {
            return GetRepository<T>().Any(parameters);
        }

        public T InsertOrUpdate<T>(T entity) where T : Entity
        {
            return GetRepository<T>().InsertOrUpdate(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            GetRepository<T>().Delete(entity);
        }

        public void Delete<T>(Guid id) where T : Entity
        {
            GetRepository<T>().Delete(id);
        }

        private IRepository<T> GetRepository<T>() where T : Entity
        {
            return (IRepository<T>)repositories[typeof(T)];
        }

        private void RegisterRepository<T>(IRepository<T> repository) where T : Entity
        {
            repositories[typeof (T)] = repository;
        }

        protected override void DisposeManaged()
        {
            DisposeContext();
        }

        private void DisposeContext()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}
