using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    //public enum RoleSelection
    //{
    //    Super_Admin,
    //    Admin,
    //    Content_Manager,
    //    Site_Supervisor,
    //    Tutor
    //}

    //public enum AvailabilitySelection
    //{
    //    Part_Time,
    //    Full_Time,
    //    Contractual,

    //}
    public class User: BaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? NickName { get; set; }
        public string Role { get; set; }
        public DateTime HireDate { get; set; }
        public string Availability { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string EmailAddress { get; set; }

        [Phone(ErrorMessage = "Invalid home phone number")]
        public string? HomePhone { get; set; }

        [Phone(ErrorMessage = "Invalid mobile phone number")]
        public string MobilePhone { get; set; }


    }
}
