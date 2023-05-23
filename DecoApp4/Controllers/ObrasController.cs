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
    public class ObrasController : Controller
    {
        private readonly DecoappContext _context;

        public ObrasController(DecoappContext context)
        {
            _context = context;
        }

        // GET: Obras
        public IActionResult Index()
        {
                var obras = _context.Obras.Include(o => o.Cliente).Include(o => o.Empresa).Include(o => o.Estado).Include(o => o.Factura);
                return View(obras.ToList());


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string dir, string cp, DateTime fechaInicial, DateTime fechaFinal, string cliente)
        {
            try
            {
                var obras = from m in _context.Obras select m;
                DateTime aux = new DateTime();
                obras = obras.Include(o => o.Cliente).Include(o => o.Factura).Include(o => o.Estado);
                Console.WriteLine(aux);
                if (fechaInicial != aux)
                {
                    obras = obras.Where(f => f.FechaInicio >= fechaInicial);
                }
                if (fechaFinal != aux)
                {
                    obras = obras.Where(f => f.FechaFinal <= fechaFinal);
                }
                if (cp != null)
                {
                    //var testContext = _context.Facturas.Include(f => f.EstadoNavigation).Include(f => f.cliente).Where(f => f.Estado == esta).Where(f=>f.NombreFactura == nom);
                    obras = obras.Where(f => f.Cp.Contains(cp));

                }
                if (cliente != null)
                {
                    obras = obras.Where(f => f.Cliente.Nombre.Contains(cliente));

                }
                if (dir != null)
                {
                    obras = obras.Where(f => f.Direccion.Contains(dir));

                }

                return View(obras);
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }



        }



        // GET: Obras/Details/5
        public IActionResult Details(int? id)
        {

                var obra = _context.Obras.Include(o => o.Cliente).Include(o => o.Empresa).Include(o => o.Estado).Include(o => o.Factura).FirstOrDefault(m => m.Id == id);
                if (obra == null)
                {
                    return NotFound();
                }

                return View(obra);
  

        }

        // GET: Obras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Obras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Direccion, int IdCliente, string Cp, DateTime FechaInicio, DateTime FechaFinal, int IdEstado, int IdFactura)
        {
            try
            {
                Obra obra = new Obra();
                DateTime aux = new DateTime();
                if (Direccion != null) { obra.Direccion = Direccion; }
                if (IdCliente != 0) { obra.IdCliente = IdCliente; }
                if (FechaInicio != aux) { obra.FechaInicio = FechaInicio; }
                if (FechaFinal != aux) { obra.FechaFinal = FechaFinal; }
                if (Cp != null) { obra.Cp = Cp; }
                obra.IdEstado = IdEstado;
                if (IdFactura != 0) { obra.IdFactura = IdFactura; } else { obra.IdFactura = null; }
                obra.IdEmpresa = 1;
                //factura.Empresa = empresa;
                //factura.Cliente = cliente;
                _context.Add(obra);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        // POST: Obras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, string Direccion, string Cp, DateTime FechaInicio, DateTime FechaFinal, int IdCliente)
        {
            try
            {
                var obra = _context.Obras.Include(e => e.Cliente).FirstOrDefault(item => item.Id == Id);
                DateTime aux = new DateTime();
                if (obra != null)
                {
                    //obra.Cliente = client;
                    if (Direccion != null) { obra.Direccion = Direccion; }
                    if (IdCliente != 0) { obra.IdCliente = IdCliente; }
                    if (FechaInicio != aux) { obra.FechaInicio = FechaInicio; }
                    obra.FechaFinal = FechaFinal;
                    if (Cp != null) { obra.Cp = Cp; }

                    _context.Update(obra);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }



        // POST: Obras/Delete/5
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Trabajadores.Any(e => e.ObraActiva == id))
                {
                    var trabajador = _context.Trabajadores.Where(f => f.ObraActiva == id).ToList();
                    for (var i = 0; i < trabajador.Count; i++)
                    {
                        trabajador[i].ObraActiva = null;
                        _context.Update(trabajador[i]);
                        _context.SaveChanges();
                    }
                }
                var obra = _context.Obras.Find(id);
                if (obra != null)
                {
                    _context.Obras.Remove(obra);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        private bool ObraExists(int id)
        {
          return (_context.Obras?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
