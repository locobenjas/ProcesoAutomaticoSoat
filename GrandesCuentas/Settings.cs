﻿using System.Configuration;

namespace GrandesCuentas
{
    public enum DB
    {
        LPG_VT,
        LPG_INX,
        LPV_VT,
        LPV_INX,
        LPE_INX,
        AFF_SQL
    }

    public static class Settings
    {
        public static string Conexion(DB db)
        {
            return ConfigurationManager.ConnectionStrings[db.ToString()].ConnectionString;
        }
    }
}