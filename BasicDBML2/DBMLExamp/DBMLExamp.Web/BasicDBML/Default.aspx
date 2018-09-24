<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DBMLExamp.Web.BasicDBML.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView_Nasabah" runat="server" AutoGenerateColumns="false" DataKeyNames="CustomerID"
     OnRowDataBound="GridView_Nasabah_RowDataBound" OnRowEditing="GridView_Nasabah_RowEditing" OnRowCancelingEdit="GridView_Nasabah_RowCancelingEdit"
     OnRowUpdating="GridView_Nasabah_RowUpdating" OnRowDeleting="GridView_Nasabah_RowDeleting" EmptyDataText="No record has been added">
        <Columns>
            <asp:TemplateField HeaderText="Nama Nasabah" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="Label_CustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_CustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Negara Asal" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="Label_Country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox_Country" runat="server" Text='<%# Eval("Country") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="150" />
        </Columns>
    </asp:GridView>

    <table>
        <tr>
            <td>Customer ID <br /> <asp:TextBox ID="TextBox_CustomerID" runat="server" Width="140" /></td>
            <td>Customer Name <br /> <asp:TextBox ID="TextBox_CustomerName" runat="server" Width="140" /></td>
            <td>Country <br /> <asp:TextBox ID="TextBox_Country" runat="server" Width="140" /></td>
            <td><asp:Button ID="Button_Add" runat="server" Text="Add" OnClick="InsertData" /></td>
        </tr>
    </table>
</asp:Content>
