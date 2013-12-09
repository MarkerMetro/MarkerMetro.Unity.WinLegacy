MarkerMetro.Unity.WinLegacy
==============================

Unity Editor (.Net 3.5), Windows 8.1 and Windows Phone 8 plugin libraries to help with missing or changes legacy .net classes in Windows apps. 

Build the solution and copy the resultant dlls into the following folder structure in your Unity project EXACTLY as follows:

MarkerMetro.Unity.WinLegacyUnity Project > /Assets/Plugins/MarkerMetro.Unity.WinLegacy.dll

MarkerMetro.Unity.WinLegacyMetro Project > /Assets/Plugins/Metro/MarkerMetro.Unity.WinLegacy.dll

MarkerMetro.Unity.WinLegacyWP8 Project > /Assets/Plugins/WP8/MarkerMetro.Unity.WinLegacy.dll

------------------------------
WinLegacy.proj is a helper MSBuild project to quickly build and deploy built dlls to dependant projects.
Running following command-line: "MSBuild WinLegacy.proj" will rebuild all projects in this solution and deploy them to LitJson and JsonFx
sub-projects, provided that they are on the same level as root of this project.

Running "MSBuild WinLegacy.proj /p:PublishDir=C:\Test" will build & publish to C:\Test (assuming that that folder contains standard
References/[Platform] sub-structure).
