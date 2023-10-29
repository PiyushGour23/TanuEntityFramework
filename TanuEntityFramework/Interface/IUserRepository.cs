using TanuEntityFramework.Model;

namespace TanuEntityFramework.Interface
{
    public interface IUserRepository
    {
        List<User> GetAll();

        Task<User> AddUser(User user);
        Task<User> UpdateAsync(int id, User user);
        Task<string> DeleteAsync(int id);
    }
}
