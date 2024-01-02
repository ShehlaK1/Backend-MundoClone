using Common.Model;

namespace Common.DTOs
{
    public class LessonDTO : BaseDTO
    {
        public string Name { get; set; }

        public string TargetSkillId { get; set; }
        public TimeOnly TimeToSpend { get; set; }
        public string Category { get; set; }
        public int? Frequency { get; set; }
        public string? Objective { get; set; }
        public string? PlanningInstructions { get; set; }
    }
}
