using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XmlImportTool.Models
{
    public class DbConnectionInfo : INotifyPropertyChanged
    {
        private string _server;
        private string _database;
        private string _user;
        private string _password;
        private bool _trustedConnection = true;

        public string Server
        {
            get => _server;
            set { _server = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConnectionString)); }
        }

        public string Database
        {
            get => _database;
            set { _database = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConnectionString)); }
        }

        public string User
        {
            get => _user;
            set { _user = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConnectionString)); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConnectionString)); }
        }

        public bool TrustedConnection
        {
            get => _trustedConnection;
            set { _trustedConnection = value; OnPropertyChanged(); OnPropertyChanged(nameof(ConnectionString)); }
        }

        public string ConnectionString => TrustedConnection
            ? $"Server={Server};Database={Database};Trusted_Connection=True;"
            : $"Server={Server};Database={Database};User Id={User};Password={Password};";

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
