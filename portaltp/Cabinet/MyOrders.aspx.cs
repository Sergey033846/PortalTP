using DatabaseFirst;
using mcperscab;

using DevExpress.XtraPrinting;
using DevExpress.Web;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp.Cabinet
{
    public partial class MyOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*StringBuilder helpString = new StringBuilder("Для просмотра информации о заявке нажмите левую клавишу мыши дважды в соответствующей строке.", 300);
                helpString.Append(Environment.NewLine);
                helpString.Append(Environment.NewLine);
                helpString.Append("Для обновления информации нажмите F5 или выполните команду \"Обновить\", нажав правую клавишу мыши в области таблицы.");

                ASPxMemoHint.Text = helpString.ToString();*/
            }

            string user_login = Page.User.Identity.Name;

            if (Page.User.Identity.IsAuthenticated)
            {
                int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);

                // предотвращаем открытие страницы через адресную строку с неразрешенными правами
                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_USER)
                {
                    perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
                    //tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.email == user_login).Select(p => p).First();
                    tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

                    /*this.SqlDataSourceMyOrders.SelectCommand =
                        String.Concat("SELECT id_zayavka, tblZT.caption_zayavkatype_short, date_create_zayavka, zayavka_number_1c, zayavka_date_1c, ",
                                        "(SELECT tblZS.caption_zayavkastatus FROM ",
                                            "(SELECT TOP 1 tblZSH.id_zayavkastatus FROM tblZayavkaStatusHistory tblZSH WHERE tblZSH.id_zayavka = tblZ.id_zayavka ORDER BY tblZSH.date_status DESC) as tblTEMP ",
                                            "LEFT JOIN tblZayavkaStatus tblZS ON tblTEMP.id_zayavkastatus = tblZS.id_zayavkastatus) as caption_status, ",                                        
                                        "caption_filial ",
                                        "FROM tblZayavka tblZ ",
                                        "LEFT JOIN tblZayavkaType tblZT ON tblZ.id_zayavkatype = tblZT.id_zayavkatype ",
                                        "LEFT JOIN tblFilial tblF ON tblZ.id_filial = tblF.id_filial ",
                                        "WHERE id_user = '", userInfo.id_user.ToString(), "' AND is_temp IS NULL ORDER BY date_create_zayavka DESC");*/

                    this.SqlDataSourceMyOrders.SelectCommand =
                        String.Concat("SELECT ",
                                      "user_nameingrid, tblZ.id_zayavka, tblZT.caption_zayavkatype_short, tblZ.comment, ",
                                      "date_create_zayavka, ",
                                      "zayavka_number_1c, zayavka_date_1c, is_viewed, viewed_operator, caption_zayavkastatus, ",
                                      "caption_filial_short, v1C_adresEPU, v1C_maxMoschnostEPU, v1C_Zayavitel, ",
                                      "tblU.caption_short AS Z_uroven_U, tblVR.caption_short AS Z_vid_Oplaty, ",
                                      "tblGPDE.nomer_dogovorenergo, tblSO.caption_short AS Z_status_Oplaty, is_vremennaya, ",
                                      "tblСS.caption_contractstatus AS Z_status_DogovoraTP, ",

                                      "(SELECT CAST(COUNT(*) AS BIT)  ",
                                      "FROM tblZayavkaChat tblZC LEFT JOIN tblUserInfo tblUI ON tblZC.id_user = tblUI.id_user ",
                                      "WHERE tblZC.id_zayavka = tblZ.id_zayavka AND tblZC.istemp IS NULL AND tblUI.id_userrole <> 0 AND tblZC.isread = 0) ",
                                      "AS msgs_not_read_from_oke_flag ",

                                      "FROM tblZayavka tblZ ",
                                      "LEFT JOIN tblZayavkaType tblZT ON tblZ.id_zayavkatype = tblZT.id_zayavkatype ",
                                      "LEFT JOIN tblUserInfo tblUI ON tblZ.id_user = tblUI.id_user ", //???
                                      "LEFT JOIN tblFilial tblF ON tblZ.id_filial = tblF.id_filial ",
                                      /*"LEFT JOIN tblZayavkaStatus tblZS ON tblZ.id_zayavkastatus = tblZS.id_zayavkastatus ",*/
                                      "LEFT JOIN tblZayavkaStatus tblZS ON tblZ.v1C_StatusZayavki = tblZS.id_zayavkastatus_1c ",
                                      "LEFT JOIN tbl1C_EnumUrovenU tblU ON tblZ.id1C_EnumUrovenU = tblU._IDRref ",
                                      "LEFT JOIN tbl1C_VidRassrochki tblVR ON tblZ.id1C_VidRassrochki = tblVR._IDRref ",
                                      "LEFT JOIN tblGP_DogovorEnergo tblGPDE ON tblGPDE.id_zayavka = tblZ.id_zayavka ",
                                      "LEFT JOIN tbl1C_StatusOplaty tblSO ON tblSO._IDRref = tblZ.v1C_StatusOplaty ",
                                      "LEFT JOIN tblContractStatus tblСS ON tblСS.id_contractstatus_1c = tblZ.v1C_StatusDogovora ",

                                      "WHERE tblZ.id_user = '", userInfo.id_user.ToString(), "' AND is_temp IS NULL ORDER BY date_create_zayavka DESC");
                    
                    /*this.SqlDataSourceMyOrders.SelectCommand =
                        String.Concat("SELECT id_zayavka, tblZT.caption_zayavkatype_short, date_create_zayavka, zayavka_number_1c, zayavka_date_1c, v1C_Zayavitel, caption_zayavkastatus, ",
                                        "caption_filial, tblSO.caption_short AS Z_status_Oplaty, is_vremennaya ",
                                        "FROM tblZayavka tblZ ",
                                        "LEFT JOIN tblZayavkaType tblZT ON tblZ.id_zayavkatype = tblZT.id_zayavkatype ",
                                        "LEFT JOIN tblFilial tblF ON tblZ.id_filial = tblF.id_filial ",                                        
                                        "LEFT JOIN tblZayavkaStatus tblZS ON tblZ.v1C_StatusZayavki = tblZS.id_zayavkastatus_1c ",
                                        "LEFT JOIN tbl1C_StatusOplaty tblSO ON tblSO._IDRref = tblZ.v1C_StatusOplaty ",

                                        "WHERE id_user = '", userInfo.id_user.ToString(), "' AND is_temp IS NULL ORDER BY date_create_zayavka DESC");*/
                }
            }
            else
            {
                Response.Redirect("~");
            }
        }

        // раскраска строк в зависимости от значений полей и пр.
        protected void ASPxGridViewMyOrders_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            // имеются непрочитанные сообщения
            e.Row.Font.Bold = Convert.ToBoolean(e.GetValue("msgs_not_read_from_oke_flag"));
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
            ((ASPxLabel)sender).Visible = Convert.ToBoolean(container.Grid.GetRowValues(currentIndex, "msgs_not_read_from_oke_flag"));
        }

    }
}