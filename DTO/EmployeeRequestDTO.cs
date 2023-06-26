
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AngularApiAssignment1.DTO
{
    public class EmployeeRequestDTO
    {
        [Key, Required]
        public int Id { get; set; }

        [StringLength(30, ErrorMessage = "Name should be maximum 30 length.")]
        [Required(ErrorMessage = "Please Enter Employee Name.")]
        [RegularExpression(pattern: "[a-zA-Z ]*$", ErrorMessage = "Please enter only alphabets.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Select Gender.")]
        public string Gender { get; set; }

        [StringLength(10, ErrorMessage = "ContactNumber should be maximum 30 length.")]
        public string ContactNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        public List<SkillRequestDTO> employeeSkills { get; set; } = new List<SkillRequestDTO>();
    }
}
