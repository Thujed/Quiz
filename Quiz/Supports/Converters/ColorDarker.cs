using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace Quiz.Supports.Converters 
{
    class ColorDarker : IValueConverter
    {
        private const double darkeningLvl = 0.22;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color)value;
            double h = 0;
            double s = 0;
            double l = 0;

            RgbToHsl.Convert(color, out h, out l, out s);
            l -= darkeningLvl;

            Color resultColor;
            RgbToHsl.ConvertBack(h, l, s, out resultColor);

            return resultColor;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
