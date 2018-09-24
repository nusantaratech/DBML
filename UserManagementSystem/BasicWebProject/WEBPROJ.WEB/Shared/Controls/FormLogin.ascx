<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FormLogin.ascx.cs" Inherits="Shared_Controls_FormLogin" %>

<div id="LoginHeader">
    <table>
        <tr>
            <td><img src="~/App_Themes/MyThemes/icons/LoginSmall.jpg" alt="INDC" runat="server" /></td>
            <td>Sistem pengelolaan user</td>
        </tr>
    </table>
</div>
<div id="LoginFormContainer">
    <asp:LoginView ID="LoginView_Public" runat="server">
        <LoggedInTemplate>
            Selamat datang
            <strong><asp:LoginName ID="LoginName1" runat="server" /></strong>
            |
            <asp:HyperLink ID="Hiperlink_Dashboard" runat="server" NavigateUrl="~/Account/Dashboard.aspx">Dashboard</asp:HyperLink>
            |
            <asp:LoginStatus ID="LoginStatus1" runat="server" LoginText="Logout" LogoutAction="Redirect" LogoutPageUrl="~/Default.aspx" />
        </LoggedInTemplate>
        <AnonymousTemplate>
            <asp:Login ID="Login_Public" runat="server" FailureText="Login failed. Please try again!" TitleText="" LoginButtonText="Sign In" PasswordLabelText="Password&nbsp; :"
               UserNameLabelText="User&nbsp;Name&nbsp; :" TextBoxStyle-Width="175px"></asp:Login>
        </AnonymousTemplate>
    </asp:LoginView>
</div>