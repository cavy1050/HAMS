using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;

namespace HAMS.Frame.Kernel.Controls
{
    public class PasswordTextConverter : IValueConverter
    {
        string realPassword = string.Empty;
        char realPasswordChar = '*';

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                string temp = parameter.ToString();
                if (!string.IsNullOrEmpty(temp))
                    realPasswordChar = temp.First();
            }

            if (value != null)
                realPassword = value.ToString();

            string maskPassword = string.Empty;
            for (int i = 0; i < realPassword.Length; i++)
            {
                maskPassword += realPasswordChar;
            }

            return maskPassword;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string backValue = string.Empty;
            if (value != null)
            {
                string strValue = value.ToString();
                for (int i = 0; i < strValue.Length; i++)
                {
                    if (strValue.ElementAt(i) == realPasswordChar)
                        backValue += realPassword.ElementAt(i);
                    else
                        backValue += strValue.ElementAt(i);
                }
            }
            return backValue;
        }
    }
}
