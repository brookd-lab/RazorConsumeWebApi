using Newtonsoft.Json;
using Product_Tutorial.Models;
using System.Text;

namespace Product_Tutorial.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly string _apiBaseUrl;

        public EmployeeService(IConfiguration config)
        {
            _config = config;
            _client = new HttpClient();
            _apiBaseUrl = _config.GetValue<string>("MySettings:ApiBaseUrl")!;
             _client.BaseAddress = new Uri(_apiBaseUrl);
        }

        private string GetEmployeeUrlById(int? id)
        {
            return $"{_apiBaseUrl}/{id}";
        }

        public async Task<Employee> GetEmployeeById(int? id)
        {
            Employee? employee = null;
            var findEmployeeUrl = GetEmployeeUrlById(id);
            var response = await _client.GetAsync(findEmployeeUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(jsonData)!;
            }
            return employee!;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee>? result = null;
            var response = await _client.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<Employee>>(jsonData)!;
            }
            return result!;
        }

        public async Task<HttpResponseMessage> CreateEmployee(Employee employee)
        {
            var jsonData = JsonConvert.SerializeObject(employee);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await _client.PostAsync($"{_apiBaseUrl}", content);
            return responseMessage;
        }

        public async Task<HttpResponseMessage> UpdateEmployee(Employee employee)
        {
            var jsonData = JsonConvert.SerializeObject(employee);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await _client.PutAsync($"{_apiBaseUrl}", content);
            return responseMessage;
        }

        public async Task<HttpResponseMessage> DeleteEmployeeById(int? id)
        {
            Employee? employee = null;
            var findEmployeeUrl = GetEmployeeUrlById(id);
            var response = await _client.DeleteAsync(findEmployeeUrl);
            return response;
        }
    }
}
