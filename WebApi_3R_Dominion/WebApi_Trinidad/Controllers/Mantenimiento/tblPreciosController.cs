using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Datos;
using Negocio.Resultados;

namespace WebApi_3R_Dominion.Controllers.Mantenimiento
{
    [EnableCors("*", "*", "*")]
    public class tblPreciosController : ApiController
    {
        private Proyecto_3REntities1 db = new Proyecto_3REntities1();

        // GET: api/tblPrecios
        public IQueryable<tbl_Precios> Gettbl_Precios()
        {
            return db.tbl_Precios;
        }


        public object Gettbl_Precios(int opcion, string filtro)
        {
            Resultado res = new Resultado();
            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    res.ok = true;
                    res.data = (from a in db.tbl_Precios 
                                join b in db.tbl_GrupoTabla_Det on a.id_TipoOrdenTrabajo equals b.id_detalleTabla
                                select new
                                {
                                    a.id_Precio,
                                    a.id_TipoOrdenTrabajo,
                                    descripcion_tipoOT = b.descripcion_grupoTabla,
                                    a.precio,
                                    a.cubicaje,
                                    a.estado,
                                    descripcion_estado = a.estado == 0 ? "INACTIVO" : "ACTIVO",
                                    a.usuario_creacion
                                }).ToList();
                    res.totalpage = 0;
                    resul = res;
                }

                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int idPrecio = Convert.ToInt32(parametros[0].ToString());

                    tbl_Precios objReemplazar;
                    objReemplazar = db.tbl_Precios.Where(u => u.id_Precio == idPrecio).FirstOrDefault<tbl_Precios>();
                    objReemplazar.estado = 0;

                    db.Entry(objReemplazar).State = EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                        res.ok = true;
                        res.data = "OK";
                        res.totalpage = 0;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        res.ok = false;
                        res.data = ex.InnerException.Message;
                        res.totalpage = 0;
                    }
                    resul = res;

                }
                else
                {
                    res.ok = false;
                    res.data = "Opcion seleccionada invalida";
                    res.totalpage = 0;

                    resul = res;
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
                res.totalpage = 0;
                resul = res;
            }
            return resul;
        }

        public object Posttbl_Precios(tbl_Precios tbl_Precios)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Precios.fecha_creacion = DateTime.Now;
                db.tbl_Precios.Add(tbl_Precios);
                db.SaveChanges();

                res.ok = true;
                //res.data = tbl_Precios.id_Precio;
                res.data = (from a in db.tbl_Precios
                            join b in db.tbl_GrupoTabla_Det on a.id_TipoOrdenTrabajo equals b.id_detalleTabla
                            select new
                            {
                                a.id_Precio,
                                a.id_TipoOrdenTrabajo,
                                b.descripcion_grupoTabla,
                                a.precio,
                                a.cubicaje,
                                a.estado,
                                descripcion_estado = a.estado == 0 ? "INACTIVO" : "ACTIVO",
                                a.usuario_creacion
                            }).ToList();
                res.totalpage = 0;
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
                res.totalpage = 0;
            }
            return res;
        }

        public object Puttbl_Precios(int id, tbl_Precios tbl_Precios)
        {
            Resultado res = new Resultado();

            tbl_Precios objReemplazar;
            objReemplazar = db.tbl_Precios.Where(u => u.id_Precio == id).FirstOrDefault<tbl_Precios>();

            objReemplazar.id_TipoOrdenTrabajo = tbl_Precios.id_TipoOrdenTrabajo;
            objReemplazar.precio = tbl_Precios.precio;
            objReemplazar.cubicaje = tbl_Precios.cubicaje;
            objReemplazar.estado = tbl_Precios.estado;

            objReemplazar.usuario_edicion = tbl_Precios.usuario_creacion;
            objReemplazar.fecha_edicion = DateTime.Now;

            db.Entry(objReemplazar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                res.ok = true;
                res.data = "OK";
                res.totalpage = 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;
                res.totalpage = 0;
            }

            return res;
        }



        // DELETE: api/tblPrecios/5
        [ResponseType(typeof(tbl_Precios))]
        public IHttpActionResult Deletetbl_Precios(int id)
        {
            tbl_Precios tbl_Precios = db.tbl_Precios.Find(id);
            if (tbl_Precios == null)
            {
                return NotFound();
            }

            db.tbl_Precios.Remove(tbl_Precios);
            db.SaveChanges();

            return Ok(tbl_Precios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_PreciosExists(int id)
        {
            return db.tbl_Precios.Count(e => e.id_Precio == id) > 0;
        }
    }
}