using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcperscab
{
    public class MC_PersCab_Consts
    {
        public static Guid notifyStatus_VOcheredi = Guid.Parse("DD909EF5-9755-4C53-A5DE-8CB2C8167E1E");
        public static Guid notifyStatus_Otpravleno = Guid.Parse("4238AC76-C80E-4E7E-A0AD-40E2D01A87DC");
        public static Guid notifyStatus_Waiting = Guid.Parse("80711CE5-4238-4B3C-AE4D-762518FDCC95");
        public static Guid notifyStatus_NeDostavleno = Guid.Parse("00BD92F7-7433-436E-B5FD-55345E74395B");
        public static Guid notifyStatus_Error = Guid.Parse("CDF1B5D3-3F6E-45D5-BF86-46CD11127312");
        public static Guid notifyStatus_ErrorInEmail = Guid.Parse("60A8061C-EA60-4D4A-BAE6-66EC7255214D");
        public static Guid notifyStatus_Canceled = Guid.Parse("EAB9118E-2363-4E09-B251-F4C0C1D2F562");

        public static Guid notifyChannel_Email = Guid.Parse("77BAC572-B341-4C6C-9048-F427899BF1B7");
        public static Guid notifyChannel_SMS = Guid.Parse("B85185B4-2AA4-4181-A735-D95F06D5E800");

        public static Guid userRegType_1CEmail = Guid.Parse("BDF2DCCB-D80B-4110-85AA-1C24AF55A814");
        public static Guid userRegType_1CSMS = Guid.Parse("24E61042-61C3-4561-A95C-0B6E1145D640");

        public static Guid notifyType_Sys_RegUserFrom1C = Guid.Parse("660EF594-52F3-478C-8E20-43341B494B79");
        public static Guid notifyType_Sys_NewZ = Guid.Parse("436A621A-E40D-4683-8626-03A6FF1F3CAB");
        public static Guid notifyType_Sys_ZakreplenFilial = Guid.Parse("52B27537-B682-4AA5-A50B-512477F06287");
        public static Guid notifyType_Sys_ChatToZayavitel = Guid.Parse("CC4B5C68-C935-4F25-9FFD-4A32B1B0FE19");
        public static Guid notifyType_Sys_ChatFromZayavitel = Guid.Parse("8908910F-BD73-4E3B-AC64-66DE0E21810A");

        public static Guid notifyType_GP_RegNewZ = Guid.Parse("AA6E4CD6-B4C0-41D4-ACD2-05310E90C725");
        public static Guid notifyType_GP_RegNewDTP = Guid.Parse("46B93A5B-A9AE-4A34-A2A9-74B969738888");
        public static Guid notifyType_GP_AktDopuskaPU = Guid.Parse("FDC8EB42-6938-41E9-AF56-185128CD07F7");
        public static Guid notifyType_GP_AktTP = Guid.Parse("61374625-B521-4DBB-AC8B-D8AB391F1704");
        public static Guid notifyType_GP_ZAnnulirovana = Guid.Parse("6E72A340-1E03-4C30-B468-A45C82347950");

        public static Guid notifyType_Z_ZAnnulirovana = Guid.Parse("CBC62D87-A063-45A1-9550-98441AC439FB");
        public static Guid notifyType_Z_AktDopuskaPU = Guid.Parse("0EB85F89-38FA-4365-8CD4-5451F951B826");
        public static Guid notifyType_Z_AktTP = Guid.Parse("77BCCAFE-835D-4F3A-903D-3BAC7C653EAC");
        public static Guid notifyType_Z_DogovorTP = Guid.Parse("B993D136-FD1D-48D2-AA8D-F4A74D75BDBE");

        public static Guid orderSource_1C = Guid.Parse("C0202436-F9D1-494A-B8C3-EEAFB430FDEA");
        public static Guid orderSource_LK = Guid.Parse("222969FC-3B67-4168-BB1B-6D326CC3DE79");
    }
}
