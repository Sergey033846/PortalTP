using DatabaseFirst;
using mcperscab;

using DevExpress.Export;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp.Cabinet
{
    public partial class AdminUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ASPxGridAdminUsers.FocusedRowIndex = -1;
            }

            string user_login = Page.User.Identity.Name;

            if (Page.User.Identity.IsAuthenticated)
            {
                int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);

                perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
                tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

                // видимость операций и компонентов администратора
                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
                {                    
                    ASPxGridAdminUsers.Columns["Действия"].Visible = true;
                    ASPxGridAdminUsers.SettingsDataSecurity.AllowDelete = true;

                    this.SqlDataSourceAdminUsers.SelectCommand =
                        String.Concat(
                            "SELECT",
                            " id_user, date_create_user, user_nameingrid, tUT.caption_usertype, user_login, comment,",
                            " IsApproved, IsLockedOut,",
                            " (SELECT TOP(1) tL.logrec_datetime FROM tblLog tL WHERE tL.id_user = tUI.id_user AND tL.id_logeventtype = 1 ORDER BY tL.logrec_datetime DESC) as date_last_login,",
                            " (SELECT COUNT(*) FROM tblZayavka tZ WHERE tZ.id_user = tUI.id_user AND tZ.is_temp IS NULL) AS countZ",
                            " FROM tblUserInfo tUI",
                            " LEFT JOIN tblUserType tUT ON tUI.id_usertype = tUT.id_usertype",                            
                            " ORDER BY date_create_user desc");
                }
                else
                {                    
                    Response.Redirect("~", true);
                }

            } // if if (Page.User.Identity.IsAuthenticated)
            else
            {
                Response.Redirect("~" ,true);
            }
        } // protected void Page_Load(object sender, EventArgs e)

        // удаление пользователя и всех связанных с ним данных
        protected void ASPxGridAdminUsers_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Guid id_user = Guid.Parse(e.Keys[ASPxGridAdminUsers.KeyFieldName].ToString());
            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.id_user == id_user).Select(p => p).First();

            // начало------------ удаляем информацию из Личного кабинета ------------------

            // удаляем лог
            perscabnewEntitiesAdapter.tblLog.RemoveRange(perscabnewEntitiesAdapter.tblLog.Where(p => p.id_user == id_user).Select(p => p));

            // удаляем уведомления по пользователю
            perscabnewEntitiesAdapter.tblNotify.RemoveRange(perscabnewEntitiesAdapter.tblNotify.Where(p => p.link_id == id_user).Select(p => p));

            // удаляем заявки пользователя
            IQueryable<tblZayavka> temp_tblZayavkaList = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_user == id_user).Select(p => p);
            
            foreach (tblZayavka tempZayavka in temp_tblZayavkaList)
            {
                Guid id_zayavka = tempZayavka.id_zayavka;

                // удаляем уведомления по заявке
                perscabnewEntitiesAdapter.tblNotify.RemoveRange(perscabnewEntitiesAdapter.tblNotify.Where(p => p.link_id == id_zayavka).Select(p => p));

                // удаляем информацию, связанную с документами заявки
                IQueryable<tblZayavkaDoc> temp_tblZayavkaDoc = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => p.id_zayavka == id_zayavka).Select(p => p);
                foreach (tblZayavkaDoc tempZayavkaDoc in temp_tblZayavkaDoc)
                {
                    perscabnewEntitiesAdapter.tblESignDoc.RemoveRange(perscabnewEntitiesAdapter.tblESignDoc.Where(p => p.id_zayavkadoc == tempZayavkaDoc.id_zayavkadoc).Select(p => p));
                }
                perscabnewEntitiesAdapter.tblZayavkaDoc.RemoveRange(temp_tblZayavkaDoc);
                                
                // удаляем информацию о заключенных договорах
                IQueryable<tblContract> temp_tblContract = perscabnewEntitiesAdapter.tblContract.Where(p => p.id_zayavka == id_zayavka).Select(p => p);
                foreach (tblContract tempContract in temp_tblContract)
                {
                    perscabnewEntitiesAdapter.tblContractDoc.RemoveRange(perscabnewEntitiesAdapter.tblContractDoc.Where(p => p.id_contract == tempContract.id_contract).Select(p => p));                    
                }
                perscabnewEntitiesAdapter.tblContract.RemoveRange(temp_tblContract);

                // удаляем чат
                perscabnewEntitiesAdapter.tblZayavkaChat.RemoveRange(perscabnewEntitiesAdapter.tblZayavkaChat.Where(p => p.id_zayavka == id_zayavka).Select(p => p));

                // удаляем саму заявку
                perscabnewEntitiesAdapter.tblZayavka.Remove(perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == id_zayavka).Select(p => p).First());
            } // foreach (tblZayavka tempZayavka in temp_tblZayavkaList)

            // удаляем самого пользователя
            perscabnewEntitiesAdapter.tblUserInfo.Remove(userInfo);

            perscabnewEntitiesAdapter.SaveChanges();

            // конец------------ удаляем информацию из Личного кабинета -------------------

            //-----------------------------------------------------------------------------

            // начало------------ удаляем информацию из IdentityMembership ----------------
            if (Membership.GetUser(userInfo.user_login) != null) Membership.DeleteUser(userInfo.user_login, true);

            e.Cancel = true;
        } // protected void ASPxGridAdminUsers_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)

        // выбор пункта меню
        protected void ASPxMenuAdminUsers_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Export2Excel":
                    ASPxGridViewExporterOrders.FileName = "Все заявки";
                    ASPxGridViewExporterOrders.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.DataAware });
                    break;

                case "menuItemLoadUsersFromIdentityMembership":
                    perscabnewEntities lkAdapter = new perscabnewEntities();
                    IQueryable<tblUserInfo> userInfoList = lkAdapter.tblUserInfo.Select(p => p);
                    MembershipUserCollection userMembershipCollection = Membership.GetAllUsers();
                    
                    string logCompareStr = String.Format("Кол-во записей: БД ЛК = {0}; IdentityMembership = {1}", userInfoList.Count(), userMembershipCollection.Count);

                    logCompareStr += String.Concat("<br/><br/>", "Отсутствуют в IdentityMembership:");
                    foreach (tblUserInfo userInfoTemp in userInfoList)
                    {
                        MembershipUser membershipUser = Membership.GetUser(userInfoTemp.user_login);
                        if (membershipUser != null)
                        {
                            userInfoTemp.IsApproved = membershipUser.IsApproved;
                            userInfoTemp.IsLockedOut = membershipUser.IsLockedOut;                    
                        }
                        else
                        {
                            logCompareStr += String.Concat("<br/>", userInfoTemp.user_login);
                        }
                    }

                    lkAdapter.SaveChanges();

                    //--------------
                    logCompareStr += String.Concat("<br/><br/>", "Отсутствуют в БД ЛК:");
                                        
                    foreach (MembershipUser userMembership in userMembershipCollection)
                    {
                        if (lkAdapter.tblUserInfo.Where(p => p.user_login == userMembership.UserName).Select(p => p).Count() == 0)                        
                        {                         
                            logCompareStr += String.Concat("<br/>", String.Format("{0}; IsApproved={1}; LastActivityDate={2}", userMembership.UserName, userMembership.IsApproved, userMembership.LastActivityDate));
                        }
                    }

                    PlaceHolderInfo.Visible = true;
                    PlaceHolderInfoText.Text = logCompareStr;
                    break;

                case "menuItemCompareUsersWithIdentityMembership":
                    lkAdapter = new perscabnewEntities();
                    userInfoList = lkAdapter.tblUserInfo.Select(p => p);
                    userMembershipCollection = Membership.GetAllUsers();

                    logCompareStr = String.Format("Кол-во записей: БД ЛК = {0}; IdentityMembership = {1}", userInfoList.Count(), userMembershipCollection.Count);

                    logCompareStr += String.Concat("<br/><br/>", "Отсутствуют в IdentityMembership:");
                    foreach (tblUserInfo userInfoTemp in userInfoList)
                    {
                        MembershipUser membershipUser = Membership.GetUser(userInfoTemp.user_login);
                        if (membershipUser == null)
                        {                            
                            logCompareStr += String.Concat("<br/>", userInfoTemp.user_login);
                        }
                    }

                    //--------------                    
                    logCompareStr += String.Concat("<br/><br/>", "Отсутствуют в БД ЛК:");

                    foreach (MembershipUser userMembership in userMembershipCollection)
                    {
                        if (lkAdapter.tblUserInfo.Where(p => p.user_login == userMembership.UserName).Select(p => p).Count() == 0)
                        {
                            logCompareStr += String.Concat("<br/>", String.Format("{0}; IsApproved={1}; LastActivityDate={2}", userMembership.UserName, userMembership.IsApproved, userMembership.LastActivityDate));
                        }
                    }

                    PlaceHolderInfo.Visible = true;
                    PlaceHolderInfoText.Text = logCompareStr;
                    break;
            }
        }

        // раскраска строк в зависимости от значений полей и пр.
        protected void ASPxGridAdminUsers_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            /*if (e.RowType != GridViewRowType.Data) return;

            string value_caption_zayavkastatus = e.GetValue("caption_zayavkastatus").ToString();
            bool is_status_equals_cancelled = value_caption_zayavkastatus.Equals("аннулированная");

            // не присвоен номер заявки
            string value_zayavka_number_1c = e.GetValue("zayavka_number_1c").ToString();
            if (String.IsNullOrWhiteSpace(value_zayavka_number_1c) && !is_status_equals_cancelled)
            {
                e.Row.BackColor = Color.LightSalmon;
                e.Row.ForeColor = Color.Black;
            }

            // не закреплен филиал
            string value_caption_filial_short = e.GetValue("caption_filial_short").ToString();
            if (String.IsNullOrWhiteSpace(value_caption_filial_short) && !is_status_equals_cancelled)
            {
                e.Row.BackColor = Color.Pink;
                e.Row.ForeColor = Color.Black;
            }*/
        }
        
        // формирование кнопки "Подтвержден"
        protected void ButtonSetApproved_Init(object sender, EventArgs e)
        {            
            GridViewDataItemTemplateContainer container = ((BootstrapButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string IsApproved_str = container.Grid.GetRowValues(currentIndex, "IsApproved").ToString();

            bool IsApproved = false;

            if (!String.IsNullOrEmpty(IsApproved_str))
            {
                IsApproved = Convert.ToBoolean(IsApproved_str);
            }
            else
            {
                string user_login = container.Grid.GetRowValues(currentIndex, "user_login").ToString();
                MembershipUser membershipUser = Membership.GetUser(user_login);
                if (membershipUser != null)
                {
                    IsApproved = membershipUser.IsApproved;
                }
            }

            if (IsApproved)
            {
                ((BootstrapButton)sender).CssClass = "glyphicon glyphicon-user mcbuttoningrid-green";
                ((BootstrapButton)sender).ToolTip = "отменить подтверждение";
            }
            else
            {
                ((BootstrapButton)sender).CssClass = "glyphicon glyphicon-user mcbuttoningrid-red";
                ((BootstrapButton)sender).ToolTip = "подтвердить";
            }
        } // protected void ButtonSetApproved_Init(object sender, EventArgs e)

        // формирование кнопки "Блокировка"
        protected void ButtonSetLockedOut_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((BootstrapButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string IsLockedOut_str = container.Grid.GetRowValues(currentIndex, "IsLockedOut").ToString();            

            bool IsLockedOut = false;

            if (!String.IsNullOrEmpty(IsLockedOut_str))
            {
                IsLockedOut = Convert.ToBoolean(IsLockedOut_str);
            }
            else
            {
                string user_login = container.Grid.GetRowValues(currentIndex, "user_login").ToString();
                MembershipUser membershipUser = Membership.GetUser(user_login);
                if (membershipUser != null)
                {                    
                    IsLockedOut = membershipUser.IsLockedOut;                    
                }
            }

            if (IsLockedOut)
            {
                ((BootstrapButton)sender).CssClass = "glyphicon glyphicon-ban-circle mcbuttoningrid-red";
                ((BootstrapButton)sender).ToolTip = "разблокировать";
            }
            else
            {
                ((BootstrapButton)sender).CssClass = "glyphicon glyphicon-ban-circle mcbuttoningrid-green";
                ((BootstrapButton)sender).ToolTip = "";
            }
        } // protected void ButtonSetLockedOut_Init(object sender, EventArgs e)

        // событие кнопки "Подтвержден"
        protected void ButtonSetApproved_Click(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((BootstrapButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string user_login = container.Grid.GetRowValues(currentIndex, "user_login").ToString();

            perscabnewEntities lkAdapter = new perscabnewEntities();
            tblUserInfo userInfo = lkAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

            MembershipUser membershipUser = Membership.GetUser(user_login);
            bool IsApproved = membershipUser.IsApproved;
            
            membershipUser.IsApproved = !IsApproved;
            Membership.UpdateUser(membershipUser);
            userInfo.IsApproved = membershipUser.IsApproved;
            
            lkAdapter.SaveChanges();
            ASPxGridAdminUsers.DataBind();
        } // protected void ButtonSetApproved_Click(object sender, EventArgs e)

        // событие кнопки "Блокировка"
        protected void ButtonSetLockedOut_Click(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((BootstrapButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string user_login = container.Grid.GetRowValues(currentIndex, "user_login").ToString();

            perscabnewEntities lkAdapter = new perscabnewEntities();            
            tblUserInfo userInfo = lkAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();
                        
            MembershipUser membershipUser = Membership.GetUser(user_login);
            
            membershipUser.UnlockUser();                
            userInfo.IsLockedOut = membershipUser.IsLockedOut;
            
            lkAdapter.SaveChanges();
            ASPxGridAdminUsers.DataBind();

        } // protected void ButtonSetLockedOut_Click(object sender, EventArgs e)

        // callback - переключатель "все"/"неподтвержденные"/"заблокированные"
        protected void CallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (RadioButton_UsersNotApproved.Checked)
            {
                ASPxGridAdminUsers.FilterExpression = "[IsApproved] = false";
            }
            else
            if (RadioButton_UsersLockedOut.Checked)
            {
                ASPxGridAdminUsers.FilterExpression = "[IsLockedOut] = true";
            }
            else
            if (RadioButton_UsersAll.Checked)
            {
                ASPxGridAdminUsers.FilterExpression = String.Empty;
            }
        }

    } // public partial class AdminUsers : System.Web.UI.Page
}