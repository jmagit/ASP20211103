using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Curso.Domains.Entities;
using Curso.Infraestructure.UoW;
using Microsoft.AspNetCore.Authorization;

namespace Curso.MVC.Controllers {
    [Authorize]
    public class ProductosController : Controller {
        private readonly TiendaDbContext _context;

        public ProductosController(TiendaDbContext context) {
            _context = context;
        }

        public async Task<IActionResult> Catalogo(string categoria, string subcategoria) {
            var categorias = await _context.ProductCategories
                .Where(f => f.ParentProductCategoryId == null)
                .Include(f => f.SubCategories)
                .ToListAsync();
            ViewBag.categorias = categorias;
            if (categoria != null) {
                var c = categorias.FirstOrDefault(f => f.Name.ToLower() == categoria.ToLower());
                if (c != null) {
                    ViewBag.categoria = categoria.ToLower();
                    var subcategorias = c.SubCategories.ToList();
                    ViewBag.subcategorias = subcategorias;
                    if (subcategoria != null) {
                        var s = subcategorias.FirstOrDefault(f => f.Name.ToLower() == subcategoria.ToLower());
                        if (s != null) {
                            ViewBag.subcategoria = subcategoria.ToLower();
                            _context.Entry(s)
                                .Collection(f => f.Products)
                                .Load();
                            ViewBag.productos = s.Products;
                            ViewBag.producto = s.Products.Count;
                        }
                    }
                }

            }
            return View();
        }


        // GET: Productos
        public async Task<IActionResult> Index(int page = 0, int rows = 10) {
            var filas = await _context.Products.CountAsync();
            ViewBag.totalPages = Math.Ceiling((decimal)filas / rows);
            ViewBag.page = page;
            ViewBag.rows = rows;
            return View(await _context.Products.OrderBy(m => m.Name).Skip(page * rows).Take(rows).ToListAsync());
        }

        // GET: Productos/Details/5
        [Route("[controller]/{id}")]
        [Route("[controller]/{id}/[action]")]
        [Route("[controller]/[action]/{id}")]
        [Route("catalogo/{categoria}/{subcategoria}/{id}/{resto}", Name = "catalogoProducto")]
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null) {
                return NotFound();
            }

            return View(product);
        }
        // GET: Productos/Photo/5
        public async Task<IActionResult> Photo(int? id) {
            if (id == null) {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null) {
                return NotFound();
            }

            return this.File(product.ThumbNailPhoto, "image/gif");
        }

        // GET: Productos/Create
        public IActionResult Create() {
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories.Where(f => f.ParentProductCategoryId != null).OrderBy(f => f.Name), "ProductCategoryId", "Name");
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels.OrderBy(f => f.Name), "ProductModelId", "Name");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,Rowguid,ModifiedDate")] Product product) {
            if (ModelState.IsValid) {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories.Where(f => f.ParentProductCategoryId != null).OrderBy(f => f.Name), "ProductCategoryId", "Name", product.ProductCategoryId);
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels.OrderBy(f => f.Name), "ProductModelId", "Name", product.ProductModelId);
            return View(product);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null) {
                return NotFound();
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories.Where(f => f.ParentProductCategoryId != null).OrderBy(f => f.Name), "ProductCategoryId", "Name", product.ProductCategoryId);
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels.OrderBy(f => f.Name), "ProductModelId", "Name", product.ProductModelId);
            return View(product);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,Rowguid,ModifiedDate")] Product product) {
            if (id != product.ProductId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!ProductExists(product.ProductId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories.Where(f => f.ParentProductCategoryId != null).OrderBy(f => f.Name), "ProductCategoryId", "Name", product.ProductCategoryId);
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels.OrderBy(f => f.Name), "ProductModelId", "Name", product.ProductModelId);
            return View(product);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null) {
                return NotFound();
            }

            return View(product);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id) {
            return _context.Products.Any(e => e.ProductId == id);
        }

   }
}
