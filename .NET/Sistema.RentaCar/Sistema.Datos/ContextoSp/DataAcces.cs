using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Datos.ContextoSp
{
    public class DataAcces
    {

        public DataSet execData(string namequery, SqlParameter[] param, int x)
        {
            DataSet ds = new DataSet();

            try
            {

                using (SqlConnection con = new SqlConnection(cadenaconexion(x)))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.ConnectionString = cadenaconexion(x);
                        con.Open();
                    }


                    SqlCommand cmd = new SqlCommand(namequery, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    if (param != null)
                    {
                        foreach (SqlParameter dbparam in param)
                        {
                            da.SelectCommand.Parameters.Add(dbparam);
                        }
                    }

                    da.Fill(ds);
                    return ds;


                }

            }
            catch (Exception ex)
            {
                //bitacoradeerror
                return null;
            }
        }

        public SqlParameter takeparam(string nameparam, SqlDbType dbType, int size, object valor)
        {
            SqlParameter dbparam;

            if (size > 0)
            {
                dbparam = new SqlParameter(nameparam, dbType, size);
            }
            else dbparam = new SqlParameter(nameparam, dbType);

            dbparam.Value = valor;

            return dbparam;
        }


        private string cadenaconexion(int x)
        {
            string rutaconexion = "";

            switch (x)
            {
                case 1:
                    rutaconexion = "data source=JUEGAOK-PC00052\\SQLEXPRESS;initial catalog=RentaCar;user id=sa;password=123;persist security info=True";
                    break;
            }

            return rutaconexion;
        }
    }
}
