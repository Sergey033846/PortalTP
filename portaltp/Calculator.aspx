<%@ Page Title="Личный кабинет по ТП" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calculator.aspx.cs" Inherits="portaltp.Calculator" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
    <div class="accountHeader">
        <h2>Расчет предварительной стоимости технологического присоединения</h2>    
    </div>
   
    <%--<dx:ASPxCallbackPanel ID="ASPxCallbackPanel2" runat="server" Width="400px" HideContentOnCallback="True" OnCallback="btnCalculate_Click" ClientInstanceName="CallbackPanelUI2">
    <PanelCollection>
        <dx:PanelContent runat="server">--%>
            <div class="form-field">
                <%--
                <p>Калькулятор расчета платы за технологическое присоединение разработан в соответствии с приказом Службы по тарифам Иркутской области №543-спр от 28.12.2018 г. «Об установлении стандартизированных тарифных ставок, ставок за единицу максимальной мощности, формул платы за технологическое присоединение к электрическим сетям территориальных сетевых организаций Иркутской области на 2019 год.</p>
                --%>
                <p><b>Внимание!</b> Данные расчеты носят справочный характер и не могут являться окончательными.</p>

                <p>Стоимость технологического присоединения составляет 550 рублей при условии, что:</p>
                <ul>
                    <li>расстояние от границ участка заявителя до объектов ОГУЭП «Облкоммунэнерго» составляет менее 300 метров в городах и поселках городского типа и 500 метров в сельской местности</li>
                    <li>размер мощности не превышает 15 кВт включительно (с учетом ранее присоединенных в данной точке присоединения энергопринимающих устройств)</li>
                    <li>технологическое присоединение предусматривается к одному источнику электроснабжения (3-я категория).</li>
                </ul>

                <p>На данной странице Вы можете предварительно оценить стоимость платы за технологическое присоединение к электрическим сетям ОГУЭП «Облкоммунэнерго».</p>

                <dx:ASPxRadioButton ID="ASPxRadioButton1" runat="server" Text="Иркутская область" Checked="True"></dx:ASPxRadioButton>
                <br />

                <dx:ASPxTextBox ID="ASPxTextBoxPower" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="Запрашиваемая максимальная мощность (кВт):" CaptionSettings-Position="Left">
                    <MaskSettings IncludeLiterals="None" Mask="&lt;0..10000&gt;"></MaskSettings>
                    <CaptionSettings Position="Left"></CaptionSettings>
                </dx:ASPxTextBox>
                
                <dx:ASPxRadioButtonList ID="ASPxRadioButtonListU" runat="server" ValueType="System.String" RepeatDirection="Horizontal" SelectedIndex="0" Width="100%" Caption="Уровень напряжения:">
                        <Items>
                            <dx:ListEditItem Selected="True" Text="0.4 кВ" Value="0.4kV" />
                            <dx:ListEditItem Text="1 кВ" Value="1kV" />
                            <dx:ListEditItem Text="6 кВ" Value="6kV" />
                            <dx:ListEditItem Text="10 кВ" Value="10kV" />
                            <dx:ListEditItem Text="20 кВ" Value="20kV" />
                        </Items>
                    <CaptionSettings Position="Top" />
                </dx:ASPxRadioButtonList>

                <dx:ASPxRadioButtonList ID="ASPxRadioButtonListCategoryType" runat="server" ValueType="System.String" RepeatDirection="Horizontal" SelectedIndex="0" Width="100%" Caption="Категория надежности:">
                        <Items>
                            <dx:ListEditItem Selected="True" Text="третья" Value="3" />
                            <%--<dx:ListEditItem Text="вторая" Value="2" />
                            <dx:ListEditItem Text="первая" Value="1" />--%>
                        </Items>
                    <CaptionSettings Position="Top" />
                </dx:ASPxRadioButtonList>
                
                <dx:ASPxRadioButtonList ID="ASPxRadioButtonListNasPunktType" runat="server" ValueType="System.String" RepeatDirection="Horizontal" SelectedIndex="0" Width="100%" Caption="Вид территории:">
                        <Items>
                            <dx:ListEditItem Selected="True" Text="для территорий городских населенных пунктов" Value="gorod" />
                            <dx:ListEditItem Text="для территорий, не относящихся к городским населенным пунктам" Value="negorod" />                            
                        </Items>
                    <CaptionSettings Position="Top" />
                </dx:ASPxRadioButtonList>

                <dx:ASPxRadioButtonList ID="ASPxRadioButtonListSredstvoKUType" runat="server" ValueType="System.String" RepeatDirection="Horizontal" SelectedIndex="0" Width="100%" Caption="Тип средства коммерческого учета:">
                        <Items>
                            <dx:ListEditItem Selected="True" Text="однофазный прямого включения" Value="1f_pryamogo" />
                            <dx:ListEditItem Text="трехфазный прямого включения" Value="3f_pryamogo" />
                            <dx:ListEditItem Text="трехфазный полукосвенного включения" Value="3f_polukosv" />
                            <dx:ListEditItem Text="трехфазный косвенного включения" Value="3f_kosv" />
                        </Items>
                    <CaptionSettings Position="Top" />
                </dx:ASPxRadioButtonList>

                <dx:ASPxRadioButtonList ID="ASPxRadioButtonListTT" runat="server" ValueType="System.String" RepeatDirection="Horizontal" SelectedIndex="0" Width="100%" Caption="Требуются трансформаторы тока (ТТ):">
                        <Items>
                            <dx:ListEditItem Selected="True" Text="без ТТ" Value="bezTT" />
                            <dx:ListEditItem Text="с ТТ" Value="sTT" />                            
                        </Items>
                    <CaptionSettings Position="Top" />
                </dx:ASPxRadioButtonList>

                <dx:ASPxRadioButtonList ID="ASPxRadioButtonListMestoUst_1_20" runat="server" ValueType="System.String" RepeatDirection="Horizontal" SelectedIndex="0" Width="100%" Caption="Место установки ПУ (для 1-20 кВ):">
                        <Items>
                            <dx:ListEditItem Selected="True" Text="ПС" Value="ПС" />
                            <dx:ListEditItem Text="ВЛ" Value="ВЛ" />                            
                        </Items>
                    <CaptionSettings Position="Top" />
                </dx:ASPxRadioButtonList>

                <%--<dx:ASPxCallbackPanel ID="ASPxCallbackPanelLastMilyaParams" runat="server" Width="100%" HideContentOnCallback="True" OnCallback="ASPxCallbackPanelLM_Callback" ClientInstanceName="CallbackPanelLMParams">
                <PanelCollection>
                <dx:PanelContent runat="server">                    
                    <dx:ASPxRadioButtonList ID="ASPxRadioButtonListCalcType" runat="server" ValueType="System.String" RepeatDirection="Vertical" SelectedIndex="0" Width="100%" Caption="Вид расчета:" ClientInstanceName="CI_RadioButtonCalcType">
                        <ClientSideEvents SelectedIndexChanged=
                            "function(s, e) {
                            var item = CI_RadioButtonCalcType.GetSelectedItem();
                            CallbackPanelLMParams.PerformCallback(item.value); }" />
                        <Items>
                            <dx:ListEditItem Selected="True" Text="по стандартизированным тарифным ставкам" Value="1" />
                            <dx:ListEditItem Text="по ставкам за единицу максимальной мощности" Value="2" />
                        </Items>
                        <CaptionSettings Position="Top" />
                    </dx:ASPxRadioButtonList>
                    <br />

                    <dx:ASPxCheckBox ID="ASPxCheckBoxLastMilya" runat="server" Text="Необходима реализация мероприятий «последней мили»" ClientInstanceName="CI_CheckBoxIsLastMilya">
                        <ClientSideEvents CheckedChanged=
                        "function(s, e) {
                            var value = CI_CheckBoxIsLastMilya.GetChecked();
                            CallbackPanelLMParams.PerformCallback(value); }" />
                    </dx:ASPxCheckBox>
                    <br />            
                                        
                    <dx:ASPxPanel ID="ASPxPanelCalcTypeStandardTariff" runat="server" Width="100%">
                    <PanelCollection>
                    <dx:PanelContent runat="server">                                  
                        <dx:ASPxTextBox ID="ASPxTextBox_C21141_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="воздушные линии на деревянных опорах изолированным алюминиевым проводом сечением до 50 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C21142_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="воздушные линии на деревянных опорах изолированным алюминиевым проводом сечением от 50 до 100 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C21143_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="воздушные линии на деревянных опорах изолированным алюминиевым проводом сечением от 100 до 200 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C21231_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="воздушные линии на деревянных опорах неизолированным сталеалюминиевым проводом сечением до 50 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C21232_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="воздушные линии на деревянных опорах неизолированным сталеалюминиевым проводом сечением от 50 до 100 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C21241_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="воздушные линии на деревянных опорах неизолированным алюминиевым проводом сечением до 50 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>

                        <dx:ASPxTextBox ID="ASPxTextBox_C23141_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="воздушные линии на железобетонных опорах изолированным алюминиевым проводом сечением до 50 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C23142_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="воздушные линии на железобетонных опорах изолированным алюминиевым проводом сечением от 50 до 100 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C23143_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="воздушные линии на железобетонных опорах изолированным алюминиевым проводом сечением от 100 до 200 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C23231_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="воздушные линии на железобетонных опорах неизолированным сталеалюминиевым проводом сечением до 50 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C23232_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="воздушные линии на железобетонных опорах неизолированным сталеалюминиевым проводом сечением от 50 до 100 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>

                        <dx:ASPxTextBox ID="ASPxTextBox_C31112_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="кабельные линии в траншеях одножильные с резиновой или пластмассовой изоляцией сечением провода от 50 до 100 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C31211_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="кабельные линии в траншеях многожильные с резиновой или пластмассовой изоляцией сечением провода до 50 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C31212_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="кабельные линии в траншеях многожильные с резиновой или пластмассовой изоляцией сечением провода от 50 до 100 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C31213_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="кабельные линии в траншеях многожильные с резиновой или пластмассовой изоляцией сечением провода от 100 до 200 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C31221_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="кабельные линии в траншеях многожильные с бумажной изоляцией сечением провода до 50 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C31222_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="кабельные линии в траншеях многожильные с бумажной изоляцией сечением провода от 50 до 100 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C31223_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="кабельные линии в траншеях многожильные с бумажной изоляцией сечением провода от 100 до 200 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C31224_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="кабельные линии в траншеях многожильные с бумажной изоляцией сечением провода от 200 до 500 квадратных мм включительно (км):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10000>.<0..99>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>         
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                   
                        </dx:ASPxTextBox>

                        <dx:ASPxTextBox ID="ASPxTextBox_C422_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="распределительные пункты номинальным током от 100 до 250А включительно (шт.):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>      
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                      
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C425_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="распределительные пункты номинальным током свыше 1000А (шт.):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>      
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                      
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C434_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="переключательные пункты номинальным током от 500 до 1000А включительно (шт.):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>      
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                      
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="ASPxTextBox_C435_P1" runat="server" Width="100px" MaskSettings-IncludeLiterals="None" Caption="переключательные пункты номинальным током свыше 1000 А (шт.):" CaptionSettings-Position="Left">
	                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..10>"></MaskSettings>
	                        <CaptionSettings Position="Left"></CaptionSettings>      
	                        <CaptionCellStyle Width="270px"></CaptionCellStyle>                      
                        </dx:ASPxTextBox>

                        <dx:ASPxCheckBox ID="ASPxCheckBox_C511_P1" runat="server" Text="однотрансформаторные подстанции (за исключением РТП) мощностью до 25 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C512_P1" runat="server" Text="однотрансформаторные подстанции (за исключением РТП) мощностью от 25 до 100 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C513_P1" runat="server" Text="однотрансформаторные подстанции (за исключением РТП) мощностью от 100 до 250 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C514_P1" runat="server" Text="однотрансформаторные подстанции (за исключением РТП) мощностью от 250 до 400 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C515_P1" runat="server" Text="однотрансформаторные подстанции (за исключением РТП) мощностью от 420 до 1000 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C516_P1" runat="server" Text="однотрансформаторные подстанции (за исключением РТП) мощностью свыше 1000 кВА"></dx:ASPxCheckBox>

                        <dx:ASPxCheckBox ID="ASPxCheckBox_C523_P1" runat="server" Text="двухтрансформаторные и более подстанции (за исключением РТП) мощностью от 100 до 250 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C524_P1" runat="server" Text="двухтрансформаторные и более подстанции (за исключением РТП) мощностью от 250 до 400 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C525_P1" runat="server" Text="двухтрансформаторные и более подстанции (за исключением РТП) мощностью от 420 до 1000 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C526_P1" runat="server" Text="двухтрансформаторные и более подстанции (за исключением РТП) мощностью свыше 1000 кВА"></dx:ASPxCheckBox>

                        <dx:ASPxCheckBox ID="ASPxCheckBox_C625_P1" runat="server" Text="распределительные двухтрансформаторные подстанции мощностью от 420 до 1000 кВА включительно"></dx:ASPxCheckBox>
                    </dx:PanelContent>
                    </PanelCollection>
                    </dx:ASPxPanel>
                                
                    <dx:ASPxPanel ID="ASPxPanelCalcTypePowerUnit" runat="server" Width="100%">
                    <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C21141_P2" runat="server" Text="воздушные линии на деревянных опорах изолированным алюминиевым проводом сечением до 50 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C21142_P2" runat="server" Text="воздушные линии на деревянных опорах изолированным алюминиевым проводом сечением от 50 до 100 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C21143_P2" runat="server" Text="воздушные линии на деревянных опорах изолированным алюминиевым проводом сечением от 100 до 200 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C21231_P2" runat="server" Text="воздушные линии на деревянных опорах неизолированным сталеалюминиевым проводом сечением до 50 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C21232_P2" runat="server" Text="воздушные линии на деревянных опорах неизолированным сталеалюминиевым проводом сечением от 50 до 100 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C21241_P2" runat="server" Text="воздушные линии на деревянных опорах неизолированным алюминиевым проводом сечением до 50 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C23141_P2" runat="server" Text="воздушные линии на железобетонных опорах изолированным алюминиевым проводом сечением до 50 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C23142_P2" runat="server" Text="воздушные линии на железобетонных опорах изолированным алюминиевым проводом сечением от 50 до 100 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C23143_P2" runat="server" Text="воздушные линии на железобетонных опорах изолированным алюминиевым проводом сечением от 100 до 200 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C23231_P2" runat="server" Text="воздушные линии на железобетонных опорах неизолированным сталеалюминиевым проводом сечением до 50 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C23232_P2" runat="server" Text="воздушные линии на железобетонных опорах неизолированным сталеалюминиевым проводом сечением от 50 до 100 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C31112_P2" runat="server" Text="кабельные линии в траншеях одножильные с резиновой или пластмассовой изоляцией сечением провода от 50 до 100 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C31211_P2" runat="server" Text="кабельные линии в траншеях многожильные с резиновой или пластмассовой изоляцией сечением провода до 50 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C31212_P2" runat="server" Text="кабельные линии в траншеях многожильные с резиновой или пластмассовой изоляцией сечением провода от 50 до 100 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C31213_P2" runat="server" Text="кабельные линии в траншеях многожильные с резиновой или пластмассовой изоляцией сечением провода от 100 до 200 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C31221_P2" runat="server" Text="кабельные линии в траншеях многожильные с бумажной изоляцией сечением провода до 50 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C31222_P2" runat="server" Text="кабельные линии в траншеях многожильные с бумажной изоляцией сечением провода от 50 до 100 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C31223_P2" runat="server" Text="кабельные линии в траншеях многожильные с бумажной изоляцией сечением провода от 100 до 200 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C31224_P2" runat="server" Text="кабельные линии в траншеях многожильные с бумажной изоляцией сечением провода от 200 до 500 квадратных мм включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C422_P2" runat="server" Text="распределительные пункты номинальным током от 100 до 250А включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C434_P2" runat="server" Text="переключательные пункты номинальным током от 500 до 1000А включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C435_P2" runat="server" Text="переключательные пункты номинальным током свыше 1000 А"></dx:ASPxCheckBox>

                        <dx:ASPxCheckBox ID="ASPxCheckBox_C511_P2" runat="server" Text="однотрансформаторные подстанции (за исключением РТП) мощностью до 25 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C512_P2" runat="server" Text="однотрансформаторные подстанции (за исключением РТП) мощностью от 25 до 100 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C513_P2" runat="server" Text="однотрансформаторные подстанции (за исключением РТП) мощностью от 100 до 250 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C514_P2" runat="server" Text="однотрансформаторные подстанции (за исключением РТП) мощностью от 250 до 400 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C515_P2" runat="server" Text="однотрансформаторные подстанции (за исключением РТП) мощностью от 420 до 1000 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C516_P2" runat="server" Text="однотрансформаторные подстанции (за исключением РТП) мощностью свыше 1000 кВА"></dx:ASPxCheckBox>

                        <dx:ASPxCheckBox ID="ASPxCheckBox_C523_P2" runat="server" Text="двухтрансформаторные и более подстанции (за исключением РТП) мощностью от 100 до 250 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C524_P2" runat="server" Text="двухтрансформаторные и более подстанции (за исключением РТП) мощностью от 250 до 400 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C525_P2" runat="server" Text="двухтрансформаторные и более подстанции (за исключением РТП) мощностью от 420 до 1000 кВА включительно"></dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox_C526_P2" runat="server" Text="двухтрансформаторные и более подстанции (за исключением РТП) мощностью свыше 1000 кВА"></dx:ASPxCheckBox>

                        <dx:ASPxCheckBox ID="ASPxCheckBox_C625_P2" runat="server" Text="распределительные двухтрансформаторные подстанции мощностью от 420 до 1000 кВА включительно"></dx:ASPxCheckBox>          
                    </dx:PanelContent>
                    </PanelCollection>
                    </dx:ASPxPanel>
                                
                </dx:PanelContent>
                </PanelCollection>
                </dx:ASPxCallbackPanel>--%>

                <br />
                <dx:ASPxButton ID="btnCalculate" runat="server" Text="Рассчитать стоимость ТП" ClientInstanceName="CI_CalcButton" AutoPostBack="False" CssClass="btn btn-primary">
                    <ClientSideEvents Click=
                        "function(s, e) {
	                        CI_CallbackPanelCalcValues.PerformCallback();
                         }" />
                </dx:ASPxButton>

                <br />
                <br />

                <dx:ASPxCallbackPanel ID="ASPxCallbackPanelCalcValues" runat="server" Width="100%" HideContentOnCallback="True" OnCallback="btnCalculate_Click" ClientInstanceName="CI_CallbackPanelCalcValues">
                <PanelCollection>
                <dx:PanelContent runat="server">  
                    <dx:ASPxLabel ID="lblStoimostLabel" runat="server" Text="Итого с НДС: " Visible="False" />
                    <br />
                    <dx:ASPxLabel ID="lblStoimostValueP1" runat="server" Text="" Visible="False" Font-Bold="True" ForeColor="Black" />     
                    <br />
                    <dx:ASPxLabel ID="lblStoimostValueP2" runat="server" Text="" Visible="False" Font-Bold="True" ForeColor="Black" />     
                </dx:PanelContent>
                </PanelCollection>
                </dx:ASPxCallbackPanel>
            </div>
    <%--</dx:PanelContent>
    </PanelCollection>
    </dx:ASPxCallbackPanel>--%>

    </div>
</asp:Content>
