<%@ Page Title="Договоры энергоснабжения" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="ViewEnergyDogovors.aspx.cs" Inherits="portaltp.Cabinet.ViewEnergyDogovors" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row">
        <div class="col-md-3">
            <dx:ASPxMenu ID="ASPxMenuViewEnergyDogovors" runat="server" OnItemClick="ASPxMenuViewEnergyDogovors_ItemClick" ShowAsToolbar="True">
                <Items>
                    <dx:MenuItem Text="Сохранить в EXCEL" Name="Export2Excel" Image-Url="../Content/Images/excel-icon.png">
                    </dx:MenuItem>
                </Items>
            </dx:ASPxMenu>    
        </div>
    </div>
    
    <br />
    <%--<dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton" OnClick="ASPxButton1_Click"></dx:ASPxButton>--%>

    <dx:ASPxGridView ID="ASPxGridViewEnergyDogovorsAll" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewEnergyDogovorsAll"
    Width="100%" Caption="Договоры энергоснабжения" DataSourceID="SqlDataSourceViewEnergyDogovors" KeyFieldName="id_dogovorenergo" EnableTheming="True" OnRowDeleting="ASPxGridViewEnergyDogovorsAll_RowDeleting" SettingsResizing-ColumnResizeMode="Disabled" OnHtmlRowPrepared="ASPxGridViewEnergyDogovorsAll_HtmlRowPrepared" SettingsPager-Position="TopAndBottom">
        <ClientSideEvents RowDblClick="function(s, e) {                
                window.open('DogovorEnergoInfo?id='+s.GetRowKey(e.visibleIndex), '_blank');
	        }"></ClientSideEvents>

        <SettingsContextMenu Enabled="True">
        </SettingsContextMenu>

        <SettingsAdaptivity>
        <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
        </SettingsAdaptivity>

        <SettingsPager PageSize="15" />
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />

        <SettingsResizing ColumnResizeMode="Disabled"></SettingsResizing>

        <SettingsDataSecurity AllowEdit="False" AllowInsert="False" AllowDelete="False" />
        <SettingsSearchPanel Visible="True" />

        <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
        <Columns>            
            <dx:GridViewDataDateColumn FieldName="date_create_de" VisibleIndex="1" Caption="Дата создания">                                
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn FieldName="v1C_Zayavitel" VisibleIndex="2" Caption="Потребитель" CellStyle-HorizontalAlign="Left">                
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                <DataItemTemplate>                
                    <a target="_blank" href="DogovorEnergoInfo?id=<%#Eval("id_dogovorenergo")%>" style="color: #337AB7"><%#Eval("v1C_Zayavitel")%></a>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn FieldName="date_create_zayavka" VisibleIndex="3" Caption="Дата подачи заявки">                
                <PropertiesDateEdit DisplayFormatString="">
                </PropertiesDateEdit>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn FieldName="zayavka_number_1c" VisibleIndex="4" Caption="Номер заявки присв.">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Дата заявки присв." FieldName="zayavka_date_1c" VisibleIndex="5">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn FieldName="caption_zayavkastatus" VisibleIndex="6" Caption="Статус заявки">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="v1C_adresEPU" VisibleIndex="7" Caption="Адрес ЭПУ" CellStyle-HorizontalAlign="Left">                
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                <CellStyle HorizontalAlign="Left"></CellStyle>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="nomer_dogovorenergo" VisibleIndex="11" Caption="Номер договора энергоснабж." CellStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="nomer_ls" VisibleIndex="12" Caption="Номер лицевого счета" CellStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataTextColumn FieldName="nomer_elektroustanovka" VisibleIndex="13" Caption="Номер электроустановки">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>
                        
            <%--<dx:GridViewCommandColumn ShowDeleteButton="False" VisibleIndex="9" Caption="Действия">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewCommandColumn>--%>
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
    
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporterOrders" runat="server" GridViewID="ASPxGridViewEnergyDogovorsAll"></dx:ASPxGridViewExporter>

    <asp:SqlDataSource ID="SqlDataSourceViewEnergyDogovors" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>


</asp:Content>
