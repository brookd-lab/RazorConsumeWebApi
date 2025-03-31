using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Product_Tutorial.Models;
using Product_Tutorial.Services;
using RazorConsumeWebApi.Data;

namespace Product_Tutorial.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeService _service;

        public string errorMessage = "";
        public string successMessage = "";
        [BindProperty]

        public Employee Employee { get; set; } = new();

        public EditModel(IWebHostEnvironment environment,
            ApplicationDbContext context,
            IEmployeeService service)
        {
            _service = service;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            Employee? employee = await _service.GetEmployeeById(id);
            if (id == null)
            {
                Response.Redirect("/Employees/Index");
            }
            Employee.Name = employee.Name;
            Employee.Age = employee.Age;
            return Page();
        }

        public async Task OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Employees/Index");
            }
            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
            }
            Employee? employee = await _service.GetEmployeeById(id);
            if (employee == null)
            {
                Response.Redirect("/Employees/Index");
            }

            // Update all change in Database
            employee.Name = Employee.Name;
            employee.Age = Employee.Age;

            await _service.UpdateEmployee(employee);
            successMessage = "Employee Updated";

            Response.Redirect("/Employees/Index");
        }
    }
}
