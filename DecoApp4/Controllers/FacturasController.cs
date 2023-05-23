using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DecoApp4.Models;
using System.Net;
using Rotativa.AspNetCore;

namespace DecoApp4.Controllers
{
    public class FacturasController : Controller
    {
        private readonly DecoappContext _context;

        public FacturasController(DecoappContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public IActionResult Index()
        {

                var facturas = _context.Facturas.Include(f => f.Cliente).Include(f => f.Empresa).Include(f => f.Estado);
                return View(facturas.ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string nom, int id, DateTime fechaInicial, DateTime fechaFinal, string nomCliente)
        {
            try
            {
                var testContext = _context.Facturas.Include(f => f.Estado).Include(f => f.Cliente);
                var facturas = from m in _context.Facturas select m;
                DateTime aux = new DateTime();
                Console.WriteLine(aux);
                facturas = facturas.Include(f => f.Estado).Include(f => f.Cliente);
                if (fechaInicial != aux)
                {
                    facturas = facturas.Where(f => f.Fecha >= fechaInicial);
                }
                if (fechaFinal != aux)
                {
                    facturas = facturas.Where(f => f.Fecha <= fechaFinal);
                }
                if (id != 0)
                {
                    facturas = facturas.Where(f => f.Id == id);

                }
                if (nomCliente != null)
                {
                    facturas = facturas.Where(f => f.Cliente.Nombre.Contains(nomCliente));

                }
                if (nom != null)
                {
                    facturas = facturas.Where(f => f.NombreFactura.Contains(nom));

                }

                return View(facturas);
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }



        }


        // GET: Facturas/Details/5
        public IActionResult Details(int? id)
        {
  
                var factura = _context.Facturas.Include(f => f.Cliente).Include(f => f.Empresa).Include(f => f.Estado).FirstOrDefault(m => m.Id == id);
                if (factura == null)
                {
                    return NotFound();
                }

                return View(factura);
   

        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facturas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string NombreFactura, int IdCliente, DateTime Fecha, int Iva, int IdEstado)
        {
            try
            {
                Factura factura = new Factura();
                DateTime aux = new DateTime();
                if (NombreFactura != null) { factura.NombreFactura = NombreFactura; }
                if (IdCliente != 0) { factura.IdCliente = IdCliente; }
                if (Fecha != aux) { factura.Fecha = Fecha; }
                if (IdEstado != 0) { factura.IdEstado = IdEstado; }
                factura.Iva = Iva;
                factura.IdEmpresa = 1;

                _context.Add(factura);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }catch 
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, string NombreFactura, int IdCliente, DateTime Fecha, int Iva, int Descuento, int estado)
        {
            try
            {
                var factura = _context.Facturas.Include(e => e.Cliente).Include(e => e.Estado).FirstOrDefault(item => item.Id == Id);
                if (factura != null)
                {
                    DateTime aux = new DateTime();
                    if (NombreFactura != null) { factura.NombreFactura = NombreFactura; }
                    if (IdCliente != 0) { factura.IdCliente = IdCliente; }
                    if (Fecha != aux) { factura.Fecha = Fecha; }
                    if (estado != 0) { factura.IdEstado = estado; }
                    if (Iva != 0) { factura.Iva = Iva; }
                    factura.Iva = Iva;
                    factura.IdEstado = estado;

                    _context.Update(factura);
                    _context.SaveChanges();
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        // POST: Facturas/Delete/5

        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
         var factura = _context.Facturas.Find(id);
                if (_context.Obras.Any(e => e.IdFactura == id))
                {
                    var obras = _context.Obras.First(f => f.IdFactura == id);
                    obras.IdFactura = null;
                    _context.Update(obras);
                    _context.SaveChanges();
                }

                if (_context.Tareas.Any(e => e.IdFactura == id))
                {
                    var tareas = _context.Tareas.Where(f => f.IdFactura == id).ToList();
                    for (var i = 0; i < tareas.Count; i++)
                    {
                        _context.Tareas.Remove(tareas[i]);
                        _context.SaveChanges();
                    }

                }
                if (factura != null)
                {
                    _context.Facturas.Remove(factura);
                    _context.SaveChanges();
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        private bool FacturaExists(int id)
        {
          return (_context.Facturas?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        
        public IActionResult DeleteTarea(int id, int id2)
        {
            try
            {
                var tarea = _context.Tareas.Find(id);

                if (tarea != null)
                {
                    _context.Tareas.Remove(tarea);
                    _context.SaveChanges();
                }

                return RedirectToAction("Details", new { id = id2 });
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        
        public IActionResult Tarea(int id, string[] descripcion, int[] cantidad, int[] precio, int[] descuento)
        {
            try
            {
                for (int i = 0; i < descripcion.Length; i++)
                {
                    //if(descripcion[i] == null) { }
                    //var idd= _context.Tareas.OrderBy(x => x.Id).LastOrDefault();
                    Tarea tarea = new Tarea();
                    tarea.Descripcion = descripcion[i].ToString();
                    if (cantidad[i] != null) { tarea.Cantidad = cantidad[i]; }
                    if (precio[i] != null) { tarea.Precio = precio[i]; }
                    if (descuento[i] != null) { tarea.Descuento = descuento[i]; }
                    tarea.IdFactura = id;
                    _context.Add(tarea);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }


        }


        public IActionResult ImprimirFactura(int id, int plantilla)
        {
            try
            {
                var factura = _context.Facturas.Include(f => f.Estado).Include(f => f.Cliente).Include(f => f.Empresa).Include(f => f.Tareas).Include(f => f.Obras).FirstOrDefault(f => f.Id == id);
                if (plantilla == 0)
                {
                    return new ViewAsPdf("Factura21", factura)
                    {
                        FileName = $"{factura.NombreFactura}.pdf",
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                        PageMargins = new Rotativa.AspNetCore.Options.Margins(5, 0, 4, 7),
                        PageSize = Rotativa.AspNetCore.Options.Size.A4
                    };

                }
                else if (plantilla == 1)
                {
                    return new ViewAsPdf("Factura10", factura)
                    {
                        FileName = $"{factura.NombreFactura}.pdf",
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                        PageMargins = new Rotativa.AspNetCore.Options.Margins(5, 0, 4, 7),
                        PageSize = Rotativa.AspNetCore.Options.Size.A4
                    };

                }
                else if (plantilla == 2)
                {
                    return new ViewAsPdf("Presupuesto21", factura)
                    {
                        FileName = $"{factura.NombreFactura}.pdf",
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                        PageMargins = new Rotativa.AspNetCore.Options.Margins(5, 0, 4, 7),
                        PageSize = Rotativa.AspNetCore.Options.Size.A4
                    };

                }
                else if (plantilla == 3)
                {
                    return new ViewAsPdf("Presupuesto10", factura)
                    {
                        FileName = $"{factura.NombreFactura}.pdf",
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                        PageMargins = new Rotativa.AspNetCore.Options.Margins(5, 0, 4, 7),
                        PageSize = Rotativa.AspNetCore.Options.Size.A4
                    };

                }
                else if (plantilla == 4)
                {
                    return new ViewAsPdf("Proreforma", factura)
                    {
                        FileName = $"{factura.NombreFactura}.pdf",
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                        PageMargins = new Rotativa.AspNetCore.Options.Margins(5, 0, 4, 7),
                        PageSize = Rotativa.AspNetCore.Options.Size.A4
                    };

                }
                else
                {
                    return new ViewAsPdf("Factura21", factura)
                    {
                        FileName = $"{factura.NombreFactura}.pdf",
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                        PageMargins = new Rotativa.AspNetCore.Options.Margins(5, 0, 4, 7),
                        PageSize = Rotativa.AspNetCore.Options.Size.A4
                    };

                }
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }


        }

        }
}
