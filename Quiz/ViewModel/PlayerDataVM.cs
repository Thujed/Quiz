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

        public string PlayerName => _player.PlayerName;
        public int PlayerPoints => _player.PlayerPoints;
        public GridLength PlayerPointsStars => new GridLength(_player.PlayerPoints, GridUnitType.Star);
        public Color PlayerColor => _player.PlayerColor;
        public DelegateCommand OnPlayerTileClick { get; }

        public void SetPlayerPoints(int newPointsValue) {
            _player.PlayerPoints = newPointsValue;
        }

        

    }
}
