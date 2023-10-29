using Microsoft.EntityFrameworkCore;
using System.Reflection;
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

        public async Task<User> UpdateAsync(int id, User user)
        {
            //await _context.Users.FindAsync(id);
            var existinguser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existinguser != null)
            {
                _context.Entry(existinguser).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
                return user;
            }
            return null;
        }

        public async Task<string> DeleteAsync(int id)
        {
            var data = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                 _context.Users.Remove(data);
                await _context.SaveChangesAsync();
                return string.Empty;
            }
            return null;
        }
    }
}
