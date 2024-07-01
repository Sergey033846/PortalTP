<%@ Page Title="Личный кабинет по ТП" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="portaltp.Cabinet.Chat" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<dx:BootstrapCallbackPanel runat="server" ClientInstanceName="callbackPanel1" OnCallback="CallbackPanel1_Callback">
<ContentCollection>
<dx:ContentControl>

<div class="row">

    <div class="col-md-3">
        <dx:ASPxMenu ID="ASPxMenuChat" runat="server" OnItemClick="ASPxMenuChat_ItemClick" ShowAsToolbar="True">
            <Items>
                <dx:MenuItem Text="Сохранить в EXCEL" Name="Export2Excel" Image-Url="../Content/Images/excel-icon.png">
                    <Image Url="../Content/Images/excel-icon.png"></Image>
                </dx:MenuItem>
            </Items>
        </dx:ASPxMenu>    
    </div>

    <div class="col-md-2">                
        <dx:BootstrapRadioButton runat="server" Text="все" GroupName="RadioGroup" ID="RadioButton_ChatAll">
            <ClientSideEvents CheckedChanged="function onUpdateClick(s, e) {
                callbackPanel1.PerformCallback();                        
            }"></ClientSideEvents>
        </dx:BootstrapRadioButton>
    </div>
    
    <div class="col-md-2">
        <dx:BootstrapRadioButton runat="server" Text="черновики" GroupName="RadioGroup" ID="RadioButton_ChatIsTemp">
            <ClientSideEvents CheckedChanged="function onUpdateClick(s, e) {
                callbackPanel1.PerformCallback();                        
            }"></ClientSideEvents>
        </dx:BootstrapRadioButton>
    </div>
</div>

    <dx:ASPxGridView ID="ASPxGridViewChat" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewChat"
    Width="100%" Caption="Переписка по заявкам (договорам)" DataSourceID="SqlDataSourceChatInfo" KeyFieldName="id_chatrec" EnableTheming="True" SettingsDataSecurity-AllowReadUnlistedFieldsFromClientApi="True" OnRowDeleting="ASPxGridViewChat_RowDeleting" OnHtmlRowPrepared="ASPxGridViewChat_HtmlRowPrepared">        
        <ClientSideEvents RowDblClick="function(s, e) {                
                s.GetRowValues(e.visibleIndex, 'id_zayavka', function (value) {                                                
                        window.open('OrderInfo?id='+value, '_blank');
                    })
            }">
        </ClientSideEvents>
        <SettingsDetail ShowDetailRow="True" />
        <SettingsContextMenu Enabled="True">
        </SettingsContextMenu>

    <SettingsAdaptivity>
    <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
    </SettingsAdaptivity>

        <Templates>
            <DetailRow>
                прикрепленные документы:
                <dx:ASPxGridView ID="ASPxGridViewChatDocs" runat="server" AutoGenerateColumns="False" Caption="" ClientInstanceName="CI_ASPxGridViewChatDocs" DataSourceID="SqlDataSourceChatDocsInfo" EnableTheming="True" KeyFieldName="id_zayavkadoc" OnBeforePerformDataSelect="ASPxGridViewChatDocs_BeforePerformDataSelect" Width="100%" SettingsResizing-ColumnResizeMode="Control">                    
                    <SettingsContextMenu Enabled="True">
                    </SettingsContextMenu>
                    <SettingsPager PageSize="10" />
                    <Settings ShowFilterRow="False" ShowFilterRowMenu="False" ShowFilterRowMenuLikeItem="False" ShowGroupPanel="False" ShowHeaderFilterButton="False" ShowColumnHeaders="True" />
                    <SettingsBehavior AllowFocusedRow="False" ConfirmDelete="True"/>
                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                    <SettingsSearchPanel Visible="False" />
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="документ" FieldName="doc_file_name" VisibleIndex="1" CellStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                        </dx:GridViewDataTextColumn>                        
                        
                        <dx:GridViewDataDateColumn Caption="дата добавления" FieldName="date_doc_add" VisibleIndex="3">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                            <PropertiesDateEdit DisplayFormatString=""></PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>                        
                        
                        <dx:GridViewDataCheckColumn Caption="действия" FieldName="" VisibleIndex="2">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                            <DataItemTemplate>                    
                                <%--<dx:ASPxHyperLink ID="ASPxHyperLinkDownloadOrderDoc" runat="server" Text="скачать" oninit="urlDownloadOrderDoc_Init">
                                </dx:ASPxHyperLink>--%>
                                <dx:ASPxHyperLink ID="ASPxHyperLinkViewOrderDocChat" runat="server" Text="" oninit="urlViewOrderDoc_Init" 
                                    CssClass="btn btn-default btn-sm glyphicon glyphicon-eye-open" ToolTip="посмотреть" Target="_blank" ForeColor="#337AB7">
                                    </dx:ASPxHyperLink>
                                <dx:ASPxHyperLink ID="ASPxHyperLinkDownloadOrderDocChat" runat="server" Text="" oninit="urlDownloadOrderDoc_Init" 
                                    CssClass="btn btn-default btn-sm glyphicon glyphicon-cloud-download" ToolTip="скачать" ForeColor="#337AB7">
                                    </dx:ASPxHyperLink>
                            </DataItemTemplate>
                        </dx:GridViewDataCheckColumn>
                    </Columns>
                    <Styles>
                        <Cell HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                        </Cell>
                    </Styles>
                    <Paddings Padding="0px" />
                    <Border BorderWidth="0px" />
                    <BorderBottom BorderWidth="1px" />
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"/>
        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
        <SettingsSearchPanel Visible="True" />

        <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
        <Columns>            
            <dx:GridViewDataCheckColumn Caption="Вложения" FieldName="count_docs" VisibleIndex="1">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                <DataItemTemplate>
                    <dx:ASPxHyperLink ID="ASPxHyperLinkViewOrderDocChat" runat="server" Text="" oninit="imgAttachment_Init2" 
                        CssClass="btn btn-default btn-sm glyphicon glyphicon-paperclip" ToolTip="вложения" ForeColor="#337AB7" 
                        ClientSideEvents-Click='<%# "function(s, e) { CI_ASPxGridViewChat.ExpandDetailRow(" + Container.VisibleIndex + "); }" %>'>
                        </dx:ASPxHyperLink>
                    <%--<dx:ASPxImage runat="server" ID="imgAttachment" Height="16px" oninit="imgAttachment_Init">
                    </dx:ASPxImage>--%>
                </DataItemTemplate>
            </dx:GridViewDataCheckColumn>

            <dx:GridViewDataDateColumn FieldName="chatrec_datetime" VisibleIndex="2" Caption="Дата сообщения">                                
                <PropertiesDateEdit DisplayFormatString="">
                </PropertiesDateEdit>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                                
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn FieldName="v1C_Zayavitel" VisibleIndex="3" Caption="Заявитель" CellStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                <DataItemTemplate>
                    <a target="_blank" href="OrderInfo?id=<%#Eval("id_zayavka")%>" style="color: #337AB7"><%#Eval("v1C_Zayavitel")%></a>                    
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataTextColumn FieldName="caption_userrole" VisibleIndex="4" Caption="Отправитель">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataTextColumn FieldName="caption_filial_short" VisibleIndex="5" Caption="Филиал ОКЭ">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>            
            
            <dx:GridViewDataTextColumn FieldName="zayavka_number_1c" VisibleIndex="7" Caption="Номер заявки">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataDateColumn Caption="Дата заявки" FieldName="zayavka_date_1c" VisibleIndex="8">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataDateColumn>
            
            <dx:GridViewDataTextColumn FieldName="id_zayavka" VisibleIndex="9" Caption="Идентификатор заявки" Visible="False">                
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataMemoColumn Caption="Сообщение" FieldName="caption_msg" VisibleIndex="10" CellStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataMemoColumn>

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

    <dx:ASPxGridViewExporter ID="ASPxGridViewExporterChat" runat="server" GridViewID="ASPxGridViewChat"></dx:ASPxGridViewExporter>

</dx:ContentControl>
</ContentCollection>
</dx:BootstrapCallbackPanel>

    <asp:SqlDataSource ID="SqlDataSourceChatInfo" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceChatDocsInfo" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>">        
        <SelectParameters>
            <asp:SessionParameter Name="id_chatrecS" SessionField="id_chatrecS" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
