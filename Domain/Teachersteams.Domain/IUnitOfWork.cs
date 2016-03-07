using System;
using System.Collections.Generic;
using Teachersteams.Domain.Query;

namespace Teachersteams.Domain
{
    /// <summary>
    /// Interface for unit of work.
    /// </summary>
    public interface IUnitOfWork: IDisposable
    {
        /// <summary>
        /// Commits all changes to database.
        /// </summary>
        void Commit();

        /// <summary>
        /// Gets entity by id from the repository of the {T} entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="id">The unique identifier.</param>
        /// <returns>The entity that was found.</returns>
        T Get<T>(Guid id) where T : Entity;

        /// <summary>
        /// Gets single entity from the repository of the {T} entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The single entity that satisfies query parameters or null otherwise.</returns>
        /// <exception cref="InvalidOperationException">More than one entity satisfies query parameters.</exception>
        T GetSingleOrDefault<T>(BaseQueryParameters parameters) where T : Entity;

        /// <summary>
        /// Gets first entity from the repository of the {T} entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The first entity that satisfies query parameters or null otherwise.</returns>
        T GetFirstOrDefault<T>(BaseQueryParameters parameters) where T : Entity;

        /// <summary>
        /// Gets all entities from the repository of the {T} entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <returns>The list of entities.</returns>
        IEnumerable<T> GetAll<T>() where T : Entity;

        /// <summary>
        /// Gets all entities that correspond query parameters from the repository of the {T} entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The list of entities.</returns>
        IEnumerable<T> GetAll<T>(BaseQueryParameters parameters) where T : Entity;

        /// <summary>
        /// Gets count of entities that correspond query parametes from the repository of the {T} entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The count of entities.</returns>
        int Count<T>(BaseQueryParameters parameters) where T : Entity;

        /// <summary>
        /// Indicates is there any entities that correspond query parameters from the repository of the {T} entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The query parameters.</returns>
        bool Any<T>(BaseQueryParameters parameters) where T : Entity;

        /// <summary>
        /// Saves entity in the repository of the {T} entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entity">The query parameters.</param>
        /// <returns>The saved entity.</returns>
        T InsertOrUpdate<T>(T entity) where T : Entity;

        /// <summary>
        /// Deletes the entity from the repository of the {T} entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entity">The query parameters.</param>
        void Delete<T>(T entity) where T : Entity;

        /// <summary>
        /// Deletes the entity by id from the repository of the {T} entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="id">The unique identifier.</param>
        void Delete<T>(Guid id) where T : Entity;
    }
}
