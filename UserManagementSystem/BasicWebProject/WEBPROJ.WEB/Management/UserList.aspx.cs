using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Security;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using SPU.Dataaccess;
using SPU.Entities;

public partial class Management_UserList : System.Web.UI.Page
{
    int itemPerPage = Convert.ToInt32(WebConfigurationManager.AppSettings["itemPerPage"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Label_Title.Text = Page.Title;
        if(!IsPostBack)
        {
            GridView_Main_DataBind(0);
        }
    }

    #region TreeView
    public void PopulateNodes()
    {
        TreeView_Main.Nodes.Clear();

        DataTable data = GetTreeViewData();
        DataView child = GetParent(data);
        foreach(DataRowView row in child)
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
        foreach(Organisasi org in data)
        {
            dr = dt.NewRow();
            dr["OrgID"] = org.OrgID;
            dr["OrgParent"] = org.OrgParent;
            dr["OrgName"] = org.OrgName;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    void AddChild(DataTable parent, TreeNode node)
    {
        DataView child = GetChild(parent, node.Value);
        foreach(DataRowView row in child)
        {
            TreeNode childNode = new TreeNode();
            childNode.Text = row["OrgName"].ToString();
            childNode.Value = row["OrgID"].ToString();
            node.ChildNodes.Add(childNode);
            AddChild(parent, childNode);
        }
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

    protected void TreeView_Main_SelectedNodeChanged(object sender, EventArgs e)
    {
        this.PopEx.Commit(TreeView_Main.SelectedValue);
    }
    #endregion

    #region GridView
    protected void GridView_Main_DataBind(int NewPageIndex)
    {
        MembershipUserCollection allUser = Membership.GetAllUsers();
        GridView_Main.PageSize = itemPerPage;
        GridView_Main.PageIndex = NewPageIndex;
        GridView_Main.DataSource = allUser;
        GridView_Main.DataBind();
    }

    protected void GridView_Main_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button_Edit.Visible = true;
        Button_Save.Visible = false;
        Button_Update.Visible = false;
        Label_Modal_Title.Text = "Detail";
        MultiView_Content.SetActiveView(View_Detail);

        string username = Convert.ToString(GridView_Main.SelectedValue);

        MembershipUser membershipUser = Membership.GetUser(username);
        if(membershipUser != null)
        {
            Label_Value_Username.Text = membershipUser.UserName;
            Label_Value_NoInduk.Text = GetNomorInduk(username);
            Label_Value_NamaLengkap.Text = GetFullName(username);
            Label_Value_Alamat.Text = GetAlamat(username);
            Label_Value_Org.Text = GetOrganisasi(username);
            Label_Value_Email.Text = membershipUser.Email;         
            Label_Value_Roles.Text = GetRoles(username);
        }

        UpdatePanel_Panel_Modal.Update();
        this.MPE.Show();
    }

    protected void GridView_Main_Sorted(object sender, EventArgs e)
    {
        GridView_Main_DataBind(GridView_Main.PageIndex);
        GridView_Main.SelectedIndex = -1;
    }

    protected void GridView_Main_PageIndexChanged(object sender, EventArgs e)
    {
        GridView_Main_DataBind(GridView_Main.PageIndex);
        GridView_Main.SelectedIndex = -1;
    }

    protected void GridView_Main_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_Main_DataBind(e.NewPageIndex);
    }
    #endregion

    #region checkboxlist
    protected void CheckBoxList_Form_Roles_DataBind()
    {
        string[] roles = Roles.GetAllRoles();
        //string[] roles = { "Administrator", "member" };
        CheckBoxList_Form_Role.DataSource = roles;
        CheckBoxList_Form_Role.DataBind();
        for(int i = 0; i < CheckBoxList_Form_Role.Items.Count; i++)
        {
            CheckBoxList_Form_Role.Items[i].Selected = false;
        }
    }
    #endregion

    #region Action
    protected void ImageButton_Close_Click(object sender, ImageClickEventArgs e)
    {
        this.MPE.Hide();
    }

    protected void ImageButton_Delete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            foreach (GridViewRow gvr in GridView_Main.Rows)
            {
                CheckBox cbitem = (CheckBox)gvr.FindControl("CheckBox_Item");
                if (cbitem.Checked)
                {
                    int remainDiv = -1;
                    int resultDiv = Math.DivRem(gvr.DataItemIndex, itemPerPage, out remainDiv);
                    if (remainDiv != -1)
                    {
                        string username = Convert.ToString(GridView_Main.DataKeys[remainDiv].Value);
                        MembershipUser _MembershipUser = Membership.GetUser(username);
                        if (_MembershipUser != null)
                        {
                            Membership.DeleteUser(_MembershipUser.UserName);
                        }
                    }
                }
            }
        }catch(Exception ex)
        {

        }

        GridView_Main_DataBind(GridView_Main.PageIndex); 
        GridView_Main.SelectedIndex = -1; 
        UpdatePanel_Main.Update();
    }

    protected void ImageButton_Tambah_Click(object sender, ImageClickEventArgs e)
    {
        Button_Edit.Visible = false; 
        Button_Update.Visible = false; 
        Button_Save.Visible = true;
        Label_Modal_Title.Text = "Tambah Data";
        MultiView_Content.SetActiveView(View_Form); 
        TextBox_Form_Username.Enabled = true; 
        RFV_Form_Password.Enabled = true; 
        RFV_Form_PasswordC.Enabled = true;    
             
        TextBox_Form_Username.Text = String.Empty; 
        TextBox_Form_Password.Text = String.Empty; 
        TextBox_Form_PasswordC.Text = String.Empty; 
        TextBox_Form_NoInduk.Text = String.Empty; 
        TextBox_Form_NamaLengkap.Text = String.Empty; 
        TextBox_Form_Email.Text = String.Empty; 
        TextBox_Form_Org.Text = String.Empty; 
        TextBox_Form_Alamat.Text = String.Empty; 

        CheckBoxList_Form_Roles_DataBind(); 
        PopulateNodes(); 
        UpdatePanel_Panel_Modal.Update(); 
        this.MPE.Show();
    }

    protected void Button_Save_Click(object sender, EventArgs e)
    {
        string username = TextBox_Form_Username.Text; 
        String roles = String.Empty; 
        Membership.CreateUser(username, TextBox_Form_Password.Text, TextBox_Form_Email.Text); 

        ProfileCommon profileCommon = Profile.GetProfile(username); 
        profileCommon.Alamat = TextBox_Form_Alamat.Text;
        profileCommon.Nama = TextBox_Form_NamaLengkap.Text; 
        profileCommon.NIK = TextBox_Form_NoInduk.Text; 
        profileCommon.OrgID = TextBox_Form_Org.Text; 
        profileCommon.Save(); 

        for (int i = 0; i < CheckBoxList_Form_Role.Items.Count; i++) 
        { 
            if (CheckBoxList_Form_Role.Items[i].Selected) 
            { 
                if (!String.IsNullOrEmpty(roles)) 
                    roles += ","; 

                    roles += CheckBoxList_Form_Role.Items[i].Value; 
            } 
            
            CheckBoxList_Form_Role.Items[i].Selected = false; 
        } 
        
        //tambahan
        //string[] splitRoles = { roles };
        Roles.AddUserToRoles(username, roles.Split(','));
        GridView_Main_DataBind(GridView_Main.PageIndex); 
        GridView_Main.SelectedIndex = -1; 
        UpdatePanel_Main.Update(); 
        this.MPE.Hide();
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        Button_Edit.Visible = true; 
        Button_Update.Visible = false; 
        Button_Save.Visible = false; 
        Label_Modal_Title.Text = "Detail"; 
        MultiView_Content.SetActiveView(View_Detail); 
        
        string username = GridView_Main.SelectedValue.ToString(); 
        MembershipUser membershipUser = Membership.GetUser(username); 
        if (membershipUser != null) 
        { 
            membershipUser.Email = TextBox_Form_Email.Text; 
            Membership.UpdateUser(membershipUser); 
            if (!String.IsNullOrEmpty(TextBox_Form_Password.Text)) 
            { 
                membershipUser.ChangePassword(membershipUser.ResetPassword(), TextBox_Form_Password.Text); 
            } 

            ProfileCommon profileCommon = Profile.GetProfile(username); 
            profileCommon.Alamat = TextBox_Form_Alamat.Text; 
            profileCommon.Nama = TextBox_Form_NamaLengkap.Text; 
            profileCommon.NIK = TextBox_Form_NoInduk.Text; 
            profileCommon.OrgID = TextBox_Form_Org.Text; 
            profileCommon.Save(); 
            
            for (int i = 0; i < CheckBoxList_Form_Role.Items.Count; i++) 
            { 
                if (CheckBoxList_Form_Role.Items[i].Selected.Equals(false)) 
                { 
                    if (Roles.IsUserInRole(username, CheckBoxList_Form_Role.Items[i].Value)) 
                    { 
                        Roles.RemoveUserFromRole(username, CheckBoxList_Form_Role.Items[i].Value); 
                    }
                } else { 
                    if (!Roles.IsUserInRole(username, CheckBoxList_Form_Role.Items[i].Value)) 
                    { 
                        Roles.AddUserToRole(username, CheckBoxList_Form_Role.Items[i].Value); 
                    } 
                }
            }

            membershipUser = Membership.GetUser(username); 
            if (membershipUser != null) 
            { 
                Label_Value_Username.Text = membershipUser.UserName;
                Label_Value_Email.Text = membershipUser.Email; 
                Label_Value_NoInduk.Text = GetNomorInduk(username); 
                Label_Value_NamaLengkap.Text = GetFullName(username); 
                Label_Value_Alamat.Text = GetAlamat(username); 
                Label_Value_Org.Text = GetOrganisasi(username); 
                Label_Value_Roles.Text = GetRoles(username);
            } 
            
            GridView_Main_DataBind(GridView_Main.PageIndex); 
            UpdatePanel_Main.Update();
        }
    }

    protected void Button_Edit_Click(object sender, EventArgs e)
    {
        Button_Edit.Visible = false; 
        Button_Update.Visible = true; 
        Button_Save.Visible = false;
        Label_Modal_Title.Text = "Edit Data";
        MultiView_Content.SetActiveView(View_Form); 
        TextBox_Form_Username.Enabled = false; 
        RFV_Form_Password.Enabled = false; 
        RFV_Form_PasswordC.Enabled = false; 
        
        CheckBoxList_Form_Roles_DataBind(); 
        PopulateNodes(); 

        string username = GridView_Main.SelectedValue.ToString(); 
        MembershipUser membershipUser = Membership.GetUser(username); 
        TextBox_Form_Username.Text = membershipUser.UserName; 
        TextBox_Form_Email.Text = membershipUser.Email; 
        TextBox_Form_NoInduk.Text = GetNomorInduk(username); 
        TextBox_Form_NamaLengkap.Text = GetFullName(username); 
        TextBox_Form_Alamat.Text = GetAlamat(username); 
        TextBox_Form_Org.Text = GetOrganisasiId(username); 

        String[] arrRoles = Roles.GetRolesForUser(membershipUser.UserName); 
        CheckBoxList_Form_Role.ClearSelection(); 
        for (int i = 0; i < arrRoles.Length; i++) 
        { 
            ListItem li = CheckBoxList_Form_Role.Items.FindByValue(arrRoles[i]); 
            if (li != null) 
            { 
                CheckBoxList_Form_Role.Items.FindByValue(arrRoles[i]).Selected = true; 
            } 
        }
    }
    #endregion

    #region AdditionalMethod

    protected string GetFullName(string username)
    {
        ProfileCommon profilecommon = Profile.GetProfile(username);
        string fullname = profilecommon.Nama;
        if(String.IsNullOrEmpty(fullname.Trim())) return "-";
        return fullname;
    }
    protected string GetNomorInduk(string username)
    {
        ProfileCommon profilecommon = Profile.GetProfile(username);
        string nik = profilecommon.NIK;
        if (String.IsNullOrEmpty(nik.Trim())) return "-";
        return nik;
    }

    protected string GetAlamat(string username)
    {
        ProfileCommon profilecommon = Profile.GetProfile(username);
        string alamat = profilecommon.Alamat;
        if (String.IsNullOrEmpty(alamat.Trim())) return "-";
        return alamat;
    }

    protected string GetOrganisasi(string username)
    {
        ProfileCommon profilecommon = Profile.GetProfile(username);
        string orgId = profilecommon.OrgID;
        string orgName = string.Empty;

        try
        {
            SPUDataaccess db = new SPUDataaccess();
            Organisasi org = db.SPUDataContent.Organisasis.Where(p => p.OrgID.Equals(orgId)).Single<Organisasi>();
            while(!org.OrgParent.Equals("0"))
            {
                orgName = org.OrgName + "<br />" + orgName;
                org = db.SPUDataContent.Organisasis.Where(p => p.OrgID.Equals(org.OrgParent)).Single<Organisasi>();
            }
        }
        catch
        {
            orgName = "-";
        }
        return orgName;
    }

    protected string GetOrganisasiId(string username)
    {
        ProfileCommon profilecommon = Profile.GetProfile(username);
        string orgID = profilecommon.OrgID;
        return orgID;
    }

    public string GetRoles(string username)
    {
        string returnVal = string.Empty;

        try
        {
            string[] arrRoles = Roles.GetRolesForUser((string)username);
            for(int i = 0; i < arrRoles.Length; i++)
            {
                returnVal += arrRoles[i] + ", ";
            }

            int lengthVal = returnVal.Length;
            returnVal = returnVal.Substring(0, returnVal.Length - 2);
        }
        catch { }

        return returnVal;
    }
    #endregion

}