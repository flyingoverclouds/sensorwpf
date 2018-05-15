using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorWpf
{
    public static class SensorErrors
    {
        public const int ERROR_NO_DATA = unchecked((int)0x800700E8);
        public const int TYPE_E_TYPEMISMATCH = unchecked((int)0x80028CA0);
        public const int ERROR_NOT_FOUND = unchecked((int)0x80070490);
    }
}
