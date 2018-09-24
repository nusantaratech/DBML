using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace DBMLExamp.Web.BasicDBML
{
    public class JFramework
    {
        public DataTable GetDataTable(string strQuery)
        {
            DataTable dt = new DataTable();

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["COMPREConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;


            try
            {
                conn.Open();
                da.SelectCommand = cmd;
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                da.Dispose();
                conn.Dispose();
            }
        }

        public void GridBind(GridView gv)
        {
            string strConn = ConfigurationManager.ConnectionStrings["COMPREConnectionString"].ConnectionString;
            using(SqlConnection conn = new SqlConnection(strConn))
            {
                using(SqlCommand cmd = new SqlCommand("SP_CUstomer_CRUD"))
                {
                    cmd.Parameters.AddWithValue("@Action", "SELECT");
                    using(SqlDataAdapter da = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        da.SelectCommand = cmd;
                        using(DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            gv.DataSource = dt;
                            gv.DataBind();
                        }
                    }
                }
            }
        }

    }
}