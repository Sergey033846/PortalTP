<%@ Page Title="Личный кабинет по ТП" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="MyContracts.aspx.cs" Inherits="portaltp.Cabinet.MyContracts" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1"
    Width="100%" Caption="Мои договоры" DataSourceID="SqlDataSourceMyContracts" KeyFieldName="id_contract" SettingsResizing-ColumnResizeMode="Control">
        <ClientSideEvents RowDblClick="function(s, e) {
                window.location.href = 'ContractInfo.aspx?cmd=view&id='+s.GetRowKey(e.visibleIndex);
	        }"></ClientSideEvents>

        <SettingsContextMenu Enabled="True">
        </SettingsContextMenu>
        <SettingsPager PageSize="10" />
        <SettingsBehavior AllowFocusedRow="True"/>
        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
        <Columns>            
            <dx:GridViewDataTextColumn FieldName="contract_number_1c" VisibleIndex="1" Caption="Номер договора">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="contract_date_1c" VisibleIndex="2" Caption="Дата договора">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataDateColumn>            
            <dx:GridViewDataTextColumn FieldName="caption_contractstatus" VisibleIndex="3" Caption="Статус">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>            
            <dx:GridViewDataTextColumn Caption="Примечание" VisibleIndex="4" PropertiesTextEdit-NullDisplayText="нажмите дважды, чтобы посмотреть">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataTextColumn>
        </Columns>
        <Styles>
            <Cell HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
            </Cell>
        </Styles>
        <Paddings Padding="0px" />
        <Border BorderWidth="0px" />
        <BorderBottom BorderWidth="1px" />
        <%-- DXCOMMENT: Configure ASPxGridView's columns in accordance with datasource fields --%>
    </dx:ASPxGridView>


     <asp:SqlDataSource ID="SqlDataSourceMyContracts" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" 
         ></asp:SqlDataSource>
</asp:Content>
