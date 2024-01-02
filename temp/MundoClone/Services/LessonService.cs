using Common.DTOs;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using ORM.DatabaseContext;

namespace RESTCore.Services
{
    public class LessonService
    {
        private readonly AppDbContext _appDbContext;
        public LessonService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<LessonDTO> GetAllLessons()
        {
            return _appDbContext.Lessons.Select(x => new LessonDTO
            {
               Name= x.Name,
               TargetSkillId= x.TargetSkillId,
               TimeToSpend= x.TimeToSpend,
               Category= x.Category,
               Frequency= x.Frequency,
               Objective= x.Objective,
               PlanningInstructions= x.PlanningInstructions,
               Id = x.Id,
            }).ToList();
        }

        public LessonDTO GetLesson(string id)
        {
            Lesson lesson = _appDbContext.Lessons.Where(x =>
            x.Id == id)
                .FirstOrDefault();
            return new LessonDTO
            {
                Name = lesson.Name,
                TargetSkillId= lesson.TargetSkillId,
                TimeToSpend = lesson.TimeToSpend,
                Category= lesson.Category,
                Frequency= lesson.Frequency,
                Objective= lesson.Objective,
                PlanningInstructions= lesson.PlanningInstructions,
                Id = lesson.Id,
            };
        }


        public void CreateLesson(LessonDTO lessonDTO)
        {
            _appDbContext.Lessons.Add(new Lesson
            {
                Name = lessonDTO.Name,
                TargetSkillId = lessonDTO.TargetSkillId,
                TimeToSpend = lessonDTO.TimeToSpend,
                Category= lessonDTO.Category,
                Frequency= lessonDTO.Frequency,
                Objective= lessonDTO.Objective,
                PlanningInstructions= lessonDTO.PlanningInstructions,   
            });
            _appDbContext.SaveChanges();
        }

        public bool UpdateLesson(string id, string attributeToUpdate, string newValue)
        {
            Lesson lesson = _appDbContext.Lessons.Find(id);

            if (lesson != null)
            {
                // Use reflection to get the property dynamically and update it
                var propertyInfo = typeof(Lesson).GetProperty(attributeToUpdate);

                if (propertyInfo != null)
                {
                    // Convert the new value to the type of the property
                    object convertedValue = Convert.ChangeType(newValue, propertyInfo.PropertyType);

                    // Set the property value
                    propertyInfo.SetValue(lesson, convertedValue);
                    _appDbContext.SaveChanges();
                    return true;
                }

            }
            return false;
        }

        public bool DeleteLesson(string id)
        {
            Lesson lesson = _appDbContext.Lessons.Where(x =>
            x.Id == id).SingleOrDefault();

            if (lesson == null)
            {
                return false;
            }
            _appDbContext.Lessons.Remove(lesson);
            _appDbContext.SaveChanges();
            return true;

        }
    }
}
