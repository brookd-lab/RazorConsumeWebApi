
using Product_Tutorial.Models;

namespace Product_Tutorial.Services
{
    public interface IEmployeeService
    {
        public Task<Employee> GetEmployeeById(int? id);
        public Task<List<Employee>> GetEmployees();
        public Task<HttpResponseMessage> CreateEmployee(Employee employee);
        public Task<HttpResponseMessage> UpdateEmployee(Employee employee);
        public Task<HttpResponseMessage> DeleteEmployeeById(int? id);
    }
}
