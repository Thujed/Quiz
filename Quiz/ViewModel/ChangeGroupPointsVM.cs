using Prism.Mvvm;
using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Quiz.ViewModel
{
    public class ChangeGroupPointsVM : BindableBase
    {
        public event Action<int> OnPlayerPointsChanged;

        public ChangeGroupPointsVM() {
            Application.Current.MainWindow.KeyDown += (s, e) => {

                if (ChangePointViewVisibility == Visibility.Hidden)
                return;
                switch (e.Key) {
                    case Key.Enter:
                        int increasingValue = Convert.ToInt32(ScoreChangeValue);
                        OnPlayerPointsChanged.Invoke(_isPositiveSign ? increasingValue : -increasingValue);
                        ChangePointViewVisibility = Visibility.Hidden;
                        break;
                    case Key.OemTilde:
                        IsPositiveSign = !IsPositiveSign;
                        break;
                }    
            };
            ChangePointViewVisibility = Visibility.Hidden;
            IsPositiveSign = true;
        }

        private bool _isPositiveSign;
        public bool IsPositiveSign {
            get => _isPositiveSign;
            set {
                SetProperty(ref _isPositiveSign, value);
            }
        } 

        private Visibility _changePointViewVisibility;
        public Visibility ChangePointViewVisibility {
            get => _changePointViewVisibility;
            set {
                SetProperty(ref _changePointViewVisibility, value);
            }
        }

        public string ScoreChangeValue { get; set; } = "1";
    }
}
