using DatabaseFirst;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcperscab
{
    public class MC_PersCab_Chat
    {

        // вычисление количества непрочтенных сообщения
        public static int CountMessagesNotRead(int user_role, Guid id_zayavka)
        {
            perscabnewEntities lkAdapter = new perscabnewEntities();

            int count_msgs_not_read = 0;

            if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_OPERATOR || user_role == (int)MC_PersCab_UserRoleType.MC_urt_DISPETCHER ||
                user_role == (int)MC_PersCab_UserRoleType.MC_urt_ADMIN || user_role == (int)MC_PersCab_UserRoleType.MC_urt_GP_OPERATOR)
            {
                count_msgs_not_read = lkAdapter.tblZayavkaChat
                .Join(lkAdapter.tblUserInfo, tZC => tZC.id_user, tUI => tUI.id_user, (tZC, tUI) => new { tblZayavkaChat = tZC, tblUserInfo = tUI })
                .Where(p => p.tblZayavkaChat.id_zayavka == id_zayavka && p.tblZayavkaChat.istemp == null &&
                    p.tblZayavkaChat.isread == false && p.tblUserInfo.id_userrole == (int)MC_PersCab_UserRoleType.MC_urt_USER).Select(p => p).Count();
            }
            else
            if (user_role == (int)MC_PersCab_UserRoleType.MC_urt_USER)
            {
                count_msgs_not_read = lkAdapter.tblZayavkaChat
                .Join(lkAdapter.tblUserInfo, tZC => tZC.id_user, tUI => tUI.id_user, (tZC, tUI) => new { tblZayavkaChat = tZC, tblUserInfo = tUI })
                .Where(p => p.tblZayavkaChat.id_zayavka == id_zayavka && p.tblZayavkaChat.istemp == null &&
                    p.tblZayavkaChat.isread == false && p.tblUserInfo.id_userrole != (int)MC_PersCab_UserRoleType.MC_urt_USER).Select(p => p).Count();
            }

            return count_msgs_not_read;
        }

    }
}
