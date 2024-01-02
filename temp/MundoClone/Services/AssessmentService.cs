using Common.DTOs;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using ORM.DatabaseContext;

namespace RESTCore.Services
{
    public class AssessmentService
    {
        private readonly AppDbContext _appDbContext;
        public AssessmentService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<AssessmentDTO> GetAllAssessments()
        {
            return _appDbContext.Assessments.Select(x => new AssessmentDTO
            {
              AssessmentNumber = x.AssessmentNumber,
              TargetSkillId = x.TargetSkillId,
              AssessmentTitle = x.AssessmentTitle,
              TotalScore = x.TotalScore,
              PassingScore = x.PassingScore,
              Details = x.Details,
              AdditionalNotes = x.AdditionalNotes,
              Id=x.Id,
            }).ToList();
        }

        public AssessmentDTO GetAssessment(string id)
        {
            Assessment assessment = _appDbContext.Assessments.Where(x =>
            x.Id == id)
                .FirstOrDefault();
            return new AssessmentDTO
            {
               AssessmentNumber=assessment.AssessmentNumber,
               TargetSkillId=assessment.TargetSkillId,
               AssessmentTitle=assessment.AssessmentTitle,
               TotalScore=assessment.TotalScore,
               PassingScore=assessment.PassingScore,
               Details = assessment.Details,
               AdditionalNotes = assessment.AdditionalNotes,
               Id=assessment.Id,
            };
        }


        public void CreateAssessment(AssessmentDTO assessmentDTO)
        {
            _appDbContext.Assessments.Add(new Assessment
            {
                AssessmentNumber =(int)assessmentDTO.AssessmentNumber,
                TargetSkillId=assessmentDTO.TargetSkillId,
                AssessmentTitle=assessmentDTO.AssessmentTitle,
                TotalScore=assessmentDTO.TotalScore,
                PassingScore=(int)assessmentDTO.PassingScore,
                Details = assessmentDTO.Details,
                AdditionalNotes=assessmentDTO.AdditionalNotes,
            });
            _appDbContext.SaveChanges();
        }

        public bool UpdateAssessment(string id, string attributeToUpdate, string newValue)
        {
            Assessment assessment = _appDbContext.Assessments.Find(id);

            if (assessment != null)
            {
                // Use reflection to get the property dynamically and update it
                var propertyInfo = typeof(Assessment).GetProperty(attributeToUpdate);

                if (propertyInfo != null)
                {
                    // Convert the new value to the type of the property
                    object convertedValue = Convert.ChangeType(newValue, propertyInfo.PropertyType);

                    // Set the property value
                    propertyInfo.SetValue(assessment, convertedValue);
                    _appDbContext.SaveChanges();
                    return true;
                }

            }
            return false;
        }

        public bool DeleteAssessment(string id)
        {
            Assessment assessment = _appDbContext.Assessments.Where(x =>
            x.Id == id).SingleOrDefault();

            if (assessment == null)
            {
                return false;
            }
            _appDbContext.Assessments.Remove(assessment);
            _appDbContext.SaveChanges();
            return true;

        }
    }
}
