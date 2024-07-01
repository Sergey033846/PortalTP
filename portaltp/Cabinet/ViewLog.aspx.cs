using DatabaseFirst;
using mcperscab;

using DevExpress.Export;
using DevExpress.XtraPrinting;
using DevExpress.Web;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp.Cabinet
{
    public partial class ViewLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ASPxGridViewLog.FocusedRowIndex = -1;
            }

            string user_login = Page.User.Identity.Name;

            if (Page.User.Identity.IsAuthenticated)
            {
                int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);
                
                // видимость операций и компонентов администратора
                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
                {
                    this.SqlDataSourceViewLog.SelectCommand =
                        String.Concat(
                            "SELECT",
                            " logrec_datetime, tUI.user_nameingrid, caption_logeventtype, id_zayavka, tL.id_user, id_logrec,",
                            " tUI.user_login, caption_registration_typeid, tUR.caption_userrole",
                            " FROM tblLog tL",
                            " LEFT JOIN tblUserInfo tUI ON tL.id_user = tUI.id_user",
                            " LEFT JOIN tblUserRole tUR ON tUI.id_userrole = tUR.id_userrole",
                            " LEFT JOIN tblLogEventType tLET ON tL.id_logeventtype = tLET.id_logeventtype",
                            " LEFT JOIN tblUserRegistrationType tURT ON tUI.id_registration_typeid = tURT.id_registration_typeid",
                            " ORDER BY logrec_datetime desc");
                }
                else
                {
                    Response.Redirect("~", true);
                }

            } // if if (Page.User.Identity.IsAuthenticated)
            else
            {
                Response.Redirect("~", true);
            }

        } // protected void Page_Load(object sender, EventArgs e)

    } // public partial class ViewLog : System.Web.UI.Page
}