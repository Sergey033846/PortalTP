<%@ Page Title="Личный кабинет по ТП" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="ContractInfo.aspx.cs" Inherits="portaltp.Cabinet.ContractInfo" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../Scripts/EDS/ie_eventlistner_polyfill.js"></script>
    
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>

    <asp:Panel ID="ASPxPanelAddContractInfo" runat="server">
         
        <dx:ASPxTextBox ID="tbContractNumber" runat="server" Width="250px" Caption="Номер договора (присвоенный):" CaptionSettings-Position="Top">
            <CaptionSettings Position="Top"></CaptionSettings>
            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxTextBox>
        
        <br />
        
        <dx:ASPxDateEdit ID="tbContractDate" runat="server"  Width="250px" Caption="Дата договора (присвоенная):" CaptionSettings-Position="Top">
            <CaptionSettings Position="Top"></CaptionSettings>
            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>
        
        <br />
        
        <dx:ASPxComboBox ID="ASPxComboBoxContractStatus" runat="server" SelectedIndex="0" Width="250px" ValueType="System.Int32" AutoPostBack="False" ClientInstanceName="comboboxCS" DropDownStyle="DropDownList" DataSourceID="SqlDataSourceContractStatus" Caption="Статус договора:">
            <ClearButton DisplayMode="Never">
            </ClearButton>
            <CaptionSettings Position="Top" />
        </dx:ASPxComboBox>                                  

        <br />

        <dx:ASPxButton ID="ASPxButtonChangeContractInfo" runat="server" Text="Изменить" Theme="SoftOrange" ClientInstanceName="CI_ASPxButtonChangeContractInfo" ValidationGroup="RegisterUserValidationGroup" OnClick="ASPxButtonChangeContractInfo_Click" AutoPostBack="True">        
        </dx:ASPxButton>

    </asp:Panel>

    <br /><hr />

    <%-- грид информации о договоре --%>
    <dx:ASPxGridView ID="ASPxGridViewContractInfo" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridViewContractInfo"
    Width="100%" Caption="Информация о договоре" DataSourceID="SqlDataSourceContractInfo" KeyFieldName="id_contract" EnableTheming="True" Theme="MetropolisBlue" SettingsResizing-ColumnResizeMode="Control">     
        <SettingsContextMenu Enabled="True">
        </SettingsContextMenu>
        <SettingsPager PageSize="50" />
        <SettingsBehavior AllowFocusedRow="False"/>
        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
        <Columns>            
            <dx:GridViewDataTextColumn FieldName="contract_number_1c" VisibleIndex="1" Caption="Номер договора (присвоенный)">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="contract_date_1c" VisibleIndex="2" Caption="Дата договора (присвоенная)">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataDateColumn>            
            <dx:GridViewDataTextColumn FieldName="caption_contractstatus" VisibleIndex="3" Caption="Статус договора">
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
    </dx:ASPxGridView>
    
    <br />

    <%-- грид информации о документах договора --%>
    <dx:ASPxGridView ID="ASPxGridViewContractDocs" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewContractDocs"
    Width="100%" Caption="Документы договора" DataSourceID="SqlDataSourceContractDocs" KeyFieldName="id_zayavkadoc" EnableTheming="True" Theme="MetropolisBlue" SettingsResizing-ColumnResizeMode="Control">        
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
            <dx:GridViewDataCheckColumn Caption="Подписан исполнителем" FieldName="esign_oke" VisibleIndex="4">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                <DataItemTemplate>
                    <dx:ASPxImage runat="server" ID="imgESign" Height="16px" oninit="imgEsignOke_Init">
                    </dx:ASPxImage>
                </DataItemTemplate>
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn Caption="Подписан заказчиком" FieldName="esign_kontr" VisibleIndex="5">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                <DataItemTemplate>
                    <dx:ASPxImage runat="server" ID="ASPxImage1" Height="16px" oninit="imgEsignKontr_Init">
                    </dx:ASPxImage>
                </DataItemTemplate>
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn Caption="Действия" FieldName="" VisibleIndex="6">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                <DataItemTemplate>                    
                    <dx:ASPxHyperLink ID="ASPxHyperLinkDownloadContractDoc" runat="server" Text="скачать" oninit="urlDownloadContractDoc_Init">
                    </dx:ASPxHyperLink>
                    <dx:ASPxHyperLink ID="ASPxHyperLinkSignContractDoc" runat="server" Text="подписать" oninit="urlSignContractDoc_Init">
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
    
    <br /><hr /><br />    
            
    <%-- тестирование прикрепления файла с указанным видом документа --%>  
    <asp:Panel ID="PanelUploadDocControls" runat="server">
        <dx:ASPxLabel ID="ASPxLabelUploadControlDocTitle" runat="server" AssociatedControlID="" Text="добавить документы (размер одного файла не более 10 Мб):" Theme="SoftOrange" />
        <br /><br />
        <dx:ASPxCallbackPanel ID="ASPxCallbackPanelDocType" runat="server" Width="100%" HideContentOnCallback="True" ClientInstanceName="CI_ASPxCallbackPanelDocType" OnCallback="ASPxComboBoxDocType_SelectedIndexChanged">                
        <PanelCollection>
        <dx:PanelContent runat="server">
            
            <dx:ASPxHiddenField ID="ASPxHiddenFieldDocType" runat="server" ClientInstanceName="ASPxHiddenFieldDocType"></dx:ASPxHiddenField>
            <dx:ASPxComboBox ID="ASPxComboBoxDocType" runat="server" SelectedIndex="0" Width="250px" ValueType="System.Int32" AutoPostBack="False" ClientInstanceName="comboboxDT" DropDownStyle="DropDownList" DataSourceID="SqlDataSourceDocType" Caption="Выберите вид документа:">            
                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                    CI_ASPxCallbackPanelDocType.PerformCallback();	
                }" />
            <ClearButton DisplayMode="Never">
            </ClearButton>
            <CaptionSettings Position="Top" />
            </dx:ASPxComboBox>
             
        </dx:PanelContent>
        </PanelCollection>    
        </dx:ASPxCallbackPanel>
    
        <br />

        <dx:ASPxUploadControl ID="ASPxUploadControlDoc" runat="server" UploadMode="Auto" Width="350px" UploadButton-Text="Прикрепить документ" UploadButton-ImagePosition="Left" ShowUploadButton="True" ShowProgressPanel="True" FileInputCount="1" ValidationSettings-MaxFileSize="10240000" ValidationSettings-MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт" ClientInstanceName="CI_UploadControlDoc" Theme="SoftOrange"
            OnFileUploadComplete="ASPxUploadControlDoc_FileUploadComplete">
            <ValidationSettings MaxFileSize="10240000" MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт"></ValidationSettings>
            <ClientSideEvents FilesUploadComplete="function(s, e) {	            
                CI_ASPxGridViewContractDocs.Refresh();
            }" />
            <UploadButton Text="Прикрепить документ"></UploadButton>            
        </dx:ASPxUploadControl>
    </asp:Panel>

    <br />                    

    <asp:SqlDataSource ID="SqlDataSourceContractInfo" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceContractDocs" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceContractStatus" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlDataSourceDocType" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>

</asp:Content>
