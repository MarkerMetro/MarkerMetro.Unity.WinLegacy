using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
#if NETFX_CORE
using System.Threading.Tasks;
using Windows.ApplicationModel;
#endif

namespace MarkerMetro.Unity.WinLegacy
{
    public class AppDomain
    {
        private static readonly AppDomain instance = new AppDomain();

        private Assembly[] assemblies;

        public static AppDomain CurrentDomain
        {
            get { return instance; }
        }

        public Assembly[] GetAssemblies()
        {
#if NETFX_CORE
            if(assemblies == null)
            {
                var t = GetAssembliesAsync();
                t.Wait();
                assemblies = t.Result.ToArray();
            }
            return assemblies;
#else
            throw new NotImplementedException();
#endif
        }

#if NETFX_CORE
        private async Task<IEnumerable<Assembly>> GetAssembliesAsync()
        {
            var folder = Package.Current.InstalledLocation;
            var list = new List<Assembly>();
            foreach(var file in await folder.GetFilesAsync())
            {
                try
                {
                    if (file.FileType == ".dll")
                    {
                        var assemblyName = new AssemblyName(file.DisplayName);
                        var assembly = Assembly.Load(assemblyName);
                        list.Add(assembly);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return list;
        }
#endif
    }
}