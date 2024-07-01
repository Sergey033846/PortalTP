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
    public partial class ViewOrdersDebugging : System.Web.UI.Page
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
                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
                {
                    ASPxGridViewOrdersAll.Columns["Идентификатор заявки"].Visible = false; // не влазит
                    ASPxGridViewOrdersAll.Columns["Действия"].Visible = true;
                    ASPxGridViewOrdersAll.SettingsDataSecurity.AllowDelete = true;
                }
                else
                {
                    ASPxGridViewOrdersAll.Columns["Идентификатор заявки"].Visible = false;
                    ASPxGridViewOrdersAll.Columns["Действия"].Visible = false;
                    ASPxGridViewOrdersAll.SettingsDataSecurity.AllowDelete = false;
                }

                // предотвращаем открытие страницы через адресную строку с неразрешенными правами
                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER || user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN
                                  || user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR)
                {
                    this.SqlDataSourceViewOrders.SelectCommand =
                        String.Concat("SELECT ",
                                      "user_nameingrid, id_zayavka, tblZT.caption_zayavkatype_short, tblZ.comment, ",
                                      "date_create_zayavka, ",
                                      "zayavka_number_1c, zayavka_date_1c, is_viewed, viewed_operator, caption_zayavkastatus, ",
                                      "caption_filial_short, v1C_adresEPU, v1C_maxMoschnostEPU, v1C_Zayavitel, ",
                                      "tblU.caption_short AS Z_uroven_U, tblVR.caption_short AS Z_vid_Oplaty, ",

                                      "status_GP_RegNewZ, status_GP_RegNewDTP, status_PrikreplenAktTP, status_PrikreplenAktDopuskaPU, status_ZAnnulirovana ",

                                      "FROM tblZayavka tblZ ",
                                      "LEFT JOIN tblZayavkaType tblZT ON tblZ.id_zayavkatype = tblZT.id_zayavkatype ",
                                      "LEFT JOIN tblUserInfo tblUI ON tblZ.id_user = tblUI.id_user ",
                                      "LEFT JOIN tblFilial tblF ON tblZ.id_filial = tblF.id_filial ",
                                      /*"LEFT JOIN tblZayavkaStatus tblZS ON tblZ.id_zayavkastatus = tblZS.id_zayavkastatus ",*/
                                      "LEFT JOIN tblZayavkaStatus tblZS ON tblZ.v1C_StatusZayavki = tblZS.id_zayavkastatus_1c ",
                                      "LEFT JOIN tbl1C_EnumUrovenU tblU ON tblZ.id1C_EnumUrovenU = tblU._IDRref ",
                                      "LEFT JOIN tbl1C_VidRassrochki tblVR ON tblZ.id1C_VidRassrochki = tblVR._IDRref ",

                                      "WHERE is_temp is NULL ",
                                      "ORDER BY date_create_zayavka DESC");
                }
                else if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR)
                {
                    this.SqlDataSourceViewOrders.SelectCommand =
                        String.Concat("SELECT ",
                                      "user_nameingrid, id_zayavka, tblZT.caption_zayavkatype_short, tblZ.comment, ",
                                      "date_create_zayavka, ",
                                      "zayavka_number_1c, zayavka_date_1c, is_viewed, viewed_operator, caption_zayavkastatus, ",
                                      "caption_filial_short, v1C_adresEPU, v1C_maxMoschnostEPU, ",
                                      "tblU.caption_short AS Z_uroven_U, tblVR.caption_short AS Z_vid_Oplaty ",
                                      "FROM tblZayavka tblZ ",
                                      "LEFT JOIN tblZayavkaType tblZT ON tblZ.id_zayavkatype = tblZT.id_zayavkatype ",
                                      "LEFT JOIN tblUserInfo tblUI ON tblZ.id_user = tblUI.id_user ",
                                      "LEFT JOIN tblFilial tblF ON tblZ.id_filial = tblF.id_filial ",
                                      /*"LEFT JOIN tblZayavkaStatus tblZS ON tblZ.id_zayavkastatus = tblZS.id_zayavkastatus ",*/
                                      "LEFT JOIN tblZayavkaStatus tblZS ON tblZ.v1C_StatusZayavki = tblZS.id_zayavkastatus_1c ",
                                      "LEFT JOIN tbl1C_EnumUrovenU tblU ON tblZ.id1C_EnumUrovenU = tblU._IDRref ",
                                      "LEFT JOIN tbl1C_VidRassrochki tblVR ON tblZ.id1C_VidRassrochki = tblVR._IDRref ",

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
        }

        // callback - переключатель "все"/"Незакрепленные"/"незарегистрированные"
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
        }

    }

}