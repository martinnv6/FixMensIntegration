using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using FixmensCMD.Models.dto;
using System.Net;
using System.Text.RegularExpressions;
using RestSharp;


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

        public void SendSMS(List<string> phones, string message,string send_at= null,string expires_at = null)
        {
            string smsDevice = ConfigurationManager.AppSettings["SMSDevice"];
            var client = new RestClient("http://smsgateway.me/api/");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("/v3/messages/send", Method.POST);
            request.AddParameter("email", "martin.nv6@gmail.com"); // adds to POST or URL querystring based on Method
            request.AddParameter("password", "1123581321"); // adds to POST or URL querystring based on Method
            request.AddParameter("device", smsDevice); // adds to POST or URL querystring based on Method

            foreach (var phone in phones)
            {
                var justNumber = Regex.Replace(phone, @"[^\d]", "");
                request.AddParameter("number[]", justNumber); // adds to POST or URL querystring based on Method
            }
            //request.AddParameter("number[]", "8281203827");
            //request.AddParameter("number[]", "8281221221");

            request.AddParameter("message", message); // adds to POST or URL querystring based on Method
            request.AddParameter("send_at", send_at ?? ""); // adds to POST or URL querystring based on Method
            request.AddParameter("expires_at", expires_at ?? ""); // adds to POST or URL querystring based on Method
            

            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            // easily add HTTP Headers
            // request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            //request.AddFile(path);

            //// execute the request
            //IRestResponse response = client.Execute(request);
            //var content = response.Content; // raw content as string

            //// or automatically deserialize result
            //// return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            IRestResponse<SmsResponseDTO> response = client.Execute<SmsResponseDTO>(request);

            FixmensEntities modelSql = new FixmensEntities();
            List<SMS> smsList = new List<SMS>();

            foreach(var item in response.Data.result.success)
            { 
             SMS sms = new SMS();
                sms.REPARCIONID = long.Parse(phones.FirstOrDefault(x => x == item.contact.number));
                sms.DEVICEID = long.Parse(item.device_id);
                sms.CONTACTID = long.Parse(item.contact.id);
                sms.CONTACTNAME = item.contact.name;
                sms.CONTACTNUMBER = item.contact.number;
                sms.CREATED = ToDateTimeFromEpoch(long.Parse(item.created_at)).ToLocalTime();
                sms.ERROR = item.error;
                sms.EXPIRES = ToDateTimeFromEpoch(long.Parse(item.expires_at)).ToLocalTime();
                sms.MESSAGE = item.message;
                sms.ID = long.Parse(item.id);
                sms.SEND = ToDateTimeFromEpoch(long.Parse(item.send_at)).ToLocalTime();
                sms.STATUS = item.status;
                smsList.Add(sms);

            }
            modelSql.SMS.AddRange(smsList);
            modelSql.SaveChanges();

            //var name = response2.Data.Name;



            // easy async support
            //client.ExecuteAsync(request, response =>
            //{
            //    Console.WriteLine(response.Content);
            //});

            //// async with deserialization
            //var asyncHandle = client.ExecuteAsync<Person>(request, response => {
            //    Console.WriteLine(response.Data.Name);
            //});

            // abort the request on demand
            //asyncHandle.Abort();
        }
        /// <summary>
        /// Converts the given epoch time to a <see cref="DateTime"/> with <see cref="DateTimeKind.Utc"/> kind.
        /// </summary>
        public static DateTime ToDateTimeFromEpoch(long intDate)
        {
            var timeInTicks = intDate * TimeSpan.TicksPerSecond;
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(timeInTicks);
        }
    }
}
