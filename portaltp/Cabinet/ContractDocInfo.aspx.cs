using DatabaseFirst;
using mcperscab;
using DevExpress.Web;
using CryptoPro.Sharpei;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp.Cabinet
{
    public partial class ContractDocInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();

            /*string id_contract = Request.QueryString["docid"].ToString(); // решили, что Guid заявки и договора должны совпадать
            Guid id_contract_GUID = Guid.Parse(id_contract);*/

            //if (!IsPostBack)
            //{
            string id_contractdoc_str = Request.QueryString["docid"].ToString(); // решили, что Guid заявки и договора должны совпадать
            Guid id_contractdoc_GUID = Guid.Parse(id_contractdoc_str);

            // формируем хэш
            if (!IsPostBack)
            {
                ESign_GetHashValue(id_contractdoc_GUID);
                ASPxHiddenFieldESign3.Set("ESign", "undefined");

                StringBuilder helpString = new StringBuilder("Для подписи документа следует использовать программное обеспечение КриптоПро CSP и браузер Internet Explorer 11 с установленным дополнением КриптоПро ЭЦП Browser plug-in", 300);
                //helpString.Append(Environment.NewLine);

                ASPxMemoHintESign.Text = helpString.ToString();
            }
            //}

            string user_login = Page.User.Identity.Name;

            if (user_login.Length > 0)
            {
                int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);

                // устанавливаем доступность компонентов в зависимости от роли пользователя
                PanelUploadESignFile.Visible = (user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN);

                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                    user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role == (int)MC_PersCab_UserRoleType.MC_urt_USER)
                {
                    string pageCommand = Request.QueryString["cmd"];

                    /*this.SqlDataSourceContractDocs.SelectCommand =
                        String.Concat("SELECT id_zayavkadoc, id_contract, date_doc_add, doc_file_name, doc_file_path, tblDT.caption_doctype ",
                                        "FROM tblZayavkaDoc tblCD LEFT JOIN tblDocType tblDT ON tblCD.id_doctype = tblDT.id_doctype ",
                                        "WHERE id_contract = '", id_contract.ToString(), "'",
                                        " ORDER BY date_doc_add DESC");*/
                    this.SqlDataSourceContractDoc.SelectCommand =
                        String.Concat("SELECT id_zayavkadoc, id_contract, date_doc_add, doc_file_name, doc_file_path, tblDT.caption_doctype ",
                                        "FROM tblZayavkaDoc tblCD LEFT JOIN tblDocType tblDT ON tblCD.id_doctype = tblDT.id_doctype ",
                                        "WHERE id_zayavkadoc = '", id_contractdoc_str, "'",
                                        " ORDER BY date_doc_add DESC");

                    this.SqlDataSourceContractDocESign.SelectCommand =
                        String.Concat("SELECT id_esign, esign_docid, esign_date, user_nameingrid, doc_file_name ",
                                        "FROM tblESignDoc tblESD ",
                                        "LEFT JOIN tblUserInfo tblUI ON tblESD.esign_user = tblUI.id_user ",
                                        "LEFT JOIN tblZayavkaDoc tblZD ON tblESD.esign_docid = tblZD.id_zayavkadoc ",
                                        "WHERE tblESD.id_zayavkadoc = '", id_contractdoc_str, "'",
                                        " ORDER BY esign_date DESC");

                    if (String.Equals(pageCommand, "edit"))
                    {

                    }
                    else
                    {

                    }
                }
                else
                {

                }

            } // if (user_login.Length > 0)   
            else
            {
                Response.Redirect("~");
            }

            // расшифровываем подписи при наличии        
            tblZayavkaDoc contractDoc = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_contractdoc_str)).Select(p => p).First();

            string fileNameDataSrc = String.Concat(
                    System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "App_Data\\UploadTemp\\",
                    contractDoc.doc_file_path, "\\", contractDoc.id_zayavka.ToString(), "\\", contractDoc.doc_file_name);

            // получаем файл подписи ОКЭ
            Guid? esign_oke_id = contractDoc.esign_oke;
            if (esign_oke_id != null)
            {
                Guid esign_doc_oke_id = perscabnewEntitiesAdapter.tblESignDoc.Where(p => String.Equals(p.id_esign.ToString(), esign_oke_id.ToString())).Select(p => p).First().esign_docid;
                tblZayavkaDoc esign_doc_oke_file = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => String.Equals(p.id_zayavkadoc.ToString(), esign_doc_oke_id.ToString())).Select(p => p).First();

                string fileNameSignatureOKE = String.Concat(
                    System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "App_Data\\UploadTemp\\",
                    esign_doc_oke_file.doc_file_path, "\\", esign_doc_oke_file.id_zayavka.ToString(), "\\", esign_doc_oke_file.doc_file_name);

                ASPxMemoESignOke.Text = MC_GetSignatureInfo(fileNameDataSrc, fileNameSignatureOKE);
                ASPxMemoESignOke.Rows = 25;
            }

            // получаем файл подписи контрагента
            Guid? esign_kontr_id = contractDoc.esign_kontr;
            if (esign_kontr_id != null)
            {
                Guid esign_doc_kontr_id = perscabnewEntitiesAdapter.tblESignDoc.Where(p => String.Equals(p.id_esign.ToString(), esign_kontr_id.ToString())).Select(p => p).First().esign_docid;
                tblZayavkaDoc esign_doc_kontr_file = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => String.Equals(p.id_zayavkadoc.ToString(), esign_doc_kontr_id.ToString())).Select(p => p).First();

                string fileNameSignatureKontr = String.Concat(
                    System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "App_Data\\UploadTemp\\",
                    esign_doc_kontr_file.doc_file_path, "\\", esign_doc_kontr_file.id_zayavka.ToString(), "\\", esign_doc_kontr_file.doc_file_name);

                ASPxMemoESignKontr.Text = MC_GetSignatureInfo(fileNameDataSrc, fileNameSignatureKontr);
                ASPxMemoESignKontr.Rows = 25;
            }
            //-----------------------------------
        }

        protected void ESign_GetHashValue(Guid id_doc)
        {
            string hashStr_Gost3411 = "";
            string hashStr_Gost3411_2012_256 = "";
            string hashStr_Gost3411_2012_512 = "";

            // имя файла
            //string path = @"C:\\Projects\\perscabnew\\perscabnew\\App_Data\\UploadTemp\\signtest.jpg";

            string docURI = String.Concat(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "App_Data\\UploadTemp\\");
            string doc_file_name = "";

            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            //tblZayavkaDoc orderDocInfo = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_doc)).Select(p => p).First();
            tblZayavkaDoc orderDocInfo = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_doc.ToString())).Select(p => p).First();

            doc_file_name = orderDocInfo.doc_file_name;
            docURI = string.Concat(docURI, orderDocInfo.doc_file_path, "\\", orderDocInfo.id_zayavka.ToString(), "\\", doc_file_name);

            FileInfo fileInfo = new FileInfo(docURI);
            byte[] buffer = new byte[fileInfo.Length];

            // Открываем на чтение и считываем данные в буфер
            FileStream fs = File.Open(docURI, FileMode.Open, FileAccess.Read, FileShare.Read);
            fs.Read(buffer, 0, Convert.ToInt32(fileInfo.Length));
            fs.Close();

            Gost3411CryptoServiceProvider GostHash_Gost3411 = new Gost3411CryptoServiceProvider();
            Gost3411_2012_256 GostHash_Gost3411_2012_256 = new Gost3411_2012_256CryptoServiceProvider();
            Gost3411_2012_512 GostHash_Gost3411_2012_512 = new Gost3411_2012_512CryptoServiceProvider();

            // Подсчитываем значение хэш для потока данных для каждого ГОСТ
            byte[] HashValue_Gost3411 = GostHash_Gost3411.ComputeHash(buffer);
            for (int i = 0; i < HashValue_Gost3411.Length; i++) hashStr_Gost3411 += String.Format("{0:X2}", HashValue_Gost3411[i]);

            byte[] HashValue_Gost3411_2012_256 = GostHash_Gost3411_2012_256.ComputeHash(buffer);
            for (int i = 0; i < HashValue_Gost3411_2012_256.Length; i++) hashStr_Gost3411_2012_256 += String.Format("{0:X2}", HashValue_Gost3411_2012_256[i]);

            byte[] HashValue_Gost3411_2012_512 = GostHash_Gost3411_2012_512.ComputeHash(buffer);
            for (int i = 0; i < HashValue_Gost3411_2012_512.Length; i++) hashStr_Gost3411_2012_512 += String.Format("{0:X2}", HashValue_Gost3411_2012_512[i]);

            //ASPxMemo1.Text = hashStr;
            ASPxHiddenFieldESign2.Set("ESignHash_Gost3411", hashStr_Gost3411);
            ASPxHiddenFieldESign2.Set("ESignHash_Gost3411_2012_256", hashStr_Gost3411_2012_256);
            ASPxHiddenFieldESign2.Set("ESignHash_Gost3411_2012_512", hashStr_Gost3411_2012_512);
            /*ASPxMemo1.Text = ASPxHiddenFieldESign2.Get("ESignHash_Gost3411").ToString();
            ASPxMemo2.Text = ASPxHiddenFieldESign2.Get("ESignHash_Gost3411_2012_256").ToString();
            ASPxMemo3.Text = ASPxHiddenFieldESign2.Get("ESignHash_Gost3411_2012_512").ToString();*/
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            // Полное имя файла.
            ASPxMemo1.Text = "";

            string path = @"C:\\Projects\\perscabnew\\perscabnew\\App_Data\\UploadTemp\\signtest.jpg";
            //string path = @"~/App_Data/UploadTemp/signtest.jpg";
            // Открываем на чтение.
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            // Объект, реализующий алгоритм вычисления подписи.
            //Gost3410CryptoServiceProvider Gost = new Gost3410CryptoServiceProvider();
            // Объект, реализующий алгоритм хэширования.
            Gost3411CryptoServiceProvider GostHash = new Gost3411CryptoServiceProvider();

            // Подсчитываем значение хэш для потока данных.
            byte[] HashValue = GostHash.ComputeHash(fs);

            for (int i = 0; i < HashValue.Length; i++)
                //ASPxMemo1.Text += HashValue[i].ToString();
                ASPxMemo1.Text += String.Format("{0:X2}", HashValue[i]);
            //ASPxMemo1.Text = Convert.ToBase64String(HashValue);
            //Console.Write("{0:X2}", HashValue[i] + "  ");

            // Считаем подпись для хэш и выводим результат:
            /*byte[] SignedData = Gost.SignHash(HashValue);
            Console.WriteLine("Подпись:");
            for (int i = 0; i < SignedData.Length; i++)
                Console.Write("{0:X2}", SignedData[i] + "  ");

            bool b;
            // Проверям правильность подписи для значения хэш и выводим результат.
            b = Gost.VerifyHash(HashValue, SignedData);
            if (b)
            {
                Console.WriteLine("Подпись верна");
            }
            else
            {
                Console.WriteLine("Подпись не верна");
            }*/

        }

        protected void ASPxButton3_Click(object sender, EventArgs e)
        {
            //ASPxMemo1.Text = ASPxHiddenFieldESign3.Get("ESign").ToString();        
            string stringESign = ASPxHiddenFieldESign3.Get("ESign").ToString();

            if (String.Equals(stringESign, "undefined"))
            {
                lblESignStatus.ForeColor = System.Drawing.Color.Red;
                lblESignStatus.Text = "Ошибка создания подписи";
            }
            else
            {
                string user_login = Page.User.Identity.Name;
                perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
                tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

                int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);

                Guid TPZayavkaDocID = Guid.Parse(Request.QueryString["docid"]); // id подписываемого документа
                tblZayavkaDoc zayavkaDocInfo = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => String.Equals(p.id_zayavkadoc.ToString(), TPZayavkaDocID.ToString())).Select(p => p).First(); // подписываемый документ

                // создаем файл                      
                Guid TPZayavkaID = zayavkaDocInfo.id_zayavka; // id заявки

                DateTime CurrDate = DateTime.Now;
                string zayavkaDocPath = String.Concat(DateTime.Now.Year.ToString(), "\\");
                if (CurrDate.Month < 10) zayavkaDocPath = String.Concat(zayavkaDocPath, "0", CurrDate.Month.ToString());
                else zayavkaDocPath = String.Concat(zayavkaDocPath, CurrDate.Month.ToString());
                zayavkaDocPath = String.Concat(zayavkaDocPath, "\\");
                if (CurrDate.Day < 10) zayavkaDocPath = String.Concat(zayavkaDocPath, "0", CurrDate.Day.ToString());
                else zayavkaDocPath = String.Concat(zayavkaDocPath, CurrDate.Day.ToString());

                //добавляем идентификатор версии
                Guid versionGUID = Guid.NewGuid();
                zayavkaDocPath = String.Concat(zayavkaDocPath, "\\ver", versionGUID.ToString());

                string docFileDir = String.Concat(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "App_Data\\UploadTemp\\", zayavkaDocPath, "\\" + TPZayavkaID.ToString());

                DirectoryInfo d = new DirectoryInfo(docFileDir);
                if (d.Exists)
                {
                }
                else
                {
                    d.Create();
                }

                string signFileName = String.Concat(zayavkaDocInfo.doc_file_name, ".sig");

                // создаем файл
                System.IO.File.WriteAllText(docFileDir + "\\" + signFileName, stringESign);

                // добавляем файл подписи в БД        
                tblZayavkaDoc zayavkaDocNew = new tblZayavkaDoc();
                zayavkaDocNew.id_zayavkadoc = Guid.NewGuid();
                zayavkaDocNew.id_doctype = 2; // файл подписи - переделать в будущем!!!
                zayavkaDocNew.id_zayavka = TPZayavkaID;
                zayavkaDocNew.doc_file_name = signFileName;
                zayavkaDocNew.doc_file_path = zayavkaDocPath;
                zayavkaDocNew.date_doc_add = DateTime.Now;
                zayavkaDocNew.is_temp = null;
                zayavkaDocNew.id_chatrec = null;
                zayavkaDocNew.id_contract = TPZayavkaID;
                zayavkaDocNew.id_user = userInfo.id_user;
                perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDocNew);
                perscabnewEntitiesAdapter.SaveChanges();

                tblESignDoc signDocNew = new tblESignDoc();
                signDocNew.id_esign = Guid.NewGuid();
                signDocNew.id_zayavkadoc = TPZayavkaDocID;
                signDocNew.verify_status = null;
                signDocNew.verify_user = null;
                signDocNew.verify_datetime = null;
                signDocNew.esign_docid = zayavkaDocNew.id_zayavkadoc;
                signDocNew.esign_user = userInfo.id_user;
                signDocNew.esign_date = DateTime.Now;
                perscabnewEntitiesAdapter.tblESignDoc.Add(signDocNew);
                perscabnewEntitiesAdapter.SaveChanges();

                // ищем подписываемый документ            
                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER || user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN)
                {
                    zayavkaDocInfo.esign_oke = signDocNew.id_esign;
                }
                else if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_USER)
                {
                    zayavkaDocInfo.esign_kontr = signDocNew.id_esign;
                }
                perscabnewEntitiesAdapter.SaveChanges();
                //-------------------------------------------------------

                ASPxGridViewContractDocESign.DataBind();

                lblESignStatus.ForeColor = System.Drawing.Color.Green;
                lblESignStatus.Text = "Подпись успешно сформирована";
            } // if (String.Equals(stringESign, "undefined"))
              //------------------------------------------------------------

            //ASPxMemo1.Text = ASPxHiddenFieldESign2.Get("ESignHash").ToString();
        }

        // получение информации о подписи (отсоединенная pkcs7)
        private string MC_GetSignatureInfo(string fileNameDataSrc, string fileNameSignature)
        {
            StringBuilder signatureInfo_str = new StringBuilder("", 1000);

            // Загружаем pkcs7 сообщение в память                        
            //string stringESign = File.ReadAllText(fileNameSignature);

            //byte[] encodedSignedCms = Convert.FromBase64String(File.ReadAllText(fileNameSignature));

            byte[] encodedSignedCms;
            // если строка подписи не в формате BASE64
            try
            {
                encodedSignedCms = Convert.FromBase64String(File.ReadAllText(fileNameSignature));
            }
            catch (FormatException)
            {
                encodedSignedCms = File.ReadAllBytes(fileNameSignature);
            }
            //-----------------------------------------------

            // Файл данных        
            byte[] srcData = File.ReadAllBytes(fileNameDataSrc);

            // Создаем объект ContentInfo по сообщению.
            // Это необходимо для создания объекта SignedCms.
            ContentInfo contentInfo = new ContentInfo(srcData);

            // Объект, в котором будут происходить декодирование и проверка.
            // Свойство Detached устанавливаем явно в true, таким 
            // образом сообщение будет отделено от подписи.
            SignedCms signedCms = new SignedCms(contentInfo, true);

            // Декодируем сообщение

            signedCms.Decode(encodedSignedCms);

            // определяем дату и время подписи
            Pkcs9SigningTime st = new Pkcs9SigningTime();
            for (int i = 0; i < signedCms.SignerInfos[0].SignedAttributes.Count; i++)
            {
                if (signedCms.SignerInfos[0].SignedAttributes[i].Values[0] is Pkcs9SigningTime)
                {
                    Pkcs9SigningTime signingTime = (Pkcs9SigningTime)signedCms.SignerInfos[0].SignedAttributes[i].Values[0];
                    signatureInfo_str.AppendFormat(String.Format("Дата и время подписи (МСК): {0}", signingTime.SigningTime.AddHours(3))); // UTC+3 = московское время
                    signatureInfo_str.Append(Environment.NewLine);
                    signatureInfo_str.Append(Environment.NewLine);
                }
            }
            //---------------------------------------------

            /*//  Проверяем число основных и дополнительных подписей.
            textBoxESign.AppendText("");
            textBoxESign.AppendText(String.Format("Количество подписавших:{0}", signedCms.SignerInfos.Count));
            textBoxESign.AppendText(Environment.NewLine);
            textBoxESign.AppendText(Environment.NewLine);*/

            if (signedCms.SignerInfos.Count == 0)
            {
                signatureInfo_str.Append("Документ не подписан");
                return null;
            }

            //bool valid = true;

            SignerInfoEnumerator enumerator = signedCms.SignerInfos.GetEnumerator();
            while (enumerator.MoveNext()) // !!! сделать без while
            {
                SignerInfo current = enumerator.Current;

                if (current.Certificate != null)
                {
                    /*signatureInfo_str.Append("FriendlyName: " + current.Certificate.FriendlyName + Environment.NewLine);*/
                    /*signatureInfo_str.Append(Environment.NewLine);*/
                    signatureInfo_str.Append("Издатель: " + current.Certificate.Issuer + Environment.NewLine);
                    signatureInfo_str.Append(Environment.NewLine);
                    /*signatureInfo_str.Append("IssuerName: " + current.Certificate.IssuerName + Environment.NewLine);*/
                    /*signatureInfo_str.Append(Environment.NewLine);*/
                    signatureInfo_str.Append("Дата выдачи сертификата: " + current.Certificate.NotBefore.ToString() + Environment.NewLine);
                    signatureInfo_str.Append(Environment.NewLine);
                    signatureInfo_str.Append("Срок действия сертификата: " + current.Certificate.NotAfter.ToString() + Environment.NewLine);
                    signatureInfo_str.Append(Environment.NewLine);
                    signatureInfo_str.Append("Номер сертификата: " + current.Certificate.SerialNumber + Environment.NewLine);
                    signatureInfo_str.Append(Environment.NewLine);
                    signatureInfo_str.Append("Владелец подписи: " + current.Certificate.Subject + Environment.NewLine);
                    signatureInfo_str.Append(Environment.NewLine);
                    /*signatureInfo_str.Append("SubjectName: " + current.Certificate.SubjectName + Environment.NewLine);*/
                    /*signatureInfo_str.Append(Environment.NewLine);*/
                    signatureInfo_str.Append("Отпечаток: " + current.Certificate.Thumbprint + Environment.NewLine);
                    /*signatureInfo_str.Append(Environment.NewLine);*/

                    /*signatureInfo_str.Append(Environment.NewLine);
                    signatureInfo_str.Append("---------------------------------");
                    signatureInfo_str.Append(Environment.NewLine);*/
                }
            }

            return signatureInfo_str.ToString();

        } // private void MC_GetSignatureInfo()

        /*protected void ASPxCallbackPanelESign_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {

        }*/

        // формирование поля "скачать" документа договора
        protected void urlDownloadContractDoc_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxHyperLink)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string doc_id = container.Grid.GetRowValues(currentIndex, "id_zayavkadoc").ToString();

            ((ASPxHyperLink)sender).NavigateUrl = String.Concat("~/Cabinet/DownloadDoc.aspx?doctype=contract&docid=", doc_id);
        }

        // формирование поля "скачать" для подписи документа
        protected void urlDownloadESignDoc_Init(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer container = ((ASPxHyperLink)sender).NamingContainer as GridViewDataItemTemplateContainer;
            int currentIndex = container.VisibleIndex;
            string doc_id = container.Grid.GetRowValues(currentIndex, "esign_docid").ToString();

            ((ASPxHyperLink)sender).NavigateUrl = String.Concat("~/Cabinet/DownloadDoc.aspx?doctype=contract&docid=", doc_id);
        }

        // функционал прикрепления файла ЭЦП заказчика администратором
        protected void ASPxUploadControlESignFile_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            string user_login = Page.User.Identity.Name;
            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();
            tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

            int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);

            Guid TPZayavkaDocID = Guid.Parse(Request.QueryString["docid"]); // id подписываемого документа
            tblZayavkaDoc zayavkaDocInfo = perscabnewEntitiesAdapter.tblZayavkaDoc.Where(p => String.Equals(p.id_zayavkadoc.ToString(), TPZayavkaDocID.ToString())).Select(p => p).First(); // подписываемый документ

            // копируем файл ЭЦП
            if (!e.IsValid) e.CallbackData = string.Empty;

            Guid TPZayavkaID = zayavkaDocInfo.id_zayavka;

            DateTime CurrDate = DateTime.Now;
            string orderDocPath = String.Concat(DateTime.Now.Year.ToString(), "\\");
            if (CurrDate.Month < 10) orderDocPath = String.Concat(orderDocPath, "0", CurrDate.Month.ToString());
            else orderDocPath = String.Concat(orderDocPath, CurrDate.Month.ToString());
            orderDocPath = String.Concat(orderDocPath, "\\");
            if (CurrDate.Day < 10) orderDocPath = String.Concat(orderDocPath, "0", CurrDate.Day.ToString());
            else orderDocPath = String.Concat(orderDocPath, CurrDate.Day.ToString());

            //добавляем идентификатор версии
            Guid versionGUID = Guid.NewGuid();
            orderDocPath = String.Concat(orderDocPath, "\\ver", versionGUID.ToString());

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

            // добавляем файл подписи в БД        
            tblZayavkaDoc zayavkaDocNew = new tblZayavkaDoc();
            zayavkaDocNew.id_zayavkadoc = Guid.NewGuid();
            zayavkaDocNew.id_doctype = 2; // файл подписи - переделать в будущем!!!
            zayavkaDocNew.id_zayavka = TPZayavkaID;
            zayavkaDocNew.doc_file_name = fileNameGood;
            zayavkaDocNew.doc_file_path = orderDocPath;
            zayavkaDocNew.date_doc_add = DateTime.Now;
            zayavkaDocNew.is_temp = null;
            zayavkaDocNew.id_chatrec = null;
            zayavkaDocNew.id_contract = TPZayavkaID;
            zayavkaDocNew.id_user = userInfo.id_user;
            perscabnewEntitiesAdapter.tblZayavkaDoc.Add(zayavkaDocNew);
            perscabnewEntitiesAdapter.SaveChanges();

            tblESignDoc signDocNew = new tblESignDoc();
            signDocNew.id_esign = Guid.NewGuid();
            signDocNew.id_zayavkadoc = TPZayavkaDocID;
            signDocNew.verify_status = null;
            signDocNew.verify_user = null;
            signDocNew.verify_datetime = null;
            signDocNew.esign_docid = zayavkaDocNew.id_zayavkadoc;
            signDocNew.esign_user = userInfo.id_user;
            signDocNew.esign_date = DateTime.Now;
            perscabnewEntitiesAdapter.tblESignDoc.Add(signDocNew);
            perscabnewEntitiesAdapter.SaveChanges();

            zayavkaDocInfo.esign_kontr = signDocNew.id_esign;

            perscabnewEntitiesAdapter.SaveChanges();
            //-------------------------------------------------------

            ASPxGridViewContractDocESign.DataBind();

        } // protected void ASPxUploadControlESignFile_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)

    }
}