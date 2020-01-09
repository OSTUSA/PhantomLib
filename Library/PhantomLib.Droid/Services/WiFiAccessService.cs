using Android.OS;
using Android.Content;
using PhantomLib.Droid.Services;
using PhantomLib.Services;
using Xamarin.Forms;
using Android.Net.Wifi;
using System;
using System.Linq;

[assembly: Dependency(typeof(WiFiAccessService))]
namespace PhantomLib.Droid.Services
{

    public class WiFiAccessService : IWiFiAccessService
    {
        [Obsolete]
        public void WiFiAccessEnabled(string ssid, string networkPassword)
        {

            WifiConfiguration conf = new WifiConfiguration();

            conf.Ssid = "\"" + ssid + "\"";

            //WEP Network parsing 
            conf.WepKeys[0] = "\"" + networkPassword + "\"";
            conf.WepTxKeyIndex = 0;
            conf.AllowedKeyManagement.Set(0); //This sets the allowed management to NONE
            conf.AllowedGroupCiphers.Set(0); //This sets the allowed Cipher to WEP40

            //WPA Network parsing
            conf.PreSharedKey = "\"" + networkPassword + "\"";

            //Open network
            conf.AllowedKeyManagement.Set(0); // This sets the management tot NONE

            var wifiManager = (WifiManager)Android.App.Application.Context
                        .GetSystemService(Context.WifiService);

            //Adding in the Wifi manager settings
           
            var addNetwork = wifiManager.AddNetwork(conf);

            var network = wifiManager.ConfiguredNetworks.FirstOrDefault(n => n.Ssid == ssid);

            if (network == null)
            {
                Console.WriteLine($"Cannot conn=ect to network: {ssid}");
                return;
            }

            wifiManager.Disconnect();

            var enableNetwork = wifiManager.EnableNetwork(network.NetworkId, true);
        }
    }

}
