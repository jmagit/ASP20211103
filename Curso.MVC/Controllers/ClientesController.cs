using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Curso.Domains.Entities;
using Curso.Infraestructure.UoW;

namespace Curso.MVC.Controllers {
    public class ClientesController : Controller {
        private readonly TiendaDbContext _context;

        public ClientesController(TiendaDbContext context) {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(int page = 0, int rows = 10) {
            var filas = await _context.Customers.CountAsync();
            ViewBag.totalPages = Math.Ceiling((decimal)filas / rows);
            ViewBag.page = page;
            return View(await _context.Customers.OrderBy(m => m.FirstName + m.LastName).Skip(page * rows).Take(rows).ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null) {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Clientes/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,CompanyName,SalesPerson,EmailAddress,Phone,PasswordHash,PasswordSalt,Rowguid,ModifiedDate")] Customer customer) {
            if (ModelState.IsValid) {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,CompanyName,SalesPerson,EmailAddress,Phone,PasswordHash,PasswordSalt,Rowguid,ModifiedDate")] Customer customer) {
            if (id != customer.CustomerId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!CustomerExists(customer.CustomerId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null) {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id) {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
