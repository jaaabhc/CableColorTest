using Banhcafe.Microservices.CableColor.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Banhcafe.Microservices.CableColor.Infraestructure.Model.conectionModel;

namespace Banhcafe.Microservices.CableColor.Api.Controllers
{
    public class CableColorController : Controller
    {

        [Route("consulta")]
        [HttpGet]
        public async Task<ActionResult<object>> consultaClientesCableColor()
        {
            var objTransaccion = new transaccion();
            objTransaccion.cajero = "6";
            objTransaccion.contrato = "676104";
            objTransaccion.id_estado = 1;
            objTransaccion.fecha = DateTime.Now;
            objTransaccion.hora = DateTime.Now.TimeOfDay;
            objTransaccion.id_moneda = 1;
            objTransaccion.id_movimiento = 1;
            objTransaccion.id_transaccion = 12038;
            objTransaccion.id_plataforma = 1;

            objTransaccion.sucursal = 1;

            Process procesos = new();
            procesos.inicializarConfiguracion();
            var a = procesos.consultaXCliente(objTransaccion,"01");

            return  a;

        }




        // GET: CableColorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CableColorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CableColorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CableColorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CableColorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CableColorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CableColorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CableColorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
