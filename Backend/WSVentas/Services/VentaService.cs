using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVentas.Models;
using WSVentas.Models.Request;

namespace WSVentas.Services
{
    public class VentaService : IVentaService
    {
        public void Add(VentaRequest model)
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
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw new Exception("Ocurrio un error en la inserción");
                    }
                }
            }
        }
    }
}
