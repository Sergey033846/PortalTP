using DatabaseFirst;
using mcperscab;
using DevExpress.Web;

using CryptoPro.Sharpei;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp.Cabinet
{
    public partial class ContractInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id_contract = Request.QueryString["id"].ToString(); // решили, что Guid заявки и договора должны совпадать
            Guid id_contract_GUID = Guid.Parse(id_contract);

            string user_login = Page.User.Identity.Name;

            if (!IsPostBack && !IsCallback)
            {
                ASPxHiddenFieldDocType.Set("DocType", "0"); // значение вида документа по умолчанию
            }

            if (user_login.Length > 0)
            {
                int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);

                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                    user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role == (int)MC_PersCab_UserRoleType.MC_urt_USER)
                {
                    perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();

                    string pageCommand = Request.QueryString["cmd"];

                    /*this.SqlDataSourceContractInfo.SelectCommand =
                        String.Concat("SELECT id_contract, contract_number_1c, contract_date_1c, ",
                                "(SELECT tblCS.caption_contractstatus FROM ",
                                "(SELECT TOP 1 tblCSH.id_contractstatus FROM tblContractStatusHistory tblCSH WHERE tblCSH.id_contract = tblC.id_contract ORDER BY tblCSH.date_status DESC) as tblTEMP ",
                                "LEFT JOIN tblContractStatus tblCS ON tblTEMP.id_contractstatus = tblCS.id_contractstatus) as caption_status ",
                                "FROM tblContract tblC ",
                                "WHERE id_contract = '", id_contract, "'");*/

                    this.SqlDataSourceContractInfo.SelectCommand =
                        String.Concat("SELECT id_contract, contract_number_1c, contract_date_1c, caption_contractstatus ",
                                "FROM tblContract tblC ",
                                "LEFT JOIN tblContractStatus tblCS ON tblC.id_contractstatus = tblCS.id_contractstatus ",
                                "WHERE id_contract = '", id_contract, "'");

                    this.SqlDataSourceContractDocs.SelectCommand =
                        String.Concat("SELECT id_zayavkadoc, id_contract, date_doc_add, doc_file_name, doc_file_path, tblDT.caption_doctype, esign_oke, esign_kontr ",
                                        "FROM tblZayavkaDoc tblCD LEFT JOIN tblDocType tblDT ON tblCD.id_doctype = tblDT.id_doctype ",
                                        "WHERE id_contract = '", id_contract.ToString(), "' AND tblCD.id_doctype <> 2",
                                        " ORDER BY date_doc_add DESC");

                    SqlDataSourceDocType.SelectCommand = "SELECT id_doctype, caption_doctype FROM perscabnew.dbo.tblDocType";
                    ASPxComboBoxDocType.ValueField = "id_doctype";
                    ASPxComboBoxDocType.TextField = "caption_doctype";
                    ASPxComboBoxDocType.DataBind();

                    if (String.Equals(pageCommand, "edit"))
                    {
                        // панель для изменения информации о договоре
                        SqlDataSourceContractStatus.SelectCommand = "SELECT id_contractstatus, caption_contractstatus FROM perscabnew.dbo.tblContractStatus";
                        ASPxComboBoxContractStatus.ValueField = "id_contractstatus";
                        ASPxComboBoxContractStatus.TextField = "caption_contractstatus";
                        ASPxComboBoxContractStatus.DataBind();
                        ASPxPanelAddContractInfo.Visible = true;

                        // заполняем значениями, если договор существует
                        if (!IsPostBack)
                        {
                            tblContract orderContract;
                            try
                            {
                                orderContract = perscabnewEntitiesAdapter.tblContract.Where(p => p.id_contract == id_contract_GUID).Select(p => p).First();
                                tbContractNumber.Text = orderContract.contract_number_1c;
                                tbContractDate.Date = Convert.ToDateTime(orderContract.contract_date_1c);

                                try
                                {
                                    ASPxComboBoxContractStatus.Value = orderContract.id_contractstatus;
                                }
                                catch (InvalidOperationException) // отсутствует запись о статусе
                                {
                                }
                            }
                            catch (InvalidOperationException) // отсутствует запись о договоре
                            {
                            }
                        }
                        //-------------------------------------------
                    }
                    else
                    {
                        ASPxPanelAddContractInfo.Visible = false;
                    }
                }
                else
                {
                    ASPxPanelAddContractInfo.Visible = false;
                }

                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER || user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
                {
                    PanelUploadDocControls.Visible = true;
                }
                else if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_USER)
                {
                    PanelUploadDocControls.Visible = false;
                }

            } // if (user_login.Length > 0)   
            else
            {
                Response.Redirect("~");
            }

        }

        // нажатие кнопки "Изменить"
        protected void ASPxButtonChangeContractInfo_Click(object sender, EventArgs e)
        {
            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();

            string id_contract = Request.QueryString["id"].ToString(); // рещили, что Guid заявки и договора должны совпадать
            Guid id_contract_GUID = Guid.Parse(id_contract);

            tblContract orderContract;

            try
            {
                orderContract = perscabnewEntitiesAdapter.tblContract.Where(p => p.id_contract == id_contract_GUID).Select(p => p).First();

                orderContract.contract_number_1c = tbContractNumber.Text;
                orderContract.contract_date_1c = tbContractDate.Date;
            }
            catch (InvalidOperationException) // отсутствует запись о договоре
            {
                orderContract = new tblContract();

                orderContract.id_contract = id_contract_GUID;
                orderContract.id_zayavka = id_contract_GUID;

                orderContract.contract_number_1c = tbContractNumber.Text;
                orderContract.contract_date_1c = tbContractDate.Date;

                perscabnewEntitiesAdapter.tblContract.Add(orderContract);
            }

            perscabnewEntitiesAdapter.SaveChanges();
            //----------------------------------------------------------------------------

            /*// сохраняем статус, предварительно проверяя, что такая запись отсутствует
            tblContractStatusHistory orderContractStatusHistory;

            int id_contractstatus = (int)ASPxComboBoxContractStatus.SelectedItem.Value;

            try
            {
                orderContractStatusHistory = perscabnewEntitiesAdapter.tblContractStatusHistory.Where(p => p.id_contract == id_contract_GUID && p.id_contractstatus == id_contractstatus).Select(p => p).First();
            }
            catch (InvalidOperationException) // отсутствует запись о статусе договора
            {
                orderContractStatusHistory = new tblContractStatusHistory();

                orderContractStatusHistory.id_contract = id_contract_GUID;
                orderContractStatusHistory.id_contractstatus = id_contractstatus;
                orderContractStatusHistory.date_status = DateTime.Now;

                tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == Page.User.Identity.Name).Select(p => p).First();
                orderContractStatusHistory.id_user = userInfo.id_user;

                perscabnewEntitiesAdapter.tblContractStatusHistory.Add(orderContractStatusHistory);
                perscabnewEntitiesAdapter.SaveChanges();
            }*/
            //----------------------------------------------------------------------------

            // обновляем гриды
            ASPxGridViewContractInfo.DataBind();
        }

        protected void ASPxUploadControlDoc_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            if (!e.IsValid) e.CallbackData = string.Empty;

            // копируем файл                
            Guid TPZayavkaID = Guid.Parse(Request.QueryString["id"]);

            DateTime CurrDate = DateTime.Now;
            string orderDocPath = String.Concat(DateTime.Now.Year.ToString(), "\\");
            if (CurrDate.Month < 10) orderDocPath = String.Concat(orderDocPath, "0", CurrDate.Month.ToString());
            else orderDocPath = String.Concat(orderDocPath, CurrDate.Month.ToString());
            orderDocPath = String.Concat(orderDocPath, "\\");
            if (CurrDate.Day < 10) orderDocPath = String.Concat(orderDocPath, "0", CurrDate.Day.ToString());
            else orderDocPath = String.Concat(orderDocPath, CurrDate.Day.ToString());

            string docFileDir = String.Concat(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "App_Data\\UploadTemp\\", orderDocPath, "\\" + TPZayavkaID.ToString());

            DirectoryInfo d = new DirectoryInfo(docFileDir);
            if (d.Exists)
            {
            }
            else
            {
                d.Create();
            }

            // удаляем нежелательные символы в имени файла
            string fileNameGood = e.UploadedFile.FileName;
            fileNameGood = fileNameGood.Replace(',', '_');

            string docFileName = docFileDir + "\\" + fileNameGood;

            e.UploadedFile.SaveAs(docFileName, true);

            e.CallbackData = e.UploadedFile.FileName;

            //----------------------------------------------

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

            tblZayavkaDoc zayavkaDoc = new tblZayavkaDoc();
            zayavkaDoc.id_zayavkadoc = Guid.NewGuid();
            zayavkaDoc.id_doctype = Convert.ToInt32(ASPxHiddenFieldDocType.Get("DocType"));
            zayavkaDoc.id_zayavka = TPZayavkaID;
            zayavkaDoc.doc_file_name = fileNameGood;
            zayavkaDoc.doc_file_path = orderDocPath;
            zayavkaDoc.date_doc_add = DateTime.Now;
            zayavkaDoc.is_temp = null;
            zayavkaDoc.id_chatrec = null;
            zayavkaDoc.id_contract = TPZayavkaID;
            zayavkaDoc.id_user = userInfo.id_user;
            perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDoc);
            perscabnewEntitiesAdapter.SaveChanges();

            //-------------------------------------------------------

            ASPxGridViewContractDocs.DataBind();
        }

        protected void ASPxComboBoxDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASPxHiddenFieldDocType["DocType"] = ASPxComboBoxDocType.SelectedItem.Value.ToString();
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
    }
}