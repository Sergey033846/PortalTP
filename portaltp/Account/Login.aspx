<%@ Page Title="Личный кабинет по ТП" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="portaltp.Account.Login" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Выполнить вход</h2>

    <div class="row">

        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <%--<h4>Используйте для входа локальную учетную запись.</h4>--%>
                    <hr />
                    
                    <p>
                        Введите логин и пароль.                        
                        <br />
                        <a runat="server" href="~/Account/Register">Зарегистрируйтесь</a> в случае их отсутствия.
                    </p>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="tbUserName" CssClass="col-md-2 control-label">Логин (e-mail или телефон)</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="tbUserName" CssClass="form-control inputlogin" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbUserName"
                                CssClass="text-danger" ErrorMessage="Поле адреса электронной почты заполнять обязательно." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="tbPassword" CssClass="col-md-2 control-label">Пароль</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="tbPassword" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPassword" CssClass="text-danger" ErrorMessage="Поле пароля заполнять обязательно." />
                        </div>
                    </div>
                    <!--<div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="RememberMe" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe">Запомнить меня</asp:Label>
                            </div>
                        </div>
                    </div>-->
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="Выполнить вход" CssClass="btn btn-primary" />
                        </div>
                    </div>
                </div>
               
                <p>                    
                    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">Забыли пароль?</asp:HyperLink>                    
                </p>
                
                <br />
                
                <%-- вывод ошибок --%>
                <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="alert alert-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                </asp:PlaceHolder>
            </section>
        </div>

    </div>
</asp:Content>
