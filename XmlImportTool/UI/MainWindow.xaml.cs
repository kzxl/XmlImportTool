using System.Windows;
using XmlImportTool.UI.ViewModel;

namespace XmlImportTool
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
