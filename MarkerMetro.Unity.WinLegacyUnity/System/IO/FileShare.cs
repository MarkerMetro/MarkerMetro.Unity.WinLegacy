using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkerMetro.Unity.WinLegacy.Plugin.IO
{
    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.io.fileshare.aspx.
    /// </summary>
    public enum FileShare
    {
        Delete,
        Inheritable,
        None,
        Read,
        ReadWrite,
        Write,
    }
}
