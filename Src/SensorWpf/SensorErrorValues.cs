using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows7.Sensors;

namespace SensorWpf
{
    /// <summary>
    /// Sensor error values
    /// </summary>
    public static class SensorErrorValues
    {
        // Property Keys are random GUIDs not associated with SensorAPI, used just for this application
        public static readonly PropertyKey ERROR_NO_DATA = PropertyKey.Create("{BD61CB73-DB2B-4927-B33A-674DE2F8BC32}", 0);
        public static readonly PropertyKey ERROR_ACCESS_DENIED = PropertyKey.Create("{BD61CB73-DB2B-4927-B33A-674DE2F8BC32}", 1);
    }

}
