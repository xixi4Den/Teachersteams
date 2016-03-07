using System;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id
        {
            get; 
            set;
        }

        /// <summary>
        /// Concurrency check property.
        /// </summary>
        public byte[] Version
        {
            get; 
            set;
        }

        /// <summary>
        /// Determines whether entity is new.
        /// </summary>
        public virtual bool IsNew
        {
            get
            {
                return Id == Guid.Empty;
            }
        }
    }
}
