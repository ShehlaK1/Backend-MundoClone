using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ORM.DatabaseContext;

namespace RepositoryUsingEFinMVC.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AppDbContext _appDbContext = null;
        private DbSet<T> table = null;

        public GenericRepository() 
        {
            this._appDbContext = new AppDbContext();
            table = _appDbContext.Set<T>();
        }

        public GenericRepository(AppDbContext appDbContext, DbSet<T> table)
        {
            _appDbContext = appDbContext;
            table= _appDbContext.Set<T>();  
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(int id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _appDbContext.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T obj = table.Find(id);
            if (obj != null)
            {
                table.Remove(obj);
            }
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
