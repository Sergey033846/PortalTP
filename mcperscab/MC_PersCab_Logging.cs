using DatabaseFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcperscab
{
    public class MC_PersCab_Logging
    {
        // роли пользователей в соответствии с таблицей tblUserRole
        public enum MC_PersCab_LogEventType : int
        {
            MC_LET_LOGIN = 1, MC_LET_RESETPASSWORD = 2
        }

        public MC_PersCab_Logging()
        {
            //
            // TODO: добавьте логику конструктора
            //
        }

        // запись лога
        public static void MC_PersCab_Logging_LogWrite(string user_login, Guid? id_zayavka, MC_PersCab_LogEventType id_logeventtype, string comment_log)
        {
            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();

            tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

            tblLog LogInfo = new tblLog();
            LogInfo.id_logrec = Guid.NewGuid();
            LogInfo.id_logeventtype = (int)id_logeventtype;
            LogInfo.id_user = userInfo.id_user;
            LogInfo.id_zayavka = id_zayavka;
            LogInfo.comment_log = comment_log;
            LogInfo.logrec_datetime = DateTime.Now;

            perscabnewEntitiesAdapter.tblLog.Add(LogInfo);
            perscabnewEntitiesAdapter.SaveChanges();
        }
    }
}
