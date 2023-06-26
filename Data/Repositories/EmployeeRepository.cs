using AngularApiAssignment1.Data.Abstract;
using AngularApiAssignment1.Data.Base;
using AngularApiAssignment1.Models.Entities;

namespace AngularApiAssignment1.Data.Repositories
{
    public class EmployeeRepository : EntityBaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ManageEmployeesContext context) : base(context) { }
    }
}
