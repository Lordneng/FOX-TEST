using FOX_TEST.Models.Entities;
using FOX_TEST.Services.Extensions;
using FOX_TEST.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace FOX_TEST.Services.Contracts
{
    public class EmployeeService : IEmployeeService
    {
        private readonly FoxContext _foxContext;
        private readonly ICacheService _cacheService;

        public EmployeeService(FoxContext foxContext,
            ICacheService cacheService)
        {
            _foxContext = foxContext;
            _cacheService = cacheService;
        }
        public List<Employee> Get()
        {
            var cacheData = _cacheService.GetData<List<Employee>>("employee");
            if (cacheData != null)
            {
                return cacheData;
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(2);
            cacheData = _foxContext.Employees.ToList();
            _cacheService.SetData<List<Employee>>("employee", cacheData, expirationTime);
            return cacheData;
        }

        public Employee? Get(int id)
        {
            var cacheData = _cacheService.GetData<Employee>("employee:" + id.ToString());
            if (cacheData != null)
            {
                return cacheData;
            }

            var expirationTime = DateTimeOffset.Now.AddMinutes(2);
            var dataEmp = _foxContext.Employees.FirstOrDefault(x => x.Id == id);
            var data = _foxContext.Employees.Include(x => x.Children).Where(x => x.ParentId == id).ToList();

            if (data != null)
            {
                dataEmp.Children = _getChildren(data);
                cacheData = dataEmp;
                _cacheService.SetData<Employee>("employee:" + id.ToString(), cacheData, expirationTime);
            }
                

            return cacheData;
        }

        public List<Employee>? GetChildren(int parentId)
        {
            var cacheData = _cacheService.GetData<List<Employee>?>("parent:" + parentId.ToString());
            if (cacheData != null)
            {
                return cacheData;
            }

            var expirationTime = DateTimeOffset.Now.AddMinutes(2);
            var data = _foxContext.Employees.Include(x => x.Children).Where(x => x.ParentId == parentId).ToList();
            if(data != null)
            {
                cacheData = _getChildren(data);
                _cacheService.SetData<List<Employee>?>("parent:" + parentId.ToString(), cacheData, expirationTime);
            }                

            return cacheData;
        }

        private List<Employee> _getChildren(List<Employee> data)
        {
            List<Employee> children = new List<Employee>();
            int i = 0;

            if(data.Count > 0)
            {
                children.AddRange(data);
            }

            foreach(Employee employee in data)
            {
                Employee? dbEmp = _foxContext.Employees.Include(x => x.Children).Where(o => o.Id == employee.Id).FirstOrDefault();

                if(dbEmp != null)
                {
                    if(dbEmp.Children == null)
                    {
                        i++;
                        continue;
                    }

                    List<Employee> child = dbEmp.Children.ToList();
                    dbEmp.Children = _getChildren(child);
                    children[i] = dbEmp;
                    i++;
                }
            }

            return children;
        }
    }
}
