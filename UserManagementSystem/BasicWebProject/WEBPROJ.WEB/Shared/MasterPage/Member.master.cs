using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shared_MasterPage_Member : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool isLogin = HttpContext.Current.User.Identity.IsAuthenticated;
        if (!isLogin) 
        { 
            Response.Redirect("~/Default.aspx"); 
        }
    }
}
