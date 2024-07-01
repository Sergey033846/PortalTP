using DatabaseFirst;
using mcperscab;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp.Cabinet
{
    public partial class DownloadDoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                //string doc_type = Request.QueryString["doctype"].ToString(); // вид документа - заявка или договор
                string id_doc = Request.QueryString["docid"].ToString();

                perscabnewEntities lkAdapter = new perscabnewEntities();

                string user_login = Page.User.Identity.Name;
                int user_role = MC_PersCab_ActionsWithUsers.MC_PersCab_ActionsWithUsers_GetRole(user_login);
                Guid id_user = lkAdapter.tblUserInfo.Where(p => String.Equals(p.user_login, user_login)).Select(p => p).First().id_user;

                tblZayavkaDoc orderDocInfo = lkAdapter.tblZayavkaDoc.Where(p => String.Equals(p.id_zayavkadoc.ToString(), id_doc)).Select(p => p).First();

                // ищем собственника документа заявки
                Guid id_owner = lkAdapter.tblZayavka.Where(p => p.id_zayavka == orderDocInfo.id_zayavka).Select(p => p).First().id_user;

                if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                    user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR ||
                    id_user == id_owner)
                {

                    try
                    {
                        string docURI = String.Concat(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "App_Data\\UploadTemp\\");
                        string doc_file_name = "";
                                                
                        doc_file_name = orderDocInfo.doc_file_name;
                        docURI = string.Concat(docURI, orderDocInfo.doc_file_path, "\\", doc_file_name);

                        //WebClient req = new WebClient();
                        HttpResponse response = HttpContext.Current.Response;
                        response.Clear();
                        response.ClearContent();
                        response.ClearHeaders();
                        //response.Buffer = true;

                        Response.ContentType = "application/octet-stream";
                        //Response.Charset = "utf-8";
                        //if (Request.UserAgent != null && Request.UserAgent.IndexOf("MSIE") >= 0) doc_file_name = Server.UrlEncode(doc_file_name); // убираем абракадабру в IE
                        response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(doc_file_name));
                        //response.AddHeader("Content-Disposition", "attachment;filename=" + doc_file_name);
                        //byte[] data = req.DownloadData(docURI);
                        response.TransmitFile(docURI);
                        //response.BinaryWrite(data);
                        response.End();
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    Response.Redirect("~", true);
                }
            }// if (Page.User.Identity.IsAuthenticated)
            else
            {
                Response.Redirect("~", true);
            }

        }

    }

}