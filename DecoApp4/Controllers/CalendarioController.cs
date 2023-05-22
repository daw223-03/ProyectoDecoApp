using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DecoApp4.Controllers
{
    public class CalendarioController : Controller
    {
        // GET: Index Calendario
        public ActionResult Index()
        {
            DateTime mesnum = DateTime.Now;
            
            var me=mesnum.Month;
            ViewBag.mesId = me;
            var listaMes = new string[13] { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            ViewBag.mesNo = listaMes;
            return View();
        }
    }
}
