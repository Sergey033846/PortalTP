<%@ Page Title="Подать заявку на ТП" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="SendOrder.aspx.cs" Inherits="portaltp.Cabinet.SendOrder" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" src="../Scripts/DaData/jquery.suggestions.min.js"></script>
<%--<link href="https://cdn.jsdelivr.net/npm/suggestions-jquery@20.3.0/dist/css/suggestions.min.css" rel="stylesheet" />--%>
<link href="../Content/suggestions.min.css" rel="stylesheet" />

<dx:ASPxHiddenField ID="ASPxHiddenFieldZayavkaID" runat="server"></dx:ASPxHiddenField>       

<h2>Подать заявку на технологическое присоединение</h2>

<%--<p>Подать заявку на технологическое присоединение к электрическим сетям ОГУЭП «Облкоммунэнерго» через Личный кабинет Вы можете для энергопринимающих устройств с максимальной мощностью до 150 кВт и уровнем напряжения до 0.4 кВ.</p>--%>

<%--<p class="alert alert-danger">В связи с участившимися случаями распространения новой коронавирусной инфекции, вызванной COVID-19, прием заявок, в том числе, через личный кабинет и выполнение мероприятий по осуществлению технологического присоединения в филиалах ОГУЭП «Облкоммунэнерго» «Иркутские электрические сети», «Саянские электрические сети», «Усть-Кутские электрические сети», «Усть-Ордынские электрические сети», «Черемховские электрические сети» приостановлены до 23.11.2020г. Надеемся на Ваше понимание.</p>--%>

<p class="alert alert-info">1. Договор между сетевой организацией и заявителем заключается с использованием личного кабинета заявителя по поданной заявке:
<br />- юридическим лицом или индивидуальным предпринимателем в целях технологического присоединения по второй или третьей категории надежности энергопринимающих устройств, максимальная мощность которых составляет до 150 кВт включительно (с учетом ранее присоединенных в данной точке присоединения энергопринимающих устройств);
<br />- юридическим лицом или индивидуальным предпринимателем в целях технологического присоединения объектов микрогенерации к объектам электросетевого хозяйства с уровнем напряжения до 1000 В;
<br />- юридическим лицом или индивидуальным предпринимателем в целях одновременного технологического присоединения к объектам электросетевоrо хозяйства с уровнем напряжения до 1000 В энергоnринимающих устройств, максимальная мощность которых составляет до 150 кВт включительно (с учетом ранее присоединенных в данной точке присоединения энергопринимающих устройств), электроснабжение которых предусматривается по одному источнику, и объектов микрогенерации;
<br />- физическим лицом в целях технологического присоединения объекта микрогенерации к объектам электросетевого хозяйства с уровнем напряжения до 1000 В;
<br />- физическим лицом в целях одновременного технологического присоединения к объектам электросетевого хозяйства с уровнем напряжения до 1000 В энергопринимающих устройств, максимальная мощность которых составляет до 150 кВт включительно (с учетом ранее присоединенных в данной точке присоединения энергопринимающих устройств), которые используются для бытовых и иных нужд. не связанных с осуществлением предпринимательской деятельности, и электроснабжение которых предусматривается по одному источнику, и объектов микрогенерации;
<br />- физическим лицом в целях технологического присоединения энергопринимающих устройств, максимальная мощность которых составляет до 15 кВт включительно (с учетом ранее присоединенных в данной точке присоединения энергопринимающих устройств), которые используются для бытовых и иных нужд, не связанных с осуществлением предпринимательской деятельности, и электроснабжение которых предусматривается no одному источнику.</p>
<p class="alert alert-info">2. Размещенные в личном кабинете заявителя условия типового договора об осуществлении технологического присоединения к электрическим сетям и технические условия признаются офертой, а оплата заявителем счета на оплату технологического присоединения по договору - акцептом договора об осуществлении технологического присоединения к электрическим сетям и технических условий. В случае неоплаты заявителем счета в установленный срок его заявка признается аннулированной.</p>
<p class="alert alert-info">3. Заявитель обязан в течение 5 рабочих дней (если для заявителя установлено требование осуществления закупки с соблюдением требований Федерального закона "О контрактной системе в сфере закупок товаров, работ, услуг для обеспечения государственных и муниципальных нужд" или Федерального закона "О государственном оборонном заказе", - в течение 15 рабочих дней) со дня выставления сетевой организацией счета на оплату технологического присоединения, оплатить такой счет.</p>
<p class="alert alert-info">4. При желании, могут воспользоваться рассрочкой платежа за технологическое присоединение, следующие заявители:
<br />- юридические лица или индивидуальные предприниматели в целях технологического присоединения по второй или третьей категории надежности энергопринимающих устройств, максимальная мощность которых составляет до 150 кВт включительно (с учетом ранее присоединенных в данной точке присоединения энергопринимающих устройств) (за исключением заявителей, присоединяемых по третьей категории надежности (no одному источнику электроснабжения) к объектам электросетевого хозяйства сетевой организации на уровне напряжения 0,4 кВ и ниже, при условии, что расстояние от этих энергопринимающих устройств до ближайшего объекта электрической сети необходимого заявителю класса напряжения составляет не более 200 метров в городах и поселках городского типа и не более 300 метров в сельской местности);
<br />- юридические лица или индивидуальные предприниматели в целях одновременного технологического присоединения к объектам электросетевого хозяйства с уровнем напряжения до 1000 В энергопринимающих устройств, максимальная мощность которых составляет до 150 кВт включительно (с учетом ранее присоединенньх в данной точке присоединения энергопринимающих устройств), электроснабжение которых предусматривается по одному источнику, и объектов микрогенерации;
<br />- физические лица в целях одновременного технологического присоединения к объектам электросетевого хозяйства с уровнем напряжения до 1000 В;
энергопринимающих устройств, максимальная мощность которых составляет до 150 кВт включительно (с учетом ранее присоединенных в данной точке присоединения энергопринимающих устройств) ,которые используются для бытовых и иных нужд, не связанных с осуществлением предпринимательской деятельности, и электроснабжение которых предусматривается по одному источнику, и объектов микроrенерации.</p>
<p class="alert alert-info">5. За предоставление рассрочки платежа за технологическое присоединение сетевой организации заявителем выплачиваются проценты. Проценты начисляются на остаток задолженности заявителя и подлежат оплате одновременно с очередным платежом, которым погашается частично или полностью такая задолженность. Размер процентов (в процентах годовых) определяется за каждый день рассрочки в размере действовавшей на указанный день ключевой ставки Центрального банка Российской Федерации, увеличенной на 4 процентных пункта.</p>
<p class="alert alert-info">Для заявителей, подающих заявки на технологическое присоединение по другим уровням напряжения и мощности, технологическое присоединение осуществляется на основании договора, заключаемого между сетевой организацией и юридическим или физическим лицом на бумажном носителе в установленные 861 ПП РФ сроки.</p>

Для направления заявки на технологическое присоединение необходимо:
<br />
- выбрать вид заявки
<br />
- распечатать <a href="../DocForms" target="_blank">форму заявки</a>
<br />
- заполнить её в соответствии с <a href="../DocForms" target="_blank">образцом</a>
<br />
- приложить скан-копию заявки
<br />
- приложить необходимые документы
<br />
- отправить заявку
<br />

<br />
<div class="row">    
<div class="col-md-6">    
    <dx:BootstrapComboBox ID="ComboBoxPrichinaPodachiZ" runat="server" Caption="Причина подачи заявки:" DataSourceID="SqlDataSourcePrichinaPodachiZ" NullText="Укажите причину подачи заявки...">
        <ClearButton DisplayMode="OnHover" />
        <ValidationSettings ValidationGroup="SendOrderValidationGroup">
            <RequiredField IsRequired="true" ErrorText="Укажите причину подачи заявки" />
        </ValidationSettings>
    </dx:BootstrapComboBox>
</div>
</div>
<br />
<div class="row">    
<div class="col-md-6">    
    <dx:BootstrapComboBox ID="ComboBoxFIASRegionCity" runat="server" Caption="Район (город) расположения энергопринимающего устройства:" DataSourceID="SqlDataSourceFIASRegionCity" NullText="Выберите район (город)...">
        <ClearButton DisplayMode="OnHover" />
        <ValidationSettings ValidationGroup="SendOrderValidationGroup">
            <RequiredField IsRequired="true" ErrorText="Укажите район (город)" />
        </ValidationSettings>
    </dx:BootstrapComboBox>
</div>
</div>
<br />
<div class="row">    
<div class="col-md-6"> 
    
    <asp:Label runat="server" AssociatedControlID="ASPxTextBoxAddressEPU2" CssClass="control-label">Адрес энергопринимающего устройства:*</asp:Label>
    <asp:TextBox ID="ASPxTextBoxAddressEPU2" runat="server" Width="100%" CssClass="form-control" ValidationGroup="SendOrderValidationGroup"></asp:TextBox>            
    <asp:RequiredFieldValidator runat="server" ControlToValidate="ASPxTextBoxAddressEPU2"
          ErrorMessage="обязательное поле" ValidationGroup="SendOrderValidationGroup" ForeColor="#FF3300" />

    <%--<dx:BootstrapTextBox ID="ASPxTextBoxAddressEPU" runat="server" Caption="Адрес энергопринимающего устройства:" ReadOnly="False" 
        CaptionSettings-Position="Before"
        HelpText="В данном поле необходимо указать информацию, касающуюся местоположения Вашего объекта согласно правоустанавливающего документа. В том случае, если точный адрес объекта не может быть указан (например, для строящегося объекта), то укажите ориентир для наиболее точного определения местоположения."
        HelpTextSettings-DisplayMode="Inline" ClientInstanceName="CI_ASPxTextBoxAddressEPU" Width="100%">
        <ValidationSettings ValidationGroup="SendOrderValidationGroup">
            <RequiredField ErrorText="обязательное поле" IsRequired="true" />
        </ValidationSettings>
    </dx:BootstrapTextBox>--%>
        
    <script type="text/javascript">    
        $("#MainContent_ASPxTextBoxAddressEPU2").suggestions({
            token: "9bfcf5339af57a6b7a2959bbbaea81712ffaa79e",
            type: "ADDRESS"            
        });
    </script>
</div>
</div>

<br />
<div class="row">    
<div class="col-md-6">    
    <dx:BootstrapTextBox ID="ASPxTextBoxMaxMoschnost" runat="server" Caption="Максимальная мощность (присоединяемых и ранее присоединенных) энергопринимающих устройств, кВт:" ReadOnly="False" 
        CaptionSettings-Position="Before"
        HelpText="суммируются существующая и вновь запрашиваемая мощности"
        HelpTextSettings-DisplayMode="Inline" Width="100%" MaskSettings-Mask="<1..3000>" ValidationSettings-ValidationGroup="SendOrderValidationGroup">
    </dx:BootstrapTextBox>
</div>
</div>
<br />
<div class="row">    
<div class="col-md-6">    
    <dx:BootstrapComboBox ID="ComboBoxUrovenU" runat="server" Caption="Уровень напряжения:" DataSourceID="SqlDataSourceUrovenU" NullText="Укажите уровень напряжения...">
        <ClearButton DisplayMode="OnHover" />
        <ValidationSettings ValidationGroup="SendOrderValidationGroup">
            <RequiredField IsRequired="true" ErrorText="Укажите уровень напряжения" />
        </ValidationSettings>
    </dx:BootstrapComboBox>
</div>
</div>

<%--<asp:PlaceHolder runat="server" ID="panelVidOplaty" ViewStateMode="Disabled" Visible="true">--%>
<br />
<div class="row">    
<div class="col-md-6">    
    <dx:BootstrapComboBox ID="ComboBoxVidRassrochki" runat="server" Caption="Вариант оплаты:" DataSourceID="SqlDataSourceVidRassrochki" NullText="Выберите вариант оплаты...">
        <ClearButton DisplayMode="OnHover" />
        <ValidationSettings ValidationGroup="SendOrderValidationGroup">
            <RequiredField IsRequired="true" ErrorText="Выберите вариант оплаты" />
        </ValidationSettings>
    </dx:BootstrapComboBox>
</div>
</div>
<%--</asp:PlaceHolder>--%>

<br />
<div class="row">    
<div class="col-md-6">    
    <dx:BootstrapComboBox ID="ComboBoxGP" runat="server" Caption="Гарантирующий поставщик:" DataSourceID="SqlDataSourceGP" NullText="выберите значение...">        
        <ValidationSettings ValidationGroup="SendOrderValidationGroup">
            <RequiredField IsRequired="true" ErrorText="выберите значение" />
        </ValidationSettings>
    </dx:BootstrapComboBox>
</div>
</div>

<br />
<h4>Необходимые документы:</h4>
<br />

<div class="row">    
<div class="col-md-12">  
Скан-копия заявки на технологическое присоединение (<span style="color: #FF0000">обязательно</span>):
</div>

<div class="col-md-4">  
<dx:ASPxLabel ID="ASPxLabel7" runat="server" AssociatedControlID="" Text="" Theme="SoftOrange" />
<dx:ASPxUploadControl ID="ASPxUploadControlOrder10" runat="server" UploadMode="Auto" Width="100%" UploadButton-Text="Прикрепить документы" UploadButton-ImagePosition="Left" ShowUploadButton="True" ShowProgressPanel="True" FileInputCount="2" ValidationSettings-MaxFileSize="10240000" ValidationSettings-MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт" ClientInstanceName="CI_UploadDocsControlOrder10" Theme="SoftOrange"
    OnFileUploadComplete="ASPxUploadControlOrderForAll_FileUploadComplete" AutoStartUpload="True">
    <ValidationSettings MaxFileSize="10240000" MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт"></ValidationSettings>
    <ClientSideEvents FilesUploadComplete="function(s, e) {	            
        CI_ASPxGridViewOrderTempDocs10.Refresh();
    }" />
    <UploadButton Text="Прикрепить документы"></UploadButton>            
</dx:ASPxUploadControl>
</div>

<br />

<%-- грид информации о временных документах заявки --%>
<div class="col-md-3">  
 <dx:ASPxGridView ID="ASPxGridViewOrderTempDocs10" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrderTempDocs10"
    Width="100%" Caption="" DataSourceID="SqlDataSourceOrderTempDocsInfo10" KeyFieldName="id_zayavkadoc" EnableTheming="True" Theme="MetropolisBlue" OnRowDeleting="ASPxGridViewOrderTempDocsForAll_RowDeleting" Settings-ShowColumnHeaders="False" SettingsResizing-ColumnResizeMode="Control">        
    <SettingsContextMenu Enabled="True">
    </SettingsContextMenu>
    <SettingsPager PageSize="10" />
    <SettingsBehavior AllowFocusedRow="False"/>
    <SettingsDataSecurity AllowDelete="True" AllowEdit="False" AllowInsert="False" />
    <Columns>                        
        <dx:GridViewDataTextColumn Caption="имя файла" FieldName="doc_file_name" VisibleIndex="0">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>            
        <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="1" Caption="удалить">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewCommandColumn>
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

</div> <%-- 10 --%>

<br />

<div class="row">    
<div class="col-md-12">  
Копия документа, указанного в заявке, удостоверяющего личность заявителя (<span style="color: #FF0000">обязательно для физических лиц</span>):
</div>

<div class="col-md-4">  
<dx:ASPxLabel ID="ASPxLabel3" runat="server" AssociatedControlID="" Text="" Theme="SoftOrange" />
<dx:ASPxUploadControl ID="ASPxUploadControlOrder8" runat="server" UploadMode="Auto" Width="100%" UploadButton-Text="Прикрепить документы" UploadButton-ImagePosition="Left" ShowUploadButton="True" ShowProgressPanel="True" FileInputCount="2" ValidationSettings-MaxFileSize="10240000" ValidationSettings-MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт" ClientInstanceName="CI_UploadDocsControlOrder8" Theme="SoftOrange"
    OnFileUploadComplete="ASPxUploadControlOrderForAll_FileUploadComplete" AutoStartUpload="True">
    <ValidationSettings MaxFileSize="10240000" MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт"></ValidationSettings>
    <ClientSideEvents FilesUploadComplete="function(s, e) {	            
        CI_ASPxGridViewOrderTempDocs8.Refresh();
    }" />
    <UploadButton Text="Прикрепить документы"></UploadButton>            
</dx:ASPxUploadControl>
</div>

<br />

<%-- грид информации о временных документах заявки --%>
<div class="col-md-3">  
 <dx:ASPxGridView ID="ASPxGridViewOrderTempDocs8" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrderTempDocs8"
    Width="100%" Caption="" DataSourceID="SqlDataSourceOrderTempDocsInfo8" KeyFieldName="id_zayavkadoc" EnableTheming="True" Theme="MetropolisBlue" OnRowDeleting="ASPxGridViewOrderTempDocsForAll_RowDeleting" Settings-ShowColumnHeaders="False" SettingsResizing-ColumnResizeMode="Control">        
    <SettingsContextMenu Enabled="True">
    </SettingsContextMenu>
    <SettingsPager PageSize="10" />
    <SettingsBehavior AllowFocusedRow="False"/>
    <SettingsDataSecurity AllowDelete="True" AllowEdit="False" AllowInsert="False" />
    <Columns>                        
        <dx:GridViewDataTextColumn Caption="имя файла" FieldName="doc_file_name" VisibleIndex="0">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>            
        <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="1" Caption="удалить">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewCommandColumn>
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

</div> <%-- 8 --%>

<br />

<div class="row">    
<div class="col-md-12">  
План расположения энергопринимающих устройств, которые необходимо присоединить к электрическим сетям сетевой организации в границах земельного участка и в масштабе, позволяющем определить расстояние от границ земельного участка до объектов электросетевого хозяйства (<span style="color: #FF0000">обязательно</span>):
</div>

<div class="col-md-4">  
<dx:ASPxLabel ID="ASPxLabel1" runat="server" AssociatedControlID="" Text="" Theme="SoftOrange" />
<dx:ASPxUploadControl ID="ASPxUploadControlOrder5" runat="server" UploadMode="Auto" Width="100%" UploadButton-Text="Прикрепить документы" UploadButton-ImagePosition="Left" ShowUploadButton="True" ShowProgressPanel="True" FileInputCount="2" ValidationSettings-MaxFileSize="10240000" ValidationSettings-MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт" ClientInstanceName="CI_UploadDocsControlOrder5" Theme="SoftOrange"
    OnFileUploadComplete="ASPxUploadControlOrderForAll_FileUploadComplete" AutoStartUpload="True">
    <ValidationSettings MaxFileSize="10240000" MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт"></ValidationSettings>
    <ClientSideEvents FilesUploadComplete="function(s, e) {	            
        CI_ASPxGridViewOrderTempDocs5.Refresh();
    }" />
    <UploadButton Text="Прикрепить документы"></UploadButton>            
</dx:ASPxUploadControl>
</div>

<br />

<%-- грид информации о временных документах заявки --%>
<div class="col-md-3">  
 <dx:ASPxGridView ID="ASPxGridViewOrderTempDocs5" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrderTempDocs5"
    Width="100%" Caption="" DataSourceID="SqlDataSourceOrderTempDocsInfo5" KeyFieldName="id_zayavkadoc" EnableTheming="True" Theme="MetropolisBlue" OnRowDeleting="ASPxGridViewOrderTempDocsForAll_RowDeleting" Settings-ShowColumnHeaders="False" SettingsResizing-ColumnResizeMode="Control">        
    <SettingsContextMenu Enabled="True">
    </SettingsContextMenu>
    <SettingsPager PageSize="10" />
    <SettingsBehavior AllowFocusedRow="False"/>
    <SettingsDataSecurity AllowDelete="True" AllowEdit="False" AllowInsert="False" />
    <Columns>                        
        <dx:GridViewDataTextColumn Caption="имя файла" FieldName="doc_file_name" VisibleIndex="0">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>            
        <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="1" Caption="удалить">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewCommandColumn>
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

</div> <%-- 5 --%>

<br />

<div class="row">    
<div class="col-md-12">  
Копия документа, подтверждающего право собственности или иное предусмотренное законом основание на объект капитального строительства и (или) земельный участок, на котором расположены (будут располагаться) объекты заявителя, либо право собственности или иное предусмотренное законом основание на энергопринимающие устройства (<span style="color: #FF0000">обязательно</span>):
</div>

<div class="col-md-4">  
<dx:ASPxLabel ID="ASPxLabel2" runat="server" AssociatedControlID="" Text="" Theme="SoftOrange" />
<dx:ASPxUploadControl ID="ASPxUploadControlOrder6" runat="server" UploadMode="Auto" Width="100%" UploadButton-Text="Прикрепить документы" UploadButton-ImagePosition="Left" ShowUploadButton="True" ShowProgressPanel="True" FileInputCount="2" ValidationSettings-MaxFileSize="10240000" ValidationSettings-MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт" ClientInstanceName="CI_UploadDocsControlOrder6" Theme="SoftOrange"
    OnFileUploadComplete="ASPxUploadControlOrderForAll_FileUploadComplete" AutoStartUpload="True">
    <ValidationSettings MaxFileSize="10240000" MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт"></ValidationSettings>
    <ClientSideEvents FilesUploadComplete="function(s, e) {	            
        CI_ASPxGridViewOrderTempDocs6.Refresh();
    }" />
    <UploadButton Text="Прикрепить документы"></UploadButton>            
</dx:ASPxUploadControl>
</div>

<br />

<%-- грид информации о временных документах заявки --%>
<div class="col-md-3">  
 <dx:ASPxGridView ID="ASPxGridViewOrderTempDocs6" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrderTempDocs6"
    Width="100%" Caption="" DataSourceID="SqlDataSourceOrderTempDocsInfo6" KeyFieldName="id_zayavkadoc" EnableTheming="True" Theme="MetropolisBlue" OnRowDeleting="ASPxGridViewOrderTempDocsForAll_RowDeleting" Settings-ShowColumnHeaders="False" SettingsResizing-ColumnResizeMode="Control">        
    <SettingsContextMenu Enabled="True">
    </SettingsContextMenu>
    <SettingsPager PageSize="10" />
    <SettingsBehavior AllowFocusedRow="False"/>
    <SettingsDataSecurity AllowDelete="True" AllowEdit="False" AllowInsert="False" />
    <Columns>                        
        <dx:GridViewDataTextColumn Caption="имя файла" FieldName="doc_file_name" VisibleIndex="0">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>            
        <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="1" Caption="удалить">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewCommandColumn>
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

</div> <%-- 6 --%>

<br />

<div class="row">    
<div class="col-md-12">  
Доверенность или иные документы, подтверждающие полномочия представителя заявителя, подающего и получающего документы (в том числе подтверждающие полномочия выдавшего доверенность лица), в случае если заявка подается в сетевую организацию представителем заявителя:
</div>

<div class="col-md-4">  
<dx:ASPxLabel ID="ASPxLabel4" runat="server" AssociatedControlID="" Text="" Theme="SoftOrange" />
<dx:ASPxUploadControl ID="ASPxUploadControlOrder7" runat="server" UploadMode="Auto" Width="100%" UploadButton-Text="Прикрепить документы" UploadButton-ImagePosition="Left" ShowUploadButton="True" ShowProgressPanel="True" FileInputCount="2" ValidationSettings-MaxFileSize="10240000" ValidationSettings-MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт" ClientInstanceName="CI_UploadDocsControlOrder7" Theme="SoftOrange"
    OnFileUploadComplete="ASPxUploadControlOrderForAll_FileUploadComplete" AutoStartUpload="True">
    <ValidationSettings MaxFileSize="10240000" MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт"></ValidationSettings>
    <ClientSideEvents FilesUploadComplete="function(s, e) {	            
        CI_ASPxGridViewOrderTempDocs7.Refresh();
    }" />
    <UploadButton Text="Прикрепить документы"></UploadButton>            
</dx:ASPxUploadControl>
</div>

<br />

<%-- грид информации о временных документах заявки --%>
<div class="col-md-3">  
 <dx:ASPxGridView ID="ASPxGridViewOrderTempDocs7" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrderTempDocs7"
    Width="100%" Caption="" DataSourceID="SqlDataSourceOrderTempDocsInfo7" KeyFieldName="id_zayavkadoc" EnableTheming="True" Theme="MetropolisBlue" OnRowDeleting="ASPxGridViewOrderTempDocsForAll_RowDeleting" Settings-ShowColumnHeaders="False" SettingsResizing-ColumnResizeMode="Control">        
    <SettingsContextMenu Enabled="True">
    </SettingsContextMenu>
    <SettingsPager PageSize="10" />
    <SettingsBehavior AllowFocusedRow="False"/>
    <SettingsDataSecurity AllowDelete="True" AllowEdit="False" AllowInsert="False" />
    <Columns>                        
        <dx:GridViewDataTextColumn Caption="имя файла" FieldName="doc_file_name" VisibleIndex="0">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>            
        <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="1" Caption="удалить">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewCommandColumn>
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

</div> <%-- 7 --%>

<br />

<div class="row">    
<div class="col-md-12">  
Учредительные документы (для ЮЛ и ИП):
</div>

<div class="col-md-4">  
<dx:ASPxLabel ID="ASPxLabel5" runat="server" AssociatedControlID="" Text="" Theme="SoftOrange" />
<dx:ASPxUploadControl ID="ASPxUploadControlOrder9" runat="server" UploadMode="Auto" Width="100%" UploadButton-Text="Прикрепить документы" UploadButton-ImagePosition="Left" ShowUploadButton="True" ShowProgressPanel="True" FileInputCount="2" ValidationSettings-MaxFileSize="25000000" ValidationSettings-MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет 25 Мбайт" ClientInstanceName="CI_UploadDocsControlOrder9" Theme="SoftOrange"
    OnFileUploadComplete="ASPxUploadControlOrderForAll_FileUploadComplete" AutoStartUpload="True">
    <ValidationSettings MaxFileSize="25000000" MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет 25 Мбайт"></ValidationSettings>
    <ClientSideEvents FilesUploadComplete="function(s, e) {	            
        CI_ASPxGridViewOrderTempDocs9.Refresh();
    }" />
    <UploadButton Text="Прикрепить документы"></UploadButton>            
</dx:ASPxUploadControl>
</div>

<br />

<%-- грид информации о временных документах заявки --%>
<div class="col-md-3">  
 <dx:ASPxGridView ID="ASPxGridViewOrderTempDocs9" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrderTempDocs9"
    Width="100%" Caption="" DataSourceID="SqlDataSourceOrderTempDocsInfo9" KeyFieldName="id_zayavkadoc" EnableTheming="True" Theme="MetropolisBlue" OnRowDeleting="ASPxGridViewOrderTempDocsForAll_RowDeleting" Settings-ShowColumnHeaders="False" SettingsResizing-ColumnResizeMode="Control">        
    <SettingsContextMenu Enabled="True">
    </SettingsContextMenu>
    <SettingsPager PageSize="10" />
    <SettingsBehavior AllowFocusedRow="False"/>
    <SettingsDataSecurity AllowDelete="True" AllowEdit="False" AllowInsert="False" />
    <Columns>                        
        <dx:GridViewDataTextColumn Caption="имя файла" FieldName="doc_file_name" VisibleIndex="0">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>            
        <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="1" Caption="удалить">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewCommandColumn>
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

</div> <%-- 9 --%>

<br />

<div class="row">    
<div class="col-md-12">  
Прочие документы:
</div>

<div class="col-md-4">  
<dx:ASPxLabel ID="ASPxLabel6" runat="server" AssociatedControlID="" Text="" Theme="SoftOrange" />
<dx:ASPxUploadControl ID="ASPxUploadControlOrder0" runat="server" UploadMode="Auto" Width="100%" UploadButton-Text="Прикрепить документы" UploadButton-ImagePosition="Left" ShowUploadButton="True" ShowProgressPanel="True" FileInputCount="2" ValidationSettings-MaxFileSize="10240000" ValidationSettings-MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт" ClientInstanceName="CI_UploadDocsControlOrder0" Theme="SoftOrange"
    OnFileUploadComplete="ASPxUploadControlOrderForAll_FileUploadComplete" AutoStartUpload="True">
    <ValidationSettings MaxFileSize="10240000" MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт"></ValidationSettings>
    <ClientSideEvents FilesUploadComplete="function(s, e) {	            
        CI_ASPxGridViewOrderTempDocs0.Refresh();
    }" />
    <UploadButton Text="Прикрепить документы"></UploadButton>            
</dx:ASPxUploadControl>
</div>

<br />

<%-- грид информации о временных документах заявки --%>
<div class="col-md-3">  
 <dx:ASPxGridView ID="ASPxGridViewOrderTempDocs0" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrderTempDocs0"
    Width="100%" Caption="" DataSourceID="SqlDataSourceOrderTempDocsInfo0" KeyFieldName="id_zayavkadoc" EnableTheming="True" Theme="MetropolisBlue" OnRowDeleting="ASPxGridViewOrderTempDocsForAll_RowDeleting" Settings-ShowColumnHeaders="False" SettingsResizing-ColumnResizeMode="Control">        
    <SettingsContextMenu Enabled="True">
    </SettingsContextMenu>
    <SettingsPager PageSize="10" />
    <SettingsBehavior AllowFocusedRow="False"/>
    <SettingsDataSecurity AllowDelete="True" AllowEdit="False" AllowInsert="False" />
    <Columns>                        
        <dx:GridViewDataTextColumn Caption="имя файла" FieldName="doc_file_name" VisibleIndex="0">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewDataTextColumn>            
        <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="1" Caption="удалить">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
        </dx:GridViewCommandColumn>
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

</div> <%-- 0 --%>

<br />

<div class="row">    
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <script type="text/javascript">
            function btnCreateOrderClick(s, e) {
                ASPxClientEdit.ValidateGroup('SendOrderValidationGroup');
               /*if (Page_ClientValidate("SendOrderValidationGroup")) btnCreateOrderCI.SetText('Подождите, заявка отправляется');                        */
            }
        </script>
        <dx:ASPxLabel ID="ASPxLabelErrorInfo" runat="server" AssociatedControlID="" Text="" CssClass="alert alert-danger" Visible="False" />
        <br /><br />

        <dx:ASPxButton ID="btnCreateOrder" runat="server" Text="Отправить заявку" OnClick="btnCreateOrder_Click" ClientInstanceName="btnCreateOrderCI" ValidationGroup="SendOrderValidationGroup" CssClass="btn btn-primary">
            <ClientSideEvents Click="btnCreateOrderClick" />
        </dx:ASPxButton>

    </ContentTemplate>
</asp:UpdatePanel>
</div>

<asp:SqlDataSource ID="SqlDataSourceUrovenU" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSourceVidRassrochki" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSourcePrichinaPodachiZ" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSourceFIASRegionCity" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSourceGP" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSourceGPCenovayaKategoriya" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>
    
<asp:SqlDataSource ID="SqlDataSourceOrderTempDocsInfo0" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSourceOrderTempDocsInfo5" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSourceOrderTempDocsInfo6" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSourceOrderTempDocsInfo7" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSourceOrderTempDocsInfo8" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSourceOrderTempDocsInfo9" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSourceOrderTempDocsInfo10" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>

</asp:Content>
