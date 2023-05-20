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
            try
            {
                DateTime date = DateTime.Now;
                Console.WriteLine(date);
                var citas = _context.Citas.Where(c => c.Fecha.DayOfYear == date.DayOfYear).Include(c => c.Cliente);
                return View(citas.ToList());
            }
            catch
            {
                return RedirectToRoute(new { controller = "Home", action = "Error" });
            }

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
            var factura = _context.Facturas.Where(m => m.IdCliente == id).ToList();
            return Json(factura);


        }
        public JsonResult MostrarCitas(int id)
        {
            //var factura = (from m in _context.Facturas where m.NombreFactura.Contains(nom) select new { label = (m.NombreFactura), val = m.Id }).ToList();
            var citas = _context.Citas.Where(m => m.IdCliente == id).ToList();
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