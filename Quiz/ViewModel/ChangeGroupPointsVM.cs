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
        private Player _player = new Player();

        public ChangeGroupPointsVM() {
            Application.Current.MainWindow.KeyDown += (s, e) => {

                if (ChangePointViewVisibility == Visibility.Hidden)
                return;
                switch (e.Key) {
                    case Key.Enter:
                        IncreasePoint();
                        ChangePointViewVisibility = Visibility.Hidden;
                        break;
                    case Key.LeftShift:
                        IsPositiveSign = !IsPositiveSign;
                        break;
                }    
            };
            ChangePointViewVisibility = Visibility.Hidden;
            IsPositiveSign = true;
        }


        public string PlayerName => _player.PlayerName;
        public int PlayerPoints => _player.PlayerPoints;
        public Color PlayerColor => _player.PlayerColor;

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
        
        public void StartChangingProcess(Player player) {

            _player = player;
            RaisePropertyChanged("PlayerName");
            RaisePropertyChanged("PlayerPoints");
            RaisePropertyChanged("PlayerColor");
            _player.PropertyChanged += (s, e) => RaisePropertyChanged(e.PropertyName);
           

            ChangePointViewVisibility = Visibility.Visible;
        }


        public void IncreasePoint()
        {
            if (IsPositiveSign) {
                int value = Convert.ToInt32(ScoreChangeValue) + _player.PlayerPoints;
                _player.PlayerPoints = value > 60 ? 60 : value;
            } else {
                int value = _player.PlayerPoints - Convert.ToInt32(ScoreChangeValue);
                _player.PlayerPoints = value < 0 ? 0 : value;
            }
        }
    }
}
