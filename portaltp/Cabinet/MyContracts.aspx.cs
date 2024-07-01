using DatabaseFirst;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp.Cabinet
{
    public partial class MyContracts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.email == user_login).Select(p => p).First();

            /*this.SqlDataSourceMyContracts.SelectCommand =
                String.Concat("SELECT id_contract, contract_number_1c, contract_date_1c, ",
                                "(SELECT tblCS.caption_contractstatus FROM ",
                                "(SELECT TOP 1 tblCSH.id_contractstatus FROM tblContractStatusHistory tblCSH WHERE tblCSH.id_contract = tblC.id_contract ORDER BY tblCSH.date_status DESC) as tblTEMP ",
                                "LEFT JOIN tblContractStatus tblCS ON tblTEMP.id_contractstatus = tblCS.id_contractstatus) as caption_status ",
                                "FROM tblContract tblC ",
                                "WHERE tblC.id_zayavka IN (SELECT id_zayavka FROM tblZayavka WHERE id_user = '", userInfo.id_user.ToString(), "')");*/

            this.SqlDataSourceMyContracts.SelectCommand =
                String.Concat("SELECT id_contract, contract_number_1c, contract_date_1c, caption_contractstatus ",
                                "FROM tblContract tblC ",
                                "LEFT JOIN tblContractStatus tblCS ON tblC.id_contractstatus = tblCS.id_contractstatus ",
                                "WHERE tblC.id_zayavka IN (SELECT id_zayavka FROM tblZayavka WHERE id_user = '", userInfo.id_user.ToString(), "')");
        }

    }
}