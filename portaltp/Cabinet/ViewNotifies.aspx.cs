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
    public partial class ViewNotifies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ASPxGridViewNotifiesAll.FocusedRowIndex = -1;
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
                    ASPxGridViewNotifiesAll.Columns["Действия"].Visible = true;
                    ASPxGridViewNotifiesAll.SettingsDataSecurity.AllowDelete = true;

                    this.SqlDataSourceViewNotifies.SelectCommand =
                        String.Concat(
                            "SELECT id_notify, tN.id_notifytype, date_create_notify, recipient_id, notifystatus_id, notify_text,",
                            " priority, date_send_notify, tN.id_notifychannel, tN.email, tel, date_plan_notify, link_id, tN.comment,",
                            " user_nameingrid, caption_notifychannel, caption_notifystatus, caption_notifytype, tZ.id_zayavka, v1C_Zayavitel,",
                            " status_GP_RegNewZ, status_GP_RegNewDTP, status_PrikreplenAktDopuskaPU, status_PrikreplenAktTP, status_ZAnnulirovana,",
                            " nomer_dogovorenergo",
                            " FROM tblNotify tN",
                            " INNER JOIN tblZayavka tZ ON tN.link_id = tZ.id_zayavka",
                            " LEFT JOIN tblGP_DogovorEnergo tGPDE ON tZ.id_zayavka = tGPDE.id_zayavka",
                            " LEFT JOIN tblUserInfo tUI ON tN.recipient_id = tUI.id_user",
                            " LEFT JOIN tblNotifyChannel tNC ON tN.id_notifychannel = tNC.id_notifychannel",
                            " LEFT JOIN tblNotifyStatus tNS ON tN.notifystatus_id = tNS.id_notifystatus",
                            " LEFT JOIN tblNotifyType tNT ON tN.id_notifytype = tNT.id_notifytype",

                            " UNION ",

                            "SELECT id_notify, tN.id_notifytype, date_create_notify, recipient_id, notifystatus_id, notify_text,",
                            " priority, date_send_notify, tN.id_notifychannel, tN.email, tel, date_plan_notify, link_id, tN.comment,",
                            " user_nameingrid, caption_notifychannel, caption_notifystatus, caption_notifytype, tZ.id_zayavka, v1C_Zayavitel,",
                            " status_GP_RegNewZ, status_GP_RegNewDTP, status_PrikreplenAktDopuskaPU, status_PrikreplenAktTP, status_ZAnnulirovana,",
                            " nomer_dogovorenergo",
                            " FROM tblNotify tN",
                            " INNER JOIN tblZayavkaChat tZC ON tN.link_id = tZC.id_chatrec",
                            " LEFT JOIN tblZayavka tZ ON tZC.id_zayavka = tZ.id_zayavka",
                            " LEFT JOIN tblGP_DogovorEnergo tGPDE ON tZ.id_zayavka = tGPDE.id_zayavka",
                            " LEFT JOIN tblUserInfo tUI ON tN.recipient_id = tUI.id_user",
                            " LEFT JOIN tblNotifyChannel tNC ON tN.id_notifychannel = tNC.id_notifychannel",
                            " LEFT JOIN tblNotifyStatus tNS ON tN.notifystatus_id = tNS.id_notifystatus",
                            " LEFT JOIN tblNotifyType tNT ON tN.id_notifytype = tNT.id_notifytype",

                            " UNION ",

                            "SELECT id_notify, tN.id_notifytype, date_create_notify, recipient_id, notifystatus_id, notify_text,",
                            " priority, date_send_notify, tN.id_notifychannel, tN.email, tel, date_plan_notify, link_id, tN.comment,",
                            " user_nameingrid, caption_notifychannel, caption_notifystatus, caption_notifytype, NULL AS id_zayavka, NULL AS v1C_Zayavitel,",
                            " NULL AS status_GP_RegNewZ, NULL AS status_GP_RegNewDTP, NULL AS status_PrikreplenAktDopuskaPU, NULL AS status_PrikreplenAktTP, NULL AS status_ZAnnulirovana,",
                            " NULL AS nomer_dogovorenergo",
                            " FROM tblNotify tN",
                            " INNER JOIN tblUserInfo tUI ON tN.link_id = tUI.id_user",
                            " LEFT JOIN tblNotifyChannel tNC ON tN.id_notifychannel = tNC.id_notifychannel",
                            " LEFT JOIN tblNotifyStatus tNS ON tN.notifystatus_id = tNS.id_notifystatus",
                            " LEFT JOIN tblNotifyType tNT ON tN.id_notifytype = tNT.id_notifytype",

                            " ORDER BY date_create_notify DESC");

                } // if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
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

        // удаление уведомления
        protected void ASPxGridViewNotifiesAll_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Guid id_notify = Guid.Parse(e.Keys[ASPxGridViewNotifiesAll.KeyFieldName].ToString());

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();

            perscabnewEntitiesAdapter.tblNotify.Remove(perscabnewEntitiesAdapter.tblNotify.Where(p => p.id_notify == id_notify).Select(p => p).First());

            perscabnewEntitiesAdapter.SaveChanges();

            e.Cancel = true;
        }

        // выбор пункта меню
        protected void ASPxMenuViewNotifies_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Export2Excel":
                    ASPxGridViewExporterNotifies.FileName = "Все уведомления";
                    ASPxGridViewExporterNotifies.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.DataAware });
                    break;

                case "menuItemCheckNotifications":
                    perscabnewEntities lkAdapter = new perscabnewEntities();
                    string logInfo = String.Format("Всего уведомлений в БД ЛК: {0}", lkAdapter.tblNotify.Select(p => p).Count());
                    PlaceHolderInfo.Visible = true;
                    PlaceHolderInfoText.Text = logInfo;
                    break;
            }
        }

        // раскраска строк в зависимости от значений полей и пр.
        protected void ASPxGridViewNotifiesAll_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != GridViewRowType.Data) return;

            string caption_notifystatus = e.GetValue("caption_notifystatus").ToString();

            if (String.Equals(caption_notifystatus, "в очереди"))
            {
                e.Row.BackColor = Color.Pink;
                e.Row.ForeColor = Color.Black;
            }

            if (String.Equals(caption_notifystatus, "отправлено"))
            {
                e.Row.BackColor = Color.LightGreen;
                e.Row.ForeColor = Color.Black;
            }
        }

        // callback - переключатель "все"/"уведомления ГП"/"в очереди"
        protected void CallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (RadioButton_NotifiesForGP.Checked)
            {
                //ASPxGridViewNotifiesAll.FilterExpression = "[caption_filial_short] = NULL OR [caption_filial_short] = ''";
            }
            else
            if (RadioButton_NotifiesNotSend.Checked)
            {
                ASPxGridViewNotifiesAll.FilterExpression = "[caption_notifystatus] = 'в очереди'";
            }
            else
            if (RadioButton_NotifiesAll.Checked)
            {
                ASPxGridViewNotifiesAll.FilterExpression = String.Empty;
            }
        }

        // кнопка "повторить" уведомление
        protected void ButtonNotifyRepeat_Click(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((BootstrapButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            Guid src_id_notify = Guid.Parse(container.Grid.GetRowValues(currentIndex, "id_notify").ToString());

            perscabnewEntities lkAdapter = new perscabnewEntities();

            tblNotify srcNotify = lkAdapter.tblNotify.Where(p => p.id_notify == src_id_notify).Select(p => p).First();
            tblNotify destNotify = new tblNotify();

            destNotify.id_notify = Guid.NewGuid();
            destNotify.id_notifytype = srcNotify.id_notifytype;
            destNotify.date_create_notify = DateTime.Now;
            destNotify.notifystatus_id = lkAdapter.tblNotifyStatus.Where(p => p.caption_notifystatus == "в очереди").Select(p => p).First().id_notifystatus;
            destNotify.recipient_id = srcNotify.recipient_id;
            destNotify.notify_text = srcNotify.notify_text;
            destNotify.priority = srcNotify.priority;
            destNotify.id_notifychannel = srcNotify.id_notifychannel;
            destNotify.email = srcNotify.email;
            destNotify.tel = srcNotify.tel;
            destNotify.date_plan_notify = null;
            destNotify.link_id = srcNotify.link_id;
            destNotify.date_send_notify = null;

            lkAdapter.tblNotify.Add(destNotify);
            lkAdapter.SaveChanges();

            ASPxGridViewNotifiesAll.DataBind();
        } // protected void ButtonNotifyRepeat_Click(object sender, EventArgs e)

    }
}