MarkerMetro.Unity.WinIntegration
================================

Unity Editor (.Net 3.5), Windows 8.1 and Windows Phone 8 plugin libraries to help with missing or changes legacy .net classes in Windows apps. 

Why?
================================
Not all unsupported WinRT/Windows Phone APIS are implemented, or implemented well by Unity. 

Where this is the case, we have implemented this plugin to help

How?
================================
This library is published on the Marker Metro NuGet Feed (https://github.com/MarkerMetro/MarkerMetro.ProcessAutomation/wiki)

To update and use a NuGet plugin on a project see an example here:
https://github.com/MarkerMetro/SportsJeopardy#marker-metro-nuget-access

Generally speaking once set up, you can push changes, run a new build via the build server

1. Push Changes
2. Run a new build via the games build server
3. Run the bat file in your project which will copy new versions into Unity plugins folders

## Building to Dependent Projects

WinLegacy.proj is a helper MSBuild project to quickly build and deploy built dlls to dependant projects.
Running following command-line: "MSBuild WinLegacy.proj" will rebuild all projects in this solution and deploy them to LitJson and JsonFx
sub-projects, provided that they are on the same level as root of this project.

Running "MSBuild WinLegacy.proj /p:PublishDir=C:\Test" will build & publish to C:\Test (assuming that that folder contains standard
References/[Platform] sub-structure). 

