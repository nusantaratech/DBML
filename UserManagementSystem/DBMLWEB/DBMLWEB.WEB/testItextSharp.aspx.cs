using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

namespace DBMLWEB.WEB
{
    public partial class testItextSharp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack) {
                this.exportToWord();
            }
        }

        private DataTable getDataTable(SqlCommand cmd) {
            //define datatabel
            DataTable dt = new DataTable();

            //define connection string
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["post_dbConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            //define data adapter
            SqlDataAdapter da = new SqlDataAdapter();

            //define command
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                //open connection
                conn.Open();

                //fill dt from da
                da.SelectCommand = cmd;
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                //close and dispose connection and dataadapter
                conn.Close();
                da.Dispose();
                conn.Dispose();
            }
        }

        //datatable to word format
        protected void exportToWord() {
            string strQuery = "SELECT CostumerName, Country FROM NasabahBersama";
            SqlCommand cmd = new SqlCommand(strQuery);

            DataTable dt = getDataTable(cmd);

            //create a dummy gridview
            GridView gdv = new GridView();
            gdv.AllowPaging = false;
            gdv.DataSource = dt;
            gdv.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DataTable.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gdv.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        //datatable to excel format
        protected void exportToExcel() {
            string strQuery = "SELECT CostumerName, Country FROM NasabahBersama";
            SqlCommand cmd = new SqlCommand(strQuery);

            DataTable dt = getDataTable(cmd);

            //create dummy gridview
            GridView gdv = new GridView();
            gdv.AllowPaging = false;
            gdv.DataSource = dt;
            gdv.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition","attachment;filename=DataTable.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            for (int i = 0; i < gdv.Rows.Count; i++)
            {

                //Apply text style to each Row
                gdv.Rows[i].Attributes.Add("classextmode","");

            }
            gdv.RenderControl(hw); 
            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }

        protected void ExportToPDF()
        {

            //Get the data from database into datatable

            string strQuery = "SELECT CostumerName, Country FROM NasabahBersama";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = getDataTable(cmd);

            

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition","attachment;filename=DataTable.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);

            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
    }
}