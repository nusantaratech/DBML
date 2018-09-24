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

namespace DBMLWEB.WEB
{
    public class JFramework
    {
        public DataTable getDataTable(string queryString) {
            DataTable dt = new DataTable();

            //make connection to database
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["post_dbConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            //define data adapter
            SqlDataAdapter da = new SqlDataAdapter();

            //define sql command
            SqlCommand cmd = new SqlCommand(queryString);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            try
            {
                //open connection
                conn.Open();

                //fill datatable from data adapter
                da.SelectCommand = cmd;
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                conn.Close();
                da.Dispose();
                conn.Dispose();
            }
        }

        public void bindingGrid(GridView gdv) {
            
            string strConn = ConfigurationManager.ConnectionStrings["post_dbConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(strConn)) {
                using (SqlCommand cmd = new SqlCommand("NasabahBersama_CRUD")) {
                    cmd.Parameters.AddWithValue("@Action", "SELECT");
                    using (SqlDataAdapter da = new SqlDataAdapter()) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        da.SelectCommand = cmd;
                        using (DataTable dt = new DataTable()) {
                            da.Fill(dt);
                            gdv.AllowPaging = false;
                            gdv.DataSource = dt;
                            gdv.DataBind();
                        }
                    }
                }
            }
        }

    }
}