using Prism.Mvvm;
using Quiz.Support.Extensions;
using System.Windows;
using System.Windows.Media;

namespace Quiz.ViewModel
{
    public class GeneralPlayerDataVM : BindableBase
    {
        private const int TILE_FONT_SIZE = 34;
        private const int TILE_PADDING = 54;
        private const int TILE_BAR_OFFSET = 19;

        private const int POINTS_FONT_SIZE = 26;
        private const int POINTS_BAR_OFFSET = 19;

        public GeneralPlayerDataVM(double maxPointValue)
        {
            MaxPointValue = maxPointValue;
        }

        private double _playerTileWidth;
        public double PlayerTileWidth {
            get => _playerTileWidth;
            set {
                SetProperty(ref _playerTileWidth, value);
            }
        }

        private double _maxPointValue;
        public double MaxPointValue
        {
            get => _maxPointValue;
            set {
                SetProperty(ref _maxPointValue, value);
            }
        }

        private bool _playersVisibility = true;
        public bool PlayersVisibility
        {
            get => _playersVisibility;
            set {
                SetProperty(ref _playersVisibility, value);
            }
        }

        /// <summary>
        /// Invokes when players info changes and count tile width
        /// </summary>
        /// <param name="biggestPlayerName">Biggest name of players</param>
        public void OnPlayersInfoUpdate(string biggestPlayerName)
        {
            //Measure text of tile with player name
            PlayerTileWidth = biggestPlayerName.MeasureStringWidth(
                new FontFamily("Bebas Neue"),
                FontStyles.Normal,
                FontWeights.Bold,
                FontStretches.Normal,
                TILE_FONT_SIZE
            ) + TILE_PADDING;
        }
    }
}