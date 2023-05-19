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
    public class TrabajadoresController : Controller
    {
        private readonly DecoappContext _context;

        public TrabajadoresController(DecoappContext context)
        {
            _context = context;
        }

        // GET: Trabajadores
        public IActionResult Index()
        {
            var trabajadores = _context.Trabajadores.Include(t => t.Obra);
            return View(trabajadores.ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string nom, string dni, string tlf)
        {
            //var testContext = _context.Clientes.Include(f => f.EstadoNavigation).Include(f => f.cliente);
            var trabajadores = from m in _context.Trabajadores select m;
            trabajadores= trabajadores.Include(t=> t.Obra);
            if (nom != null)
            {
                trabajadores = trabajadores.Where(f => f.Nombre == nom);

            }
            if (dni != null)
            {
                trabajadores = trabajadores.Where(f => f.Dni == dni);

            }
            if (tlf != null)
            {
                trabajadores = trabajadores.Where(f => f.Telefono == tlf);

            }
            return View(trabajadores);


        }


        // GET: Trabajadores/Details/5
        public IActionResult Details(int? id)
        {
            var trabajadore =  _context.Trabajadores.Include(t => t.Obra).FirstOrDefault(m => m.Id == id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            return View(trabajadore);
        }

        // GET: Trabajadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trabajadores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Nombre, string Dni, string Telefono, string Direccion, string Email, int ObraActiva)
        {
            Trabajadore trabajador = new Trabajadore();
            if (Nombre != null) { trabajador.Nombre = Nombre; }
            if (Dni != null) { trabajador.Dni = Dni; }
            if (Telefono != null) { trabajador.Telefono = Telefono; }
            if (Direccion != null) { trabajador.Direccion = Direccion; }
            if (Email != null) { trabajador.Email = Email; }
            if (ObraActiva != 0) { trabajador.ObraActiva = ObraActiva; } else { trabajador.ObraActiva = null; }

            _context.Add(trabajador);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // POST: Trabajadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Dni,Telefono,Direccion,Email,ObraActiva")] Trabajadore trabajadore)
         {
             if (id != trabajadore.Id)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(trabajadore);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!TrabajadoreExists(trabajadore.Id))
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
             ViewData["ObraActiva"] = new SelectList(_context.Obras, "Id", "Id", trabajadore.ObraActiva);
             return View(trabajadore);
         }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, string Nombre, string Dni, string Telefono, string Direccion, string Email, int ObraActiva)
        {
            var trabajador = _context.Trabajadores.Include(e => e.Obra).FirstOrDefault(item => item.Id == Id);
            if (trabajador != null)
            {
                //Obra obra = _context.Obras.FirstOrDefault(item => item.Id == ObraActiva);
                //trabajador.Obra = obra;
                if (Nombre != null) { trabajador.Nombre = Nombre; }
                if (Dni != null) { trabajador.Dni = Dni; }
                if (Telefono != null) { trabajador.Telefono = Telefono; }
                if (Direccion != null) { trabajador.Direccion = Direccion; }
                if (Email != null) { trabajador.Email = Email; }
                if (ObraActiva != 0) { trabajador.ObraActiva = ObraActiva; } else { trabajador.ObraActiva = null; }
                

                _context.Update(trabajador);
                 _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
            }

        // POST: Trabajadores/Delete/5
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Trabajadores == null)
            {
                return Problem("Entity set 'DecoappContext.Trabajadores'  is null.");
            }
            var trabajadore =  _context.Trabajadores.Find(id);
            if (trabajadore != null)
            {
                _context.Trabajadores.Remove(trabajadore);
            }
            
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabajadoreExists(int id)
        {
          return (_context.Trabajadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
