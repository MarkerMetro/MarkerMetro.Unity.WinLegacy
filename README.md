## Getting Started

Unity Plugin for Windows 8.1 (including Windows Universal) and Windows Phone 8.0 used when porting existing code bases which have used Mono/.Net APIs not supported by WinRT. 

Not all unsupported WinRT/Windows Phone APIS are implemented, or implemented well by Unity. Where this is the case, we have implemented this plugin to help. If they are supported by Unity.

## Prerequisites
Visual Studio 2013
Unity 4.6.1f1 (tested only on this version but should work on other 4.x builds)

##Building Latest

We recommend using the latest stable tagged release.

Configure the solution to Release | Any CPU and Rebuild.

You can then simply copy the outputted dlls to the following folders in Unity

MarkerMetro.Unity.WinLegacyMetro > /Assets/Plugins/Metro
MarkerMetro.Unity.WinLegacyUnity > /Assets/Plugins
MarkerMetro.Unity.WinLegacyWP8 > /Assets/Plugins/WP8

## Initialize the Plugins

Without your Windows application, just need to ensure you initialize the plugin appropriately with examples as follows:

For Windows Universal Apps (Windows 8.1/Windows Phone 8.1):
https://github.com/MarkerMetro/MarkerMetro.Unity.WinShared/blob/master/WindowsSolutionUniversal/UnityProject/UnityProject.Shared/App.xaml.cs#L204

For Windows 8.1 Apps:
https://github.com/MarkerMetro/MarkerMetro.Unity.WinShared/blob/master/WindowsSolution/WindowsStore/UnityProject/App.xaml.cs#L130

For Windows Phone 8.0 Apps:
https://github.com/MarkerMetro/MarkerMetro.Unity.WinShared/blob/master/WindowsSolution/WindowsPhone/UnityProject/MainPage.xaml.cs#L106

## Use WinShared to make things easier

If you are starting a new port and/or you want the best ongoing Unity integration with WinLegacy and related plugins, consider [MarkerMetro.Unity.WinShared[(https://github.com/MarkerMetro/MarkerMetro.Unity.WinShared). 

This will provide features such as Unity menu integration allowing you to get the latest plugins automatically from Nuget as well as allowing you to easily use and debug forked versions of WinLegacy for yourself. It'll automatic copy your build output into the right Unity plugin folders automatically, what's not to like?



