using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HSIS_Web.Models
{
    public class Assistant
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Nick name")]
        public string NickName { get; set; }
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public int Salary { get; set; }

        [Display(Name = "Phone line number")]
        public string PhoneLineNumber { get; set; }
        public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

        [Display(Name = "Assistant")]
        public string FullName => LastName + " " + FirstName;
    }
}