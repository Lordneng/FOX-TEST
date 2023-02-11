using FOX_TEST.Models.Entities;
using FOX_TEST.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace FOX_TEST.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService; 
        }

        [HttpGet()]
        public List<Employee> Get()
        {
            return _employeeService.Get();
        }

        [HttpGet("ById")]
        public Employee? Get(int id)
        {
            return _employeeService.Get(id);
        }

        [HttpGet("Children")]
        public List<Employee>? GetChildren(int parentId)
        {
            return _employeeService.GetChildren(parentId);
        }
    }
}
