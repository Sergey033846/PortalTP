<%@ Page Title="Журнал событий" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="ViewLog.aspx.cs" Inherits="portaltp.Cabinet.ViewLog" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<dx:BootstrapCallbackPanel runat="server" ClientInstanceName="callbackPanel1">
<ContentCollection>
<dx:ContentControl>
        
<dx:ASPxGridView ID="ASPxGridViewLog" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewLog"
Width="100%" Caption="Журнал событий" DataSourceID="SqlDataSourceViewLog" KeyFieldName="id_logrec" EnableTheming="True" SettingsResizing-ColumnResizeMode="Disabled" SettingsPager-Position="TopAndBottom">
    <%--<ClientSideEvents RowDblClick="function(s, e) {                
            window.open('OrderInfo?id='+s.GetRowKey(e.visibleIndex), '_blank');
        }"></ClientSideEvents>--%>

    <SettingsContextMenu Enabled="True">
    </SettingsContextMenu>

    <SettingsAdaptivity>
        <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
    </SettingsAdaptivity>

    <SettingsPager PageSize="20" />
    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />

    <SettingsResizing ColumnResizeMode="Control"></SettingsResizing>

    <SettingsDataSecurity AllowEdit="False" AllowInsert="False" AllowDelete="False" />
    <SettingsSearchPanel Visible="True" />

    <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
    <Columns>            
        <dx:GridViewDataDateColumn FieldName="logrec_datetime" VisibleIndex="1" Caption="Дата события">                
            <PropertiesDateEdit DisplayFormatString="">
            </PropertiesDateEdit>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataDateColumn>
    
        <dx:GridViewDataTextColumn FieldName="user_nameingrid" VisibleIndex="2" Caption="Пользователь" CellStyle-HorizontalAlign="Left">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>            

        <dx:GridViewDataTextColumn FieldName="user_login" VisibleIndex="3" Caption="Логин" CellStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="caption_userrole" VisibleIndex="4" Caption="Роль">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="caption_logeventtype" VisibleIndex="5" Caption="Событие">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="caption_registration_typeid" VisibleIndex="11" Caption="Вид регистрации">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>
        
    </Columns>
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

</dx:ContentControl>
</ContentCollection>
</dx:BootstrapCallbackPanel>

<asp:SqlDataSource ID="SqlDataSourceViewLog" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>

</asp:Content>
