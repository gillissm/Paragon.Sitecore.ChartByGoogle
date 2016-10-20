# Sitecore Experience Analytics Google Chart

Why require your team to login to multiple systems just to review collected analytics? 

This solution contains a SPEAK 2.0 compontent that allows for custom Google Analyitc reports to be displayed within a dashboard page right in xAnalytics.

## Short Description
Sitecore SPEAK 2.0 custom compontent for displaying Google Analytic reports.

## Solution Requirements
* Visual Studio 2015
* .NET 4.5.2
* [Access to the Sitecore NuGet feed](https://doc.sitecore.net/sitecore_experience_platform/developing/developing_with_sitecore/sitecore_public_nuget_packages_faq), https://doc.sitecore.net/sitecore_experience_platform/developing/developing_with_sitecore/sitecore_public_nuget_packages_faq
* Optional Team Development For Sitecore (TDS)
* Default build is targeting Sitecore 8.2 Initial Release (rev. 160729)
    ** This is the MASTER branch
    ** Sitecore_81 branch targets Sitecore 8.1 update 3, but should run agianst all 8.1 instances
* Sitecore site built on MVC

## What's Included
* TDS project for Sitecore artifacts
* Two branches of code
    * MASTER - targets Sitecore 8.2 Initial Release, and is built using the new dependcy injection pattern
    * Sitecore_81 - targets Sitecore 8.1 update 3, and should work against any version that supports SPEAK UI 2.0
* Sitecore package as found in _Documents folder
    * If you are running Sitecore 8.1 install the Sitecore package 'Paragon.Sitecore.ChartByGoogle_8.1.zip'
    * If you are running Sitecore 8.2+ install the 'Paragon Sitecore ChartByGoogle_82-1.zip'

### Sitecore artifacts
When either installing from the package or syncing from TDS the following items will be added to your Sitecore content tree.
All items are added to the CORE database!

* Custom SPEAK UI charting rendering 
    * db:CORE -> sitecore \ client \ Your Apps \ Paragon_GoogleAnalytics \ Layouts \ Renderings \ ChartByGoogle
* Rendering Parameters to support custom component
    * db:CORE -> sitecore \ client \ Your Apps \ Paragon_GoogleAnalytics \ Layouts \ Renderings \ ChartByGoogle_Paramters
* Rendering Parameter base template
    * db:CORE -> sitecore \ client \ Your Apps \ Paragon_GoogleAnalytics \ Templates \ GoogleReportBaseParameters
* SPEAK UI 2.0 Dashboard Page Branch template
    * db:CORE -> sitecore \ client \ Your Apps \ Paragon_GoogleAnalytics \ Templates \ Branches \ Speak20 \ Dashboard20
* Sample Dashboard Page with a sinlge ChartByGoogle control
    * db:CORE -> sitecore \ client \ Your Apps \ Paragon_GoogleAnalytics \ Layouts \ Renderings \ ChartByGoogle
    * This page is based on the above custom SPEAK UI 2.0 branch template

### File System
The deployment involves the following files on the file system:
* < webroot > / bin / Newtonsoft.Json.dll
    * this will be version 7.0.0 and is required by Google's assemblies
* < webroot > / bin / Google.Apis.Analytics.v3.dll
* < webroot > / bin / Google.Apis.AnalyticsReporting.v4.dll
* < webroot > / bin / Google.Apis.Auth.dll
* < webroot > / bin / Google.Apis.Auth.PlatformServices.dll
* < webroot > / bin / Google.Apis.Core.dll
* < webroot > / bin / Google.Apis.dll
* < webroot > / bin / Google.Apis.PlatformServices.dll
* < webroot > / bin / Paragon.GoogleAnalytics.Factory.dll
* < webroot > / bin / Paragon.GoogleAnalytics.Repository.dll
* < webroot > / bin / Paragon.Sitecore.GoogleAnalyticsChart.dll
* < webroot > / App_Config / Include / z.Pargon.GoogleAnalyticsChart / z.Paragon.GoogleAnalyticsChart.config
* < webroot > / sitecore / shell / client / Applications / GoogleAnalytics / Components / ChartByGoogle / ChartByGoogle.cshtml
* < webroot > / sitecore / shell / client / Applications / GoogleAnalytics / Components / ChartByGoogle.js
* < webroot > / sitecore / shell / client / Applications / GoogleAnalytics / Scripts / GA.js

## How to Use
1. Install the included Sitecore package or sync via TDS
    a. This only should be installed on a CM server!
2. Login to Google Anayltics and create a service account, see https://developers.google.com/identity/protocols/OAuth2ServiceAccount
3. Save the .JSON  and .P12 files provided to the data folder on the CM server
4. Update the following values in z.Paragon.GoogleAnalyticsChart.config 
    a. JsonService with the file name of the .JSON
    b. P12Service with the file name of .P2 file
    c. AccountEmail wiht the email address supplied as part of the service account created
5. The Google API assemblies have a dependicie on Newtonsoft.Json verision 7 so you must update your web.config with the following
``` xml
      <dependentAssembly>
        <assemblyIdentity name="Newtonsoft.Json" publicKeyToken="30ad4fe6b2a6aeed" />
        <bindingRedirect oldVersion="0.0.0.0-6.0.0.0" newVersion="7.0.0.0" />
      </dependentAssembly>
```
6. Open Sitecore Expierience Analytics and you should find a new report page named 'Google Anayltics'
7. Add the new control as needed to new pages. 

## Licensed Under
MIT License

Copyright (c) 2016 Scott Gillis

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.


Severity	Code	Description	Project	File	Line	Suppression State
Error		Unable to resolve dependencies. 'Newtonsoft.Json 6.0.8' is not compatible with 'Google.Apis.Core 1.17.0 constraint: Newtonsoft.Json (>= 7.0.1)'.