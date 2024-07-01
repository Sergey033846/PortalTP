using DatabaseFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace mcperscab
{
    public class MC_PersCab_Notify
    {
        // добавление уведомления в БД
        public static void AddNotify(Guid id_notifytype, Guid? link_id, int priority, Guid? recipient_id, Guid? id_notifychannel, bool uniqueNotifyKeep)
        {
            perscabnewEntities lkAdapter = new perscabnewEntities();

            bool notifyWithThisNotifyTypeAlreadeExists = false;
            if (uniqueNotifyKeep)
                notifyWithThisNotifyTypeAlreadeExists = lkAdapter.tblNotify.Where(p => p.link_id == link_id && p.id_notifytype == id_notifytype).Select(p => p).Any();

            if (!notifyWithThisNotifyTypeAlreadeExists)
            {
                tblNotify notifyNew = new tblNotify();
                notifyNew.id_notify = Guid.NewGuid();

                notifyNew.id_notifytype = id_notifytype;
                notifyNew.link_id = link_id;
                notifyNew.priority = priority;
                notifyNew.recipient_id = recipient_id;
                notifyNew.id_notifychannel = id_notifychannel;

                //notifyNew.notify_text = null;
                notifyNew.notify_text = GenerateNotifyBodyText(id_notifytype, link_id, id_notifychannel);

                notifyNew.date_create_notify = DateTime.Now;
                                
                notifyNew.notifystatus_id = MC_PersCab_Consts.notifyStatus_VOcheredi;

                lkAdapter.tblNotify.Add(notifyNew);

                lkAdapter.SaveChanges();
            }
        } // public static void AddNotify(Guid id_notifytype, Guid? link_id, int priority, Guid? recipient_id, Guid? id_notifychannel, bool uniqueNotifyKeep)

        // формирование темы сообщения
        public static string GenerateMailSubject(Guid id_notifytype)
        {
            string mailSubject = String.Empty;

            if (id_notifytype == MC_PersCab_Consts.notifyType_GP_RegNewZ) mailSubject = "Уведомление для ИЭСБК о регистрации заявки в Личном кабинете ОГУЭП Облкоммунэнерго";
            else if (id_notifytype == MC_PersCab_Consts.notifyType_GP_RegNewDTP) mailSubject = "Уведомление для ИЭСБК о размещении договора на ТП в Личном кабинете ОГУЭП Облкоммунэнерго";
            else if (id_notifytype == MC_PersCab_Consts.notifyType_GP_AktDopuskaPU) mailSubject = "Уведомление для ИЭСБК о размещении Акта допуска ПУ в Личном кабинете ОГУЭП Облкоммунэнерго";
            else if (id_notifytype == MC_PersCab_Consts.notifyType_GP_AktTP) mailSubject = "Уведомление для ИЭСБК о размещении Акта об осуществлении ТП в Личном кабинете ОГУЭП Облкоммунэнерго";
            else if (id_notifytype == MC_PersCab_Consts.notifyType_GP_ZAnnulirovana) mailSubject = "Уведомление для ИЭСБК об аннулировании заявки в Личном кабинете ОГУЭП Облкоммунэнерго";
            //-------------------------------------------------------------
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Z_ZAnnulirovana) mailSubject = "Уведомление для Заявителя об аннулировании заявки в Личном кабинете ОГУЭП Облкоммунэнерго";
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Z_AktDopuskaPU) mailSubject = "Уведомление для Заявителя о размещении Акта допуска ПУ в Личном кабинете ОГУЭП Облкоммунэнерго";
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Z_AktTP) mailSubject = "Уведомление для Заявителя о размещении Акта об осуществлении ТП в Личном кабинете ОГУЭП Облкоммунэнерго";
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Z_DogovorTP) mailSubject = "Уведомление для Заявителя о размещении договора и выставлении счета в Личном кабинете ОГУЭП Облкоммунэнерго";
            //-------------------------------------------------------------
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Sys_ChatFromZayavitel) mailSubject = "Личный кабинет ТП (поступило новое сообщение)";
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Sys_ChatToZayavitel) mailSubject = "Личный кабинет ТП (поступило новое сообщение)";
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Sys_NewZ) mailSubject = "Личный кабинет ТП (поступила новая заявка)";
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Sys_ZakreplenFilial) mailSubject = "Личный кабинет ТП (закреплена новая заявка)";

            return mailSubject;
        } // public static string GenerateMailSubject(Guid id_notifytype)

        // формирование текста сообщения
        public static string GenerateNotifyBodyText(Guid id_notifytype, Guid? link_id, Guid? id_notifychannel)
        {
            string notifyText = String.Empty;

            perscabnewEntities lkAdapter = new perscabnewEntities();

            tblZayavka zayavkaTemp = null;
            tblZayavkaChat userMessage = null; ;

            string nomerD = "";
            string nomerLS = "";
            string urovenUstr = "";

            if (id_notifytype == MC_PersCab_Consts.notifyType_GP_RegNewZ || id_notifytype == MC_PersCab_Consts.notifyType_GP_RegNewDTP ||
            id_notifytype == MC_PersCab_Consts.notifyType_GP_AktDopuskaPU || id_notifytype == MC_PersCab_Consts.notifyType_GP_AktTP || id_notifytype == MC_PersCab_Consts.notifyType_GP_ZAnnulirovana ||
            id_notifytype == MC_PersCab_Consts.notifyType_Z_ZAnnulirovana || id_notifytype == MC_PersCab_Consts.notifyType_Z_AktTP || id_notifytype == MC_PersCab_Consts.notifyType_Z_AktDopuskaPU || id_notifytype == MC_PersCab_Consts.notifyType_Z_DogovorTP ||
            id_notifytype == MC_PersCab_Consts.notifyType_Sys_NewZ || id_notifytype == MC_PersCab_Consts.notifyType_Sys_ZakreplenFilial)
            {
                zayavkaTemp = lkAdapter.tblZayavka.Where(p => p.id_zayavka == link_id).Select(p => p).First();
                                
                if (lkAdapter.tblGP_DogovorEnergo.Where(p => String.Equals(p.id_dogovorenergo.ToString(), zayavkaTemp.id_zayavka.ToString())).Select(p => p).Count() == 1)
                {
                    tblGP_DogovorEnergo dogovorEnergoTemp =
                        lkAdapter.tblGP_DogovorEnergo.Where(p => String.Equals(p.id_dogovorenergo.ToString(), zayavkaTemp.id_zayavka.ToString())).Select(p => p).First();

                    nomerD = dogovorEnergoTemp.nomer_dogovorenergo;
                    nomerLS = dogovorEnergoTemp.nomer_ls;
                }

                if (zayavkaTemp.id1C_EnumUrovenU != null)
                    urovenUstr = lkAdapter.tbl1C_EnumUrovenU.Where(p => p.C_IDRref == zayavkaTemp.id1C_EnumUrovenU).Select(p => p).First().caption_short;
            }
            else 
            if (id_notifytype == MC_PersCab_Consts.notifyType_Sys_ChatFromZayavitel || id_notifytype == MC_PersCab_Consts.notifyType_Sys_ChatToZayavitel)                
            {
                userMessage = lkAdapter.tblZayavkaChat.Where(p => p.id_chatrec == link_id).Select(p => p).First();
                zayavkaTemp = lkAdapter.tblZayavka.Where(p => p.id_zayavka == userMessage.id_zayavka).Select(p => p).First();
            }

            if (id_notifytype == MC_PersCab_Consts.notifyType_GP_RegNewZ)
            {
                /*notifyText = String.Concat(
                    "<p>ОГУЭП Облкоммунэнерго уведомляет Вас о том, что в целях исполнения п 105 ПП РФ 861 (в редакции ПП РФ 262) в личном кабинете зарегистрирована новая заявка.</p>",
                    //"<p>Заявитель: ", zayavka1c["_Fld26191"].ToString(), " [физическое лицо / индивидуальный предприниматель / юридическое лицо]. Номер заявки: ", zayavkaNew.zayavka_number_1c, ". Дата заявки: ", zayavkaNew.zayavka_date_1c.ToString(), ".</p>",                        
                    String.Format("<p>Заявитель: <b>{0}</b>. Номер заявки: <b>{1}</b>. Дата заявки: <b>{2:d}</b>.</p>", zayavkaTemp.v1C_Zayavitel, zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                    String.Format("<p>Наименование энергопринимающего устройства: <b>{0}</b>. Место нахождения энергопринимающего устройства: <b>{1}</b>. Мощность Рmax: <b>{2}</b>. Уровень напряжения: <b>{3}</b>.</p>", zayavkaTemp.v1C_EPU, zayavkaTemp.v1C_adresEPU, zayavkaTemp.v1C_maxMoschnostEPU, urovenUstr),
                    "<p>Просим разместить в личном кабинете заявителя в течение 10 рабочих дней:</p>",
                    "<p>- проект договора, обеспечивающего продажу электрической энергии(мощности) на розничном рынке, подписанный усиленной квалифицированной электронной подписью уполномоченного лица гарантирующего поставщика</p>",
                    "<p>- наименование и платежные реквизиты гарантирующего поставщика, а также информацию о номере лицевого счета заявителя в случае если заявителем выступает физическое лицо в целях технологического присоединения энергопринимающих устройств, которые используются для бытовых и иных нужд, не связанных с осуществлением предпринимательской деятельности, и электроснабжение которых предусматривается по одному источнику</p>");
                    */
                notifyText = String.Concat(
                    //String.Format("<tr><td>{0}</td><td>{1}</td><td>{2:d}</td>", zayavkaTemp.v1C_Zayavitel, zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                    String.Format("<tr><td>{0}</td><td>{1}</td><td>{2:d}</td>", 
                        String.Format("<a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/OrderInfo?id={0}\">{1}</a>", zayavkaTemp.id_zayavka.ToString(), zayavkaTemp.v1C_Zayavitel), 
                        zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                    String.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", zayavkaTemp.v1C_EPU, zayavkaTemp.v1C_adresEPU, zayavkaTemp.v1C_maxMoschnostEPU, urovenUstr)
                    );
                                    
            }
            else if (id_notifytype == MC_PersCab_Consts.notifyType_GP_RegNewDTP)
            {
                /*DateTime? dataDogovoraTP = zayavkaTemp.v1C_DataDogovora;
                if (dataDogovoraTP < Convert.ToDateTime("01.01.2001")) dataDogovoraTP = null;*/
                /*notifyText = String.Concat(
                    "<p>ОГУЭП Облкоммунэнерго уведомляет Вас о том, что в целях исполнения п 105 ПП РФ 861 (в редакции ПП РФ 262) в личном кабинете заявителя размещен договор об осуществлении технологического присоединения.</p>",
                    //String.Format("<p>Заявитель: <b>{0}</b>. Номер договора: <b>{1}</b>. Дата договора: <b>{2:d}</b>.</p>", zayavkaTemp.v1C_Zayavitel, zayavkaTemp.v1C_NomerDogovora, dataDogovoraTP),
                    String.Format("<p>Заявитель: <b>{0}</b>. Номер договора: <b>{1}</b>.</p>", zayavkaTemp.v1C_Zayavitel, zayavkaTemp.v1C_NomerDogovora),
                    String.Format("<p>Наименование энергопринимающего устройства: <b>{0}</b>. Место нахождения энергопринимающего устройства: <b>{1}</b>. Мощность Рmax: <b>{2}</b>. Уровень напряжения: <b>{3}</b>.</p>", zayavkaTemp.v1C_EPU, zayavkaTemp.v1C_adresEPU, zayavkaTemp.v1C_maxMoschnostEPU, urovenUstr));
                */
                notifyText = String.Concat(                  
                    String.Format("<tr><td>{0}</td><td>{1}</td>",
                        String.Format("<a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/OrderInfo?id={0}\">{1}</a>", zayavkaTemp.id_zayavka.ToString(), zayavkaTemp.v1C_Zayavitel),
                        zayavkaTemp.v1C_NomerDogovora),
                    String.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", zayavkaTemp.v1C_EPU, zayavkaTemp.v1C_adresEPU, zayavkaTemp.v1C_maxMoschnostEPU, urovenUstr)
                    );
            }
            else if (id_notifytype == MC_PersCab_Consts.notifyType_GP_AktDopuskaPU)
            {
                /*notifyText = String.Concat(
                    "<p>ОГУЭП Облкоммунэнерго уведомляет Вас о том, что в целях исполнения п 109 ПП РФ 861 (в редакции ПП РФ 262) в личном кабинете заявителя размещен акт допуска прибора учета в эксплуатацию. С этого дня прибор учета считается введенным в эксплуатацию и его показания учитываются при определении объема потребления электрической энергии (мощности).</p>",
                    String.Format("<p>Заявитель: <b>{0}</b>. Номер заявки: <b>{1}</b>. Дата заявки: <b>{2:d}</b>.</p>", zayavkaTemp.v1C_Zayavitel, zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                    String.Format("<p>Необходимо внести данные ПУ и первоначальные показания ПУ в договор энергоснабжения: <b>{0}</b> ({1}).</p>", nomerD, nomerLS));
                */
                notifyText = String.Concat(                    
                    String.Format("<tr><td>{0}</td><td>{1}</td><td>{2:d}</td>",
                        String.Format("<a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/OrderInfo?id={0}\">{1}</a>", zayavkaTemp.id_zayavka.ToString(), zayavkaTemp.v1C_Zayavitel),
                        zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                    String.Format("<td>{0}</td><td>{1}</td></tr>", nomerD, nomerLS)
                    );
            }
            else if (id_notifytype == MC_PersCab_Consts.notifyType_GP_AktTP)
            {
                /*notifyText = String.Concat(
                    "<p>ОГУЭП Облкоммунэнерго уведомляет Вас о том, что в целях исполнения п 112 ПП РФ 861 (в редакции ПП РФ 262) в личном кабинете заявителя размещен акт об осуществлении технологического присоединения. Со дня составления и размещения в личном кабинете акта об осуществлении технологического присоединения, подписанного со стороны сетевой организации, гарантирующим поставщиком осуществляется исполнение обязательств  по договору, обеспечивающему продажу электрической энергии (мощности) на розничном рынке, в отношении энергопринимающего устройства, технологическое присоединение которого осуществлялось.</p>",
                    String.Format("<p>Заявитель: <b>{0}</b>. Номер заявки: <b>{1}</b>. Дата заявки: <b>{2:d}</b>.</p>", zayavkaTemp.v1C_Zayavitel, zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                    String.Format("<p>Необходимо активировать договор энергоснабжения: <b>{0}</b> ({1}).</p>", nomerD, nomerLS));
                */
                notifyText = String.Concat(                    
                    String.Format("<tr><td>{0}</td><td>{1}</td><td>{2:d}</td>",
                        String.Format("<a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/OrderInfo?id={0}\">{1}</a>", zayavkaTemp.id_zayavka.ToString(), zayavkaTemp.v1C_Zayavitel),
                        zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                    String.Format("<td>{0}</td><td>{1}</td></tr>", nomerD, nomerLS)
                    );
            }
            else if (id_notifytype == MC_PersCab_Consts.notifyType_GP_ZAnnulirovana)
            {
                /*notifyText = String.Concat(
                    "<p>ОГУЭП Облкоммунэнерго уведомляет Вас об аннулировании заявки в связи с несоблюдением заявителем обязанности по оплате счета.</p>",
                    String.Format("<p>Заявитель: <b>{0}</b>. Номер заявки: <b>{1}</b>. Дата заявки: <b>{2:d}</b>.</p>", zayavkaTemp.v1C_Zayavitel, zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                    String.Format("<p>Наименование энергопринимающего устройства: <b>{0}</b>. Место нахождения энергопринимающего устройства: <b>{1}</b>. Мощность Рmax: <b>{2}</b>. Уровень напряжения: <b>{3}</b>.</p>", zayavkaTemp.v1C_EPU, zayavkaTemp.v1C_adresEPU, zayavkaTemp.v1C_maxMoschnostEPU, urovenUstr));
                    */
                notifyText = String.Concat(                    
                    String.Format("<tr><td>{0}</td><td>{1}</td><td>{2:d}</td>",
                        String.Format("<a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/OrderInfo?id={0}\">{1}</a>", zayavkaTemp.id_zayavka.ToString(), zayavkaTemp.v1C_Zayavitel),
                        zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                    String.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", zayavkaTemp.v1C_EPU, zayavkaTemp.v1C_adresEPU, zayavkaTemp.v1C_maxMoschnostEPU, urovenUstr)
                    );
            }
            //-------------------------------------------------------------
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Z_ZAnnulirovana)
            {
                if (id_notifychannel == MC_PersCab_Consts.notifyChannel_Email)
                    notifyText = String.Concat(
                        "<p>Автоматическое уведомление. Не отвечайте на данное письмо!<br/>Переписку необходимо осуществлять в Личном кабинете!</p>",
                        String.Format("<p>В соответствии с ПП РФ №-861 в связи с предоставлением неполных сведений / предоставлением недостоверных сведений / не оплатой выставленного сетевой организацией счета ОГУЭП Облкоммунэнерго уведомляет Вас, что Ваша заявка номер <b>{0}</b> от <b>{1:d}</b> аннулирована.</p>", zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                        String.Format("<p><a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/OrderInfo?id={0}\">просмотр заявки</a></p>", zayavkaTemp.id_zayavka.ToString()));
                else if (id_notifychannel == MC_PersCab_Consts.notifyChannel_SMS)
                    notifyText = String.Concat(                        
                        String.Format("OKE38.RU Ваша заявка {0} от {1:d} аннулирована", zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c));
            }
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Z_AktDopuskaPU)
            {
                if (id_notifychannel == MC_PersCab_Consts.notifyChannel_Email)
                    notifyText = String.Concat(
                        "<p>Автоматическое уведомление. Не отвечайте на данное письмо!<br/>Переписку необходимо осуществлять в Личном кабинете!</p>",
                        "<p>ОГУЭП Облкоммунэнерго уведомляет Вас о том, что в целях исполнения п 109 ПП РФ 861 (в редакции ПП РФ 262) в личном кабинете заявителя размещен акт допуска прибора учета в эксплуатацию. С этого дня прибор учета считается введенным в эксплуатацию и его показания учитываются при определении объема потребления электрической энергии (мощности).</p>",
                        String.Format("<p>Заявитель: <b>{0}</b>. Номер заявки: <b>{1}</b>. Дата заявки: <b>{2:d}</b>.</p>", zayavkaTemp.v1C_Zayavitel, zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                        String.Format("<p><a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/OrderInfo?id={0}\">просмотр заявки</a></p>", zayavkaTemp.id_zayavka.ToString()));
                else if (id_notifychannel == MC_PersCab_Consts.notifyChannel_SMS)
                    notifyText = String.Concat(                                                
                        String.Format("OKE38.RU Заявка {0}. Размещен акт допуска ПУ", zayavkaTemp.zayavka_number_1c));
            }
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Z_AktTP)
            {
                if (id_notifychannel == MC_PersCab_Consts.notifyChannel_Email)
                    notifyText = String.Concat(
                        "<p>Автоматическое уведомление. Не отвечайте на данное письмо!<br/>Переписку необходимо осуществлять в Личном кабинете!</p>",
                        "<p>ОГУЭП Облкоммунэнерго уведомляет Вас о том, что в целях исполнения п 112 ПП РФ 861 (в редакции ПП РФ 262) в личном кабинете заявителя размещен акт об осуществлении технологического присоединения <b>(Уведомление об обеспечении СО возможности присоединения)</b>. Со дня составления и размещения в личном кабинете акта об осуществлении технологического присоединения, подписанного со стороны сетевой организации, гарантирующим поставщиком осуществляется исполнение обязательств  по договору, обеспечивающему продажу электрической энергии (мощности) на розничном рынке, в отношении энергопринимающего устройства, технологическое присоединение которого осуществлялось.</p>",
                        String.Format("<p>Заявитель: <b>{0}</b>. Номер заявки: <b>{1}</b>. Дата заявки: <b>{2:d}</b>.</p>", zayavkaTemp.v1C_Zayavitel, zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                        String.Format("<p><a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/OrderInfo?id={0}\">просмотр заявки</a></p>", zayavkaTemp.id_zayavka.ToString()));
                else if (id_notifychannel == MC_PersCab_Consts.notifyChannel_SMS)
                    notifyText = String.Concat(
                        String.Format("OKE38.RU Заявка {0}. Размещен акт о ТП (Уведомление)", zayavkaTemp.zayavka_number_1c));
            }
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Z_DogovorTP)
            {
                if (id_notifychannel == MC_PersCab_Consts.notifyChannel_Email)
                    notifyText = String.Concat(
                        "<p>Автоматическое уведомление. Не отвечайте на данное письмо!<br/>Переписку необходимо осуществлять в Личном кабинете!</p>",
                        String.Format("<p>По Вашей заявке номер <b>{0}</b> от <b>{1:d}</b> в личном кабинете размещен договор и технические условия на подключение к сетям ОГУЭП Облкоммунэнерго, а так же счет для внесения платы за технологическое присоединение.</p>", zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                        String.Format("<p><a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/OrderInfo?id={0}\">просмотр заявки</a></p>", zayavkaTemp.id_zayavka.ToString()),
                        "<p>Просим Вас оплатить в течение 5 рабочих дней выставленный сетевой организацией счет.</p>",
                        "<p>При внесении платы за технологическое присоединение в назначении платежа обязательно указать полные реквизиты указанного счета.</p>",
                        "<p>Уведомляем Вас, что договор считается заключенным, со дня оплаты счета.<br/>",
                        "Наличие заключенного заявителем договора подтверждается документом об оплате.<br/>",
                        "В случае несоблюдения заявителем обязанности по оплате счета заявка будет аннулирована.</p>"
                        );
                else if (id_notifychannel == MC_PersCab_Consts.notifyChannel_SMS)
                    notifyText = String.Concat(                        
                        String.Format("OKE38.RU Заявка {0}. Размещены договор,ТУ,счет", zayavkaTemp.zayavka_number_1c)                        
                        );
            }
            //-------------------------------------------------------------
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Sys_ChatFromZayavitel)
            {
                notifyText = String.Concat(                    
                    "<p>Автоматическое уведомление. Не отвечайте на данное письмо!</p>",
                    "<p>Поступило новое сообщение.</p>",
                    String.Format("<p>Заявитель: <b>{0}</b>. Номер заявки: <b>{1}</b>. Дата заявки: <b>{2:d}</b>.</p>", zayavkaTemp.v1C_Zayavitel, zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                    String.Format("<p><a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/OrderInfo?id={0}\">просмотр заявки</a></p>", zayavkaTemp.id_zayavka.ToString()),
                    String.Format("<p><b>Сообщение:</b></p><p>{0}</p>", userMessage.caption_msg),
                    "<p><b>Вложения:</b></p>");

                if (link_id != null)
                {
                    IQueryable<tblZayavkaDoc> temp_tblZayavkaDoc = lkAdapter.tblZayavkaDoc.Where(p => p.id_chatrec == link_id).Select(p => p);
                    foreach (tblZayavkaDoc tempZayavkaDoc in temp_tblZayavkaDoc)
                    {
                        notifyText += String.Format("<a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/ViewDoc?doctype=order&docid={0}\">{1}</a><br/>", tempZayavkaDoc.id_zayavkadoc.ToString(), tempZayavkaDoc.doc_file_name);
                    }
                }
            }
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Sys_ChatToZayavitel)
            {
                if (id_notifychannel == MC_PersCab_Consts.notifyChannel_Email)
                { 
                    notifyText = String.Concat(
                        "<p><b>Автоматическое уведомление. Не отвечайте на данное письмо!</b><br/><b>Переписку необходимо осуществлять в Личном кабинете!</b></p>",
                        "<p>Поступило новое сообщение.</p>",
                        String.Format("<p>Заявитель: <b>{0}</b>. Номер заявки: <b>{1}</b>. Дата заявки: <b>{2:d}</b>.</p>", zayavkaTemp.v1C_Zayavitel, zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c),
                        String.Format("<p><a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/OrderInfo?id={0}\">просмотр заявки</a></p>", zayavkaTemp.id_zayavka.ToString()),
                        String.Format("<p><b>Сообщение:</b></p><p>{0}</p>", userMessage.caption_msg),
                        "<p><b>Вложения:</b></p>");

                    if (link_id != null)
                    {
                        IQueryable<tblZayavkaDoc> temp_tblZayavkaDoc = lkAdapter.tblZayavkaDoc.Where(p => p.id_chatrec == link_id).Select(p => p);
                        foreach (tblZayavkaDoc tempZayavkaDoc in temp_tblZayavkaDoc)
                        {
                            notifyText += String.Format("<a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/ViewDoc?doctype=order&docid={0}\">{1}</a><br/>", tempZayavkaDoc.id_zayavkadoc.ToString(), tempZayavkaDoc.doc_file_name);
                        }
                    }
                }
                else if (id_notifychannel == MC_PersCab_Consts.notifyChannel_SMS)
                    notifyText = String.Concat(                        
                        String.Format("OKE38.RU Заявка {0} от {1:d}. Поступило сообщение", zayavkaTemp.zayavka_number_1c, zayavkaTemp.zayavka_date_1c)
                        );
            }
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Sys_NewZ)
            {
                notifyText = String.Concat(
                    "<p>Автоматическое уведомление. Не отвечайте на данное письмо!</p>",
                    "<p>Поступила новая заявка.</p>",
                    String.Format("<p>Заявитель: <b>{0}</b>.</p>", zayavkaTemp.v1C_Zayavitel),
                    String.Format("<p><a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/OrderInfo?id={0}\">просмотр заявки</a></p>", zayavkaTemp.id_zayavka.ToString()));                    
            }
            else if (id_notifytype == MC_PersCab_Consts.notifyType_Sys_ZakreplenFilial)
            {
                notifyText = String.Concat(
                    "<p>Автоматическое уведомление. Не отвечайте на данное письмо!</p>",
                    "<p>Закреплена новая заявка.</p>",
                    String.Format("<p>Заявитель: <b>{0}</b>.</p>", zayavkaTemp.v1C_Zayavitel),
                    String.Format("<p><a target=\"_blank\" href=\"https://www.oke38.ru/portaltp/Cabinet/OrderInfo?id={0}\">просмотр заявки</a></p>", zayavkaTemp.id_zayavka.ToString()));
            }

            return notifyText;
        } // public static string GenerateMailBodyText(Guid id_notifytype)

        // заполнение списка получателей сообщения
        public static void FillMailTo(tblNotify notifyTemp, MailMessage mail)
        {
            perscabnewEntities lkAdapter = new perscabnewEntities();
            tblZayavka zayavkaTemp = null;// lkAdapter.tblZayavka.Where(p => p.id_zayavka == notifyTemp.link_id).Select(p => p).First();

            if (notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_RegNewZ || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_RegNewDTP ||
            notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_AktDopuskaPU || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_AktTP || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_ZAnnulirovana ||
            notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Z_ZAnnulirovana || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Z_AktTP || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Z_AktDopuskaPU || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Z_DogovorTP ||
            notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Sys_NewZ || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Sys_ZakreplenFilial
            )
            {
                zayavkaTemp = lkAdapter.tblZayavka.Where(p => p.id_zayavka == notifyTemp.link_id).Select(p => p).First();
            }
            else
            if (notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Sys_ChatFromZayavitel || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Sys_ChatToZayavitel)
            {
                tblZayavkaChat userMessage = lkAdapter.tblZayavkaChat.Where(p => p.id_chatrec == notifyTemp.link_id).Select(p => p).First();
                zayavkaTemp = lkAdapter.tblZayavka.Where(p => p.id_zayavka == userMessage.id_zayavka).Select(p => p).First();
            }

            /*int id_notifyTypeGroup = -1; // id группы уведомлений = 1 - для ГП; 2 - для заявителя; 3 - чат, новая заявка, закрепление за филиалом
            if (notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_RegNewZ || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_RegNewDTP ||
                notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_AktDopuskaPU || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_AktTP ||
                notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_ZAnnulirovana)
            {
                id_notifyTypeGroup = 1;
            }
            else 
            if (notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Z_ZAnnulirovana || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Z_DogovorTP ||
                notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Z_AktDopuskaPU || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Z_AktTP)
            {
                id_notifyTypeGroup = 2;
            }*/

            try
            {
                if (notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_RegNewZ || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_RegNewDTP ||
                notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_AktDopuskaPU || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_AktTP ||
                notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_GP_ZAnnulirovana)
                {
                    switch (zayavkaTemp.id_filial)
                    {
                        case 1: // Ангарские ЭС
                            mail.To.Add(new MailAddress("vashchenko_yi@es.irkutskenergo.ru", "vashchenko_yi@es.irkutskenergo.ru"));
                            //mail.To.Add(new MailAddress("Savinskaya_EA@es.irkutskenergo.ru", "Savinskaya_EA@es.irkutskenergo.ru"));
                            mail.To.Add(new MailAddress("Krylova_ns@es.irkutskenergo.ru", "Krylova_ns@es.irkutskenergo.ru"));
                            //mail.To.Add(new MailAddress("Martishenko_OI@es.irkutskenergo.ru", "Martishenko_OI@es.irkutskenergo.ru"));
                            //mail.To.Add(new MailAddress("pogodskaya_ts@es.irkutskenergo.ru", "pogodskaya_ts@es.irkutskenergo.ru"));
                            break;

                        case 2: // Иркутские ЭС
                            mail.To.Add(new MailAddress("Martishenko_OI@es.irkutskenergo.ru", "Martishenko_OI@es.irkutskenergo.ru"));
                            mail.To.Add(new MailAddress("pogodskaya_ts@es.irkutskenergo.ru", "pogodskaya_ts@es.irkutskenergo.ru"));
                            break;

                        case 3: // Киренские ЭС
                            mail.To.Add(new MailAddress("Martishenko_OI@es.irkutskenergo.ru", "Martishenko_OI@es.irkutskenergo.ru"));
                            mail.To.Add(new MailAddress("pogodskaya_ts@es.irkutskenergo.ru", "pogodskaya_ts@es.irkutskenergo.ru"));
                            break;

                        case 4: // Мамско-Чуйские ЭС
                            mail.To.Add(new MailAddress("Martishenko_OI@es.irkutskenergo.ru", "Martishenko_OI@es.irkutskenergo.ru"));
                            mail.To.Add(new MailAddress("pogodskaya_ts@es.irkutskenergo.ru", "pogodskaya_ts@es.irkutskenergo.ru"));
                            break;

                        case 5: // Нижнеудинские ЭС
                            mail.To.Add(new MailAddress("chernickiy@es.irkutskenergo.ru", "chernickiy@es.irkutskenergo.ru"));
                            break;

                        case 6: // Саянские ЭС
                            mail.To.Add(new MailAddress("korotkova_av@es.irkutskenergo.ru", "korotkova_av@es.irkutskenergo.ru"));
                            break;

                        case 7: // Тайшетские ЭС
                            mail.To.Add(new MailAddress("Yuhnovec_nv@es.irkutskenergo.ru", "Yuhnovec_nv@es.irkutskenergo.ru"));
                            break;

                        case 8: // Усть-Кутские ЭС
                            mail.To.Add(new MailAddress("pitskhelauri_il@es.irkutskenergo.ru", "pitskhelauri_il@es.irkutskenergo.ru"));
                            break;

                        case 9: // Усть-Ордынские ЭС
                                //mail.To.Add(new MailAddress("frolova_td@es.irkutskenergo.ru", "frolova_td@es.irkutskenergo.ru")); замена на Полковникова А.С. по письму на robot@oke38.ru
                            mail.To.Add(new MailAddress("polkovnikov_as@es.irkutskenergo.ru", "polkovnikov_as@es.irkutskenergo.ru"));
                            break;

                        case 10: // Черемховские ЭС
                            mail.To.Add(new MailAddress("veretnov@es.irkutskenergo.ru", "veretnov@es.irkutskenergo.ru"));
                            break;
                    }
                } // if уведомления для ГП

                else
                if (notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Z_ZAnnulirovana || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Z_DogovorTP ||
                notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Z_AktDopuskaPU || notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Z_AktTP)
                {
                    string userEmail = lkAdapter.tblUserInfo.Where(p => p.id_user == notifyTemp.recipient_id).Select(p => p).First().email;
                    //notifyTemp.email = userEmail;
                    mail.To.Add(new MailAddress(userEmail, userEmail));
                } // else if уведомления для заявителя

                else
                if (notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Sys_ChatFromZayavitel)
                {
                    // переделать на константы ролей
                    /*IQueryable<tblUserInfo> listOperators = lkAdapter.tblUserInfo.
                            Where(p => (p.id_userrole == 3) || (p.id_userrole == 1) || (p.id_userrole == 2 && p.id_filial == zayavkaTemp.id_filial)).Select(p => p);*/
                    IQueryable<tblUserInfo> listOperators = lkAdapter.tblUserInfo.
                            Where(p => (p.id_userrole == 3) || (p.id_userrole == 2 && p.id_filial == zayavkaTemp.id_filial)).Select(p => p);

                    foreach (tblUserInfo userOperator in listOperators)
                    {
                        MailAddress ma = new MailAddress(userOperator.email);
                        if (!mail.To.Contains(ma)) mail.To.Add(ma);
                    }
                } // else if сообщения чата от заявителя

                else
                if (notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Sys_ChatToZayavitel)
                {
                    string userEmail = lkAdapter.tblUserInfo.Where(p => p.id_user == notifyTemp.recipient_id).Select(p => p).First().email;
                    //notifyTemp.email = userEmail;
                    mail.To.Add(new MailAddress(userEmail, userEmail));
                    //mail.To.Add(new MailAddress("it@oblkomenergo.ru", "it@oblkomenergo.ru")); // копия письма на ящик администратора
                } // else if сообщения чата для заявителя

                else
                if (notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Sys_NewZ)
                {
                    // переделать на константы ролей
                    IQueryable<tblUserInfo> listOperators = lkAdapter.tblUserInfo.Where(p => p.id_userrole == 3).Select(p => p);

                    foreach (tblUserInfo userOperator in listOperators)
                    {
                        MailAddress ma = new MailAddress(userOperator.email);
                        if (!mail.To.Contains(ma)) mail.To.Add(ma);
                    }
                    mail.To.Add(new MailAddress("it@oblkomenergo.ru", "it@oblkomenergo.ru")); // копия письма на ящик администратора
                } // else if поступление новой заявки

                else
                if (notifyTemp.id_notifytype == MC_PersCab_Consts.notifyType_Sys_ZakreplenFilial)
                {
                    // переделать на константы ролей
                    IQueryable<tblUserInfo> listOperators;
                    if (zayavkaTemp.id_filial == 4) // МЧЭС
                    {
                        listOperators = lkAdapter.tblUserInfo.
                                Where(p => p.id_userrole == 3).Select(p => p); // отправляем АУПу
                    }
                    else
                    {
                        listOperators = lkAdapter.tblUserInfo.
                                Where(p => (p.id_userrole == 2 && p.id_filial == zayavkaTemp.id_filial)).Select(p => p);
                    }

                    foreach (tblUserInfo userOperator in listOperators)
                    {
                        MailAddress ma = new MailAddress(userOperator.email);
                        if (!mail.To.Contains(ma)) mail.To.Add(ma);
                    }
                    //mail.To.Add(new MailAddress("it@oblkomenergo.ru", "it@oblkomenergo.ru")); // копия письма на ящик администратора
                } // else if закрепление заявки за филиалом
            }
            catch (FormatException)
            {
                notifyTemp.notifystatus_id = MC_PersCab_Consts.notifyStatus_ErrorInEmail; //lkAdapter.tblNotifyStatus.Where(p => p.caption_notifystatus == "ошибка в email").Select(p => p).First().id_notifystatus;
                //continue;
            }

        } // public static void FillMailTo(Guid id_notifytype, MailMessage mail)

    }
}
