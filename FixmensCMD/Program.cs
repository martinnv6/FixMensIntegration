using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FixmensCMD.BLL;
using FixmensCMD.Models;


namespace FixmensCMD
{
    class Program
    {
        static void Main(string[] args)
        {
            string logAvance = "\r\n";
            Console.Write("\n ----------TAREA DE INTEGRACION DE ORDENES SGTALLER A AZURE EN INTERNET----------");
            Console.Write("\n ----------Martin Navarrete Villegas----------");

            Console.Write("\n\n Leyendo cambios recientes en ordenes.....");
            OrdenBLL bllOrden = new OrdenBLL();
            var res = bllOrden.GetReparaciones();
           

            logAvance += " ------------------- \r\n";
            logAvance += res.Count + " ordenes con cambios en BD \r\n";
            logAvance += " ------------------- \r\n\r\n";


            FixmensEntities modelSql = new FixmensEntities();

            List<PhoneModel> phones = new List<PhoneModel>();
            List<long> changedAux = new List<long>();


            try
            {
                logAvance += " -------------------";
                foreach (var item in res)
                {

                    modelSql.REPARACIONESVIEW.AddOrUpdate(x => x.CODIGO, item);
                    Console.Write("Orden actualizada: " + item.CODIGO);

                    logAvance += " \r\nOrden actualizada: " + item.CODIGO;
                    changedAux.Add(item.CODIGO);
                    if (item.CELULAR.Length >= 10 || item.TELEFONO.Length >=10)
                    {
                        PhoneModel ph = new PhoneModel
                        {
                            PhoneNumber = string.IsNullOrEmpty(item.CELULAR) ? item.TELEFONO : item.CELULAR,
                            ReparacionId = item.CODIGO
                        };
                        logAvance += " Celular: " + ph.PhoneNumber + "\r\n";
                        phones.Add(ph);
                    }
                }
                logAvance += " ------------------- \r\n\r\n";

                if (changedAux.Count > 0)
                {
                    Console.Write("\n Integrando datos a Microsoft AZURE...");

                    logAvance += " ------------------- \r\n";
                    logAvance += " Integrando datos a Microsoft AZURE...\r\n";
                    modelSql.SaveChanges();
                    Console.Write("\n Cambios guardaos en Azure...");

                    logAvance += " Cambios guardaos en Azure \r\n";
                    logAvance += " ------------------- \r\n\r\n";

                    //Update to integrated orders
                    Console.Write("\n Actualizando estatus a correctamente Integrado...");

                    logAvance += " ------------------- \r\n";
                    logAvance += " Actualizando estatus a correctamente Integrado..." + "\r\n";

                    bllOrden.UpdateStatusChanged(changedAux);

                    Console.Write("Status actualizados");
                    logAvance += " Status actualizados" + "\r\n";
                    Console.Write("\n" + changedAux.Count + " Ordenes se actualizaron correctamente");
                    logAvance += changedAux.Count + " Ordenes se actualizaron correctamente" + "\r\n";
                    logAvance += " ------------------- \r\n\r\n";

                    logAvance += " ------------------- \r\n";
                    Console.Write("\n" + phones.Count + "\n Enviando SMS...");
                    logAvance += "Enviando " + phones.Count + "SMS..." + "\r\n";
                    bllOrden.SendSMS(ref logAvance, phones,
                        "---FIXMENS--- ESTIMADO CLIENTE, SU EQUIPO HA CAMBIADO DE ESTATUS, INFO EN https://www.fixmens.com.mx/consultar.html O AL 8282840220, ENCUESTA DE SATISFACCION ---> goo.gl/forms/CMqBSKznBN6WzBI72");
                    Console.Write("\n" + changedAux.Count + "\n SMS Enviados");
                    logAvance += phones.Count + " SMS Enviados" + "\r\n";
                    logAvance += " ------------------- \r\n\r\n";
                }
                else
                {
                    Console.Write("\n No hay ordenes actualizadas ......Presione una tecla");
                    logAvance += Environment.NewLine + " No hay ordenes actualizadas";
                }

                FixmensLog log = new FixmensLog(logAvance);
            }
            catch (Exception e)
            {
                FixmensLog log = new FixmensLog(logAvance + Environment.NewLine + "\n" + e.ToString() + e.StackTrace);



                Console.WriteLine(e);
                throw;
            }
            finally
            {
                
            }

            //Console.ReadKey();
        }
    }
}
