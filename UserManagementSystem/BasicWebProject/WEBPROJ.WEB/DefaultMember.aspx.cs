using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Linq;
using SPU.Dataaccess;
using SPU.Entities;

public partial class DefaultMember : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SPUDataaccess db = new SPUDataaccess(); 
        var data = db.SPUDataContent.Organisasis.Select(p => p);
        DropDownList_Organisasi.DataSource = data;
        DropDownList_Organisasi.DataValueField = "OrgID";
        DropDownList_Organisasi.DataTextField = "OrgName";
        DropDownList_Organisasi.DataBind();
    }
}