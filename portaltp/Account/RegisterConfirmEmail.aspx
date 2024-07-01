<%@ Page Title="Личный кабинет по ТП" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterConfirmEmail.aspx.cs" Inherits="portaltp.Account.RegisterConfirmEmail" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Подтверждение учетной записи</h2>
    
    <div class="form-horizontal">
        
        <asp:PlaceHolder runat="server" ID="successPanel" ViewStateMode="Disabled" Visible="false">
            <p class="alert alert-success">Учетная запись успешно подтверждена.</p>
            <p>Теперь Вы можете выполнить <asp:HyperLink ID="login" runat="server" NavigateUrl="~/Account/Login">вход в личный кабинет.</asp:HyperLink></p>
        </asp:PlaceHolder>

        <asp:PlaceHolder runat="server" ID="errorPanel" ViewStateMode="Disabled" Visible="false">
            <p class="alert alert-danger">
                <asp:Literal runat="server" ID="FailureText" Visible="True" />
            </p>
        </asp:PlaceHolder>

    </div>
</asp:Content>
