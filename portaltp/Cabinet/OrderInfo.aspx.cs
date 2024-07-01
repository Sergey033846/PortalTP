using DatabaseFirst;
using mcperscab;

using DevExpress.Web;
using DevExpress.Web.Bootstrap;

//using CryptoPro.Sharpei;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace portaltp.Cabinet
{
    public partial class OrderInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            if (!IsPostBack)
            {
                Guid id_chatrec = Guid.NewGuid();
                ASPxHiddenFieldChatRecID.Set("chatRecID", id_chatrec);

                ASPxTextBoxOrderGUID.Text = Request.QueryString["id"].ToString();
            }

            if (Page.User.Identity.IsAuthenticated)
            {                
                // информация о заявке
                string id_zayavka_str = Request.QueryString["id"].ToString();
                Guid id_zayavka = Guid.Parse(Request.QueryString["id"].ToString());

                perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
                                
                int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);
                Guid id_user = perscabnewEntitiesAdapter.tblUserInfo.Where(p => String.Equals(p.user_login, user_login)).Select(p => p).First().id_user;

                // ищем собственника заявки
                Guid id_owner = perscabnewEntitiesAdapter.tblZayavka.Where(p => String.Equals(p.id_zayavka.ToString(), id_zayavka_str)).Select(p => p).First().id_user;

                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                    user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR ||
                    id_user == id_owner)
                {
                    /*this.SqlDataSourceOrderInfo.SelectCommand =
                        String.Concat("SELECT ",
                                      "user_nameingrid, id_zayavka, tblZT.caption_zayavkatype_short, tblZ.comment, ",
                                      "date_create_zayavka, ",
                                      "zayavka_number_1c, zayavka_date_1c, is_viewed, viewed_operator, ",
                                      "(SELECT tblZS.caption_zayavkastatus FROM ",
                                            "(SELECT TOP 1 tblZSH.id_zayavkastatus FROM tblZayavkaStatusHistory tblZSH WHERE tblZSH.id_zayavka = tblZ.id_zayavka ORDER BY tblZSH.date_status DESC) as tblTEMP ",
                                            "LEFT JOIN tblZayavkaStatus tblZS ON tblTEMP.id_zayavkastatus = tblZS.id_zayavkastatus) as caption_status, ",
                                      "caption_filial_short ",
                                      "FROM tblZayavka tblZ ",
                                      "LEFT JOIN tblZayavkaType tblZT ON tblZ.id_zayavkatype = tblZT.id_zayavkatype ",
                                      "LEFT JOIN tblUserInfo tblUI ON tblZ.id_user = tblUI.id_user ",
                                      "LEFT JOIN tblFilial tblF ON tblZ.id_filial = tblF.id_filial ",
                                      "WHERE id_zayavka = '", id_zayavka, "'");*/

                    /*this.SqlDataSourceOrderInfo.SelectCommand =
                    String.Concat("SELECT ",
                                  "user_nameingrid, id_zayavka, tblZT.caption_zayavkatype_short, tblZ.comment, ",
                                  "date_create_zayavka, v1C_Zayavitel, ",
                                  "zayavka_number_1c, zayavka_date_1c, is_viewed, viewed_operator, caption_zayavkastatus, ",
                                  "caption_filial_short ",
                                  "FROM tblZayavka tblZ ",
                                  "LEFT JOIN tblZayavkaType tblZT ON tblZ.id_zayavkatype = tblZT.id_zayavkatype ",
                                  "LEFT JOIN tblUserInfo tblUI ON tblZ.id_user = tblUI.id_user ",
                                  "LEFT JOIN tblFilial tblF ON tblZ.id_filial = tblF.id_filial ",                                  
                                  "LEFT JOIN tblZayavkaStatus tblZS ON tblZ.v1C_StatusZayavki = tblZS.id_zayavkastatus_1c ",
                                  "WHERE id_zayavka = '", id_zayavka_str, "'");*/

                    this.SqlDataSourceOrderInfo.SelectCommand =
                        String.Concat("SELECT ",
                                      "user_nameingrid, tblZ.id_zayavka, tblZT.caption_zayavkatype_short, tblZ.comment, ",
                                      "date_create_zayavka, ",
                                      "zayavka_number_1c, zayavka_date_1c, is_viewed, viewed_operator, caption_zayavkastatus, ",
                                      "caption_filial_short, v1C_adresEPU, v1C_maxMoschnostEPU, v1C_Zayavitel, ",
                                      "tblU.caption_short AS Z_uroven_U, tblVR.caption_short AS Z_vid_Oplaty, ",
                                      "tblSO.caption_short AS Z_status_Oplaty, is_vremennaya ",                                      

                                      "FROM tblZayavka tblZ ",
                                      "LEFT JOIN tblZayavkaType tblZT ON tblZ.id_zayavkatype = tblZT.id_zayavkatype ",
                                      "LEFT JOIN tblUserInfo tblUI ON tblZ.id_user = tblUI.id_user ", //???
                                      "LEFT JOIN tblFilial tblF ON tblZ.id_filial = tblF.id_filial ",
                                      /*"LEFT JOIN tblZayavkaStatus tblZS ON tblZ.id_zayavkastatus = tblZS.id_zayavkastatus ",*/
                                      "LEFT JOIN tblZayavkaStatus tblZS ON tblZ.v1C_StatusZayavki = tblZS.id_zayavkastatus_1c ",
                                      "LEFT JOIN tbl1C_EnumUrovenU tblU ON tblZ.id1C_EnumUrovenU = tblU._IDRref ",
                                      "LEFT JOIN tbl1C_VidRassrochki tblVR ON tblZ.id1C_VidRassrochki = tblVR._IDRref ",
                                      "LEFT JOIN tbl1C_StatusOplaty tblSO ON tblSO._IDRref = tblZ.v1C_StatusOplaty ",

                                      "WHERE tblZ.id_zayavka = '", id_zayavka_str, "'");


                    // документы заявки (кроме типов документов № 4,11 - договор энергоснабжения и платежные реквизиты ГП)
                    this.SqlDataSourceOrderDocs.SelectCommand =
                        String.Concat("SELECT id_zayavkadoc, id_zayavka, date_doc_add, doc_file_name, doc_file_path, tblDT.caption_doctype ",
                                      "FROM tblZayavkaDoc tblZD ",
                                      "LEFT JOIN tblDocType tblDT ON tblZD.id_doctype = tblDT.id_doctype ",
                                      "WHERE id_zayavka = '", id_zayavka_str, "' AND is_temp IS NULL AND id_contract IS NULL ",
                                      "AND tblZD.id_doctype NOT IN (4, 11)",
                                      " ORDER BY date_doc_add DESC");

                    // документы заявки и договора из 1С -------------------------
                    string zayavka_number_1c = perscabnewEntitiesAdapter.tblZayavka.Where(p => String.Equals(p.id_zayavka.ToString(), id_zayavka_str)).Select(p => p).First().zayavka_number_1c;
                    if (String.IsNullOrWhiteSpace(zayavka_number_1c) || String.Equals(zayavka_number_1c, "-")) zayavka_number_1c = "UNREGISTEREDORDER";
                    //zayavka_number_1c = zayavka_number_1c.;

                    // получаем _IDRRef, договор заявки по её номеру и всю сопутствуюущую информацию
                    // создаем соединение с УПП                
                    DataTable tblUPP_Zayavka = new DataTable();
                    string queryString = String.Concat(
                                    "SELECT _IDRRef, _Fld26430RRef",
                                    " FROM upp.dbo._Document26172",
                                    " WHERE (_Fld26403 = '", zayavka_number_1c, "')", " AND (_Marked = 0)");
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["1CDbConnectionString"].ToString());
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(queryString, connection);
                    adapter.SelectCommand.CommandTimeout = 0; //бесконечное время ожидания
                    adapter.Fill(tblUPP_Zayavka);
                    connection.Close();

                    // информация о документах заявки 1С
                    string zayavka_id_1C = "notfound";
                    try
                    {
                        zayavka_id_1C = BitConverter.ToString((byte[])tblUPP_Zayavka.Rows[0]["_IDRRef"]).Replace("-", "");

                        this.SqlDataSourceOrderDocs1C.SelectCommand =
                        String.Concat(
                                    "SELECT tDoc._IDRRef, _Fld24758, tVidDoc._Description, " +
                                    "DATEADD(year, -2000,_Fld27102) AS Z_DocDate, " +
                                    "_Fld28469RRef, _Fld28470, _Fld28471, _Fld28472",
                                    " FROM upp.dbo._Reference24727 tDoc",
                                    " LEFT JOIN upp.dbo._Reference26373 tVidDoc",
                                    " ON tDoc._Fld24917_RRRef = tVidDoc._IDRRef",
                                    " WHERE (_Fld24760_RRRef = 0x", zayavka_id_1C, ") AND (tDoc._Marked = 0) AND (tVidDoc._Fld36611 = 1)",
                                    /*" WHERE (_Fld24760_RRRef = ",
                                            " (SELECT TOP 1 _IDRRef AS Z_IDRRef FROM upp.dbo._Document26172",
                                            " WHERE _Fld26403 = '", zayavka_number_1c, "')) AND (tDoc._Marked = 0)",*/
                                    " ORDER BY _Fld27102 DESC");
                    }
                    catch (IndexOutOfRangeException)
                    {
                        zayavka_id_1C = "notfound";
                    }


                    // информация о договоре 1С                
                    string dogovor_id_1C = "notfound";
                    try
                    {
                        dogovor_id_1C = BitConverter.ToString((byte[])tblUPP_Zayavka.Rows[0]["_Fld26430RRef"]).Replace("-", "");

                        this.SqlDataSourceDogovorInfo1C.SelectCommand =
                        String.Concat(
                                    "SELECT _IDRRef AS D_1C_id, _Fld24839 AS D_1C_Nomer, _Fld27576RRef AS D_1C_Status," +
                                    " DATEADD(year, -2000,_Fld24840) AS D_1C_Data",
                                    " FROM upp.dbo._Document24731",
                                    " WHERE _IDRRef = 0x", dogovor_id_1C);

                        // документы договора отображать только в случае, если статус договора не равен
                        // "создание", "согласование", "не согласован", "аннулирован", "в архиве", "аннулирован (неоплата)"
                        this.SqlDataSourceDogovorDocs1C.SelectCommand =
                          String.Concat(
                                      "SELECT tDoc._IDRRef, _Fld24758, tVidDoc._Description, tVidDoc._Code AS VidDocCode," +
                                      " DATEADD(year, -2000,_Fld27102) AS D_DocDate," +
                                      " _Fld28469RRef, _Fld28470, _Fld28471, _Fld28472",
                                      " FROM upp.dbo._Reference24727 tDoc",
                                      " LEFT JOIN upp.dbo._Reference26373 tVidDoc",
                                      " ON tDoc._Fld24917_RRRef = tVidDoc._IDRRef",
                                      " WHERE (_Fld24760_RRRef = 0x", dogovor_id_1C, ") AND (tDoc._Marked = 0) AND (tVidDoc._Fld36611 = 1)",

                                      " AND (SELECT _EnumOrder",
                                      " FROM upp.dbo._Document24731 tD",
                                      " LEFT JOIN upp.dbo._Enum27554 tDS",
                                      " ON td._Fld27576RRef = tDS._IDRRef",
                                      " WHERE tD._IDRRef = 0x", dogovor_id_1C, ") NOT IN (0, 1, 2, 11, 12, 13)",

                                      " ORDER BY _Fld27102 DESC");

                        this.SqlDataSourceOplataDocs1C.SelectCommand =
                          String.Concat(
                                      "SELECT tDoc._IDRRef, _Fld24758, tVidDoc._Description, tVidDoc._Code AS VidDocCode," +
                                      " DATEADD(year, -2000,_Fld27102) AS D_DocDate," +
                                      " _Fld28469RRef, _Fld28470, _Fld28471, _Fld28472",
                                      " FROM upp.dbo._Reference24727 tDoc",
                                      " LEFT JOIN upp.dbo._Reference26373 tVidDoc",
                                      " ON tDoc._Fld24917_RRRef = tVidDoc._IDRRef",
                                      " WHERE (_Fld24760_RRRef = 0x", dogovor_id_1C, ") AND (tDoc._Marked = 0) AND (tVidDoc._Fld36611 = 1)",

                                      " AND (SELECT _EnumOrder",
                                      " FROM upp.dbo._Document24731 tD",
                                      " LEFT JOIN upp.dbo._Enum27554 tDS",
                                      " ON td._Fld27576RRef = tDS._IDRRef",
                                      " WHERE tD._IDRRef = 0x", dogovor_id_1C, ") NOT IN (0, 1, 2, 11, 12, 13)",

                                      " ORDER BY _Fld27102 DESC");
                    }
                    catch (IndexOutOfRangeException)
                    {
                        dogovor_id_1C = "notfound";
                    }
                    ASPxGridViewContractDocs.DataBind();
                    if (ASPxGridViewContractDocs.VisibleRowCount > 0)
                        BootstrapPageControl1.TabPages.FindByName("D1C").Badge.Text = ASPxGridViewContractDocs.VisibleRowCount.ToString();

                    PlaceHolder_DocsPril2Pril6.Visible = ASPxGridViewContractDocs.VisibleRowCount > 0;

                    // информация о документах оплаты                
                    ASPxGridViewOplataDocs.FilterExpression = "[VidDocCode] = '000010292' OR [VidDocCode] = '000010293' OR [VidDocCode] = '000000208'";
                    if (ASPxGridViewOplataDocs.VisibleRowCount > 0)
                    {
                        BootstrapPageControl1.TabPages.FindByName("Oplata1C").Badge.Text = ASPxGridViewOplataDocs.VisibleRowCount.ToString();
                    }

                    //-----------------------------------------

                    // информация о договоре энергоснабжения
                    try
                    {
                        Guid? id_dogovor_energo = perscabnewEntitiesAdapter.tblGP_DogovorEnergo.Where(p => String.Equals(p.id_zayavka.ToString(), id_zayavka_str)).Select(p => p).First().id_dogovorenergo;

                        if (id_dogovor_energo.HasValue)
                        {
                            // информация договора энергоснабжения
                            this.SqlDataSourceDogovorEnergoInfo.SelectCommand =
                                String.Concat(
                                            "SELECT id_dogovorenergo, id_zayavka, id_gp_predstavitel, id_gp_filial, id_gp, id_cenovayakategoriya, id_prichinade,",
                                            " nomer_ls, nomer_dogovorenergo, nomer_elektroustanovka, date_create_de, date_podpis_de",
                                            " FROM tblGP_DogovorEnergo",
                                            " WHERE id_dogovorenergo = '", id_dogovor_energo.ToString(), "'");
                        }
                    }
                    catch (InvalidOperationException)
                    {

                    }

                    // документы договора энергоснабжения (берем строго по типу документа № 4,11 - договор энергоснабжения и платежные реквизиты ГП)
                    this.SqlDataSourceDogovorEnergoDocs.SelectCommand =
                        String.Concat(
                                      "SELECT id_zayavkadoc, date_doc_add, doc_file_name, doc_file_path, tblDT.caption_doctype",
                                      " FROM tblZayavkaDoc tblD",
                                      " LEFT JOIN tblDocType tblDT ON tblD.id_doctype = tblDT.id_doctype",
                                      " WHERE id_zayavka = '", id_zayavka_str, "' AND tblD.id_doctype IN (4, 11)",
                                      " ORDER BY date_doc_add DESC");

                    ASPxGridViewDogovorEnergoDocs.DataBind();
                    if (ASPxGridViewDogovorEnergoDocs.VisibleRowCount > 0)
                        BootstrapPageControl1.TabPages.FindByName("DEnergo").Badge.Text = ASPxGridViewDogovorEnergoDocs.VisibleRowCount.ToString();
                    //-----------------------------------------

                    // информация о договоре (убрать)

                    try
                    {
                        Guid? id_contract = perscabnewEntitiesAdapter.tblContract.Where(p => String.Equals(p.id_zayavka.ToString(), id_zayavka_str)).Select(p => p).First().id_contract;

                        if (id_contract.HasValue)
                        {
                            this.SqlDataSourceContractInfo.SelectCommand =
                                String.Concat("SELECT id_contract, contract_number_1c, contract_date_1c, caption_contractstatus ",
                                              "FROM tblContract tblC ",
                                              "LEFT JOIN tblContractStatus tblCS ON tblC.id_contractstatus = tblCS.id_contractstatus ",
                                              "WHERE id_contract = '", id_contract.ToString(), "'");

                            // документы договора
                            this.SqlDataSourceContractDocs.SelectCommand =
                                String.Concat("SELECT id_zayavkadoc, id_contract, date_doc_add, doc_file_name, doc_file_path, tblDT.caption_doctype, esign_oke, esign_kontr ",
                                              "FROM tblZayavkaDoc tblCD ",
                                              "LEFT JOIN tblDocType tblDT ON tblCD.id_doctype = tblDT.id_doctype ",
                                              "WHERE id_contract = '", id_contract.ToString(), "' AND tblCD.id_doctype <> 2",
                                              " ORDER BY date_doc_add DESC");
                        }
                    }
                    catch (InvalidOperationException)
                    {

                    }
                    //------------------------------------------------------------------

                    // переписка по заявке
                    StringBuilder scChatInfo = new StringBuilder("SELECT id_chatrec, tblZC.id_zayavka as id_zayavka, chatrec_datetime, tblZC.isread as chatrec_isread, tblZC.isread_user as chatrec_isread_user, tblZC.isread_datetime as chatrec_isread_datetime, ", 2000);
                    //scChatInfo.Append("(SELECT user_nameingrid FROM tblZayavka tblZ LEFT JOIN tblUserInfo tblUI ON tblZ.id_user = tblUI.id_user WHERE tblZ.id_zayavka = tblZC.id_zayavka) as user_nameingrid,");
                    scChatInfo.Append("caption_filial_short,id_userrole,user_nameingrid,");
                    scChatInfo.Append("(SELECT caption_userrole FROM tblUserInfo tblUI LEFT JOIN tblUserRole tblUR ON tblUI.id_userrole = tblUR.id_userrole WHERE tblUI.id_user = tblZC.id_user) as caption_userrole,");
                    //scChatInfo.Append("zayavka_number_1c, zayavka_date_1c, caption_msg, ");
                    scChatInfo.Append("caption_msg, ");
                    scChatInfo.AppendFormat("(SELECT COUNT(*) FROM tblZayavkaDoc tblZD WHERE tblZD.id_chatrec = tblZC.id_chatrec) as count_docs ");
                    scChatInfo.Append("FROM tblZayavkaChat tblZC ");
                    scChatInfo.Append("LEFT JOIN tblUserInfo tblUI ON tblZC.id_user = tblUI.id_user ");
                    scChatInfo.Append("LEFT JOIN tblZayavka tblZ ON tblZC.id_zayavka = tblZ.id_zayavka ");
                    scChatInfo.Append("LEFT JOIN tblFilial tblF ON tblUI.id_filial = tblF.id_filial ");
                    scChatInfo.AppendFormat("WHERE tblZC.id_zayavka = '{0}' AND tblZC.istemp IS NULL ", id_zayavka_str);
                    scChatInfo.Append("ORDER BY chatrec_datetime DESC");
                    this.SqlDataSourceChatInfo.SelectCommand = scChatInfo.ToString();

                    this.SqlDataSourceChatDocsInfo.SelectCommand =
                        String.Concat("SELECT * ",
                                      "FROM tblZayavkaDoc ",
                                      "WHERE id_chatrec = CAST(@id_chatrecS as uniqueidentifier) AND is_temp IS NULL",
                                      " ORDER BY date_doc_add DESC");

                    // временные файлы, прикрепляемые к сообщению
                    string id_chatrec = ASPxHiddenFieldChatRecID.Get("chatRecID").ToString();

                    StringBuilder scChatTempDocsInfo = new StringBuilder("SELECT id_zayavkadoc, doc_file_name FROM tblZayavkaDoc ", 200);
                    scChatTempDocsInfo.AppendFormat("WHERE is_temp IS NOT NULL AND id_chatrec = '{0}'", id_chatrec);
                    scChatTempDocsInfo.Append(" ORDER BY doc_file_name ASC");
                    this.SqlDataSourceChatTempDocsInfo.SelectCommand = scChatTempDocsInfo.ToString();

                    /*int count_msgs_not_read = 0;
                    if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER || 
                        user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR)
                    {
                        count_msgs_not_read = perscabnewEntitiesAdapter.tblZayavkaChat
                        .Join(perscabnewEntitiesAdapter.tblUserInfo, tZC => tZC.id_user, tUI => tUI.id_user, (tZC, tUI) => new { tblZayavkaChat = tZC, tblUserInfo = tUI })
                        .Where(p => p.tblZayavkaChat.id_zayavka.ToString() == id_zayavka_str && p.tblZayavkaChat.istemp == null &&
                            p.tblZayavkaChat.isread == false && p.tblUserInfo.id_userrole == (int)MC_PersCab_UserRoleType.MC_urt_USER).Select(p => p).Count();
                    }
                    else
                    if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_USER)                    
                    {
                        count_msgs_not_read = perscabnewEntitiesAdapter.tblZayavkaChat
                        .Join(perscabnewEntitiesAdapter.tblUserInfo, tZC => tZC.id_user, tUI => tUI.id_user, (tZC, tUI) => new { tblZayavkaChat = tZC, tblUserInfo = tUI })
                        .Where(p => p.tblZayavkaChat.id_zayavka.ToString() == id_zayavka_str && p.tblZayavkaChat.istemp == null &&
                            p.tblZayavkaChat.isread == false && p.tblUserInfo.id_userrole != (int)MC_PersCab_UserRoleType.MC_urt_USER).Select(p => p).Count();
                    }*/

                    // выводим кол-во непрочитанных сообщений
                    if (!Page.IsPostBack)
                    {
                        int count_msgs_not_read = MC_PersCab_Chat.CountMessagesNotRead(user_role, id_zayavka);
                        BootstrapPageControl1.TabPages.FindByName("Chat").Badge.Text = count_msgs_not_read > 0 ? count_msgs_not_read.ToString() : String.Empty;
                    }
                    //----------------------------------------------------------------------

                    // настраиваем видимость элементов и доп. функционал в зависимости от роли (ПРИВЕСТИ К ОДНОМУ УСЛОВИЮ)
                    if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER || user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
                    {
                        //ASPxButtonSetFilial.Visible = true;

                        SqlDataSourceUserFilial.SelectCommand = "SELECT id_filial, caption_filial FROM perscabnew.dbo.tblFilial";
                        ASPxComboBoxUserFilial.ValueField = "id_filial";
                        ASPxComboBoxUserFilial.TextField = "caption_filial";
                        ASPxComboBoxUserFilial.DataBind();
                    }
                    else
                    {
                        //ASPxButtonSetFilial.Visible = false;
                    }

                    // скрываем колонки с ФИО работника ОКЭ, отметкой о прочтении для заявителя
                    if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                        user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR)
                    {
                        ASPxGridViewChat.Columns["user_nameingrid"].Visible = true;
                        ASPxGridViewChat.Columns["caption_userrole"].Caption = "Роль";
                        ASPxGridViewChat.Columns["Отметка"].Visible = true;

                        //ASPxButtonAddContractInfo.Visible = ASPxButtonAddContractDoc.Visible = true;

                        // информация о контактном лице (заявителе)
                        tblZayavka userOrder = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka.ToString() == id_zayavka_str).Select(p => p).First();
                        tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.id_user == userOrder.id_user).Select(p => p).First();

                        /*this.SqlDataSourceUserInfo.SelectCommand =
                            String.Concat(
                                "SELECT",
                                " date_create_user, id_user, fl_familiya, fl_name, fl_otchestvo, telefon, email,",
                                " yl_fullname, yl_shortname, inn, contact_familiya, contact_name, contact_otchestvo,",
                                " comment, user_nameingrid, id_registration_typeid",
                                " FROM tblUserInfo",
                                String.Format(" WHERE id_user = '{0}'", userOrder.id_user)
                            );*/
                        //-----------------------------------------

                        StringBuilder userInfoText = new StringBuilder(500);
                        switch (userInfo.id_usertype)
                        {
                            // ФЛ
                            case 0:
                                userInfoText.AppendLine(userInfo.user_nameingrid);
                                userInfoText.AppendLine("физическое лицо");
                                userInfoText.AppendLine(userInfo.telefon);
                                userInfoText.AppendLine(userInfo.email);
                                ASPxMemoUserInfo.Rows = 5;
                                break;

                            // ЮЛ
                            case 1:
                                userInfoText.AppendLine(userInfo.user_nameingrid);
                                userInfoText.AppendLine("юридическое лицо");
                                userInfoText.AppendFormat("Полное наименование: {0}{1}", userInfo.yl_fullname, Environment.NewLine);
                                userInfoText.AppendFormat("ИНН: {0}{1}", userInfo.inn, Environment.NewLine);
                                userInfoText.AppendFormat("Контактное лицо: {0}{1}", String.Concat(userInfo.contact_familiya, " ", userInfo.contact_name, " ", userInfo.contact_otchestvo), Environment.NewLine);
                                userInfoText.AppendLine(userInfo.telefon);
                                userInfoText.AppendLine(userInfo.email);
                                ASPxMemoUserInfo.Rows = 8;
                                break;

                            // ИП
                            case 2:
                                userInfoText.AppendLine(userInfo.user_nameingrid);
                                userInfoText.AppendLine("индивидуальный предприниматель");
                                userInfoText.AppendFormat("ИНН: {0}{1}", userInfo.inn, Environment.NewLine);
                                userInfoText.AppendFormat("Контактное лицо: {0}{1}", String.Concat(userInfo.contact_familiya, " ", userInfo.contact_name, " ", userInfo.contact_otchestvo), Environment.NewLine);
                                userInfoText.AppendLine(userInfo.telefon);
                                userInfoText.AppendLine(userInfo.email);
                                ASPxMemoUserInfo.Rows = 7;
                                break;
                        }
                        ASPxMemoUserInfo.Text = userInfoText.ToString();
                        ASPxMemoUserInfo.Visible = true;

                        // устанавливаем заголовок страницы
                        Page.Title = userInfo.user_nameingrid;
                    }
                    else
                    {
                        ASPxGridViewChat.Columns["user_nameingrid"].Visible = false;
                        ASPxGridViewChat.Columns["caption_userrole"].Caption = "Отправитель";
                        ASPxGridViewChat.Columns["Отметка"].Visible = false;

                        //ASPxButtonAddContractInfo.Visible = ASPxButtonAddContractDoc.Visible = false;
                        ASPxTextBoxOrderGUID.Visible = ASPxMemoUserInfo.Visible = false;

                        // устанавливаем заголовок страницы
                        Page.Title = "Моя заявка";
                    }

                    // доступность кнопки Присвоить номер/Статус
                    if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER || user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
                    {
                        SqlDataSourceOrderStatus.SelectCommand = "SELECT id_zayavkastatus, caption_zayavkastatus FROM perscabnew.dbo.tblZayavkaStatus";
                        ASPxComboBoxOrderStatus.ValueField = "id_zayavkastatus";
                        ASPxComboBoxOrderStatus.TextField = "caption_zayavkastatus";
                        ASPxComboBoxOrderStatus.DataBind();
                        //ASPxButtonSetOrderNumber.Visible = true;
                    }
                    else
                    {
                        //ASPxButtonSetOrderNumber.Visible = false;
                    }

                    // устанавливаем доступность элементов управления
                    ASPxButtonSetOrderNumber.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR ||
                                                        user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                                                        user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);

                    ASPxButtonSetFilial.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER || user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);

                    ASPHyperLinkAddDogovorEnergoInfo.NavigateUrl = String.Concat("~/Cabinet/DogovorEnergoInfo?cmd=edit&id=", id_zayavka_str);
                    ASPHyperLinkAddDogovorEnergoInfo.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);

                    BootstrapPageControl1.TabPages.FindByName("Z1C").Visible = true;// (user_role != (int)MC_PersCab_UserRoleType.MC_urt_USER);

                    //-------------------------------------------------
                }
                else
                {
                    Response.Redirect("~", true);
                }
            } // if (Page.User.Identity.IsAuthenticated)
            else
            {
                //Response.Redirect("~", true);                
                string strReturnUrl = HttpUtility.UrlEncode(String.Format("{0}?{1}", Request.Path, Request.QueryString.ToString()));
                Response.Redirect(String.Format("~/Account/Login?ReturnUrl={0}", strReturnUrl), true);
            }

        } // Page_Load(object sender, EventArgs e)


        // нажатие кнопки отправки сообщения
        protected void ASPxButtonSendMessage_Click(object sender, EventArgs e)
        {
            /* проверяем - включен ли режим обслуживания */
            bool pcMaintenanceMode = bool.Parse(WebConfigurationManager.AppSettings["pcMaintenanceMode"]);
            if (pcMaintenanceMode)
            {
                Response.Redirect("~/Maintenance.aspx", true);
            }
            else
            {
                perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();

                string user_login = Page.User.Identity.Name;
                tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

                // формируем сообщение (проверяя на наличие с признаком istemp
                Guid id_chatrec = (Guid)ASPxHiddenFieldChatRecID.Get("chatRecID");
                tblZayavkaChat userMessage;

                try
                {
                    //userMessage = perscabnewEntitiesAdapter.tblZayavkaChat.Where(p => Guid.Equals(p.id_chatrec, id_chatrec) && p.istemp == true).Select(p => p).First();
                    userMessage = perscabnewEntitiesAdapter.tblZayavkaChat.Where(p => p.id_chatrec == id_chatrec && p.istemp == true).Select(p => p).First();

                    userMessage.caption_msg = ASPxMemoChat.Text;
                    userMessage.istemp = null;
                    userMessage.chatrec_datetime = DateTime.Now;
                    userMessage.isread = false;
                }
                catch (InvalidOperationException) // запись не найдена
                {
                    userMessage = new tblZayavkaChat();
                    userMessage.id_chatrec = id_chatrec;
                    userMessage.id_zayavka = Guid.Parse(Request.QueryString["id"]);
                    userMessage.id_user = userInfo.id_user;
                    userMessage.caption_msg = ASPxMemoChat.Text;
                    userMessage.istemp = null;
                    userMessage.chatrec_datetime = DateTime.Now;
                    userMessage.isread = false;                    
                    perscabnewEntitiesAdapter.tblZayavkaChat.Add(userMessage);
                }

                perscabnewEntitiesAdapter.SaveChanges();

                // снимаем признак is_temp
                IQueryable<tblZayavkaDoc> zayavkaDocsTemp = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => p.id_chatrec == userMessage.id_chatrec).Select(p => p);
                foreach (tblZayavkaDoc docTemp in zayavkaDocsTemp)
                {
                    docTemp.is_temp = null;
                }
                perscabnewEntitiesAdapter.SaveChanges();

                // уведомляем о поступлении сообщения
                tblZayavka orderTemp = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == userMessage.id_zayavka).Select(p => p).First();

                if (userInfo.id_userrole == 0) // если сообщение отправляет заявитель
                {
                    MC_PersCab_Notify.AddNotify(MC_PersCab_Consts.notifyType_Sys_ChatFromZayavitel, userMessage.id_chatrec, 1, null, MC_PersCab_Consts.notifyChannel_Email, false);

                    /*try
                    {
                        MailMessage mail = new MailMessage();

                        //mail.From = new MailAddress("admin@oke38.ru", "ОГУЭП \"Облкоммунэнерго\"");
                        mail.From = new MailAddress("admin@oke38.ru", "ОГУЭП \"Облкоммунэнерго\"", Encoding.GetEncoding(1251));

                        IQueryable<tblUserInfo> listOperators = perscabnewEntitiesAdapter.tblUserInfo.
                            Where(p => (p.id_userrole == 3) || (p.id_userrole == 1) || (p.id_userrole == 2 && p.id_filial == orderTemp.id_filial)).Select(p => p);
                        if (listOperators.Count() > 0)
                        {
                            foreach (tblUserInfo userOperator in listOperators)
                            {
                                MailAddress ma = new MailAddress(userOperator.email);
                                if (!mail.To.Contains(ma)) mail.To.Add(ma);
                            }
                            //mail.To.Add(new MailAddress("it@oblkomenergo.ru", "it@oblkomenergo.ru")); // копия письма на ящик администратора

                            mail.Subject = "Личный кабинет ТП (поступило новое сообщение)";
                            mail.IsBodyHtml = true;

                            mail.Body = "<div><p>Автоматическое уведомление. Не отвечайте на данное письмо.</p></div>";
                            mail.Body += "<div><p>Поступило новое сообщение.</p></div>";
                            mail.Body += String.Format("<div><p>Заявитель: </p><p>{0}</p></div>",
                                perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.id_user == orderTemp.id_user).Select(p => p).First().user_nameingrid);
                            mail.Body += String.Format("<div><p>Номер заявки: </p><p>{0}</p></div>", orderTemp.zayavka_number_1c);
                            mail.Body += String.Format("<div><p>Сообщение: </p><p>{0}</p></div>", userMessage.caption_msg);

                            SmtpClient smtp = new SmtpClient("mail.nic.ru", 2525);
                            smtp.Credentials = new NetworkCredential("admin@oke38.ru", "77pHmX5TgPZ3c");

                            smtp.Send(mail);
                        }
                    }
                    catch (Exception)
                    {
                        
                    }*/
                }
                else // если сообщение отправляет администратор или оператор
                {
                    // если email заявителя не пустой
                    string emailUser = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.id_user == orderTemp.id_user).Select(p => p).First().email;
                    if (!String.IsNullOrEmpty(emailUser))
                    {
                        MC_PersCab_Notify.AddNotify(MC_PersCab_Consts.notifyType_Sys_ChatToZayavitel, userMessage.id_chatrec, 1, orderTemp.id_user, MC_PersCab_Consts.notifyChannel_Email, false);                        
                    }
                    else
                    {
                        MC_PersCab_Notify.AddNotify(MC_PersCab_Consts.notifyType_Sys_ChatToZayavitel, userMessage.id_chatrec, 1, orderTemp.id_user, MC_PersCab_Consts.notifyChannel_SMS, false);
                    }

                } // else if (userInfo.id_userrole == 0) // если сообщение отправляет завитель
                  //----------------------------------------------

                // новая генерация id_chatrec
                ASPxHiddenFieldChatRecID.Set("chatRecID", Guid.NewGuid());

                // обновляем grid-ы
                ASPxGridViewChat.DataBind();
                ASPxGridViewOrderDocs.DataBind();
                ASPxGridViewChatTempDocs.DataBind();

                ASPxMemoChat.Text = "";
                ASPxButtonSendMessage.Text = "Отправить сообщение";

            } // if (pcMaintenanceMode)
        } // protected void ASPxButtonSendMessage_Click(object sender, EventArgs e)

        protected void ASPxUploadControlChat_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            if (!e.IsValid) e.CallbackData = string.Empty;

            // копируем файл                
            Guid TPZayavkaID = Guid.Parse(Request.QueryString["id"]);

            DateTime CurrDate = DateTime.Now;

            string orderDocPath, docFileName, docFileFullName;

            MC_PersCab_FileIO.MC_PersCab_FileIO_CreateZayavkaDocPath(TPZayavkaID, e.UploadedFile.FileName, CurrDate,
                                                                       out orderDocPath, out docFileName, out docFileFullName);

            e.UploadedFile.SaveAs(docFileFullName, false);
            
            e.CallbackData = e.UploadedFile.FileName;

            //----------------------------------------------

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

            // формируем временное сообщение для прикрепления файлов, в случае его отсутствия
            Guid id_chatrec = (Guid)ASPxHiddenFieldChatRecID.Get("chatRecID");
            tblZayavkaChat userTempMessage;
            try
            {
                userTempMessage = perscabnewEntitiesAdapter.tblZayavkaChat.Where(p => p.id_chatrec == id_chatrec).Select(p => p).First();
            }
            catch (InvalidOperationException) // запись не найдена
            {
                userTempMessage = new tblZayavkaChat();
                userTempMessage.id_chatrec = id_chatrec;
                userTempMessage.id_zayavka = TPZayavkaID;
                userTempMessage.id_user = userInfo.id_user;
                userTempMessage.istemp = true;
                userTempMessage.chatrec_datetime = DateTime.Now;
                perscabnewEntitiesAdapter.tblZayavkaChat.Add(userTempMessage);
                perscabnewEntitiesAdapter.SaveChanges();
            }

            // добавляем запись в таблицу документов c установкой флага is_temp 1
            tblZayavkaDoc zayavkaDoc = new tblZayavkaDoc();
            zayavkaDoc.id_zayavkadoc = Guid.NewGuid();
            zayavkaDoc.id_doctype = 0;
            zayavkaDoc.id_zayavka = TPZayavkaID;
            zayavkaDoc.doc_file_name = docFileName;// e.UploadedFile.FileName;
            zayavkaDoc.doc_file_path = orderDocPath;
            zayavkaDoc.date_doc_add = DateTime.Now;
            zayavkaDoc.is_temp = true;
            zayavkaDoc.id_chatrec = id_chatrec;
            zayavkaDoc.id_user = userInfo.id_user;
            perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDoc);
            perscabnewEntitiesAdapter.SaveChanges();
        }

        // используется для связки Master-Detail гридов   
        protected void ASPxGridViewChatDocs_BeforePerformDataSelect(object sender, EventArgs e)
        {
            Session["id_chatrecS"] = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        }

        /*// формирование поля "Наличие вложений"
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
                //((ASPxImage)sender).ImageUrl = "Images/no.jpg";

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

        // формирование поля "Подписан исполнителем (ОКЭ)"
        protected void imgEsignOke_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxImage)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string esign_oke = container.Grid.GetRowValues(currentIndex, "esign_oke").ToString();

            if (!String.IsNullOrWhiteSpace(esign_oke))
            {
                ((ASPxImage)sender).ImageUrl = "~/Content/Images/ok_green.png";
            }
            else
            {

            }
        }

        // формирование поля "Подписан заказчиком (контрагентом)"
        protected void imgEsignKontr_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxImage)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string esign_kontr = container.Grid.GetRowValues(currentIndex, "esign_kontr").ToString();

            if (!String.IsNullOrWhiteSpace(esign_kontr))
            {
                ((ASPxImage)sender).ImageUrl = "~/Content/Images/ok_green.png";
            }
            else
            {

            }
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

        // формирование поля "посмотреть" документа заявки из 1С
        protected void urlViewOrderDoc_Init_1С(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxHyperLink)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;            
            string doc_id = "notfound";
            try
            {
                doc_id = BitConverter.ToString((byte[])container.Grid.GetRowValues(currentIndex, "_IDRRef")).Replace("-", "");                
            }
            catch (IndexOutOfRangeException)
            {
                doc_id = "notfound";
            }

            ((ASPxHyperLink)sender).NavigateUrl = String.Concat("~/Cabinet/ViewDoc2?doctype=order&docid=", doc_id);
        }

        // формирование поля "скачать" документа заявки из 1С
        protected void urlDownloadOrderDoc_Init_1С(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxHyperLink)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;

            string doc_id = "notfound";
            try
            {
                doc_id = BitConverter.ToString((byte[])container.Grid.GetRowValues(currentIndex, "_IDRRef")).Replace("-", "");
            }
            catch (IndexOutOfRangeException)
            {
                doc_id = "notfound";
            }

            ((ASPxHyperLink)sender).NavigateUrl = String.Concat("~/Cabinet/DownloadDoc2?doctype=order&docid=", doc_id);
        }

        // формирование поля "скачать" документа договора
        protected void urlDownloadContractDoc_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxHyperLink)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string doc_id = container.Grid.GetRowValues(currentIndex, "id_zayavkadoc").ToString();

            ((ASPxHyperLink)sender).NavigateUrl = String.Concat("~/Cabinet/DownloadDoc.aspx?doctype=contract&docid=", doc_id);
        }

        // формирование поля "подписать" документа договора
        protected void urlSignContractDoc_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxHyperLink)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string doc_id = container.Grid.GetRowValues(currentIndex, "id_zayavkadoc").ToString();

            ((ASPxHyperLink)sender).NavigateUrl = String.Concat("~/Cabinet/ContractDocInfo.aspx?doctype=contract&docid=", doc_id);
        }

        // формирование поля "статус договора ТП"
        protected void field_StatusDogovora_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((Label)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;

            byte[] D_1C_Status = (byte[])container.Grid.GetRowValues(currentIndex, "D_1C_Status");

            perscabnewEntities lkEntitiesAdapter = new perscabnewEntities();
            ((Label)sender).Text = 
                lkEntitiesAdapter.tblContractStatus.Where(p => p.id_contractstatus_1c == D_1C_Status).Select(p => p).First().caption_contractstatus;            
        }

        // удаление временного документа чата
        protected void ASPxGridViewChatTempDocs_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string id_zayavkadoc = e.Keys[ASPxGridViewChatTempDocs.KeyFieldName].ToString();

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblZayavkaDoc docTemp = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_zayavkadoc)).Select(p => p).First();
            MC_PersCab_FileIO.MC_PersCab_FileIO_DeleteFile(docTemp.doc_file_name, docTemp.doc_file_path);
            perscabnewEntitiesAdapter.tblZayavkaDoc.Remove(docTemp);
            perscabnewEntitiesAdapter.SaveChanges();

            e.Cancel = true;
        }

        // нажатие на кнопку "добавить (изменить)"
        protected void ASPxButtonAddContractInfo_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Concat("~/Cabinet/ContractInfo.aspx?cmd=edit&id=", Request.QueryString["id"]));
        }

        // нажатие на кнопку "добавить документ"
        protected void ASPxButtonAddContractDoc_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Concat("~/Cabinet/ContractInfo.aspx?cmd=view&id=", Request.QueryString["id"]));
        }

        // callback - выбор филиала
        protected void callbackPanelSetFilial_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
        {
            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblZayavka orderTemp = perscabnewEntitiesAdapter.tblZayavka.Where(p => String.Equals(p.id_zayavka.ToString(), e.Parameter.ToString())).Select(p => p).First();
            ASPxComboBoxUserFilial.Value = orderTemp.id_filial;
        }

        // OK - закрепление заявки за филиалом
        protected void ASPxButtonSetFilialOk_Click(object sender, EventArgs e)
        {
            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            Guid id_order_GUID = Guid.Parse(ASPxTextBoxOrderGUID.Text);

            tblZayavka orderTemp = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == id_order_GUID).Select(p => p).First();
            orderTemp.id_filial = (int)ASPxComboBoxUserFilial.SelectedItem.Value;
            perscabnewEntitiesAdapter.SaveChanges();
            ASPxGridViewOrderInfo.DataBind();

            // уведомляем операторов филиала о поступлении (делегировании) заявки
            MC_PersCab_Notify.AddNotify(MC_PersCab_Consts.notifyType_Sys_ZakreplenFilial, orderTemp.id_zayavka, 1, null, MC_PersCab_Consts.notifyChannel_Email, true);

            /*try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("admin@oke38.ru", "ОГУЭП \"Облкоммунэнерго\"", Encoding.GetEncoding(1251));

                IQueryable<tblUserInfo> listOperators = perscabnewEntitiesAdapter.tblUserInfo.Where(p => (p.id_userrole == 2) && (p.id_filial == orderTemp.id_filial)).Select(p => p);
                if (listOperators.Count() > 0)
                {
                    foreach (tblUserInfo userOperator in listOperators)
                    {
                        MailAddress ma = new MailAddress(userOperator.email);
                        if (!mail.To.Contains(ma)) mail.To.Add(ma);
                    }
                    mail.To.Add(new MailAddress("it@oblkomenergo.ru", "it@oblkomenergo.ru")); // копия письма на ящик администратора

                    mail.Subject = "Личный кабинет ТП (закреплена новая заявка)";
                    mail.IsBodyHtml = true;

                    mail.Body = "<div><p>Автоматическое уведомление. Не отвечайте на данное письмо.</p></div>";
                    mail.Body += "<div><p>Закреплена новая заявка.</p></div>";
                    mail.Body += String.Format("<div><p>Заявитель: </p><p>{0}</p></div>",
                        perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.id_user == orderTemp.id_user).Select(p => p).First().user_nameingrid);

                    SmtpClient smtp = new SmtpClient("mail.nic.ru", 2525);
                    smtp.Credentials = new NetworkCredential("admin@oke38.ru", "77pHmX5TgPZ3c");

                    smtp.Send(mail);
                }
            }
            catch (Exception)
            {
                
            }*/
        }

        // callback - чтение номера/даты/статуса
        protected void callbackPanelSetOrderNumber_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
        {
            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            Guid id_order_GUID = Guid.Parse(ASPxTextBoxOrderGUID.Text);
            tblZayavka orderTemp = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == id_order_GUID).Select(p => p).First();
            
            tbOrderNumber.Text = orderTemp.zayavka_number_1c;
            tbOrderDate.Date = Convert.ToDateTime(orderTemp.zayavka_date_1c);
            if (orderTemp.v1C_StatusZayavki != null)
                ASPxComboBoxOrderStatus.Value =
                    perscabnewEntitiesAdapter.tblZayavkaStatus.Where(p => p.id_zayavkastatus_1c == orderTemp.v1C_StatusZayavki).Select(p => p).First().id_zayavkastatus;            
        }

        // OK - сохранение номера/даты/статуса заявки
        protected void ASPxButtonSetOrderNumberOk_Click(object sender, EventArgs e)
        {
            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            Guid id_order_GUID = Guid.Parse(ASPxTextBoxOrderGUID.Text);
            
            tblZayavka orderTemp = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == id_order_GUID).Select(p => p).First();

            string zayavitelTemp = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.id_user == orderTemp.id_user).Select(p => p).First().user_nameingrid;

            orderTemp.zayavka_number_1c = tbOrderNumber.Text;
            if (String.IsNullOrWhiteSpace(tbOrderDate.Text)) orderTemp.zayavka_date_1c = null;
            else orderTemp.zayavka_date_1c = tbOrderDate.Date;

            /*int id_orderstatus = (int)ASPxComboBoxOrderStatus.SelectedItem.Value;
            orderTemp.id_zayavkastatus = id_orderstatus;*/

            int id_zayavkastatus = (int)ASPxComboBoxOrderStatus.SelectedItem.Value;            
            orderTemp.v1C_StatusZayavki = perscabnewEntitiesAdapter.tblZayavkaStatus.Where(p => p.id_zayavkastatus == id_zayavkastatus).Select(p => p).First().id_zayavkastatus_1c;

            /*
            // формируем уведомления для ГП (сделать отдельный класс)
            // если новая заявка не аннулирована
            bool statusZayavkaAnnulirovana =
                (orderTemp.v1C_StatusZayavki == perscabnewEntitiesAdapter.tblZayavkaStatus.Where(p => p.caption_zayavkastatus == "аннулированная").Select(p => p).First().id_zayavkastatus_1c)
                || (orderTemp.v1C_StatusZayavki == perscabnewEntitiesAdapter.tblZayavkaStatus.Where(p => p.caption_zayavkastatus == "аннулированная (неоплата)").Select(p => p).First().id_zayavkastatus_1c);
            if (!statusZayavkaAnnulirovana)
            {
                // добавляем запись в таблицу уведомлений
                tblNotify notifyNew = new tblNotify();
                notifyNew.id_notify = Guid.NewGuid();
                notifyNew.link_id = orderTemp.id_zayavka;
                notifyNew.id_notifytype = perscabnewEntitiesAdapter.tblNotifyType.Where(p => p.caption_notifytype == "ГП (зарегистрирована новая заявка)").Select(p => p).First().id_notifytype;
                notifyNew.date_create_notify = DateTime.Now;
                notifyNew.notifystatus_id = perscabnewEntitiesAdapter.tblNotifyStatus.Where(p => p.caption_notifystatus == "в очереди").Select(p => p).First().id_notifystatus;
                notifyNew.priority = 1;
                notifyNew.recipient_id = null; // пока убрал - при отправлении выберу филиал из заявки в link_id
                notifyNew.id_notifychannel =
                        perscabnewEntitiesAdapter.tblNotifyChannel.Where(p => p.caption_notifychannel == "email").Select(p => p).First().id_notifychannel;

                notifyNew.notify_text =
                                    String.Concat(
                            "<p>ОГУЭП \"Облкоммунэнерго\" уведомляет Вас о том, что в целях исполнения п 105 ПП РФ 861 (в редакции ПП РФ 262) в личном кабинете зарегистрирована новая заявка.</p>",
                            //"<p>Заявитель: ", zayavka1c["_Fld26191"].ToString(), " [физическое лицо / индивидуальный предприниматель / юридическое лицо]. Номер заявки: ", zayavkaNew.zayavka_number_1c, ". Дата заявки: ", zayavkaNew.zayavka_date_1c.ToString(), ".</p>",
                            "<p>Заявитель: ", zayavitelTemp, ". Номер заявки: ", orderTemp.zayavka_number_1c, ". Дата заявки: ", orderTemp.zayavka_date_1c.ToString(), ".</p>",
                            //"<p>Наименование энергопринимающего устройства: ", zayavkaNew.v1C_EPU, ". Место нахождения энергопринимающего устройства: ", zayavkaNew.v1C_adresEPU, ". Мощность Рmax: ", zayavkaNew.v1C_maxMoschnostEPU, "Уровень напряжения:.</p>",
                            "<p>Наименование энергопринимающего устройства: ", orderTemp.v1C_EPU, ". Место нахождения энергопринимающего устройства: ", orderTemp.v1C_adresEPU, ". Мощность Рmax: ", orderTemp.v1C_maxMoschnostEPU, ".</p>",
                            "<p>Просим разместить в личном кабинете заявителя в течение 10 рабочих дней:</p>",
                            "<p>- проект договора, обеспечивающего продажу электрической энергии(мощности) на розничном рынке, подписанный усиленной квалифицированной электронной подписью уполномоченного лица гарантирующего поставщика</p>",
                            "<p>- наименование и платежные реквизиты гарантирующего поставщика, а также информацию о номере лицевого счета заявителя в случае если заявителем выступает физическое лицо в целях технологического присоединения энергопринимающих устройств, которые используются для бытовых и иных нужд, не связанных с осуществлением предпринимательской деятельности, и электроснабжение которых предусматривается по одному источнику</p>"
                                    );
                //-----------------------------------------                                    
                perscabnewEntitiesAdapter.tblNotify.Add(notifyNew);
            }
            //------------------ завершение формирование уведомлений для ГП*/

            perscabnewEntitiesAdapter.SaveChanges();

            /*// сохраняем статус, предварительно проверяя, что такая запись отсутствует
            tblZayavkaStatusHistory orderStatusHistory = new tblZayavkaStatusHistory(); 

            int id_orderstatus = (int)ASPxComboBoxOrderStatus.SelectedItem.Value;

            try
            {
                orderStatusHistory = perscabnewEntitiesAdapter.tblZayavkaStatusHistory.Where(p => p.id_zayavka == id_order_GUID && p.id_zayavkastatus == id_orderstatus).Select(p => p).First();
            }
            catch (InvalidOperationException) // отсутствует запись о статусе заявки
            {

                orderStatusHistory.id_zayavka = id_order_GUID;
                orderStatusHistory.id_zayavkastatus = id_orderstatus;
                orderStatusHistory.date_status = DateTime.Now;

                tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == Page.User.Identity.Name).Select(p => p).First();
                orderStatusHistory.id_user = userInfo.id_user;

                perscabnewEntitiesAdapter.tblZayavkaStatusHistory.Add(orderStatusHistory);
                perscabnewEntitiesAdapter.SaveChanges();
            }*/

            //----------------------------------------------------------------------------

            ASPxGridViewOrderInfo.DataBind();
        } // protected void ASPxButtonSetOrderNumberOk_Click(object sender, EventArgs e)

        // раскраска строк в зависимости от отправителя сообщения
        protected void ASPxGridViewChat_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            // фон строки
            if (e.RowType != GridViewRowType.Data) return;
            string value = e.GetValue("caption_userrole").ToString();
            if (value.Equals("заявитель"))
            {
                e.Row.BackColor = Color.Wheat;
            }

            // жирный шрифт непрочитанного сообщения
            string user_login = Page.User.Identity.Name;
            int user_role_login = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);

            int user_role_chatrec = Convert.ToInt32(e.GetValue("id_userrole"));
            string value_isread_str = e.GetValue("chatrec_isread").ToString();
            bool value_isread = String.IsNullOrEmpty(value_isread_str) ? true : Convert.ToBoolean(value_isread_str);
            if (!value_isread)
            {
                if (user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                    user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR)
                {
                    e.Row.Font.Bold = (user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_USER);
                }
                else if (user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_USER)
                {
                    e.Row.Font.Bold =
                        (user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                        user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR);
                }
            }
        }

        /*// нажатие на кнопку "редактировать" договора энергоснабжения
        protected void ASPxButtonAddDogovorEnergoInfo_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Concat("~/Cabinet/DogovorEnergoInfo?cmd=edit&id=", Request.QueryString["id"]));
        }*/

        // callback - "быстрые сообщения"
        protected void CallbackPanelFastMessages_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (!String.IsNullOrEmpty(e.Parameter))
            {
                ASPxMemoChat.Text =
                    String.Concat(
                        "Вами при размещении документов в личном кабинете «___»_________20__года не представлены следующие сведения (документы):",
                        Environment.NewLine,
                        "1)", Environment.NewLine,
                        "2)", Environment.NewLine,
                        "3)", Environment.NewLine,
                        "Просим Вас в соответствии с п. 10 ПП РФ №-861 представить недостающие сведения(документы) в течение 20 рабочих дней со дня получения указанного уведомления.",
                        Environment.NewLine,
                        "До получения недостающих сведений(документов) ОГУЭП Облкоммунэнерго приостанавливает рассмотрение Вашей заявки.",
                        Environment.NewLine,
                        "Уведомляем Вас, что в случае непредставления заявителем недостающих сведений(документов) в течение 20 рабочих дней со дня получения указанного уведомления заявка будет аннулирована."
                        );
            }
        } // protected void CallbackPanelFastMessages_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)

        // установка видимости кнопок "сообщение прочитано"
        protected void ButtonChatMessage_SetReadOk_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((BootstrapButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            
            string user_login = Page.User.Identity.Name;
            int user_role_login = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);

            int user_role_chatrec = Convert.ToInt32(container.Grid.GetRowValues(currentIndex, "id_userrole"));
            string value_isread_str = container.Grid.GetRowValues(currentIndex, "chatrec_isread").ToString();
            bool value_isread = String.IsNullOrEmpty(value_isread_str) ? true : Convert.ToBoolean(value_isread_str);
            ((BootstrapButton)sender).Visible = false;
            if (!value_isread) 
            {
                if (user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                    user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
                {
                    ((BootstrapButton)sender).Visible = (user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_USER);
                }
                else if (user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_USER)
                {
                    ((BootstrapButton)sender).Visible =
                        (user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                        user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR);
                }
            }
        } // protected void ButtonChatMessage_SetReadOk_Init(object sender, EventArgs e)

        // установка видимости кнопок "сообщение не прочитано"
        protected void ButtonChatMessage_SetReadNo_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((BootstrapButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;

            string user_login = Page.User.Identity.Name;
            int user_role_login = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);

            int user_role_chatrec = Convert.ToInt32(container.Grid.GetRowValues(currentIndex, "id_userrole"));
            string value_isread_str = container.Grid.GetRowValues(currentIndex, "chatrec_isread").ToString();
            bool value_isread = String.IsNullOrEmpty(value_isread_str) ? true : Convert.ToBoolean(value_isread_str);
            ((BootstrapButton)sender).Visible = false;
            if (value_isread)
            {
                if (user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                    user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR)
                {
                    ((BootstrapButton)sender).Visible = (user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_USER);
                }
                else if (user_role_login == (int)MC_PersCab_UserRoleType.MC_urt_USER)
                {
                    ((BootstrapButton)sender).Visible =
                        (user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                        user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role_chatrec == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR);
                }
            }
        } // protected void ButtonChatMessage_SetReadNo_Init(object sender, EventArgs e)

        // кнопки "сообщение прочитано"
        protected void ButtonChatMessage_SetReadOk_Click(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((BootstrapButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            Guid id_chatrec = Guid.Parse(container.Grid.GetRowValues(currentIndex, "id_chatrec").ToString());
            
            perscabnewEntities lkAdapter = new perscabnewEntities();
            tblZayavkaChat userMessage = lkAdapter.tblZayavkaChat.Where(p => p.id_chatrec == id_chatrec).Select(p => p).First();
            tblUserInfo userInfo = lkAdapter.tblUserInfo.Where(p => p.user_login == Page.User.Identity.Name).Select(p => p).First();
            
            userMessage.isread = true;
            userMessage.isread_user = userInfo.id_user;
            userMessage.isread_datetime = DateTime.Now;

            lkAdapter.SaveChanges();
            ASPxGridViewChat.DataBind();

            Guid id_zayavka = Guid.Parse(Request.QueryString["id"].ToString());
            int count_msgs_not_read = MC_PersCab_Chat.CountMessagesNotRead(userInfo.id_userrole, id_zayavka);
            BootstrapPageControl1.TabPages.FindByName("Chat").Badge.Text = count_msgs_not_read > 0 ? count_msgs_not_read.ToString() : String.Empty;
        } // protected void ButtonChatMessage_SetReadOk_Click(object sender, EventArgs e)

        // кнопки "сообщение не прочитано"
        protected void ButtonChatMessage_SetReadNo_Click(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((BootstrapButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            Guid id_chatrec = Guid.Parse(container.Grid.GetRowValues(currentIndex, "id_chatrec").ToString());

            perscabnewEntities lkAdapter = new perscabnewEntities();
            tblZayavkaChat userMessage = lkAdapter.tblZayavkaChat.Where(p => p.id_chatrec == id_chatrec).Select(p => p).First();
            tblUserInfo userInfo = lkAdapter.tblUserInfo.Where(p => p.user_login == Page.User.Identity.Name).Select(p => p).First();

            userMessage.isread = false;
            userMessage.isread_user = userInfo.id_user;
            userMessage.isread_datetime = DateTime.Now;

            lkAdapter.SaveChanges();
            ASPxGridViewChat.DataBind();

            Guid id_zayavka = Guid.Parse(Request.QueryString["id"].ToString());
            int count_msgs_not_read = MC_PersCab_Chat.CountMessagesNotRead(userInfo.id_userrole, id_zayavka);
            BootstrapPageControl1.TabPages.FindByName("Chat").Badge.Text = count_msgs_not_read > 0 ? count_msgs_not_read.ToString() : String.Empty;
        } // protected void ButtonChatMessage_SetReadNo_Click(object sender, EventArgs e)

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

        // формирование поля "Комментарий (чат - отметка о прочтении)"
        protected void ASPxLabelIsReadStamp_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxLabel)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;

            perscabnewEntities lkAdapter = new perscabnewEntities();
            string value_isreaduser_str = container.Grid.GetRowValues(currentIndex, "chatrec_isread_user").ToString();

            if (!String.IsNullOrEmpty(value_isreaduser_str))
            {
                Guid chatrec_isread_user = Guid.Parse(value_isreaduser_str);
                tblUserInfo userInfo_ChatRecIsRead =
                    lkAdapter.tblUserInfo.Where(p => p.id_user == chatrec_isread_user).Select(p => p).First();

                string value_isread_str = container.Grid.GetRowValues(currentIndex, "chatrec_isread").ToString();
                bool value_isread = String.IsNullOrEmpty(value_isread_str) ? true : Convert.ToBoolean(value_isread_str);

                ((ASPxLabel)sender).Text =
                    String.Concat(
                        value_isread ? "прочитано" : "не прочитано", ", ",
                        userInfo_ChatRecIsRead.user_nameingrid, ", ",
                        container.Grid.GetRowValues(currentIndex, "chatrec_isread_datetime").ToString()
                        );
            }
        } // protected void ASPxLabelIsReadStamp_Init(object sender, EventArgs e)

    }

}