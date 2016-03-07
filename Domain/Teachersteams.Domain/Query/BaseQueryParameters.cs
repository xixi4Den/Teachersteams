using System;
using System.Collections.Generic;

namespace Teachersteams.Domain.Query
{
    /// <summary>
    /// Base class for query parameters. Includes sort settings, filter settings, page settings and other parameters for retrieving entities from repository.
    /// </summary>
    public class BaseQueryParameters
    {
        /// <summary>
        /// Represents list of unique identifiers.
        /// </summary>
        public IEnumerable<Guid> Ids
        {
            get; 
            set;
        }

        /// <summary>
        /// Represents sort rules to be applied in repository.
        /// </summary>
        public SortSettings SortSettings
        {
            get; 
            set;
        }

        /// <summary>
        /// Represents page rules to be applied in repository.
        /// </summary>
        public PageSettings PageSettings
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets empty parameters.
        /// </summary>
        public static BaseQueryParameters Empty
        {
            get
            {
                return new BaseQueryParameters();
            }
        }
    }
}
