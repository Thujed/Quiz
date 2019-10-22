using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Prism.Commands;
using Prism.Mvvm;


namespace Quiz.ViewModel
{
    public class MainBarVM : BindableBase
    {

        private ResourceDictionary _redCloseButtonIcon = new ResourceDictionary();
        private ResourceDictionary _blackCloseButtonIcon = new ResourceDictionary();
        
        public MainBarVM() {
            _redCloseButtonIcon.Source = new Uri("Resources/Images/Cancel.xaml", UriKind.Relative);
            _redCloseButtonIcon["CancelIconColor"] = new SolidColorBrush(Color.FromRgb(255, 10, 10));

            _blackCloseButtonIcon.Source= new Uri("Resources/Images/Cancel.xaml", UriKind.Relative); 
            _blackCloseButtonIcon["CancelIconColor"] = new SolidColorBrush(Color.FromRgb(1, 0, 2));

            this.OnMouseOverCloseButton = new DelegateCommand(() => {
                Application.Current.Resources.MergedDictionaries[0] = _redCloseButtonIcon;
            });
      
            this.OnMouseLeaveCloseButton = new DelegateCommand(() => {
                Application.Current.Resources.MergedDictionaries[0] = _blackCloseButtonIcon;
            });

            this.OnCloseButtonClick = new DelegateCommand(() => {
                Application.Current.MainWindow.Close();
            });

            QuestionNumber = "56";
        }

        public DelegateCommand OnMouseOverCloseButton { get; }
        public DelegateCommand OnMouseLeaveCloseButton { get; }
        public DelegateCommand OnCloseButtonClick { get; }

        private string _questionNumber;
        public string QuestionNumber {
            get => _questionNumber;
            set {
                SetProperty(ref _questionNumber, value);
            }
        }

    }
}
