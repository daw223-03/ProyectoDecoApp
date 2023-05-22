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
    public class EmpresasController : Controller
    {
        private readonly DecoappContext _context;

        public EmpresasController(DecoappContext context)
        {
            _context = context;
        }

        // GET: Empresas
        public  IActionResult Index()
        {
                var empresas = from m in _context.Empresas select m;
                return View(empresas);
        }

        // GET: Empresas/Details/5
        public IActionResult Details(int? id)
        {

            var empresa = _context.Empresas.FirstOrDefault(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nombre,Cif,Telefono,Direccion,Email,Poblacion,Ciudad,Cp")] Empresa empresa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(empresa);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(empresa);
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre,Cif,Telefono,Direccion,Email,Poblacion,Ciudad,Cp")] Empresa empresa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(empresa);
                        _context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmpresaExists(empresa.Id))
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
                return View(empresa);
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        // POST: Empresas/Delete/5
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var empresa = _context.Empresas.Find(id);
                if (empresa != null)
                {
                    _context.Empresas.Remove(empresa);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        private bool EmpresaExists(int id)
        {
          return (_context.Empresas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
