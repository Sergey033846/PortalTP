<%@ Page Title="Все заявки" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="ViewOrders.aspx.cs" Inherits="portaltp.Cabinet.ViewOrders" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
<dx:BootstrapCallbackPanel runat="server" ClientInstanceName="callbackPanel1" OnCallback="CallbackPanel1_Callback">
<ContentCollection>
<dx:ContentControl>
    
<dx:ASPxTextBox ID="ASPxTextBoxUserCount" runat="server" Width="270px" Caption="Количество сеансов:" ReadOnly="True" CaptionSettings-Position="Top" HorizontalAlign="Center" ClientInstanceName="CI_ASPxTextBoxUserCount"></dx:ASPxTextBox>

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
        <dx:BootstrapRadioButton runat="server" Text="новые сообщения" GroupName="RadioGroup" ID="RadioButton_ZayavkaNewMessages">
            <ClientSideEvents CheckedChanged="function onUpdateClick(s, e) {
                callbackPanel1.PerformCallback();                        
            }"></ClientSideEvents>
        </dx:BootstrapRadioButton>
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
    
    <div class="col-md-2">
        <dx:BootstrapRadioButton runat="server" Text="черновики" GroupName="RadioGroup" ID="RadioButton_IsTemp">
            <ClientSideEvents CheckedChanged="function onUpdateClick(s, e) {
                callbackPanel1.PerformCallback();                        
            }"></ClientSideEvents>
        </dx:BootstrapRadioButton>
    </div>
</div>
        
<dx:ASPxGridView ID="ASPxGridViewOrdersAll" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrdersAll"
Width="100%" Caption="Все заявки" DataSourceID="SqlDataSourceViewOrders" KeyFieldName="id_zayavka" EnableTheming="True" OnRowDeleting="ASPxGridViewOrdersAll_RowDeleting" OnHtmlRowPrepared="ASPxGridViewOrdersAll_HtmlRowPrepared"
SettingsPager-Position="TopAndBottom">
    <ClientSideEvents RowDblClick="function(s, e) {                
            window.open('OrderInfo?id='+s.GetRowKey(e.visibleIndex), '_blank');
        }"></ClientSideEvents>
    
    <SettingsContextMenu Enabled="True" RowMenuItemVisibility-DeleteRow="False">
        <RowMenuItemVisibility DeleteRow="False"></RowMenuItemVisibility>
    </SettingsContextMenu>

    <SettingsAdaptivity>
        <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
    </SettingsAdaptivity>

    <SettingsPager PageSize="20" />
    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />

    <SettingsResizing ColumnResizeMode="Disabled"></SettingsResizing>

    <SettingsDataSecurity AllowEdit="False" AllowInsert="False" />
    <SettingsSearchPanel Visible="True" />

    <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
    <Columns>        
        <dx:GridViewDataDateColumn FieldName="date_create_zayavka" VisibleIndex="1" Caption="Дата подачи">                
            <PropertiesDateEdit DisplayFormatString="">
            </PropertiesDateEdit>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataDateColumn>

        <dx:GridViewDataCheckColumn Caption="Откуда" FieldName="" VisibleIndex="2">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            <DataItemTemplate>
                <dx:ASPxImage runat="server" ID="ASPxImageOrderSource" oninit="ASPxImageOrderSource_Init">
                </dx:ASPxImage>
            </DataItemTemplate>
        </dx:GridViewDataCheckColumn>

        <dx:GridViewDataDateColumn FieldName="date_copy_from1c" VisibleIndex="3" Caption="Дата переноса">                
            <PropertiesDateEdit DisplayFormatString="">
            </PropertiesDateEdit>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataDateColumn>

        <dx:GridViewDataCheckColumn FieldName="msgs_not_read_from_user_flag" VisibleIndex="4" Caption="Новое сообщ.">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            <DataItemTemplate>    
                <dx:ASPxLabel ID="ASPxLabelNewMessages" runat="server" Text="" OnInit="ASPxLabelNewMessages_Init" 
                    CssClass="glyphicon glyphicon-flag mcbuttoningrid-red" ToolTip="поступили новые сообщения">
                </dx:ASPxLabel>                                
            </DataItemTemplate>
        </dx:GridViewDataCheckColumn>

        <dx:GridViewDataTextColumn FieldName="caption_zayavkatype_short" VisibleIndex="5" Caption="Вид заявки">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="v1C_Zayavitel" VisibleIndex="6" Caption="Заявитель" CellStyle-HorizontalAlign="Left">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            <DataItemTemplate>                
                <a target="_blank" href="OrderInfo?id=<%#Eval("id_zayavka")%>" style="color: #337AB7"><%#Eval("v1C_Zayavitel")%></a>
            </DataItemTemplate>
            <CellStyle HorizontalAlign="Left"></CellStyle>
        </dx:GridViewDataTextColumn>            

        <dx:GridViewDataTextColumn FieldName="caption_filial_short" VisibleIndex="7" Caption="Филиал">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="zayavka_number_1c" VisibleIndex="8" Caption="Номер заявки">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataDateColumn Caption="Дата заявки" FieldName="zayavka_date_1c" VisibleIndex="9">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
        </dx:GridViewDataDateColumn>

        <dx:GridViewDataTextColumn FieldName="caption_zayavkastatus" VisibleIndex="10" Caption="Статус заявки">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="Z_status_DogovoraTP" VisibleIndex="11" Caption="Статус договора">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="Z_status_Oplaty" VisibleIndex="12" Caption="Статус оплаты">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />    
            <DataItemTemplate>
                <dx:ASPxLabel ID="ASPxLabelStatusOplaty" runat="server" Text="" OnInit="ASPxLabelStatusOplaty_Init"></dx:ASPxLabel>                
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        
        <dx:GridViewDataTextColumn FieldName="v1C_adresEPU" VisibleIndex="13" Caption="Адрес ЭПУ" CellStyle-HorizontalAlign="Left">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            <CellStyle HorizontalAlign="Left"></CellStyle>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="v1C_maxMoschnostEPU" VisibleIndex="14" Caption="Pmax">                
            <PropertiesTextEdit DisplayFormatString="F1">
            </PropertiesTextEdit>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="Z_uroven_U" VisibleIndex="15" Caption="U">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="Z_vid_Oplaty" VisibleIndex="16" Caption="Вариант оплаты">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="nomer_dogovorenergo" VisibleIndex="17" Caption="Договор энергоснабж.">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="id_zayavka" VisibleIndex="20" Caption="Идентификатор заявки">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>                                

        <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="21" Caption="Действия" ShowClearFilterButton="True">
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
