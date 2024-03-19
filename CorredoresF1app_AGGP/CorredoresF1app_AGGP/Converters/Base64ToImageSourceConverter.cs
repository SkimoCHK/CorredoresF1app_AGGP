using System;
using System.Globalization;
using Xamarin.Forms;

namespace CorredoresF1app_AGGP.Converters
{
    public class Base64ToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string base64String)
            {
                try
                {
                    byte[] imageBytes = System.Convert.FromBase64String(base64String);
                    return ImageSource.FromStream(() => new System.IO.MemoryStream(imageBytes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error converting base64 string to image source: {ex.Message}");
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
