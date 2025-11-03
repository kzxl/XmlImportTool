using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlImportTool.Data;

namespace XmlImportTool.Core
{
    public class XmlImporter
    {
        private readonly IDatabaseProvider _provider;
        private readonly string _connectionString;

        public XmlImporter(IDatabaseProvider provider, string connectionString)
        {
            _provider = provider;
            _connectionString = connectionString;
        }

        public void Import(string xmlPath, string tableName)
        {
            var dt = XmlReaderHelper.ReadToDataTable(xmlPath);
            _provider.CreateTableIfNotExists(_connectionString, dt, tableName);
            _provider.BulkInsert(_connectionString, dt, tableName);
        }
    }
}
