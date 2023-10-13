using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TanuEntityFramework.Interface;
using TanuEntityFramework.Model;

namespace TanuEntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            try
            {
                var data = _employeeRepository.GetAll();
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> AddEmployee(Employee emp)
        //{
        //    try
        //    {
        //        await _context.Employees.AddAsync(emp);
        //        await _context.SaveChangesAsync();
        //        return Ok(emp);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}