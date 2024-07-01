using DatabaseFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcperscab
{
    // роли пользователей в соответствии с таблицей tblUserRole
    public enum MC_PersCab_UserRoleType : int { MC_urt_USER = 0, MC_urt_ADMIN = 1, MC_urt_OPERATOR = 2, MC_urt_DISPETCHER = 3, MC_urt_GP_OPERATOR = 4 }
    public class MC_PersCab_ActionsWithUsers
    {
        public MC_PersCab_ActionsWithUsers()
        {
            //
            // TODO: добавьте логику конструктора
            //
        }

        // получение идентификатора роли пользователя по логину (в данный момент роль только одна)
        public static int MC_PersCab_ActionsWithUsers_GetRole(string user_login)
        {
            perscabnewEntities perscabnewEntitiesAdapter = new perscabnewEntities();

            tblUserInfo userInfo = perscabnewEntitiesAdapter.tblUserInfo.Where(p => p.user_login == user_login).Select(p => p).First();

            return userInfo.id_userrole;
        }
    }
}
