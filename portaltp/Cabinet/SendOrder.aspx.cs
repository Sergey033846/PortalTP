using DatabaseFirst;
using mcperscab;
using DevExpress.Web;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Security.AccessControl;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp.Cabinet
{
    public partial class SendOrder : System.Web.UI.Page
    {
        const string UploadDirectory = "~/App_Data/UploadTemp/";

        Guid TPZayavkaID;

        protected void Page_Load(object sender, EventArgs e)
        {
            // формируем параметры сессии
            if (!IsPostBack)
            {
                TPZayavkaID = Guid.NewGuid();
                ASPxHiddenFieldZayavkaID.Set("TPzayavkaID", TPZayavkaID);
            }

            /*perscabnewEntities eee = new perscabnewEntities();
            tblDocType dt = new tblDocType();
            dt.id_doctype = 13;
            dt.caption_doctype = "entity";
            eee.tblDocType.Add(dt);
            eee.SaveChanges();*/

            string user_login = Page.User.Identity.Name;
            if (Page.User.Identity.IsAuthenticated)            
            {
                perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
                tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

                /*TPZayavkaID = Guid.NewGuid();
                ASPxHiddenFieldZayavkaID.Set("TPzayavkaID", TPZayavkaID);*/

                /*zayavkaInfoOnPage = new myEntityZayavka();
                zayavkaInfoOnPage.id_zayavka = Guid.NewGuid();*/

                //Guid TPOrderGuid = Guid.NewGuid();
                //Session["orderNumber"] = Guid.NewGuid();

                /*DateTime CurrDate = DateTime.Now;
                string orderDocsPath = String.Concat(DateTime.Now.Year.ToString(), "\\");
                if (CurrDate.Month < 10) orderDocsPath = String.Concat(orderDocsPath, "0", CurrDate.Month.ToString());
                else orderDocsPath = String.Concat(orderDocsPath, CurrDate.Month.ToString());
                orderDocsPath += "\\";
                if (CurrDate.Day < 10) orderDocsPath = String.Concat(orderDocsPath, "0", CurrDate.Day.ToString());
                else orderDocsPath = String.Concat(orderDocsPath, CurrDate.Day.ToString());
                orderDocsPath = String.Concat(orderDocsPath, "\\", TPZayavkaID.ToString());*/

                //ASPxTextBox1.Text = TPZayavkaID.ToString();                
                //----------------------------------

                // формируем список доступных заявок, в зависимости от типа пользователя и его роли

                string connectionString = WebConfigurationManager.ConnectionStrings["PerscabDbConnection"].ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);

                string queryString = String.Concat("SELECT id_user, id_userrole, id_usertype FROM tblUserInfo WHERE email = @email");
                SqlCommand cmd = new SqlCommand(queryString, connection);
                cmd.Parameters.AddWithValue("@email", user_login);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable datasetUserInfo = new DataTable();
                adapter.Fill(datasetUserInfo);

                /*
                 queryString = String.Concat("SELECT id_zayavkatype, caption_zayavkatype FROM tblZayavkaType WHERE id_usertype = @idusertype");
                 cmd = new SqlCommand(queryString, connection);
                 cmd.Parameters.AddWithValue("@idusertype", datasetUserInfo.Rows[0]["id_usertype"]);
                 adapter = new SqlDataAdapter();
                 adapter.SelectCommand = cmd;
                 DataTable datasetZayavkaTypeValid = new DataTable();
                 adapter.Fill(datasetZayavkaTypeValid);*/

                if (!IsPostBack)
                {
                    /*SqlDataSourceZayavkaType.SelectCommand = String.Concat("SELECT id_zayavkatype, caption_zayavkatype FROM tblZayavkaType WHERE id_usertype = ", datasetUserInfo.Rows[0]["id_usertype"].ToString());
                    ASPxRadioButtonListZayavkaType.ValueField = "id_zayavkatype";
                    ASPxRadioButtonListZayavkaType.TextField = "caption_zayavkatype";
                    ASPxRadioButtonListZayavkaType.DataBind();
                    if (ASPxRadioButtonListZayavkaType.Items.Count > 0) ASPxRadioButtonListZayavkaType.SelectedIndex = 0;*/
                }
                else
                {

                }

                /*foreach (DataRow rowZt in datasetZayavkaTypeValid.Rows)
                 {
                     ASPxRadioButtonListZayavkaType.Items.Add(rowZt["caption_zayavkatype"].ToString(), rowZt["id_zayavkatype"]);
                 }*/

                //if (datasetZayavkaTypeValid.Rows.Count > 0) ASPxRadioButtonListZayavkaType.SelectedIndex = 0;
                //if (ASPxRadioButtonListZayavkaType.Items.Count > 0) ASPxRadioButtonListZayavkaType.SelectedIndex = 0;

                //Session["zayavkaType"] = ASPxRadioButtonListZayavkaType.SelectedIndex;
                //zayavkaInfoOnPage.id_zayavkatype = ASPxRadioButtonListZayavkaType.SelectedIndex;

                connection.Close();

                // временные файлы, прикрепляемые к заявке                
                TPZayavkaID = (Guid)ASPxHiddenFieldZayavkaID.Get("TPzayavkaID");

                StringBuilder scOrderTempDocsInfo0 = new StringBuilder("SELECT id_zayavkadoc, doc_file_name FROM tblZayavkaDoc ", 200);
                scOrderTempDocsInfo0.AppendFormat("WHERE is_temp IS NOT NULL AND id_zayavka = '{0}' AND id_doctype = 0", TPZayavkaID.ToString());
                scOrderTempDocsInfo0.Append(" ORDER BY doc_file_name ASC");

                StringBuilder scOrderTempDocsInfo5 = new StringBuilder("SELECT id_zayavkadoc, doc_file_name FROM tblZayavkaDoc ", 200);
                scOrderTempDocsInfo5.AppendFormat("WHERE is_temp IS NOT NULL AND id_zayavka = '{0}' AND id_doctype = 5", TPZayavkaID.ToString());
                scOrderTempDocsInfo5.Append(" ORDER BY doc_file_name ASC");

                StringBuilder scOrderTempDocsInfo6 = new StringBuilder("SELECT id_zayavkadoc, doc_file_name FROM tblZayavkaDoc ", 200);
                scOrderTempDocsInfo6.AppendFormat("WHERE is_temp IS NOT NULL AND id_zayavka = '{0}' AND id_doctype = 6", TPZayavkaID.ToString());
                scOrderTempDocsInfo6.Append(" ORDER BY doc_file_name ASC");

                StringBuilder scOrderTempDocsInfo7 = new StringBuilder("SELECT id_zayavkadoc, doc_file_name FROM tblZayavkaDoc ", 200);
                scOrderTempDocsInfo7.AppendFormat("WHERE is_temp IS NOT NULL AND id_zayavka = '{0}' AND id_doctype = 7", TPZayavkaID.ToString());
                scOrderTempDocsInfo7.Append(" ORDER BY doc_file_name ASC");

                StringBuilder scOrderTempDocsInfo8 = new StringBuilder("SELECT id_zayavkadoc, doc_file_name FROM tblZayavkaDoc ", 200);
                scOrderTempDocsInfo8.AppendFormat("WHERE is_temp IS NOT NULL AND id_zayavka = '{0}' AND id_doctype = 8", TPZayavkaID.ToString());
                scOrderTempDocsInfo8.Append(" ORDER BY doc_file_name ASC");

                StringBuilder scOrderTempDocsInfo9 = new StringBuilder("SELECT id_zayavkadoc, doc_file_name FROM tblZayavkaDoc ", 200);
                scOrderTempDocsInfo9.AppendFormat("WHERE is_temp IS NOT NULL AND id_zayavka = '{0}' AND id_doctype = 9", TPZayavkaID.ToString());
                scOrderTempDocsInfo9.Append(" ORDER BY doc_file_name ASC");

                StringBuilder scOrderTempDocsInfo10 = new StringBuilder("SELECT id_zayavkadoc, doc_file_name FROM tblZayavkaDoc ", 200);
                scOrderTempDocsInfo10.AppendFormat("WHERE is_temp IS NOT NULL AND id_zayavka = '{0}' AND id_doctype = 10", TPZayavkaID.ToString());
                scOrderTempDocsInfo10.Append(" ORDER BY doc_file_name ASC");

                this.SqlDataSourceOrderTempDocsInfo0.SelectCommand = scOrderTempDocsInfo0.ToString();
                this.SqlDataSourceOrderTempDocsInfo5.SelectCommand = scOrderTempDocsInfo5.ToString();
                this.SqlDataSourceOrderTempDocsInfo6.SelectCommand = scOrderTempDocsInfo6.ToString();
                this.SqlDataSourceOrderTempDocsInfo7.SelectCommand = scOrderTempDocsInfo7.ToString();
                this.SqlDataSourceOrderTempDocsInfo8.SelectCommand = scOrderTempDocsInfo8.ToString();
                this.SqlDataSourceOrderTempDocsInfo9.SelectCommand = scOrderTempDocsInfo9.ToString();
                this.SqlDataSourceOrderTempDocsInfo10.SelectCommand = scOrderTempDocsInfo10.ToString();

                // заполняем комбобоксы
                SqlDataSourceFIASRegionCity.SelectCommand = "SELECT id_FIASRegionCity, caption_FIASRegionCity, id_filial FROM tblFIASRegionCity ORDER BY caption_FIASRegionCity";
                ComboBoxFIASRegionCity.ValueField = "id_FIASRegionCity";
                ComboBoxFIASRegionCity.TextField = "caption_FIASRegionCity";
                ComboBoxFIASRegionCity.DataBind();

                SqlDataSourcePrichinaPodachiZ.SelectCommand = "SELECT id_prichinapodachiz, caption_short FROM tblPrichinaPodachiZ ORDER BY caption_short";
                ComboBoxPrichinaPodachiZ.ValueField = "id_prichinapodachiz";
                ComboBoxPrichinaPodachiZ.TextField = "caption_short";
                ComboBoxPrichinaPodachiZ.DataBind();

                SqlDataSourceUrovenU.SelectCommand = "SELECT _EnumOrder, caption_long FROM tbl1C_EnumUrovenU ORDER BY _EnumOrder";
                ComboBoxUrovenU.ValueField = "_EnumOrder";
                ComboBoxUrovenU.TextField = "caption_long";
                ComboBoxUrovenU.DataBind();

                SqlDataSourceVidRassrochki.SelectCommand = "SELECT _EnumOrder, caption_long FROM tbl1C_VidRassrochki ORDER BY _EnumOrder";
                ComboBoxVidRassrochki.ValueField = "_EnumOrder";
                ComboBoxVidRassrochki.TextField = "caption_long";
                ComboBoxVidRassrochki.DataBind();
                //ComboBoxVidRassrochki.SelectedIndex = 0; // 100%  

                SqlDataSourceGP.SelectCommand = "SELECT id_gp, caption_GP FROM tblGP";
                ComboBoxGP.ValueField = "id_gp";
                ComboBoxGP.TextField = "caption_GP";
                ComboBoxGP.DataBind();
                //ComboBoxGP.SelectedIndex = 0; // Иркутскэнергосбыт

                /*SqlDataSourceGPCenovayaKategoriya.SelectCommand = "SELECT _EnumOrder, caption_short FROM tbl1C_CenovayaKategoriya ORDER BY _EnumOrder";
                ComboBoxCenovayaKategoriya.ValueField = "_EnumOrder";
                ComboBoxCenovayaKategoriya.TextField = "caption_short";
                ComboBoxCenovayaKategoriya.DataBind();*/
                //ComboBoxCenovayaKategoriya.SelectedIndex = 0; // ЦК определяет ГП

                if (!IsPostBack)
                {
                    ComboBoxVidRassrochki.SelectedIndex = 0; // 100%  
                    ComboBoxGP.SelectedIndex = 0; // Иркутскэнергосбыт
                    //ComboBoxCenovayaKategoriya.SelectedIndex = 0; // ЦК определяет ГП
                }
                else
                {                    
                    
                }
                
                //---------------------

                // устанавливаем доступность элементов управления
                int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);
                //InfoRassrochka.Visible = ComboBoxVidRassrochki.Visible = (user_role != (int)MC_PersCab_UserRoleType.MC_urt_USER) || userInfo.id_usertype != 0;

                btnCreateOrder.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_USER);

            } // if (Page.User.Identity.IsAuthenticated)
            else
            {
                Response.Redirect("~");
            }
                        
        } // protected void Page_Load(object sender, EventArgs e)

        /*protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            //Session["zayavkaType"] = ASPxRadioButtonListZayavkaType.SelectedItem.Value;
        }*/

        protected void btnCreateOrder_Click(object sender, EventArgs e)
        {
            /* проверяем - включен ли режим обслуживания */
            bool pcMaintenanceMode = bool.Parse(WebConfigurationManager.AppSettings["pcMaintenanceMode"]);
            if (pcMaintenanceMode)
            {
                Response.Redirect("~/Maintenance.aspx", true);
            }
            else
            {
                string user_login = Page.User.Identity.Name;
                TPZayavkaID = (Guid)ASPxHiddenFieldZayavkaID.Get("TPzayavkaID");

                perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();

                tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();
                //tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.email == user_login).Select(p => p).First();

                // проверяем загруженность документов по заявке во временной таблице
                // добавить сюда проверку по видам 8,5,6 **************************
                int zayavkaDocCount = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => p.id_zayavka == TPZayavkaID).Select(p => p).Count();

                if (zayavkaDocCount == 0)
                {
                    ASPxLabelErrorInfo.Text = "Ошибка создания заявки: загрузите необходимые документы!";
                    ASPxLabelErrorInfo.Visible = true;
                }
                /*else
                if (Convert.ToInt32(ASPxTextBoxMaxMoschnost.Value) >= 670 && ComboBoxCenovayaKategoriya.SelectedIndex != 0)
                {
                    ASPxLabelErrorInfo.Text = "Внимание! При мощности 670 кВт и более ценовую категорию определяет Гарантирующий поставщик. Измените ценовую категорию или мощность.";
                    ASPxLabelErrorInfo.Visible = true;
                }*/
                else
                {
                    tblZayavka zayavkaTP = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == TPZayavkaID).Select(p => p).First();

                    //zayavkaTP.id_zayavkatype = (int)ASPxRadioButtonListZayavkaType.SelectedItem.Value;
                    zayavkaTP.date_create_zayavka = DateTime.Now;
                    zayavkaTP.is_temp = null;
                    //zayavkaTP.id_zayavkastatus = 1; // "поступившая" - сделать через перечисление в своих классах                    
                    zayavkaTP.v1C_StatusZayavki = 
                        perscabnewEntitiesAdapter.tblZayavkaStatus.Where(p => p.caption_zayavkastatus == "поступившая").Select(p => p).First().id_zayavkastatus_1c;

                    zayavkaTP.id_prichinapodachiz = Guid.Parse(ComboBoxPrichinaPodachiZ.SelectedItem.Value.ToString());

                    zayavkaTP.id_FIASRegionCity = Guid.Parse(ComboBoxFIASRegionCity.SelectedItem.Value.ToString());
                    zayavkaTP.v1C_adresEPU = ASPxTextBoxAddressEPU2.Text;
                    zayavkaTP.v1C_maxMoschnostEPU = Convert.ToInt32(ASPxTextBoxMaxMoschnost.Text);
                                        
                    zayavkaTP.id_filial = perscabnewEntitiesAdapter.tblFIASRegionCity.Where(p => p.id_FIASRegionCity == zayavkaTP.id_FIASRegionCity).First().id_filial;

                    decimal id_urovenU = Convert.ToDecimal(ComboBoxUrovenU.SelectedItem.Value);
                    zayavkaTP.id1C_EnumUrovenU = perscabnewEntitiesAdapter.tbl1C_EnumUrovenU.Where(p => p.C_EnumOrder == id_urovenU).Select(p => p).First().C_IDRref;

                    decimal id_vidOplaty = Convert.ToDecimal(ComboBoxVidRassrochki.SelectedItem.Value);
                    zayavkaTP.id1C_VidRassrochki = perscabnewEntitiesAdapter.tbl1C_VidRassrochki.Where(p => p.C_EnumOrder == id_vidOplaty).Select(p => p).First().C_IDRref;

                    zayavkaTP.id_gp = Guid.Parse(ComboBoxGP.SelectedItem.Value.ToString());

                    /*decimal id_cenovayakategoriya = Convert.ToDecimal(ComboBoxCenovayaKategoriya.SelectedItem.Value);
                    zayavkaTP.id1C_CenovayaKategoriya = perscabnewEntitiesAdapter.tbl1C_CenovayaKategoriya.Where(p => p.C_EnumOrder == id_cenovayakategoriya).Select(p => p).First().C_IDRref;*/

                    if (zayavkaTP.v1C_maxMoschnostEPU <= 15)
                    {
                        zayavkaTP.id_zayavkatype = (userInfo.id_usertype == 0) ? 0 : 2;
                    }
                    else
                    {
                        zayavkaTP.id_zayavkatype = (userInfo.id_usertype == 0) ? 1 : 2;
                    }
                    // что делать с 1С-овским статусом?

                    //perscabnewEntitiesAdapter.SaveChanges();
                    //----------------------

                    perscabnewEntitiesAdapter.SaveChanges();

                    // снимаем признак is_temp с документов заявки
                    IQueryable<tblZayavkaDoc> zayavkaDocsTemp = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => p.id_zayavka == TPZayavkaID).Select(p => p);
                    foreach (tblZayavkaDoc docTemp in zayavkaDocsTemp)
                    {
                        docTemp.is_temp = null;
                    }

                    perscabnewEntitiesAdapter.SaveChanges();

                    // отправляем сообщение операторам (АУП) о поступлении заявки
                    //MC_PersCab_Notify.AddNotify(MC_PersCab_Consts.notifyType_Sys_NewZ, zayavkaTP.id_zayavka, 1, null, MC_PersCab_Consts.notifyChannel_Email, true);

                    // уведомляем операторов филиала о поступлении (делегировании) заявки
                    MC_PersCab_Notify.AddNotify(MC_PersCab_Consts.notifyType_Sys_ZakreplenFilial, zayavkaTP.id_zayavka, 1, null, MC_PersCab_Consts.notifyChannel_Email, true);

                    /*try
                    {
                        MailMessage mail = new MailMessage();

                        //mail.From = new MailAddress("admin@oke38.ru", "ОГУЭП \"Облкоммунэнерго\"");
                        mail.From = new MailAddress("admin@oke38.ru", "ОГУЭП \"Облкоммунэнерго\"", Encoding.GetEncoding(1251));

                        IQueryable<tblUserInfo> listOperators = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.id_userrole == 3).Select(p => p);
                        foreach (tblUserInfo userOperator in listOperators)
                        {
                            mail.To.Add(new MailAddress(userOperator.email));
                        }
                        mail.To.Add(new MailAddress("it@oblkomenergo.ru", "it@oblkomenergo.ru")); // копия письма на ящик администратора

                        mail.Subject = "Личный кабинет ТП (поступила новая заявка)";
                        mail.IsBodyHtml = true;

                        mail.Body = "<div><p>Автоматическое уведомление. Не отвечайте на данное письмо.</p></div>";
                        mail.Body += "<div><p>Поступила новая заявка.</p></div>";
                        mail.Body += String.Format("<div><p>Заявитель: </p><p>{0}</p></div>", userInfo.user_nameingrid);

                        SmtpClient smtp = new SmtpClient("mail.nic.ru", 2525);
                        smtp.Credentials = new NetworkCredential("admin@oke38.ru", "77pHmX5TgPZ3c");

                        smtp.Send(mail);
                    }
                    catch (Exception)
                    {
                        
                    }*/
                    //-----------------------------------------

                    //Response.Redirect("~/Account/SendOrderSuccess.aspx?email=" + tbEmail.Text);
                    Response.Redirect("~/Cabinet/SendOrderSuccess.aspx");
                }

            } // if (pcMaintenanceMode)
        } // protected void btnCreateOrder_Click(object sender, EventArgs e)

        // удаление временного документа заявки
        protected void ASPxGridViewOrderTempDocsForAll_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string id_zayavkadoc = e.Keys[ASPxGridViewOrderTempDocs10.KeyFieldName].ToString();

            /*// ASPxGridViewOrderTempDocs8
            int idDocType = Convert.ToInt32((sender as ASPxGridView).ID.Replace("ASPxGridViewOrderTempDocs", String.Empty));*/
            
            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            
            /*tblZayavkaDoc docTemp = perscabnewEntitiesAdapter.tblZayavkaDoc.
                Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_zayavkadoc) && p.id_doctype == 10).Select(p => p).First();*/

            tblZayavkaDoc docTemp = perscabnewEntitiesAdapter.tblZayavkaDoc.
                Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_zayavkadoc)).Select(p => p).First();

            MC_PersCab_FileIO.MC_PersCab_FileIO_DeleteFile(docTemp.doc_file_name, docTemp.doc_file_path);

            perscabnewEntitiesAdapter.tblZayavkaDoc.Remove(docTemp);
            perscabnewEntitiesAdapter.SaveChanges();

            e.Cancel = true;
        }
        /*
        protected void ASPxGridViewOrderTempDocs10_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string id_zayavkadoc = e.Keys[ASPxGridViewOrderTempDocs10.KeyFieldName].ToString();

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblZayavkaDoc docTemp = perscabnewEntitiesAdapter.tblZayavkaDoc.
                Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_zayavkadoc) && p.id_doctype == 10).Select(p => p).First();
            perscabnewEntitiesAdapter.tblZayavkaDoc.Remove(docTemp);
            perscabnewEntitiesAdapter.SaveChanges();

            e.Cancel = true;
        }

        protected void ASPxGridViewOrderTempDocs8_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string id_zayavkadoc = e.Keys[ASPxGridViewOrderTempDocs8.KeyFieldName].ToString();

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblZayavkaDoc docTemp = perscabnewEntitiesAdapter.tblZayavkaDoc.
                Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_zayavkadoc) && p.id_doctype == 8).Select(p => p).First();
            perscabnewEntitiesAdapter.tblZayavkaDoc.Remove(docTemp);
            perscabnewEntitiesAdapter.SaveChanges();

            e.Cancel = true;
        }
                
        protected void ASPxGridViewOrderTempDocs5_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string id_zayavkadoc = e.Keys[ASPxGridViewOrderTempDocs5.KeyFieldName].ToString();

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblZayavkaDoc docTemp = perscabnewEntitiesAdapter.tblZayavkaDoc.
                Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_zayavkadoc) && p.id_doctype == 5).Select(p => p).First();
            perscabnewEntitiesAdapter.tblZayavkaDoc.Remove(docTemp);
            perscabnewEntitiesAdapter.SaveChanges();

            e.Cancel = true;
        }

        protected void ASPxGridViewOrderTempDocs6_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string id_zayavkadoc = e.Keys[ASPxGridViewOrderTempDocs6.KeyFieldName].ToString();

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblZayavkaDoc docTemp = perscabnewEntitiesAdapter.tblZayavkaDoc.
                Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_zayavkadoc) && p.id_doctype == 6).Select(p => p).First();
            perscabnewEntitiesAdapter.tblZayavkaDoc.Remove(docTemp);
            perscabnewEntitiesAdapter.SaveChanges();

            e.Cancel = true;
        }

        protected void ASPxGridViewOrderTempDocs7_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string id_zayavkadoc = e.Keys[ASPxGridViewOrderTempDocs7.KeyFieldName].ToString();

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblZayavkaDoc docTemp = perscabnewEntitiesAdapter.tblZayavkaDoc.
                Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_zayavkadoc) && p.id_doctype == 7).Select(p => p).First();
            perscabnewEntitiesAdapter.tblZayavkaDoc.Remove(docTemp);
            perscabnewEntitiesAdapter.SaveChanges();

            e.Cancel = true;
        }

        protected void ASPxGridViewOrderTempDocs9_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string id_zayavkadoc = e.Keys[ASPxGridViewOrderTempDocs9.KeyFieldName].ToString();

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblZayavkaDoc docTemp = perscabnewEntitiesAdapter.tblZayavkaDoc.
                Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_zayavkadoc) && p.id_doctype == 9).Select(p => p).First();
            perscabnewEntitiesAdapter.tblZayavkaDoc.Remove(docTemp);
            perscabnewEntitiesAdapter.SaveChanges();

            e.Cancel = true;
        }
        protected void ASPxGridViewOrderTempDocs0_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string id_zayavkadoc = e.Keys[ASPxGridViewOrderTempDocs0.KeyFieldName].ToString();

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblZayavkaDoc docTemp = perscabnewEntitiesAdapter.tblZayavkaDoc.
                Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_zayavkadoc) && p.id_doctype == 0).Select(p => p).First();
            perscabnewEntitiesAdapter.tblZayavkaDoc.Remove(docTemp);
            perscabnewEntitiesAdapter.SaveChanges();

            e.Cancel = true;
        }*/

        protected void ASPxUploadControlOrderForAll_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            if (!e.IsValid) e.CallbackData = string.Empty;

            // копируем файл                
            TPZayavkaID = (Guid)ASPxHiddenFieldZayavkaID.Get("TPzayavkaID");

            DateTime CurrDate = DateTime.Now;

            string orderDocPath, docFileName, docFileFullName;

            MC_PersCab_FileIO.MC_PersCab_FileIO_CreateZayavkaDocPath(TPZayavkaID, e.UploadedFile.FileName, CurrDate, 
                                                                       out orderDocPath, out docFileName, out docFileFullName);

            e.UploadedFile.SaveAs(docFileFullName, false);

            e.CallbackData = e.UploadedFile.FileName;

            //----------------------------------------------

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

            // формируем временную заявку для прикрепления файлов, в случае её отсутствия        
            tblZayavka userTempOrder;
            try
            {
                userTempOrder = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == TPZayavkaID).Select(p => p).First();
            }
            catch (InvalidOperationException) // запись не найдена
            {
                userTempOrder = new tblZayavka();
                userTempOrder.id_user = userInfo.id_user;
                userTempOrder.id_zayavka = TPZayavkaID;

                userTempOrder.id_zayavkatype = 0; // !!!

                userTempOrder.comment = null;
                userTempOrder.zayavka_number_1c = null;
                userTempOrder.zayavka_date_1c = null;
                userTempOrder.viewed_operator = null;
                userTempOrder.date_create_zayavka = DateTime.Now;
                userTempOrder.is_viewed = false;
                userTempOrder.is_temp = true;
                userTempOrder.id_ordersource = MC_PersCab_Consts.orderSource_LK;

                userTempOrder.v1C_Zayavitel = userInfo.user_nameingrid;

                perscabnewEntitiesAdapter.tblZayavka.Add(userTempOrder);
                perscabnewEntitiesAdapter.SaveChanges();
            }

            // добавляем запись в таблицу документов c установкой флага is_temp 1
            tblZayavkaDoc zayavkaDoc = new tblZayavkaDoc();
            zayavkaDoc.id_zayavkadoc = Guid.NewGuid();

            // ASPxUploadControlOrder0
            string docTypeStr = (sender as ASPxUploadControl).ID.Replace("ASPxUploadControlOrder", String.Empty);
            zayavkaDoc.id_doctype = Convert.ToInt32(docTypeStr); //8; 

            zayavkaDoc.id_zayavka = TPZayavkaID;
            zayavkaDoc.doc_file_name = docFileName;
            zayavkaDoc.doc_file_path = orderDocPath;
            zayavkaDoc.date_doc_add = DateTime.Now;
            zayavkaDoc.is_temp = true;
            zayavkaDoc.id_chatrec = null;
            zayavkaDoc.id_user = userInfo.id_user;

            perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDoc);
            perscabnewEntitiesAdapter.SaveChanges();
        } // protected void ASPxUploadControlOrderForAll_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)

        /*
        protected void ASPxUploadControlOrder8_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            if (!e.IsValid) e.CallbackData = string.Empty;
            
            // копируем файл                
            TPZayavkaID = (Guid)ASPxHiddenFieldZayavkaID.Get("TPzayavkaID");
            
            DateTime CurrDate = DateTime.Now;

            string orderDocPath = String.Concat(CurrDate.Year.ToString(), "\\");
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

            // e.UploadedFile.SaveAs(MapPath("Images/" + e.UploadedFile.FileName));
            e.UploadedFile.SaveAs(docFileName, true);

            e.CallbackData = e.UploadedFile.FileName;

            //----------------------------------------------

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

            // формируем временную заявку для прикрепления файлов, в случае её отсутствия        
            tblZayavka userTempOrder;
            try
            {
                userTempOrder = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == TPZayavkaID).Select(p => p).First();
            }
            catch (InvalidOperationException) // запись не найдена
            {
                userTempOrder = new tblZayavka();
                userTempOrder.id_user = userInfo.id_user;
                userTempOrder.id_zayavka = TPZayavkaID;

                userTempOrder.id_zayavkatype = 0; // !!!

                userTempOrder.comment = null;
                userTempOrder.zayavka_number_1c = null;
                userTempOrder.zayavka_date_1c = null;
                userTempOrder.viewed_operator = null;
                userTempOrder.date_create_zayavka = DateTime.Now;
                userTempOrder.is_viewed = false;
                userTempOrder.is_temp = true;

                perscabnewEntitiesAdapter.tblZayavka.Add(userTempOrder);
                perscabnewEntitiesAdapter.SaveChanges();
            }

            // добавляем запись в таблицу документов c установкой флага is_temp 1
            tblZayavkaDoc zayavkaDoc = new tblZayavkaDoc();
            zayavkaDoc.id_zayavkadoc = Guid.NewGuid();
            zayavkaDoc.id_doctype = 8; // ******************************************
            zayavkaDoc.id_zayavka = TPZayavkaID;
            zayavkaDoc.doc_file_name = fileNameGood;//e.UploadedFile.FileName;
            zayavkaDoc.doc_file_path = orderDocPath;
            zayavkaDoc.date_doc_add = DateTime.Now;
            zayavkaDoc.is_temp = true;
            zayavkaDoc.id_chatrec = null;
            zayavkaDoc.id_user = userInfo.id_user;

            perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDoc);
            perscabnewEntitiesAdapter.SaveChanges();
        } // protected void ASPxUploadControlOrder8_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)

        protected void ASPxUploadControlOrder5_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            if (!e.IsValid) e.CallbackData = string.Empty;

            // копируем файл                
            TPZayavkaID = (Guid)ASPxHiddenFieldZayavkaID.Get("TPzayavkaID");

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

            // формируем временную заявку для прикрепления файлов, в случае её отсутствия        
            tblZayavka userTempOrder;
            try
            {
                userTempOrder = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == TPZayavkaID).Select(p => p).First();
            }
            catch (InvalidOperationException) // запись не найдена
            {
                userTempOrder = new tblZayavka();
                userTempOrder.id_user = userInfo.id_user;
                userTempOrder.id_zayavka = TPZayavkaID;
                userTempOrder.id_zayavkatype = 0;
                userTempOrder.comment = null;
                userTempOrder.zayavka_number_1c = null;
                userTempOrder.zayavka_date_1c = null;
                userTempOrder.viewed_operator = null;
                userTempOrder.date_create_zayavka = DateTime.Now;
                userTempOrder.is_viewed = false;
                userTempOrder.is_temp = true;

                perscabnewEntitiesAdapter.tblZayavka.Add(userTempOrder);
                perscabnewEntitiesAdapter.SaveChanges();
            }

            // добавляем запись в таблицу документов c установкой флага is_temp 1
            tblZayavkaDoc zayavkaDoc = new tblZayavkaDoc();
            zayavkaDoc.id_zayavkadoc = Guid.NewGuid();
            zayavkaDoc.id_doctype = 5; // ******************************************
            zayavkaDoc.id_zayavka = TPZayavkaID;
            zayavkaDoc.doc_file_name = fileNameGood;//e.UploadedFile.FileName;
            zayavkaDoc.doc_file_path = orderDocPath;
            zayavkaDoc.date_doc_add = DateTime.Now;
            zayavkaDoc.is_temp = true;
            zayavkaDoc.id_chatrec = null;
            zayavkaDoc.id_user = userInfo.id_user;

            perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDoc);
            perscabnewEntitiesAdapter.SaveChanges();
        } // protected void ASPxUploadControlOrder5_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)

        protected void ASPxUploadControlOrder6_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            if (!e.IsValid) e.CallbackData = string.Empty;

            // копируем файл                
            TPZayavkaID = (Guid)ASPxHiddenFieldZayavkaID.Get("TPzayavkaID");

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

            // формируем временную заявку для прикрепления файлов, в случае её отсутствия        
            tblZayavka userTempOrder;
            try
            {
                userTempOrder = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == TPZayavkaID).Select(p => p).First();
            }
            catch (InvalidOperationException) // запись не найдена
            {
                userTempOrder = new tblZayavka();
                userTempOrder.id_user = userInfo.id_user;
                userTempOrder.id_zayavka = TPZayavkaID;
                userTempOrder.id_zayavkatype = 0;
                userTempOrder.comment = null;
                userTempOrder.zayavka_number_1c = null;
                userTempOrder.zayavka_date_1c = null;
                userTempOrder.viewed_operator = null;
                userTempOrder.date_create_zayavka = DateTime.Now;
                userTempOrder.is_viewed = false;
                userTempOrder.is_temp = true;

                perscabnewEntitiesAdapter.tblZayavka.Add(userTempOrder);
                perscabnewEntitiesAdapter.SaveChanges();
            }

            // добавляем запись в таблицу документов c установкой флага is_temp 1
            tblZayavkaDoc zayavkaDoc = new tblZayavkaDoc();
            zayavkaDoc.id_zayavkadoc = Guid.NewGuid();
            zayavkaDoc.id_doctype = 6; // ******************************************
            zayavkaDoc.id_zayavka = TPZayavkaID;
            zayavkaDoc.doc_file_name = fileNameGood;//e.UploadedFile.FileName;
            zayavkaDoc.doc_file_path = orderDocPath;
            zayavkaDoc.date_doc_add = DateTime.Now;
            zayavkaDoc.is_temp = true;
            zayavkaDoc.id_chatrec = null;
            zayavkaDoc.id_user = userInfo.id_user;

            perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDoc);
            perscabnewEntitiesAdapter.SaveChanges();
        } // protected void ASPxUploadControlOrder6_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)

        protected void ASPxUploadControlOrder7_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            if (!e.IsValid) e.CallbackData = string.Empty;

            // копируем файл                
            TPZayavkaID = (Guid)ASPxHiddenFieldZayavkaID.Get("TPzayavkaID");

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

            // формируем временную заявку для прикрепления файлов, в случае её отсутствия        
            tblZayavka userTempOrder;
            try
            {
                userTempOrder = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == TPZayavkaID).Select(p => p).First();
            }
            catch (InvalidOperationException) // запись не найдена
            {
                userTempOrder = new tblZayavka();
                userTempOrder.id_user = userInfo.id_user;
                userTempOrder.id_zayavka = TPZayavkaID;
                userTempOrder.id_zayavkatype = 0;
                userTempOrder.comment = null;
                userTempOrder.zayavka_number_1c = null;
                userTempOrder.zayavka_date_1c = null;
                userTempOrder.viewed_operator = null;
                userTempOrder.date_create_zayavka = DateTime.Now;
                userTempOrder.is_viewed = false;
                userTempOrder.is_temp = true;

                perscabnewEntitiesAdapter.tblZayavka.Add(userTempOrder);
                perscabnewEntitiesAdapter.SaveChanges();
            }

            // добавляем запись в таблицу документов c установкой флага is_temp 1
            tblZayavkaDoc zayavkaDoc = new tblZayavkaDoc();
            zayavkaDoc.id_zayavkadoc = Guid.NewGuid();
            zayavkaDoc.id_doctype = 7; // ******************************************
            zayavkaDoc.id_zayavka = TPZayavkaID;
            zayavkaDoc.doc_file_name = fileNameGood;//e.UploadedFile.FileName;
            zayavkaDoc.doc_file_path = orderDocPath;
            zayavkaDoc.date_doc_add = DateTime.Now;
            zayavkaDoc.is_temp = true;
            zayavkaDoc.id_chatrec = null;
            zayavkaDoc.id_user = userInfo.id_user;

            perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDoc);
            perscabnewEntitiesAdapter.SaveChanges();
        } // protected void ASPxUploadControlOrder7_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)

        protected void ASPxUploadControlOrder9_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            if (!e.IsValid) e.CallbackData = string.Empty;

            // копируем файл                
            TPZayavkaID = (Guid)ASPxHiddenFieldZayavkaID.Get("TPzayavkaID");

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

            // формируем временную заявку для прикрепления файлов, в случае её отсутствия        
            tblZayavka userTempOrder;
            try
            {
                userTempOrder = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == TPZayavkaID).Select(p => p).First();
            }
            catch (InvalidOperationException) // запись не найдена
            {
                userTempOrder = new tblZayavka();
                userTempOrder.id_user = userInfo.id_user;
                userTempOrder.id_zayavka = TPZayavkaID;
                userTempOrder.id_zayavkatype = 0;
                userTempOrder.comment = null;
                userTempOrder.zayavka_number_1c = null;
                userTempOrder.zayavka_date_1c = null;
                userTempOrder.viewed_operator = null;
                userTempOrder.date_create_zayavka = DateTime.Now;
                userTempOrder.is_viewed = false;
                userTempOrder.is_temp = true;

                perscabnewEntitiesAdapter.tblZayavka.Add(userTempOrder);
                perscabnewEntitiesAdapter.SaveChanges();
            }

            // добавляем запись в таблицу документов c установкой флага is_temp 1
            tblZayavkaDoc zayavkaDoc = new tblZayavkaDoc();
            zayavkaDoc.id_zayavkadoc = Guid.NewGuid();
            zayavkaDoc.id_doctype = 9; // ******************************************
            zayavkaDoc.id_zayavka = TPZayavkaID;
            zayavkaDoc.doc_file_name = fileNameGood;//e.UploadedFile.FileName;
            zayavkaDoc.doc_file_path = orderDocPath;
            zayavkaDoc.date_doc_add = DateTime.Now;
            zayavkaDoc.is_temp = true;
            zayavkaDoc.id_chatrec = null;
            zayavkaDoc.id_user = userInfo.id_user;

            perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDoc);
            perscabnewEntitiesAdapter.SaveChanges();
        } // protected void ASPxUploadControlOrder9_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)

        protected void ASPxUploadControlOrder0_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            if (!e.IsValid) e.CallbackData = string.Empty;

            // копируем файл                
            TPZayavkaID = (Guid)ASPxHiddenFieldZayavkaID.Get("TPzayavkaID");

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

            // формируем временную заявку для прикрепления файлов, в случае её отсутствия        
            tblZayavka userTempOrder;
            try
            {
                userTempOrder = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == TPZayavkaID).Select(p => p).First();
            }
            catch (InvalidOperationException) // запись не найдена
            {
                userTempOrder = new tblZayavka();
                userTempOrder.id_user = userInfo.id_user;
                userTempOrder.id_zayavka = TPZayavkaID;
                userTempOrder.id_zayavkatype = 0;
                userTempOrder.comment = null;
                userTempOrder.zayavka_number_1c = null;
                userTempOrder.zayavka_date_1c = null;
                userTempOrder.viewed_operator = null;
                userTempOrder.date_create_zayavka = DateTime.Now;
                userTempOrder.is_viewed = false;
                userTempOrder.is_temp = true;

                perscabnewEntitiesAdapter.tblZayavka.Add(userTempOrder);
                perscabnewEntitiesAdapter.SaveChanges();
            }

            // добавляем запись в таблицу документов c установкой флага is_temp 1
            tblZayavkaDoc zayavkaDoc = new tblZayavkaDoc();
            zayavkaDoc.id_zayavkadoc = Guid.NewGuid();
            zayavkaDoc.id_doctype = 0; // ******************************************
            zayavkaDoc.id_zayavka = TPZayavkaID;
            zayavkaDoc.doc_file_name = fileNameGood;//e.UploadedFile.FileName;
            zayavkaDoc.doc_file_path = orderDocPath;
            zayavkaDoc.date_doc_add = DateTime.Now;
            zayavkaDoc.is_temp = true;
            zayavkaDoc.id_chatrec = null;
            zayavkaDoc.id_user = userInfo.id_user;

            perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDoc);
            perscabnewEntitiesAdapter.SaveChanges();
        } // protected void ASPxUploadControlOrder0_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)

        protected void ASPxUploadControlOrder10_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            string user_login = Page.User.Identity.Name;

            if (!e.IsValid) e.CallbackData = string.Empty;

            // копируем файл                
            TPZayavkaID = (Guid)ASPxHiddenFieldZayavkaID.Get("TPzayavkaID");

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

            // формируем временную заявку для прикрепления файлов, в случае её отсутствия        
            tblZayavka userTempOrder;
            try
            {
                userTempOrder = perscabnewEntitiesAdapter.tblZayavka.Where(p => p.id_zayavka == TPZayavkaID).Select(p => p).First();
            }
            catch (InvalidOperationException) // запись не найдена
            {
                userTempOrder = new tblZayavka();
                userTempOrder.id_user = userInfo.id_user;
                userTempOrder.id_zayavka = TPZayavkaID;
                userTempOrder.id_zayavkatype = 0;
                userTempOrder.comment = null;
                userTempOrder.zayavka_number_1c = null;
                userTempOrder.zayavka_date_1c = null;
                userTempOrder.viewed_operator = null;
                userTempOrder.date_create_zayavka = DateTime.Now;
                userTempOrder.is_viewed = false;
                userTempOrder.is_temp = true;

                perscabnewEntitiesAdapter.tblZayavka.Add(userTempOrder);
                perscabnewEntitiesAdapter.SaveChanges();
            }

            // добавляем запись в таблицу документов c установкой флага is_temp 1
            tblZayavkaDoc zayavkaDoc = new tblZayavkaDoc();
            zayavkaDoc.id_zayavkadoc = Guid.NewGuid();
            zayavkaDoc.id_doctype = 10; // ******************************************
            zayavkaDoc.id_zayavka = TPZayavkaID;
            zayavkaDoc.doc_file_name = fileNameGood;//e.UploadedFile.FileName;
            zayavkaDoc.doc_file_path = orderDocPath;
            zayavkaDoc.date_doc_add = DateTime.Now;
            zayavkaDoc.is_temp = true;
            zayavkaDoc.id_chatrec = null;
            zayavkaDoc.id_user = userInfo.id_user;

            perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDoc);
            perscabnewEntitiesAdapter.SaveChanges();
        } // protected void ASPxUploadControlOrder10_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)*/
    }
}