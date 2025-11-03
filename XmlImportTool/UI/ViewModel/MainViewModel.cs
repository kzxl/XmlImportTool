using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using XmlImportTool.Core;
using XmlImportTool.Core.Helpers;
using XmlImportTool.Data;
using XmlImportTool.Models;

namespace XmlImportTool.UI.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _xmlFilePath;
        private DataTable _previewData;
        private string _selectedDbProvider = "SQL Server";

        public DbConnectionInfo ConnectionInfo { get; set; }

        public string XmlFilePath
        {
            get => _xmlFilePath;
            set { _xmlFilePath = value; OnPropertyChanged(); }
        }

        public DataTable PreviewData
        {
            get => _previewData;
            set { _previewData = value; OnPropertyChanged(); }
        }

        public string ConnectionString => ConnectionInfo.ConnectionString;

        public ObservableCollection<string> DbProviders { get; } = new ObservableCollection<string> { "SQL Server" };

        public string SelectedDbProvider
        {
            get => _selectedDbProvider;
            set { _selectedDbProvider = value; OnPropertyChanged(); }
        }

        public ICommand BrowseCommand { get; }
        public ICommand ImportCommand { get; }
        public ICommand TestConnectionCommand { get; }

        public MainViewModel()
        {
            ConnectionInfo = new DbConnectionInfo { Server = ".", Database = "ImportTest", TrustedConnection = true };
            ConnectionInfo.PropertyChanged += (s, e) => OnPropertyChanged(nameof(ConnectionString));

            BrowseCommand = new RelayCommand(BrowseFile);
            ImportCommand = new RelayCommand(ImportData, CanImportData);
            TestConnectionCommand = new RelayCommand(TestConnection, CanImportData);
        }

        private void BrowseFile(object obj)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                XmlFilePath = openFileDialog.FileName;
                try
                {
                    PreviewData = XmlReaderHelper.ReadToDataTable(XmlFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading XML file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    PreviewData = null;
                }
            }
        }

        private void ImportData(object obj)
        {
            try
            {
                var provider = DatabaseFactory.GetProvider(SelectedDbProvider);
                var importer = new XmlImporter(provider, ConnectionString);
                var tableName = new System.IO.FileInfo(XmlFilePath).Name.Replace(".xml", "");

                importer.Import(XmlFilePath, tableName);

                MessageBox.Show("Import successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Import failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TestConnection(object obj)
        {
            try
            {
                var provider = DatabaseFactory.GetProvider(SelectedDbProvider);
                bool success = provider.TestConnection(ConnectionString);
                if (success)
                {
                    MessageBox.Show("Connection successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Connection failed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanImportData(object obj)
        {
            return !string.IsNullOrEmpty(XmlFilePath) && PreviewData != null && !string.IsNullOrEmpty(ConnectionString);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
