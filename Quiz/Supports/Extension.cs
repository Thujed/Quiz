using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Quiz.Supports
{
    class Extension
    {
        public static double MeasureStringWidth(string candidate, FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, int fontSize)
        {
            FormattedText formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(fontFamily, fontStyle, fontWeight, fontStretch),
                fontSize,
                Brushes.Black,
                new NumberSubstitution(),
                TextFormattingMode.Display);


            return formattedText.Width;
        }

    }
}
