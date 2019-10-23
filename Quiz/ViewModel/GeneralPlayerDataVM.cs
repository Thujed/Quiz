using Prism.Mvvm;
using Quiz.Supports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public GeneralPlayerDataVM(int maxPointValue)
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

        private int _maxPointValue;
        public int MaxPointValue
        {
            get => _maxPointValue;
            set {
                SetProperty(ref _maxPointValue, value);
            }
        }

        /// <summary>
        /// Invokes when players info changes and count tile width
        /// </summary>
        /// <param name="biggestPlayerName">Biggest name of players</param>
        public void OnPlayersInfoUpdate(string biggestPlayerName)
        {
            //Measure text of tile with player name
            PlayerTileWidth = Extension.MeasureStringWidth(
                biggestPlayerName,
                new FontFamily("Bebas Neue"),
                FontStyles.Normal,
                FontWeights.Bold,
                FontStretches.Normal,
                TILE_FONT_SIZE
            ) + TILE_PADDING;
        }
    }
}
