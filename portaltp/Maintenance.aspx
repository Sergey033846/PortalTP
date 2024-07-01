<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Maintenance.aspx.cs" Inherits="portaltp.Maintenance" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Личный кабинет ТП</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    
    <%--<link rel="stylesheet" type="text/css" href="Content/SiteMaintenance.css">--%>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <%--<asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />--%>
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>

        <div class="container">

            <div style="text-align: center;">
                <h2>Личный кабинет находится на обслуживании</h2>
                <p>Мы скоро закончим.</p>

                <img src="Content/Images/MaintenanceGallery/ЭнергияДобра.jpg" alt="Энергия добра" class="img-rounded img-responsive center-block" width="600"/>

                <br />
                
                <a href="~/" runat="server">Попробовать снова</a>
            </div>            

            <%--<div id="carousel" class="carousel slide" data-ride="carousel" style="max-width: 800px; margin: 0 auto;">
                <div class="carousel-inner">
                    <div class="item active">
                        <div class="item-responsive item-16by9">
                            <div class="content" style="background: url('Content/Images/MaintenanceGallery/01.jpg')  no-repeat center center;"></div>                            
                         </div>
                    </div>

                    <div class="item">
                        <div class="item-responsive item-16by9">
                            <div class="content" style="background: url('Content/Images/MaintenanceGallery/02.jpg') no-repeat center center;"></div>
                         </div>
                    </div>

                    <div class="item">
                        <div class="item-responsive item-16by9">
                            <div class="content" style="background: url('Content/Images/MaintenanceGallery/03.jpg') no-repeat center center;"></div>
                         </div>
                    </div>
                </div>

                <!-- Элементы управления -->
                <a class="left carousel-control" href="#carousel" role="button" data-slide="prev">
                    <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                    <span class="sr-only">Предыдущий</span>
                </a>
                <a class="right carousel-control" href="#carousel" role="button" data-slide="next">
                    <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                    <span class="sr-only">Следующий</span>
                </a>
            </div>--%>

        </div>
    </form>
</body>
</html>
