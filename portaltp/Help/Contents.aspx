<%@ Page Title="Справка" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="Contents.aspx.cs" Inherits="portaltp.Help.Contents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="accountHeader">
        <h2>Термины, используемые в личном кабинете</h2>    
    </div>
       
     <div class="content">
         <p><a runat="server" href="~/Help/OrderStates">Статусы заявки на технологическое присоединение</a></p>
         <p><a runat="server" href="~/Help/ContractStates">Статусы договора на технологическое присоединение</a></p>
    </div>
</asp:Content>
