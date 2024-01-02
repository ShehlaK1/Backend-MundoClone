using Common.DTOs;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using ORM.DatabaseContext;

namespace RESTCore.Services
{
    public class CompositeLevelService
    {
        private readonly AppDbContext _appDbContext;
        public CompositeLevelService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<CompositeLevelDTO> GetAllCompositeLevels()
        {
            return _appDbContext.CompositeLevels.Select(x => new CompositeLevelDTO
            {
               Name = x.Name,
               ShortName = x.ShortName,
               Description = x.Description,
               Id = x.Id,
            }).ToList();
        }

        public CompositeLevelDTO GetCompositeLevel(string id)
        {
            CompositeLevel compositeLevel = _appDbContext.CompositeLevels.Where(x =>
            x.Id == id)
                .FirstOrDefault();
            return new CompositeLevelDTO
            {
                Name=compositeLevel.Name,
                ShortName=compositeLevel.ShortName,
                Description=compositeLevel.Description,
                Id = compositeLevel.Id,
            };
        }


        public void CreateCompositeLevel(CompositeLevelDTO compositeLevelDTO)
        {
            _appDbContext.CompositeLevels.Add(new CompositeLevel
            {
                Name= compositeLevelDTO.Name,
                ShortName=compositeLevelDTO.ShortName,
                Description=compositeLevelDTO.Description,
            });
            _appDbContext.SaveChanges();
        }

        public bool UpdateCompositeLevel(string id, string attributeToUpdate, string newValue)
        {
            CompositeLevel compositeLevel = _appDbContext.CompositeLevels.Find(id);

            if (compositeLevel != null)
            {
                // Use reflection to get the property dynamically and update it
                var propertyInfo = typeof(CompositeLevel).GetProperty(attributeToUpdate);

                if (propertyInfo != null)
                {
                    // Convert the new value to the type of the property
                    object convertedValue = Convert.ChangeType(newValue, propertyInfo.PropertyType);

                    // Set the property value
                    propertyInfo.SetValue(compositeLevel, convertedValue);
                    _appDbContext.SaveChanges();
                    return true;
                }

            }
            return false;
        }

        public bool DeleteCompositeLevel(string id)
        {
            CompositeLevel compositeLevel = _appDbContext.CompositeLevels.Where(x =>
            x.Id == id).SingleOrDefault();

            if (compositeLevel == null)
            {
                return false;
            }
            _appDbContext.CompositeLevels.Remove(compositeLevel);
            _appDbContext.SaveChanges();
            return true;

        }
    }
}
