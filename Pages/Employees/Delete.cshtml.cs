using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Product_Tutorial.Models;
using Product_Tutorial.Services;
using RazorConsumeWebApi.Data;

namespace Product_Tutorial.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly IEmployeeService _service;
        
        public DeleteModel(IEmployeeService service)
        {
            _service = service;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _service.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                Employee = employee;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee? employee = await _service.GetEmployeeById(id);

            if (employee != null)
            {
                Employee = employee;
                await _service.DeleteEmployeeById(id);
            }

            return RedirectToPage("/Employees/Index");
        }
    }
}
