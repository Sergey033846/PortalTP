<%@ Page Title="Личный кабинет по ТП" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="ContractDocInfo.aspx.cs" Inherits="portaltp.Cabinet.ContractDocInfo" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../Scripts/EDS/ie_eventlistner_polyfill.js"></script>
    <script type="text/javascript" src="../Scripts/EDS/cadesplugin_api.js"></script>
    <script type="text/javascript" src="../Scripts/EDS/Code.js"></script>
    <script type="text/javascript" src="../Scripts/EDS/cadesplugin_api2.js"></script>

    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>

    <dx:ASPxHiddenField ID="ASPxHiddenFieldESign2" runat="server" ClientInstanceName="ASPxHiddenFieldESign2"></dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="ASPxHiddenFieldESign3" runat="server" ClientInstanceName="ASPxHiddenFieldESign3"></dx:ASPxHiddenField>

    <br />
        
    <%-- грид информации о документе договора --%>    
    <dx:ASPxGridView ID="ASPxGridViewContractDocs" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewContractDocs"
    Width="100%" Caption="Документы договора" DataSourceID="SqlDataSourceContractDoc" KeyFieldName="id_zayavkadoc" EnableTheming="True" Theme="MetropolisBlue" SettingsResizing-ColumnResizeMode="Control">        
        <SettingsContextMenu Enabled="True">
        </SettingsContextMenu>
        <SettingsPager PageSize="10" />
        <SettingsBehavior AllowFocusedRow="False"/>
        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
        <Columns>            
            <dx:GridViewDataTextColumn FieldName="caption_doctype" VisibleIndex="1" Caption="Вид документа">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="date_doc_add" VisibleIndex="2" Caption="Дата добавления">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataDateColumn>            
            <dx:GridViewDataTextColumn Caption="Имя файла" FieldName="doc_file_name" VisibleIndex="0">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataCheckColumn Caption="Действия" FieldName="" VisibleIndex="3">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                <DataItemTemplate>                    
                    <dx:ASPxHyperLink ID="ASPxHyperLinkDownloadContractDoc" runat="server" Text="скачать" oninit="urlDownloadContractDoc_Init">
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
        
    <br /><hr />
    <table width="100%">
        <tr>
            <td width="45%">                
                <dx:ASPxMemo ID="ASPxMemoESignOke" runat="server" Height="71px" Width="100%" ReadOnly="True" Text="документ не подписан" Caption="Расшифровка подписи исполнителя:" CaptionSettings-Position="Top"></dx:ASPxMemo>
            </td>
            <td width="10%">

            </td>
            <td width="45%">                
                <dx:ASPxMemo ID="ASPxMemoESignKontr" runat="server" Height="71px" Width="100%" ReadOnly="True" Text="документ не подписан" Caption="Расшифровка подписи заказчика:" CaptionSettings-Position="Top"></dx:ASPxMemo>
            </td>
        </tr>
    </table>

    <%-- грид информации о подписях документа договора --%>    
    <dx:ASPxGridView ID="ASPxGridViewContractDocESign" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewContractDocsESign"
    Width="100%" Caption="Файлы подписей документа" DataSourceID="SqlDataSourceContractDocESign" KeyFieldName="esign_docid" EnableTheming="True" Theme="MetropolisBlue" SettingsResizing-ColumnResizeMode="Control">        
        <SettingsContextMenu Enabled="True">
        </SettingsContextMenu>
        <SettingsPager PageSize="10" />
        <SettingsBehavior AllowFocusedRow="False"/>

<SettingsAdaptivity>
<AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
</SettingsAdaptivity>

<SettingsResizing ColumnResizeMode="Control"></SettingsResizing>

        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
        <Columns>            
            <dx:GridViewDataTextColumn FieldName="user_nameingrid" VisibleIndex="1" Caption="Пользователь">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="esign_date" VisibleIndex="2" Caption="Дата добавления">
                <PropertiesDateEdit DisplayFormatString="">
                </PropertiesDateEdit>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataDateColumn>            
            <dx:GridViewDataTextColumn Caption="Имя файла" FieldName="doc_file_name" VisibleIndex="0">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataCheckColumn Caption="Действия" FieldName="" VisibleIndex="3">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                <DataItemTemplate>                    
                    <dx:ASPxHyperLink ID="ASPxHyperLinkDownloadESignDoc" runat="server" Text="скачать" oninit="urlDownloadESignDoc_Init">
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
            
    <br /><hr />
        
    <dx:ASPxCallbackPanel ID="ASPxCallbackPanelESign" runat="server" Width="100%" HideContentOnCallback="True" ClientInstanceName="CI_ASPxCallbackPanelESign" OnCallback="ASPxButton3_Click">
                <ClientSideEvents EndCallback="function(s, e) {
CI_ASPxGridViewContractDocsESign.Refresh();	
                    window.location.reload();
}" />
                <PanelCollection>
                <dx:PanelContent runat="server">  
    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Сформировать хэш" OnClick="ASPxButton1_Click" Visible="False"></dx:ASPxButton>

    <dx:ASPxMemo ID="ASPxMemo1" runat="server" Height="71px" Width="170px" Visible="False" ClientInstanceName="signatureserver" Caption="Подпись:"></dx:ASPxMemo>
    <%--<dx:ASPxMemo ID="ASPxMemo2" runat="server" Height="71px" Width="170px" Visible="True" ClientInstanceName="signatureserver2" Caption="Подпись:"></dx:ASPxMemo>
    <dx:ASPxMemo ID="ASPxMemo3" runat="server" Height="71px" Width="170px" Visible="True" ClientInstanceName="signatureserver3" Caption="Подпись:"></dx:ASPxMemo>--%>

    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Подписать документ" AutoPostBack="False" Theme="SoftOrange">
        <ClientSideEvents Click="function(s, e) {
	        run();                          
            CI_ASPxCallbackPanelESign.PerformCallback();                        
}" />
    </dx:ASPxButton>
    
    <%--<textarea id="signatureclient" cols="50" rows="10" title="Подпись"></textarea>                --%>
    <%--                <textarea id="hash1" cols="50" rows="10" title="Подпись"></textarea>
                    <textarea id="hash2" cols="50" rows="10" title="Подпись"></textarea>
                    <textarea id="hash3" cols="50" rows="10" title="Подпись"></textarea>
                    <textarea id="hash0" cols="50" rows="10" title="Подпись"></textarea>--%>
                    
    <dx:ASPxLabel ID="lblESignStatus" runat="server" Text="" Visible="True" />

                </dx:PanelContent>
                </PanelCollection>
                </dx:ASPxCallbackPanel>

    <br />

    <dx:ASPxMemo ID="ASPxMemoHintESign" runat="server" Width="100%" Text="" CaptionSettings-Position="Top" BackColor="#FFFFCC" Border-BorderColor="Silver" Height="50px"></dx:ASPxMemo>

    <br /><hr /><br />    
            
    <%-- функционал прикрепления файла ЭЦП заказчика администратором --%>  
    <asp:Panel ID="PanelUploadESignFile" runat="server">
        <dx:ASPxLabel ID="ASPxLabelUploadESignFile" runat="server" AssociatedControlID="" Text="прикрепить файл ЭЦП заказчика:" Theme="SoftOrange" />

        <br />

        <dx:ASPxUploadControl ID="ASPxUploadControlESignFile" runat="server" UploadMode="Auto" Width="350px" UploadButton-Text="Прикрепить документ" UploadButton-ImagePosition="Left" ShowUploadButton="True" ShowProgressPanel="True" FileInputCount="1" ValidationSettings-MaxFileSize="10240000" ValidationSettings-MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт" ClientInstanceName="CI_UploadControlDoc" Theme="SoftOrange"
            OnFileUploadComplete="ASPxUploadControlESignFile_FileUploadComplete">
            <ValidationSettings MaxFileSize="10240000" MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт"></ValidationSettings>
            <ClientSideEvents FilesUploadComplete="function(s, e) {	            
                CI_ASPxGridViewContractDocsESign.Refresh();
            }" />
            <UploadButton Text="Прикрепить файл ЭЦП заказчика"></UploadButton>            
        </dx:ASPxUploadControl>

        <br />
    </asp:Panel>

    <%-- %><dx:ASPxButton ID="ASPxButton3" runat="server" Text="Прикрепить подпись" Visible="True" AutoPostBack="True" OnClick="ASPxButton3_Click">        
    </dx:ASPxButton>--%>

    <%--<dx:ASPxButton ID="ASPxButton3" runat="server" Text="Прикрепить подпись" Visible="True" AutoPostBack="False">
        <ClientSideEvents Click=
                        "function(s, e) {
	                        CI_ASPxCallbackPanelESign.PerformCallback();
                         }" />
    </dx:ASPxButton>--%>

    <asp:SqlDataSource ID="SqlDataSourceContractDoc" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceContractDocESign" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>

</asp:Content>
