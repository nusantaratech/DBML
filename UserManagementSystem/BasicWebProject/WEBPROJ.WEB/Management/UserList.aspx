<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage/Member.master" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="Management_UserList" %>
<%@ Register  Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadMember" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainMember" Runat="Server">
    <%-- Main View - Start --%> 
    <div id="ApplicationContainer"> 
        <%-- Title & Buttons - Start --%> 
        <div id="TitleToolContainer"> 
            <div id="Title"> 
                <asp:Label ID="Label_Title" runat="server" CssClass="TextTitle" Text="Label"></asp:Label> 
            </div> 
            <asp:UpdatePanel ID="UpdatePanel_ActionTool" runat="server" UpdateMode="Conditional"> 
                <ContentTemplate> 
                    <div id="ActionTool"> 
                        <div class="ActionToolItemText"> 
                            <asp:Label ID="Label_Delete" runat="server" AssociatedControlID="ImageButton_Delete" Text="Hapus"></asp:Label> 
                        </div> 
                        <div class="ActionToolItem"> 
                            <asp:ImageButton ID="ImageButton_Delete" runat="server" ImageUrl="~/App_Themes/MyThemes/icons/icons8-delete-35.png" onclick="ImageButton_Delete_Click" Width="25" Height="25" /> 
                            <ajaxToolkit:ConfirmButtonExtender ID="CBE_ConfirmationDelete" runat="server" TargetControlID="ImageButton_Delete" 
                                ConfirmText="Apakah data akan dihapus?" />
                        </div> 
                        <div class="ActionToolItemText"> 
                            <asp:Label ID="Label_Tambah" runat="server" Text="Tambah"></asp:Label> 
                        </div> 
                        <div class="ActionToolItem"> 
                            <asp:ImageButton ID="ImageButton_Tambah" runat="server" ImageUrl="~/App_Themes/MyThemes/icons/Add.png" onclick="ImageButton_Tambah_Click" Width="25" Height="25" /> 
                        </div> 
                    </div> 
                    <div style="visibility:hidden"> 
                        <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton> 
                        <ajaxToolkit:ModalPopupExtender ID="MPE" runat="server" TargetControlID="LinkButton1" PopupControlID="Panel_Modal" PopupDragHandleControlID="Panel_Modal_Header"
                            BackgroundCssClass="modalBackground" DropShadow="true" /> 
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
                    <asp:GridView ID="GridView_Main" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" DataKeyNames="UserName" 
                        AllowPaging="true" AllowSorting="true" Width="100%" BorderWidth="1" OnSelectedIndexChanged="GridView_Main_SelectedIndexChanged" 
                        OnSorted="GridView_Main_Sorted" OnPageIndexChanged="GridView_Main_PageIndexChanged" 
                        OnPageIndexChanging="GridView_Main_PageIndexChanging" > 
                        <EmptyDataTemplate> 
                            <div class="EmptyDataMessage"> 
                                <asp:Label ID="Label_GridView_Empty" runat="server" Text="Data tidak ditemukan."></asp:Label> 
                            </div> 
                        </EmptyDataTemplate> 
                        <HeaderStyle CssClass="GridviewHeader" />
                        <SelectedRowStyle CssClass="GridviewSelect" /> 
                        <AlternatingRowStyle CssClass="GridviewAlternate" /> 
                        <Columns> 
                            <asp:TemplateField> 
                                <HeaderTemplate> 
                                    <div style="text-align:center">i</div> 
                                </HeaderTemplate> 
                                <ItemTemplate> 
                                    <asp:CheckBox ID="CheckBox_Item" runat="server" /> 
                                </ItemTemplate> 
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" /> 
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Aksi" ItemStyle-CssClass="GridviewItem" ItemStyle-VerticalAlign="Top" 
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" ItemStyle-Wrap="false"> 
                                <ItemTemplate> 
                                    <asp:LinkButton ID="LinkButton_Select" runat="server" CommandName="Select">Pilih</asp:LinkButton> 
                                </ItemTemplate> 
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Username"  DataField="UserName" ItemStyle-CssClass="GridviewItem" 
                                HeaderStyle-Width="15%" ItemStyle-VerticalAlign="Top" /> 
                            <asp:TemplateField HeaderText="Nomor Induk" ItemStyle-CssClass="GridviewItem" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"> 
                                <ItemTemplate> 
                                    <%# GetNomorInduk((String)DataBinder.Eval(Container.DataItem, "UserName")) %> 
                                </ItemTemplate> 
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Name Lengkap" ItemStyle-CssClass="GridviewItem" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%"> 
                                <ItemTemplate> 
                                    <%# GetFullName((String)DataBinder.Eval(Container.DataItem, "UserName")) %> 
                                </ItemTemplate> 
                            </asp:TemplateField> 
                            <asp:BoundField HeaderText="Email" DataField="Email" ItemStyle-CssClass="GridviewItem" HeaderStyle-Width="20%" ItemStyle-VerticalAlign="Top" />
                            <asp:TemplateField HeaderText="Role" ItemStyle-VerticalAlign="Top" ItemStyle-CssClass="GridviewItem" HeaderStyle-Width="25%"> 
                                <ItemTemplate> 
                                    <%# GetRoles((String)DataBinder.Eval(Container.DataItem, "UserName")) %> 
                                </ItemTemplate> 
                            </asp:TemplateField> 
                        </Columns> 
                        <PagerSettings Mode="NumericFirstLast" FirstPageImageUrl="~/App_Themes/SPU/icons/icon_nav_first_on.gif" 
                            FirstPageText="Pertama" LastPageImageUrl="~/App_Themes/SPU/icons/icon_nav_last_on.gif" LastPageText="Akhir" 
                            NextPageImageUrl="~/App_Themes/SPU/icons/icon_nav_next_on.gif" PreviousPageImageUrl="~/App_Themes/SPU/icons/icon_nav_prev_on.gif" /> 
                        <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="#543000" /> 
                    </asp:GridView> 
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
                            <asp:ImageButton ID="ImageButton_Close" runat="server" ImageUrl="~/App_Themes/SPU/icons/icon_modal_button_close.gif" AlternateText="Tutup" 
                                onclick="ImageButton_Close_Click" />
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
                                <div style="padding-left:5px;padding-right:5px"> 
                                    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Height="250px"> 
                                        <ajaxToolkit:TabPanel ID="TabPanel_General" runat="server" HeaderText="General"> 
                                            <ContentTemplate> 
                                                <table> 
                                                    <tr> 
                                                        <td> <asp:Label ID="Label_Detail_Username" SkinID="LabelDetail" runat="server" Text="Username"></asp:Label> </td> 
                                                        <td class="SeparatorH">:</td> 
                                                        <td> <asp:Label ID="Label_Value_Username" runat="server" Text="Label"></asp:Label> </td> 
                                                    </tr> 
                                                    <tr> 
                                                        <td> <asp:Label ID="Label_Detail_NoInduk" SkinID="LabelDetail" runat="server" Text="No. Induk"></asp:Label> </td> 
                                                        <td class="SeparatorH">:</td> 
                                                        <td> <asp:Label ID="Label_Value_NoInduk" runat="server" Text="Label"></asp:Label> </td> 
                                                    </tr> 
                                                    <tr> 
                                                        <td> <asp:Label ID="Label_Detail_NamaLengkap" SkinID="LabelDetail" runat="server" Text="Nama Lengkap"> </asp:Label> </td> 
                                                        <td class="SeparatorH">:</td> 
                                                        <td> <asp:Label ID="Label_Value_NamaLengkap" runat="server" Text="Label"> </asp:Label> </td> 
                                                    </tr> 
                                                    <tr> 
                                                        <td> <asp:Label ID="Label_Detail_Email" SkinID="LabelDetail" runat="server" Text="Email"> </asp:Label> </td> 
                                                        <td class="SeparatorH">:</td> 
                                                        <td><asp:Label ID="Label_Value_Email" runat="server" Text="Label"> </asp:Label> </td> 
                                                    </tr> 
                                                    <tr> 
                                                        <td valign="top"> <asp:Label ID="Label_Detail_Org" SkinID="LabelDetail" runat="server" Text="Organisasi"> </asp:Label> </td> 
                                                        <td valign="top" class="SeparatorH">:</td> 
                                                        <td> <asp:Label ID="Label_Value_Org" runat="server" Text="Label"> </asp:Label> </td> 
                                                    </tr> 
                                                    <tr> 
                                                        <td> <asp:Label ID="Label_Detail_Alamat" SkinID="LabelDetail" runat="server" Text="Alamat"> </asp:Label> </td> 
                                                        <td class="SeparatorH">:</td> 
                                                        <td> <asp:Label ID="Label_Value_Alamat" runat="server" Text="Label"> </asp:Label> </td> 
                                                    </tr> 
                                                </table> 
                                            </ContentTemplate> 
                                        </ajaxToolkit:TabPanel> 
                                        <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Role"> 
                                            <ContentTemplate> 
                                                <asp:Label ID="Label_Value_Roles" runat="server" Text="Label"> </asp:Label> 
                                            </ContentTemplate> 
                                        </ajaxToolkit:TabPanel> 
                                    </ajaxToolkit:TabContainer> 
                                </div>
                            </asp:View> 
                            <asp:View ID="View_Form" runat="server"> 
                                <table> 
                                    <tr> 
                                        <td colspan="3"> <asp:ValidationSummary ID="ValidationSummary_Form" runat="server" ValidationGroup="Default" /> </td> 
                                    </tr> 
                                    <tr> 
                                        <td> <asp:Label ID="Label_Form_Username" SkinID="LabelDetail" runat="server" Text="Username"></asp:Label> </td> 
                                        <td class="SeparatorH">:</td> 
                                        <td> 
                                            <asp:TextBox ID="TextBox_Form_Username" SkinID="TextboxForm" runat="server" ValidationGroup="Default"> </asp:TextBox> 
                                            <asp:RequiredFieldValidator ID="RFV_Form_Username" runat="server" ValidationGroup="Default" ControlToValidate="TextBox_Form_Username" 
                                            ErrorMessage="Username harus diisi." ToolTip="Username harus diisi.">* </asp:RequiredFieldValidator> 
                                        </td> 
                                    </tr> 
                                    <tr> 
                                        <td> <asp:Label ID="Label_Form_Password" SkinID="LabelDetail" runat="server" Text="Password"></asp:Label> </td> 
                                        <td class="SeparatorH">:</td> 
                                        <td> 
                                            <asp:TextBox ID="TextBox_Form_Password" SkinID="TextboxForm" runat="server" ValidationGroup="Default" TextMode="Password"> </asp:TextBox> 
                                            <asp:RequiredFieldValidator ID="RFV_Form_Password"  runat="server"  ValidationGroup="Default" ControlToValidate="TextBox_Form_Password" 
                                               ErrorMessage="Password harus diisi." ToolTip="Password harus diisi.">* </asp:RequiredFieldValidator> 
                                        </td> 
                                    </tr> 
                                    <tr> 
                                        <td>&nbsp;</td> 
                                        <td class="SeparatorH">:</td> 
                                        <td> 
                                            <asp:TextBox ID="TextBox_Form_PasswordC" SkinID="TextboxForm" runat="server"  ValidationGroup="Default" TextMode="Password"> </asp:TextBox> 
                                            <asp:RequiredFieldValidator ID="RFV_Form_PasswordC"  runat="server"  ValidationGroup="Default" ControlToValidate="TextBox_Form_PasswordC" 
                                               ErrorMessage="Password harus diisi." ToolTip="Password harus diisi.">* </asp:RequiredFieldValidator> 
                                            <asp:CompareValidator ID="CV_Form_Password" runat="server" ValidationGroup="Default" ControlToValidate="TextBox_Form_PasswordC" 
                                                ControlToCompare="TextBox_Form_Password" Operator="Equal" 
                                                ErrorMessage="Password harus sama." ToolTip="Password harus sama.">* </asp:CompareValidator> 
                                        </td> 
                                    </tr> 
                                    <tr> 
                                        <td> 
                                            <asp:Label ID="Label_Form_NIK" SkinID="LabelDetail" runat="server" Text="No. Induk"></asp:Label> 
                                        </td> 
                                        <td class="SeparatorH">:</td> 
                                        <td> <asp:TextBox ID="TextBox_Form_NoInduk" SkinID="TextboxForm" runat="server"></asp:TextBox> </td> 
                                    </tr> 
                                    <tr>
                                         <td> <asp:Label ID="Label_Form_NamaLengkap" SkinID="LabelDetail" runat="server" Text="Nama Lengkap"></asp:Label> </td> 
                                        <td class="SeparatorH">:</td> 
                                        <td> <asp:TextBox ID="TextBox_Form_NamaLengkap" SkinID="TextboxForm" runat="server"></asp:TextBox> </td> 
                                    </tr> 
                                    <tr> 
                                        <td> <asp:Label ID="Label_Form_Email" SkinID="LabelDetail" runat="server" Text="Email"></asp:Label> </td> 
                                        <td class="SeparatorH">:</td>
                                        <td> 
                                            <asp:TextBox ID="TextBox_Form_Email" SkinID="TextboxForm" runat="server"></asp:TextBox> 
                                            <asp:RequiredFieldValidator ID="RFV_Form_Email" runat="server"  ValidationGroup="Default" ControlToValidate="TextBox_Form_Email" 
                                                ErrorMessage="Email harus diisi." ToolTip="Email harus diisi.">* </asp:RequiredFieldValidator> 
                                        </td> 
                                    </tr> 
                                    <tr> 
                                        <td> <asp:Label ID="Label_Form_Organisasi" SkinID="LabelDetail" runat="server" Text="Organisasi"></asp:Label> </td> 
                                        <td class="SeparatorH">:</td> 
                                        <td> 
                                            <asp:TextBox ID="TextBox_Form_Org" SkinID="TextboxForm" runat="server"></asp:TextBox> 
                                            <ajaxToolkit:PopupControlExtender ID="PopEx" runat="server" TargetControlID="TextBox_Form_Org" PopupControlID="Panel_Org" Position="Bottom" /> 
                                            <asp:Panel ID="Panel_Org" runat="server" BorderColor="#000000" BackColor="#534000"> 
                                                <div id="PopupPanel"> 
                                                    <asp:TreeView ID="TreeView_Main" runat="server" ShowLines="true" OnSelectedNodeChanged="TreeView_Main_SelectedNodeChanged"> 
                                                        <SelectedNodeStyle Font-Bold="true" /> 
                                                    </asp:TreeView> 
                                                </div> 
                                            </asp:Panel> 
                                        </td> 
                                    </tr> 
                                    <tr> 
                                        <td valign="top"><asp:Label ID="Label_Form_Alamat" SkinID="LabelDetail" runat="server" Text="Alamat"></asp:Label> </td> 
                                        <td class="SeparatorH" valign="top">:</td> 
                                        <td> <asp:TextBox ID="TextBox_Form_Alamat" SkinID="TextboxForm" runat="server" TextMode="MultiLine"></asp:TextBox> </td> 
                                    </tr> 
                                    <tr> 
                                        <td valign="top"><asp:Label ID="Label_Form_Role" SkinID="LabelDetail" runat="server" Text="Role"></asp:Label> </td> 
                                        <td valign="top" class="SeparatorH">:</td>
                                        <td> 
                                            <asp:CheckBoxList ID="CheckBoxList_Form_Role" runat="server"> </asp:CheckBoxList> 
                                        </td> 
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
                        <asp:Button ID="Button_Edit" SkinID="ButtonModal" runat="server" Text="Edit" onclick="Button_Edit_Click" /> 
                        <asp:Button ID="Button_Update" SkinID="ButtonModal" ValidationGroup="Default" runat="server" Text="Update" onclick="Button_Update_Click" /> 
                        <asp:Button ID="Button_Save" SkinID="ButtonModal" ValidationGroup="Default" runat="server" Text="Simpan" onclick="Button_Save_Click" /> 
                    </div> 
                    <%-- Modal Windows Action - End --%> 
                </div> 
            </ContentTemplate> 
        </asp:UpdatePanel> 
    </asp:Panel> 
    <%-- Modal Windows - End --%>
</asp:Content>

