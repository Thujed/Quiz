using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Quiz.Supports.Converters
{
    /// <summary>
    /// Converter for propotional pixel values. 
    /// This converter get window size as a value and pixels value for this property for 1920x1080 resolution as a parameter
    /// !!! Parameter is positive when parameter is width value and negative when height
    /// </summary>
    class ProportionalPixelsConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            string param = (string)parameter;

            if (param == "Width") {
                return val * SystemParameters.MaximizedPrimaryScreenWidth / 1920;
            } else if (param == "Height") {
                return val * SystemParameters.MaximizedPrimaryScreenHeight / 1080;
            }

            return val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
