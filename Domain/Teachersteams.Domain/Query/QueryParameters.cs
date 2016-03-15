using System;
using System.Linq;
using System.Linq.Expressions;

namespace Teachersteams.Domain.Query
{
    /// <summary>
    /// Base class for query parameters. Includes sort settings, filter settings, page settings and other parameters for retrieving entities from repository.
    /// </summary>
    public class QueryParameters<T> where T : Entity
    {
        /// <summary>
        /// Represents filter rules to be applied in repository.
        /// </summary>
        public Expression<Func<T, bool>> FilterRules
        {
            get;
            set;
        }

        /// <summary>
        /// Represents sort rules to be applied in repository.
        /// </summary>
        public Func<IQueryable<T>, IOrderedQueryable<T>> SortRules
        {
            get; 
            set;
        }

        /// <summary>
        /// Represents page rules to be applied in repository.
        /// </summary>
        public PageSettings PageRules
        {
            get;
            set;
        }

        /// <summary>
        /// Gets empty parameters.
        /// </summary>
        public static QueryParameters<T> Empty
        {
            get
            {
                return new QueryParameters<T>();
            }
        }
    }
}
