using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FixmensCMD.BLL;
using FixmensCMD.Repository;
using FixmensIntegrationApi;

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

            FixMensSQLModel modelSql = new FixMensSQLModel();

            
            foreach (var item in res)
            {
                if (changed.Contains(item.CODIGO)) { 
                modelSql.REPARACIONESVIEWs.AddOrUpdate(x=> x.CODIGO, item);
                Console.Write("\n Orden actualizada :" + item.CODIGO);
                }
            }
            Console.Write("\n Integrando datos a Microsoft AZURE...");
            modelSql.SaveChanges();
            
            //Update to integrated orders
            bllOrden.UpdateStatusChanged(changed);

            Console.Write("\n Ordenes se actualizaron correctamente ......Presione una tecla");

            //Console.ReadKey();
        }
    }
}
