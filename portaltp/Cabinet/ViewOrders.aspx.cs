using DatabaseFirst;
using mcperscab;

using DevExpress.Export;
using DevExpress.XtraPrinting;
using DevExpress.Web;

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
    public partial class ViewOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                ASPxGridViewOrdersAll.FocusedRowIndex = -1;
            }

            string user_login = Page.User.Identity.Name;

            if (Page.User.Identity.IsAuthenticated)
            {
                int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);

                perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
                tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

                // видимость операций и компонентов администратора
                RadioButton_IsTemp.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN); // видимость черновиков для администратора

                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
                {
                    ASPxGridViewOrdersAll.Columns["Идентификатор заявки"].Visible = false; // не влазит
                    ASPxGridViewOrdersAll.Columns["Действия"].Visible = true;
                    ASPxGridViewOrdersAll.Columns["Дата переноса"].Visible = true;
                    ASPxGridViewOrdersAll.SettingsDataSecurity.AllowDelete = true;

                    ASPxTextBoxUserCount.Visible = true; // видимость статистики сеансов
                    ASPxTextBoxUserCount.Text = Membership.GetNumberOfUsersOnline().ToString();
                }
                else
                {
                    ASPxGridViewOrdersAll.Columns["Идентификатор заявки"].Visible = false;
                    ASPxGridViewOrdersAll.Columns["Действия"].Visible = false;
                    ASPxGridViewOrdersAll.Columns["Дата переноса"].Visible = false;
                    ASPxGridViewOrdersAll.SettingsDataSecurity.AllowDelete = false;
                }
                
                // предотвращаем открытие страницы через адресную строку с неразрешенными правами
                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)                                  
                {
                    this.SqlDataSourceViewOrders.SelectCommand =
                        String.Concat("SELECT ",
                                      "user_nameingrid, tblZ.id_zayavka, tblZT.caption_zayavkatype_short, tblZ.comment, ",
                                      "date_create_zayavka, id_ordersource, date_copy_from1c, ",
                                      "zayavka_number_1c, zayavka_date_1c, is_viewed, viewed_operator, caption_zayavkastatus, ",
                                      "caption_filial_short, v1C_adresEPU, v1C_maxMoschnostEPU, v1C_Zayavitel, ",
                                      "tblU.caption_short AS Z_uroven_U, tblVR.caption_short AS Z_vid_Oplaty, ",
                                      "tblGPDE.nomer_dogovorenergo, tblSO.caption_short AS Z_status_Oplaty, is_vremennaya, ",
                                      "tblСS.caption_contractstatus AS Z_status_DogovoraTP, tblZ.is_temp, ",

                                      /*"(SELECT COUNT(*) AS msgs_not_read_from_user_count, CAST(COUNT(*) AS BIT) AS msgs_not_read_from_user_flag ",
                                      "FROM tblZayavkaChat tblZC LEFT JOIN tblUserInfo tblUI ON tblZC.id_user = tblUI.id_user ",
                                      "WHERE tblZC.id_zayavka = tblZ.id_zayavka AND tblZC.istemp IS NULL AND tblUI.id_userrole = 0 AND tblZC.isread = 0) ",*/

                                      "(SELECT CAST(COUNT(*) AS BIT)  ",
                                      "FROM tblZayavkaChat tblZC LEFT JOIN tblUserInfo tblUI ON tblZC.id_user = tblUI.id_user ",
                                      "WHERE tblZC.id_zayavka = tblZ.id_zayavka AND tblZC.istemp IS NULL AND tblUI.id_userrole = 0 AND tblZC.isread = 0) ",
                                      "AS msgs_not_read_from_user_flag ",

                                      "FROM tblZayavka tblZ ",
                                      "LEFT JOIN tblZayavkaType tblZT ON tblZ.id_zayavkatype = tblZT.id_zayavkatype ",
                                      "LEFT JOIN tblUserInfo tblUI ON tblZ.id_user = tblUI.id_user ",
                                      "LEFT JOIN tblFilial tblF ON tblZ.id_filial = tblF.id_filial ",
                                      /*"LEFT JOIN tblZayavkaStatus tblZS ON tblZ.id_zayavkastatus = tblZS.id_zayavkastatus ",*/
                                      "LEFT JOIN tblZayavkaStatus tblZS ON tblZ.v1C_StatusZayavki = tblZS.id_zayavkastatus_1c ",
                                      "LEFT JOIN tbl1C_EnumUrovenU tblU ON tblZ.id1C_EnumUrovenU = tblU._IDRref ",
                                      "LEFT JOIN tbl1C_VidRassrochki tblVR ON tblZ.id1C_VidRassrochki = tblVR._IDRref ",
                                      "LEFT JOIN tblGP_DogovorEnergo tblGPDE ON tblGPDE.id_zayavka = tblZ.id_zayavka ",
                                      "LEFT JOIN tbl1C_StatusOplaty tblSO ON tblSO._IDRref = tblZ.v1C_StatusOplaty ",
                                      "LEFT JOIN tblContractStatus tblСS ON tblСS.id_contractstatus_1c = tblZ.v1C_StatusDogovora ",

                                      //"WHERE is_temp is NULL ",
                                      "ORDER BY date_create_zayavka DESC");
                }
                else if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER || user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR)
                {
                    this.SqlDataSourceViewOrders.SelectCommand =
                        String.Concat("SELECT ",
                                      "user_nameingrid, tblZ.id_zayavka, tblZT.caption_zayavkatype_short, tblZ.comment, ",
                                      "date_create_zayavka, id_ordersource, date_copy_from1c, ",
                                      "zayavka_number_1c, zayavka_date_1c, is_viewed, viewed_operator, caption_zayavkastatus, ",
                                      "caption_filial_short, v1C_adresEPU, v1C_maxMoschnostEPU, v1C_Zayavitel, ",
                                      "tblU.caption_short AS Z_uroven_U, tblVR.caption_short AS Z_vid_Oplaty, ",
                                      "tblGPDE.nomer_dogovorenergo, tblSO.caption_short AS Z_status_Oplaty, is_vremennaya, ",
                                      "tblСS.caption_contractstatus AS Z_status_DogovoraTP, ",

                                      /*"(SELECT COUNT(*) AS msgs_not_read_from_user_count, CAST(COUNT(*) AS BIT) AS msgs_not_read_from_user_flag ",
                                      "FROM tblZayavkaChat tblZC LEFT JOIN tblUserInfo tblUI ON tblZC.id_user = tblUI.id_user ",
                                      "WHERE tblZC.id_zayavka = tblZ.id_zayavka AND tblZC.istemp IS NULL AND tblUI.id_userrole = 0 AND tblZC.isread = 0) ",*/

                                      "(SELECT CAST(COUNT(*) AS BIT)  ",
                                      "FROM tblZayavkaChat tblZC LEFT JOIN tblUserInfo tblUI ON tblZC.id_user = tblUI.id_user ",
                                      "WHERE tblZC.id_zayavka = tblZ.id_zayavka AND tblZC.istemp IS NULL AND tblUI.id_userrole = 0 AND tblZC.isread = 0) ",
                                      "AS msgs_not_read_from_user_flag ",

                                      "FROM tblZayavka tblZ ",
                                      "LEFT JOIN tblZayavkaType tblZT ON tblZ.id_zayavkatype = tblZT.id_zayavkatype ",
                                      "LEFT JOIN tblUserInfo tblUI ON tblZ.id_user = tblUI.id_user ",
                                      "LEFT JOIN tblFilial tblF ON tblZ.id_filial = tblF.id_filial ",
                                      /*"LEFT JOIN tblZayavkaStatus tblZS ON tblZ.id_zayavkastatus = tblZS.id_zayavkastatus ",*/
                                      "LEFT JOIN tblZayavkaStatus tblZS ON tblZ.v1C_StatusZayavki = tblZS.id_zayavkastatus_1c ",
                                      "LEFT JOIN tbl1C_EnumUrovenU tblU ON tblZ.id1C_EnumUrovenU = tblU._IDRref ",
                                      "LEFT JOIN tbl1C_VidRassrochki tblVR ON tblZ.id1C_VidRassrochki = tblVR._IDRref ",
                                      "LEFT JOIN tblGP_DogovorEnergo tblGPDE ON tblGPDE.id_zayavka = tblZ.id_zayavka ",
                                      "LEFT JOIN tbl1C_StatusOplaty tblSO ON tblSO._IDRref = tblZ.v1C_StatusOplaty ",
                                      "LEFT JOIN tblContractStatus tblСS ON tblСS.id_contractstatus_1c = tblZ.v1C_StatusDogovora ",

                                      "WHERE is_temp is NULL ",
                                      "ORDER BY date_create_zayavka DESC");
                }
                else if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR)
                {
                    this.SqlDataSourceViewOrders.SelectCommand =
                        String.Concat("SELECT ",
                                      "user_nameingrid, tblZ.id_zayavka, tblZT.caption_zayavkatype_short, tblZ.comment, ",
                                      "date_create_zayavka, id_ordersource, date_copy_from1c, ",
                                      "zayavka_number_1c, zayavka_date_1c, is_viewed, viewed_operator, caption_zayavkastatus, ",
                                      "caption_filial_short, v1C_adresEPU, v1C_maxMoschnostEPU, v1C_Zayavitel, ",
                                      "tblU.caption_short AS Z_uroven_U, tblVR.caption_short AS Z_vid_Oplaty, ",
                                      "tblGPDE.nomer_dogovorenergo, tblSO.caption_short AS Z_status_Oplaty, is_vremennaya, ",
                                      "tblСS.caption_contractstatus AS Z_status_DogovoraTP, ",

                                      "(SELECT CAST(COUNT(*) AS BIT)  ",
                                      "FROM tblZayavkaChat tblZC LEFT JOIN tblUserInfo tblUI ON tblZC.id_user = tblUI.id_user ",
                                      "WHERE tblZC.id_zayavka = tblZ.id_zayavka AND tblZC.istemp IS NULL AND tblUI.id_userrole = 0 AND tblZC.isread = 0) ",
                                      "AS msgs_not_read_from_user_flag ",

                                      "FROM tblZayavka tblZ ",
                                      "LEFT JOIN tblZayavkaType tblZT ON tblZ.id_zayavkatype = tblZT.id_zayavkatype ",
                                      "LEFT JOIN tblUserInfo tblUI ON tblZ.id_user = tblUI.id_user ",
                                      "LEFT JOIN tblFilial tblF ON tblZ.id_filial = tblF.id_filial ",
                                      /*"LEFT JOIN tblZayavkaStatus tblZS ON tblZ.id_zayavkastatus = tblZS.id_zayavkastatus ",*/
                                      "LEFT JOIN tblZayavkaStatus tblZS ON tblZ.v1C_StatusZayavki = tblZS.id_zayavkastatus_1c ",
                                      "LEFT JOIN tbl1C_EnumUrovenU tblU ON tblZ.id1C_EnumUrovenU = tblU._IDRref ",
                                      "LEFT JOIN tbl1C_VidRassrochki tblVR ON tblZ.id1C_VidRassrochki = tblVR._IDRref ",
                                      "LEFT JOIN tblGP_DogovorEnergo tblGPDE ON tblGPDE.id_zayavka = tblZ.id_zayavka ",
                                      "LEFT JOIN tbl1C_StatusOplaty tblSO ON tblSO._IDRref = tblZ.v1C_StatusOplaty ",
                                      "LEFT JOIN tblContractStatus tblСS ON tblСS.id_contractstatus_1c = tblZ.v1C_StatusDogovora ",

                                      "WHERE (is_temp is NULL) AND (tblZ.id_filial = ",
                                      userInfo.id_filial.ToString(),
                                      ")",
                                      "ORDER BY date_create_zayavka DESC");
                }
                else
                {
                    Response.Redirect("~");
                }
            } // if if (Page.User.Identity.IsAuthenticated)
            else
            {
                Response.Redirect("~");
            }
        } // protected void Page_Load(object sender, EventArgs e)

        // удаление заявки и всех связанных с ней данных
        protected void ASPxGridViewOrdersAll_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Guid id_zayavka = Guid.Parse(e.Keys[ASPxGridViewOrdersAll.KeyFieldName].ToString());

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();

            // удаляем уведомления по заявке
            perscabnewEntitiesAdapter.tblNotify.RemoveRange(perscabnewEntitiesAdapter.tblNotify.Where(p => p.link_id == id_zayavka).Select(p => p));

            // удаляем информацию, связанную с документами заявки
            IQueryable<tblZayavkaDoc> temp_tblZayavkaDoc = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => p.id_zayavka == id_zayavka).Select(p => p);
            foreach (tblZayavkaDoc tempZayavkaDoc in temp_tblZayavkaDoc)
            {
                MC_PersCab_FileIO.MC_PersCab_FileIO_DeleteFile(tempZayavkaDoc.doc_file_name, tempZayavkaDoc.doc_file_path); // удаляем файл документа
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

            // удаляем лог
            perscabnewEntitiesAdapter.tblLog.RemoveRange(perscabnewEntitiesAdapter.tblLog.Where(p => p.id_zayavka == id_zayavka).Select(p => p));

            // удаляем саму заявку
            perscabnewEntitiesAdapter.tblZayavka.Remove(perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == id_zayavka).Select(p => p).First());

            perscabnewEntitiesAdapter.SaveChanges();

            e.Cancel = true;
        }

        // выбор пункта меню
        protected void ASPxMenuViewOrders_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Export2Excel":
                    ASPxGridViewExporterOrders.FileName = "Все заявки";
                    ASPxGridViewExporterOrders.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.DataAware });
                    break;
            }
        }
        
        // раскраска строк в зависимости от значений полей и пр.
        protected void ASPxGridViewOrdersAll_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != GridViewRowType.Data) return;

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
            }

            /*// черновик
            string value_zayavka_istemp_str = e.GetValue("is_temp").ToString();
            if (!String.IsNullOrWhiteSpace(value_zayavka_istemp_str))
            {
                bool value_zayavka_istemp = Convert.ToBoolean(value_zayavka_istemp_str);
                if (value_zayavka_istemp)
                {
                    e.Row.BackColor = Color.LightGray;
                    e.Row.ForeColor = Color.Black;
                }
            }*/

            // имеются непрочитанные сообщения
            e.Row.Font.Bold = Convert.ToBoolean(e.GetValue("msgs_not_read_from_user_flag"));            
        }
        
        // callback - переключатель "все"/"Незакрепленные"/"незарегистрированные"/"новые сообщения"/черновики
        protected void CallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (RadioButton_ZayavkaNotFilialSet.Checked)
            {                
                ASPxGridViewOrdersAll.FilterExpression = "[caption_filial_short] = NULL OR [caption_filial_short] = ''";             
            }
            else
            if (RadioButton_ZayavkaNotRegister.Checked)
            {                
                ASPxGridViewOrdersAll.FilterExpression = "[zayavka_number_1c] = NULL OR [zayavka_number_1c] = ''";             
            }
            else
            if (RadioButton_ZayavkaAll.Checked)
            {                
                ASPxGridViewOrdersAll.FilterExpression = String.Empty;                
            }
            else
            if (RadioButton_ZayavkaNewMessages.Checked)
            {
                ASPxGridViewOrdersAll.FilterExpression = "[msgs_not_read_from_user_flag] = true";
            }
            else
            if (RadioButton_IsTemp.Checked)
            {
                ASPxGridViewOrdersAll.FilterExpression = "[is_temp] = true";
            }
        }

        // формирование поля "статус оплаты"
        protected void ASPxLabelStatusOplaty_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxLabel)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string statusOplaty = container.Grid.GetRowValues(currentIndex, "Z_status_Oplaty").ToString();

            string isVremennayaZayavkaStr = container.Grid.GetRowValues(currentIndex, "is_vremennaya").ToString();
            bool isVremennayaZayavka = 
                String.IsNullOrEmpty(isVremennayaZayavkaStr) ? false : Convert.ToBoolean(container.Grid.GetRowValues(currentIndex, "is_vremennaya").ToString());

            if (!isVremennayaZayavka)
            {
                ((ASPxLabel)sender).Text = statusOplaty;

                if (String.Equals(statusOplaty, "полная оплата")) ((ASPxLabel)sender).CssClass = "label label-success";
                else if (String.Equals(statusOplaty, "не оплачено")) ((ASPxLabel)sender).CssClass = "label label-danger";
                else if (String.Equals(statusOplaty, "частичная оплата")) ((ASPxLabel)sender).CssClass = "label label-warning";
            }
        }

        // формирование поля наличия непрочитанных сообщений
        protected void ASPxLabelNewMessages_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxLabel)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            ((ASPxLabel)sender).Visible = Convert.ToBoolean(container.Grid.GetRowValues(currentIndex, "msgs_not_read_from_user_flag"));
        }

        // формирование поля "Источник заявки"
        protected void ASPxImageOrderSource_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxImage)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            Guid id_ordersource = Guid.Parse(container.Grid.GetRowValues(currentIndex, "id_ordersource").ToString());

            if (id_ordersource == MC_PersCab_Consts.orderSource_1C)
            {
                ((ASPxImage)sender).ImageUrl = "~/Content/Images/1c24_2.png";
            }
            else 
            if (id_ordersource == MC_PersCab_Consts.orderSource_LK)
            {
                ((ASPxImage)sender).ImageUrl = "~/Content/Images/globe16_2.png";
            }
        }

    }

}