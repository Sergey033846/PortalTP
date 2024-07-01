using DatabaseFirst;
using mcperscab;

using DevExpress.Export;
using DevExpress.XtraPrinting;
using DevExpress.Web;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp.Cabinet
{
    public partial class ViewEnergyDogovors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ASPxGridViewEnergyDogovorsAll.FocusedRowIndex = -1;
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
                    /*ASPxGridViewEnergyDogovorsAll.Columns["Идентификатор заявки"].Visible = true;
                    ASPxGridViewEnergyDogovorsAll.Columns["Действия"].Visible = true;
                    ASPxGridViewEnergyDogovorsAll.SettingsDataSecurity.AllowDelete = true;*/
                }
                else
                {
                    /*ASPxGridViewEnergyDogovorsAll.Columns["Идентификатор заявки"].Visible = true;
                    ASPxGridViewEnergyDogovorsAll.Columns["Действия"].Visible = false;
                    ASPxGridViewEnergyDogovorsAll.SettingsDataSecurity.AllowDelete = false;*/
                }

                // предотвращаем открытие страницы через адресную строку с неразрешенными правами
                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER || user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN
                                || user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR 
                                || user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR)
                {
                    this.SqlDataSourceViewEnergyDogovors.SelectCommand =
                        String.Concat(
                                    "SELECT",
                                    " id_dogovorenergo, tGPDE.id_zayavka, id_gp_predstavitel, id_gp_filial, tGPDE.id_gp, id_cenovayakategoriya, id_prichinade,",
                                    " nomer_ls, nomer_dogovorenergo, nomer_elektroustanovka, date_create_de, date_podpis_de,",
                                    " v1C_Zayavitel,",
                                    " date_create_zayavka, zayavka_number_1c, zayavka_date_1c, caption_zayavkastatus, v1C_adresEPU",
                                    " FROM tblGP_DogovorEnergo tGPDE",
                                    " LEFT JOIN tblZayavka tZ ON tGPDE.id_zayavka = tZ.id_zayavka",
                                    " LEFT JOIN tblZayavkaStatus tZS ON tZ.v1C_StatusZayavki = tZS.id_zayavkastatus_1c",
                                    " ORDER BY date_create_de DESC");
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

        // удаление заявки и всех связанных с ней данных
        protected void ASPxGridViewEnergyDogovorsAll_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            /*Guid id_zayavka = Guid.Parse(e.Keys[ASPxGridViewEnergyDogovorsAll.KeyFieldName].ToString());

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();

            // удаляем информацию, связанную с документами заявки
            IQueryable<tblZayavkaDoc> temp_tblZayavkaDoc = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => p.id_zayavka == id_zayavka).Select(p => p);
            foreach (tblZayavkaDoc tempZayavkaDoc in temp_tblZayavkaDoc)
            {
                perscabnewEntitiesAdapter.tblESignDoc.RemoveRange(perscabnewEntitiesAdapter.tblESignDoc.Where(p => p.id_zayavkadoc == tempZayavkaDoc.id_zayavkadoc).Select(p => p));
            }
            perscabnewEntitiesAdapter.tblZayavkaDoc.RemoveRange(temp_tblZayavkaDoc);

            // удаляем историю изменения статусов заявки
            perscabnewEntitiesAdapter.tblZayavkaStatusHistory.RemoveRange(perscabnewEntitiesAdapter.tblZayavkaStatusHistory.Where(p => p.id_zayavka == id_zayavka).Select(p => p));

            // удаляем информацию о заключенных договорах
            IQueryable<tblContract> temp_tblContract = perscabnewEntitiesAdapter.tblContract.Where(p => p.id_zayavka == id_zayavka).Select(p => p);
            foreach (tblContract tempContract in temp_tblContract)
            {
                perscabnewEntitiesAdapter.tblContractDoc.RemoveRange(perscabnewEntitiesAdapter.tblContractDoc.Where(p => p.id_contract == tempContract.id_contract).Select(p => p));
                perscabnewEntitiesAdapter.tblContractStatusHistory.RemoveRange(perscabnewEntitiesAdapter.tblContractStatusHistory.Where(p => p.id_contract == tempContract.id_contract).Select(p => p));
            }
            perscabnewEntitiesAdapter.tblContract.RemoveRange(temp_tblContract);

            // удаляем чат
            perscabnewEntitiesAdapter.tblZayavkaChat.RemoveRange(perscabnewEntitiesAdapter.tblZayavkaChat.Where(p => p.id_zayavka == id_zayavka).Select(p => p));

            // удаляем лог
            perscabnewEntitiesAdapter.tblLog.RemoveRange(perscabnewEntitiesAdapter.tblLog.Where(p => p.id_zayavka == id_zayavka).Select(p => p));

            // удаляем саму заявку
            perscabnewEntitiesAdapter.tblZayavka.Remove(perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == id_zayavka).Select(p => p).First());

            perscabnewEntitiesAdapter.SaveChanges();

            e.Cancel = true;*/
        }

        // выбор пункта меню
        protected void ASPxMenuViewEnergyDogovors_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Export2Excel":
                    ASPxGridViewExporterOrders.FileName = "Договоры энергоснабжения";
                    ASPxGridViewExporterOrders.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.DataAware });
                    break;
            }
        }

        /*protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            Membership.GetUser("SarnackayaOV").ChangePassword("FyufhcRGjhngk46", "FyufhcRGjhnfk46");
        }*/

        // раскраска строк в зависимости от значений полей и пр.
        protected void ASPxGridViewEnergyDogovorsAll_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
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

    }

}