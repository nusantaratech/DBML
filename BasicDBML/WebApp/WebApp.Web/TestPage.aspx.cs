using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

namespace WebApp.Web
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.BindingGrid();
            }
        }

        private void BindingGrid()
        {
            using(CompreDataContext ctx = new CompreDataContext())
            {
                gvUser.DataSource = from c in ctx.linqUsers
                                    select c;
                gvUser.DataBind();
            }
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUser.EditIndex = e.NewEditIndex;
            this.BindingGrid();
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            gvUser.EditIndex = -1;
            this.BindingGrid();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvUser.Rows[e.RowIndex];
            int linqID = Convert.ToInt32(gvUser.DataKeys[e.RowIndex].Value);
            string name = (row.FindControl("txtName") as TextBox).Text;
            string email = (row.FindControl("txtEmail") as TextBox).Text;

            using(CompreDataContext ctx = new CompreDataContext())
            {
                linqUser usr = (from c in ctx.linqUsers
                                where c.LinqID == linqID
                                select c).FirstOrDefault();

                usr.Name = name;
                usr.Email = email;
                ctx.SubmitChanges();
            }
            gvUser.EditIndex = -1;
            this.BindingGrid();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int linqID = Convert.ToInt32(gvUser.DataKeys[e.RowIndex].Value);
            using(CompreDataContext ctx = new CompreDataContext())
            {
                linqUser usr = (from c in ctx.linqUsers
                                where c.LinqID == linqID
                                select c).FirstOrDefault();
                ctx.linqUsers.DeleteOnSubmit(usr);
                ctx.SubmitChanges();
            }
            this.BindingGrid();
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            this.BindingGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //set path image
            string image = Server.MapPath("~/Images/") + Guid.NewGuid() + fUpload.PostedFile.FileName;
            fUpload.PostedFile.SaveAs(image);
            string file = image.Substring(image.LastIndexOf("\\"));
            string[] split = file.Split('\\');
            string imgpath = split[1];
            string imagepath = "~/Images/" + imgpath;

            using(CompreDataContext ctx = new CompreDataContext())
            {
                linqUser usr = new linqUser() { 
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    Image = imagepath
                };

                ctx.linqUsers.InsertOnSubmit(usr);
                ctx.SubmitChanges();
                clear();
                this.BindingGrid();
            }
        }

        private void clear()
        {
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

    }
}