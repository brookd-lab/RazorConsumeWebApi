using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Product_Tutorial.Models;
using Product_Tutorial.Services;
using RazorConsumeWebApi.Data;

namespace Product_Tutorial.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly IEmployeeService _service;

        public DetailsModel(IEmployeeService service)
        {
            _service = service;
        }

        public Employee Employee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee? employee = await _service.GetEmployeeById(id);
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
    }
}
