using System;
using System.Linq;
using AngularApiAssignment1.Data.Abstract;
using AngularApiAssignment1.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using AngularApiAssignment1.Models.Enums;
using Newtonsoft.Json;
using AutoMapper;
using AngularApiAssignment1.DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace AngularApiAssignment1.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _imapper;
        public EmployeeController(IEmployeeRepository employeeRepository, 
                                  IMapper imapper)
        {
            this._employeeRepository = employeeRepository;
            this._imapper = imapper;
        }
        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };

        [HttpGet]
        public IActionResult Get()
        {
            //var employees = _employeeRepository
            //    .AllIncluding(a => a.Skills).ToListAsync();

            //if (employees!=null)
            //    return Ok(employees);
            //return NoContent();

            var employees = JsonConvert.SerializeObject(_employeeRepository
                .AllIncluding(p => p.employeeSkills)
                , Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            ContractResolver = contractResolver,
                            Formatting = Formatting.Indented
                        });
            if (employees!=null)
                return Ok(employees);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var employee = JsonConvert.SerializeObject(_employeeRepository.AllIncluding(p => p.employeeSkills).Where(p => p.Id == id), Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            ContractResolver = contractResolver,
                            Formatting = Formatting.Indented
                        }); 
            if (employee != null) return Ok(employee);

            return NotFound();
        }

        
        [HttpPost]
        public IActionResult Post([FromBody]  EmployeeRequestDTO employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var _employee = employee;
                if (_employee == null) throw new ArgumentNullException(nameof(_employee));
                Employee employeemap = _imapper.Map<EmployeeRequestDTO, Employee>(_employee);
                _employeeRepository.Add(employeemap);
                _employeeRepository.Commit();
                
                if (_employee == null) throw new ArgumentNullException(nameof(_employee));
                //return Created($"/{employeemap.Id}", employeemap);
                return Ok(200);
                //return Created($"/api/employee/{employeemap.Id}", employeemap);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] EmployeeRequestDTO employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Employee valid employee name");
                }
                var _employee = _employeeRepository.GetSingle(id);
                if (_employee == null)
                {
                    throw new Exception($"Record not found");
                }
                else
                {
                    _employee.Id = employee.Id;
                    _employee.Name = employee.Name;
                    _employee.ContactNumber = employee.ContactNumber;
                    _employee.Email = employee.Email;
                    _employee.Gender = employee.Gender;


                    var existingSkills = _employeeRepository.AllIncluding(x => x.employeeSkills).FirstOrDefault(x => x.Id == employee.Id);
                    foreach(var empskills in existingSkills.employeeSkills.ToList())
                    {
                        _employee.employeeSkills.Remove(empskills);
                    }
                   employee.employeeSkills.ForEach(e =>
                    {
                        var result = _employee.employeeSkills.FirstOrDefault(x => x.SkillName == e.SkillName);
                        
                        if (result == null)
                        {
                            Skill skill = new Skill();
                            skill.EmployeeId = employee.Id;
                            skill.SkillName = e.SkillName;
                            skill.SkillExperience = e.SkillExperience;
                            _employee.employeeSkills.Add(skill);
                        }
                        else
                        {
                            _employee.employeeSkills.FirstOrDefault(x => x.SkillName == e.SkillName).SkillName = e.SkillName;
                            _employee.employeeSkills.FirstOrDefault(x => x.SkillName == e.SkillName).SkillExperience = e.SkillExperience;

                        }
                    });
                    _employeeRepository.Commit();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Employee employee = _employeeRepository.GetSingle(id);

                var existingSkills = _employeeRepository.AllIncluding(x => x.employeeSkills).FirstOrDefault(x => x.Id == employee.Id);
                foreach (var empskills in existingSkills.employeeSkills.ToList())
                {
                    employee.employeeSkills.Remove(empskills);
                }

                _employeeRepository.Commit();

                if (employee == null) return new NotFoundResult();

                _employeeRepository.Delete(employee);
                _employeeRepository.Commit();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
