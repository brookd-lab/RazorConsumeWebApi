using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Tutorial.Models;
using Product_Tutorial.Services;
using RazorConsumeWebApi.Data;

namespace Product_Tutorial.Pages.Employees
{
    public class CreateModel : PageModel
    {  
        private readonly IEmployeeService _service;
        private string errormessage;

        [BindProperty]
        public Employee Employee { get; set; } = new();

        public CreateModel(IEmployeeService service)
        {
            _service = service;
            errormessage = "";
        }
        public void OnGet()
        {
        }
        public async Task OnPost()
        {
            if (!ModelState.IsValid)
            {
                errormessage = "Please provide all The Required fields";
                return;
            }

            // save on database

            Employee employee = new()
            {
                Name = Employee.Name,
                Age = Employee.Age,
            };
            
            await _service.CreateEmployee(employee);

            //clear the Form
            Employee.Name = "";
            Employee.Age = 0;
            
            ModelState.Clear();
            Response.Redirect("/Employees/Index");
        }

        // [BindProperty]
        //  public ProductTable ProductTable { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

    }
}
