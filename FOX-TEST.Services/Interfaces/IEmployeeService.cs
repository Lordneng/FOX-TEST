using FOX_TEST.Models.Entities;

namespace FOX_TEST.Services.Interfaces
{
    public interface IEmployeeService
    {
        List<Employee> Get();
        Employee? Get(int id);
        List<Employee>? GetChildren(int parentId);
    }
}
