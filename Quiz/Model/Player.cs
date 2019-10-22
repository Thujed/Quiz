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
        public Player(string playerName, int playerPoints, Color playerColor) {
            this.PlayerName = playerName;
            this.PlayerPoints = playerPoints;
            this.PlayerColor = playerColor;
        }

        public Player() { }

        public string PlayerName { get; }

        private int _playerPoints;
        public int PlayerPoints {
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
