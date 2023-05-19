using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DecoApp4.Models;
using System.Net;

namespace DecoApp4.Controllers
{
    public class MaterialesController : Controller
    {
        private readonly DecoappContext _context;

        public MaterialesController(DecoappContext context)
        {
            _context = context;
        }

        // GET: Materiales
        public IActionResult Index()
        {
            var materiales = from m in _context.Materiales select m;
            return View(materiales);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string nom)
        {
            //var testContext = _context.Citas.Include(f => f.Cliente);
            var material = from m in _context.Materiales select m;
            if (nom != null)
            {
                material = material.Where(f => f.Nombre.Contains(nom));
            }

            return View(material);


        }



        // GET: Materiales/Details/5
        public IActionResult Details(int? id)
        {
            var materiale =  _context.Materiales.FirstOrDefault(m => m.Id == id);
            if (materiale == null)
            {
                return NotFound();
            }

            return View(materiale);
        }

        // GET: Materiales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materiales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(string Nombre, int Cantidad, int Precio)
        {
            Materiale material = new Materiale();
            if (Nombre != null) { material.Nombre = Nombre; }
            material.Cantidad = Cantidad;
            material.Precio = Precio;
            _context.Add(material);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // POST: Materiales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre,Cantidad,Precio")] Materiale materiale)
        {
            if (id != materiale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materiale);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialeExists(materiale.Id))
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
            return View(materiale);
        }

        // POST: Materiales/Delete/5
        public IActionResult DeleteConfirmed(int id)
        {
            var materiale =  _context.Materiales.Find(id);
            if (materiale != null)
            {
                _context.Materiales.Remove(materiale);
            }
            
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialeExists(int id)
        {
          return (_context.Materiales?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
