using System.Windows;
using Quiz.ViewModel;

namespace Quiz
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainVM MainWindowVM { get; set; }

        public MainWindow()
        {
            MainWindowVM = new MainVM();
            DataContext = MainWindowVM;
            InitializeComponent();
        }

        private void MainWindowView_Loaded(object sender, RoutedEventArgs e)
        {
            


        }
    }
}
