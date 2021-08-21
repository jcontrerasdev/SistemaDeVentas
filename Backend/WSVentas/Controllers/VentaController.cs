using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVentas.Models;
using WSVentas.Models.Request;
using WSVentas.Models.Response;

namespace WSVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add(VentaRequest model)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var venta = new Ventum();
                            venta.Total = model.Conceptos.Sum(d => d.Cantidad * d.PrecioUnitario);
                            venta.Fecha = DateTime.Now;
                            venta.IdCliente = model.IdCliente;
                            db.Venta.Add(venta);
                            db.SaveChanges();

                            foreach (var concepto in model.Conceptos)
                            {
                                var newConcepto = new Models.Concepto()
                                {
                                    Cantidad = concepto.Cantidad,
                                    IdProducto = concepto.IdProducto,
                                    Importe = concepto.Importe,
                                    PrecioUnitario = concepto.PrecioUnitario,
                                    IdVenta = venta.Id,
                                };
                                db.Conceptos.Add(newConcepto);
                            }
                            db.SaveChanges();
                            transaction.Commit();
                            respuesta.Exito = 1;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }catch(Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }
    }
}
