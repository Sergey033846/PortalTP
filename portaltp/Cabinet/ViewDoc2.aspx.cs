using DatabaseFirst;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp.Cabinet
{
    public partial class ViewDoc2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            string id_doc = Request.QueryString["docid"].ToString();

            try
            {                
                string docURI = "~//File1CDir//";
                string doc_file_name = "";

                // формируем имя файла                
                DataTable tblFile1CInfo = new DataTable();
                string queryString = String.Concat(
                                "SELECT tDoc._IDRRef, _Fld24758, _Fld28469RRef, _Fld28470, _Fld28471",
                                " FROM upp.dbo._Reference24727 tDoc",
                                " WHERE tDoc._IDRRef = 0x", id_doc);
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["1CDbConnectionString"].ToString());
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(queryString, connection);
                adapter.SelectCommand.CommandTimeout = 0; //бесконечное время ожидания
                adapter.Fill(tblFile1CInfo);
                connection.Close();

                doc_file_name = tblFile1CInfo.Rows[0]["_Fld28471"].ToString();
                string doc_file_name_1C = tblFile1CInfo.Rows[0]["_Fld24758"].ToString();
                docURI = string.Concat(docURI, tblFile1CInfo.Rows[0]["_Fld28470"].ToString().Replace("\\","//"));                

                //--------------------------

                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                //response.Buffer = true;

                Response.ContentType = System.Web.MimeMapping.GetMimeMapping(doc_file_name);
                                
                response.AddHeader("Content-Disposition", "inline;filename=" + Server.UrlEncode(doc_file_name_1C));
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
    }
}