using AngularApiAssignment1.Models.Entities;
using AutoMapper;
using AngularApiAssignment1.DTO;

namespace AngularApiAssignment1.AutomapperConfig
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Employee,
                EmployeeRequestDTO>().ReverseMap();
            CreateMap<Skill,
                SkillRequestDTO>().ReverseMap();

        }
    }
}
