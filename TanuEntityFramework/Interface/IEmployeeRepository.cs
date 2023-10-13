using TanuEntityFramework.Model;

namespace TanuEntityFramework.Interface
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
    }
}
