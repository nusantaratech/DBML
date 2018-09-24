<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage/Public.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Shared/Controls/FormLogin.ascx" TagName="Login" TagPrefix="CommonControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPublic" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPublic" Runat="Server">
    <div id="panelContainer">
        <asp:Panel ID="Panel_Login" CssClass="panelLogin" runat="server">
            <div id="loginContainer">
                <CommonControls:Login ID="LoginForm" runat="server" />
            </div>
        </asp:Panel>
        <ajaxToolkit:RoundedCornersExtender ID="RCE" runat="server" TargetControlID="Panel_Login" Radius="13" Corners="All" BorderColor="#b3b3b3" />
    </div>
</asp:Content>

