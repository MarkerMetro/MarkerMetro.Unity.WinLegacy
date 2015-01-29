## Getting Started

Unity Plugin for Windows 8.1 (including Windows Universal) and Windows Phone 8.0 used when porting existing code bases which have used Mono/.Net APIs not supported by WinRT. 

Not all unsupported WinRT/Windows Phone APIS are implemented, or implemented well by Unity. Where this is the case, we have implemented this plugin to help. 

## Prerequisites

- Visual Studio 2013
- Unity 4.6.1f1 (tested only on this version but should work on other 4.x builds)

## Getting Latest

### Build Latest from Source

We recommend using the latest stable tagged release, or you can build straight off the head if you are brave.

Configure the solution to Release | Any CPU and Rebuild.

You can then copy the folder contents as follows:

- /MarkerMetro.Unity.WinLegacyMetro/bin/Release > /Assets/Plugins/Metro/
- /MarkerMetro.Unity.WinLegacyUnity/bin/Release > /Assets/Plugins/
- /MarkerMetro.Unity.WinLegacyWP8/bin/Release > /Assets/Plugins/WP8/

### Download Latest Stable Binaries

Alternatively, you can download latest from [Nuget](https://www.nuget.org/api/v2/package/MarkerMetro.Unity.WinLegacy)

Extract the files from the package and copy the folder contents as follows:

- /lib/netcore45/ > /Assets/Plugins/Metro/
- /lib/net35 > /Assets/Plugins/
- /lib/windowsphone8 > /Assets/Plugins/WP8/

Note: The Metro output will work fine for Universal projects with both Windows 8.1 and Windows Phone 8.1

## Initialize the Plugins

Within your Windows application, just need to ensure you initialize the plugin appropriately with examples as follows:

For Windows Universal Apps (Windows 8.1/Windows Phone 8.1):
https://github.com/MarkerMetro/MarkerMetro.Unity.WinShared/blob/master/WindowsSolutionUniversal/UnityProject/UnityProject.Shared/App.xaml.cs#L204

For Windows 8.1 Apps:
https://github.com/MarkerMetro/MarkerMetro.Unity.WinShared/blob/master/WindowsSolution/WindowsStore/UnityProject/App.xaml.cs#L130

For Windows Phone 8.0 Apps:
https://github.com/MarkerMetro/MarkerMetro.Unity.WinShared/blob/master/WindowsSolution/WindowsPhone/UnityProject/MainPage.xaml.cs#L106

## Use WinShared to make things easier

If you are starting a new port and/or you want the best ongoing Unity integration with WinLegacy and related plugins, consider [MarkerMetro.Unity.WinShared](https://github.com/MarkerMetro/MarkerMetro.Unity.WinShared). 

This will provide features such as:

- Initialization included within Windows projects provided
- Unity menu integration allowing you to get the latest stable version automatically from (Nuget)[https://www.nuget.org/packages/MarkerMetro.Unity.WinLegacy/]
- Unity menu integration for using local solution with automatic copy of build output into correct Unity plugin folders



