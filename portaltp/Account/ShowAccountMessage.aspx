<%@ Page Title="Личный кабинет по ТП" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowAccountMessage.aspx.cs" Inherits="portaltp.Account.ShowAccountMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h2>Восстановление пароля</h2>--%>

    <div class="row">        
        <div class="col-md-8">            
            <asp:PlaceHolder runat="server" ID="MsgForgotPasswordEmailSend" Visible="false">                
                <div>
                    <p class="alert alert-success">На указанный электронный почтовый адрес отправлено письмо с ссылкой на восстановление пароля.</p>
                    <p class="alert alert-danger">Если письмо не пришло, необходимо убедиться, что указанный электронный почтовый адрес правильный. После чего рекомендуется проверить папку «Спам» и/или «Нежелательная почта».</p>
                    <p class="alert alert-warning">На почтовых серверах существует временная задержка доставки писем, поэтому электронные письма могут приходить не сразу. В данной ситуации необходимо дождаться сообщения о подтверждении или повторить процедуру восстановления пароля немного позже.</p>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="MsgConfirmCodeEmailSend" Visible="false">                
                <div>
                    <p class="alert alert-success">На указанный электронный почтовый адрес отправлено письмо с ссылкой для подтверждения адреса электронной почты.</p>
                    <p class="alert alert-danger">Если письмо не пришло, необходимо убедиться, что указанный электронный почтовый адрес правильный. После чего рекомендуется проверить папку «Спам» и/или «Нежелательная почта».</p>
                    <p class="alert alert-warning">На почтовых серверах существует временная задержка доставки писем, поэтому электронные письма могут приходить не сразу. В данной ситуации необходимо дождаться сообщения о подтверждении или повторить процедуру восстановления пароля немного позже.</p>
                </div>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
