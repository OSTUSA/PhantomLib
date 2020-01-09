using System;
using NetworkExtension;
using PhantomLib.iOS.Services;
using PhantomLib.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(WiFiAccessService))]
namespace PhantomLib.iOS.Services
{
    public class WiFiAccessService: IWiFiAccessService
    {
        public void WiFiAccessEnabled(string ssid, string networkPassword)
        {
            var wifiManager = new NEHotspotConfigurationManager();
            var wifiConfig = new NEHotspotConfiguration(ssid, networkPassword, false);

            wifiManager.ApplyConfiguration(wifiConfig, (error) =>
            {
                if (error != null)
                {
                    Console.WriteLine($"Error while connecting to  WiFi network {ssid}: {error}");
                }
            });
        }
    }
}
