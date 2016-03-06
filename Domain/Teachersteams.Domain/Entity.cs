using System;

namespace Teachersteams.Domain
{
    /// <summary>
    /// Base class for data entities.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public Guid Id
        {
            get; 
            set;
        }
    }
}
