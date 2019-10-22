using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Model
{
    public class PlayersProgressBar : BindableBase
    {
        public float MaxBarWidth { get; set; }
        public bool IsSettingsTileOpen = false;
        public bool IsQuestionTileOpen = false;



    }
}
