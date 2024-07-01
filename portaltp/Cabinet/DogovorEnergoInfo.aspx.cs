using DatabaseFirst;
using mcperscab;
using DevExpress.Web;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp.Cabinet
{
    public partial class DogovorEnergoInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            if (Page.User.Identity.IsAuthenticated)
            {
                string id_zayavka_str = Request.QueryString["id"].ToString();

                int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);

                ASPxGridViewDogovorEnergoDocs.Columns["Идентификатор"].Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);

                ASPxHyperLinkOpenOrder.HRef = String.Format("~/Cabinet/OrderInfo?id={0}", id_zayavka_str);
                //------------------

                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
                {
                    perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();                    

                    tblZayavka userOrder = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka.ToString() == id_zayavka_str).Select(p => p).First();
                    tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.id_user == userOrder.id_user).Select(p => p).First();
                    Page.Title = String.Concat("ДЭ-",userInfo.user_nameingrid);
                                        
                    // информация о договоре энергоснабжения
                    try
                    {                        
                        string id_dogovor_energo = id_zayavka_str; // id ДЭ = id заявки, отнощение 1 к 1

                        /*if (!IsPostBack)
                        {*/
                            // информация договора энергоснабжения
                            /*this.SqlDataSourceDogovorEnergoInfo.SelectCommand =
                                String.Concat(
                                            "SELECT id_dogovorenergo, id_zayavka, id_gp_predstavitel, id_gp_filial, id_gp, id_cenovayakategoriya, id_prichinade,",
                                            " nomer_ls, nomer_dogovorenergo, nomer_elektroustanovka, date_create_de, date_podpis_de",
                                            " FROM tblGP_DogovorEnergo",
                                            " WHERE id_dogovorenergo = '", id_dogovor_energo.ToString(), "'");*/

                            // документы договора энергоснабжения (берем строго по типу документа № 4 - договор энергоснабжения)
                            this.SqlDataSourceDogovorEnergoDocs.SelectCommand =
                                String.Concat(
                                              "SELECT id_zayavkadoc, date_doc_add, doc_file_name, doc_file_path, tblDT.caption_doctype",
                                              " FROM tblZayavkaDoc tblD",
                                              " LEFT JOIN tblDocType tblDT ON tblD.id_doctype = tblDT.id_doctype",
                                              " WHERE id_zayavka = '", id_dogovor_energo.ToString(), "' AND tblD.id_doctype IN (4, 11)",
                                              " ORDER BY date_doc_add DESC");
                        if (!IsPostBack)
                        { 
                            // если договор найден
                            if (perscabnewEntitiesAdapter.tblGP_DogovorEnergo.Where(p => String.Equals(p.id_dogovorenergo.ToString(), id_dogovor_energo)).Select(p => p).Count() == 1)
                            {
                                tblGP_DogovorEnergo dogovorEnergo =
                                    perscabnewEntitiesAdapter.tblGP_DogovorEnergo.Where(p => String.Equals(p.id_dogovorenergo.ToString(), id_dogovor_energo)).Select(p => p).First();

                                ASPxTextBoxDogovorEnergo_NomerLS.Text = dogovorEnergo.nomer_ls;
                                ASPxTextBoxDogovorEnergo_NomerDogovorEnergo.Text = dogovorEnergo.nomer_dogovorenergo;
                                ASPxTextBoxDogovorEnergo_NomerElektroustanovka.Text = dogovorEnergo.nomer_elektroustanovka;
                                ASPxDateEdit_DogovorEnergoDate.Date = Convert.ToDateTime(dogovorEnergo.date_create_de);
                            }
                            else // если договор не найден
                            {

                            }
                        } // if (!IsPostBack)
                    }
                    catch (InvalidOperationException)
                    {

                    }
                } // if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
                else
                {
                    Response.Redirect("~", true);
                }

                // устанавливаем видимость элементов управления
                ASPxButtonSaveDogovorEnergoInfo.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);
                ASPxUploadControlDogovorEnergo.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);
                ASPxUploadControlPlatRekv.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);
                //---------------------------------------------

            } // if (Page.User.Identity.IsAuthenticated)
            else
            {
                Response.Redirect("~", true);
            }
        }

        // формирование поля "посмотреть" документа договора энергоснабжения
        protected void urlViewOrderDoc_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxHyperLink)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string doc_id = container.Grid.GetRowValues(currentIndex, "id_zayavkadoc").ToString();

            ((ASPxHyperLink)sender).NavigateUrl = String.Concat("~/Cabinet/ViewDoc?doctype=order&docid=", doc_id);
        }

        // формирование поля "скачать" документа договора энергоснабжения
        protected void urlDownloadOrderDoc_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxHyperLink)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string doc_id = container.Grid.GetRowValues(currentIndex, "id_zayavkadoc").ToString();

            ((ASPxHyperLink)sender).NavigateUrl = String.Concat("~/Cabinet/DownloadDoc?doctype=order&docid=", doc_id);
        }

        // сохранение информации о договоре энергоснабжения
        protected void ASPxButtonSaveDogovorEnergoInfo_Click(object sender, EventArgs e)
        {
            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            string id_zayavka = Request.QueryString["id"].ToString();

            try
            {
                string id_dogovor_energo = id_zayavka; // id ДЭ = id заявки, отнощение 1 к 1
                
                // если договор найден, то это - редактирование
                if (perscabnewEntitiesAdapter.tblGP_DogovorEnergo.Where(p => String.Equals(p.id_dogovorenergo.ToString(), id_dogovor_energo)).Select(p => p).Count() == 1)
                {
                    tblGP_DogovorEnergo dogovorEnergo =
                        perscabnewEntitiesAdapter.tblGP_DogovorEnergo.Where(p => String.Equals(p.id_dogovorenergo.ToString(), id_dogovor_energo)).Select(p => p).First();

                    dogovorEnergo.nomer_ls = ASPxTextBoxDogovorEnergo_NomerLS.Text;
                    dogovorEnergo.nomer_dogovorenergo = ASPxTextBoxDogovorEnergo_NomerDogovorEnergo.Text;
                    dogovorEnergo.nomer_elektroustanovka = ASPxTextBoxDogovorEnergo_NomerElektroustanovka.Text;

                    if (String.IsNullOrWhiteSpace(ASPxDateEdit_DogovorEnergoDate.Text)) dogovorEnergo.date_create_de = null;
                    else dogovorEnergo.date_create_de = ASPxDateEdit_DogovorEnergoDate.Date;

                    perscabnewEntitiesAdapter.SaveChanges();
                }
                else // если договор не найден, то это - создание
                {
                    tblGP_DogovorEnergo dogovorEnergo = new tblGP_DogovorEnergo();

                    dogovorEnergo.id_dogovorenergo = Guid.Parse(Request.QueryString["id"]);
                    dogovorEnergo.id_zayavka = dogovorEnergo.id_dogovorenergo;

                    dogovorEnergo.nomer_ls = ASPxTextBoxDogovorEnergo_NomerLS.Text;
                    dogovorEnergo.nomer_dogovorenergo = ASPxTextBoxDogovorEnergo_NomerDogovorEnergo.Text;
                    dogovorEnergo.nomer_elektroustanovka = ASPxTextBoxDogovorEnergo_NomerElektroustanovka.Text;

                    if (String.IsNullOrWhiteSpace(ASPxDateEdit_DogovorEnergoDate.Text)) dogovorEnergo.date_create_de = null;
                    else dogovorEnergo.date_create_de = ASPxDateEdit_DogovorEnergoDate.Date;

                    perscabnewEntitiesAdapter.tblGP_DogovorEnergo.Add(dogovorEnergo);
                    perscabnewEntitiesAdapter.SaveChanges();
                }
            }
            catch (InvalidOperationException)
            {

            }

        } // protected void ASPxButtonSaveDogovorEnergoInfo_Click(object sender, EventArgs e)

        // загрузка файла договора энергоснабжения
        protected void ASPxUploadControlDogovorEnergo_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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
            
            // добавляем запись в таблицу документов
            tblZayavkaDoc zayavkaDoc = new tblZayavkaDoc();
            zayavkaDoc.id_zayavkadoc = Guid.NewGuid();
            zayavkaDoc.id_doctype = 4; // договор энергоснабжения
            zayavkaDoc.id_zayavka = TPZayavkaID;
            zayavkaDoc.doc_file_name = docFileName;// e.UploadedFile.FileName;
            zayavkaDoc.doc_file_path = orderDocPath;
            zayavkaDoc.date_doc_add = DateTime.Now;
            zayavkaDoc.is_temp = null;
            zayavkaDoc.id_chatrec = null;
            zayavkaDoc.id_user = userInfo.id_user;
            perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDoc);
            perscabnewEntitiesAdapter.SaveChanges();

        } // protected void ASPxUploadControlDogovorEnergo_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)

        // загрузка файла платежных реквизитов
        protected void ASPxUploadControlPlatRekv_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

            // добавляем запись в таблицу документов
            tblZayavkaDoc zayavkaDoc = new tblZayavkaDoc();
            zayavkaDoc.id_zayavkadoc = Guid.NewGuid();
            zayavkaDoc.id_doctype = 11; // платежные реквизиты
            zayavkaDoc.id_zayavka = TPZayavkaID;
            zayavkaDoc.doc_file_name = docFileName;// e.UploadedFile.FileName;
            zayavkaDoc.doc_file_path = orderDocPath;
            zayavkaDoc.date_doc_add = DateTime.Now;
            zayavkaDoc.is_temp = null;
            zayavkaDoc.id_chatrec = null;
            zayavkaDoc.id_user = userInfo.id_user;
            perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDoc);
            perscabnewEntitiesAdapter.SaveChanges();

        } // protected void ASPxUploadControlPlatRekv_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)

    }
}