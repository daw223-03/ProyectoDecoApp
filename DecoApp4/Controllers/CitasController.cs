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
            //var testContext = _context.Citas.Include(f => f.Cliente);
            var citas = from m in _context.Citas select m;
            DateTime aux = new DateTime();
            Console.WriteLine(aux);
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
                citas = citas.Where(f => f.Cliente.Nombre.Contains(nom) );
            }

            return View(citas);


        }



        // GET: Citas/Details/5
        public IActionResult Details(int? id)
        {
            var cita =  _context.Citas.Include(c => c.Cliente).FirstOrDefault(m => m.Id == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }



        

        // GET: Citas/Create
        public IActionResult Create()
        {
            //ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id");
            return View();
        }

        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DateTime Fecha, TimeSpan Hora, string Comentario, int IdCliente)
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

        // POST: Citas/Edit/5

        /*    [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Hora,Comentario,IdCliente")] Cita cita)
            {
                if (id != cita.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(cita);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CitaExists(cita.Id))
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
                ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", cita.IdCliente);
                return View(cita);
            }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, DateTime Fecha, TimeSpan Hora, string Comentario, int IdCliente)
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
                    //Cliente client = _context.Clientes.FirstOrDefault(item => item.Id == IdCliente);
                    //cita.Cliente = client;
                }
                if (Comentario != null)
                {
                    cita.Comentario = Comentario;
                }
                //factura.cliente.
                //factura.NombreFactura = NombreFactura;
                cita.Hora = Hora;

                

                _context.Update(cita);
                 _context.SaveChanges();

            }
            return RedirectToAction(nameof(Index));
        }


        // POST: Citas/Delete/5
        public IActionResult DeleteConfirmed(int id)
        {
            var cita =  _context.Citas.Find(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
            }
            
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
          return (_context.Citas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
