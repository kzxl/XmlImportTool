using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using XmlImportTool.Data;

public class SqlServerProvider : IDatabaseProvider
{
    public string Name => "SQL Server";

    public bool TestConnection(string connectionString)
    {
        try { using (var conn = new SqlConnection(connectionString)) conn.Open(); return true; }
        catch { return false; }
    }

    public void CreateTableIfNotExists(string connectionString, DataTable dt, string tableName)
    {
        using (var conn = new SqlConnection(connectionString))
        using (var cmd = conn.CreateCommand())
        {
            conn.Open();
            string checkSql = $"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{tableName}') ";
            string createSql = $"CREATE TABLE [{tableName}] (" +
                string.Join(",", dt.Columns.Cast<DataColumn>().Select(c => $"[{c.ColumnName}] NVARCHAR(MAX)")) + ")";
            cmd.CommandText = checkSql + createSql;
            cmd.ExecuteNonQuery();
        }
    }

    public void BulkInsert(string connectionString, DataTable dt, string tableName)
    {
        using (var conn = new SqlConnection(connectionString))
        using (var bulk = new SqlBulkCopy(conn))
        {
            conn.Open();
            bulk.DestinationTableName = tableName;
            bulk.WriteToServer(dt);
        }
    }
}
