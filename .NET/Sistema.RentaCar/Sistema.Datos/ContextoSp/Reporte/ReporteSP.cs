using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Sistema.Entidades.Reportes;

namespace Sistema.Datos.ContextoSp.Reporte
{
    public  class ReporteSP
    {
        public List<ReportModel> queryListar(int x,DateTime fecha_ini, DateTime fecha_fin)
        {
            DataAcces data = new DataAcces();

            List<ReportModel> lista = new List<ReportModel>();

            try
            {

                DataSet ds = new DataSet();

                SqlParameter[] param = new SqlParameter[]
                {
                    data.takeparam("@fecha_ini",SqlDbType.Date,0,fecha_ini),
                    data.takeparam("@fecha_fin",SqlDbType.Date,0,fecha_fin),
                };

                ds = data.execData("[dbo].[sp_reporte_renta]", param, 1);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            ReportModel p = new ReportModel();

                            p.id_renta = int.Parse(dr[0].ToString());
                            p.Cliente = dr[1].ToString();
                            p.Empleado = dr[2].ToString();
                            p.fecha_ini = dr[3].ToString();
                            p.fecha_fin = dr[4].ToString();
                            p.fecha = dr[5].ToString();
                            p.Costo_dia = Convert.ToDouble(dr[6].ToString());
                            p.Cantidad_dia = Convert.ToInt32(dr[7].ToString());
                            p.Total =Convert.ToDouble(dr[8].ToString());
                            lista.Add(p);
                        }

                    }

                }
                return lista;

            }
            catch (Exception ex)
            {

                return lista;
            }

        }

    }
    
     
}
