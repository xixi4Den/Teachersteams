using System;
using System.Collections.Generic;
using Teachersteams.Domain.Exceptions;
using Teachersteams.Domain.Query;

namespace Teachersteams.Domain
{
    /// <summary>
    /// Interface for repositories of the system.
    /// </summary>
    /// <typeparam name="T">Type of entity that repository works with.</typeparam>
    public interface IRepository<T> 
        where T : Entity
    {
        /// <summary>
        /// Gets entity from the repository by id.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity that was found.</returns>
        /// <exception cref="DataNotFoundException">The entity with specified unique identifier is not found.</exception>
        T Get(Guid id);

        /// <summary>
        /// Gets single entity from the repository.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>The single entity that satisfies query parameters or null otherwise.</returns>
        /// <exception cref="InvalidOperationException">More than one entity satisfies query parameters.</exception>
        T GetSingleOrDefault(BaseQueryParameters parameters);

        /// <summary>
        /// Gets first entity from the repository.
        /// </summary>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The first entity that satisfies query parameters or null otherwise.</returns>
        T GetFirstOrDefault(BaseQueryParameters parameters);

        /// <summary>
        /// Gets all entities from the repository.
        /// </summary>
        /// <returns>The list of entities.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets all entities that correspond query parameters.
        /// </summary>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The list of entities.</returns>
        IEnumerable<T> GetAll(BaseQueryParameters parameters);

        /// <summary>
        /// Gets count of entities that correspond query parametes.
        /// </summary>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The count of entities.</returns>
        int Count(BaseQueryParameters parameters);

        /// <summary>
        /// Indicates is there any entities that correspond query parameters.
        /// </summary>
        /// <param name="parameters">The query parameters.</param>
        bool Any(BaseQueryParameters parameters);

        /// <summary>
        /// Saves entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>The saved entity.</returns>
        T InsertOrUpdate(T entity);

        /// <summary>
        /// Deletes the entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes the entity by id from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <exception cref="DataNotFoundException">The entity with specified unique identifier is not found.</exception>
        void Delete(Guid id);
    }
}
