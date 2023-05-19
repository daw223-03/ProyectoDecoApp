using DecoApp4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DecoApp4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DecoappContext _context;
        public HomeController(ILogger<HomeController> logger, DecoappContext context)
        {
            _logger = logger;
            _context = context;
        }

        public ActionResult Index()
        {
            DateTime date = DateTime.Now;
            //date.ToShortDateString();
            //txtFecha.Text = date.ToShortDateString();
            Console.WriteLine(date);
            var citas = _context.Citas.Where(c=>c.Fecha.DayOfYear == date.DayOfYear).Include(c => c.Cliente);
            return View(citas.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //FUNCION PARA BUSCAR ESTADOS Y DELVOLVER POR JSON (USADO PARA SUGERIR OPCION EN INPUT)
        //PERMITE BUSQUEDA POR TEXTO DEL NOMBRE
        public JsonResult GetEstado(string? nom)
        {
            if (nom == null)
            {
                //En formato label y val de id;
                var estado = (from m in _context.Estados select new { label = m.Nombre, val = m.Id }).ToList();
                return Json(estado);
            }
            else
            {
                var estado = (from m in _context.Estados where m.Nombre.Contains(nom) select new { label = m.Nombre, val = m.Id }).ToList();
                return Json(estado);
            }

        }

        //FUNCION PARA BUSCAR CLIENTES Y DELVOLVER POR JSON (USADO PARA SUGERIR OPCION EN INPUT)
        public JsonResult GetClientes(string? nom)
        {
            if (nom == null)
            {
                var clientes = (from m in _context.Clientes select new { label = m.Nombre, val = m.Id }).ToList();
                return Json(clientes);
            }
            else
            {
                var clientes = (from m in _context.Clientes where m.Nombre.Contains(nom) select new { label = m.Nombre, val = m.Id }).ToList();
                return Json(clientes);
            }

        }

        //FUNCION PARA BUSCAR OBRAS Y DELVOLVER POR JSON (USADO PARA SUGERIR OPCION EN INPUT)
        public JsonResult GetObras(string? nom)
        {
            if (nom == null)
            {
                var obras = (from m in _context.Obras select new { label = m.Direccion, val = m.Id }).ToList();
                return Json(obras);
            }
            else
            {
                var obras = (from m in _context.Obras where m.Direccion.Contains(nom) select new { label = m.Direccion, val = m.Id }).ToList();
                return Json(obras);
            }

        }


        //FUNCION PARA BUSCAR FACTURAS Y DELVOLVER POR JSON (USADO PARA SUGERIR OPCION EN INPUT)
        public JsonResult GetFactura(string? nom)
        {
            if (nom == null)
            {
                var factura = (from m in _context.Facturas select new { label = m.NombreFactura, val = m.Id }).ToList();
                return Json(factura);
            }
            else
            {
                var factura = (from m in _context.Facturas where m.NombreFactura.Contains(nom) select new { label = ( m.NombreFactura), val = m.Id }).ToList();
                return Json(factura);
            }

        }

        [HttpPost]





        //FUNCION PARA BUSCAR OBRAS Y DELVOLVER POR JSON (USADO PARA SUGERIR OPCION EN INPUT)
        public JsonResult MostrarObras(int id)
        {
            var obras = (from m in _context.Obras where m.IdCliente==id select new { label = m.Direccion, val = m.Id }).ToList();
            var obra = _context.Obras.Where(m=>m.IdCliente==id).ToList();
            return Json(obra);

        }


        //FUNCION PARA BUSCAR FACTURAS Y DELVOLVER POR JSON (USADO PARA SUGERIR OPCION EN INPUT)
        public JsonResult MostrarFactura(int id)
        {
            //var factura = (from m in _context.Facturas where m.NombreFactura.Contains(nom) select new { label = (m.NombreFactura), val = m.Id }).ToList();
            var factura = _context.Facturas.Where(m => m.IdCliente == id).Include(m => m.Cliente).Include(m => m.Estado).ToList();
            return Json(factura);


        }
        public JsonResult MostrarCitas(int id)
        {
            //var factura = (from m in _context.Facturas where m.NombreFactura.Contains(nom) select new { label = (m.NombreFactura), val = m.Id }).ToList();
            var citas = _context.Citas.Where(m => m.IdCliente == id).Include(m => m.Cliente).ToList();
            return Json(citas);


        }
        public JsonResult MostrarTareas(int id)
        {
            //var factura = (from m in _context.Facturas where m.NombreFactura.Contains(nom) select new { label = (m.NombreFactura), val = m.Id }).ToList();
            //if (_context.Tareas.Any(e => e.IdFactura == id)) {
                var tarea = _context.Tareas.Where(m => m.IdFactura == id).ToList();
                return Json(tarea);
            //}
        }

        public JsonResult ObrasCalendario(int mes)
        {
            //var factura = (from m in _context.Facturas where m.NombreFactura.Contains(nom) select new { label = (m.NombreFactura), val = m.Id }).ToList();
            var obras = _context.Obras.Where(m => m.FechaInicio.Month == mes);
            return Json(obras);


        }
        public JsonResult CitasCalendario(int mes)
        {
            //var factura = (from m in _context.Facturas where m.NombreFactura.Contains(nom) select new { label = (m.NombreFactura), val = m.Id }).ToList();
            //if (_context.Tareas.Any(e => e.IdFactura == id)) {
            var citas = _context.Citas.Where(m => m.Fecha.Month == mes).ToList();
            return Json(citas);
            //}
        }


    }

}
/*
 
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string NombreFactura, int IdCliente, DateTime Fecha, int Iva, int Descuento, int Estado)
        {
            var factura = _context.Facturas.Include(e => e.cliente).Include(e => e.EstadoNavigation).FirstOrDefault(item => item.Id == id);
            var factura2 = _context.Facturas.clie;
            factura.cliente = new Cliente();
            factura.EstadoNavigation = new Estado();
            var client = from m in _context.Clientes select m;
            client = client.Where(f => f.Id == IdCliente);
            factura.cliente = client;
            factura.NombreFactura = NombreFactura;
            factura.IdCliente = IdCliente;
            factura.Fecha = Fecha;
            factura.Iva = Iva;
            factura.Descuento = Descuento;
            factura.Estado = Estado;

                    _context.Update(factura);
                    await _context.SaveChangesAsync();

            return View(factura);
        }*/