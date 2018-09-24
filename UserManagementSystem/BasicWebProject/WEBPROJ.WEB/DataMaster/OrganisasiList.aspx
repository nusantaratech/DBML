<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage/Member.master" AutoEventWireup="true" CodeFile="OrganisasiList.aspx.cs" Inherits="DataMaster_OrganisasiList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadMember" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainMember" Runat="Server">
    <%-- Main View - Start --%> 
    <div id="ApplicationContainer">
         <%-- Title & Buttons - Start --%> 
        <div id="TitleToolContainer">
             <div id="Title"> 
                 <asp:Label ID="Label_Title" CssClass="TextTitle" runat="server" Text="Label"></asp:Label> 
             </div> 
            <asp:UpdatePanel ID="UpdatePanel_ActionTool" runat="server" UpdateMode="Conditional" > 
                <ContentTemplate> 
                    <div id="ActionTool"> 
                        <div class="ActionToolItemText"> 
                            <asp:Label ID="Label_Delete" runat="server" AssociatedControlID="ImageButton_Delete" Text="Hapus"></asp:Label> 
                        </div> 
                        <div class="ActionToolItem"> 
                            <asp:ImageButton ID="ImageButton_Delete" ImageUrl="~/App_Themes/MyThemes/icons/icons8-delete-35.png" runat="server" onclick="ImageButton_Delete_Click" Width="20px" Height="20px" />
                            <ajaxToolkit:ConfirmButtonExtender ID="CBE_ConfirmationDelete" runat="server" ConfirmText="Apakah data akan dihapus?" 
                                TargetControlID="ImageButton_Delete" /> 
                        </div> 
                    </div> 
                    <div style="visibility:hidden"> 
                        <asp:LinkButton ID="LinkButton1" runat="server"> LinkButton</asp:LinkButton> 
                        <ajaxToolkit:ModalPopupExtender ID="MPE" runat="server" TargetControlID="LinkButton1" PopupControlID="Panel_Modal" 
                            BackgroundCssClass="modalBackground" DropShadow="true" PopupDragHandleControlID="Panel_Modal_Header" /> 
                    </div> 
                </ContentTemplate> 
            </asp:UpdatePanel> 
        </div> 
        <%-- Title & Buttons - End --%> 
        <div class="Separator"></div>
         <%-- Main Data - Start --%> 
        <div id="ContentContainer"> 
            <asp:UpdatePanel ID="UpdatePanel_Main" UpdateMode="Conditional" runat="server"> 
                <ContentTemplate> 
                    <asp:TreeView ID="TreeView_Main" runat="server" ShowLines="true" ShowCheckBoxes="Leaf" OnSelectedNodeChanged="TreeView_Main_SelectedNodeChanged"> 
                        <SelectedNodeStyle Font-Bold="true" /> 
                    </asp:TreeView> 
                </ContentTemplate> 
            </asp:UpdatePanel> 
        </div> 
        <%-- Main Data - End --%> 
    </div> 
    <%-- Main View - End --%>

    <%-- Modal Windows - Start --%> 
    <asp:Panel ID="Panel_Modal" CssClass="PanelModal" runat="server"> 
        <asp:UpdatePanel ID="UpdatePanel_Panel_Modal" UpdateMode="Conditional" runat="server"> 
            <ContentTemplate> 
                <div class="PanelModalHeader"> 
                    <%-- Header Modal Windows - Start --%> 
                    <asp:Panel ID="Panel_Modal_Header" runat="server"> 
                        <div class="TextTitleModalHeader"> 
                            <asp:Label ID="Label_Modal_Title" runat="server" Text="Detail"></asp:Label> 
                        </div> 
                        <div class="ActionCloseModalHeader"> 
                            <asp:ImageButton ID="ImageButton_Close" runat="server" ImageUrl="~/App_Themes/MyThemes/icons/icon_modal_button_close.gif" 
                                AlternateText="Tutup" onclick="ImageButton_Close_Click" /> 
                        </div> 
                    </asp:Panel> 
                    <%-- Header Modal Windows - End --%> 
                </div> 
                <div class="Separator"></div> 
                <div class="PanelModalContentContainer"> 
                    <asp:Panel ID="Panel_Modal_Content" CssClass="PanelModalContent" runat="server"> 
                        <%-- Message, Detail & Form - Start --%> 
                        <asp:MultiView ID="MultiView_Content" runat="server"> 
                            <asp:View ID="View_Detail" runat="server"> 
                                <table> 
                                    <tr> 
                                        <td> <asp:Label ID="Label_Detail_ID" SkinID="LabelDetail" runat="server" Text="ORG ID"></asp:Label> </td> 
                                        <td class="SeparatorH">:</td> 
                                        <td> <asp:Label ID="Label_Value_ID" runat="server" Text="Label"></asp:Label> </td> 
                                    </tr> 
                                    <tr> 
                                        <td> <asp:Label ID="Label_Detail_Parent" SkinID="LabelDetail" runat="server" Text="ORG PARENT"></asp:Label> </td> 
                                        <td class="SeparatorH">:</td> 
                                        <td> <asp:Label ID="Label_Value_Parent" runat="server" Text="Label"></asp:Label> </td> 
                                    </tr> 
                                    <tr> 
                                        <td> <asp:Label ID="Label_Detail_Name" SkinID="LabelDetail" runat="server" Text="ORG NAME"></asp:Label> </td> 
                                        <td class="SeparatorH">:</td> 
                                        <td> <asp:Label ID="Label_Value_Name" runat="server" Text="Label"></asp:Label> </td> 
                                    </tr> 
                                    <tr> 
                                        <td> <asp:Label ID="Label_Detail_desc" SkinID="LabelDetail" runat="server" Text="DESCRIPTION"></asp:Label> </td> 
                                        <td class="SeparatorH">:</td>
                                        <td> <asp:Label ID="Label_Value_Desc" runat="server" Text="Label"></asp:Label> </td> 
                                    </tr> 
                                </table> 
                            </asp:View> 
                            <asp:View ID="View_Form" runat="server"> 
                                <table> 
                                    <tr> 
                                        <td colspan="3"> <asp:ValidationSummary ID="ValidationSummary_Form" runat="server" ValidationGroup="Default" /> </td> 
                                    </tr> 
                                    <tr> 
                                        <td> <asp:Label ID="Label_Form_ID" runat="server" SkinID="LabelDetail" Text="ORG ID"></asp:Label> </td> 
                                        <td class="SeparatorH">:</td> 
                                        <td> 
                                            <asp:TextBox ID="TextBox_Form_ID" runat="server" SkinID="TextboxForm" ValidationGroup="Default" ></asp:TextBox> 
                                            <asp:RequiredFieldValidator ID="RFV_Form_ID" runat="server" ValidationGroup="Default" ControlToValidate="TextBox_Form_ID" 
                                                ErrorMessage="ID harus diisi." ToolTip="ID harus diisi."> *</asp:RequiredFieldValidator> 
                                        </td> 
                                    </tr> 
                                    <tr> 
                                        <td> <asp:Label ID="Label_Form_Parent" runat="server" SkinID="LabelDetail" Text="ORG PARENT"></asp:Label> </td> 
                                        <td class="SeparatorH">:</td> 
                                        <td> <asp:TextBox ID="TextBox_Form_Parent" runat="server" SkinID="TextboxForm" ></asp:TextBox> </td>
                                    </tr> 
                                    <tr> 
                                        <td> <asp:Label ID="Label_Form_Name" runat="server" SkinID="LabelDetail" Text="NAME"></asp:Label> </td> 
                                        <td class="SeparatorH">:</td> 
                                        <td> <asp:TextBox ID="TextBox_Form_Name" runat="server" SkinID="TextboxForm" ValidationGroup="Default"></asp:TextBox> 
                                            <asp:RequiredFieldValidator ID="RFV_Form_Name" runat="server" ValidationGroup="Default" ControlToValidate="TextBox_Form_Name" 
                                                ErrorMessage="Nama harus diisi." ToolTip="Nama harus diisi."> *</asp:RequiredFieldValidator> 
                                        </td> 
                                    </tr> 
                                    <tr>
                                        <td> <asp:Label ID="Label_Form_Desc" SkinID="LabelDetail" runat="server" Text="DESCRIPTION"></asp:Label> </td> 
                                        <td class="SeparatorH">:</td> 
                                        <td> <asp:TextBox ID="TextBox_Form_Desc" SkinID="TextboxForm" runat="server"></asp:TextBox> </td> 
                                    </tr> 
                                </table> 
                            </asp:View> 
                            <asp:View ID="View_Message" runat="server"> 
                                <div class="Message"> 
                                    <asp:Label ID="Label_Message" runat="server" Text="Label"></asp:Label> 
                                </div> 
                            </asp:View> 
                        </asp:MultiView> 
                        <%-- Message, Detail & Form - End --%> 
                    </asp:Panel> 
                    <ajaxToolkit:RoundedCornersExtender ID="rce" TargetControlID="Panel_Modal_Content" Radius="13" Corners="All" BorderColor="#040404" runat="server" /> 
                </div> 
                <div class="PanelModalFooter"> 
                    <%-- Modal Windows Action - Start --%> 
                    <div class="PanelModalAction"> 
                        <asp:Button ID="Button_AddChild" SkinID="ButtonModal" runat="server" Text="Tambah" onclick="Button_AddChild_Click" /> 
                        <asp:Button ID="Button_Edit" SkinID="ButtonModal" runat="server" Text="Edit" onclick="Button_Edit_Click" /> 
                        <asp:Button ID="Button_Update" SkinID="ButtonModal" runat="server" Text="Update" ValidationGroup="Default" onclick="Button_Update_Click" /> 
                        <asp:Button ID="Button_Save" SkinID="ButtonModal" runat="server" Text="Simpan" ValidationGroup="Default" onclick="Button_Save_Click" /> 
                    </div> 
                    <%-- Modal Windows Action - End --%> 
                </div> 
            </ContentTemplate> 
        </asp:UpdatePanel> 
    </asp:Panel> 
    <%-- Modal Windows - End --%>
</asp:Content>

