using DatabaseFirst;
using mcperscab;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace portaltp
{
    public partial class SiteCabinet : MasterPage
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
                    // throw new InvalidOperationException("Ошибка проверки маркера Anti-XSRF."); // я закомментировал
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
                BootstrapMenuMain.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_USER);

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
                ButtonANotifies.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);
                ButtonAAllUsers.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);
                ButtonADebugging.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);                

                ButtonASendRegistrationNotyfiesToUsers.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);                
                ButtonASendRegistrationSMSToUsers.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);

                ButtonABugButton.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);

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

        // отправка уведомлений пользователям о регистрации в ЛК (из 1С)
        protected void ButtonASendRegistrationNotyfiesToUsers_Click(object sender, EventArgs e)
        {
            perscabnewEntities lkAdapter = new perscabnewEntities();
                        
            //Guid notifystatusVOcheredi = lkAdapter.tblNotifyStatus.Where(p => p.caption_notifystatus == "в очереди").Select(p => p).First().id_notifystatus;
            //Guid notifytypeReg1C = lkAdapter.tblNotifyType.Where(p => p.caption_notifytype == "регистрация в ЛК из 1С").Select(p => p).First().id_notifytype;
            //Guid notifyChannelEmail = lkAdapter.tblNotifyChannel.Where(p => p.caption_notifychannel == "email").Select(p => p).First().id_notifychannel;

            // выбираем записи в очереди с видом регистрации из 1С
            IQueryable<tblNotify> notifiesList =             
                lkAdapter.tblNotify.Where(p => p.notifystatus_id == MC_PersCab_Consts.notifyStatus_VOcheredi && p.id_notifytype == MC_PersCab_Consts.notifyType_Sys_RegUserFrom1C).Select(p => p);

            /*tblNotify ttt = lkAdapter.tblNotify.Where(p => p.notifystatus_id == notifystatusVOcheredi && p.id_notifytype == notifytypeReg1C).Select(p => p);*/

            int i = 0;

            Random rndPsw = new Random();

            foreach (tblNotify notifyTemp in notifiesList)
            {                                             
                if (i > 10) break;

                // если канал отправки - email
                if (notifyTemp.id_notifychannel == MC_PersCab_Consts.notifyChannel_Email)
                {
                    //string userName = notifyTemp.email;
                    //string userName = lkAdapter.tblUserInfo.Where(p => p.id_user == notifyTemp.link_id).Select(p => p).First().user_login;
                    tblUserInfo userInfo = lkAdapter.tblUserInfo.Where(p => p.id_user == notifyTemp.recipient_id).Select(p => p).First();
                    string userName = userInfo.user_login;
                    string userEmail = userInfo.email;// notifyTemp.email;
                    //string userPassword = Membership.GeneratePassword(11,0);
                    string userPassword = MC_GeneratePass(rndPsw.Next(1000), 11);

                    string urlLk = "https://www.oke38.ru/portaltp";

                    // если пользователь с таким email и именем не существует
                    if (Membership.FindUsersByEmail(userEmail).Count == 0 && Membership.FindUsersByName(userName).Count == 0)
                    {
                        i++;

                        // добавляем пользователя в БД ASP.NET            
                        MembershipUser user = Membership.CreateUser(userName, userPassword, userEmail);
                        user.IsApproved = true;
                        Membership.UpdateUser(user);
                        //----------------------------------------

                        // формируем и отправляем письмо                
                        /*try
                        {*/
                        MailMessage mail = new MailMessage();

                        mail.From = new MailAddress("robot@oke38.ru", "ОГУЭП \"Облкоммунэнерго\"", Encoding.GetEncoding(1251));

                        try
                        {
                            mail.To.Add(new MailAddress(userEmail, userEmail));
                        }
                        catch (FormatException)
                        {
                            //notifyTemp.notifystatus_id = lkAdapter.tblNotifyStatus.Where(p => p.caption_notifystatus == "ошибка в email").Select(p => p).First().id_notifystatus;
                            notifyTemp.notifystatus_id = MC_PersCab_Consts.notifyStatus_ErrorInEmail;
                            continue;
                        }

                        mail.Bcc.Add(new MailAddress("admin@oke38.ru", "admin@oke38.ru")); // копия письма на ящик администратора
                        //mail.To.Add(new MailAddress("admin@oke38.ru", "admin@oke38.ru")); // копия письма на ящик администратора

                        mail.Subject = "Регистрация в личном кабинете ОГУЭП \"Облкоммунэнерго\"";
                        mail.IsBodyHtml = true;

                        mail.Body = "<p>Осуществлена регистрация в личном кабинете ОГУЭП \"Облкоммунэнерго\"</p>";
                        mail.Body += String.Format("<p><a href=\"{0}\">Личный кабинет ТП</a></p>", urlLk);
                        mail.Body += String.Format("<p>Логин:</p><p>{0}</p>", userEmail);
                        mail.Body += String.Format("<p>Пароль:</p><p>{0}</p>", userPassword);

                        /*mail.Body += "<p>Это автоматическое уведомление!</p>";
                        mail.Body += "<p>Пожалуйста, не отвечайте на данное письмо.</p>";*/

                        SmtpClient smtp = new SmtpClient("mail.nic.ru", 2525);
                        smtp.Credentials = new NetworkCredential("robot@oke38.ru", "77pHmX5TgPZ3c");

                        notifyTemp.notify_text = mail.Body;
                        notifyTemp.date_send_notify = DateTime.Now; 
                        
                        try
                        {
                            smtp.Send(mail);

                            //notifyTemp.notifystatus_id = lkAdapter.tblNotifyStatus.Where(p => p.caption_notifystatus == "отправлено").Select(p => p).First().id_notifystatus;
                            notifyTemp.notifystatus_id = MC_PersCab_Consts.notifyStatus_Otpravleno;
                        }
                        catch (SmtpFailedRecipientException)
                        {
                            //notifyTemp.notifystatus_id = lkAdapter.tblNotifyStatus.Where(p => p.caption_notifystatus == "не доставлено").Select(p => p).First().id_notifystatus;
                            notifyTemp.notifystatus_id = MC_PersCab_Consts.notifyStatus_NeDostavleno;
                        }

                        //---------------------------
                        
                        //tblUserInfo userInfo = lkAdapter.tblUserInfo.Where(p => p.id_user == notifyTemp.link_id).Select(p => p).First(); и не линк, а ресипиент
                        //userInfo.id_registration_typeid = lkAdapter.tblUserRegistrationType.Where(p => p.caption_registration_typeid == "1С (уведомление по email)").Select(p => p).First().id_registration_typeid;
                        userInfo.id_registration_typeid = MC_PersCab_Consts.userRegType_1CEmail;
                        //userInfo.password_text = userPassword; // пароль больше не схраняем
                                                    
                        //lkAdapter.SaveChanges();

                        /*}
                        catch ()
                        {
                        }*/
                        
                        // lkAdapter.SaveChanges();

                    } // if (Membership.FindUsersByEmail(userEmail).Count == 0)

                } // if (notifyTemp.id_notifychannel == notifyChannelEmail)

            } // foreach (tblNotify notifyTemp in notifiesList)

            lkAdapter.SaveChanges();

        } // protected void ButtonASendRegistrationNotyfiesToUsers_Click(object sender, EventArgs e)

        // отправка уведомлений пользователям о регистрации в ЛК (из 1С) по смс (формирование текста БЕЗ ОТПРАВКИ!!!)
        protected void ButtonASendRegistrationSMSToUsers_Click(object sender, EventArgs e)
        {
            perscabnewEntities lkAdapter = new perscabnewEntities();

            // переделать на "ожидание"
            /*Guid notifystatus_Wait = lkAdapter.tblNotifyStatus.Where(p => p.caption_notifystatus == "ожидание").Select(p => p).First().id_notifystatus;
            Guid notifystatus_VOcheredi = lkAdapter.tblNotifyStatus.Where(p => p.caption_notifystatus == "в очереди").Select(p => p).First().id_notifystatus;
            Guid notifytypeReg1C = lkAdapter.tblNotifyType.Where(p => p.caption_notifytype == "регистрация в ЛК из 1С").Select(p => p).First().id_notifytype;
            Guid notifyChannelSMS = lkAdapter.tblNotifyChannel.Where(p => p.caption_notifychannel == "sms").Select(p => p).First().id_notifychannel;*/

            // выбираем "ожидающие" записи с видом регистрации из 1С
            IQueryable<tblNotify> notifiesList =
                //lkAdapter.tblNotify.Where(p => p.notifystatus_id == notifystatus_Wait && p.id_notifytype == notifytypeReg1C && p.id_notifychannel == notifyChannelSMS).Select(p => p);
                lkAdapter.tblNotify.Where(p => 
                    p.notifystatus_id == MC_PersCab_Consts.notifyStatus_Waiting && 
                    p.id_notifytype == MC_PersCab_Consts.notifyType_Sys_RegUserFrom1C &&
                    p.id_notifychannel == MC_PersCab_Consts.notifyChannel_SMS).Select(p => p);

            Random rndPsw = new Random();

            foreach (tblNotify notifyTemp in notifiesList)
            {                                                
                string userName = lkAdapter.tblUserInfo.Where(p => p.id_user == notifyTemp.link_id).Select(p => p).First().user_login;
                //string userEmail = notifyTemp.email;                                
                string userPassword = MC_GeneratePass(rndPsw.Next(1000), 11);

                // если пользователь с таким именем не существует
                if (Membership.FindUsersByName(userName).Count == 0)
                {                   

                    // добавляем пользователя в БД ASP.NET            
                    MembershipUser user = Membership.CreateUser(userName, userPassword);
                    user.IsApproved = true;
                    Membership.UpdateUser(user);
                    //----------------------------------------

                    /*string notifyText =
                            "https://it@oblkomenergo.ru:FIDRYKpdq5wzB1s5jTrmA53eQDbX@gate.smsaero.ru/v2/sms/send?number=" +
                            userName +
                            "&text=ЛичныйКабинетТехП oke38.ru. Логин: " +
                            userName +
                            " Пароль: " +
                            userPassword +
                            "&sign=OKE38.RU&channel=DIRECT";*/

                    string notifyText =                            
                            "ЛичныйКабинетТехП oke38.ru. Логин: " +  userName + " Пароль: " + userPassword;
                            
                    notifyTemp.notify_text = notifyText;
                    //notifyTemp.date_send_notify = DateTime.Now;

                    notifyTemp.notifystatus_id = MC_PersCab_Consts.notifyStatus_VOcheredi;// lkAdapter.tblNotifyStatus.Where(p => p.caption_notifystatus == "отправлено").Select(p => p).First().id_notifystatus;
                    //---------------------------

                    tblUserInfo userInfo = lkAdapter.tblUserInfo.Where(p => p.id_user == notifyTemp.link_id).Select(p => p).First();
                    //userInfo.id_registration_typeid = lkAdapter.tblUserRegistrationType.Where(p => p.caption_registration_typeid == "1С (уведомление по sms)").Select(p => p).First().id_registration_typeid;
                    userInfo.password_text = userPassword;

               } // if (Membership.FindUsersByEmail(userEmail).Count == 0)

            } // foreach (tblNotify notifyTemp in notifiesList)

            lkAdapter.SaveChanges();
            
        } // protected void ButtonASendRegistratioSMSToUsers_Click(object sender, EventArgs e)

        protected string MC_GeneratePass(int randInit, int pswLength)
        {            
            string iPass = "";
            string[] arr = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Z", "b", "c", "d", "f", "g", "h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z", "A", "E", "U", "Y", "a", "e", "i", "o", "u", "y" };
            Random rnd = new Random(randInit);
            for (int i = 0; i < pswLength; i = i + 1)
            {
                iPass = iPass + arr[rnd.Next(0, 57)];
            }

            return iPass;
        }

        // моя спецкнопка для выполнения специальных операций
        protected void ButtonABugButton_Click(object sender, EventArgs e)
        {
            //MembershipUser user = Membership.CreateUser("mogorod@slud.ru", "!QAZ3edc@WSX", "mogorod@slud.ru");
        }
    }

}