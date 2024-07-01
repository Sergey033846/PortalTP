<%@ Page Title="Пользователи" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="AdminUsers.aspx.cs" Inherits="portaltp.Cabinet.AdminUsers" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<dx:BootstrapCallbackPanel runat="server" ClientInstanceName="callbackPanel1" OnCallback="CallbackPanel1_Callback">
<ContentCollection>
<dx:ContentControl>
    
<div class="row">

    <div class="col-md-4">
        <dx:ASPxMenu ID="ASPxMenuAdminUsers" runat="server" OnItemClick="ASPxMenuAdminUsers_ItemClick" ShowAsToolbar="True">
            <Items>
                <dx:MenuItem Text="Сохранить в EXCEL" Name="Export2Excel" Image-Url="../Content/Images/excel-icon.png">
                    <Image Url="../Content/Images/excel-icon.png"></Image>
                </dx:MenuItem>

                <dx:MenuItem Text="Обновить данные" Name="menuItemLoadUsersFromIdentityMembership" Image-Url="../Content/Images/refresh-icon.png">
                    <Image Url="../Content/Images/refresh-icon.png"></Image>
                </dx:MenuItem>

                <dx:MenuItem Text="Проверка" Name="menuItemCompareUsersWithIdentityMembership" Image-Url="../Content/Images/compare-icon.png">
                    <Image Url="../Content/Images/compare-icon.png"></Image>
                </dx:MenuItem>
            </Items>
        </dx:ASPxMenu>    
    </div>

    <div class="col-md-2">                
        <dx:BootstrapRadioButton runat="server" Text="все" GroupName="RadioGroup" ID="RadioButton_UsersAll">
            <ClientSideEvents CheckedChanged="function onUpdateClick(s, e) {
                callbackPanel1.PerformCallback();                        
            }"></ClientSideEvents>
        </dx:BootstrapRadioButton>
    </div>

    <div class="col-md-2">
        <dx:BootstrapRadioButton runat="server" Text="неподтвержденные" GroupName="RadioGroup" ID="RadioButton_UsersNotApproved">
            <ClientSideEvents CheckedChanged="function onUpdateClick(s, e) {
                callbackPanel1.PerformCallback();                        
            }"></ClientSideEvents>
        </dx:BootstrapRadioButton>
    </div>

    <div class="col-md-2">
        <dx:BootstrapRadioButton runat="server" Text="заблокированные" GroupName="RadioGroup" ID="RadioButton_UsersLockedOut">
            <ClientSideEvents CheckedChanged="function onUpdateClick(s, e) {
                callbackPanel1.PerformCallback();                        
            }"></ClientSideEvents>
        </dx:BootstrapRadioButton>
    </div>
</div>

<div class="row">      
    <asp:PlaceHolder runat="server" ID="PlaceHolderInfo" Visible="false">
        <p class="alert alert-info alert-dismissible">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
            <asp:Literal runat="server" ID="PlaceHolderInfoText" Mode="Transform" />
        </p>
    </asp:PlaceHolder>
</div>

<dx:ASPxGridView ID="ASPxGridAdminUsers" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridAdminUsers"
Width="100%" Caption="Пользователи" DataSourceID="SqlDataSourceAdminUsers" KeyFieldName="id_user" EnableTheming="True" OnRowDeleting="ASPxGridAdminUsers_RowDeleting" SettingsResizing-ColumnResizeMode="Control" OnHtmlRowPrepared="ASPxGridAdminUsers_HtmlRowPrepared" SettingsPager-Position="TopAndBottom">
    <%--<ClientSideEvents RowDblClick="function(s, e) {                
            window.open('OrderInfo?id='+s.GetRowKey(e.visibleIndex), '_blank');
        }"></ClientSideEvents>--%>

    <SettingsContextMenu Enabled="False">
    </SettingsContextMenu>

    <SettingsAdaptivity>
        <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
    </SettingsAdaptivity>

    <SettingsPager PageSize="20" />
    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" ShowFooter="True" />
    <SettingsBehavior AllowFocusedRow="False" ConfirmDelete="True" />

    <SettingsResizing ColumnResizeMode="Disabled"></SettingsResizing>

    <SettingsDataSecurity AllowEdit="False" AllowInsert="False" />
    <SettingsSearchPanel Visible="True" />

    <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
    <Columns>            
        <dx:GridViewDataDateColumn FieldName="date_create_user" VisibleIndex="1" Caption="Дата регистрации">                
            <PropertiesDateEdit DisplayFormatString="">
            </PropertiesDateEdit>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataDateColumn> 
        
        <dx:GridViewDataTextColumn FieldName="user_nameingrid" VisibleIndex="4" Caption="Имя" CellStyle-HorizontalAlign="Left">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="caption_usertype" VisibleIndex="5" Caption="Тип">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="user_login" VisibleIndex="6" Caption="Логин" CellStyle-HorizontalAlign="Left">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="comment" VisibleIndex="8" Caption="Комментарий">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>        

        <dx:GridViewDataTextColumn FieldName="IsApproved" VisibleIndex="13" Caption="Подтвержден">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
            <DataItemTemplate>                    
                <dx:BootstrapButton ID="ButtonSetApproved" runat="server" AutoPostBack="True" Text=""
                    ToolTip="изменить" OnInit="ButtonSetApproved_Init" OnClick="ButtonSetApproved_Click">                                            
                </dx:BootstrapButton>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="IsLockedOut" VisibleIndex="14" Caption="Блокировка">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />  
            <DataItemTemplate>                    
                <dx:BootstrapButton ID="ButtonSetLockedOut" runat="server" AutoPostBack="True" Text=""
                    OnInit="ButtonSetLockedOut_Init" OnClick="ButtonSetLockedOut_Click">                                            
                </dx:BootstrapButton>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="date_last_login" VisibleIndex="15" Caption="Посл. вход">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                        
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="countZ" VisibleIndex="20" Caption="Кол-во заявок">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>

        <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="25" ShowClearFilterButton="True" Caption="Действия">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewCommandColumn>
    </Columns>

    <TotalSummary>    
        <dx:ASPxSummaryItem FieldName="countZ" SummaryType="Sum" />            
    </TotalSummary>

    <Styles>            
        <FocusedRow BackColor="Black" ForeColor="White">
        </FocusedRow>
        <Cell HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
        </Cell>
    </Styles>
    <Paddings Padding="0px" />
    <Border BorderWidth="0px" />
    <BorderBottom BorderWidth="1px" />        
</dx:ASPxGridView>
    
<dx:ASPxGridViewExporter ID="ASPxGridViewExporterOrders" runat="server" GridViewID="ASPxGridAdminUsers"></dx:ASPxGridViewExporter>

</dx:ContentControl>
</ContentCollection>
</dx:BootstrapCallbackPanel>

<asp:SqlDataSource ID="SqlDataSourceAdminUsers" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>

</asp:Content>
