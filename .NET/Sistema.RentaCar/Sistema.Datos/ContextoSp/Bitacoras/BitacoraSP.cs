using Sistema.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Datos.ContextoSp.Bitacoras
{
    public class BitacoraSP
    {
        DataAcces data = new DataAcces();
        public bool queryinsert(Bitacora data_)
        {
            var result = false;

            try
            {

                DataSet ds = new DataSet();

                SqlParameter[] param = new SqlParameter[]
                {
                    data.takeparam("@usuario",SqlDbType.VarChar,50,data_.accion),
                    data.takeparam("@fecha",SqlDbType.DateTime,0,data_.controlador),
                    data.takeparam("@accion",SqlDbType.VarChar,50,data_.usuario),
                    data.takeparam("@tipo",SqlDbType.VarChar,50,data_.descripcion),
                    data.takeparam("@controlador",SqlDbType.VarChar,50,data_.descripcion),
                    data.takeparam("@descripcion",SqlDbType.VarChar,10000,data_.descripcion)
                };

                ds = data.execData("[BITACORA].[Sp_Insertar_Bitacora]", param, 1);

                if (ds != null)
                {
                    result = true;

                }
                else
                    result = false;

                return result;

            }
            catch (Exception ex)
            {
                result = false;
                return result;
            }

        }
    }
}
