using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.IntegrationUI.Enums
{
    [Flags]
    public enum EIntegrationGroup : int
    {
        ERP = 0,
        RMS = 1,
        Bonee = 2,
        PowerApp = 4,
        FileStore = 8,
        ATSVivaCell = 16,
        ATSUCom = 32
    }
}