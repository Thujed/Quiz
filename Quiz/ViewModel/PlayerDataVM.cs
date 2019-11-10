using Quiz.Model;
using System;
using System.Windows.Media;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows;
using Quiz.Supports.PropertyAnimationHandlers;
using Quiz.Supports.Extensions;

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
        public double PlayerPoints => _player.PlayerPoints;
        public Color PlayerColor => _player.PlayerColor;
        public DelegateCommand OnPlayerTileClick { get; }

        public void SetPlayerPoints(double newPointsValue) {
            _player.PlayerPoints = newPointsValue;
        }
    }
}
