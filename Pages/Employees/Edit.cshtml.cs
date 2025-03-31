using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Product_Tutorial.Models;
using Product_Tutorial.Services;

namespace Product_Tutorial.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        public string errorMessage = "";
        public string successMessage = "";
        [BindProperty]

        public Employee Employee { get; set; } = new();

        public EditModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            Employee? employee = context.employees.Find(id)!;
            if (id == null)
            {
                Response.Redirect("/Employees/Index");
                return;
            }
            Employee.Name = employee.Name;
            Employee.Age = employee.Age;
        }

        public void OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Employees/Index");
                return;
            }
            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;
            }
            Employee? employee = context.employees.Find(id)!;
            if (employee == null)
            {
                Response.Redirect("/Employees/Index");
                return;
            }

            // Update all change in Database
            employee.Name = Employee.Name;
            employee.Age = Employee.Age;

            context.SaveChanges();
            successMessage = "Employee Updated";

            Response.Redirect("/Employees/Index");
        }
    }
}
