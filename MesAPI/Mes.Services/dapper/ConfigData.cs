using System.Configuration;

namespace Mes.Services.dapper
{
    public static class ConfigData
    {
        public static string ConnectionString1 { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionString1"].ToString();
        public static string ConnectionString2 { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionString2"].ToString();

        public static string ConnectionString13 { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionString1"].ToString();
        public static string ConnectionString4 { get; set; } = ConfigurationManager.ConnectionStrings["ConnectionString1"].ToString();
    }
}