<%@ Page Title="Личный кабинет по ТП" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPasswordConfirmation.aspx.cs" Inherits="portaltp.Account.ResetPasswordConfirmation" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Изменение пароля</h2>
    <div class="content">
        <p>Ваш пароль изменен. Теперь Вы можете <asp:HyperLink ID="login" runat="server" NavigateUrl="~/Account/Login">войти</asp:HyperLink> в личный кабинет.</p>
    </div>
</asp:Content>
