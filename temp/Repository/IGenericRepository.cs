using System.Collections.Generic;
namespace RepositoryUsingEFinMVC.GenericRepository
{
    //Here, we are creating the IGenericRepository interface as a Generic Interface
    //Here, we are applying the Generic Constraint 
    //The constraint is, T is going to be a class
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}