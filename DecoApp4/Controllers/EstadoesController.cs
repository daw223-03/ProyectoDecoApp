using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DecoApp4.Models;

namespace DecoApp4.Controllers
{
    public class EstadoesController : Controller
    {
        private readonly DecoappContext _context;

        public EstadoesController(DecoappContext context)
        {
            _context = context;
        }

        // GET: Estadoes
        public IActionResult Index()
        {
              return _context.Estados != null ? 
                          View( _context.Estados.ToList()) :
                          Problem("Entity set 'DecoappContext.Estados'  is null.");
        }

        // GET: Estadoes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estado =  _context.Estados
                .FirstOrDefault(m => m.Id == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // GET: Estadoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nombre")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estado);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(estado);
        }

        // GET: Estadoes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estado =  _context.Estados.Find(id);
            if (estado == null)
            {
                return NotFound();
            }
            return View(estado);
        }

        // POST: Estadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre")] Estado estado)
        {
            if (id != estado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estado);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoExists(estado.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estado);
        }

        // GET: Estadoes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estado = _context.Estados
                .FirstOrDefault(m => m.Id == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // POST: Estadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Estados == null)
            {
                return Problem("Entity set 'DecoappContext.Estados'  is null.");
            }
            var estado = _context.Estados.Find(id);
            if (estado != null)
            {
                _context.Estados.Remove(estado);
            }
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoExists(int id)
        {
          return (_context.Estados?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
