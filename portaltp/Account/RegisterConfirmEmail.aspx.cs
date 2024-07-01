using DatabaseFirst;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.Security;

namespace portaltp.Account
{
    public partial class RegisterConfirmEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Request.QueryString.Count == 2)
            {
                string emailUser = Request.QueryString["email"].ToString();
                string confirmCodeString = Request.QueryString["confirmcode"].ToString();

                perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
                try
                {
                    tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == emailUser).Select(p => p).First();

                    if (String.Equals(userInfo.confirm_code, confirmCodeString)) // если код правильный
                    {
                        // разрешаем пользователю вход в систему
                        try
                        {                               
                            MembershipUser user = Membership.GetUser(emailUser);
                            user.IsApproved = true;
                            Membership.UpdateUser(user);

                            Response.Redirect("~/Account/EmailConfirmation");
                        }
                        catch (MembershipCreateUserException exc)
                        {
                            FailureText.Text = exc.Message;
                            errorPanel.Visible = true;
                            return;
                        }                        
                    } // if (userInfo.confirm_code == confirmCodeString) // если код правильный
                    else
                    {
                        FailureText.Text = "Неверный код подтверждения.";
                        errorPanel.Visible = true;
                    }

                }
                catch (InvalidOperationException)
                {
                    FailureText.Text = "Ошибка данных.";
                    errorPanel.Visible = true;
                    return;
                }
                catch (NullReferenceException ex)
                {
                    FailureText.Text = ex.Message;
                    errorPanel.Visible = true;
                    return;
                }
            }
            else
            {
                Response.Redirect("~/");
            }
        } // protected void Page_Load(object sender, EventArgs e)

    }
}