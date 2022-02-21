using HP_WpfApp_Sampl.ViewModels;
using System.Windows;

namespace HP_WpfApp_Sampl.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
