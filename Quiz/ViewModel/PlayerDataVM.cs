using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows;
using System.Windows.Media.Animation;

namespace Quiz.ViewModel
{
    public class PlayerDataVM : BindableBase
    {
        private Player _player;

        private double _maxBarWidth;
        private int _maxPointValue;

        public PlayerDataVM(Player player, MainVM mainVM) {
            
            _player = player;
            _player.PropertyChanged += (s, e) => {
                RaisePropertyChanged(e.PropertyName);
                if (e.PropertyName == "PlayerPoints") {
                    UpdatePlayerPoints();
                    RaisePropertyChanged("PlayerPointsStars");
                }
            };

            this.OnPlayerTileClick = new DelegateCommand(() => {
                mainVM.ChangePointsVM.StartChangingProcess(this._player);        
            });
        }

        public string PlayerName => _player.PlayerName;
        public int PlayerPoints => _player.PlayerPoints;
        public GridLength PlayerPointsStars => new GridLength(_player.PlayerPoints, GridUnitType.Star);
        public Color PlayerColor => _player.PlayerColor;
        public DelegateCommand OnPlayerTileClick { get; }

        #region BarWidthSettings

        /// <summary>
        /// Trigger for bar width animation
        /// </summary>
        public bool BarWidthAnimationTrigger { get; private set; } = true;

        /// <summary>
        /// Max points minus player points
        /// </summary>
        private GridLength _invertedPlayerPointsStars;
        public GridLength InvertedPlayerPointsStars {
            get => _invertedPlayerPointsStars;
            set {
                SetProperty(ref _invertedPlayerPointsStars, value);
                RaisePropertyChanged("BarWidthAnimationTrigger");
            }
        }

        #endregion

        /// <summary>
        /// Count new width of bar with points
        /// </summary>
        public void UpdatePlayerPoints()
        {
            InvertedPlayerPointsStars = new GridLength(_maxPointValue - PlayerPoints, GridUnitType.Star);
        }

        /// <summary>
        /// Update width of bar if maximum point value changes
        /// </summary>
        /// <param name="value">New maximum point value</param>
        /// <param name="tryCountWidth">If we dont need updating bar width - false</param>
        public void UpdateMaxPointValue(int value, bool tryCountWidth = false)
        {
            _maxPointValue = value;

            if (tryCountWidth)
                InvertedPlayerPointsStars = new GridLength(_maxPointValue - PlayerPoints, GridUnitType.Star);
        }
    }
}
