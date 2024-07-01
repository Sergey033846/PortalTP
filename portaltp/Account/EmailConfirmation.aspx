<%@ Page Title="Личный кабинет по ТП" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmailConfirmation.aspx.cs" Inherits="portaltp.Account.Confirm" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Подтверждение учетной записи</h2>

    <div>
        <asp:PlaceHolder runat="server" ID="successPanel" ViewStateMode="Disabled" Visible="true">
            <p class="alert alert-success">Учетная запись успешно подтверждена.</p>
            <p>Теперь Вы можете выполнить <asp:HyperLink ID="login" runat="server" NavigateUrl="~/Account/Login">вход в личный кабинет.</asp:HyperLink></p>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="errorPanel" ViewStateMode="Disabled" Visible="false">
            <p class="alert alert-danger">
                Произошла ошибка.
            </p>
        </asp:PlaceHolder>
    </div>
</asp:Content>
