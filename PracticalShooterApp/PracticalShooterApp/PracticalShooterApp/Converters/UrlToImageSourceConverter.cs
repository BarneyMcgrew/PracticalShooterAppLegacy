using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using PracticalShooterApp.Shared.Services;
using Xamarin.Forms;

namespace PracticalShooterApp.Converters
{
    public class UrlToImageSourceConverter : IValueConverter
    {
        public object Convert(object urlObj, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource imageSource = null;

            var imageUrl = (string)urlObj;

            var webCacheService = new WebCacheService();

            var imageByte = Task.Run(() => webCacheService.GetWebImageCache(imageUrl)).Result;
            
            imageSource = ImageSource.FromStream(() => new MemoryStream(imageByte));
            
            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}