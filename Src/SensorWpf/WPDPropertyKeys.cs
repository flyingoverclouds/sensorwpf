using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows7.Sensors;

namespace SensorWpf
{
    /// <summary>
    /// General WPD Property Keys
    /// </summary>
    public static class WPDPropertyKeys
    {
        private static readonly Guid WPD_OBJECT_PROPERTIES_V1 = new Guid(0xEF6B490D, 0x5CD8, 0x437A, 0xAF, 0xFC, 0xDA, 0x8B, 0x60, 0xEE, 0x4A, 0x3C);
        public static readonly PropertyKey WPD_OBJECT_ID = PropertyKey.Create(WPD_OBJECT_PROPERTIES_V1, 2);
        public static readonly PropertyKey WPD_OBJECT_PARENT_ID = PropertyKey.Create(WPD_OBJECT_PROPERTIES_V1, 3);
        public static readonly PropertyKey WPD_OBJECT_NAME = PropertyKey.Create(WPD_OBJECT_PROPERTIES_V1, 4);
        public static readonly PropertyKey WPD_OBJECT_PERSISTENT_UNIQUE_ID = PropertyKey.Create(WPD_OBJECT_PROPERTIES_V1, 5);
        public static readonly PropertyKey WPD_OBJECT_FORMAT = PropertyKey.Create(WPD_OBJECT_PROPERTIES_V1, 6);
        public static readonly PropertyKey WPD_OBJECT_CONTENT_TYPE = PropertyKey.Create(WPD_OBJECT_PROPERTIES_V1, 7);

        public static readonly PropertyKey WPD_OBJECT_CAN_DELETE = PropertyKey.Create(WPD_OBJECT_PROPERTIES_V1, 26);

        private static readonly Guid WPD_FUNCTIONAL_OBJECT_PROPERTIES_V1 = new Guid(0x8F052D93, 0xABCA, 0x4FC5, 0xA5, 0xAC, 0xB0, 0x1D, 0xF4, 0xDB, 0xE5, 0x98);
        public static readonly PropertyKey WPD_FUNCTIONAL_OBJECT_CATEGORY = PropertyKey.Create(WPD_FUNCTIONAL_OBJECT_PROPERTIES_V1, 2);
    }
}
