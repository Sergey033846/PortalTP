<%@ Page Title="Отладка" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="ViewOrdersDebugging.aspx.cs" Inherits="portaltp.Cabinet.ViewOrdersDebugging" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
<dx:BootstrapCallbackPanel runat="server" ClientInstanceName="callbackPanel1" OnCallback="CallbackPanel1_Callback">
<ContentCollection>
<dx:ContentControl>
    
<div class="row">

    <div class="col-md-3">
        <dx:ASPxMenu ID="ASPxMenuViewOrders" runat="server" OnItemClick="ASPxMenuViewOrders_ItemClick" ShowAsToolbar="True">
            <Items>
                <dx:MenuItem Text="Сохранить в EXCEL" Name="Export2Excel" Image-Url="../Content/Images/excel-icon.png">
                    <Image Url="../Content/Images/excel-icon.png"></Image>
                </dx:MenuItem>
            </Items>
        </dx:ASPxMenu>    
    </div>

    <div class="col-md-2">                
        <dx:BootstrapRadioButton runat="server" Text="все" GroupName="RadioGroup" ID="RadioButton_ZayavkaAll">
            <ClientSideEvents CheckedChanged="function onUpdateClick(s, e) {
                callbackPanel1.PerformCallback();                        
            }"></ClientSideEvents>
        </dx:BootstrapRadioButton>
    </div>

    <div class="col-md-2">
        <dx:BootstrapRadioButton runat="server" Text="незакрепленные" GroupName="RadioGroup" ID="RadioButton_ZayavkaNotFilialSet">
            <ClientSideEvents CheckedChanged="function onUpdateClick(s, e) {
                callbackPanel1.PerformCallback();                        
            }"></ClientSideEvents>
        </dx:BootstrapRadioButton>
    </div>

    <div class="col-md-2">
        <dx:BootstrapRadioButton runat="server" Text="незарегистрированные" GroupName="RadioGroup" ID="RadioButton_ZayavkaNotRegister">
            <ClientSideEvents CheckedChanged="function onUpdateClick(s, e) {
                callbackPanel1.PerformCallback();                        
            }"></ClientSideEvents>
        </dx:BootstrapRadioButton>
    </div>            
</div>
        
<dx:ASPxGridView ID="ASPxGridViewOrdersAll" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrdersAll"
Width="100%" Caption="Все заявки" DataSourceID="SqlDataSourceViewOrders" KeyFieldName="id_zayavka" EnableTheming="True" OnRowDeleting="ASPxGridViewOrdersAll_RowDeleting" SettingsResizing-ColumnResizeMode="Control" OnHtmlRowPrepared="ASPxGridViewOrdersAll_HtmlRowPrepared" SettingsPager-Position="TopAndBottom">
    <ClientSideEvents RowDblClick="function(s, e) {                
            window.open('OrderInfo?id='+s.GetRowKey(e.visibleIndex), '_blank');
        }"></ClientSideEvents>

    <SettingsContextMenu Enabled="True">
    </SettingsContextMenu>

    <SettingsAdaptivity>
        <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
    </SettingsAdaptivity>

    <SettingsPager PageSize="20" />
    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />

    <SettingsResizing ColumnResizeMode="Control"></SettingsResizing>

    <SettingsDataSecurity AllowEdit="False" AllowInsert="False" />
    <SettingsSearchPanel Visible="True" />

    <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
    <Columns>            
        <dx:GridViewDataTextColumn FieldName="caption_zayavkatype_short" VisibleIndex="2" Caption="Вид заявки">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="zayavka_number_1c" VisibleIndex="5" Caption="Номер заявки">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="caption_zayavkastatus" VisibleIndex="7" Caption="Статус заявки">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="caption_filial_short" VisibleIndex="4" Caption="Филиал">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataDateColumn FieldName="date_create_zayavka" VisibleIndex="1" Caption="Дата поступления">                
            <PropertiesDateEdit DisplayFormatString="">
            </PropertiesDateEdit>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataDateColumn>            

        <dx:GridViewDataTextColumn FieldName="v1C_Zayavitel" VisibleIndex="3" Caption="Заявитель">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            <DataItemTemplate>
                <a target="_blank" href="OrderInfo?id=<%#Eval("id_zayavka")%>" style="color: #337AB7"><%#Eval("v1C_Zayavitel")%></a>                    
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>            

        <dx:GridViewDataDateColumn Caption="Дата заявки" FieldName="zayavka_date_1c" VisibleIndex="6">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
        </dx:GridViewDataDateColumn>

        <%--<dx:GridViewDataTextColumn FieldName="v1C_adresEPU" VisibleIndex="8" Caption="Адрес ЭПУ">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="v1C_maxMoschnostEPU" VisibleIndex="9" Caption="Pmax">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="Z_uroven_U" VisibleIndex="10" Caption="U">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>--%>

        <dx:GridViewDataTextColumn FieldName="Z_vid_Oplaty" VisibleIndex="11" Caption="Вид оплаты">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="status_GP_RegNewZ" VisibleIndex="20" Caption="Статус ГП ЗаявкаТП">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="status_GP_RegNewDTP" VisibleIndex="21" Caption="Статус ГП ДоговорТП">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="status_PrikreplenAktDopuskaPU" VisibleIndex="24" Caption="Статус АктДПУ">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="status_PrikreplenAktTP" VisibleIndex="25" Caption="Статус АктТП">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>        

        <dx:GridViewDataTextColumn FieldName="status_ZAnnulirovana" VisibleIndex="26" Caption="Статус Заявка Аннул.">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>        

        <dx:GridViewDataTextColumn FieldName="id_zayavka" VisibleIndex="30" Caption="Идентификатор заявки">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>                                

        <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="31" Caption="Действия" ShowClearFilterButton="True">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewCommandColumn>
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
    
<dx:ASPxGridViewExporter ID="ASPxGridViewExporterOrders" runat="server" GridViewID="ASPxGridViewOrdersAll"></dx:ASPxGridViewExporter>

</dx:ContentControl>
</ContentCollection>
</dx:BootstrapCallbackPanel>

<asp:SqlDataSource ID="SqlDataSourceViewOrders" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>
        
</asp:Content>
