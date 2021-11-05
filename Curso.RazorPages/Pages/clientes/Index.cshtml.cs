using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Curso.Domains.Entities;
using Curso.Infraestructure.UoW;

namespace Curso.RazorPages.Pages.clientes
{
    public class IndexModel : PageModel
    {
        private readonly Curso.Infraestructure.UoW.TiendaContext _context;

        public IndexModel(Curso.Infraestructure.UoW.TiendaContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; }

        public async Task OnGetAsync()
        {
            Customer = await _context.Customers.ToListAsync();
        }
    }
}
