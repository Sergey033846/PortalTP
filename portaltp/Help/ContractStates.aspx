<%@ Page Title="Статусы договора на ТП" Language="C#" MasterPageFile="~/Site.Cabinet.Master" AutoEventWireup="true" CodeBehind="ContractStates.aspx.cs" Inherits="portaltp.Help.ContractStates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="accountHeader">
        <h2>Статусы договора на технологическое присоединение</h2>    
    </div>

    <table class="table">        
        <tr>
            <td>
                <span class="label label-primary" style="font-size: small">согласование</span>
                <span class="label label-primary" style="font-size: small">на подписи ОКЭ</span>
            </td>                                    
            <td>идет процедура согласования договора технологического присоединения ответственными сотрудниками</td>
        </tr>

        <tr>
            <td><span class="label label-primary" style="font-size: small">ожидание оплаты (подписи) клиента</span></td>                                    
            <td>договор технологического присоединения, технические условия, счет на оплату размещены в личном кабинете</td>
        </tr>

        <tr>
            <td><span class="label label-primary" style="font-size: small">исполнение</span></td>                                    
            <td>договор заключен</td>
        </tr>
        
        <tr>
            <td><span class="label label-primary" style="font-size: small">исполнен (закрыт)</span></td>                                    
            <td>мероприятия по договору со стороны сетевой организации выполнены, в личном кабинете размещен акт технологического присоединения</td>
        </tr>
        
        <tr>
            <td><span class="label label-primary" style="font-size: small">аннулирован</span></td>                                    
            <td>договор аннулирован</td>
        </tr>

        <tr>            
            <td><span class="label label-primary" style="font-size: small">аннулирован (неоплата)</span></td>                                    
            <td>отсутствие оплаты по договору технологического присоединения со стороны Заявителя</td>
        </tr>                
    </table>

</asp:Content>
