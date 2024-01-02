using Common.DTOs;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using ORM.DatabaseContext;

namespace RESTCore.Services
{
    public class YearService
    {
        private readonly AppDbContext _appDbContext;
        public YearService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<YearDTO> GetAllYears()
        {
            return _appDbContext.Years.Select(x => new YearDTO
            {
                Name = x.Name,
               StartDate=x.StartDate,
               EndDate=x.EndDate,
               Id = x.Id,
            }).ToList();
        }

        public YearDTO GetYear(string id)
        {
            Year year = _appDbContext.Years.Where(x =>
            x.Id == id)
                .FirstOrDefault();
            return new YearDTO
            {
                Name = year.Name,
                StartDate = year.StartDate,
                EndDate = year.EndDate,
                Id = year.Id,
            };
        }


        public void CreateYear(YearDTO yearDTO)
        {
            _appDbContext.Years.Add(new Year
            {
                Name= yearDTO.Name,
                StartDate = yearDTO.StartDate,
                EndDate=yearDTO.EndDate,
            });
            _appDbContext.SaveChanges();
        }


        public bool UpdateYear(string id, string attributeToUpdate, string newValue)
        {
            Year year = _appDbContext.Years.Find(id);

            if (year != null)
            {
                // Use reflection to get the property dynamically and update it
                var propertyInfo = typeof(Year).GetProperty(attributeToUpdate);

                if (propertyInfo != null)
                {
                    // Convert the new value to the type of the property
                    object convertedValue = Convert.ChangeType(newValue, propertyInfo.PropertyType);

                    // Set the property value
                    propertyInfo.SetValue(year, convertedValue);
                    _appDbContext.SaveChanges();
                    return true;
                }

            }
            return false;
        }

        public bool DeleteYear(string id)
        {
            Year year = _appDbContext.Years.Where(x =>
            x.Id == id).SingleOrDefault();

            if (year == null)
            {
                return false;
            }
            _appDbContext.Years.Remove(year);
            _appDbContext.SaveChanges();
            return true;

        }
    }
}
