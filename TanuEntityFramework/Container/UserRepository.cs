using Microsoft.EntityFrameworkCore;
using TanuEntityFramework.Interface;
using TanuEntityFramework.Model;

namespace TanuEntityFramework.Container
{
    public class UserRepository : IUserRepository
    {
        private readonly TanuDbContext _context;

        public UserRepository(TanuDbContext context)
        {
            _context = context;
        }

        public List<User> GetAll() 
        {
            return _context.Users.ToList();
        }

        public async Task<User> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //public async Task<User> UpdateAsync(int id, User user)
        //{
        //    //await _context.Users.FindAsync(id);
        //    var existinguser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        //    if (existinguser != null)
        //    {
                
        //    }
        //}
    }
}
