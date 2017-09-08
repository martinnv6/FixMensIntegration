using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace FixmensIntegrationApi.BLL
{
    public class SmsBLL
    {
        public void UpdateSMSStatus(long id, string status)
        {
           serviceEntities entiti = new serviceEntities();
            SMS model = entiti.SMS.FirstOrDefault(x => x.ID == id);
            model.STATUS = status;
            entiti.SaveChanges();

        }
    }
}