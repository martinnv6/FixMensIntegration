using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using FixmensCMD.Models.dto;


namespace FixmensCMD.BLL
{
    public class OrdenBLL
    {
        public List<long> GetStatusChanged()
        {
            var result = new List<long>();



            FbConnection conn = new FbConnection();
            FbCommand cmd = new FbCommand();
            FbDataAdapter da = new FbDataAdapter();
            DataTable dt = new DataTable();
            string firebirdServer = ConfigurationManager.AppSettings["firebird.server"];

            conn.ConnectionString = "User=SYSDBA;Password=masterkey;Database=" + firebirdServer +
                                    ";Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;" +
                                    "MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;";
            cmd.Connection = conn;

            string stardate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");




            cmd.CommandText = "SELECT rh.IDREPARACION from REPARACIONES_HISTORICO_ESTADO rh " +
                                "WHERE RH.INTEGRADO IS DISTINCT FROM 'S' " +
                                "group by rh.IDREPARACION";
            cmd.CommandType = CommandType.Text;


            var a =conn.State;
            conn.Open();
            da.SelectCommand = cmd;
            da.Fill(dt);
            conn.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    result.Add(int.Parse(row["IDREPARACION"].ToString()));
                }


            }



            return result;

        }


        public List<REPARACIONESVIEW> GetReparaciones()
        {

            var result = new List<REPARACIONESVIEW>();



            FbConnection conn = new FbConnection();
            FbCommand cmd = new FbCommand();
            FbDataAdapter da = new FbDataAdapter();
            DataTable dt = new DataTable();
            string firebirdServer = ConfigurationManager.AppSettings["firebird.server"];

            conn.ConnectionString = "User=SYSDBA;Password=masterkey;Database=" + firebirdServer +
                                    ";Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;" +
                                    "MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;";
            cmd.Connection = conn;

            string stardate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");




            cmd.CommandText = "SELECT R.CODIGO, a.MARCA || ' - ' || a.MODELO as EQUIPO, C.NOMBRES as NOMBRES, " +
                              "C.telefono AS TELEFONO, C.celular as CELULAR, C.EMAIL AS EMAIL,R.FALLA,R.INFORMETALLER,P.DETALLE, " +
                              "P.PRESUPUESTO, I.NOMBRES AS TECNICO, R.FECHAINGRESO,R.PROMETIDO,R.FECHATERMINADO, ESTADO.NOMBRE ESTADO, " +
                              "R.ENTREGADO, R.FECHA_ENTREGADO, R.NS, R.AVISADO, R.CAMPOLIBRE1 AS COLOR, CAMPOLIBRE2 AS ESTADOEQUIPO " +
                              "FROM REPARACIONES R JOIN CLIENTES C ON R.CLIENTE = C.CODIGO " +
                              "JOIN PRESUPUESTOS P ON R.CODIGO = P.IDREPARACION " +
                              "JOIN INTEGRANTES I ON R.TECNICO = I.CODIGO " +
                              "inner join ESTADO on estado.CODIGO = R.ESTADO " +
                              "inner join APARATO a on R.NS = a.NS " +
                              "WHERE R.FECHA_AUD > '" + stardate + "' " +
                              "ORDER BY R.CODIGO ASC";
            cmd.CommandType = CommandType.Text;


            conn.Open();
            da.SelectCommand = cmd;
            da.Fill(dt);
            conn.Close();
            var dateAux = DateTime.Now.AddYears(-30);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DateTime ingreso;
                    DateTime prometido;
                    DateTime entregado;
                    DateTime terminado;
                    var rowResult = new REPARACIONESVIEW()
                    {
                        CODIGO = long.Parse(row["CODIGO"].ToString()),
                        EQUIPO = row["EQUIPO"].ToString(),
                        NOMBRES = row["NOMBRES"].ToString(),
                        FALLA = row["FALLA"].ToString(),
                        INFORMETALLER = row["INFORMETALLER"].ToString(),
                        TECNICO = row["TECNICO"].ToString(),
                        DETALLE = row["DETALLE"].ToString(),
                        PRESPUPUESTO = "$" + $"{row["PRESUPUESTO"]:0,0.00}",
                        FECHAINGRESO = DateTime.TryParse(row["FECHAINGRESO"].ToString(), out ingreso) && ingreso > dateAux ? ingreso : (DateTime?)null,
                        PROMETIDO = DateTime.TryParse(row["PROMETIDO"].ToString(), out prometido) && prometido > dateAux ? prometido : (DateTime?)null,
                        FECHATERMINADO = DateTime.TryParse(row["FECHATERMINADO"].ToString(), out terminado) && terminado > dateAux ? terminado : (DateTime?)null,
                        ESTADO = row["ESTADO"].ToString(),
                        FECHAUPDATE = DateTime.Now,

                        CELULAR = row["CELULAR"].ToString(),
                        TELEFONO = row["TELEFONO"].ToString(),
                        EMAIL = row["EMAIL"].ToString(),
                        ENTREGADO = row["ENTREGADO"].ToString() == "S",
                        NS = row["NS"].ToString(),
                        FECHAENTREGADO = DateTime.TryParse(row["FECHA_ENTREGADO"].ToString(), out entregado) && entregado > dateAux ? entregado : (DateTime?)null,
                        AVISADO = row["AVISADO"].ToString() == "S",
                        COLOR = row["COLOR"].ToString(),
                        ESTADOEQUIPO = row["ESTADOEQUIPO"].ToString()

                    };
                    result.Add(rowResult);
                }


            }
            else
            {

            }


            return result;
        }

        public void UpdateStatusChanged(List<long> ordersId)
        {
           



            FbConnection conn = new FbConnection();
            FbCommand cmd = new FbCommand();
            FbDataAdapter da = new FbDataAdapter();
            DataTable dt = new DataTable();
            string firebirdServer = ConfigurationManager.AppSettings["firebird.server"];

            conn.ConnectionString = "User=SYSDBA;Password=masterkey;Database=" + firebirdServer +
                                    ";Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;" +
                                    "MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;";
            cmd.Connection = conn;

            string orders = string.Join<long>(",", ordersId);




            cmd.CommandText = "update REPARACIONES_HISTORICO_ESTADO rh " +
                              "set RH.INTEGRADO = 'Y' " +
                              "where rh.IDREPARACION in ("+ orders + ")";
            cmd.CommandType = CommandType.Text;

            var a = conn.State;
            conn.Open();
            da.SelectCommand = cmd;
            da.Fill(dt);
            conn.Close();

            
        }
    }
}
