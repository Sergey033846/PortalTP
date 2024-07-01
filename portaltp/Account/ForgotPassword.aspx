<%@ Page Title="Личный кабинет по ТП" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="portaltp.Account.ForgotPassword" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Восстановление пароля</h2>

    <div class="row">        
        <div class="col-md-8">
            <asp:PlaceHolder id="loginForm" runat="server">
                <div class="form-horizontal">
                    <%--<h4>Забыли пароль?</h4>--%>
                    <p>Введите адрес электронной почты, указанный Вами при регистрации.</p>
                    <br />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="alert alert-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Адрес электронной почты</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="Поле адреса электронной почты заполнять обязательно." />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="Forgot" Text="Отправить письмо" CssClass="btn btn-primary" ID="ButtonSendEmail" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
            <%-- %><asp:PlaceHolder runat="server" ID="DisplayEmail" Visible="false">                
                <div class="alert alert-success">
                    <p>На указанный электронный почтовый адрес отправлено письмо с ссылкой на восстановление пароля.</p>
                    <p>Если письмо не пришло, необходимо убедиться, что указанный  выше электронный почтовый адрес правильный. После чего рекомендуется проверить папку «Спам» и/или «Нежелательная почта».</p>
                    <p>На почтовых серверах существует временная задержка доставки писем, поэтому электронные письма могут приходить не сразу. В данной ситуации необходимо дождаться сообщения о подтверждении или повторить процедуру восстановления пароля немного позже.</p>
                </div>
            </asp:PlaceHolder>--%>
        </div>
    </div>
</asp:Content>
