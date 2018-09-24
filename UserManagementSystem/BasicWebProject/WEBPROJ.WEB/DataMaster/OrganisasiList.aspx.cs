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

public partial class DataMaster_OrganisasiList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label_Title.Text = Page.Title;
        if (!IsPostBack) { 
            PopulateNodes(); 
        }
    }

    #region TreeView Data
    public void PopulateNodes()
    {
        TreeView_Main.Nodes.Clear();

        DataTable data = GetTreeViewData();
        DataView child = GetParent(data);
        foreach (DataRowView row in child)
        {
            TreeNode childNode = new TreeNode();
            childNode.Text = row["OrgName"].ToString();
            childNode.Value = row["OrgID"].ToString();
            TreeView_Main.Nodes.Add(childNode);
            AddChild(data, childNode);
        }

        TreeView_Main.ExpandAll();
    }

    protected DataTable GetTreeViewData()
    {
        SPUDataaccess db = new SPUDataaccess();
        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add("OrgID");
        dt.Columns.Add("OrgParent");
        dt.Columns.Add("OrgName");

        var data = db.SPUDataContent.Organisasis.Select(p => p);
        foreach (Organisasi org in data)
        {
            dr = dt.NewRow();
            dr["OrgID"] = org.OrgID;
            dr["OrgParent"] = org.OrgParent;
            dr["OrgName"] = org.OrgName;
            dt.Rows.Add(dr);
        }

        return dt;
    }

    DataView GetParent(DataTable parent)
    {
        DataView view = new DataView(parent);
        view.RowFilter = "OrgParent='0'";
        return view;
    }

    DataView GetChild(DataTable parent, string parentID)
    {
        DataView view = new DataView(parent);
        view.RowFilter = "OrgParent='" + parentID + "'";
        return view;
    }
    void AddChild(DataTable parent, TreeNode node)
    {
        DataView child = GetChild(parent, node.Value);
        foreach (DataRowView row in child)
        {
            TreeNode childNode = new TreeNode();
            childNode.Text = row["OrgName"].ToString();
            childNode.Value = row["OrgID"].ToString();
            node.ChildNodes.Add(childNode);
            AddChild(parent, childNode);
        }
    }

    protected void TreeView_Main_SelectedNodeChanged(object sender, EventArgs e)
    {
        Button_Edit.Visible = true;
        Button_Update.Visible = false;
        Button_Save.Visible = false;
        Button_AddChild.Visible = true;
        Label_Modal_Title.Text = "Detail";
        MultiView_Content.SetActiveView(View_Detail);

        string id = TreeView_Main.SelectedValue;
        SPUDataaccess db = new SPUDataaccess();
        Organisasi org = db.SPUDataContent.Organisasis.Where(p => p.OrgID.Equals(id)).Single<Organisasi>();

        Label_Value_ID.Text = org.OrgID;
        Label_Value_Parent.Text = org.OrgParent;
        Label_Value_Name.Text = org.OrgName;
        Label_Value_Desc.Text = org.OrgDesc;

        UpdatePanel_Panel_Modal.Update();
        this.MPE.Show();
    }
    #endregion

    #region Action
    protected void ImageButton_Delete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if(TreeView_Main.CheckedNodes.Count > 0)
            {
                foreach(TreeNode node in TreeView_Main.CheckedNodes)
                {
                    SPUDataaccess db = new SPUDataaccess();
                    Organisasi org = db.SPUDataContent.Organisasis.Where(p => p.OrgID.Equals(node.Value)).Single<Organisasi>();
                    db.SPUDataContent.Organisasis.DeleteOnSubmit(org);
                    db.SPUDataContent.SubmitChanges();
                }
            }
        }
        catch (Exception ex)
        {

        }

        PopulateNodes();
        UpdatePanel_Main.Update();
    }

    protected void ImageButton_Close_Click(object sender, ImageClickEventArgs e)
    {
        this.MPE.Hide();
        PopulateNodes();
        UpdatePanel_Main.Update();
    }

    protected void Button_Edit_Click(object sender, EventArgs e)
    {
        Button_Edit.Visible = false;
        Button_Update.Visible = true;
        Button_Save.Visible = false;
        Button_AddChild.Visible = false;
        TextBox_Form_Parent.Enabled = false;
        MultiView_Content.SetActiveView(View_Form);

        string id = TreeView_Main.SelectedValue;
        SPUDataaccess db = new SPUDataaccess();
        Organisasi org = db.SPUDataContent.Organisasis.Where(p => p.OrgID.Equals(id)).Single<Organisasi>();

        TextBox_Form_ID.Text = org.OrgID;
        TextBox_Form_Name.Text = org.OrgName;
        TextBox_Form_Parent.Text = org.OrgParent;
        TextBox_Form_Desc.Text = org.OrgDesc;
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        Button_Edit.Visible = true;
        Button_Update.Visible = false;
        Button_Save.Visible = false;
        Button_AddChild.Visible = true;
        MultiView_Content.SetActiveView(View_Detail);

        string id = TreeView_Main.SelectedValue;
        SPUDataaccess db = new SPUDataaccess();
        Organisasi org = db.SPUDataContent.Organisasis.Where(p => p.OrgID.Equals(id)).Single<Organisasi>();
        org.OrgName = TextBox_Form_Name.Text;
        org.OrgDesc = TextBox_Form_Desc.Text;
        db.SPUDataContent.SubmitChanges();

        org = db.SPUDataContent.Organisasis.Where(p => p.OrgID.Equals(id)).Single<Organisasi>();
        Label_Value_ID.Text = org.OrgID;
        Label_Value_Name.Text = org.OrgName;
        Label_Value_Parent.Text = org.OrgParent;
        Label_Value_Desc.Text = org.OrgDesc;
    }

    protected void Button_Save_Click(object sender, EventArgs e)
    {
        SPUDataaccess db = new SPUDataaccess();
        Organisasi org = new Organisasi();
        org.OrgID = TextBox_Form_ID.Text;
        org.OrgName = TextBox_Form_Name.Text;
        org.OrgParent = TextBox_Form_Parent.Text;
        org.OrgDesc = TextBox_Form_Desc.Text;

        db.SPUDataContent.Organisasis.InsertOnSubmit(org);
        db.SPUDataContent.SubmitChanges();

        this.MPE.Hide();
        PopulateNodes();
        UpdatePanel_Panel_Modal.Update();
        UpdatePanel_Main.Update();
    }

    protected void Button_AddChild_Click(object sender, EventArgs e)
    {
        Button_Edit.Visible = false;
        Button_Update.Visible = false;
        Button_Save.Visible = true;
        Button_AddChild.Visible = false;
        TextBox_Form_ID.Enabled = true;
        MultiView_Content.SetActiveView(View_Form);

        TextBox_Form_ID.Text = string.Empty;
        TextBox_Form_Parent.Text = TreeView_Main.SelectedValue;
        TextBox_Form_Name.Text = string.Empty;
        TextBox_Form_Desc.Text = string.Empty;
    }
    #endregion    
}