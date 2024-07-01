<%@ Page Title="Личный кабинет по ТП" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="OrderInfo.aspx.cs" Inherits="portaltp.Cabinet.OrderInfo" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    

    <script type="text/javascript">      
        var orderID;

        function popupSetFilial_Shown(s, e)
        {
            CI_callbackPanelSetFilial.PerformCallback(orderId);
        }
        function ShowPopupSetFilialInOrder(s, e)
        {
            orderId = CI_ASPxTextBoxOrderGUID.GetText();
            CI_callbackPanelSetFilial.SetContentHtml("");
            CI_ASPxPopupControlSetFilial.Show();
        }
        function PopupButtonOkSetFilial(s, e)
        {
            CI_ASPxPopupControlSetFilial.Hide();
            e.processOnServer = true;
        }
        //-------------------------------------
        function popupSetOrderNumber_Shown(s, e) {
            CI_callbackPanelSetOrderNumber.PerformCallback(orderId);
        }
        function ShowPopupSetOrderNumber(s, e) {
            orderId = CI_ASPxTextBoxOrderGUID.GetText();
            CI_callbackPanelSetOrderNumber.SetContentHtml("");
            CI_ASPxPopupControlSetOrderNumber.Show();
        }
        function PopupButtonOkSetOrderNumber(s, e) {
            CI_ASPxPopupControlSetOrderNumber.Hide();
            e.processOnServer = true;
        }

    </script>

    <dx:ASPxTextBox ID="ASPxTextBoxOrderGUID" runat="server" Width="270px" Caption="Идентификатор заявки:" ReadOnly="True" CaptionSettings-Position="Top" HorizontalAlign="Center" ClientInstanceName="CI_ASPxTextBoxOrderGUID"></dx:ASPxTextBox>
    
    <%--<dx:ASPxCardView ID="ASPxCardView1" runat="server" DataSourceID="SqlDataSourceUserInfo" KeyFieldName="id_user" Width="100%" CardLayoutProperties-ColumnCount="3"></dx:ASPxCardView>--%>
    
    <dx:ASPxMemo ID="ASPxMemoUserInfo" runat="server" Height="81px" Width="100%" Caption="Информация о заявителе (контактном лице):" ReadOnly="True">
        <CaptionSettings Position="Top" />            
    </dx:ASPxMemo>

    <br />
    <%--<p class="alert alert-danger">В связи с участившимися случаями распространения новой коронавирусной инфекции, вызванной COVID-19, прием заявок, в том числе, через личный кабинет и выполнение мероприятий по осуществлению технологического присоединения в филиалах ОГУЭП «Облкоммунэнерго» «Иркутские электрические сети», «Саянские электрические сети», «Усть-Кутские электрические сети», «Усть-Ордынские электрические сети», «Черемховские электрические сети» приостановлены до 23.11.2020г. Надеемся на Ваше понимание.</p>--%>

    <%-- грид информации об исходной заявке ("черновик") заявителя --%>
    <dx:ASPxGridView ID="ASPxGridViewOrderInfo" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrderInfo"
    Width="100%" Caption="Информация о заявке" DataSourceID="SqlDataSourceOrderInfo" KeyFieldName="id_zayavka" EnableTheming="True" SettingsResizing-ColumnResizeMode="Disabled">        
        <SettingsContextMenu Enabled="True">
        </SettingsContextMenu>
        <SettingsAdaptivity>
            <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
        </SettingsAdaptivity>
        <SettingsPager PageSize="50" />
        <Settings ShowFilterRow="False" ShowFilterRowMenu="False" ShowFilterRowMenuLikeItem="False" ShowGroupPanel="False" ShowHeaderFilterButton="False" />
        <SettingsBehavior AllowFocusedRow="False"/>
        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
        <SettingsSearchPanel Visible="False" />
        <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
        <Columns>            

            <dx:GridViewDataDateColumn FieldName="date_create_zayavka" VisibleIndex="1" Caption="Дата подачи">                
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn FieldName="caption_zayavkatype_short" VisibleIndex="2" Caption="Вид заявки">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="v1C_Zayavitel" VisibleIndex="3" Caption="Заявитель">                
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>            

            <dx:GridViewDataTextColumn FieldName="caption_filial_short" VisibleIndex="4" Caption="Филиал">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="zayavka_number_1c" VisibleIndex="5" Caption="Номер заявки">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Дата заявки" FieldName="zayavka_date_1c" VisibleIndex="6">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn FieldName="caption_zayavkastatus" VisibleIndex="7" Caption="Статус заявки">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Z_status_Oplaty" VisibleIndex="10" Caption="Статус оплаты">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />    
                <DataItemTemplate>
                    <dx:ASPxLabel ID="ASPxLabelStatusOplaty" runat="server" Text="" OnInit="ASPxLabelStatusOplaty_Init"></dx:ASPxLabel>                
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="v1C_adresEPU" VisibleIndex="11" Caption="Адрес ЭПУ" CellStyle-HorizontalAlign="Left">                
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                <CellStyle HorizontalAlign="Left"></CellStyle>
            </dx:GridViewDataTextColumn>
                                    
            <dx:GridViewDataTextColumn FieldName="v1C_maxMoschnostEPU" VisibleIndex="12" Caption="Pmax">                
                <PropertiesTextEdit DisplayFormatString="F1">
                </PropertiesTextEdit>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Z_uroven_U" VisibleIndex="13" Caption="U">                
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Z_vid_Oplaty" VisibleIndex="14" Caption="Вариант оплаты">                
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
        
    <dx:ASPxPopupControl ID="ASPxPopupControlSetFilial" ClientInstanceName="CI_ASPxPopupControlSetFilial" runat="server" AllowDragging="true"
                            PopupHorizontalAlign="Center" PopupVerticalAlign="Middle" HeaderText="Выберите филиал" CloseAction="CloseButton" CloseOnEscape="True" Modal="True" PopupElementID="ASPxButtonSetFilial">
                            <ContentCollection>
                                <dx:PopupControlContentControl runat="server">
                                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanelSetFilial" ClientInstanceName="CI_callbackPanelSetFilial" runat="server"
                                        Width="255px" Height="150px" OnCallback="callbackPanelSetFilial_Callback" RenderMode="Table">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                <dx:ASPxComboBox ID="ASPxComboBoxUserFilial" runat="server" SelectedIndex="0" Width="250px" ValueType="System.Int32" AutoPostBack="False" ClientInstanceName="comboboxUF" DropDownStyle="DropDownList" DataSourceID="SqlDataSourceUserFilial" Caption="Филиал:" CaptionSettings-Position="Top">
                                                    <ClearButton DisplayMode="Never">
                                                    </ClearButton>
                                                    <CaptionSettings Position="Top"></CaptionSettings>
                                                </dx:ASPxComboBox>    
                                        
                                                <br />
                            
                                                <dx:ASPxButton ID="ASPxButtonSetFilialOk" runat="server" Text="Принять" Theme="SoftOrange" AutoPostBack="False" ClientInstanceName="CI_ASPxButtonSetFilialOk" OnClick="ASPxButtonSetFilialOk_Click">        
                                                    <ClientSideEvents Click="function(s, e) {
	                                                    PopupButtonOkSetFilial(s,e);
                                                    }" />
                                                </dx:ASPxButton>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>                
                                </dx:PopupControlContentControl>
                            </ContentCollection>        
                            <ClientSideEvents Shown="popupSetFilial_Shown" />
                        </dx:ASPxPopupControl>

                        <dx:ASPxButton ID="ASPxButtonSetFilial" runat="server" Text="Закрепить за филиалом" AutoPostBack="False" ClientInstanceName="CI_ASPxButtonSetFilial" CssClass="btn btn-primary btn-xs" Visible="False">
                            <ClientSideEvents Click="function(s, e) {
                                ShowPopupSetFilialInOrder(s, e);            
                            }" />
                        </dx:ASPxButton>

                        <dx:ASPxPopupControl ID="ASPxPopupControlSetOrderNumber" ClientInstanceName="CI_ASPxPopupControlSetOrderNumber" runat="server" AllowDragging="true"
                            PopupHorizontalAlign="Center" PopupVerticalAlign="Middle" HeaderText="Введите реквизиты" CloseAction="CloseButton" CloseOnEscape="True" Modal="True" PopupElementID="ASPxButtonSetOrderNumber">
                            <ContentCollection>
                                <dx:PopupControlContentControl runat="server">
                                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanelSetOrderNumber" ClientInstanceName="CI_callbackPanelSetOrderNumber" runat="server"
                                        Width="255px" Height="250px" OnCallback="callbackPanelSetOrderNumber_Callback" RenderMode="Table">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                <dx:ASPxTextBox ID="tbOrderNumber" runat="server" Width="250px" Caption="Номер заявки (присвоенный):" CaptionSettings-Position="Top">
                                                    <CaptionSettings Position="Top"></CaptionSettings>
                                                    <ValidationSettings ValidationGroup="SetOrderNumberValidationGroup">
                                                        <RequiredField IsRequired="false" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
        
                                                <br />
        
                                                <dx:ASPxDateEdit ID="tbOrderDate" runat="server"  Width="250px" Caption="Дата заявки (присвоенная):" CaptionSettings-Position="Top">
                                                    <CaptionSettings Position="Top"></CaptionSettings>
                                                    <ValidationSettings ValidationGroup="SetOrderNumberValidationGroup">
                                                        <RequiredField IsRequired="false" />
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
        
                                                <br />
            
                                                <dx:ASPxComboBox ID="ASPxComboBoxOrderStatus" runat="server" SelectedIndex="0" Width="250px" ValueType="System.Int32" AutoPostBack="False" ClientInstanceName="comboboxOS" DropDownStyle="DropDownList" DataSourceID="SqlDataSourceOrderStatus" Caption="Статус заявки:">
                                                    <ClearButton DisplayMode="Never">
                                                    </ClearButton>
                                                    <CaptionSettings Position="Top" />
                                                </dx:ASPxComboBox>
                                        
                                                <br />
                            
                                                <dx:ASPxButton ID="ASPxButtonSetOrderNumberOK" runat="server" Text="Принять" Theme="SoftOrange" AutoPostBack="False" ClientInstanceName="CI_ASPxButtonSetOrderNumberOk" OnClick="ASPxButtonSetOrderNumberOk_Click" ValidationGroup="SetOrderNumberValidationGroup">        
                                                    <ClientSideEvents Click="function(s, e) {
	                                                    PopupButtonOkSetOrderNumber(s,e);
                                                    }" />
                                                </dx:ASPxButton>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>                
                                </dx:PopupControlContentControl>
                            </ContentCollection>        
                            <ClientSideEvents Shown="popupSetOrderNumber_Shown" />
                        </dx:ASPxPopupControl>

                        <dx:ASPxButton ID="ASPxButtonSetOrderNumber" runat="server" Text="Присвоить номер/Статус" AutoPostBack="False" ClientInstanceName="CI_ASPxButtonSetOrderNumber" CssClass="btn btn-primary btn-xs" Visible="False">
                            <ClientSideEvents Click="function(s, e) {
                                ShowPopupSetOrderNumber(s, e);            
                            }" />
                        </dx:ASPxButton>
    
                        <br /><br />

    <dx:BootstrapPageControl ID="BootstrapPageControl1" runat="server">
        <TabPages>
                        
            <dx:BootstrapTabPage Text="Заявка (черновик)" Name="Z">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">

                        <%-- грид информации о документах заявки --%>
                        <%--<asp:UpdatePanel ID="UpdatePanelGridOrderDocs" runat="server">
                            <ContentTemplate>--%>
                        <dx:ASPxGridView ID="ASPxGridViewOrderDocs" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrderDocs"
                        Width="100%" Caption="Документы заявки" DataSourceID="SqlDataSourceOrderDocs" KeyFieldName="id_zayavkadoc" EnableTheming="True" SettingsResizing-ColumnResizeMode="Disabled" SettingsPager-Position="TopAndBottom">        
                            <SettingsContextMenu Enabled="True">
                            </SettingsContextMenu>

<SettingsAdaptivity>
<AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
</SettingsAdaptivity>

                            <SettingsPager PageSize="10" />
                            <SettingsBehavior AllowFocusedRow="False"/>
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>            
                                <dx:GridViewDataTextColumn FieldName="caption_doctype" VisibleIndex="3" Caption="Вид документа">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>
                                
                                <dx:GridViewDataDateColumn FieldName="date_doc_add" VisibleIndex="4" Caption="Дата добавления">
                                    <PropertiesDateEdit DisplayFormatString=""></PropertiesDateEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataDateColumn>            
                                
                                <dx:GridViewDataTextColumn Caption="Документ" FieldName="doc_file_name" VisibleIndex="1" CellStyle-HorizontalAlign="Left">                
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>
                                
                                <dx:GridViewDataCheckColumn Caption="Действия" FieldName="" VisibleIndex="2">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                    <DataItemTemplate>                    
                                        <dx:ASPxHyperLink ID="ASPxHyperLinkViewOrderDoc" runat="server" Text="" oninit="urlViewOrderDoc_Init" 
                                            CssClass="btn btn-default glyphicon glyphicon-eye-open btn-sm" ToolTip="посмотреть" Target="_blank" ForeColor="#337AB7">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="ASPxHyperLinkDownloadOrderDoc" runat="server" Text="" oninit="urlDownloadOrderDoc_Init" 
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

                                <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:BootstrapTabPage> <%-- Заявка-"черновик" --%>

            <dx:BootstrapTabPage Text="Заявка (рабочая)" Name="Z1C">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">

                        <%-- грид информации из 1С о заявке заявителя --%>
                        <%--<dx:ASPxGridView ID="ASPxGridViewOrderInfo1С" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrderInfo1С"
                        Width="100%" Caption="Информация о заявке" DataSourceID="SqlDataSourceOrderInfo" KeyFieldName="id_zayavka" EnableTheming="True" SettingsResizing-ColumnResizeMode="Disabled">        
                            <SettingsContextMenu Enabled="True">
                            </SettingsContextMenu>
                            <SettingsPager PageSize="50" />
                            <Settings ShowFilterRow="False" ShowFilterRowMenu="False" ShowFilterRowMenuLikeItem="False" ShowGroupPanel="False" ShowHeaderFilterButton="False" />
                            <SettingsBehavior AllowFocusedRow="False"/>
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                            <SettingsSearchPanel Visible="False" />
                            <Columns>            
                                <dx:GridViewDataTextColumn FieldName="caption_zayavkatype_short" VisibleIndex="1" Caption="Вид заявки">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="zayavka_number_1c" VisibleIndex="4" Caption="Номер заявки (присвоенный)">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="caption_zayavkastatus" VisibleIndex="6" Caption="Статус заявки">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="caption_filial_short" VisibleIndex="2" Caption="Филиал">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="date_create_zayavka" VisibleIndex="3" Caption="Дата поступления заявки">                
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataDateColumn>            
                                <dx:GridViewDataTextColumn FieldName="user_nameingrid" VisibleIndex="0" Caption="Заявитель">                
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>            
                                <dx:GridViewDataDateColumn Caption="Дата заявки (присвоенная)" FieldName="zayavka_date_1c" VisibleIndex="5">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
                                </dx:GridViewDataDateColumn>
                            </Columns>
                            <Styles>            
                                <Cell HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                                </Cell>
                            </Styles>
                            <Paddings Padding="0px" />
                            <Border BorderWidth="0px" />
                            <BorderBottom BorderWidth="1px" />        
                        </dx:ASPxGridView>--%>
                                
                        <br />

                        <%-- грид информации о документах заявки --%>
                        <%--<asp:UpdatePanel ID="UpdatePanelGridOrderDocs" runat="server">
                            <ContentTemplate>--%>
                        <dx:ASPxGridView ID="ASPxGridViewOrderDocs1С" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrderDocs1С"
                        Width="100%" Caption="Документы заявки" DataSourceID="SqlDataSourceOrderDocs1C" KeyFieldName="_IDRRef" EnableTheming="True" SettingsResizing-ColumnResizeMode="Disabled" SettingsPager-Position="TopAndBottom">        
                            <SettingsContextMenu Enabled="True">
                            </SettingsContextMenu>

<SettingsAdaptivity>
<AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
</SettingsAdaptivity>

                            <SettingsPager PageSize="10" />
                            <SettingsBehavior AllowFocusedRow="False"/>
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>            
                                <dx:GridViewDataTextColumn FieldName="_Description" VisibleIndex="3" Caption="Вид документа">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>
                                
                                <dx:GridViewDataDateColumn FieldName="Z_DocDate" VisibleIndex="4" Caption="Дата добавления">
                                    <PropertiesDateEdit DisplayFormatString=""></PropertiesDateEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataDateColumn>            
                                
                                <dx:GridViewDataTextColumn Caption="Документ" FieldName="_Fld24758" VisibleIndex="1" CellStyle-HorizontalAlign="Left">                
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>
                                
                                <dx:GridViewDataCheckColumn Caption="Действия" FieldName="" VisibleIndex="2">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                    <DataItemTemplate>                    
                                        <dx:ASPxHyperLink ID="ASPxHyperLinkViewOrderDoc" runat="server" Text="" oninit="urlViewOrderDoc_Init_1С" 
                                            CssClass="btn btn-default glyphicon glyphicon-eye-open btn-sm" ToolTip="посмотреть" Target="_blank" ForeColor="#337AB7">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="ASPxHyperLinkDownloadOrderDoc" runat="server" Text="" oninit="urlDownloadOrderDoc_Init_1С" 
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

                                <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:BootstrapTabPage> <%-- Заявка 1С --%>

            <dx:BootstrapTabPage Text="Договор ТП" Name="D1C">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">

                        <%-- грид информации о договоре ТП --%>
                        <dx:ASPxGridView ID="ASPxGridViewContract" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridViewContractInfo"
                        Width="100%" Caption="Информация о договоре ТП" DataSourceID="SqlDataSourceDogovorInfo1C" KeyFieldName="D_1C_id" EnableTheming="True" SettingsResizing-ColumnResizeMode="Disabled">     
                            <SettingsContextMenu Enabled="True">
                            </SettingsContextMenu>

<SettingsAdaptivity>
<AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
</SettingsAdaptivity>

                            <SettingsPager PageSize="50" />
                            <SettingsBehavior AllowFocusedRow="False"/>
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>            
                                <dx:GridViewDataTextColumn FieldName="D_1C_Nomer" VisibleIndex="1" Caption="Номер договора ТП">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="D_1C_Data" VisibleIndex="2" Caption="Дата договора ТП">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataDateColumn>            
                                <%--<dx:GridViewDataTextColumn FieldName="D_1C_Status" VisibleIndex="3" Caption="Статус договора ТП">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>--%>

                                <dx:GridViewDataTextColumn FieldName="" VisibleIndex="3" Caption="Статус договора ТП">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                    <DataItemTemplate>
                                        <asp:Label ID="Label_Field_StatusDogovora" runat="server" Text="" oninit="field_StatusDogovora_Init"></asp:Label>                                        
                                    </DataItemTemplate>
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
                        <%--<dx:ASPxButton ID="ASPxButtonAddContractInfo" runat="server" Text="Присвоить номер/Статус" Theme="Youthful" AutoPostBack="True" ClientInstanceName="CI_ASPxButtonAddContractInfo" OnClick="ASPxButtonAddContractInfo_Click">        
                        </dx:ASPxButton>
                        <br />--%>

                        <%-- грид информации о документах договора ТП --%>
                        <dx:ASPxGridView ID="ASPxGridViewContractDocs" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridViewContractDocs"
                        Width="100%" Caption="Документы договора" DataSourceID="SqlDataSourceDogovorDocs1C" KeyFieldName="_IDRRef" EnableTheming="True" SettingsResizing-ColumnResizeMode="Control">        
                            <SettingsContextMenu Enabled="True">
                            </SettingsContextMenu>
                            <SettingsPager PageSize="10" />
                            <SettingsBehavior AllowFocusedRow="False"/>

<SettingsAdaptivity>
<AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
</SettingsAdaptivity>

                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>            
                                <dx:GridViewDataTextColumn FieldName="_Description" VisibleIndex="3" Caption="Вид документа">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>
                                
                                <dx:GridViewDataDateColumn FieldName="D_DocDate" VisibleIndex="4" Caption="Дата добавления">
                                    <PropertiesDateEdit DisplayFormatString=""></PropertiesDateEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataDateColumn>            
                                
                                <dx:GridViewDataTextColumn Caption="Документ" FieldName="_Fld24758" VisibleIndex="1" CellStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>

                                <%--<dx:GridViewDataCheckColumn Caption="Подписан исполнителем" FieldName="esign_oke" VisibleIndex="4" Visible="True">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                    <DataItemTemplate>
                                        <dx:ASPxImage runat="server" ID="imgESign" Height="16px" oninit="imgEsignOke_Init">
                                        </dx:ASPxImage>
                                    </DataItemTemplate>
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn Caption="Подписан заказчиком" FieldName="esign_kontr" VisibleIndex="5" Visible="True">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                    <DataItemTemplate>
                                        <dx:ASPxImage runat="server" ID="ASPxImage1" Height="16px" oninit="imgEsignKontr_Init">
                                        </dx:ASPxImage>
                                    </DataItemTemplate>
                                </dx:GridViewDataCheckColumn>

                                <dx:GridViewDataTextColumn Caption="Идентификатор файла" FieldName="id_zayavkadoc" VisibleIndex="6" Visible="True">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>--%>

                                <dx:GridViewDataCheckColumn Caption="Действия" FieldName="" VisibleIndex="2">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                    <DataItemTemplate>                    
                                        <%--<dx:ASPxHyperLink ID="ASPxHyperLinkDownloadContractDoc" runat="server" Text="скачать" oninit="urlDownloadContractDoc_Init">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="ASPxHyperLinkSignContractDoc" runat="server" Text="подписать" oninit="urlSignContractDoc_Init">
                                        </dx:ASPxHyperLink>--%>

                                        <dx:ASPxHyperLink ID="ASPxHyperLinkViewDogovorDoc" runat="server" Text="" oninit="urlViewOrderDoc_Init_1С" 
                                            CssClass="btn btn-default glyphicon glyphicon-eye-open btn-sm" ToolTip="посмотреть" Target="_blank" ForeColor="#337AB7">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="ASPxHyperLinkDownloadDogovorDoc" runat="server" Text="" oninit="urlDownloadOrderDoc_Init_1С" 
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
                        <br />
                        <%--<dx:ASPxButton ID="ASPxButtonAddContractDoc" runat="server" Text="Добавить документ" Theme="Youthful" AutoPostBack="True" ClientInstanceName="CI_ASPxButtonAddContractDoc" OnClick="ASPxButtonAddContractDoc_Click">        
                        </dx:ASPxButton>--%>
                        
                        <asp:PlaceHolder runat="server" ID="PlaceHolder_DocsPril2Pril6" Visible="false">    
                            <p><a runat="server" href="~/Docs/Инструкция по самостоятельному осуществлению фактического присоединения.pdf" visible="true" target="_blank">Инструкция по самостоятельному осуществлению фактического присоединения объектов заявителя к электрическим сетям и фактического приема (подачи) напряжения и мощности для потребления</a></p>
                            <p><a runat="server" href="~/Docs/Уведомление о возможности временного ТП.pdf" visible="true" target="_blank">Уведомление о возможности временного технологического присоединения, о последствиях наступления бездоговорного потребления электрической энергии</a></p>
                        </asp:PlaceHolder>

                    </dx:ContentControl>
                </ContentCollection>
            </dx:BootstrapTabPage> <%-- Договор ТП  --%>
            
            <dx:BootstrapTabPage Text="Счет (оплата)" Name="Oplata1C"> <%-- Счет (оплата) --%>
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                   
<div class="alert alert-warning">
<p>Оплата за услуги по технологическому присоединению должна производиться строго в соответствии с выставленным счетом!</p>
<p>Заявитель при внесении платы за технологическое присоединение в назначении платежа обязан указать реквизиты счета и договора в полном соответствии с полем «Назначение платежа» выставленного счета, например:</p>
<blockquote>Оплата по счёту № НОМЕРСЧЁТА от ДАТАСЧЁТА договор НОМЕРДОГОВОРА</blockquote>
<p><b>Для удобства и правильности заполнения рекизитов физическим лицам рекомендуется осуществлять оплату по QR-коду.</b></p>
<p>Дата договора присваивается в день поступления денежных средств на расчетный счет сетевой организации.</p>
<p><b>Оплата в обязательном порядке должна производиться непосредственно Заявителем!</b></p>
</div>

                        <%-- грид информации о счетах к оплате --%>                        
                        <dx:ASPxGridView ID="ASPxGridViewOplataDocs" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewOrderDocs1С"
                        Width="100%" Caption="Счета к оплате" DataSourceID="SqlDataSourceOplataDocs1C" KeyFieldName="_IDRRef" EnableTheming="True" SettingsResizing-ColumnResizeMode="Control" SettingsPager-Position="TopAndBottom">        
                            <SettingsContextMenu Enabled="True">
                            </SettingsContextMenu>

<SettingsAdaptivity>
<AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
</SettingsAdaptivity>

                            <SettingsPager PageSize="10" />
                            <SettingsBehavior AllowFocusedRow="False"/>
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>            
                                <dx:GridViewDataTextColumn FieldName="_Description" VisibleIndex="3" Caption="Вид документа">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataDateColumn FieldName="D_DocDate" VisibleIndex="4" Caption="Дата добавления">
                                    <PropertiesDateEdit DisplayFormatString=""></PropertiesDateEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataDateColumn>            

                                <dx:GridViewDataTextColumn Caption="Документ" FieldName="_Fld24758" VisibleIndex="1" CellStyle-HorizontalAlign="Left">                
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataCheckColumn Caption="Действия" FieldName="" VisibleIndex="2">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                    <DataItemTemplate>                    
                                        <dx:ASPxHyperLink ID="ASPxHyperLinkViewOrderDoc" runat="server" Text="" oninit="urlViewOrderDoc_Init_1С" 
                                            CssClass="btn btn-default glyphicon glyphicon-eye-open btn-sm" ToolTip="посмотреть" Target="_blank" ForeColor="#337AB7">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="ASPxHyperLinkDownloadOrderDoc" runat="server" Text="" oninit="urlDownloadOrderDoc_Init_1С" 
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

                    </dx:ContentControl>
                </ContentCollection>
            </dx:BootstrapTabPage> <%-- Счет (оплата) --%>

            <dx:BootstrapTabPage Text="Договор энергоснабжения" Visible="True" Name="DEnergo">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        
                        <%-- информация о договоре энергоснабжения --%>
                        
                        <dx:ASPxGridView ID="ASPxGridViewDogovorEnergoInfo" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridViewDogovorEnergoInfo"
                        Width="100%" Caption="Информация о договоре энергоснабжения" DataSourceID="SqlDataSourceDogovorEnergoInfo" KeyFieldName="id_dogovorenergo" EnableTheming="True" SettingsResizing-ColumnResizeMode="Disabled">     
                            <SettingsContextMenu Enabled="True">
                            </SettingsContextMenu>

<SettingsAdaptivity>
<AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
</SettingsAdaptivity>

                            <SettingsPager PageSize="5" />
                            <SettingsBehavior AllowFocusedRow="False"/>
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>            
                                
                                <dx:GridViewDataTextColumn FieldName="nomer_ls" VisibleIndex="1" Caption="Номер лицевого счета">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="nomer_dogovorenergo" VisibleIndex="2" Caption="Номер договора энергоснабжения">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="nomer_elektroustanovka" VisibleIndex="3" Caption="Номер электроустановки">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="nomer_ls" VisibleIndex="7" Caption="Текущий статус" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>
                                                                

                                <%--<dx:GridViewDataDateColumn FieldName="D_1C_Data" VisibleIndex="2" Caption="Дата договора">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataDateColumn>--%>
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
                        <%--<dx:ASPxButton ID="ASPxButtonAddDogovorEnergoInfo" runat="server" Text="Редактировать" AutoPostBack="True" ClientInstanceName="CI_ASPxButtonAddDogovorEnergoInfo" OnClick="ASPxButtonAddDogovorEnergoInfo_Click" CssClass="btn btn-primary btn-xs">        
                        </dx:ASPxButton>--%>
                        
                        <%--<dx:ASPxHyperLink ID="ASPxHyperLinkAddDogovorEnergoInfo" runat="server" Text="Редактировать"  
                            CssClass="btn btn-primary btn-sm" Target="_blank" Visible="False">
                        </dx:ASPxHyperLink>--%>

                        <asp:HyperLink ID="ASPHyperLinkAddDogovorEnergoInfo" runat="server" Text="Редактировать"  
                            CssClass="btn btn-primary btn-sm" Target="_blank" Visible="False">
                        </asp:HyperLink>

                        <br /><br />

                        <%-- грид информации о документах договора энергоснабжения --%>
                        <dx:ASPxGridView ID="ASPxGridViewDogovorEnergoDocs" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridViewDogovorEnergoDocs"
                        Width="100%" Caption="Документы договора энергоснабжения" DataSourceID="SqlDataSourceDogovorEnergoDocs" KeyFieldName="id_zayavkadoc" EnableTheming="True" SettingsResizing-ColumnResizeMode="Disabled">        
                            <SettingsContextMenu Enabled="True">
                            </SettingsContextMenu>
                            <SettingsPager PageSize="10" />
                            <SettingsBehavior AllowFocusedRow="False"/>

<SettingsAdaptivity>
<AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
</SettingsAdaptivity>

                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
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

                                <%--<dx:GridViewDataCheckColumn Caption="Подписан исполнителем" FieldName="esign_oke" VisibleIndex="4" Visible="True">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                    <DataItemTemplate>
                                        <dx:ASPxImage runat="server" ID="imgESign" Height="16px" oninit="imgEsignOke_Init">
                                        </dx:ASPxImage>
                                    </DataItemTemplate>
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn Caption="Подписан заказчиком" FieldName="esign_kontr" VisibleIndex="5" Visible="True">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                    <DataItemTemplate>
                                        <dx:ASPxImage runat="server" ID="ASPxImage1" Height="16px" oninit="imgEsignKontr_Init">
                                        </dx:ASPxImage>
                                    </DataItemTemplate>
                                </dx:GridViewDataCheckColumn>

                                <dx:GridViewDataTextColumn Caption="Идентификатор файла" FieldName="id_zayavkadoc" VisibleIndex="6" Visible="True">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>--%>

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
                        <br />
                        <%--<dx:ASPxButton ID="ASPxButtonAddContractDoc" runat="server" Text="Добавить документ" Theme="Youthful" AutoPostBack="True" ClientInstanceName="CI_ASPxButtonAddContractDoc" OnClick="ASPxButtonAddContractDoc_Click">        
                        </dx:ASPxButton>--%>

                    </dx:ContentControl>
                </ContentCollection>
            </dx:BootstrapTabPage> <%-- договор энергоснабжения --%>

            <dx:BootstrapTabPage Text="Переписка" Visible="True" Name="Chat" Badge-CssClass="mc-tabbadge-red">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">

                        <%-- грид информации о переписке по заявке --%>
    
                        <%--<asp:UpdatePanel ID="UpdatePanelChatAttach" runat="server">
                            <ContentTemplate>--%>
    
                        <dx:ASPxHiddenField ID="ASPxHiddenFieldChatRecID" runat="server"></dx:ASPxHiddenField>       

                        <dx:ASPxGridView ID="ASPxGridViewChat" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewChat"
                        Width="100%" Caption="Переписка по заявке" DataSourceID="SqlDataSourceChatInfo" KeyFieldName="id_chatrec" EnableTheming="True" SettingsResizing-ColumnResizeMode="Disabled" OnHtmlRowPrepared="ASPxGridViewChat_HtmlRowPrepared">        
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
                                        <SettingsBehavior AllowFocusedRow="False"/>
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                        <SettingsSearchPanel Visible="False" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="документ" FieldName="doc_file_name" VisibleIndex="1" CellStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                            </dx:GridViewDataTextColumn>                        
                                            
                                            <dx:GridViewDataDateColumn Caption="дата добавления" FieldName="date_doc_add" VisibleIndex="3">
                                                <PropertiesDateEdit DisplayFormatString=""></PropertiesDateEdit>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                            </dx:GridViewDataDateColumn>
                                            
                                            <dx:GridViewDataCheckColumn Caption="действия" FieldName="" VisibleIndex="2">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                <DataItemTemplate>                                                                        
                                                    <dx:ASPxHyperLink ID="ASPxHyperLinkViewOrderDocChat" runat="server" Text="" oninit="urlViewOrderDoc_Init" 
                                                        CssClass="btn btn-default glyphicon glyphicon-eye-open btn-sm" ToolTip="посмотреть" Target="_blank" ForeColor="#337AB7">
                                                        </dx:ASPxHyperLink>
                                                    <dx:ASPxHyperLink ID="ASPxHyperLinkDownloadOrderDocChat" runat="server" Text="" oninit="urlDownloadOrderDoc_Init" 
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
                                </DetailRow>
                            </Templates>
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                            <SettingsBehavior AllowFocusedRow="False"/>
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
                                    </DataItemTemplate>
                                </dx:GridViewDataCheckColumn>

                                <dx:GridViewDataDateColumn FieldName="chatrec_datetime" VisibleIndex="2" Caption="Дата отправки">                                
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                    <PropertiesDateEdit DisplayFormatString="">
                                    </PropertiesDateEdit>                                    
                                </dx:GridViewDataDateColumn>

                                <dx:GridViewDataTextColumn FieldName="user_nameingrid" VisibleIndex="3" Caption="Отправитель">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>
                                
                                <dx:GridViewDataTextColumn FieldName="caption_userrole" VisibleIndex="4" Caption="Роль">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>
                                
                                <dx:GridViewDataTextColumn FieldName="caption_filial_short" VisibleIndex="5" Caption="Филиал ОКЭ">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>            

                                <dx:GridViewDataMemoColumn Caption="Сообщение" FieldName="caption_msg" VisibleIndex="8" CellStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataMemoColumn>

                                <dx:GridViewDataTextColumn FieldName="id_userrole" VisibleIndex="10" Caption="id_userrole" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="chatrec_isread" VisibleIndex="11" Caption="isread" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="chatrec_isread_user" VisibleIndex="12" Caption="isread_user" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="chatrec_isread_datetime" VisibleIndex="13" Caption="isread_datetime" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataCheckColumn Caption="Действия" FieldName="" VisibleIndex="15">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                    <DataItemTemplate>                                 
                                        <dx:BootstrapButton ID="ButtonChatMessage_SetReadOk" runat="server" AutoPostBack="True" Text=""
                                            CssClasses-Control="btn btn-default btn-sm glyphicon glyphicon-check mcbuttoningrid-red" ToolTip="отметить, как прочитанное"
                                            OnInit="ButtonChatMessage_SetReadOk_Init" OnClick="ButtonChatMessage_SetReadOk_Click">                                            
                                        </dx:BootstrapButton>
                                        <dx:BootstrapButton ID="ButtonChatMessage_SetReadNo" runat="server" AutoPostBack="True" Text=""
                                            CssClasses-Control="btn btn-default btn-sm glyphicon glyphicon-check mcbuttoningrid-green" ToolTip="отметить, как непрочитанное"
                                            OnInit="ButtonChatMessage_SetReadNo_Init" OnClick="ButtonChatMessage_SetReadNo_Click">                                            
                                        </dx:BootstrapButton>
                                    </DataItemTemplate>
                                </dx:GridViewDataCheckColumn>

                                <dx:GridViewDataTextColumn FieldName="" VisibleIndex="16" Caption="Отметка">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />    
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabelIsReadStamp" runat="server" Text="" OnInit="ASPxLabelIsReadStamp_Init" Font-Size="X-Small"></dx:ASPxLabel>                
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>

                                <%--<dx:GridViewDataCheckColumn Caption="Вложения" FieldName="count_docs" VisibleIndex="1">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                    <DataItemTemplate>
                                        <dx:ASPxImage runat="server" ID="imgAttachment" Height="16px" oninit="imgAttachment_Init">
                                        </dx:ASPxImage>
                                    </DataItemTemplate>
                                </dx:GridViewDataCheckColumn>--%>

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
                        
                        <dx:BootstrapCallbackPanel runat="server" ClientInstanceName="callbackPanelFastMessages" OnCallback="CallbackPanelFastMessages_Callback">
                        <ContentCollection>
                        <dx:ContentControl>

                            <dx:ASPxHyperLink ID="ASPxHyperLinkFastMessage1" runat="server" Text="О представлении недостающих документов" 
                                CssClass="btn btn-default btn-sm" ForeColor="#337AB7" 
                                ClientSideEvents-Click="function onUpdateClick(s, e) {
                                callbackPanelFastMessages.PerformCallback('message1');}" Visible="False"></dx:ASPxHyperLink>
                            <br />
                            <dx:ASPxMemo ID="ASPxMemoChat" runat="server" Height="71px" Width="100%" Caption="Введите текст сообщения (не более 2000 символов):" Theme="SoftOrange" MaxLength="2000">
                                <CaptionSettings Position="Top" />
                                <ValidationSettings ValidationGroup="SendMessageValidationGroup">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxMemo>

                        </dx:ContentControl>
                        </ContentCollection>
                        </dx:BootstrapCallbackPanel>

                            <br />   
            
                            <div class="row">
                            <div class="col-xs-10 col-sm-6 col-md-5 col-lg-4">            
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" AssociatedControlID="" Text="документы сообщения" Theme="SoftOrange"/>
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" AssociatedControlID="" Text="(можно прикрепить несколько файлов по очереди, размер одного файла не более 20 Мб):" Theme="SoftOrange" />
                            <dx:ASPxUploadControl ID="ASPxUploadControlChat" runat="server" UploadMode="Auto" Width="100%" UploadButton-Text="Прикрепить документы" UploadButton-ImagePosition="Left" ShowUploadButton="True" ShowProgressPanel="True" FileInputCount="1" ValidationSettings-MaxFileSize="20971520" ValidationSettings-MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет {0} байт" ClientInstanceName="CI_UploadDocsControlChat" Theme="SoftOrange"
                                OnFileUploadComplete="ASPxUploadControlChat_FileUploadComplete" AutoStartUpload="True">
                                <ValidationSettings MaxFileSize="20971520" MaxFileSizeErrorText="Превышен максимальный размер файла, который составляет 20 Мбайт"></ValidationSettings>
                                <ClientSideEvents FilesUploadComplete="function(s, e) {	            
                                    CI_ASPxGridViewChatTempDocs.Refresh();
                                }" />
                                <UploadButton Text="Прикрепить документы"></UploadButton>            
                            </dx:ASPxUploadControl>

                                </div>
                                </div>
                            <br />

                            <%-- грид информации о временных документах сообщения --%>
                                <div class="row">
                                <div class="col-xs-11 col-sm-7 col-md-6 col-lg-5">
                            <dx:ASPxGridView ID="ASPxGridViewChatTempDocs" runat="server" AutoGenerateColumns="False" ClientInstanceName="CI_ASPxGridViewChatTempDocs"
                            Width="100%" Caption="" DataSourceID="SqlDataSourceChatTempDocsInfo" KeyFieldName="id_zayavkadoc" EnableTheming="True" OnRowDeleting="ASPxGridViewChatTempDocs_RowDeleting" Settings-ShowColumnHeaders="False" SettingsResizing-ColumnResizeMode="Control">        
                            <SettingsContextMenu Enabled="True">
                            </SettingsContextMenu>

<SettingsAdaptivity>
<AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
</SettingsAdaptivity>

                            <SettingsPager PageSize="50" />
                            <SettingsBehavior AllowFocusedRow="False"/>

<Settings ShowColumnHeaders="False"></Settings>

<SettingsResizing ColumnResizeMode="Control"></SettingsResizing>

                            <SettingsDataSecurity AllowDelete="True" AllowEdit="False" AllowInsert="False" />

<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>                        
                                <dx:GridViewDataTextColumn Caption="файл" FieldName="doc_file_name" VisibleIndex="0">                
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
                                </div>

                            <br />
                            <dx:ASPxButton ID="ASPxButtonSendMessage" runat="server" Text="Отправить сообщение" ClientInstanceName="CI_btnSendMessage" OnClick="ASPxButtonSendMessage_Click" ValidationGroup="SendMessageValidationGroup" CssClass="btn btn-primary btn-sm">
                                <ClientSideEvents Click="function(s, e) {
	                                if (ASPxClientEdit.ValidateGroup('SendMessageValidationGroup'))
                                    {
                                        CI_btnSendMessage.SetText('Сообщение отправляется');                        	
                                    }                                            
                                 }" />
                            </dx:ASPxButton>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                        
                    </dx:ContentControl>
                </ContentCollection>
            </dx:BootstrapTabPage> <%-- переписка --%>

        </TabPages>
    </dx:BootstrapPageControl>

    <%--<asp:SqlDataSource ID="SqlDataSourceUserInfo" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>--%>
    <asp:SqlDataSource ID="SqlDataSourceOrderInfo" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceOrderDocs" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource> 
    <asp:SqlDataSource ID="SqlDataSourceOrderStatus" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>   
    <asp:SqlDataSource ID="SqlDataSourceContractInfo" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceContractDocs" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>    
    <asp:SqlDataSource ID="SqlDataSourceChatInfo" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceChatDocsInfo" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>">        
        <SelectParameters>
            <asp:SessionParameter Name="id_chatrecS" SessionField="id_chatrecS" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceChatTempDocsInfo" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" ></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceUserFilial" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlDataSourceOrderDocs1C" runat="server" ConnectionString="<%$ ConnectionStrings:1CDbConnectionString %>"></asp:SqlDataSource> 
    <asp:SqlDataSource ID="SqlDataSourceDogovorInfo1C" runat="server" ConnectionString="<%$ ConnectionStrings:1CDbConnectionString %>"></asp:SqlDataSource> 
    <asp:SqlDataSource ID="SqlDataSourceDogovorDocs1C" runat="server" ConnectionString="<%$ ConnectionStrings:1CDbConnectionString %>"></asp:SqlDataSource> 
    
    <asp:SqlDataSource ID="SqlDataSourceOplataDocs1C" runat="server" ConnectionString="<%$ ConnectionStrings:1CDbConnectionString %>"></asp:SqlDataSource> 

    <asp:SqlDataSource ID="SqlDataSourceDogovorEnergoInfo" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource> 
    <asp:SqlDataSource ID="SqlDataSourceDogovorEnergoDocs" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>"></asp:SqlDataSource> 
</asp:Content>
