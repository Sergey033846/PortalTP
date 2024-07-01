using DatabaseFirst;
using mcperscab;
using DevExpress.Web;
using DevExpress.Export;
using DevExpress.XtraPrinting;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp.Cabinet
{
    public partial class Chat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            if (!IsPostBack)
            {
                ASPxGridViewChat.FocusedRowIndex = -1;
            }

            if (Page.User.Identity.IsAuthenticated)
            {
                int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);

                perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
                tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

                // видимость операций и компонентов администратора
                RadioButton_ChatIsTemp.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN); // видимость черновиков для администратора

                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
                {
                    //ASPxGridViewChat.Columns["Идентификатор заявки"].Visible = false; // не влазит
                    ASPxGridViewChat.Columns["Действия"].Visible = true;
                    ASPxGridViewChat.SettingsDataSecurity.AllowDelete = true;
                }
                else
                {
                    //ASPxGridViewChat.Columns["Идентификатор заявки"].Visible = false;
                    ASPxGridViewChat.Columns["Действия"].Visible = false;
                    ASPxGridViewChat.SettingsDataSecurity.AllowDelete = false;
                }

                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
                {
                    // переписка по заявке
                    StringBuilder scChatInfo = new StringBuilder("SELECT id_chatrec, tblZC.id_zayavka as id_zayavka, chatrec_datetime, caption_filial_short,", 2000);
                    scChatInfo.Append("(SELECT user_nameingrid FROM tblZayavka tblZ LEFT JOIN tblUserInfo tblUI ON tblZ.id_user = tblUI.id_user WHERE tblZ.id_zayavka = tblZC.id_zayavka) as user_nameingrid,");
                    scChatInfo.Append("(SELECT caption_userrole FROM tblUserInfo tblUI LEFT JOIN tblUserRole tblUR ON tblUI.id_userrole = tblUR.id_userrole WHERE tblUI.id_user = tblZC.id_user) as caption_userrole,");
                    scChatInfo.Append("zayavka_number_1c, zayavka_date_1c, caption_msg, tblZ.v1C_Zayavitel, tblZC.istemp, ");
                    scChatInfo.AppendFormat("(SELECT COUNT(*) FROM tblZayavkaDoc tblZD WHERE tblZD.id_chatrec = tblZC.id_chatrec) as count_docs ");
                    scChatInfo.Append("FROM tblZayavkaChat tblZC ");
                    scChatInfo.Append("LEFT JOIN tblUserInfo tblUI ON tblZC.id_user = tblUI.id_user ");
                    scChatInfo.Append("LEFT JOIN tblZayavka tblZ ON tblZC.id_zayavka = tblZ.id_zayavka ");
                    scChatInfo.Append("LEFT JOIN tblFilial tblF ON tblUI.id_filial = tblF.id_filial ");
                    //scChatInfo.Append("WHERE tblZC.istemp IS NULL ");
                    scChatInfo.Append("ORDER BY chatrec_datetime DESC");
                    this.SqlDataSourceChatInfo.SelectCommand = scChatInfo.ToString();

                    this.SqlDataSourceChatDocsInfo.SelectCommand =
                        String.Concat("SELECT * ",
                                      "FROM tblZayavkaDoc ",
                                      "WHERE id_chatrec = CAST(@id_chatrecS as uniqueidentifier) AND is_temp IS NULL",
                                      " ORDER BY date_doc_add DESC");

                    //ASPxGridViewChat.Columns["user_nameingrid"].Visible = true;
                    //ASPxGridViewChat.Columns["caption_userrole"].Caption = "Роль";
                }
                else if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER)
                {
                    // переписка по заявке
                    StringBuilder scChatInfo = new StringBuilder("SELECT id_chatrec, tblZC.id_zayavka as id_zayavka, chatrec_datetime, caption_filial_short,", 2000);
                    scChatInfo.Append("(SELECT user_nameingrid FROM tblZayavka tblZ LEFT JOIN tblUserInfo tblUI ON tblZ.id_user = tblUI.id_user WHERE tblZ.id_zayavka = tblZC.id_zayavka) as user_nameingrid,");
                    scChatInfo.Append("(SELECT caption_userrole FROM tblUserInfo tblUI LEFT JOIN tblUserRole tblUR ON tblUI.id_userrole = tblUR.id_userrole WHERE tblUI.id_user = tblZC.id_user) as caption_userrole,");
                    scChatInfo.Append("zayavka_number_1c, zayavka_date_1c, caption_msg, tblZ.v1C_Zayavitel, ");
                    scChatInfo.AppendFormat("(SELECT COUNT(*) FROM tblZayavkaDoc tblZD WHERE tblZD.id_chatrec = tblZC.id_chatrec) as count_docs ");
                    scChatInfo.Append("FROM tblZayavkaChat tblZC ");
                    scChatInfo.Append("LEFT JOIN tblUserInfo tblUI ON tblZC.id_user = tblUI.id_user ");
                    scChatInfo.Append("LEFT JOIN tblZayavka tblZ ON tblZC.id_zayavka = tblZ.id_zayavka ");
                    scChatInfo.Append("LEFT JOIN tblFilial tblF ON tblUI.id_filial = tblF.id_filial ");
                    scChatInfo.Append("WHERE tblZC.istemp IS NULL ");
                    scChatInfo.Append("ORDER BY chatrec_datetime DESC");
                    this.SqlDataSourceChatInfo.SelectCommand = scChatInfo.ToString();

                    this.SqlDataSourceChatDocsInfo.SelectCommand =
                        String.Concat("SELECT * ",
                                      "FROM tblZayavkaDoc ",
                                      "WHERE id_chatrec = CAST(@id_chatrecS as uniqueidentifier) AND is_temp IS NULL",
                                      " ORDER BY date_doc_add DESC");

                    //ASPxGridViewChat.Columns["user_nameingrid"].Visible = true;
                    //ASPxGridViewChat.Columns["caption_userrole"].Caption = "Роль";
                }
                else if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR)
                {
                    // переписка по заявке
                    StringBuilder scChatInfo = new StringBuilder("SELECT id_chatrec, tblZC.id_zayavka as id_zayavka, chatrec_datetime, caption_filial_short,", 2000);
                    scChatInfo.Append("(SELECT user_nameingrid FROM tblZayavka tblZ LEFT JOIN tblUserInfo tblUI ON tblZ.id_user = tblUI.id_user WHERE tblZ.id_zayavka = tblZC.id_zayavka) as user_nameingrid,");
                    scChatInfo.Append("(SELECT caption_userrole FROM tblUserInfo tblUI LEFT JOIN tblUserRole tblUR ON tblUI.id_userrole = tblUR.id_userrole WHERE tblUI.id_user = tblZC.id_user) as caption_userrole,");
                    scChatInfo.Append("zayavka_number_1c, zayavka_date_1c, caption_msg, tblZ.v1C_Zayavitel, ");
                    scChatInfo.AppendFormat("(SELECT COUNT(*) FROM tblZayavkaDoc tblZD WHERE tblZD.id_chatrec = tblZC.id_chatrec) as count_docs ");
                    scChatInfo.Append("FROM tblZayavkaChat tblZC ");
                    scChatInfo.Append("LEFT JOIN tblUserInfo tblUI ON tblZC.id_user = tblUI.id_user ");
                    scChatInfo.Append("LEFT JOIN tblZayavka tblZ ON tblZC.id_zayavka = tblZ.id_zayavka ");
                    scChatInfo.Append("LEFT JOIN tblFilial tblF ON tblUI.id_filial = tblF.id_filial ");
                    //scChatInfo.Append("WHERE tblZC.istemp IS NULL ");
                    scChatInfo.Append("WHERE (tblZC.istemp is NULL) AND (tblZ.id_filial = ");
                    scChatInfo.Append(userInfo.id_filial.ToString());
                    scChatInfo.Append(")");
                    scChatInfo.Append("ORDER BY chatrec_datetime DESC");
                    this.SqlDataSourceChatInfo.SelectCommand = scChatInfo.ToString();

                    this.SqlDataSourceChatDocsInfo.SelectCommand =
                        String.Concat("SELECT * ",
                                      "FROM tblZayavkaDoc ",
                                      "WHERE id_chatrec = CAST(@id_chatrecS as uniqueidentifier) AND is_temp IS NULL",
                                      " ORDER BY date_doc_add DESC");

                    //ASPxGridViewChat.Columns["user_nameingrid"].Visible = true;
                    //ASPxGridViewChat.Columns["caption_userrole"].Caption = "Роль";
                }
                else
                {
                    Response.Redirect("~");
                }
            } // if (Page.User.Identity.IsAuthenticated)
            else
            {
                Response.Redirect("~");
            }
        } // protected void Page_Load(object sender, EventArgs e)

        // callback - переключатель "все"/черновики
        protected void CallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {            
            if (RadioButton_ChatAll.Checked)
            {
                ASPxGridViewChat.FilterExpression = String.Empty;
            }            
            else
            if (RadioButton_ChatIsTemp.Checked)
            {
                ASPxGridViewChat.FilterExpression = "[istemp] = true";
            }
        }

        protected void ASPxMenuChat_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Export2Excel":
                    ASPxGridViewExporterChat.FileName = "Электронная переписка";
                    ASPxGridViewExporterChat.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.DataAware });
                    break;
            }
        }

        // используется для связки Master-Detail гридов   
        protected void ASPxGridViewChatDocs_BeforePerformDataSelect(object sender, EventArgs e)
        {
            Session["id_chatrecS"] = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        }

        /*// формирование поля "Вложения"
        protected void imgAttachment_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxImage)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            int countAttachment = Convert.ToInt32(container.Grid.GetRowValues(currentIndex, "count_docs"));

            if (countAttachment > 0)
            {
                ((ASPxImage)sender).ImageUrl = "~/Content/Images/attach.png";
            }
            else
            {                

            }
        }*/

        // формирование поля "Вложения"
        protected void imgAttachment_Init2(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxHyperLink)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            int countAttachment = Convert.ToInt32(container.Grid.GetRowValues(currentIndex, "count_docs"));
            
            ((ASPxHyperLink)sender).Visible = (countAttachment > 0);            
        }
                
        // формирование поля "посмотреть" документа заявки
        protected void urlViewOrderDoc_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxHyperLink)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string doc_id = container.Grid.GetRowValues(currentIndex, "id_zayavkadoc").ToString();

            ((ASPxHyperLink)sender).NavigateUrl = String.Concat("~/Cabinet/ViewDoc?doctype=order&docid=", doc_id);
        }

        // формирование поля "скачать" документа заявки
        protected void urlDownloadOrderDoc_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxHyperLink)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string doc_id = container.Grid.GetRowValues(currentIndex, "id_zayavkadoc").ToString();

            ((ASPxHyperLink)sender).NavigateUrl = String.Concat("~/Cabinet/DownloadDoc?doctype=order&docid=", doc_id);
        }

        // раскраска строк в зависимости от отправителя сообщения
        protected void ASPxGridViewChat_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != GridViewRowType.Data) return;
            string value = e.GetValue("caption_userrole").ToString();
            if (value.Equals("заявитель"))
            {
                e.Row.BackColor = Color.Wheat;
            }
        }

        // удаление сообщения и всех связанных с ним данных
        protected void ASPxGridViewChat_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Guid id_chatrec = Guid.Parse(e.Keys[ASPxGridViewChat.KeyFieldName].ToString());

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            
            // удаляем уведомления по сообщению
            perscabnewEntitiesAdapter.tblNotify.RemoveRange(perscabnewEntitiesAdapter.tblNotify.Where(p => p.link_id == id_chatrec).Select(p => p));

            // удаляем информацию, связанную с документами сообщения
            IQueryable<tblZayavkaDoc> temp_tblZayavkaDoc = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => p.id_chatrec == id_chatrec).Select(p => p);
            /*foreach (tblZayavkaDoc tempZayavkaDoc in temp_tblZayavkaDoc)
            {
                perscabnewEntitiesAdapter.tblESignDoc.RemoveRange(perscabnewEntitiesAdapter.tblESignDoc.Where(p => p.id_zayavkadoc == tempZayavkaDoc.id_zayavkadoc).Select(p => p));
            }*/
            // удаляем файлы
            foreach (tblZayavkaDoc tempZayavkaDoc in temp_tblZayavkaDoc)
            {
                MC_PersCab_FileIO.MC_PersCab_FileIO_DeleteFile(tempZayavkaDoc.doc_file_name, tempZayavkaDoc.doc_file_path);                 
            }
            perscabnewEntitiesAdapter.tblZayavkaDoc.RemoveRange(temp_tblZayavkaDoc);
                        
            // удаляем чат
            perscabnewEntitiesAdapter.tblZayavkaChat.Remove(perscabnewEntitiesAdapter.tblZayavkaChat.Where(p => p.id_chatrec == id_chatrec).Select(p => p).First());
            
            perscabnewEntitiesAdapter.SaveChanges();

            e.Cancel = true;
        }

    }
}