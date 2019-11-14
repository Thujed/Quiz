using Quiz.Model;
using System;
using System.Windows.Media;
using Prism.Mvvm;
using Prism.Commands;

namespace Quiz.ViewModel
{
    public class PlayerDataVM : BindableBase
    {
        public event Action<PlayerDataVM> OnPlayerSelected; 

        private Player _player;
    
        public PlayerDataVM(Player player) {
            
            _player = player;
            _player.PropertyChanged += (s, e) => {
                RaisePropertyChanged(e.PropertyName);
            };

            this.OnPlayerTileClick = new DelegateCommand(() => {
                OnPlayerSelected.Invoke(this);
            });
        }

        public string PlayerName {
            get => _player.PlayerName;
            set {
                _player.PlayerName = value;
            }
        }
        
        public Color PlayerColor {
            get => _player.PlayerColor;
            set {
                _player.PlayerColor = value;
            }
        }
        public double PlayerPoints => _player.PlayerPoints;
        public DelegateCommand OnPlayerTileClick { get; }

        public void SetPlayerPoints(double newPointsValue) {
            _player.PlayerPoints = newPointsValue;
        }
    }
}
