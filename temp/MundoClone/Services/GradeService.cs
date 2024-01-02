using Common.DTOs;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using ORM.DatabaseContext;

namespace RESTCore.Services
{
    public class GradeService
    {
        private readonly AppDbContext _appDbContext;
        public GradeService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<GradeDTO> GetAllGrades()
        {
            return _appDbContext.Grades.Select(x => new GradeDTO
            {
                Name = x.Name,
                Description = x.Description,
                Id = x.Id,
            }).ToList();
        }

        public GradeDTO GetGrade(string id)
        {
            Grade grade = _appDbContext.Grades.Where(x =>
            x.Id == id)
                .FirstOrDefault();
            return new GradeDTO
            {
                Name = grade.Name,
                Description = grade.Description,
                Id = grade.Id,
            };
        }


        public void CreateGrade(GradeDTO gradeDTO)
        {
            _appDbContext.Grades.Add(new Grade
            {
                Name=gradeDTO.Name,
                Description=gradeDTO.Description,
            });
            _appDbContext.SaveChanges();
        }

        public bool UpdateGrade(string id, string attributeToUpdate, string newValue)
        {
            Grade grade = _appDbContext.Grades.Find(id);

            if (grade != null)
            {
                // Use reflection to get the property dynamically and update it
                var propertyInfo = typeof(Grade).GetProperty(attributeToUpdate);

                if (propertyInfo != null)
                {
                    // Convert the new value to the type of the property
                    object convertedValue = Convert.ChangeType(newValue, propertyInfo.PropertyType);

                    // Set the property value
                    propertyInfo.SetValue(grade, convertedValue);
                    _appDbContext.SaveChanges();
                    return true;
                }

            }
            return false;
        }

        public bool DeleteGrade(string id)
        {
            Grade grade = _appDbContext.Grades.Where(x =>
            x.Id == id).SingleOrDefault();

            if (grade == null)
            {
                return false;
            }
            _appDbContext.Grades.Remove(grade);
            _appDbContext.SaveChanges();
            return true;

        }
    }
}
