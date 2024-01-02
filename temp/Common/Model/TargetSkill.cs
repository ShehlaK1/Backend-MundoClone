namespace Common.Model
{
    public class TargetSkill: BaseEntity
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string? Description { get; set; }
    }
}
