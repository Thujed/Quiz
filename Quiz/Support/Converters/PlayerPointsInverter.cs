using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace Quiz.Support.Converters
{
    /// <summary>
    /// Data converter. Gets a max player points value and invert current points
    /// </summary>
    class PlayerPointsInverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values[0] != null && values[1] != null) {
                return new GridLength((double)values[1] - (double)values[0], GridUnitType.Star);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}