using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FixmensCMD.BLL;


namespace FixmensCMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\n ----------TAREA DE INTEGRACION DE ORDENES SGTALLER A AZURE EN INTERNET----------");
            Console.Write("\n ----------Martin Navarrete Villegas----------");

            Console.Write("\n\n Leyendo cambios recientes en ordenes.....");
            OrdenBLL bllOrden = new OrdenBLL();
            var res = bllOrden.GetReparaciones();
            var changed = bllOrden.GetStatusChanged();

            FixmensEntities modelSql = new FixmensEntities();

            List<string> phones = new List<string>();
            List<long> changedAux = new List<long>();


            try
            {
                foreach (var item in res)
                {
                    if (changed.Contains(item.CODIGO))
                    {
                        modelSql.REPARACIONESVIEW.AddOrUpdate(x => x.CODIGO, item);
                        Console.Write("\n Orden actualizada: " + item.CODIGO);
                        changedAux.Add(item.CODIGO);
                        if (item.CELULAR.Length >= 10)
                        {
                            phones.Add(item.CELULAR);
                        }
                    }
                }
                
                if (changedAux.Count > 0)
                {
                    Console.Write("\n Integrando datos a Microsoft AZURE...");
                    modelSql.SaveChanges();

                    //Update to integrated orders
                    bllOrden.UpdateStatusChanged(changedAux);
                    Console.Write("\n"+changedAux.Count + " Ordenes se actualizaron correctamente ......Presione una tecla");
                    bllOrden.SendSMS(phones, "---FIXMENS--- ESTIMADO CLIENTE, SU EQUIPO HA CAMBIADO DE ESTATUS, INFO EN https://www.fixmens.com.mx/consultar.html O AL 8282840220, ENCUESTA DE SATISFACCION ---> goo.gl/forms/CMqBSKznBN6WzBI72");
                }
                else
                {
                    Console.Write("\n No hay ordenes actualizadas ......Presione una tecla");
                }

               
            }
            catch (Exception e)
            {
                FixmensLog log = new FixmensLog(e.ToString() + e.StackTrace);
                
                
                Console.WriteLine(e);
                throw;
            }

            //Console.ReadKey();
        }
    }
}
