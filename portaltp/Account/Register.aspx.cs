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

namespace portaltp.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            /* проверяем - включен ли режим обслуживания */
            bool pcMaintenanceMode = bool.Parse(WebConfigurationManager.AppSettings["pcMaintenanceMode"]);
            if (pcMaintenanceMode)
            {
                Response.Redirect("~/Maintenance.aspx", true);
            }
            else
            {
                try
                {                    
                    string userName = tbEmail.Text.ToLower();
                    string emailText = tbEmail.Text.ToLower();

                    // исключаем спам *.ua
                    if (!userName.Contains(".ua"))
                    {
                        int usertypeIndex = comboboxUserType.SelectedIndex; // 0 - ФЛ, 1 - ЮЛ, 2 - ИП

                        // добавляем пользователя в БД ASP.NET            
                        MembershipUser user = Membership.CreateUser(userName, tbPassword.Text, emailText);

                        // запрещаем вход до момента подтверждения e-mail
                        user.IsApproved = false;

                        Membership.UpdateUser(user);
                        //----------------------------------------

                        // добавляем пользователя в "нашу" таблицу
                        string connectionString = WebConfigurationManager.ConnectionStrings["PerscabDbConnection"].ConnectionString;
                        SqlConnection connection = new SqlConnection(connectionString);

                        string queryString = string.Concat(
                            "INSERT INTO tblUserInfo(id_user,id_usertype,id_userrole,",
                            "fl_familiya,fl_name,fl_otchestvo,",
                            "telefon,email,inn,",
                            "yl_fullname,yl_shortname,",
                            "contact_familiya,contact_name,contact_otchestvo,",
                            "confirm_code,confirm_code_date_send,comment,",
                            //"date_create_user, password_text, user_login, user_nameingrid, id_filial)",
                            "date_create_user, user_login, user_nameingrid, id_filial)", // убрал пароль
                            " VALUES(NEWID(), @id_usertype, @id_userrole, @fl_familiya, @fl_name, @fl_otchestvo, @telefon, @email, @inn,",
                            "@yl_fullname, @yl_shortname, @contact_familiya, @contact_name, @contact_otchestvo,",
                            //"@confirm_code, @confirm_code_date_send, @comment, @date_create_user, @password_text, @user_login, @user_nameingrid, @id_filial)");
                            "@confirm_code, @confirm_code_date_send, @comment, @date_create_user, @user_login, @user_nameingrid, @id_filial)"); // убрал пароль

                        SqlCommand cmd = new SqlCommand(queryString, connection);

                        cmd.Parameters.AddWithValue("@id_usertype", usertypeIndex.ToString());
                        cmd.Parameters.AddWithValue("@id_userrole", "0"); // "заявитель"
                        cmd.Parameters.AddWithValue("@date_create_user", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@comment", DBNull.Value);
                        cmd.Parameters.AddWithValue("@id_filial", -1);

                        //cmd.Parameters.AddWithValue("@password_text", tbPassword.Text); // пароль больше не схраняем

                        Random confirmCode = new Random();
                        string confirmCodeString = confirmCode.Next(10000, 99999).ToString();
                        cmd.Parameters.AddWithValue("@confirm_code", confirmCodeString);
                        cmd.Parameters.AddWithValue("@confirm_code_date_send", DateTime.Now.ToString());

                        switch (usertypeIndex)
                        {
                            case 0: // ФЛ                    
                                cmd.Parameters.AddWithValue("@fl_familiya", tbFamiliya.Text);
                                cmd.Parameters.AddWithValue("@fl_name", tbName.Text);
                                cmd.Parameters.AddWithValue("@fl_otchestvo", tbOtchestvo.Text);                                
                                cmd.Parameters.AddWithValue("@telefon", tbPhone.Text);
                                cmd.Parameters.AddWithValue("@email", emailText);
                                cmd.Parameters.AddWithValue("@yl_fullname", DBNull.Value);
                                cmd.Parameters.AddWithValue("@yl_shortname", DBNull.Value);
                                cmd.Parameters.AddWithValue("@inn", DBNull.Value);
                                cmd.Parameters.AddWithValue("@contact_familiya", DBNull.Value);
                                cmd.Parameters.AddWithValue("@contact_name", DBNull.Value);
                                cmd.Parameters.AddWithValue("@contact_otchestvo", DBNull.Value);
                                cmd.Parameters.AddWithValue("@user_login", userName);

                                string user_nameingrid = String.Concat(tbFamiliya.Text, " ", tbName.Text);
                                if (!String.IsNullOrWhiteSpace(tbOtchestvo.Text)) user_nameingrid = String.Concat(user_nameingrid, " ", tbOtchestvo.Text);
                                cmd.Parameters.AddWithValue("@user_nameingrid", user_nameingrid);
                                break;

                            case 1: // ЮЛ
                                cmd.Parameters.AddWithValue("@fl_familiya", DBNull.Value);
                                cmd.Parameters.AddWithValue("@fl_name", DBNull.Value);
                                cmd.Parameters.AddWithValue("@fl_otchestvo", DBNull.Value);                                
                                cmd.Parameters.AddWithValue("@telefon", tbPhone.Text);
                                cmd.Parameters.AddWithValue("@email", emailText);
                                cmd.Parameters.AddWithValue("@yl_fullname", tbYLFullName.Text);
                                cmd.Parameters.AddWithValue("@yl_shortname", tbYLShortName.Text);
                                cmd.Parameters.AddWithValue("@inn", tbINN.Text);
                                cmd.Parameters.AddWithValue("@contact_familiya", tbContactFamiliya.Text);
                                cmd.Parameters.AddWithValue("@contact_name", tbContactName.Text);
                                cmd.Parameters.AddWithValue("@contact_otchestvo", tbContactOtchestvo.Text);
                                cmd.Parameters.AddWithValue("@user_login", userName);

                                cmd.Parameters.AddWithValue("@user_nameingrid", tbYLShortName.Text);
                                break;

                            case 2: // ИП                
                                cmd.Parameters.AddWithValue("@fl_familiya", tbFamiliya.Text);
                                cmd.Parameters.AddWithValue("@fl_name", tbName.Text);
                                cmd.Parameters.AddWithValue("@fl_otchestvo", tbOtchestvo.Text);                                
                                cmd.Parameters.AddWithValue("@telefon", tbPhone.Text);
                                cmd.Parameters.AddWithValue("@email", emailText);
                                cmd.Parameters.AddWithValue("@yl_fullname", DBNull.Value);
                                cmd.Parameters.AddWithValue("@yl_shortname", DBNull.Value);
                                cmd.Parameters.AddWithValue("@inn", tbINN.Text);
                                cmd.Parameters.AddWithValue("@contact_familiya", tbContactFamiliya.Text);
                                cmd.Parameters.AddWithValue("@contact_name", tbContactName.Text);
                                cmd.Parameters.AddWithValue("@contact_otchestvo", tbContactOtchestvo.Text);
                                cmd.Parameters.AddWithValue("@user_login", userName);

                                user_nameingrid = String.Concat(tbFamiliya.Text, " ", tbName.Text);
                                if (!String.IsNullOrWhiteSpace(tbOtchestvo.Text)) user_nameingrid = String.Concat(user_nameingrid, " ", tbOtchestvo.Text);
                                cmd.Parameters.AddWithValue("@user_nameingrid", user_nameingrid);
                                break;
                        }

                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();

                        adapter.InsertCommand = cmd;
                        adapter.InsertCommand.ExecuteNonQuery();

                        connection.Close();
                        //-----------------------------------------------

                        /*// отсылаем сообщение пользователю для подтверждения регистрации
                        try
                        {
                            MailMessage mail = new MailMessage();

                            //mail.From = new MailAddress("admin@oke38.ru", "ОГУЭП \"Облкоммунэнерго\"");
                            mail.From = new MailAddress("admin@oke38.ru", "ОГУЭП \"Облкоммунэнерго\"", Encoding.GetEncoding(1251));
                            mail.To.Add(new MailAddress(tbEmail.Text, tbEmail.Text));
                            //mail.To.Add(new MailAddress("it@oblkomenergo.ru", "it@oblkomenergo.ru")); // копия письма на ящик администратора

                            mail.Subject = "Регистрация в личном кабинете ОГУЭП \"Облкоммунэнерго\"";
                            mail.IsBodyHtml = true;

                            mail.Body = "<div><p>Добрый день!</p><p>Осуществлена регистрация в личном кабинете ОГУЭП \"Облкоммунэнерго\"</p></div>";
                            mail.Body += String.Format("<div><p>Логин:</p><p>{0}</p></div>", tbEmail.Text) + " Код подтверждения: " + confirmCodeString;

                            SmtpClient smtp = new SmtpClient("mail.nic.ru", 2525);
                            smtp.Credentials = new NetworkCredential("admin@oke38.ru", "77pHmX5TgPZ3c");

                            smtp.Send(mail);
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(this, "Error sending a report.\n" + ex.ToString());
                        }*/
                        //----------------

                        // отправляем письмо
                        string urlConfirmEmail = "https://www.oke38.ru/portaltp/Account/RegisterConfirmEmail?email=" + emailText + "&confirmcode=" + confirmCodeString;
                        try
                        {
                            MailMessage mail = new MailMessage();
                            
                            //mail.From = new MailAddress("admin@oke38.ru", "ОГУЭП \"Облкоммунэнерго\"", Encoding.GetEncoding(1251));
                            mail.From = new MailAddress("admin@oke38.ru", "OKE38.RU");
                            mail.To.Add(new MailAddress(emailText, emailText));
                            //mail.To.Add(new MailAddress("admin@oke38.ru", "admin@oke38.ru")); // копия письма на ящик администратора

                            mail.Subject = "Регистрация в личном кабинете ОГУЭП \"Облкоммунэнерго\"";
                            mail.IsBodyHtml = true;

                            mail.Body = "<p>Осуществлена регистрация в личном кабинете ОГУЭП \"Облкоммунэнерго\"</p>";
                            mail.Body += String.Format("<p>Логин:</p><p>{0}</p>", emailText);
                            mail.Body += "<p>Для завершения процедуры регистрации необходимо подтвердить адрес электронной почты:</p>";
                            mail.Body += String.Format("<p><a href=\"{0}\">подтвердить адрес</a></p>", urlConfirmEmail);
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

                        //--------------------------------------------------------------                                        
                        //Response.Redirect("~/Account/RegisterConfirmEmail.aspx?email=" + tbEmail.Text);
                        Response.Redirect("~/Account/ShowAccountMessage?msg=MsgConfirmCodeEmailSend");

                    } // if (userName.Contains(".ua"))
                }
                catch (MembershipCreateUserException exc)
                {
                    if (exc.StatusCode == MembershipCreateStatus.DuplicateEmail || exc.StatusCode == MembershipCreateStatus.InvalidEmail)
                    {
                        tbEmail.ErrorText = exc.Message;
                        tbEmail.IsValid = false;
                    }
                    else if (exc.StatusCode == MembershipCreateStatus.InvalidPassword)
                    {
                        tbPassword.ErrorText = exc.Message;
                        tbPassword.IsValid = false;
                    }
                    else
                    {
                        tbEmail.ErrorText = exc.Message;
                        tbEmail.IsValid = false;
                    }
                }

            } // if (pcMaintenanceMode)
        } // protected void btnCreateUser_Click(object sender, EventArgs e)

        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            int cbIndex = Convert.ToInt32(e.Parameter);

            lblFamiliya.Visible = lblName.Visible = lblOtchestvo.Visible = cbIndex != 1;
            tbFamiliya.Visible = tbName.Visible = tbOtchestvo.Visible = cbIndex != 1;
            
            lblFullName.Visible = lblShortName.Visible = cbIndex == 1;
            tbYLFullName.Visible = tbYLShortName.Visible = cbIndex == 1;

            lblINN.Visible = lblContactGroupCaption.Visible = lblContactFamiliya.Visible = lblContactName.Visible = lblContactOtchestvo.Visible = cbIndex != 0;
            tbINN.Visible = tbContactFamiliya.Visible = tbContactName.Visible = tbContactOtchestvo.Visible = cbIndex != 0;

            if (cbIndex == 1) // ЮЛ
            {
                tbINN.MaskSettings.Mask = "0000000000";
            }
            else if (cbIndex == 2) // ИП
            {
                tbINN.MaskSettings.Mask = "000000000000";
            }
        } // protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)

    }
}