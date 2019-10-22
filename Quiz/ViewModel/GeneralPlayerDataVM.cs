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


        private double _maxPointValueTextWidth;

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
        /// Return maximum width of bar
        /// </summary>
        /// <param name="containerWidth">Width of players info container</param>
        /// <returns></returns>
        public double CountMaxBarWidth(double containerWidth)
        { 
            return containerWidth - _playerTileWidth - TILE_BAR_OFFSET - _maxPointValueTextWidth - POINTS_BAR_OFFSET;
        }

        /// <summary>
        /// When maximum value changes, this method count width of text with max value
        /// </summary>
        /// <param name="maxPointValue"></param>
        public void UpdateMaxPointValue(int maxPointValue)
        {
            _maxPointValue = maxPointValue;

            _maxPointValueTextWidth = Extension.MeasureStringWidth(
                _maxPointValue.ToString(),
                new FontFamily("Gadugi"),
                FontStyles.Normal,
                FontWeights.Regular,
                FontStretches.Normal,
                POINTS_FONT_SIZE
            ) + TILE_PADDING;
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
