using DatabaseFirst;
using mcperscab;

using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using portaltp.Models;

namespace portaltp.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ForgotPasswordHyperLink.NavigateUrl = "ForgotPassword";

            string user_login = Page.User.Identity.Name;

            // в случае аутентификации ранее (например, кнопка браузера "назад" перенаправляем пользователя в зависимости от его роли
            if (Page.User.Identity.IsAuthenticated)
            {                   
                int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);

                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                    user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR)
                {
                    Response.Redirect("~/Cabinet/ViewOrders");
                }
                else if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_USER)
                {
                    Response.Redirect("~/Cabinet/MyOrders");
                }
                else
                {
                    Response.Redirect("~/");
                }
            } // if (Page.User.Identity.IsAuthenticated)

            /*RegisterHyperLink.NavigateUrl = "Register";
            // Включите, когда будет включено подтверждение учетной записи для функции сброса пароля
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }*/

            //LabelLoginError.Visible = true;            
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            { 
                if (Membership.ValidateUser(tbUserName.Text, tbPassword.Text))
                {
                    /*FormsAuthentication.SetAuthCookie(tbUserName.Text, false);
                    MC_Logging.MC_Logging_LogWrite(tbUserName.Text, null, 1, null);
                    FormsAuthentication.RedirectFromLoginPage(tbUserName.Text, false);*/

                    if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                    {
                        FormsAuthentication.SetAuthCookie(tbUserName.Text, false);
                        MC_PersCab_Logging.MC_PersCab_Logging_LogWrite(tbUserName.Text, null, MC_PersCab_Logging.MC_PersCab_LogEventType.MC_LET_LOGIN, null);

                        // перенаправляем пользователя в зависимости от его роли
                        int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(tbUserName.Text);

                        if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                            user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR)
                        {
                            Response.Redirect("~/Cabinet/ViewOrders");
                        }
                        else if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_USER)
                        {
                            Response.Redirect("~/Cabinet/MyOrders");
                        }
                        else
                        {
                            Response.Redirect("~/");
                        }                            
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(tbUserName.Text, false);
                        MC_PersCab_Logging.MC_PersCab_Logging_LogWrite(tbUserName.Text, null, MC_PersCab_Logging.MC_PersCab_LogEventType.MC_LET_LOGIN, null);
                        FormsAuthentication.RedirectFromLoginPage(tbUserName.Text, false);
                    }
                }
                else
                {
                    perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
                    if (perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == tbUserName.Text).Count() == 1)
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Неправильные данные.";
                    }
                    else // пользователь не найден
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Пользователь не найден. Зарегистрируйтесь.";
                    }
                                        
                } // if (Membership.ValidateUser(tbUserName.Text, tbPassword.Text))
            } // if (IsValid)
        }
    }
}