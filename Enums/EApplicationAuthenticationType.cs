using System;

namespace ERP.IntegrationUI.Enums
{
    [Flags]
    public enum EApplicationAuthenticationType : int
    {
        DEFAULT = 0,
        UserName = 1,
        Password = 2,
        Database = 4,
        Key = 8,
        KeyFile = 16,
    }
}
