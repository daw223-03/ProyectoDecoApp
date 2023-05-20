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
            var estados = from m in _context.Estados select m;
            return View(estados);
        }

        // GET: Estadoes/Details/5
        public IActionResult Details(int? id)
        {


            var estado =  _context.Estados.FirstOrDefault(m => m.Id == id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nombre")] Estado estado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(estado);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(estado);
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }


        // POST: Estadoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre")] Estado estado)
        {
            try
            {
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
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        // POST: Estadoes/Delete/5
        [HttpPost,]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var estado = _context.Estados.Find(id);
                if (estado != null)
                {
                    _context.Estados.Remove(estado);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        private bool EstadoExists(int id)
        {
          return (_context.Estados?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
