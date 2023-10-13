using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TanuEntityFramework.Model;

namespace TanuEntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TanuController : ControllerBase
    {
        private readonly TanuDbContext _context;

        public TanuController(TanuDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                //return await _context.Users.ToListAsync();
                var records = await _context.Users.Select(x => new User()
                { 
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Password = x.Password,
                }).ToListAsync();
                return records;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
