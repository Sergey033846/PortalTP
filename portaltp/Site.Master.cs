using mcperscab;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace portaltp
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Код ниже защищает от XSRF-атак
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Использование маркера Anti-XSRF из файла cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Создание нового маркера Anti-XSRF и его сохранение в файле cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Задание маркера Anti-XSRF
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Проверка маркера Anti-XSRF
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    //throw new InvalidOperationException("Ошибка проверки маркера Anti-XSRF."); // я закомментировал
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /* проверяем - включен ли режим обслуживания */
            bool pcMaintenanceMode = true; // по умолчанию
            pcMaintenanceMode = bool.Parse(WebConfigurationManager.AppSettings["pcMaintenanceMode"]);
            if (pcMaintenanceMode)
            {
                Response.Redirect("~/Maintenance.aspx");
            }
            else
            {
                string user_login = Page.User.Identity.Name;
                int user_role = -1;

                if (user_login.Length > 0)
                {
                    user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);
                }

                // устанавливаем доступность элементов управления
                ButtonZMyOrders.Visible = ButtonZSendOrder.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_USER);

                ButtonOAllOrders.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR) || (user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER) 
                                || (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN) || (user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR);

                ButtonOAllChat.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR) || (user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER)
                                || (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);

                ButtonOShowZayavkaForm.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR) || (user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER)
                                || (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);

                ButtonOShowHelpContents.Visible =
                    (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR) || (user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER) ||
                    (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN) || (user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR) ||
                    (user_role == (int)MC_PersCab_UserRoleType.MC_urt_USER);

                ButtonALog.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);
                ButtonAAllUsers.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);

                ButtonGPAllDogovorsEnergo.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR) || (user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER) || 
                                                (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN) || (user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR);
                //-----------------------------------------------

            } // if (pcMaintenanceMode)
        }

        // посмотреть!!!
        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

    }

}