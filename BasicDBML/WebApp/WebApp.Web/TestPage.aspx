<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="WebApp.Web.TestPage" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="background">
        <fieldset>
            <legend>User Detail</legend>
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell>Name :</asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="txtName" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Email :</asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="txtEmail" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Image</asp:TableCell>
                    <asp:TableCell><asp:FileUpload ID="fUpload" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </fieldset>
    </div>
    <br />
    <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="false" DataKeyNames="LinqID" PageSize="3" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" 
        OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting"
        BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
        <Columns>
            <asp:TemplateField HeaderText="Name" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Width="150" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtName" runat="server" Width="150" Text='<%# Eval("Name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Width="150" Text='<%# Eval("Email") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server" Width="150" Text='<%# Eval("Email") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Image" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:Image ID="fUpload" runat="server" ImageUrl='<%# Eval("Image") %>' Width="100" Height="100" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="150" />
        </Columns>
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />  
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />  
        <PagerSettings PageButtonCount="8" />  
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />  
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />  
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />  
        <SortedAscendingCellStyle BackColor="#FFF1D4" />  
        <SortedAscendingHeaderStyle BackColor="#B95C30" />  
        <SortedDescendingCellStyle BackColor="#F1E5CE" />  
        <SortedDescendingHeaderStyle BackColor="#93451F" />   
    </asp:GridView>
</asp:Content>
