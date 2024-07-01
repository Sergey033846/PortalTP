using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp.Account
{
    public partial class ShowAccountMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count == 1)
            {
                string messageID = Request.QueryString["msg"].ToString();

                switch (messageID)
                {
                    case "MsgForgotPasswordEmailSend":
                        MsgForgotPasswordEmailSend.Visible = true;
                        break;
                }
                switch (messageID)
                {
                    case "MsgConfirmCodeEmailSend":
                        MsgConfirmCodeEmailSend.Visible = true;
                        break;
                }
            }
        }
           
    }
}