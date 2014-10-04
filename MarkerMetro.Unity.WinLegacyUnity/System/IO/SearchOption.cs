using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    // Summary:
    //     Specifies whether to search the current directory, or the current directory
    //     and all subdirectories.
#if !NETFX_CORE && !WINDOWS_PHONE
    [Serializable]
#endif
    //[ComVisible(true)]
    public enum SearchOption
    {
        // Summary:
        //     Includes only the current directory in a search operation.
        TopDirectoryOnly = 0,
        //
        // Summary:
        //     Includes the current directory and all its subdirectories in a search operation.
        //     This option includes reparse points such as mounted drives and symbolic links
        //     in the search.
        AllDirectories = 1,
    }
}
