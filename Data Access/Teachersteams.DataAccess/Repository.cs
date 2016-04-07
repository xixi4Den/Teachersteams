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
        private readonly Context context;

        public Repository(Context context)
        {
            this.context = context;
        }

        public Context Context
        {
            get { return context; }
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

        public T GetSingleOrDefault(QueryParameters<T> parameters)
        {
            IQueryable<T> queryable = DbSet.AsQueryable();
            queryable = ApplyFilterParameters(queryable, parameters);
            return queryable.SingleOrDefault();
        }

        public T GetFirstOrDefault(QueryParameters<T> parameters)
        {
            IQueryable<T> queryable = DbSet.AsQueryable();
            queryable = ApplyFilterParameters(queryable, parameters);
            queryable = ApplySortParameters(queryable, parameters);
            return queryable.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return GetAll(QueryParameters<T>.Empty);
        }

        public IEnumerable<T> GetAll(QueryParameters<T> parameters)
        {
            IQueryable<T> queryable = DbSet.AsQueryable();
            queryable = ApplyFilterParameters(queryable, parameters);
            queryable = ApplySortParameters(queryable, parameters);
            queryable = ApplyPageParameters(queryable, parameters);
            return queryable.ToList();
        }

        public int Count(QueryParameters<T> parameters)
        {
            IQueryable<T> queryable = DbSet.AsQueryable();
            queryable = ApplyFilterParameters(queryable, parameters);
            return queryable.Count();
        }

        public bool Any(QueryParameters<T> parameters)
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

        protected virtual IQueryable<T> ApplyFilterParameters(IQueryable<T> queryable, QueryParameters<T> parameters)
        {
            if (parameters != null)
            {
                if (parameters.FilterRules != null)
                {
                    return queryable.Where(parameters.FilterRules);
                }
            }
            return queryable;
        } 

        private IQueryable<T> ApplySortParameters(IQueryable<T> queryable, QueryParameters<T> parameters)
        {
            if (parameters != null)
            {
                if (parameters.SortRules != null)
                {
                    return parameters.SortRules(queryable);
                }
            }
            return queryable;
        }

        private IQueryable<T> ApplyPageParameters(IQueryable<T> queryable, QueryParameters<T> parameters)
        {
            if (parameters != null)
            {
                if (parameters.PageRules != null)
                {
                    var p = parameters.PageRules;
                    return queryable.Skip((p.Index - 1)*p.Size).Take(p.Size);
                }
            }
            return queryable;
        }
    }
}