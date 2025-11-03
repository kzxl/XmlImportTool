using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlImportTool.Data
{
    public interface IDatabaseProvider
    {
        string Name { get; }
        bool TestConnection(string connectionString);
        void CreateTableIfNotExists(string connectionString, DataTable dt, string tableName);
        void BulkInsert(string connectionString, DataTable dt, string tableName);
    }
}
