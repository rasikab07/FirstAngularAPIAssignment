using System.ComponentModel.DataAnnotations;

namespace AngularApiAssignment1.DTO
{
    public class SkillRequestDTO
    {       

        [Required(ErrorMessage = "Please Enter Skill Name.")]
        public string SkillName { get; set; }

        [Required(ErrorMessage = "Please Select Skill Experience.")]
        public string SkillExperience { get; set; }
        public int EmployeeId { get; set; }
    }
}
