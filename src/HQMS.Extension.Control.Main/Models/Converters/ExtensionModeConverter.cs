using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using HQMS.Extension.Kernel.Core;
using System.Diagnostics;

namespace HQMS.Extension.Control.Main.Models
{
    public class ExtensionModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("value:" + value.ToString());

            DisplayModePart displayMode = (DisplayModePart)value;

            if (displayMode == DisplayModePart.Standard)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
