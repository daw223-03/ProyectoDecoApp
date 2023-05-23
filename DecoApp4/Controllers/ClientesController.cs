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
    public class ClientesController : Controller
    {
        private readonly DecoappContext _context;

        public ClientesController(DecoappContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public IActionResult Index()
        {
                var clientes = from m in _context.Clientes select m;
                return View(clientes);

        }


        //FUNCION QUE REALIZA LA BUSQUEDA EN LA PAGINA INDEX DE CLIENTES
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string nom, string tlf)
        {
            try
            {
                var clientes = from m in _context.Clientes select m;
                if (nom != null)
                {
                    clientes = clientes.Where(f => f.Nombre.Contains(nom));

                }
                if (tlf != null)
                {
                    clientes = clientes.Where(f => f.Telefono == tlf);

                }
                return View(clientes);
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        // Funcion para ver detalles
        public IActionResult Details(int? id)
        {
                var cliente = _context.Clientes.FirstOrDefault(m => m.Id == id);
                if (cliente == null)
                {
                    return NotFound();
                }

                return View(cliente);


        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Nombre, string Dni, string Telefono, string Direccion, string Email, string Poblacion, string Ciudad, string Cp)
        {
            try
            {
                Cliente cliente = new Cliente();
                if (Nombre != null) { cliente.Nombre = Nombre; }
                if (Dni != null) { cliente.Dni = Dni; }
                if (Telefono != null) { cliente.Telefono = Telefono; }
                if (Direccion != null) { cliente.Direccion = Direccion; }
                if (Email != null) { cliente.Email = Email; }
                if (Poblacion != null) { cliente.Poblacion = Poblacion; }
                if (Ciudad != null) { cliente.Ciudad = Ciudad; }
                if (Cp != null) { cliente.Cp = Cp; }
                _context.Add(cliente);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre,Dni,Telefono,Direccion,Email,Poblacion,Ciudad,Cp")] Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(cliente);
                        _context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClienteExists(cliente.Id))
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
                return View(cliente);
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }


        // POST: Clientes/Delete/5
        public IActionResult DeleteConfirmed(int id)
        {

            try
            {
                var cliente = _context.Clientes.Find(id);

                if (_context.Obras.Any(e => e.IdCliente == id))
                {
                    var obras = _context.Obras.Where(f => f.IdCliente == id).ToList();
                    for (var i = 0; i < obras.Count; i++)
                    {
                        obras[i].IdCliente = null;
                        _context.Update(obras[i]);
                        _context.SaveChanges();
                    }
                }
                if (_context.Facturas.Any(e => e.IdCliente == id))
                {
                    var factura = _context.Facturas.Where(f => f.IdCliente == id).ToList();
                    for(var i=0;i<factura.Count;i++)
                    {
                        factura[i].IdCliente = null;
                        _context.Update(factura[i]);
                        _context.SaveChanges();
                    }
                }
                if (_context.Citas.Any(e => e.IdCliente == id))
                {
                    var citas = _context.Citas.Where(f => f.IdCliente == id).ToList();
                    for (var i = 0; i < citas.Count; i++)
                    {
                        citas[i].IdCliente = null;
                        _context.Update(citas[i]);
                        _context.SaveChanges();
                    }
                }
                if (cliente != null)
                {
                    _context.Clientes.Remove(cliente);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
