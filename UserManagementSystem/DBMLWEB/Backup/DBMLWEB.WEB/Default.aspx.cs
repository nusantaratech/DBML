using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;


namespace DBMLWEB.WEB
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            using (CustomerDataContext ctx = new CustomerDataContext())
            {
                GridView1.DataSource = from NasabahBersama in ctx.NasabahBersamas
                                       select NasabahBersama;
                GridView1.DataBind();
            }
        }

        private void BindGrid2()
        {
            JFramework FW = new JFramework();

            string strQuery = "SELECT CostumerID,CostumerName,Country FROM NasabahBersama";
            DataTable dt = FW.getDataTable(strQuery);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private void BindGrid3()
        {
            JFramework FW = new JFramework();
            FW.bindingGrid(GridView1);
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridView1.EditIndex)
            {
                (e.Row.Cells[2].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string customerId = GridView1.DataKeys[e.RowIndex].Values[0].ToString();
            string CustomerName = (row.FindControl("txtCostumerName") as TextBox).Text;
            string Country = (row.FindControl("txtCountry") as TextBox).Text;
            using (CustomerDataContext ctx = new CustomerDataContext())
            {
                NasabahBersama NasabahBersama = (from c in ctx.NasabahBersamas
                                     where c.CostumerID == customerId
                                     select c).FirstOrDefault();
                NasabahBersama.CostumerName = CustomerName;
                NasabahBersama.Country = Country;
                ctx.SubmitChanges();
            }
            GridView1.EditIndex = -1;
            this.BindGrid();
        }
           

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string customerId = GridView1.DataKeys[e.RowIndex].Values[0].ToString();
            using (CustomerDataContext ctx = new CustomerDataContext())
            {
                NasabahBersama NasabahBersama = (from c in ctx.NasabahBersamas
                                     where c.CostumerID == customerId
                                     select c).FirstOrDefault();
                ctx.NasabahBersamas.DeleteOnSubmit(NasabahBersama);
                ctx.SubmitChanges();
            }
            this.BindGrid();
        }
               

        protected void Insert(object sender, EventArgs e)
        {
            using (CustomerDataContext ctx = new CustomerDataContext())
            {
                NasabahBersama NasabahBersama = new NasabahBersama
                {
                    CostumerID = txtCostumerID.Text,
                    CostumerName = txtCostumerName.Text,
                    Country = txtCountry.Text
                };
                ctx.NasabahBersamas.InsertOnSubmit(NasabahBersama);
                ctx.SubmitChanges();
            }

            this.BindGrid();
        }

        protected void ProcessInsert(object sender, EventArgs e) {
            
            string customerId = txtCostumerID.Text;
            string customerName = txtCostumerName.Text;
            string country = txtCountry.Text;

            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["post_dbConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(strConn)) {
                using (SqlCommand cmd = new SqlCommand("NasabahBersama_CRUD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "INSERT");
                    cmd.Parameters.AddWithValue("@CostumerID", customerId);
                    cmd.Parameters.AddWithValue("@CostumerName", customerName);
                    cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            this.BindGrid();
        }

        protected void ProcessUpdate(object sender, GridViewUpdateEventArgs e) {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string customerId = GridView1.DataKeys[e.RowIndex].Values[0].ToString();
            string customerName = (row.FindControl("txtCostumerName") as TextBox).Text;
            string country = (row.FindControl("txtCountry") as TextBox).Text;

            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["strconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(conStr)) {
                using (SqlCommand cmd = new SqlCommand("")) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UPDATE");
                    cmd.Parameters.AddWithValue("@CostumerID", customerId);
                    cmd.Parameters.AddWithValue("@CostumerName", customerName);
                    cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void processDelete(object sender, GridViewDeleteEventArgs e) {
            string customerId = GridView1.DataKeys[e.RowIndex].Values[0].ToString();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr)) {
                using (SqlCommand cmd = new SqlCommand("NasabahBersama_CRUD")) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "DELETE");
                    cmd.Parameters.AddWithValue("@CostumerID", customerId);
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            this.BindGrid();
        }
       
    }
}
