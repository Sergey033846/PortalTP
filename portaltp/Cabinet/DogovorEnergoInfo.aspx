<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="DogovorEnergoInfo.aspx.cs" Inherits="portaltp.Cabinet.DogovorEnergoInfo" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class ="container">
        
        <a runat="server" class="btn btn-primary btn-sm" id="ASPxHyperLinkOpenOrder" target="_blank"><span class="glyphicon glyphicon-folder-open" aria-hidden="true"></span>&nbsp;&nbsp;просмотр заявки</a>
        
        <br /><br />

        <div class="row">

            <div class="col-md-3">
                <dx:ASPxTextBox ID="ASPxTextBoxDogovorEnergo_NomerLS" runat="server" Width="100%" Caption="Номер лицевого счета:" ReadOnly="False" 
                    CaptionSettings-Position="Top" HorizontalAlign="Center"></dx:ASPxTextBox>
            </div>

            <div class="col-md-3">
                <dx:ASPxTextBox ID="ASPxTextBoxDogovorEnergo_NomerDogovorEnergo" runat="server" Width="100%" Caption="Номер договора энергоснабжения:" ReadOnly="False" 
                    CaptionSettings-Position="Top" HorizontalAlign="Center"></dx:ASPxTextBox>
            </div>

            <div class="col-md-3">
                <dx:ASPxTextBox ID="ASPxTextBoxDogovorEnergo_NomerElektroustanovka" runat="server" Width="100%" Caption="Номер электроустановки:" ReadOnly="False" 
                    CaptionSettings-Position="Top" HorizontalAlign="Center"></dx:ASPxTextBox>
            </div>
        </div>

        <br />

        <div class="row">
            <div class="col-md-3">
                <dx:ASPxDateEdit ID="ASPxDateEdit_DogovorEnergoDate" runat="server"  Width="100" Caption="Дата создания:" CaptionSettings-Position="Top">
                    <CaptionSettings Position="Top"></CaptionSettings>
                    <%--<ValidationSettings ValidationGroup="SetOrderNumberValidationGroup">
                        <RequiredField IsRequired="false" />
                    </ValidationSettings>--%>
                </dx:ASPxDateEdit>
            </div>
        </div>

        <br />

        <dx:ASPxButton ID="ASPxButtonSaveDogovorEnergoInfo" runat="server" Text="Сохранить" AutoPostBack="True" CssClass="btn btn-primary btn-xs" Visible="False" OnClick="ASPxButtonSaveDogovorEnergoInfo_Click">
        </dx:ASPxButton>

        <br /><br />

        <div class="row">

            <%-- грид информации о документах договора энергоснабжения --%>
             <dx:ASPxGridView ID="ASPxGridViewDogovorEnergoDocs" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewDogovorEnergoDocs"
             Width="100%" Caption="Документы договора энергоснабжения" DataSourceID="SqlDataSourceDogovorEnergoDocs" KeyFieldName="id_zayavkadoc" EnableTheming="True" SettingsResizing-ColumnResizeMode="Disabled">        
                 <SettingsContextMenu Enabled="True">
                 </SettingsContextMenu>
                 <SettingsPager PageSize="10" />
                 <SettingsBehavior AllowFocusedRow="False"/>
                 <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                 <Columns>            
                     
                     <dx:GridViewDataTextColumn Caption="Документ" FieldName="doc_file_name" VisibleIndex="1" CellStyle-HorizontalAlign="Left">
                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                     </dx:GridViewDataTextColumn>
                     
                     <dx:GridViewDataTextColumn FieldName="caption_doctype" VisibleIndex="3" Caption="Вид документа">
                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                     </dx:GridViewDataTextColumn>

                     <dx:GridViewDataDateColumn FieldName="date_doc_add" VisibleIndex="4" Caption="Дата добавления">
                         <PropertiesDateEdit DisplayFormatString=""></PropertiesDateEdit>
                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                     </dx:GridViewDataDateColumn>

                     <dx:GridViewDataTextColumn FieldName="id_zayavkadoc" VisibleIndex="20" Caption="Идентификатор">                
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                    </dx:GridViewDataTextColumn>

                     <dx:GridViewDataCheckColumn Caption="Действия" FieldName="" VisibleIndex="2">
                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                         <DataItemTemplate>
                             <dx:ASPxHyperLink ID="ASPxHyperLinkViewDogovorDoc" runat="server" Text="" oninit="urlViewOrderDoc_Init" 
                                 CssClass="btn btn-default glyphicon glyphicon-eye-open btn-sm" ToolTip="посмотреть" Target="_blank" ForeColor="#337AB7">
                             </dx:ASPxHyperLink>
                             <dx:ASPxHyperLink ID="ASPxHyperLinkDownloadDogovorDoc" runat="server" Text="" oninit="urlDownloadOrderDoc_Init" 
                                 CssClass="btn btn-default glyphicon glyphicon-cloud-download btn-sm" ToolTip="скачать" ForeColor="#337AB7">
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

        </div>

        <br />

        <div class="row">
            <div class="col-xs-10 col-sm-6 col-md-5 col-lg-4">            
            <dx:ASPxLabel ID="ASPxLabel1" runat="server" AssociatedControlID="" Text="Загрузить платежные реквизиты:" Theme="SoftOrange" Font-Bold="True" />
            <dx:ASPxUploadControl ID="ASPxUploadControlPlatRekv" runat="server" UploadMode="Auto" Width="100%" UploadButton-Text="Прикрепить документы" UploadButton-ImagePosition="Left" ShowUploadButton="True" ShowProgressPanel="True" FileInputCount="1" ValidationSettings-MaxFileSize="10240000" ValidationSettings-MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт" ClientInstanceName="CI_ASPxUploadControlPlatRekv" Theme="SoftOrange"
                OnFileUploadComplete="ASPxUploadControlPlatRekv_FileUploadComplete" AutoStartUpload="True">
                <ValidationSettings MaxFileSize="10485760" MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет 10 Мбайт"></ValidationSettings>
                <ClientSideEvents FilesUploadComplete="function(s, e) {	            
                    CI_ASPxGridViewDogovorEnergoDocs.Refresh();
                }" />
                <UploadButton Text="Прикрепить документы"></UploadButton>            
            </dx:ASPxUploadControl>
            </div>
        </div>

        <br />

        <div class="row">
            <div class="col-xs-10 col-sm-6 col-md-5 col-lg-4">            
            <dx:ASPxLabel ID="ASPxLabel2" runat="server" AssociatedControlID="" Text="Загрузить договор энергоснабжения:" Theme="SoftOrange" Font-Bold="True" />
            <dx:ASPxUploadControl ID="ASPxUploadControlDogovorEnergo" runat="server" UploadMode="Auto" Width="100%" UploadButton-Text="Прикрепить документы" UploadButton-ImagePosition="Left" ShowUploadButton="True" ShowProgressPanel="True" FileInputCount="1" ValidationSettings-MaxFileSize="10240000" ValidationSettings-MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт" ClientInstanceName="CI_ASPxUploadControlDogovorEnergo" Theme="SoftOrange"
                OnFileUploadComplete="ASPxUploadControlDogovorEnergo_FileUploadComplete" AutoStartUpload="True">
                <ValidationSettings MaxFileSize="10485760" MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет 10 Мбайт"></ValidationSettings>
                <ClientSideEvents FilesUploadComplete="function(s, e) {	            
                    CI_ASPxGridViewDogovorEnergoDocs.Refresh();
                }" />
                <UploadButton Text="Прикрепить документы"></UploadButton>            
            </dx:ASPxUploadControl>
            </div>
        </div>
        
    </div>
        
    <asp:SqlDataSource ID="SqlDataSourceDogovorEnergoDocs" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource> 
</asp:Content>
