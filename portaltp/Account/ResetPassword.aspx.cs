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
    public partial class ResetPassword : Page
    {
        protected string StatusMessage
        {
            get;
            private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count == 2)
            {
                string emailUser = Request.QueryString["email"].ToString();
                string confirmCodeString = Request.QueryString["confirmcode"].ToString();

                perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
                try
                {
                    tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.email == emailUser).Select(p => p).First();

                    if (userInfo.confirm_code == confirmCodeString)
                    {
                        this.Email.Text = emailUser;
                        this.Password.Focus();

                        PlaceHolderNewPassword.Visible = true;
                    }
                    else
                    {
                        FailureText.Text = "Неверный код подтверждения.";
                        PlaceHolderErrorMessage.Visible = true;                        
                    }
                    
                }
                catch (InvalidOperationException)
                {
                    FailureText.Text = "Ошибка данных.";
                    PlaceHolderErrorMessage.Visible = true;
                }
                catch (NullReferenceException ex)
                {
                    FailureText.Text = ex.Message;
                    PlaceHolderErrorMessage.Visible = true;
                }
            }
            else
            {
                Response.Redirect("~/");
            }
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            /* исходный код шаблона
            string code = IdentityHelper.GetCodeFromRequest(Request);
            if (code != null)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

                var user = manager.FindByName(Email.Text);
                if (user == null)
                {
                    FailureText.Text = "Пользователь не найден";
                    return;
                }
                var result = manager.ResetPassword(user.Id, code, Password.Text);
                if (result.Succeeded)
                {
                    Response.Redirect("~/Account/ResetPasswordConfirmation");
                    return;
                }
                FailureText.Text = result.Errors.FirstOrDefault();
                return;
            }

            FailureText.Text = "Произошла ошибка";*/
            //---------------------------------------------

            if (IsValid)
            {
                try
                {
                    string userName = Email.Text;
                    MembershipUser userOld = Membership.GetUser(userName);                    

                    if (userOld != null)
                    {
                        userOld.IsApproved = true;
                        Membership.UpdateUser(userOld);
                        string resetPassword = userOld.ResetPassword();
                        if (userOld.ChangePassword(resetPassword, Password.Text))
                        {
                            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
                            try
                            {
                                tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == Email.Text).Select(p => p).First();
                                                                
                                userInfo.confirm_code = null;
                                userInfo.password_text = Password.Text;
                                perscabnewEntitiesAdapter.SaveChanges();

                                MC_PersCab_Logging.MC_PersCab_Logging_LogWrite(userInfo.user_login, null, MC_PersCab_Logging.MC_PersCab_LogEventType.MC_LET_RESETPASSWORD, null);
                            }
                            catch (InvalidOperationException)
                            {
                                FailureText.Text = "Ошибка данных.";
                                PlaceHolderErrorMessage.Visible = true;
                                return;
                            }

                            Response.Redirect("~/Account/ResetPasswordConfirmation");
                        }
                        else
                        {
                            FailureText.Text = "Ошибка изменения пароля.";
                            PlaceHolderErrorMessage.Visible = true;
                            return;
                        }

                    }
                    else
                    {
                        FailureText.Text = "Пользователь не найден.";
                        PlaceHolderErrorMessage.Visible = true;
                        return;
                    }
                }
                catch (ArgumentException exc)
                {
                    FailureText.Text = exc.Message;
                    PlaceHolderErrorMessage.Visible = true;
                    return;
                }
                catch (MembershipCreateUserException exc)
                {
                    FailureText.Text = exc.Message;
                    PlaceHolderErrorMessage.Visible = true;
                    return;
                }
            } // if (IsValid)

        }
    }
}