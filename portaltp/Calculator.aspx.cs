using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portaltp
{
    public partial class Calculator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ASPxPanelCalcTypeStandardTariff.ClientVisible = ASPxPanelCalcTypePowerUnit.ClientVisible = false;
        }

        /*protected void ASPxCallbackPanel2_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
         /*   int cbIndex = (int)comboboxZayavkaType.SelectedItem.Value;

            ASPxPanelTPFirst.Visible = cbIndex == 0; // Первичное присоединение
            ASPxPanelTPUvelichenie.Visible = cbIndex == 1; // увеличение ранее присоединенной мощности   

            lblStoimostLabel.Visible = lblStoimostValue.Visible = false;*/
        //}

        protected void ASPxCallbackPanelLM_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            /*ASPxPanelCalcTypeStandardTariff.ClientVisible = ASPxCheckBoxLastMilya.Checked && String.Equals(ASPxRadioButtonListCalcType.Value.ToString(), "1");
            ASPxPanelCalcTypePowerUnit.ClientVisible = ASPxCheckBoxLastMilya.Checked && String.Equals(ASPxRadioButtonListCalcType.Value.ToString(), "2");*/
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            double priceTP_P1 = 0, priceTP_P2 = 0;

            // P1 - приложение 1 (стандартизированные ставки), P2 - приложение 2 (за единицу мощности)
            double C1_P1 = 14417.38;
            double C21141_P1_gorod_04 = 1090390.00;
            double C21142_P1_gorod_04 = 1057540.00;
            double C21143_P1_gorod_04 = 801920.00;
            double C21231_P1_gorod_04 = 921130.00; double C21231_P1_gorod_1_20 = 3432750.00;
            double C21232_P1_gorod_04 = 2248410.00; double C21232_P1_gorod_1_20 = 1102230.00;
            double C21241_P1_gorod_04 = 1246970.00;
            double C23141_P1_gorod_04 = 1692620.00; double C23141_P1_gorod_1_20 = 1107220.00;
            double C23142_P1_gorod_04 = 1231400.00; double C23142_P1_gorod_1_20 = 2824420.00;
            double C23143_P1_gorod_04 = 1647370.00;
            double C23231_P1_gorod_1_20 = 935550.00;
            double C23232_P1_gorod_1_20 = 1053120.00;
            double C31112_P1_gorod_04 = 2966350.00; double C31112_P1_gorod_1_20 = 2293100.00;
            double C31113_P1_gorod_04 = 0.00;
            double C31211_P1_gorod_04 = 730490.00;
            double C31212_P1_gorod_04 = 1478860.00;
            double C31213_P1_gorod_04 = 3233210.00;
            double C31221_P1_gorod_1_20 = 2116310.00;
            double C31222_P1_gorod_1_20 = 1867560.00;
            double C31223_P1_gorod_04 = 1599570.00; double C31223_P1_gorod_1_20 = 1722800.00;
            double C31224_P1_gorod_04 = 4219980.00; double C31224_P1_gorod_1_20 = 3505140.00;
            double C422_P1_gorod_04 = 252692.71;
            double C425_P1_gorod_1_20 = 33736770.00;
            double C434_P1_gorod_04 = 705222.76;
            double C435_P1_gorod_04 = 1993193.33;
            double C511_P1_gorod_04_6_10 = 13160.30;
            double C512_P1_gorod_04_6_10 = 7252.93;
            double C513_P1_gorod_04_6_10 = 2998.33;
            double C514_P1_gorod_04_6_10 = 1982.41;
            double C515_P1_gorod_04_6_10 = 1164.59;
            double C516_P1_gorod_04_6_10 = 1358.12;
            double C523_P1_gorod_04_6_10 = 14424.45;
            double C524_P1_gorod_04_6_10 = 3861.15;
            double C525_P1_gorod_04_6_10 = 1893.90;
            double C526_P1_gorod_04_6_10 = 1731.56;
            double C612_P1_gorod_04_6_10 = 0.00;
            double C625_P1_gorod_04_6_10 = 2701.72;
            double C72_P1_gorod_6_10_35 = 0.00;
            double C811_P1_gorod_04_bezTT = 17700.45;
            double C821_P1_gorod_04_bezTT = 25650.80; double C821_P1_gorod_1_20 = 188079.80;
            double C822_P1_gorod_04_sTT = 35586.10; double C822_P1_gorod_04_bezTT = 30659.40;
            double C823_P1_gorod_1_20_PS = 104242.50; double C823_P1_gorod_1_20_VL = 306894.00;

            double C21141_P1_negorod_04 = 1269600.00;
            double C21142_P1_negorod_04 = 1173700.00;
            double C21143_P1_negorod_04 = 909200.00;
            double C21231_P1_negorod_04 = 2240100.00;
            double C21232_P1_negorod_1_20 = 3137400.00;
            double C23141_P1_negorod_04 = 1624200.00; double C23141_P1_negorod_1_20 = 1916800.00;
            double C23142_P1_negorod_04 = 1335500.00; double C23142_P1_negorod_1_20 = 1735400.00;
            double C23143_P1_negorod_04 = 1601400.00;
            double C23231_P1_negorod_04 = 4344300.00; double C23231_P1_negorod_1_20 = 2951100.00;
            double C23232_P1_negorod_04 = 2635600.00;
            double C31112_P1_negorod_04 = 0.00; double C31112_P1_negorod_1_20 = 0.00;
            double C31113_P1_negorod_04 = 1616160.00; 
            double C31211_P1_negorod_04 = 0.00;
            double C31212_P1_negorod_04 = 2080430.00;
            double C31213_P1_negorod_04 = 2516990.00;
            double C31221_P1_negorod_1_20 = 0.00;
            double C31222_P1_negorod_1_20 = 0.00;
            double C31223_P1_negorod_04 = 0.00; double C31223_P1_negorod_1_20 = 0.00;
            double C31224_P1_negorod_04 = 0.00; double C31224_P1_negorod_1_20 = 2352730.00;
            double C422_P1_negorod_04 = 0.00;
            double C425_P1_negorod_1_20 = 0.00;
            double C434_P1_negorod_04 = 235554.9;
            double C435_P1_negorod_04 = 0.00;
            double C511_P1_negorod_04_6_10 = 20858.38;
            double C512_P1_negorod_04_6_10 = 7767.09;
            double C513_P1_negorod_04_6_10 = 3282.89;
            double C514_P1_negorod_04_6_10 = 1595.82;
            double C515_P1_negorod_04_6_10 = 1392.25;
            double C516_P1_negorod_04_6_10 = 1780.19;
            double C523_P1_negorod_04_6_10 = 5317.63;
            double C524_P1_negorod_04_6_10 = 2738.58;
            double C525_P1_negorod_04_6_10 = 1624.55;
            double C526_P1_negorod_04_6_10 = 2155.93;
            double C612_P1_negorod_04_6_10 = 4843.07;
            double C625_P1_negorod_04_6_10 = 0.00;
            double C72_P1_negorod_6_10_35 = 8755.37;
            double C811_P1_negorod_04_bezTT = 17700.45;
            double C821_P1_negorod_04_bezTT = 25650.80; double C821_P1_negorod_1_20 = 188079.80;
            double C822_P1_negorod_04_sTT = 35586.10; double C822_P1_negorod_04_bezTT = 30659.40;
            double C823_P1_negorod_04_sTT = 32189.70; double C823_P1_negorod_04_bezTT = 32637.68; double C823_P1_negorod_1_20_PS = 104242.5; double C823_P1_negorod_1_20_VL = 306894.00;

            //---------------------

            double C1_P2 = 515.33;

            double C811_P2_gorod_04_bezTT = 1739.94;
            double C821_P2_gorod_04_bezTT = 1720.6; double C821_P2_gorod_1_20 = 983.42;
            double C822_P2_gorod_04_sTT = 400.97;

            double C811_P2_negorod_04_bezTT = 1739.94;
            double C821_P2_negorod_04_bezTT = 1720.6; double C821_P2_negorod_1_20 = 983.42;
            double C822_P2_negorod_04_sTT = 400.97;
            //-----------------------------------------------------------------------------------------

            int Nmax = Convert.ToInt32(ASPxTextBoxPower.Text);                        
            int N = 1; // количество точек учета
                        
            double C8_P1 = 0;
            if (String.Equals(ASPxRadioButtonListNasPunktType.Value.ToString(), "gorod")) // территория-город
            {
                if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "1f_pryamogo"))
                {
                    C8_P1 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") ? C811_P1_gorod_04_bezTT : 0;
                }

                else if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "3f_pryamogo"))
                {
                    C8_P1 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") ? C821_P1_gorod_04_bezTT : C821_P1_gorod_1_20;
                }

                else if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "3f_polukosv"))
                {
                    C8_P1 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") && String.Equals(ASPxRadioButtonListTT.Value.ToString(), "bezTT") ? C822_P1_gorod_04_bezTT : 0;
                    C8_P1 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") && String.Equals(ASPxRadioButtonListTT.Value.ToString(), "sTT") ? C822_P1_gorod_04_sTT : 0;
                }

                else if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "3f_kosv"))
                {
                    C8_P1 += !String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") && String.Equals(ASPxRadioButtonListMestoUst_1_20.Value.ToString(), "ПС") ? C823_P1_gorod_1_20_PS : 0;
                    C8_P1 += !String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") && String.Equals(ASPxRadioButtonListMestoUst_1_20.Value.ToString(), "ВЛ") ? C823_P1_gorod_1_20_VL : 0;
                }
            }
            else if (String.Equals(ASPxRadioButtonListNasPunktType.Value.ToString(), "negorod")) // территория-не город
            {
                if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "1f_pryamogo"))
                {
                    C8_P1 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") ? C811_P1_negorod_04_bezTT : 0;
                }

                else if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "3f_pryamogo"))
                {
                    C8_P1 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") ? C821_P1_negorod_04_bezTT : C821_P1_negorod_1_20;
                }

                else if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "3f_polukosv"))
                {
                    C8_P1 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") && String.Equals(ASPxRadioButtonListTT.Value.ToString(), "bezTT") ? C822_P1_negorod_04_bezTT : 0;
                    C8_P1 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") && String.Equals(ASPxRadioButtonListTT.Value.ToString(), "sTT") ? C822_P1_negorod_04_sTT : 0;
                }

                else if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "3f_kosv"))
                {
                    C8_P1 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") && String.Equals(ASPxRadioButtonListTT.Value.ToString(), "bezTT") ? C823_P1_negorod_04_bezTT : 0;
                    C8_P1 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") && String.Equals(ASPxRadioButtonListTT.Value.ToString(), "sTT") ? C823_P1_negorod_04_sTT : 0;
                    C8_P1 += !String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") && String.Equals(ASPxRadioButtonListMestoUst_1_20.Value.ToString(), "ПС") ? C823_P1_negorod_1_20_PS : 0;
                    C8_P1 += !String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") && String.Equals(ASPxRadioButtonListMestoUst_1_20.Value.ToString(), "ВЛ") ? C823_P1_negorod_1_20_VL : 0;
                }
            }

            priceTP_P1 = C1_P1 + C8_P1 * N;

            /*if (ASPxCheckBoxLastMilya.Checked) // требуются мероприятия "последней мили"
            {
                priceTP += C2_04 * Convert.ToDouble(ASPxTextBoxVL04.Value) * Zvl + C2_610 * Convert.ToDouble(ASPxTextBoxVL610.Value) * Zvl +
                            C3_04 * Convert.ToDouble(ASPxTextBoxKL04.Value) * Zkl + C3_610 * Convert.ToDouble(ASPxTextBoxKL610.Value) * Zkl +
                            C4 * Convert.ToInt32(ASPxTextBoxTPCountStandardTariff.Text) * powerMAX * Zpr;
            } // if (ASPxCheckBoxLastMilya.Checked) // требуются мероприятия "последней мили"*/

            //--------------------------------

            double C8_P2 = 0;

            if (String.Equals(ASPxRadioButtonListNasPunktType.Value.ToString(), "gorod")) // территория-город
            {
                if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "1f_pryamogo"))
                {
                    C8_P2 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") ? C811_P2_gorod_04_bezTT : 0;
                }

                else if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "3f_pryamogo"))
                {
                    C8_P2 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") ? C821_P2_gorod_04_bezTT : C821_P2_gorod_1_20;
                }

                else if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "3f_polukosv"))
                {                    
                    C8_P2 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") && String.Equals(ASPxRadioButtonListTT.Value.ToString(), "sTT") ? C822_P2_gorod_04_sTT : 0;
                }
            }
            else if (String.Equals(ASPxRadioButtonListNasPunktType.Value.ToString(), "negorod")) // территория-не город
            {
                if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "1f_pryamogo"))
                {
                    C8_P2 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") ? C811_P2_negorod_04_bezTT : 0;
                }

                else if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "3f_pryamogo"))
                {
                    C8_P2 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") ? C821_P2_negorod_04_bezTT : C821_P2_negorod_1_20;
                }

                else if (String.Equals(ASPxRadioButtonListSredstvoKUType.Value.ToString(), "3f_polukosv"))
                {
                    C8_P2 += String.Equals(ASPxRadioButtonListU.Value.ToString(), "0.4kV") && String.Equals(ASPxRadioButtonListTT.Value.ToString(), "sTT") ? C822_P2_negorod_04_sTT : 0;
                }
            }

            priceTP_P2 = (C1_P2 + C8_P2) * Nmax;

            //скопировать посл милю

            priceTP_P1 *= 1.20; // учитываем НДС
            priceTP_P2 *= 1.20; // учитываем НДС
            
            NumberFormatInfo nfi = new CultureInfo("ru-RU", false).NumberFormat;
            nfi.NumberGroupSeparator = " ";
            //lblStoimostValueP1.Text = String.Format(priceTP_P1.ToString("N2", nfi) + " руб.");
            lblStoimostValueP1.Text = String.Concat("по стандартизированным тарифным ставкам - ", priceTP_P1.ToString("N2", nfi), " руб.");
            lblStoimostValueP2.Text = String.Concat("по ставкам за единицу макс. мощности - ", priceTP_P2.ToString("N2", nfi), " руб.");

            lblStoimostLabel.Visible = lblStoimostValueP1.Visible = lblStoimostValueP2.Visible = true;
        }

    }
}