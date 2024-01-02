namespace Common.Model
{
    public class Assessment : BaseEntity
    {
        public int AssessmentNumber { get; set; }
        public TargetSkill TargetSkill { get; set; }
        public string TargetSkillId { get; set; }
        public string AssessmentTitle { get; set; } 
        public int TotalScore { get; set; }
        public int PassingScore { get; set; }
        public string? Details { get; set; } 
        public string? AdditionalNotes { get; set; } 
    }
}
