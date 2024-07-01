using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using DatabaseFirst;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Configuration;
using System.Data;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using portaltp.Models;

namespace portaltp.Account
{
    public partial class ForgotPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Forgot(object sender, EventArgs e)
        {
            if (IsValid)
            {
                /* // исходный код шаблона
                // Проверка электронного адреса пользователя
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser user = manager.FindByName(Email.Text);
                if (user == null || !manager.IsEmailConfirmed(user.Id))
                {
                    FailureText.Text = "Пользователь не существует или не подтвержден.";
                    ErrorMessage.Visible = true;
                    return;
                }
                // Дополнительные сведения о включении подтверждения учетной записи и сброса пароля см. на странице https://go.microsoft.com/fwlink/?LinkID=320771.
                // Отправка сообщения электронной почты с кодом и перенаправление на страницу сброса пароля
                //string code = manager.GeneratePasswordResetToken(user.Id);
                //string callbackUrl = IdentityHelper.GetResetPasswordRedirectUrl(code, Request);
                //manager.SendEmail(user.Id, "Сброс пароля", "Сбросьте ваш пароль, щелкнув <a href=\"" + callbackUrl + "\">здесь</a>.");
                */

                //loginForm.Visible = false;
                //-------------------------------------------
                                
                // если пользователь с таким email существует и он единственный                
                if (Membership.FindUsersByEmail(Email.Text).Count == 1)
                {
                    // формируем код и строку подтверждения, сохраняем код в БД
                    Random confirmCode = new Random();
                    string confirmCodeString = confirmCode.Next(10000, 99999).ToString();

                    //string urlPasswordRecovery = "https://www.oke38.ru/perscabnew/Account/ResetPassword?email=" + Email.Text + "&confirmcode=" + confirmCodeString;
                    string urlPasswordRecovery = "https://www.oke38.ru/portaltp/Account/ResetPassword?email=" + Email.Text + "&confirmcode=" + confirmCodeString;

                    perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
                    try
                    {
                        tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.email == Email.Text).Select(p => p).First();
                        
                        userInfo.confirm_code = confirmCodeString;
                        userInfo.confirm_code_date_send = DateTime.Now;
                        perscabnewEntitiesAdapter.SaveChanges();
                    }
                    catch (InvalidOperationException)
                    {
                        FailureText.Text = "Ошибка идентификации пользователя.";
                        ErrorMessage.Visible = true;
                        return;
                    }                    
                    //---------------------------------------

                    // отправляем письмо
                    try
                    {
                        MailMessage mail = new MailMessage();
                                                
                        mail.From = new MailAddress("admin@oke38.ru", "ОГУЭП \"Облкоммунэнерго\"", Encoding.GetEncoding(1251));
                        mail.To.Add(new MailAddress(Email.Text, Email.Text));
                        //mail.To.Add(new MailAddress("it@oblkomenergo.ru", "it@oblkomenergo.ru")); // копия письма на ящик администратора

                        mail.Subject = "Восстановление пароля в личном кабинете ОГУЭП \"Облкоммунэнерго\"";
                        mail.IsBodyHtml = true;

                        mail.Body = "<p>Ссылка для восстановления пароля в личном кабинете ОГУЭП \"Облкоммунэнерго\":</p>";
                        mail.Body += String.Format("<p><a href=\"{0}\">восстановить пароль</a></p>", urlPasswordRecovery);                        
                        mail.Body += "<p>Это автоматическое уведомление!</p>";
                        mail.Body += "<p>Пожалуйста, не отвечайте на данное письмо.</p>";

                        SmtpClient smtp = new SmtpClient("mail.nic.ru", 2525);
                        smtp.Credentials = new NetworkCredential("admin@oke38.ru", "77pHmX5TgPZ3c");

                        smtp.Send(mail);
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(this, "Error sending a report.\n" + ex.ToString());
                    }
                    //---------------------

                    /*ButtonSendEmail.Visible = false;
                    DisplayEmail.Visible = true;
                    ErrorMessage.Visible = false;*/

                    Response.Redirect("~/Account/ShowAccountMessage?msg=MsgForgotPasswordEmailSend");
                } // if (Membership.FindUsersByEmail(Email.Text).Count > 0)
                else
                {
                    FailureText.Text = "Ошибка идентификации пользователя.";
                    ErrorMessage.Visible = true;
                }
                
            }
        }
    }
}