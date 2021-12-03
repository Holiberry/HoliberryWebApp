using System;
using System.Collections.Generic;
using System.Text;

namespace Holiberry.Api.Common.Request
{
    public class DeviceInfoJSON
    {
        /// <summary>
        /// Android: "google", "xiaomi"; iOS: "Apple"; web: null
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Android: "Pixel 2"; iOS: "iPhone XS Max"; web: "iPhone", null
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Android: "Android"; iOS: "iOS" or "iPadOS"; web: "iOS", "Android", "Windows"
        /// </summary>
        public string OsName { get; set; }

        /// <summary>
        /// Android: "4.0.3"; iOS: "12.3.1"; web: "11.0", "8.1.0"
        /// </summary>
        public string OsVersion { get; set; }

        /// <summary>
        /// Android: 19; iOS: null; web: null
        /// </summary>
        public string PlatformApiLevel { get; set; }

        /// <summary>
        /// An enum of the different types of devices supported by Expo, with these values: UNKNOWN, PHONE, TABLET, DESKTOP, TV
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// Version of application currently instaled
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        /// User's personal device name
        /// </summary>
        public string DeviceName { get; set; }


        public string IPAddress { get; set; }
    }
}
