using System.Collections.Generic;

namespace TodoSampleBackend.Interfaces.Business
{
    public interface IBusinessManager<T>
    {
        /// <summary>
        /// Finds entity with id 
        /// </summary>
        /// <param name="id">integer represent the id of the entity desired</param>
        /// <returns>an instnace of enity with the id</returns>
        T Find(string id);

        /// <summary>
        /// Adds a new entity to the database
        /// </summary>
        /// <param name="entity">entity database object</param>
        /// <returns>true if successful and false if failure</returns>
        bool Add(T entity);

        /// <summary>
        /// Returns a list of Appointments
        /// </summary>
        /// <returns>List of glossaries</returns>
        List<T> All();

        /// <summary>
        /// Removes a entity from the database
        /// </summary>
        /// <param name="entity">entity database object</param>
        /// <returns>true if successful and false if failure</returns>
        bool Remove(T entity);

        /// <summary>
        /// Updates entity from the database
        /// </summary>
        /// <param name="entity">entity database object</param>
        /// <returns>true if successful and false if failure</returns>
        bool Update(T entity);
    }
}
