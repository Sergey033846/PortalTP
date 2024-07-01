<%@ Page Title="Уведомления" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="ViewNotifies.aspx.cs" Inherits="portaltp.Cabinet.ViewNotifies" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
<dx:BootstrapCallbackPanel runat="server" ClientInstanceName="callbackPanel1" OnCallback="CallbackPanel1_Callback">
<ContentCollection>
<dx:ContentControl>
        
<div class="row">

    <div class="col-md-3">
        <dx:ASPxMenu ID="ASPxMenuViewNotifies" runat="server" OnItemClick="ASPxMenuViewNotifies_ItemClick" ShowAsToolbar="True">
            <Items>
                <dx:MenuItem Text="Сохранить в EXCEL" Name="Export2Excel" Image-Url="../Content/Images/excel-icon.png">
                    <Image Url="../Content/Images/excel-icon.png"></Image>
                </dx:MenuItem>

                <dx:MenuItem Text="Проверка" Name="menuItemCheckNotifications" Image-Url="../Content/Images/compare-icon.png">
                    <Image Url="../Content/Images/compare-icon.png"></Image>
                </dx:MenuItem>
            </Items>
        </dx:ASPxMenu>    
    </div>

    <div class="col-md-2">                
        <dx:BootstrapRadioButton runat="server" Text="все" GroupName="RadioGroup" ID="RadioButton_NotifiesAll">
            <ClientSideEvents CheckedChanged="function onUpdateClick(s, e) {
                callbackPanel1.PerformCallback();                        
            }"></ClientSideEvents>
        </dx:BootstrapRadioButton>
    </div>

    <div class="col-md-2">
        <dx:BootstrapRadioButton runat="server" Text="уведомления ГП (отбор не работает)" GroupName="RadioGroup" ID="RadioButton_NotifiesForGP">
            <ClientSideEvents CheckedChanged="function onUpdateClick(s, e) {
                callbackPanel1.PerformCallback();                        
            }"></ClientSideEvents>
        </dx:BootstrapRadioButton>
    </div>

    <div class="col-md-2">
        <dx:BootstrapRadioButton runat="server" Text="в очереди" GroupName="RadioGroup" ID="RadioButton_NotifiesNotSend">
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

<dx:ASPxGridView ID="ASPxGridViewNotifiesAll" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewNotifiesAll"
Width="100%" Caption="Уведомления" DataSourceID="SqlDataSourceViewNotifies" KeyFieldName="id_notify" EnableTheming="True" OnRowDeleting="ASPxGridViewNotifiesAll_RowDeleting" SettingsResizing-ColumnResizeMode="Control" OnHtmlRowPrepared="ASPxGridViewNotifiesAll_HtmlRowPrepared"
SettingsPager-Position="TopAndBottom">
    
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
        
        <dx:GridViewDataDateColumn FieldName="date_create_notify" VisibleIndex="1" Caption="Дата создания">                            
            <PropertiesDateEdit DisplayFormatString=""></PropertiesDateEdit>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataDateColumn>

        <dx:GridViewDataTextColumn FieldName="caption_notifytype" VisibleIndex="2" Caption="Тип">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="notify_text" VisibleIndex="3" Caption="Текст" Settings-AllowEllipsisInText="True" CellStyle-Wrap="False">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="user_nameingrid" VisibleIndex="4" Caption="Получатель" CellStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="v1C_Zayavitel" VisibleIndex="10" Caption="Заявитель" CellStyle-HorizontalAlign="Left">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            <DataItemTemplate>                
                <a target="_blank" href="OrderInfo?id=<%#Eval("id_zayavka")%>" style="color: #337AB7"><%#Eval("v1C_Zayavitel")%></a>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn> 

        <dx:GridViewDataTextColumn FieldName="status_GP_RegNewZ" VisibleIndex="15" Caption="Статус ЗаявкаТП">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="status_GP_RegNewDTP" VisibleIndex="16" Caption="Статус ДоговорТП">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="status_PrikreplenAktDopuskaPU" VisibleIndex="17" Caption="Статус АктДПУ">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>            

        <dx:GridViewDataTextColumn FieldName="status_PrikreplenAktTP" VisibleIndex="18" Caption="Статус АктТП">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>            

        <dx:GridViewDataTextColumn FieldName="status_ZAnnulirovana" VisibleIndex="19" Caption="Статус ЗаявкаАннул">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="nomer_dogovorenergo" VisibleIndex="20" Caption="Договор энергоснабж.">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="caption_notifychannel" VisibleIndex="25" Caption="Канал">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="caption_notifystatus" VisibleIndex="30" Caption="Статус">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>            

        <%--<dx:GridViewDataTextColumn FieldName="priority" VisibleIndex="9" Caption="Приоритет">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>--%>            

        <%--<dx:GridViewDataTextColumn FieldName="comment" VisibleIndex="10" Caption="4252352345">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>--%>

        <dx:GridViewDataTextColumn FieldName="comment" VisibleIndex="31" Caption="Коммент.">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />            
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataDateColumn Caption="Дата отправки" FieldName="date_send_notify" VisibleIndex="35">
            <PropertiesDateEdit DisplayFormatString=""></PropertiesDateEdit>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
        </dx:GridViewDataDateColumn>                                

        <dx:GridViewDataCheckColumn Caption="#" FieldName="" VisibleIndex="40">
          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
          <DataItemTemplate>                   
              
              <dx:BootstrapButton ID="ButtonNotifyRepeat" runat="server" AutoPostBack="True" Text="" 
                  CssClasses-Control="btn btn-default btn-sm glyphicon glyphicon-repeat mcbuttoningrid" ToolTip="повторить"
                  OnClick="ButtonNotifyRepeat_Click">
              </dx:BootstrapButton>

            </DataItemTemplate>
        </dx:GridViewDataCheckColumn>

        <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="45" Caption="Действия" ShowClearFilterButton="True">
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
    
<dx:ASPxGridViewExporter ID="ASPxGridViewExporterNotifies" runat="server" GridViewID="ASPxGridViewNotifiesAll"></dx:ASPxGridViewExporter>

</dx:ContentControl>
</ContentCollection>
</dx:BootstrapCallbackPanel>

<asp:SqlDataSource ID="SqlDataSourceViewNotifies" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>
        
</asp:Content>

