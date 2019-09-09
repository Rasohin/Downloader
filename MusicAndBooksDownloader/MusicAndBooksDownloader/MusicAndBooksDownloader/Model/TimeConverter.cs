using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MusicAndBooksDownloader.Model
{
    class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string res = "";
            int variable = (int)value / 1000;

            if (variable % 60 < 10)
                res = $"{variable / 60}:0{variable % 60}";
            else
                res = $"{variable / 60}:{variable % 60}";

            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
