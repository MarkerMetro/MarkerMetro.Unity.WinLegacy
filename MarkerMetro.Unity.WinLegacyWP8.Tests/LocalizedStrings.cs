using MarkerMetro.Unity.WinLegacyWP8.Tests.Resources;

namespace MarkerMetro.Unity.WinLegacyWP8.Tests
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return _localizedResources; } }
    }
}