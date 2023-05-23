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
        public IActionResult Index(string nom, string tlf)
        {
            try
            {
                var trabajadores = from m in _context.Trabajadores select m;
                trabajadores = trabajadores.Include(t => t.Obra);
                if (nom != null)
                {
                    trabajadores = trabajadores.Where(f => f.Nombre == nom);

                }
                if (tlf != null)
                {
                    trabajadores = trabajadores.Where(f => f.Telefono == tlf);

                }
                return View(trabajadores);
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }



        }


        // GET: Trabajadores/Details/5
        public IActionResult Details(int? id)
        {

                var trabajadore = _context.Trabajadores.Include(t => t.Obra).FirstOrDefault(m => m.Id == id);
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
            try
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
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, string Nombre, string Dni, string Telefono, string Direccion, string Email, int ObraActiva)
        {
            try
            {
                var trabajador = _context.Trabajadores.Include(e => e.Obra).FirstOrDefault(item => item.Id == Id);
                if (trabajador != null)
                {
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
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

            }

        // POST: Trabajadores/Delete/5
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Trabajadores == null)
                {
                    return Problem("Entity set 'DecoappContext.Trabajadores'  is null.");
                }
                var trabajadore = _context.Trabajadores.Find(id);
                if (trabajadore != null)
                {
                    _context.Trabajadores.Remove(trabajadore);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        private bool TrabajadoreExists(int id)
        {
          return (_context.Trabajadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
