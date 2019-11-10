using System;
using System.Collections.Generic;
using System.Linq;
using Quiz.Model;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Quiz.View
{
    /// <summary>
    /// Логика взаимодействия для ChangeGroupPointsVeiw.xaml
    /// </summary>
    public partial class ChangeGroupPointsVeiw : UserControl
    {
        public ChangeGroupPointsVeiw()
        {
            InitializeComponent();
        }

        private void Points_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9\.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
