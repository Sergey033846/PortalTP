<%@ Page Title="Регистрация заявителя" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="portaltp.Account.Register" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
    <div class="accountHeader">
        <h2>Регистрация заявителя</h2>
    <%--<p>Passwords are required to be a minimum of 6 characters in length.</p>--%>
    </div>   

    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="400px" HideContentOnCallback="True" OnCallback="ASPxCallbackPanel1_Callback" ClientInstanceName="CallbackPanelUI">
        <PanelCollection>
            <dx:PanelContent runat="server">
                <div class="form-field">
                    <dx:ASPxLabel ID="lblUserType" runat="server" AssociatedControlID="tbFamiliya" Text="Тип заявителя:" />
                    <dx:ASPxComboBox ID="comboboxUserType" runat="server" SelectedIndex="0" Width="250px" ValueType="System.Int32" AutoPostBack="False" ClientInstanceName="comboboxUT" DropDownStyle="DropDownList">
                        <ClientSideEvents SelectedIndexChanged=
                            "function(s, e) {
                                var item = comboboxUT.GetSelectedItem();
                                CallbackPanelUI.PerformCallback(item.value); }" />
                        <Items>
                            <dx:ListEditItem Selected="True" Text="Физическое лицо" Value="0" />
                            <dx:ListEditItem Text="Юридическое лицо" Value="1" />
                            <dx:ListEditItem Text="Индивидуальный предприниматель" Value="2" />
                        </Items>
                        <ClearButton DisplayMode="Never">
                        </ClearButton>
                    </dx:ASPxComboBox>
                </div>
                <%-- блок ввода для ФИО ФЛ и ИП --%>
                <div id="divUserInfoFIO">
                    <dx:ASPxLabel ID="lblFamiliya" runat="server" AssociatedControlID="tbFamiliya" Text="Фамилия заявителя:" />
                    <div class="form-field">
                        <dx:ASPxTextBox ID="tbFamiliya" runat="server" Width="250px">
                            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                                <RequiredField ErrorText="обязательное поле" IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                    <dx:ASPxLabel ID="lblName" runat="server" AssociatedControlID="tbName" Text="Имя заявителя:" />
                    <div class="form-field">
                        <dx:ASPxTextBox ID="tbName" runat="server" Width="250px">
                            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                                <RequiredField ErrorText="обязательное поле" IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                    <dx:ASPxLabel ID="lblOtchestvo" runat="server" AssociatedControlID="tbOtchestvo" Text="Отчество заявителя:" />
                    <div class="form-field">
                        <dx:ASPxTextBox ID="tbOtchestvo" runat="server" Width="250px">
                            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                                <RequiredField ErrorText="обязательное поле" IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                    
                </div>
                <%-- блок ввода для ЮЛ --%>
                <div id="divUserInfoYL">
                    <dx:ASPxLabel ID="lblFullName" runat="server" AssociatedControlID="tbYLFullName" Text="Полное наименование:" Visible="False" />
                    <div class="form-field">
                        <dx:ASPxTextBox ID="tbYLFullName" runat="server" Width="250px" Visible="False">
                            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                                <RequiredField ErrorText="обязательное поле" IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                    <dx:ASPxLabel ID="lblShortName" runat="server" AssociatedControlID="tbYLShortName" Text="Сокращенное наименование:" Visible="False" />
                    <div class="form-field">
                        <dx:ASPxTextBox ID="tbYLShortName" runat="server" Width="250px" Visible="False">
                            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                                <RequiredField ErrorText="обязательное поле" IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                    <dx:ASPxLabel ID="lblINN" runat="server" AssociatedControlID="tbINN" Text="ИНН:" Visible="False" />
                    <div class="form-field">
                        <dx:ASPxTextBox ID="tbINN" runat="server" Width="250px" Visible="False">
                            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                                <RequiredField ErrorText="обязательное поле" IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <dx:ASPxLabel ID="lblEmail" runat="server" AssociatedControlID="tbEmail" Text="e-mail:" />
                <div class="form-field">
                    <dx:ASPxTextBox ID="tbEmail" runat="server" Width="250px">
                        <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                            <RequiredField ErrorText="обязательное поле" IsRequired="true" />
                            <RegularExpression ErrorText="некорректный адрес" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
                <dx:ASPxLabel ID="lblPhone" runat="server" AssociatedControlID="tbPhone" Text="Контактный телефон:" />
                <div class="form-field">
                    <dx:ASPxTextBox ID="tbPhone" runat="server" Width="250px" MaskSettings-ShowHints="False" MaskSettings-Mask="+70000000000">
                        <MaskSettings Mask="+70000000000">
                        </MaskSettings>
                        <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                            <RequiredField ErrorText="обязательное поле" IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
                <%-- блок ввода для ФИО контактного лица в ЮЛ и ИП --%>
                <div id="divUserInfoContact">
                    <dx:ASPxLabel ID="lblContactGroupCaption" runat="server" Text="Контактное лицо:" Font-Bold="True" Visible="False" />
                    <div>
                        <p>
                        </p>
                    </div>
                    <dx:ASPxLabel ID="lblContactFamiliya" runat="server" AssociatedControlID="tbContactFamiliya" Text="Фамилия:" Visible="False" />
                    <div class="form-field">
                        <dx:ASPxTextBox ID="tbContactFamiliya" runat="server" Width="250px" Visible="False">
                            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                                <RequiredField ErrorText="обязательное поле" IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                    <dx:ASPxLabel ID="lblContactName" runat="server" AssociatedControlID="tbContactName" Text="Имя:" Visible="False" />
                    <div class="form-field">
                        <dx:ASPxTextBox ID="tbContactName" runat="server" Width="250px" Visible="False">
                            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                                <RequiredField ErrorText="обязательное поле" IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                    <dx:ASPxLabel ID="lblContactOtchestvo" runat="server" AssociatedControlID="tbContactOtchestvo" Text="Отчество:" Visible="False" />
                    <div class="form-field">
                        <dx:ASPxTextBox ID="tbContactOtchestvo" runat="server" Width="250px" Visible="False">
                            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                                <RequiredField ErrorText="обязательное поле" IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </div>
                </div>
                <dx:ASPxLabel ID="lblPassword" runat="server" AssociatedControlID="tbPassword" Text="Пароль (не менее 10 символов):" />
                <div class="form-field">
                    <dx:ASPxTextBox ID="tbPassword" ClientInstanceName="Password" Password="true" runat="server" Width="250px" MaxLength="19">
                        <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                            <RequiredField ErrorText="обязательное поле" IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
                <dx:ASPxLabel ID="lblConfirmPassword" runat="server" AssociatedControlID="tbConfirmPassword" Text="Подтвердите пароль:" />
                <div class="form-field">
                    <dx:ASPxTextBox ID="tbConfirmPassword" Password="true" runat="server" Width="250px" MaxLength="19">
                        <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                            <RequiredField ErrorText="подтверждение пароля обязательно!" IsRequired="true" />
                        </ValidationSettings>
                        <ClientSideEvents Validation="function(s, e) {
                            var originalPasswd = Password.GetText();
                            var currentPasswd = s.GetText();
                            e.isValid = (originalPasswd  == currentPasswd );
                            e.errorText = 'Пароли должны совпадать!';
                        }" />
                    </dx:ASPxTextBox>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    
    <br />

    <div style="width: 100%">
        <p>В соответствии с требованиями статьи 9 Федерального закона от 27.07.2006 №152-ФЗ «О персональных данных» заявитель подтверждает свое согласие на обработку своих персональных данных, включающих: фамилию, имя, отчество, паспортные данные, адрес проживания, контактный телефон. Заявитель предоставляет Оператору право осуществлять действия (операции) со своими персональными данными в объеме, необходимом для формирования документов на технологическое присоединение. Оператор имеет право во исполнение своих обязательств на обмен (прием и передачу) персональных данных заявителя между своими информационными системами с использованием машинных носителей или по открытым каналам связи. Передача персональных данных заявителя иным лицам или иное их разглашение может осуществляться только с письменного согласия заявителя. Настоящее согласие дано заявителем и действует на весь срок подготовки документов на технологическое присоединение к сетям ОГУЭП «Облкоммунэнерго»</p>
        
        <dx:ASPxCheckBox ID="checkBoxFZ152" runat="server" Text="Заявитель согласен на обработку своих персональных данных" Width="100%">
            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                <RequiredField ErrorText="необходимо подтверждение" IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxCheckBox>
        
        <div></div>
        
        <dx:ASPxCheckBox ID="checkBoxPersData" runat="server" Text="Заявитель согласен на пересылку своих персональных данных по открытым каналам передачи данных" Width="100%">
            <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
                <RequiredField ErrorText="необходимо подтверждение" IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxCheckBox>
    </div>

    <br />
     
    <dx:ASPxButton ID="btnCreateUser" runat="server" Text="Зарегистрироваться" ValidationGroup="RegisterUserValidationGroup" OnClick="btnCreateUser_Click" CssClass="btn btn-primary"></dx:ASPxButton>
     
    </div>
</asp:Content>
