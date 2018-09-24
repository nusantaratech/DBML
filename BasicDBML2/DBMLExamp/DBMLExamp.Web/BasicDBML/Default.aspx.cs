using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

namespace DBMLExamp.Web.BasicDBML
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                this.GridView_Nasabah_DataBind();
            }
        }

        #region GridView
        private void GridView_Nasabah_DataBind()
        {
            using(CustomerDataContext ctx = new CustomerDataContext())
            {
                GridView_Nasabah.DataSource = from Customer in ctx.Customers
                                              select Customer;
                GridView_Nasabah.DataBind();
            }
        }

        protected void GridView_Nasabah_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridView_Nasabah.EditIndex)
            {
                (e.Row.Cells[2].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?'";
            }
        }

        protected void GridView_Nasabah_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_Nasabah.EditIndex = e.NewEditIndex;
            this.GridView_Nasabah_DataBind();
        }

        protected void GridView_Nasabah_RowCancelingEdit(object sender, EventArgs e)
        {
            GridView_Nasabah.EditIndex = -1;
            this.GridView_Nasabah_DataBind();
        }

        protected void GridView_Nasabah_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = GridView_Nasabah.Rows[e.RowIndex];
            string customerId = GridView_Nasabah.DataKeys[e.RowIndex].Values[0].ToString();
            string customerName = (gvr.FindControl("TextBox_CustomerName") as TextBox).Text;
            string country = (gvr.FindControl("TextBox_Country") as TextBox).Text;

            using(CustomerDataContext ctx = new CustomerDataContext())
            {
                Customer cust = (from c in ctx.Customers
                                 where c.CustomerID == customerId
                                 select c).FirstOrDefault();
                cust.CustomerName = customerName;
                cust.Country = country;
                ctx.SubmitChanges();
            }

            GridView_Nasabah.EditIndex = -1;
            this.GridView_Nasabah_DataBind();
        }

        protected void GridView_Nasabah_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string customerId = GridView_Nasabah.DataKeys[e.RowIndex].Values[0].ToString();
            using(CustomerDataContext ctx = new CustomerDataContext())
            {
                Customer cust = (from c in ctx.Customers
                                 where c.CustomerID == customerId
                                 select c).FirstOrDefault();
                ctx.Customers.DeleteOnSubmit(cust);
                ctx.SubmitChanges();
            }
            this.GridView_Nasabah_DataBind();
        }
        #endregion


        #region Action
        protected void Button_Add_Click(object sender, EventArgs e)
        {
            using(CustomerDataContext ctx = new CustomerDataContext())
            {
                Customer cust = new Customer { 
                    CustomerID = TextBox_CustomerID.Text,
                    CustomerName = TextBox_CustomerName.Text,
                    Country = TextBox_Country.Text
                };

                ctx.Customers.InsertOnSubmit(cust);
                ctx.SubmitChanges();
            }

            this.GridView_Nasabah_DataBind();
        }

        protected void InsertData(object sender, EventArgs e)
        {
            string customerID = TextBox_CustomerID.Text;
            string customerName = TextBox_CustomerName.Text;
            string country = TextBox_Country.Text;

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["COMPREConnectionString"].ConnectionString;
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                using(SqlCommand cmd = new SqlCommand("SP_CUstomer_CRUD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "CREATE");
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Connection = conn;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            this.GridView_Nasabah_DataBind();
        }
        #endregion
    }
}