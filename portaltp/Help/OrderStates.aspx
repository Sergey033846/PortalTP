<%@ Page Title="Статусы заявки на ТП" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="OrderStates.aspx.cs" Inherits="portaltp.Help.OrderStates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="accountHeader">
        <h2>Статусы заявки на технологическое присоединение</h2>    
    </div>

    <table class="table">        
        <tr>
            <td><span class="label label-primary" style="font-size: small">поступившая</span></td>                                    
            <td>новая заявка, ожидает обработки</td>
        </tr>

        <tr>
            <td><span class="label label-primary" style="font-size: small">на проверке</span></td>                                    
            <td>заявка на технологическое присоединение находится на рассмотрении сотрудниками ОГУЭП «Облкоммунэнерго» на полноту предоставленных документов и сведений</td>
        </tr>

        <tr>
            <td><span class="label label-primary" style="font-size: small">ожидающая исправлений</span></td>                                    
            <td>есть замечания по заявке, не хватает некоторых документов, заявка оформлена некорректно, заявителю направлено соответствующее уведомление</td>
        </tr>
        
        <tr>
            <td><span class="label label-primary" style="font-size: small">ожидание ТР</span></td>                                    
            <td>заявка зарегистрирована, техническое решение находится на стадии подготовки</td>
        </tr>
        
        <tr>
            <td><span class="label label-primary" style="font-size: small">на согласовании</span></td>                                    
            <td>заявка зарегистрирована, подготовлено техническое решение, идет согласование заявки ответственными сотрудниками</td>
        </tr>

        <tr>
            <td><span class="label label-primary" style="font-size: small">создание договора</span></td>                                    
            <td>заявка направлена на подготовку договора</td>
        </tr>

        <tr>
            <td><span class="label label-primary" style="font-size: small">закрытая (отработанная)</span></td>                                    
            <td>по заявке подготовлено  техническое решение, она  согласована всеми ответственными сотрудниками</td>
        </tr>

        <tr>
            <td><span class="label label-primary" style="font-size: small">аннулированная</span></td>                                    
            <td>заявка, аннулированная на основании заявления Заявителя, или непредоставления недостающей информации в установленные сроки</td>
        </tr>

        <tr>            
            <td><span class="label label-primary" style="font-size: small">аннулированная (неоплата)</span></td>                                    
            <td>отсутствие оплаты по договору технологического присоединения со стороны Заявителя в установленные сроки</td>
        </tr>                
    </table>

</asp:Content>
