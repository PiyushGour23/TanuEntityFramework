using TanuEntityFramework.Model;

namespace TanuEntityFramework.Interface
{
    public interface IUserRepository
    {
        List<User> GetAll();

        Task<User> AddUser(User user);
    }
}
