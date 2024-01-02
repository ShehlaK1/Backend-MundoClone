using Common.Model;

namespace Common.DTOs
{
    public class AssessmentDTO : BaseDTO
    {
        public int AssessmentNumber { get; set; }
        public string TargetSkillId { get; set; }
        public string AssessmentTitle { get; set; }
        public int TotalScore { get; set; }
        public int PassingScore { get; set; }
        public string? Details { get; set; }
        public string? AdditionalNotes { get; set; }
    }
}
