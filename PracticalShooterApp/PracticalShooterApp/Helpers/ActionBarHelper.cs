using Xamarin.Essentials;

namespace PracticalShooterApp.Helpers
{
    public class ActionBarHelper : IActionBarHelper
    {
        private readonly int _shortSpaceHeader = 15;
        private readonly int _longSpaceHeader = 35;
        public int GetTopSafeArea()
        {
            // Virtual devices provide processor type instead of model
            var isVirtual = DeviceInfo.DeviceType == DeviceType.Virtual;
                
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                return GetTopSafeAreaForiOS(isVirtual);
            }
            
            return 0;
        }
        
        private int GetTopSafeAreaForiOS(bool isVirtual)
        {
            if (isVirtual)
            {
                switch (DeviceInfo.Name)
                {
                    case "iPhone 8":
                    case "iPhone SE (2nd generation)":
                        return _shortSpaceHeader;
                    default:
                        return _longSpaceHeader;
                }
            }
            
            switch (DeviceInfo.Model)
            {
                case "iPhone8":
                case "iPhone12,8":
                    return _shortSpaceHeader;
                default:
                    return _longSpaceHeader;
            }
        }
    }
}