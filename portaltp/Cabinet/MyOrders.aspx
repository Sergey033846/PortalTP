<%@ Page Title="Личный кабинет по ТП" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="portaltp.Cabinet.MyOrders" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <%--<p class="alert alert-danger">В связи с участившимися случаями распространения новой коронавирусной инфекции, вызванной COVID-19, прием заявок, в том числе, через личный кабинет и выполнение мероприятий по осуществлению технологического присоединения в филиалах ОГУЭП «Облкоммунэнерго» «Иркутские электрические сети», «Саянские электрические сети», «Усть-Кутские электрические сети», «Усть-Ордынские электрические сети», «Черемховские электрические сети» приостановлены до 23.11.2020г. Надеемся на Ваше понимание.</p>--%>

    <p class="alert alert-info">Дополнительные или недостающие документы следует отправлять сопроводительным сообщением на вкладке «Переписка» соответствующей заявки.</p>

    <%--<dx:ASPxMemo ID="ASPxMemoHint" runat="server" Width="100%" Text="" CaptionSettings-Position="Top" BackColor="#FFFFCC" Border-BorderColor="Silver" Height="50px"></dx:ASPxMemo>
    <br />--%>

    <dx:ASPxGridView ID="ASPxGridViewMyOrders" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1"
    Width="100%" Caption="Мои заявки" DataSourceID="SqlDataSourceMyOrders" KeyFieldName="id_zayavka" EnableTheming="True" SettingsResizing-ColumnResizeMode="Control" SettingsPager-Position="TopAndBottom"
        OnHtmlRowPrepared="ASPxGridViewMyOrders_HtmlRowPrepared">
        <ClientSideEvents RowDblClick="function(s, e) {                
                window.open('OrderInfo?id='+s.GetRowKey(e.visibleIndex), '_blank');
	        }"></ClientSideEvents>

        <SettingsContextMenu Enabled="True">
        </SettingsContextMenu>
        <SettingsPager PageSize="10" />
        
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" />

        <SettingsAdaptivity>
            <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
        </SettingsAdaptivity>

        <SettingsBehavior AllowFocusedRow="False"/>

        <SettingsResizing ColumnResizeMode="Disabled"></SettingsResizing>

        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
        <SettingsSearchPanel Visible="True" />

        <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>

        <Columns>            

            <dx:GridViewDataDateColumn FieldName="date_create_zayavka" VisibleIndex="1" Caption="Дата подачи">                
                <PropertiesDateEdit DisplayFormatString="">
                </PropertiesDateEdit>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataDateColumn>                        

            <dx:GridViewDataCheckColumn FieldName="msgs_not_read_from_oke_flag" VisibleIndex="2" Caption="Новое сообщ.">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                <DataItemTemplate>    
                    <dx:ASPxLabel ID="ASPxLabelNewMessages" runat="server" Text="" OnInit="ASPxLabelNewMessages_Init" 
                        CssClass="glyphicon glyphicon-flag mcbuttoningrid-red" ToolTip="поступили новые сообщения">
                    </dx:ASPxLabel>                                
                </DataItemTemplate>
            </dx:GridViewDataCheckColumn>

            <dx:GridViewDataTextColumn FieldName="v1C_Zayavitel" VisibleIndex="3" Caption="Заявитель" CellStyle-HorizontalAlign="Left">                
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            <DataItemTemplate>
                <%#Eval("v1C_Zayavitel")%>
                <br />
                <a target="_blank" href="OrderInfo?id=<%#Eval("id_zayavka")%>" style="color: #337AB7">(просмотр заявки)</a>                    
            </DataItemTemplate>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="caption_zayavkatype_short" VisibleIndex="4" Caption="Вид заявки">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="caption_filial_short" VisibleIndex="5" Caption="Филиал">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="zayavka_number_1c" VisibleIndex="6" Caption="Номер заявки" CellStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn Caption="Дата заявки" FieldName="zayavka_date_1c" VisibleIndex="7">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn FieldName="caption_zayavkastatus" VisibleIndex="8" Caption="Статус заявки">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />                
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Z_status_DogovoraTP" VisibleIndex="9" Caption="Статус договора">
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

            <dx:GridViewDataTextColumn FieldName="Z_vid_Oplaty" VisibleIndex="14" Caption="Вариант оплаты" Visible="False">                
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="nomer_dogovorenergo" VisibleIndex="15" Caption="Договор энергоснабж.">                
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
       
     <asp:SqlDataSource ID="SqlDataSourceMyOrders" runat="server" ConnectionString="<%$ ConnectionStrings:perscabnewConnectionString %>" 
         ></asp:SqlDataSource>
</asp:Content>
