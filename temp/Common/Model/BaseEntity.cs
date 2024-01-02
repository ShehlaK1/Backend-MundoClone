
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "CHAR(36)"), MaxLength(36)]
        public string Id { get; set; }

        //public string CreatedBy { get; set; }
        //public string UpdatedBy { get; set; }

        //public DateTime CreatedOn { get; set; }
        //public DateTime UpdatedOn { get; set; }
    }
}
