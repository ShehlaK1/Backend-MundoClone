using Common.DTOs;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using ORM.DatabaseContext;

namespace RESTCore.Services
{
    public class TargetSkillService
    {
        private readonly AppDbContext _appDbContext;
        public TargetSkillService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<TargetSkillDTO> GetAllTargetSkills()
        {
            return _appDbContext.TargetSkills.Select(x => new TargetSkillDTO
            {
              Name=x.Name,
              Description=x.Description,
              Level=x.Level,
                Id = x.Id,
            }).ToList();
        }

        public TargetSkillDTO GetTargetSkill(string id)
        {
            TargetSkill targetSkill = _appDbContext.TargetSkills.Where(x =>
            x.Id == id)
                .FirstOrDefault();
            return new TargetSkillDTO
            {
               Name=targetSkill.Name,
               Description=targetSkill.Description,
               Level=targetSkill.Level,
                Id = targetSkill.Id,
            };
        }


        public void CreateTargetSkill(TargetSkillDTO targetSkillDTO)
        {
            _appDbContext.TargetSkills.Add(new TargetSkill
            {
               Name = targetSkillDTO.Name,
               Description=targetSkillDTO.Description,
               Level=targetSkillDTO.Level,
            });
            _appDbContext.SaveChanges();
        }

        public bool UpdateTargetSkill(string id, string attributeToUpdate, string newValue)
        {
            TargetSkill targetSkill = _appDbContext.TargetSkills.Find(id);

            if (targetSkill != null)
            {
                // Use reflection to get the property dynamically and update it
                var propertyInfo = typeof(TargetSkill).GetProperty(attributeToUpdate);

                if (propertyInfo != null)
                {
                    // Convert the new value to the type of the property
                    object convertedValue = Convert.ChangeType(newValue, propertyInfo.PropertyType);

                    // Set the property value
                    propertyInfo.SetValue(targetSkill, convertedValue);
                    _appDbContext.SaveChanges();
                    return true;
                }

            }
            return false;
        }

        public bool DeleteTargetSkill(string id)
        {
            TargetSkill targetSkill = _appDbContext.TargetSkills.Where(x =>
            x.Id == id).SingleOrDefault();

            if (targetSkill == null)
            {
                return false;
            }
            _appDbContext.TargetSkills.Remove(targetSkill);
            _appDbContext.SaveChanges();
            return true;

        }
    }
}
