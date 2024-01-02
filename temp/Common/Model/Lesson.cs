namespace Common.Model
{
    public class Lesson : BaseEntity
    {
        public string Name { get; set; }
        public TargetSkill TargetSkill { get; set; }
        public string TargetSkillId { get; set; }
        public TimeOnly TimeToSpend { get; set; }
        public string Category { get; set; }
        public int? Frequency { get; set; }
        public string? Objective { get; set; }
        public string? PlanningInstructions { get; set; } 
    }
}
