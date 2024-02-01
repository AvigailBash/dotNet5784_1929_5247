namespace DalApi;

/// <summary>
/// An interface that defines generic CRUD methods
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICrud<T> where T : class
{
    /// <summary>
    /// Creates new entity object in DAL
    /// </summary>
    /// <param name="item"> The object that the method receives </param>
    /// <returns></returns>
    int Create(T item);

    /// <summary>
    /// Returns an object according to a certain search
    /// </summary>
    /// <param name="filter"> What you are looking for on each object </param>
    /// <returns></returns>
    T? Read(Func<T, bool> filter);

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id"> The ID of the object </param>
    /// <returns></returns>
    T? Read(int id);

    /// <summary>
    /// Returns all objects according to a certain search
    /// </summary>
    /// <param name="filter"> According to what are the objective ones searched for </param>
    /// <returns></returns>
    IEnumerable<T> ReadAll(Func<T, bool>? filter = null);

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item"> The object with the updated details </param>
    void Update(T item);

    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id"> The ID of the object to delete</param>
    void Delete(int id);

    /// <summary>
    /// A method for deleting all objects from the same entity
    /// </summary>
    void deleteAll();
}
