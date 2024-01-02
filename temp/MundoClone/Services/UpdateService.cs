using Microsoft.EntityFrameworkCore;
using ORM.DatabaseContext;
using System.Reflection;

namespace RESTCore.Services
{
    public class UpdateService<TEntity> where TEntity : class
    {
        private readonly AppDbContext _appDbContext;

        public UpdateService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool UpdateEntity<TProperty>(string id, string attributeToUpdate, TProperty newValue) where TProperty : IConvertible
        {
            TEntity entity = _appDbContext.Set<TEntity>().Find(id);

            if (entity != null)
            {
                // Use reflection to get the property dynamically and update it
                var propertyInfo = typeof(TEntity).GetProperty(attributeToUpdate, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    // Convert the new value to the type of the property
                    object convertedValue = Convert.ChangeType(newValue, propertyInfo.PropertyType);

                    // Set the property value
                    propertyInfo.SetValue(entity, convertedValue);
                    _appDbContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }
    }
}
