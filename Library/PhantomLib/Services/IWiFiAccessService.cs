using System;
using System.Collections.Generic;


namespace PhantomLib.Services
{
    public interface IWiFiAccessService
    {
        void WiFiAccessEnabled(string ssid, string networkPassword);
    }
}
