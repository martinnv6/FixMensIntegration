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
using FixmensCMD.Models;
using RestSharp;


namespace FixmensCMD.BLL
{
    public class OrdenBLL
    {
     /*   public List<long> GetStatusChanged()
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

        }*/


        public List<REPARACIONESVIEW> GetReparaciones()
        {
            var result = new List<REPARACIONESVIEW>();
            string firebirdServer = ConfigurationManager.AppSettings["firebird.server"];

            var ConnectionString = "User=SYSDBA;Password=masterkey;Database=" + firebirdServer +
                                    ";Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;" +
                                    "MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;";

            var query = "SELECT DISTINCT R.CODIGO, a.MARCA || ' - ' || a.MODELO as EQUIPO, C.NOMBRES as NOMBRES, " +
                              "C.telefono AS TELEFONO, C.celular as CELULAR, C.EMAIL AS EMAIL,R.FALLA,R.INFORMETALLER,P.DETALLE, " +
                              "P.PRESUPUESTO, I.NOMBRES AS TECNICO, R.FECHAINGRESO,R.PROMETIDO,R.FECHATERMINADO, ESTADO.NOMBRE ESTADO, " +
                              "R.ENTREGADO, R.FECHA_ENTREGADO, R.NS, R.AVISADO, R.CAMPOLIBRE1 AS COLOR, CAMPOLIBRE2 AS ESTADOEQUIPO " +
                              "FROM REPARACIONES R JOIN CLIENTES C ON R.CLIENTE = C.CODIGO " +
                              "JOIN PRESUPUESTOS P ON R.CODIGO = P.IDREPARACION " +
                              "JOIN INTEGRANTES I ON R.TECNICO = I.CODIGO " +
                              "inner join ESTADO on estado.CODIGO = R.ESTADO " +
                              "inner join APARATO a on R.NS = a.NS " +
                              "inner join REPARACIONES_HISTORICO_ESTADO RH on R.CODIGO = RH.IDREPARACION " +
                              "where RH.INTEGRADO = 'N' " +
                              "ORDER BY R.CODIGO ASC";
            using (FbConnection SelectConnection = new FbConnection(ConnectionString))
            {
                SelectConnection.Open();
                FbCommand writeCommand = new FbCommand(query, SelectConnection);
                FbDataAdapter da = new FbDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = writeCommand;
                da.Fill(dt);
                SelectConnection.Close();
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
            }

            return result;
        }

        public void UpdateStatusChanged(List<long> ordersId)
        {
            string orders = string.Join<long>(",", ordersId);
            string firebirdServer = ConfigurationManager.AppSettings["firebird.server"];
           
            var ConnectionString = "User=SYSDBA;Password=masterkey;Database=" + firebirdServer +
                                    ";Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;" +
                                    "MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;";
            using (FbConnection UpdateConnection = new FbConnection(ConnectionString))
            {
                UpdateConnection.Open();
                var query = "update REPARACIONES_HISTORICO_ESTADO rh " +
                            "set RH.INTEGRADO = 'S' " +
                            "where rh.IDREPARACION in (" + orders + ")";
                FbCommand writeCommand = new FbCommand(query, UpdateConnection);
                
                writeCommand.ExecuteNonQuery();
            }
    
        }



        public void SendSMS(ref string logAvance,List<PhoneModel> phones, string message,string send_at= null,string expires_at = null)
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
                logAvance += "\r\n #Envio SMS--> " + phone.PhoneNumber;
                var justNumber = Regex.Replace(phone.PhoneNumber, @"[^\d]", "");
                logAvance += " #Formateado--> " + justNumber + Environment.NewLine;
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
            logAvance += "\r\n";
            foreach (var item in response.Data.result.success)
            {
                logAvance += "\r\nGuardando registro en Azure de sms enviado a " + item.contact.number;
                var firstOrDefault = phones.FirstOrDefault(x => Regex.Replace(x.PhoneNumber, @"[^\d]", "") == item.contact.number);
                if (firstOrDefault != null)
                {
                    SMS sms = new SMS
                    {
                        REPARCIONID = firstOrDefault
                            .ReparacionId,
                        DEVICEID = long.Parse(item.device_id),
                        CONTACTID = long.Parse(item.contact.id),
                        CONTACTNAME = item.contact.name,
                        CONTACTNUMBER = item.contact.number,
                        CREATED = ToDateTimeFromEpoch(long.Parse(item.created_at)).ToLocalTime(),
                        ERROR = item.error,
                        EXPIRES = ToDateTimeFromEpoch(long.Parse(item.expires_at)).ToLocalTime(),
                        MESSAGE = item.message,
                        ID = long.Parse(item.id),
                        SEND = ToDateTimeFromEpoch(long.Parse(item.send_at)).ToLocalTime(),
                        STATUS = item.status
                    };
                    smsList.Add(sms);
                }
                else
                {
                    logAvance += Environment.NewLine+item.contact.number + " No se encontro en la Lista";
                }
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
