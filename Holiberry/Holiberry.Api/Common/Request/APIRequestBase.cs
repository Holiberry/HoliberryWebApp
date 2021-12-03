using System;
using System.Collections.Generic;
using System.Text;

namespace Holiberry.Api.Common.Request
{
    public abstract class APIRequestBase
    {
        //Lokalizacja
        public double? LocLng { get; set; }
        public double? LocLat { get; set; }

        public DeviceInfoJSON DeviceInfo { get; set; }
    }
}
