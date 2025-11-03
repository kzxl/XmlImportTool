using System;

namespace XmlImportTool.Data
{
    public static class DatabaseFactory
    {
        public static IDatabaseProvider GetProvider(string providerName)
        {
            switch (providerName.ToLower())
            {
                case "sql server":
                    return new SqlServerProvider();
                // case "mysql": // Future implementation
                //    return new MySqlProvider();
                default:
                    throw new NotSupportedException($"Database provider '{providerName}' is not supported.");
            }
        }
    }
}
