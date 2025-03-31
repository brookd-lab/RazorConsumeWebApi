using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Product_Tutorial.Models;
using Product_Tutorial.Services;
using RazorConsumeWebApi.Data;

namespace Product_Tutorial.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeService _service;

        //pagination

        public int pageIndex = 1;
        public int totalpage = 0;
        private readonly int pageSize = 3;

        //Search
        public string Search = "";
        //sorting
        public string column = "id";
        public string orderBy = "desc";

        public IndexModel(IEmployeeService service)
        {
            _service = service;
        }

        [BindProperty]
        public List<Employee> employees { get; set; }

        public async Task<IActionResult> OnGet(int? pageIndex, string? Search, string? column, string? orderBy)
        {
            employees = await _service.GetEmployees();

            if (Search == null)
                return Page();
            
            this.Search = Search;

            var Employees = employees.ToList().Where(p => p.Name.Contains(Search) || p.Age.ToString().Contains(Search));

            employees = Employees.ToList();

            return Page();
        }

    }
}
