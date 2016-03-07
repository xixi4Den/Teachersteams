using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Teachersteams.Domain;
using Teachersteams.Domain.Exceptions;
using Teachersteams.Domain.Query;
using Teachersteams.Shared.Validation;

namespace Teachersteams.DataAccess
{
    public class Repository<T>: IRepository<T>
        where T : Entity
    {
        private readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        private IDbSet<T> DbSet
        {
            get
            {
                return context.Set<T>();
            }
        } 

        public T Get(Guid id)
        {
            Contract.Requires<ArgumentException>(id != default(Guid), "id should not be empty");

            var entity = DbSet.Find(id);
            if (entity == null)
            {
                throw new DataNotFoundException();
            }
            return entity;
        }

        public T GetSingleOrDefault(BaseQueryParameters parameters)
        {
            IQueryable<T> queryable = DbSet.AsQueryable();
            queryable = ApplyFilterParameters(queryable, parameters);
            return queryable.SingleOrDefault();
        }

        public T GetFirstOrDefault(BaseQueryParameters parameters)
        {
            IQueryable<T> queryable = DbSet.AsQueryable();
            queryable = ApplyFilterParameters(queryable, parameters);
            queryable = ApplySortParameters(queryable, parameters);
            return queryable.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return GetAll(BaseQueryParameters.Empty);
        }

        public IEnumerable<T> GetAll(BaseQueryParameters parameters)
        {
            IQueryable<T> queryable = DbSet.AsQueryable();
            queryable = ApplyFilterParameters(queryable, parameters);
            queryable = ApplySortParameters(queryable, parameters);
            queryable = ApplyPageParameters(queryable, parameters);
            return queryable.ToList();
        }

        public int Count(BaseQueryParameters parameters)
        {
            IQueryable<T> queryable = DbSet.AsQueryable();
            queryable = ApplyFilterParameters(queryable, parameters);
            return queryable.Count();
        }

        public bool Any(BaseQueryParameters parameters)
        {
            IQueryable<T> queryable = DbSet.AsQueryable();
            queryable = ApplyFilterParameters(queryable, parameters);
            return queryable.Any();
        }

        public T InsertOrUpdate(T entity)
        {
            if (entity.IsNew)
            {
                DbSet.Add(entity);
            }
            else
            {
                DbSet.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
            }
            return entity;
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Delete(Guid id)
        {
            var entity = Get(id);
            Delete(entity);
        }

        protected virtual IQueryable<T> ApplyFilterParameters(IQueryable<T> queryable, BaseQueryParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters.Ids != null && parameters.Ids.Any())
                {
                    queryable = queryable.Where(x => parameters.Ids.Contains(x.Id));
                }
            }
            return queryable;
        } 

        private IQueryable<T> ApplySortParameters(IQueryable<T> queryable, BaseQueryParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters.SortSettings != null)
                {
                    // apply sort rules
                }
            }
            return queryable;
        }

        private IQueryable<T> ApplyPageParameters(IQueryable<T> queryable, BaseQueryParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters.PageSettings != null)
                {
                    // apply page rules
                }
            }
            return queryable;
        }
    }
}