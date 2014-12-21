MarkerMetro.Unity.WinLegacy
================================

Unity Editor (.Net 3.5), Windows 8.1 and Windows Phone 8 plugin libraries to help with missing or changes legacy .net classes in Windows apps. 

Why?
================================
Not all unsupported WinRT/Windows Phone APIS are implemented, or implemented well by Unity. 

Where this is the case, we have implemented this plugin to help

How?
================================
The latest stable version of this library is published on Nuget and tagged on this repo (TODO public link).

We recommend you look at MarkerMetro.Unity.WinShared (TODO link). This has Unity menu integration allowing you to get the latest from NuGet automatically as well as use local versions of this plugin by specifying the repository root on your local PC. 

If you are not using WinShared, simply ensure you initialize the plugin's Dispatcher for marshalling threads:

You can use the App.xaml.cs for Windows 8.1 when AppCallbacks has been initialized:
https://github.com/MarkerMetro/SportsJeopardy/blob/windows/WindowsSolution/WindowsStoreApps/Sports%20Jeopardy!/App.xaml.cs

And in Windows Phone 8, you can use Mainpage.xaml.cs in the Unity_Loaded handler:
https://github.wdig.com/MarkerMetro/BlockSumProduction/blob/windows/WindowsSolution/WindowsStoreApps/LostLight/MainPage.xaml.cs

