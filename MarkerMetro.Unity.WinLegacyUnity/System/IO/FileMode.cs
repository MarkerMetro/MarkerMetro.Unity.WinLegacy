namespace MarkerMetro.Unity.WinLegacy.IO
{
    public enum FileMode
    {
        Append,
        Create,
        CreateNew,
        Open,
        OpenOrCreate,
        Truncate
    }
#if !NETFX_CORE
    public static class FileModeExtensions
    {
        public static System.IO.FileMode ToSystemIO(this FileMode mode)
        {
            switch (mode)
            {
                case FileMode.Append:
                    return System.IO.FileMode.Append;
                case FileMode.Create:
                    return System.IO.FileMode.Create;
                case FileMode.CreateNew:
                    return System.IO.FileMode.CreateNew;
                case FileMode.Open:
                    return System.IO.FileMode.Open;
                case FileMode.OpenOrCreate:
                    return System.IO.FileMode.OpenOrCreate;
                case FileMode.Truncate:
                    return System.IO.FileMode.Truncate;
                default:
                    throw new System.ArgumentOutOfRangeException("Unknown FileMode");
            }
        }
    }
#endif
}
