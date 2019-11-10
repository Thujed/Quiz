using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Quiz.Model
{
    public class Player : BindableBase
    {
        public Player(string playerName, double playerPoints, Color playerColor) {
            this.PlayerName = playerName;
            this.PlayerPoints = playerPoints;
            this.PlayerColor = playerColor;
        }

        public Player() { }

        private string _playerName;
        public string PlayerName
        {
            get => _playerName;
            set {
                SetProperty(ref _playerName, value);
            }
        }

        private double _playerPoints;
        public double PlayerPoints {
            get => _playerPoints;
            set {
                SetProperty(ref _playerPoints, value);
            }
        }

        private Color _playerColor;
        public Color PlayerColor {
            get => _playerColor;
            set {
                SetProperty(ref _playerColor, value);
            }
        }
    }
}
