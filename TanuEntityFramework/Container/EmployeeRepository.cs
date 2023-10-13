using Microsoft.EntityFrameworkCore;
using TanuEntityFramework.Interface;
using TanuEntityFramework.Model;

namespace TanuEntityFramework.Container
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TanuDbContext _context;

        public EmployeeRepository(TanuDbContext context)
        {
            _context = context;
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
    }
}
