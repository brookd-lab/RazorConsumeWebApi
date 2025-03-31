using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Product_Tutorial.Models;
using RazorConsumeWebApi.Data;

namespace Product_Tutorial.Pages.Admin.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProductTable ProductTable { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producttable = await _context.productTables.FirstOrDefaultAsync(m => m.Id == id);
            if (producttable == null)
            {
                return NotFound();
            }
            else
            {
                ProductTable = producttable;
            }
            return Page();
        }
    }
}
