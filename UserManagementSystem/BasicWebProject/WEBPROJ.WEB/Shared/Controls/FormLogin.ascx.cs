using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shared_Controls_FormLogin : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool isLogin = HttpContext.Current.User.Identity.IsAuthenticated;
        if(isLogin)
            Response.Redirect("~/DefaultMember.aspx"); 
    }
}