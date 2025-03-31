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

namespace Product_Tutorial.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private string errormessage;
        private string successmessage;
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public Employee Employee { get; set; } = new();

        public CreateModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;

            errormessage = "";
            successmessage = "";
        }
        public void OnGet()
        {
        }
        public void OnPost()
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
            context.employees.Add(employee);
            context.SaveChanges();
            Response.Redirect("/Employees/Index");

            //clear the Form
            Employee.Name = "";
            Employee.Age = 0;
            
            ModelState.Clear();
            successmessage = "Employee Added successfully ";
        }

        // [BindProperty]
        //  public ProductTable ProductTable { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

    }
}
