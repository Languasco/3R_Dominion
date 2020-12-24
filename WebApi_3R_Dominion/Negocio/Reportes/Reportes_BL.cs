using Entidades.Reportes;
using Negocio.Conexion;
using Negocio.Resultados;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
 
using System.Threading;
using Excel = OfficeOpenXml;
using Style = OfficeOpenXml.Style;


namespace Negocio.Reportes
{
    public class Reportes_BL
    {
        public DataTable get_ubicacionesPorPersonal( int idServicio, string fechaGps, int idTipoOT, int  idProveedor, int idUsuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Reporte_Ubicacion_Personal", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = fechaGps;
                        cmd.Parameters.Add("@Servicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@TipoOrden", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@Proveedor", SqlDbType.Int).Value = idProveedor;
                        cmd.Parameters.Add("@TipoRepor", SqlDbType.VarChar).Value = "W";

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        public DataTable get_eventosCelular(int idServicio, string fechaGps, int idTipoOT, int idProveedor, int idUsuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Reporte_Eventos_Personal", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = fechaGps;
                        cmd.Parameters.Add("@Servicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@TipoOrden", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@Proveedor", SqlDbType.Int).Value = idProveedor; 

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        //------
        // REPORTE DETALLE OT
        ///

        //public DataTable get_detalleOt(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int  idEstado, int idUsuario)
        //{
        //    //DataTable dt_detalle = new DataTable();
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
        //        {
        //            cn.Open();
        //            using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Reporte_Detallado", cn))
        //            {
        //                cmd.CommandTimeout = 0;
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
        //                cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
        //                cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;
        //                cmd.Parameters.Add("@FecIni", SqlDbType.VarChar).Value = fechaIni;
        //                cmd.Parameters.Add("@FecFin", SqlDbType.VarChar).Value = fechaFin;

        //                cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
        //                cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

        //                //using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //                //{
        //                //    da.Fill(dt_detalle);
        //                //}
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return dt_detalle;
        //}


        public object get_detalleOt(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario)
        {
            Resultado res = new Resultado();
            List<detalleOT_E> obj_List = new List<detalleOT_E>();
            //DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Reporte_Detallado", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;
                        cmd.Parameters.Add("@FecIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@FecFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                detalleOT_E Entidad = new detalleOT_E();

                                Entidad.checkeado = false;

                                Entidad.id_OT = Convert.ToInt32(dr["id_OT"]);
                                Entidad.descripcionEstado = dr["descripcionEstado"].ToString();
                                Entidad.tipoOT = dr["tipoOT"].ToString();

                                Entidad.nroSed = dr["nroSed"].ToString();
                                Entidad.nroSuministro = dr["nroSuministro"].ToString();


                                Entidad.nroObra = dr["nroObra"].ToString();
                                Entidad.direccion = dr["direccion"].ToString();
                                Entidad.distrito = dr["distrito"].ToString();

                                Entidad.FechaOrigen = dr["FechaOrigen"].ToString();
                                Entidad.FechaAsignacion = dr["FechaAsignacion"].ToString();
                                Entidad.FechaMovil = dr["FechaMovil"].ToString();


                                Entidad.latitud = dr["latitud"].ToString();
                                Entidad.longitud = dr["longitud"].ToString();

                                Entidad.empresaContratista = dr["empresaContratista"].ToString();
                                Entidad.jefeCuadrilla = dr["jefeCuadrilla"].ToString();

                                Entidad.fechaRegistro = dr["fechaRegistro"].ToString();

                                Entidad.generaVolumen = dr["generaVolumen"].ToString();
                                Entidad.volumenDesmonte = dr["volumenDesmonte"].ToString();
                                Entidad.volumenDesmonteRecoger = dr["volumenDesmonteRecoger"].ToString();

                                Entidad.fechaAprobacion = dr["fechaAprobacion"].ToString();
                                Entidad.fechaCancelacion = dr["fechaCancelacion"].ToString();

                                Entidad.totalVolumen = dr["totalVolumen"].ToString();
                                Entidad.totalDesmonte = dr["totalDesmonte"].ToString();
                                Entidad.totalDesmonteRecoger = dr["totalDesmonteRecoger"].ToString();
                                Entidad.diasVencimiento = dr["diasVencimiento"].ToString();


                                Entidad.id_tipoTrabajo = Convert.ToInt32(dr["id_tipoTrabajo"]);
                                Entidad.id_Distrito = dr["id_Distrito"].ToString();
                                Entidad.referencia = dr["referencia"].ToString();
                                Entidad.descripcion_OT = dr["descripcion_OT"].ToString();
                                Entidad.id_estado = Convert.ToInt32(dr["id_estado"]);
                                Entidad.descripcionServicio = dr["descripcionServicio"].ToString();
                                Entidad.color = dr["color"].ToString();
                                Entidad.viajeIndebido = dr["viajeIndebido"].ToString();
                                Entidad.tipoTrabajo_OTOrigen = dr["tipoTrabajo_OTOrigen"].ToString();

                                obj_List.Add(Entidad);
                            }

                            res.ok = true;
                            res.data = obj_List;
                            res.totalpage = 0;
                        }


                        //using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        //{
                        //    da.Fill(dt_detalle);

                        //    res.ok = true;
                        //    res.data = dt_detalle;
                        //    res.totalpage = 0;
                        //}


                    }
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
                res.totalpage = 0;
            }
            return res;
        }
               
        public object get_descargarDetalleOT(int idServicio, int idTipoOT, int idProveedor,string fechaIni, string fechaFin, int idEstado, int idUsuario)
        {
            Resultado res = new Resultado();
            List<detalleOT_E> obj_List = new List<detalleOT_E>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Reporte_Detallado", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;
                        cmd.Parameters.Add("@FecIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@FecFin", SqlDbType.VarChar).Value = fechaFin;

                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                detalleOT_E Entidad = new detalleOT_E();
                                                               
                                Entidad.id_OT = Convert.ToInt32(dr["id_OT"]);
                                Entidad.descripcionEstado = dr["descripcionEstado"].ToString();
                                Entidad.tipoOT = dr["tipoOT"].ToString();

                                Entidad.nroSed = dr["nroSed"].ToString();
                                Entidad.nroSuministro = dr["nroSuministro"].ToString();

                                Entidad.nroObra = dr["nroObra"].ToString();
                                Entidad.direccion = dr["direccion"].ToString();
                                Entidad.distrito = dr["distrito"].ToString();

                                Entidad.FechaOrigen = dr["FechaOrigen"].ToString();
                                Entidad.FechaAsignacion = dr["FechaAsignacion"].ToString();
                                Entidad.FechaMovil = dr["FechaMovil"].ToString();

                                Entidad.empresaContratista = dr["empresaContratista"].ToString();
                                Entidad.jefeCuadrilla = dr["jefeCuadrilla"].ToString();

                                Entidad.fechaRegistro = dr["fechaRegistro"].ToString();                                

                                Entidad.generaVolumen = dr["generaVolumen"].ToString();
                                Entidad.volumenDesmonte = dr["volumenDesmonte"].ToString();
                                Entidad.volumenDesmonteRecoger = dr["volumenDesmonteRecoger"].ToString();

                                Entidad.fechaAprobacion = dr["fechaAprobacion"].ToString();
                                Entidad.fechaCancelacion = dr["fechaCancelacion"].ToString();
 
                                Entidad.totalVolumen = dr["totalVolumen"].ToString();
                                Entidad.totalDesmonte = dr["totalDesmonte"].ToString();
                                Entidad.totalDesmonteRecoger = dr["totalDesmonteRecoger"].ToString(); 
                                Entidad.diasVencimiento = dr["diasVencimiento"].ToString();
                                Entidad.viajeIndebido = dr["viajeIndebido"].ToString();

                                obj_List.Add(Entidad);
                            }

                            if (obj_List.Count == 0)
                            {
                                res.ok = false;
                                res.data = "No hay informacion disponible";
                                res.totalpage = 0;
                            }
                            else
                            {
                                res.ok = true;
                                 res.data = GenerarArchivoExcel_detalle(obj_List, idUsuario);
                                res.totalpage = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
                res.totalpage = 0;
            }
            return res;
        }

        public string GenerarArchivoExcel_detalle(List<detalleOT_E> listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_detalleOT.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["Archivos"];

                FileExcel = rutaServer + "Excel/" + id_usuario + "_detalleOT.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("DetalleOT");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));


                    oWs.Cells[1, 1].Style.Font.Size = 24; //letra tamaño  2
                    oWs.Cells[1, 1].Value = "DETALLE DE ORDENES TRABAJO";
                    oWs.Cells[1, 1, 1, 23].Merge = true;  // combinar celdaS


                    oWs.Cells[3, 1].Value = "ESTADO";
                    oWs.Cells[3, 2].Value = "TIPO DE ORDEN";

                    oWs.Cells[3, 3].Value = "SED";
                    oWs.Cells[3, 4].Value = "NRO SUMINISTRO";

                    oWs.Cells[3, 5].Value = "NRO OBRA/ TD";
                    oWs.Cells[3, 6].Value = "DIRECCION";
                    oWs.Cells[3, 7].Value = "DISTRITO";

                    oWs.Cells[3, 8].Value = "VIAJE INDEBIDO ";

                    oWs.Cells[3, 9].Value = "FECHA ORIGEN ";
                    oWs.Cells[3, 10].Value = "FECHA ASIGNACION ";
                    oWs.Cells[3, 11].Value = "FECHA ENVIO MOVIL";

                    oWs.Cells[3, 12].Value = "EMPRESA CONTRATISTA";
                    oWs.Cells[3, 13].Value = "JEFE CUADRILLA";

                    oWs.Cells[3, 14].Value = "FECHA DE REGISTRO";

                    oWs.Cells[3, 15].Value = "GENERO UN VOLUMEN";
                    oWs.Cells[3, 16].Value = "VOLUMEN DE DESMONTE";
                    oWs.Cells[3, 17].Value = "VOLUMEN DE DESMONTE POR RECOGER";

                    oWs.Cells[3, 18].Value = "FECHA APROBACION";
                    oWs.Cells[3, 19].Value = "FECHA CANCELACION";

                    oWs.Cells[3, 20].Value = "TOTAL EN VOLUMEN ";
                    oWs.Cells[3, 21].Value = "TOTAL EN DESMONTE ";
                    oWs.Cells[3, 22].Value = "TOTAL EN DESMONTE POR RECOGER"; 
                    oWs.Cells[3, 23].Value = "DIAS DE VENCIMIENTO";


                    foreach (var item in listDetalle)
                    {
                        oWs.Cells[_fila, 1].Value = item.descripcionEstado.ToString();
                        oWs.Cells[_fila, 2].Value = item.tipoOT.ToString();

                        oWs.Cells[_fila, 3].Value = item.nroSed.ToString();
                        oWs.Cells[_fila, 4].Value = item.nroSuministro.ToString();

                        oWs.Cells[_fila, 5].Value = item.nroObra.ToString(); 
                        oWs.Cells[_fila, 6].Value = item.direccion.ToString();
                        oWs.Cells[_fila, 7].Value = item.distrito.ToString();

                        oWs.Cells[_fila, 8].Value = item.viajeIndebido.ToString();

                        oWs.Cells[_fila, 9].Value = item.FechaOrigen.ToString();
                        oWs.Cells[_fila, 10].Value = item.FechaAsignacion.ToString();
                        oWs.Cells[_fila, 11].Value = item.FechaMovil.ToString();

                        oWs.Cells[_fila, 12].Value = item.empresaContratista.ToString();
                        oWs.Cells[_fila, 13].Value = item.jefeCuadrilla.ToString();

                        oWs.Cells[_fila, 14].Value = item.fechaRegistro.ToString();

                        oWs.Cells[_fila, 15].Value = item.generaVolumen.ToString();
                        oWs.Cells[_fila, 16].Value = item.volumenDesmonte.ToString();
                        oWs.Cells[_fila, 17].Value = item.volumenDesmonteRecoger.ToString();

                        oWs.Cells[_fila, 18].Value = item.fechaAprobacion.ToString();
                        oWs.Cells[_fila, 19].Value = item.fechaCancelacion.ToString();

                        oWs.Cells[_fila, 20].Value = item.totalVolumen.ToString();
                        oWs.Cells[_fila, 20].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;

                        oWs.Cells[_fila, 21].Value = item.totalDesmonte.ToString();
                        oWs.Cells[_fila, 21].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;

                        oWs.Cells[_fila, 22].Value = item.totalDesmonteRecoger.ToString();
                        oWs.Cells[_fila, 22].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
 
                        oWs.Cells[_fila, 23].Value = item.diasVencimiento.ToString();

                        _fila++;
                    }


                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 23; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }
                
        public object get_fueraPlazoOT(int idServicio, int idTipoOT, int idProveedor, int idUsuario)
        {
            Resultado res = new Resultado();
            List<fueraPlazoOT_E> obj_List = new List<fueraPlazoOT_E>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Reporte_FueraPlazo", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                fueraPlazoOT_E Entidad = new fueraPlazoOT_E();
                                                               
                                Entidad.id_OT = Convert.ToInt32(dr["id_OT"]);
                                Entidad.descripcionEstado = dr["descripcionEstado"].ToString();
                                Entidad.tipoOT = dr["tipoOT"].ToString();
                                Entidad.nroObra = dr["nroObra"].ToString();
                                Entidad.direccion = dr["direccion"].ToString();
                                Entidad.distrito = dr["distrito"].ToString();

                                Entidad.latitud = dr["latitud"].ToString();
                                Entidad.longitud = dr["longitud"].ToString();

                                Entidad.FechaAsignacion = dr["FechaAsignacion"].ToString();
                                Entidad.FechaMovil = dr["FechaMovil"].ToString();

                                Entidad.empresaContratista = dr["empresaContratista"].ToString();
                                Entidad.jefeCuadrilla = dr["jefeCuadrilla"].ToString();

                                Entidad.fueraPlazoHoras = dr["fueraPlazoHoras"].ToString();
                                Entidad.fueraPlazoDias = dr["fueraPlazoDias"].ToString();

                                Entidad.id_tipoTrabajo = Convert.ToInt32(dr["id_tipoTrabajo"]);
                                Entidad.id_Distrito = dr["id_Distrito"].ToString();
                                Entidad.referencia = dr["referencia"].ToString();
                                Entidad.descripcion_OT = dr["descripcion_OT"].ToString();
                                Entidad.id_estado = Convert.ToInt32(dr["id_estado"]);

                                obj_List.Add(Entidad);
                            }

                            res.ok = true;
                            res.data = obj_List;
                            res.totalpage = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
                res.totalpage = 0;
            }
            return res;
        }
        
        public object get_descargarFueraPlazoOT(int idServicio, int idTipoOT, int idProveedor, int idUsuario)
        {
            Resultado res = new Resultado();
            List<fueraPlazoOT_E> obj_List = new List<fueraPlazoOT_E>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Reporte_FueraPlazo", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                fueraPlazoOT_E Entidad = new fueraPlazoOT_E();

                                Entidad.id_OT = Convert.ToInt32(dr["id_OT"]);
                                Entidad.descripcionEstado = dr["descripcionEstado"].ToString();
                                Entidad.tipoOT = dr["tipoOT"].ToString();
                                Entidad.nroObra = dr["nroObra"].ToString();
                                Entidad.direccion = dr["direccion"].ToString();
                                Entidad.distrito = dr["distrito"].ToString();

                                Entidad.FechaAsignacion = dr["FechaAsignacion"].ToString();
                                Entidad.FechaMovil = dr["FechaMovil"].ToString();
                                Entidad.empresaContratista = dr["empresaContratista"].ToString();
                                Entidad.jefeCuadrilla = dr["jefeCuadrilla"].ToString();

                                Entidad.fueraPlazoHoras = dr["fueraPlazoHoras"].ToString();
                                Entidad.fueraPlazoDias = dr["fueraPlazoDias"].ToString();

                                obj_List.Add(Entidad);
                            }

                            if (obj_List.Count == 0)
                            {
                                res.ok = false;
                                res.data = "No hay informacion disponible";
                                res.totalpage = 0;
                            }
                            else
                            {
                                res.ok = true;
                                res.data = GenerarArchivoExcel_fueraPlazoOT(obj_List, idUsuario);
                                res.totalpage = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
                res.totalpage = 0;
            }
            return res;
        }

        public string GenerarArchivoExcel_fueraPlazoOT(List<fueraPlazoOT_E> listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {
                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_fueraPlazoOT.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["Archivos"];

                FileExcel = rutaServer + "Excel/" + id_usuario + "_fueraPlazoOT.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("OTFueraPlazo");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));

                    oWs.Cells[1, 1].Style.Font.Size = 24; //letra tamaño  2
                    oWs.Cells[1, 1].Value = "ORDEN DE TRABAJO FUERA DE PLAZO";
                    oWs.Cells[1, 1, 1, 11].Merge = true;  // combinar celdaS


                    oWs.Cells[3, 1].Value = "ESTADO";
                    oWs.Cells[3, 2].Value = "TIPO DE ORDEN";
                    oWs.Cells[3, 3].Value = "NRO OBRA/ TD";
                    oWs.Cells[3, 4].Value = "DIRECCION";
                    oWs.Cells[3, 5].Value = "DISTRITO";

                    oWs.Cells[3, 6].Value = "FECHA ASIGNACION ";
                    oWs.Cells[3, 7].Value = "FECHA ENVIO MOVIL";

                    oWs.Cells[3, 8].Value = "EMPRESA CONTRATISTA";
                    oWs.Cells[3, 9].Value = "JEFE CUADRILLA";
                    oWs.Cells[3, 10].Value = "FUERA DE PLAZO EN HORAS";
                    oWs.Cells[3, 11].Value = "FUERA DE PLAZO EN DIAS";
 


                    foreach (var item in listDetalle)
                    {
                        oWs.Cells[_fila, 1].Value = item.descripcionEstado.ToString();
                        oWs.Cells[_fila, 2].Value = item.tipoOT.ToString();
                        oWs.Cells[_fila, 3].Value = item.nroObra.ToString();
                        oWs.Cells[_fila, 4].Value = item.direccion.ToString();
                        oWs.Cells[_fila, 5].Value = item.distrito.ToString();

                        oWs.Cells[_fila, 6].Value = item.FechaAsignacion.ToString();
                        oWs.Cells[_fila, 7].Value = item.FechaMovil.ToString();

                        oWs.Cells[_fila, 8].Value = item.empresaContratista.ToString();
                        oWs.Cells[_fila, 9].Value = item.jefeCuadrilla.ToString();

                        oWs.Cells[_fila, 10].Value = item.fueraPlazoHoras.ToString();
                        oWs.Cells[_fila, 11].Value = item.fueraPlazoDias.ToString();
                        _fila++;
                    }


                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 11; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }

        public DataTable get_ubicacionesOT(int idServicio, string fechaGps, int idTipoOT, int idProveedor, int idEstado, int idUsuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_UBICACION_OT", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = fechaGps;
                        cmd.Parameters.Add("@Servicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@TipoOrden", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@Proveedor", SqlDbType.Int).Value = idProveedor;
                        cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = idEstado;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }


        public void funcionGlobal_centrarNegrita_Fila(Excel.ExcelWorksheet oWs, int[] fil)
        {
            for (int i = 0; i < fil.Length; i++)
            {
                oWs.Row(fil[i]).Style.Font.Bold = true;
                oWs.Row(fil[i]).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                oWs.Row(fil[i]).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;
            }
        }


        public void funcionGlobal_bordes_fila_columna(Excel.ExcelWorksheet oWs, int[] fil, int[] col)
        {
            for (int x = 0; x < fil.Length; x++)
            {
                for (int y = 0; y < col.Length; y++)
                {
                    oWs.Cells[fil[x], col[y]].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                }
            }
        }


        public void funcionGlobal_ajustarAnchoAutomatica_columna(Excel.ExcelWorksheet oWs, int[] col)
        {
            for (int y = 0; y < col.Length; y++)
            {
                oWs.Column(col[y]).AutoFit();
            }
        }

        public object get_descargar_roturaVereda(int idServicio, int idTipoOT, int idProveedor, string fechaIni,string  fechaFin, int idEstado, int  idUsuario)
        {
            Resultado res = new Resultado();
            DataTable listaDetallado = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_ANALISIS_ROTURA_VEREDA", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;

                        cmd.Parameters.Add("@fechaIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(listaDetallado);
                        }

                    }
                }

                if (listaDetallado.Rows.Count <= 0)
                {
                    res.ok = false;
                    res.data = "0|No hay informacion disponible";
                }
                else
                {
                    res.ok = true;
                    res.data = generarArchivoExcel_roturaVereda(listaDetallado, idUsuario);
                }

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

        public string generarArchivoExcel_roturaVereda(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_roturaVereda.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_roturaVereda.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("roturaVereda");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));


                    oWs.Cells[1, 1].Style.Font.Size = 15; //letra tamaño  2
                    oWs.Cells[1, 1].Value = listDetalle.Rows[0]["tituloReporte"].ToString();
                    oWs.Cells[1, 1, 1, 13].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Cells[3, 1].Value = "TIPO DE TRABAJO";
                    oWs.Cells[3, 2].Value = "N° DE ORDEN";
                    oWs.Cells[3, 3].Value = "SUMINISTRO";
                    oWs.Cells[3, 4].Value = "SED";
                    oWs.Cells[3, 5].Value = "FECHA EJECUSION ELECTRICA";

                    oWs.Cells[3, 6].Value = " TECNICO RESPONSABLE";
                    oWs.Cells[3, 7].Value = "DIRECCION ";
                    oWs.Cells[3, 8].Value = "DISTRITO";
                    oWs.Cells[3, 9].Value = "ZONA";
                    oWs.Cells[3, 10].Value = "VEREDAS m2";

                    oWs.Cells[3, 11].Value = "PROVEEDOR ASIGNADO ";
                    oWs.Cells[3, 12].Value = "M3 - REPORTADO - DESMONTE";
                    oWs.Cells[3, 13].Value = "OBSERVACIONES  ";
 

                    int ac = 0;
                    foreach (DataRow item in listDetalle.Rows)
                    {
                        ac += 1;

                        oWs.Cells[_fila, 1].Value = item["tipoTrabajo"].ToString();
                        oWs.Cells[_fila, 2].Value = item["nroOrden"].ToString();
                        oWs.Cells[_fila, 3].Value = item["suministro"].ToString();

                        oWs.Cells[_fila, 4].Value = item["sed"].ToString();
                        oWs.Cells[_fila, 5].Value = item["fechaEjecucion"].ToString();
                        oWs.Cells[_fila, 6].Value = item["tecnicoResponsable"].ToString();
                        oWs.Cells[_fila, 7].Value = item["direccion"].ToString();

                        oWs.Cells[_fila, 8].Value = item["distrito"].ToString();
                        oWs.Cells[_fila, 9].Value = item["zona"].ToString();
                        oWs.Cells[_fila, 10].Value = item["veredas"].ToString();
                        oWs.Cells[_fila, 11].Value = item["proveedorAsignado"].ToString();
                        oWs.Cells[_fila, 12].Value = item["m3reportado"].ToString();
                        oWs.Cells[_fila, 13].Value = item["observaciones"].ToString();

                        _fila++;
                    }

                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 13; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }
        
        public object get_descargar_reparacionVereda(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario)
        {
            Resultado res = new Resultado();
            DataTable listaDetallado = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_ANALISIS_REPARACION_VEREDA", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;

                        cmd.Parameters.Add("@fechaIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(listaDetallado);
                        }

                    }
                }

                if (listaDetallado.Rows.Count <= 0)
                {
                    res.ok = false;
                    res.data = "0|No hay informacion disponible";
                }
                else
                {
                    res.ok = true;
                    res.data = generarArchivoExcel_reparacionVereda(listaDetallado, idUsuario);
                }

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

        public string generarArchivoExcel_reparacionVereda(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_reparacionVereda.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_reparacionVereda.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("reparacionVereda");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));


                    oWs.Cells[1, 1].Style.Font.Size = 15; //letra tamaño  2
                    oWs.Cells[1, 1].Value = listDetalle.Rows[0]["tituloReporte"].ToString();
                    oWs.Cells[1, 1, 1, 13].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Cells[3, 1].Value = "NRO ORDEN ";
                    oWs.Cells[3, 2].Value = "PROVEEDOR ";
                    oWs.Cells[3, 3].Value = "FECHA ASIGNACION PROVEEDOR)";
                    oWs.Cells[3, 4].Value = "ESTADO DE VEREDA";
                    oWs.Cells[3, 5].Value = "FECHA EJECUCION VEREDA";
                    oWs.Cells[3, 6].Value = "VEREDAS M2";

                    oWs.Cells[3, 7].Value = "ADOQUIN  ";
                    oWs.Cells[3, 8].Value = "GRASS";
                    oWs.Cells[3, 9].Value = "ASFALTO";
                    oWs.Cells[3, 10].Value = "MAYOLICA";
                    oWs.Cells[3, 11].Value = "PISTA CONCRETO";
                    oWs.Cells[3, 12].Value = "PISO ESPECIAL ";
                    oWs.Cells[3, 13].Value = "M3 - REPORTADO - DESMONTE";
 


                    int ac = 0;
                    foreach (DataRow item in listDetalle.Rows)
                    {

                        oWs.Cells[_fila, 1].Value = item["nroOrden"].ToString();
                        oWs.Cells[_fila, 2].Value = item["proveedor"].ToString();
                        oWs.Cells[_fila, 3].Value = item["fechaAsignacion"].ToString();
                        oWs.Cells[_fila, 4].Value = item["estadoVereda"].ToString();
                        oWs.Cells[_fila, 5].Value = item["fechaEjecucion"].ToString();
                        oWs.Cells[_fila, 6].Value = item["veredasm2"].ToString();
                        oWs.Cells[_fila, 7].Value = item["adoquin"].ToString();

                        oWs.Cells[_fila, 8].Value = item["grass"].ToString();
                        oWs.Cells[_fila, 9].Value = item["asfalto"].ToString();
                        oWs.Cells[_fila, 10].Value = item["mayolica"].ToString();
                        oWs.Cells[_fila, 11].Value = item["pistaConcreto"].ToString();
                        oWs.Cells[_fila, 12].Value = item["pisoEspecial"].ToString();
                        oWs.Cells[_fila, 13].Value = item["m3reportado"].ToString();

                        _fila++;
                    }


                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 13; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }
        
        public object get_descargar_reparacionDesmonte(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario)
        {
            Resultado res = new Resultado();
            DataTable listaDetallado = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_ANALISIS_DESMONTE", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;

                        cmd.Parameters.Add("@fechaIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(listaDetallado);
                        }

                    }
                }

                if (listaDetallado.Rows.Count <= 0)
                {
                    res.ok = false;
                    res.data = "0|No hay informacion disponible";
                }
                else
                {
                    res.ok = true;
                    res.data = generarArchivoExcel_reparacionDesmonte(listaDetallado, idUsuario);
                }

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

        public string generarArchivoExcel_reparacionDesmonte(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_reparacionDesmonte.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_reparacionDesmonte.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("reparacionDesmonte");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));
                    
                    oWs.Cells[1, 1].Style.Font.Size = 15; //letra tamaño  2
                    oWs.Cells[1, 1].Value = listDetalle.Rows[0]["tituloReporte"].ToString();
                    oWs.Cells[1, 1, 1, 6].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Cells[3, 1].Value = "NRO ORDEN";
                    oWs.Cells[3, 2].Value = "PROVEEDOR";
                    oWs.Cells[3, 3].Value = "F. ASIGNACION";
                    oWs.Cells[3, 4].Value = "M3 - ENCONTRADO PARTE ELEC.";
                    oWs.Cells[3, 5].Value = "M3 - ENCONTRADO REPARACION DE VEREDA";
                    oWs.Cells[3, 6].Value = "F.RECOJO";

                    int ac = 0;
                    foreach (DataRow item in listDetalle.Rows)
                    {
                        ac += 1;

                        oWs.Cells[_fila, 1].Value = item["nroOrden"].ToString();
                        oWs.Cells[_fila, 2].Value = item["proveedor"].ToString();
                        oWs.Cells[_fila, 3].Value = item["fechaAsignacion"].ToString();
                        oWs.Cells[_fila, 4].Value = item["m3EncontradoElectrico"].ToString();
                        oWs.Cells[_fila, 5].Value = item["m3EncontradoReparacionVereda"].ToString();
                        oWs.Cells[_fila, 6].Value = item["fechaRecojo"].ToString();

                        _fila++;
                    }


                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 6; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }
        
        public object get_descargar_detalleOT(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario)
        {
            Resultado res = new Resultado();
            DataTable listaDetallado = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_ANALISIS_DETALLADO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;

                        cmd.Parameters.Add("@fechaIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(listaDetallado);
                        }

                    }
                }

                if (listaDetallado.Rows.Count <= 0)
                {
                    res.ok = false;
                    res.data = "0|No hay informacion disponible";
                }
                else
                {
                    res.ok = true;
                    res.data = generarArchivoExcel_detalleOT(listaDetallado, idUsuario);
                }

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

        public string generarArchivoExcel_detalleOT(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_detalleOT.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_detalleOT.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("detalleOT");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));

                    oWs.Cells[1, 1].Style.Font.Size = 15; //letra tamaño  2
                    oWs.Cells[1, 1].Value = listDetalle.Rows[0]["tituloReporte"].ToString();
                    oWs.Cells[1, 1, 1, 34].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Cells[3, 1].Value = "SERVICIO";
                    oWs.Cells[3, 2].Value = "TIPO DE ORDEN";
                    oWs.Cells[3, 3].Value = "NRO ORDEN ";
                    oWs.Cells[3, 4].Value = "SUMINISTRO";
                    oWs.Cells[3, 5].Value = "NRO SED";

                    oWs.Cells[3, 6].Value = "DIRECCION";
                    oWs.Cells[3, 7].Value = "DISTRITO";
                    oWs.Cells[3, 8].Value = "PROVEEDOR ";
                    oWs.Cells[3, 9].Value = "FECHA ASIGNACION PROVEEDOR";
                    oWs.Cells[3, 10].Value = "JEFE DE CUADRILLA";

                    oWs.Cells[3, 11].Value = "TIPO DE TRABAJO";
                    oWs.Cells[3, 12].Value = "TIPO DE MATERIAL";
                    oWs.Cells[3, 13].Value = "LARGO";
                    oWs.Cells[3, 14].Value = "ANCHO";
                    oWs.Cells[3, 15].Value = "ESPESOR";

                    oWs.Cells[3, 16].Value = "FACTOR MULTIPLO";
                    oWs.Cells[3, 17].Value = "TOTAL";
                    oWs.Cells[3, 18].Value = "PRECIO";
                    oWs.Cells[3, 19].Value = "TOTAL IMPORTE";


                    oWs.Cells[3, 20].Value = "CANT PAÑOS";
                    oWs.Cells[3, 21].Value = "MEDIDA HORIZONTAL";
                    oWs.Cells[3, 22].Value = "MEDIDA VERTICAL";

                    oWs.Cells[3, 23].Value = "FECHA EJECUCION VEREDA";
                    oWs.Cells[3, 24].Value = "ESTADO";
                    oWs.Cells[3, 25].Value = "ORDEN DE ORIGEN";

                    oWs.Cells[3, 26].Value = "EMPRESA ORIGEN";
                    oWs.Cells[3, 27].Value = "TIPO MATERIAL ORIGEN";
                    oWs.Cells[3, 28].Value = "LARGO ORIGEN";
                    oWs.Cells[3, 29].Value = "ANCHO ORIGEN";
                    oWs.Cells[3, 30].Value = "ESPESOR ORIGEN";
                    oWs.Cells[3, 31].Value = "TOTAL ORIGEN";

                    oWs.Cells[3, 32].Value = "CANT PAÑOS ORIGEN ";
                    oWs.Cells[3, 33].Value = "MEDIDA HORIZONTAL ORIGEN";
                    oWs.Cells[3, 34].Value = "MEDIDA VERTICAL ORIGEN";


                    int ac = 0;
                    foreach (DataRow item in listDetalle.Rows)
                    {
                        ac += 1;


                        oWs.Cells[_fila, 1].Value = item["Servicio"].ToString();
                        oWs.Cells[_fila, 2].Value = item["TipoOrden"].ToString();
                        oWs.Cells[_fila, 3].Value = item["NroOrden"].ToString();
                        oWs.Cells[_fila, 4].Value = item["Suminstro"].ToString();
                        oWs.Cells[_fila, 5].Value = item["NroSed"].ToString();

                        oWs.Cells[_fila, 6].Value = item["Direccion"].ToString();
                        oWs.Cells[_fila, 7].Value = item["Distrito"].ToString();
                        oWs.Cells[_fila, 8].Value = item["Proovedor"].ToString();
                        oWs.Cells[_fila, 9].Value = item["FechaAsignacionProovedor"].ToString();
                        oWs.Cells[_fila, 10].Value = item["JefeCuadrilla"].ToString();

                        oWs.Cells[_fila, 11].Value = item["TipoTrabajo"].ToString();
                        oWs.Cells[_fila, 12].Value = item["TipoMaterial"].ToString();
                        oWs.Cells[_fila, 13].Value = item["Largo"].ToString();
                        oWs.Cells[_fila, 13].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 14].Value = item["Ancho"].ToString();
                        oWs.Cells[_fila, 14].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 15].Value = item["Espesor"].ToString();
                        oWs.Cells[_fila, 15].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 16].Value = item["FactorMultiplo"].ToString();
                        oWs.Cells[_fila, 16].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 17].Value = item["Total"].ToString();
                        oWs.Cells[_fila, 17].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 18].Value = item["Precio"].ToString();
                        oWs.Cells[_fila, 18].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 19].Value = item["TotalImporte"].ToString();
                        oWs.Cells[_fila, 19].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 20].Value = item["CantPanios"].ToString();
                        oWs.Cells[_fila, 21].Value = item["MedidaHorizontal"].ToString();
                        oWs.Cells[_fila, 22].Value = item["MedidaVertical"].ToString();


                        oWs.Cells[_fila, 23].Value = item["FechaEjecucionVereda"].ToString();
                        oWs.Cells[_fila, 24].Value = item["Estado"].ToString();
                        oWs.Cells[_fila, 25].Value = item["OrdenOrigen"].ToString();

                        oWs.Cells[_fila, 26].Value = item["EmpresaOrigen"].ToString();
                        oWs.Cells[_fila, 27].Value = item["TipoMaterialOrigen"].ToString();

                        oWs.Cells[_fila, 28].Value = item["LargoOrigen"].ToString();
                        oWs.Cells[_fila, 28].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 29].Value = item["AnchoOrigen"].ToString();
                        oWs.Cells[_fila, 29].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 30].Value = item["EspesorOrigen"].ToString();
                        oWs.Cells[_fila, 30].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 31].Value = item["TotalOrigen"].ToString();
                        oWs.Cells[_fila, 31].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 32].Value = item["CantPaniosOrigen"].ToString();
                        oWs.Cells[_fila, 33].Value = item["MedidaHorizontalOrigen"].ToString();
                        oWs.Cells[_fila, 34].Value = item["MedidaVerticalOrigen"].ToString();

                        _fila++;
                    }


                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 34; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }
        
        public object get_descargar_detalleContratista(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario)
        {
            Resultado res = new Resultado();
            DataTable listaDetallado = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_ANALISIS_DETALLADO_PROVEEDOR", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;

                        cmd.Parameters.Add("@fechaIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(listaDetallado);
                        }

                    }
                }

                if (listaDetallado.Rows.Count <= 0)
                {
                    res.ok = false;
                    res.data = "0|No hay informacion disponible";
                }
                else
                {
                    res.ok = true;
                    res.data = generarArchivoExcel_detalleContratista(listaDetallado, idUsuario);
                }

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

        public string generarArchivoExcel_detalleContratista(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_detalleContratista.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_detalleContratista.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("detalleContratista");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));

                    oWs.Cells[1, 1].Style.Font.Size = 15; //letra tamaño  2
                    oWs.Cells[1, 1].Value = listDetalle.Rows[0]["tituloReporte"].ToString();
                    oWs.Cells[1, 1, 1, 34].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Cells[3, 1].Value = "SERVICIO";
                    oWs.Cells[3, 2].Value = "TIPO DE ORDEN";
                    oWs.Cells[3, 3].Value = "NRO ORDEN ";
                    oWs.Cells[3, 4].Value = "SUMINISTRO";
                    oWs.Cells[3, 5].Value = "NRO SED";

                    oWs.Cells[3, 6].Value = "DIRECCION";
                    oWs.Cells[3, 7].Value = "DISTRITO";
                    oWs.Cells[3, 8].Value = "PROVEEDOR ";
                    oWs.Cells[3, 9].Value = "FECHA ASIGNACION PROVEEDOR";
                    oWs.Cells[3, 10].Value = "JEFE DE CUADRILLA";

                    oWs.Cells[3, 11].Value = "TIPO DE TRABAJO";
                    oWs.Cells[3, 12].Value = "TIPO DE MATERIAL";
                    oWs.Cells[3, 13].Value = "LARGO";
                    oWs.Cells[3, 14].Value = "ANCHO";
                    oWs.Cells[3, 15].Value = "ESPESOR";

                    oWs.Cells[3, 16].Value = "FACTOR MULTIPLO";
                    oWs.Cells[3, 17].Value = "TOTAL";
                    oWs.Cells[3, 18].Value = "PRECIO";
                    oWs.Cells[3, 19].Value = "TOTAL IMPORTE";


                    oWs.Cells[3, 20].Value = "CANT PAÑOS";
                    oWs.Cells[3, 21].Value = "MEDIDA HORIZONTAL";
                    oWs.Cells[3, 22].Value = "MEDIDA VERTICAL";

                    oWs.Cells[3, 23].Value = "FECHA EJECUCION VEREDA";
                    oWs.Cells[3, 24].Value = "ESTADO";
                    oWs.Cells[3, 25].Value = "ORDEN DE ORIGEN";

                    oWs.Cells[3, 26].Value = "EMPRESA ORIGEN";
                    oWs.Cells[3, 27].Value = "TIPO MATERIAL ORIGEN";
                    oWs.Cells[3, 28].Value = "LARGO ORIGEN";
                    oWs.Cells[3, 29].Value = "ANCHO ORIGEN";
                    oWs.Cells[3, 30].Value = "ESPESOR ORIGEN";
                    oWs.Cells[3, 31].Value = "TOTAL ORIGEN";

                    oWs.Cells[3, 32].Value = "CANT PAÑOS ORIGEN ";
                    oWs.Cells[3, 33].Value = "MEDIDA HORIZONTAL ORIGEN";
                    oWs.Cells[3, 34].Value = "MEDIDA VERTICAL ORIGEN";


                    int ac = 0;
                    foreach (DataRow item in listDetalle.Rows)
                    {
                        ac += 1;

                        oWs.Cells[_fila, 1].Value = item["Servicio"].ToString();
                        oWs.Cells[_fila, 2].Value = item["TipoOrden"].ToString();
                        oWs.Cells[_fila, 3].Value = item["NroOrden"].ToString();
                        oWs.Cells[_fila, 4].Value = item["Suminstro"].ToString();
                        oWs.Cells[_fila, 5].Value = item["NroSed"].ToString();

                        oWs.Cells[_fila, 6].Value = item["Direccion"].ToString();
                        oWs.Cells[_fila, 7].Value = item["Distrito"].ToString();
                        oWs.Cells[_fila, 8].Value = item["Proovedor"].ToString();
                        oWs.Cells[_fila, 9].Value = item["FechaAsignacionProovedor"].ToString();
                        oWs.Cells[_fila, 10].Value = item["JefeCuadrilla"].ToString();

                        oWs.Cells[_fila, 11].Value = item["TipoTrabajo"].ToString();
                        oWs.Cells[_fila, 12].Value = item["TipoMaterial"].ToString();
                        oWs.Cells[_fila, 13].Value = item["Largo"].ToString();
                        oWs.Cells[_fila, 13].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 14].Value = item["Ancho"].ToString();
                        oWs.Cells[_fila, 14].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 15].Value = item["Espesor"].ToString();
                        oWs.Cells[_fila, 15].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 16].Value = item["FactorMultiplo"].ToString();
                        oWs.Cells[_fila, 16].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 17].Value = item["Total"].ToString();
                        oWs.Cells[_fila, 17].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 18].Value = item["Precio"].ToString();
                        oWs.Cells[_fila, 18].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 19].Value = item["TotalImporte"].ToString();
                        oWs.Cells[_fila, 19].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 20].Value = item["CantPanios"].ToString();
                        oWs.Cells[_fila, 21].Value = item["MedidaHorizontal"].ToString();
                        oWs.Cells[_fila, 22].Value = item["MedidaVertical"].ToString();


                        oWs.Cells[_fila, 23].Value = item["FechaEjecucionVereda"].ToString();
                        oWs.Cells[_fila, 24].Value = item["Estado"].ToString();
                        oWs.Cells[_fila, 25].Value = item["OrdenOrigen"].ToString();

                        oWs.Cells[_fila, 26].Value = item["EmpresaOrigen"].ToString();
                        oWs.Cells[_fila, 27].Value = item["TipoMaterialOrigen"].ToString();

                        oWs.Cells[_fila, 28].Value = item["LargoOrigen"].ToString();
                        oWs.Cells[_fila, 28].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 29].Value = item["AnchoOrigen"].ToString();
                        oWs.Cells[_fila, 29].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 30].Value = item["EspesorOrigen"].ToString();
                        oWs.Cells[_fila, 30].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 31].Value = item["TotalOrigen"].ToString();
                        oWs.Cells[_fila, 31].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 32].Value = item["CantPaniosOrigen"].ToString();
                        oWs.Cells[_fila, 33].Value = item["MedidaHorizontalOrigen"].ToString();
                        oWs.Cells[_fila, 34].Value = item["MedidaVerticalOrigen"].ToString();


                        _fila++;
                    }
                    
                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 34; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }




    }
}
