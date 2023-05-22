using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DecoApp4.Models;
using System.Net;
using System.Runtime.Intrinsics.X86;

namespace DecoApp4.Controllers
{
    public class CitasController : Controller
    {
        private readonly DecoappContext _context;

        public CitasController(DecoappContext context)
        {
            _context = context;
        }

        // GET: Citas
        public IActionResult Index()
        {
                var citas = _context.Citas.Include(c => c.Cliente);
                return View(citas.ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string nom, int id, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                var citas = from m in _context.Citas select m;
                DateTime aux = new DateTime();
                citas = citas.Include(c => c.Cliente);
                if (fechaInicial != aux)
                {
                    citas = citas.Where(f => f.Fecha >= fechaInicial);
                }
                if (fechaFinal != aux)
                {
                    citas = citas.Where(f => f.Fecha <= fechaFinal);
                }
                if (id != 0)
                {
                    citas = citas.Where(f => f.Id == id);
                }
                if (nom != null)
                {
                    citas = citas.Where(f => f.Cliente.Nombre.Contains(nom));
                }

                return View(citas);

            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }
        }



        // GET: Citas/Details/5
        public IActionResult Details(int? id)
        {

                var cita = _context.Citas.Include(c => c.Cliente).FirstOrDefault(m => m.Id == id);
                if (cita == null)
                {
                    return NotFound();
                }

                return View(cita);

        }



        

        // GET: Citas/Create
        public IActionResult Create()
        {
            return View();
        }




        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DateTime Fecha, TimeSpan Hora, string Comentario, int IdCliente)
        {
            try
            {
                Cita cita = new Cita();
                DateTime aux = new DateTime();
                if (Fecha != aux)
                {
                    cita.Fecha = Fecha;
                }
                if (Comentario != null)
                {
                    cita.Comentario = Comentario;
                }
                if (IdCliente != 0)
                {
                    cita.IdCliente = IdCliente;
                }
                cita.Hora = Hora;
                _context.Add(cita);
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
        public IActionResult Edit(int Id, DateTime Fecha, TimeSpan Hora, string Comentario, int IdCliente)
        {
            try
            {
                var cita = _context.Citas.Include(e => e.Cliente).FirstOrDefault(item => item.Id == Id);
                if (cita != null)
                {
                    DateTime aux = new DateTime();
                    if (Fecha != aux)
                    {
                        cita.Fecha = Fecha;
                    }
                    if (IdCliente != 0)
                    {
                        cita.IdCliente = IdCliente;

                    }
                    if (Comentario != null)
                    {
                        cita.Comentario = Comentario;
                    }
                    cita.Hora = Hora;



                    _context.Update(cita);
                    _context.SaveChanges();

                }
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }


        // POST: Citas/Delete/5
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var cita = _context.Citas.Find(id);
                if (cita != null)
                {
                    _context.Citas.Remove(cita);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        private bool CitaExists(int id)
        {
          return (_context.Citas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
