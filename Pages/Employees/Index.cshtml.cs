using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Product_Tutorial.Models;
using Product_Tutorial.Services;

namespace Product_Tutorial.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        //pagination

        public int pageIndex = 1;
        public int totalpage = 0;
        private readonly int pageSize = 3;

        //Search
        public string Search = "";
        //sorting
        public string column = "id";
        public string orderBy = "desc";

        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Employee> employees { get; set; } = new();

        public void OnGet(int? pageIndex, string? Search, string? column, string? orderBy)
        {
            IQueryable<Employee> query = context.employees; 
          
            if (Search != null)
            {
                this.Search = Search;
                query = query.Where(p => p.Name.Contains(Search) || p.Age.ToString().Contains(Search));
            }
            // products = context.productTables.OrderByDescending(p => p.Id).ToList();
            string[] validColumn = { "Id", "Name", "Brand", "Category", "Price", "CreateDate" };
            string[] validOrderBy = { "desc", "asc" };


            if (!validColumn.Contains(column))
            {
                column = "Id";
            }

            if (!validOrderBy.Contains(orderBy))
            {
                orderBy = "desc";
            }
            this.orderBy = orderBy;
            this.column = column;

            if (column == "Id")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Id);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Id);
                }
            }
            else
            if (column == "Name")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Name);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Name);
                }
            }
            else if (column == "Age")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Age);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Age);
                }
            }
           
            //   query = query.OrderByDescending(p => p.Id);
            if (pageIndex == null || pageIndex < 1)
            {
                pageIndex = 1;
            }
            this.pageIndex = (int)pageIndex;

            decimal count = query.Count();
            totalpage = (int)Math.Ceiling(count / pageSize);
            query = query.Skip((this.pageIndex - 1) * pageSize).Take(pageSize);
            employees=query.ToList();

        }

    }
}
