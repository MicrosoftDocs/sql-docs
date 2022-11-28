---
title: "Server Properties Advanced Page | Microsoft Docs"
description: Use the Advanced Server Properties page to set system properties on the report server. This tool provides a graphical user interface so that you can set properties without writing code.
author: maggiesMSFT
ms.author: maggies
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.date: 10/17/2022
monikerRange: ">=sql-server-2016"
---

# Server Properties Advanced Page - Power BI Report Server & Reporting Services

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

Use this page to set system properties on the report server. There are a number of ways to set system properties. This tool provides a graphical user interface so that you can set properties without having to write code.

To open this page, start SQL Server Management Studio, connect to a report server instance, right-click the report server name, and select **Properties**. Select **Advanced** to open this page.

## Options

### AccessControlAllowCredentials

*Power BI Report Server, Reporting Services 2017 and later only* 

Indicates whether the response to the client request can be exposed when the `credentials` flag is set to true. The default value is **false**.

### AccessControlAllowHeaders

*Power BI Report Server, Reporting Services 2017 and later only* 

A comma-separated list of headers that the server will allow when a client makes a request. This property can be an empty string, specifying * will allow all headers.

### AccessControlAllowMethods

*Power BI Report Server, Reporting Services 2017 and later only* 

A comma-separated list of HTTP methods that the server will allow when a client makes a request. The default values are (GET, PUT, POST, PATCH, DELETE), specifying * will allow all methods.

### AccessControlAllowOrigin

*Power BI Report Server, Reporting Services 2017 and later only* 

A comma-separated list of origins that the server will allow when a client makes a request. The default value is blank, which prevents all requests, specifying * will allow all origins when credentials aren't set; if credentials are specified an explicit list of origins must be specified.

### AccessControlExposeHeaders

*Power BI Report Server, Reporting Services 2017 and later only* 

A comma-separated list of headers that the server will expose to clients. The default value is blank.

### AccessControlMaxAge

*Power BI Report Server, Reporting Services 2017 and later only* 

Specifies the number of seconds the results of the preflight request can be cached. The default value is 600 (10 minutes).

### AllowedResourceExtensionsForUpload

*Power BI Report Server, Reporting Services 2017 and later only* 

Set extensions of resources that can be uploaded to the report server. Extensions for built-in file types like &ast;.rdl and &ast;.pbix aren't required to be included. Default is "&ast;, &ast;.xml, &ast;.xsd, &ast;.xsl, &ast;.png, &ast;.gif, &ast;.jpg, &ast;.tif, &ast;.jpeg, &ast;.tiff, &ast;.bmp, &ast;.pdf, &ast;.svg, &ast;.rtf, &ast;.txt, &ast;.doc, &ast;.docx, &ast;.pps, &ast;.ppt, &ast;.pptx".

### CustomHeaders 

*Power BI Report Server January 2020, Reporting Services 2019 and later only*

Sets header values for all URLs matching the specified regex pattern. Users can update the CustomHeaders value with valid XML to set header values for selected request URLs. Admins can add any number of headers in the XML. By default in Reporting Services 2019, there are no custom headers and the value is blank. By default in Power BI Report Server January 2020 and later, the value is this:

```xml
<CustomHeaders>
    <Header>
        <Name>X-Frame-Options</Name>
        <Pattern>(?(?=.*api.*|.*rs:embed=true.*|.*rc:toolbar=false.*)(^((?!(.+)((\/api)|(\/(.+)(rs:embed=true|rc:toolbar=false)))).*$))|(^(?!(http|https):\/\/([^\/]+)\/powerbi.*$)))</Pattern>
        <Value>SAMEORIGIN</Value>
    </Header>
</CustomHeaders>
```

> [!NOTE]
> Too many headers may impact performance. 

We recommend validating the configuration of your topology to ensure the set of headers is compatible with your deployment of Reporting Services. It's possible to choose settings that cause errors in browsers if the browsers don't also have the appropriate settings. For example, you shouldn't add an HSTS configuration if your server isn't configured for https. Incompatible headers may result in browser rendering errors.

#### CustomHeaders XML format

```xml
<CustomHeaders>
    <Header>
        <Name>{Name of the header}</Name>
        <Pattern>{Regex pattern to match URLs}</Pattern>
        <Value>{Value of the header}</Value>
    </Header>
</CustomHeaders>
```

#### Setting the CustomHeaders property

- You can set it using [SetSystemProperties](/dotnet/api/reportservice2010.reportingservice2010.setsystemproperties) SOAP endpoint passing CustomHeaders property as parameter.
- You can use REST endpoint [UpdateSystemProperties](https://app.swaggerhub.com/apis/microsoft-rs/PBIRS/2.0#/System/UpdateSystemProperties):  `/System/Properties` passing CustomHeaders property

#### Example

The below example shows how to set the HSTS and other custom headers for URLs with matching regex pattern.

```xml
<CustomHeaders>
    <Header>
        <Name>Strict-Transport-Security</Name>
        <Pattern>(.+)\/Reports\/mobilereport(.+)</Pattern>
        <Value>max-age=86400; includeSubDomains=true</Value>
    </Header>
    <Header>
        <Name>Embed</Name>
        <Pattern>(.+)(/reports/)(.+)(rs:embed=true)</Pattern>
        <Value>True</Value>
    </Header>
</CustomHeaders>
```

The first header in the above XML adds `Strict-Transport-Security: max-age=86400; includeSubDomains=true` header to the matched requests.
- http://adventureworks/Reports/mobilereport/New%20Mobile%20Report - Regex matched and will set HSTS header
- http://adventureworks/ReportServer/mobilereport/New%20Mobile%20Report – Match Failed

The second header in above XML adds `Embed: True` header for URL which contains `/reports/` and `rs:embed=true` query parameter.
- https://adventureworks/reports/mobilereport/New%20Mobile%20Report?rs:embed=true - Match
- https://adventureworks/reports/mobilereport/New%20Mobile%20Report?rs:embed=false - Fail to Match

### CustomUrlLabel and CustomUrlValue

*Power BI Report Server, Reporting Services 2022 and later only*

Branding option to add a custom hyperlink. Default values are **empty**. 

|Values |Description  |
|---------|---------|
| CustomUrlLabel | Defines the text shown as the URL label in the top right navigation bar in the web portal (for example, `Go to Contoso`) |
| CustomUrlValue  | Defines the URL (for example, `http://www.contoso.com`) | 

### EditSessionCacheLimit
Specifies the number of data cache entries that can be active in a report edit session. The default number is 5.  

### EditSessionTimeout
Specifies the number of seconds until a report edit session times out. The default value is 7200 seconds (two hours). 

### EnableCDNVisuals 

*Power BI Report Server only* 

When enabled, Power BI reports load the latest certified custom visuals from a content delivery network (CDN) hosted by Microsoft. If your server doesn't have access to internet resources, you can turn off this option. In that case, custom visuals are loaded from the report that was published to the server. Default is **True**.  

###  EnableClientPrinting  
Determines whether the RSClientPrint ActiveX control is available for download from the report server. The valid values are **true** and **false**. The default value is **true**. For more information about additional settings that are required for this control, see [Enable and Disable Client-Side Printing for Reporting Services](../../reporting-services/report-server/enable-and-disable-client-side-printing-for-reporting-services.md).  

### EnableCustomVisuals 

*Power BI Report Server only* 

To enable the display of Power BI custom visuals. Values are True/False. *Default is True.*  

###  EnableExecutionLogging  
Indicates whether report execution logging is enabled. The default value is **true**. For more information about the report server execution log, see [Report Server ExecutionLog and the ExecutionLog3 View](../../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md).  

### EnableIntegratedSecurity
Determines whether Windows-integrated security is supported for report data source connections. The default is **True**. The valid values are as follows:

|Values|Description|
|---------|---------|
|**True**|Windows-integrated security is enabled.|
|**False**|Windows-integrated security isn't enabled. Report data sources that are configured to use Windows-integrated security will not run.|

### EnableLoadReportDefinition
Select this option to specify whether users can perform an unplanned report execution from a Report Builder report. Setting this option determines the value of the **EnableLoadReportDefinition** property on the report server.  

If you clear this option, the property is set to False. Report server won't generate clickthrough reports for reports that use a report model as a data source. Any calls to the LoadReportDefinition method are blocked.  

Turning off this option mitigates a threat whereby a malicious user launches a denial of service attack by overloading the report server with LoadReportDefinition requests.  

### EnableMyReports  
Indicates whether the My Reports feature is enabled. A value of **true** indicates that the feature is enabled.  

### EnablePowerBIReportExportData 

*Power BI Report Server only* 

Enable Power BI Report Server data export from Power BI visuals. Values are True, False.  Default is True. 

### EnablePowerBIReportExportUnderlyingData 

*Power BI Report Server only* 

Indicates whether or not a customer can export underlying data from Power BI visuals on Power BI Report Server. A value of True indicates that the feature is enabled.

### EnableRemoteErrors
Includes external error information (for example, error information about report data sources) with the error messages that are returned for users who request reports from remote computers. Valid values are **true** and **false**. The default value is **false**. For more information, see [Enable Remote Errors &#40;Reporting Services&#41;](../../reporting-services/report-server/enable-remote-errors-reporting-services.md).  

### EnableTestConnectionDetailedErrors
Indicates whether to send detailed error messages to the client computer when users test data source connections using the report server. The default value is **true**. If the option is set to **false**, only generic error messages are sent.

###  ExecutionLogDaysKept  
The number of days to keep report execution information in the execution log. Valid values for this property include **-1** through **2**,**147**,**483**,**647**. If the value is **-1**, entries aren't deleted from the Execution Log table. The default value is **60**.  

> [!NOTE]
> Setting a value of **0** *deletes* all entries from the execution log. A value of **-1** keeps the entries of the execution log and doesn't delete them.

### ExecutionLogLevel
Set the Execution Log Level. *Default is Normal.*

### ExternalImagesTimeout
Determines the length of time within which an external image file must be retrieved before the connection is timed out. The default is **600** seconds.  

### InterProcessTimeoutMinutes

*Power BI Report Server, Reporting Services 2019 and later only* 

Set the process timeout in minutes. *Default is 30.*

### LogClientIPAddress

*Power BI Report Server, Reporting Services 2022 and later only* 

Exclude/included Client IP Address when INFO Logging in Enabled. Default is **false**.

|Values |Description  |
|---------|---------|
|**True** | Client IP is logged  |
| **False** | Client IP is not logged |

### MaxFileSizeMb
Set the max file size of the report in MB. *Default is 1000.  Max is 2000.*

### ModelCleanupCycleMinutes 

*Power BI Report Server only* 

Set the frequency to check for unused models in memory in minutes. *Default is 15.*

### ModelExpirationMinutes 

*Power BI Report Server only* 

Set the frequency when unused models are evicted from memory in minutes. *Default is 60.*

###  MyReportsRole  
The name of the role used when creating security policies on user's My Reports folders. The default value is **My Reports Role**.  

### OfficeAccessTokenExpirationSeconds 

*Power BI Report Server, Reporting Services 2019 and later only* 

Set for how long you want the office access token to expire in seconds. *Default is 60.*

### OfficeOnlineDiscoveryURL

*Power BI Report Server only*

Set the address of your Office Online Server instance for viewing Excel Workbooks.

### RDLXReportTimetout
RDLX report *(Power View reports in a SharePoint Server)* processing timeout value, in seconds, for all reports managed in the report server namespace. This value can be overridden at the report level. If this property is set, the report server attempts to stop the processing of a report when the specified time has expired. Valid values are **-1** through **2**,**147**,**483**,**647**. If the value is **-1**, reports in the namespace don't time out during processing. The default value is **1800**.

> [!NOTE]
> Power View support is no longer available after SQL Server 2017.

### RequireIntune

*Power BI Report Server, Reporting Services 2017 and later only* 

Requires Intune to access your organization's reports via the Power BI mobile app. *Default is False.*

### RestrictedResourceMimeTypeForUpload

*Power BI Report Server January 2019, Reporting Services 2017 and later only* 

Set of mime types users aren't allowed to upload content with. Any resources that are already stored with a restricted mime type can only be downloaded as an application/octet-stream instead of being opened/executed by the browser.  By default, there are no restricted items in this list, but we recommended that organizations populate this to provide the most secure experience.

### ScheduleRefreshTimeoutMinutes 

*Power BI Report Server only* 

Data refresh timeout in minutes for scheduled refresh on Power BI reports with embedded AS models. Default is 120 minutes.

### SessionTimeout
The length of time, in seconds, that a session remains active. The default value is **600**.  

### SharePointIntegratedMode
This read-only property indicates the server mode. If this value is False, the report server runs in native mode.  

### ShowDownloadMenu

*Power BI Report Server, Reporting Services 2017 and later only* 

Enables the client tools download menu. *Default is true.*

### SiteName
The name of the report server site displayed in the page title of the web portal. The default value is [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. This property can be an empty string. The maximum length is 8,000 characters.  

### SnapshotCompression
Defines how snapshots are compressed. The default value is **SQL**. The valid values are as follows:

|Values|Description|
|---------|---------|
|**SQL**|Snapshots are compressed when stored in the report server database. This compression is the current behavior.|
|**None**|Snapshots aren't compressed.|
|**All**|Snapshots are compressed for all storage options, which include the report server database or the file system.|

### StoredParametersLifetime
Specifies the maximum number of days that a stored parameter can be stored. Valid values are **-1**, **+1** through **2,147,483,647**. The default value is **180** days.  

### StoredParametersThreshold
Specifies the maximum number of parameter values that can be stored by the report server. Valid values are **-1**, **+1** through **2,147,483,647**. The default value is **1500**.  

### SupportedHyperlinkSchemes 

*Power BI Report Server January 2019, Reporting Services 2019 and later only* 

Sets a comma separated list of the URI schemes allowed to be defined on Hyperlink actions that are allowed to be rendered or "&ast;" to enable all hyperlink schemes. For example, setting "http, https" would allow hyperlinks to "https://www. contoso.com", but would remove hyperlinks to "mailto:bill@contoso.com" or "javascript:window.open('www.contoso.com', '_blank')". Default is "&ast;".

### SystemReportTimeout
The default report processing timeout value, in seconds, for all reports managed in the report server namespace. This value can be overridden at the report level. If this property is set, the report server attempts to stop the processing of a report when the specified time has expired. Valid values are **-1** through **2**,**147**,**483**,**647**. If the value is **-1**, reports in the namespace don't time out during processing. The default value is **1800**.  

### SystemSnapshotLimit
The maximum number of snapshots that are stored for a report. Valid values are **-1** through **2**,**147**,**483**,**647**. If the value is **-1**, there is no snapshot limit.  

### TileViewByDefault

*Power BI Report Server, Reporting Services 2022 and later only* 

List view by default option in catalog.  It defines if Tiles or List view is selected for all users by default. The default is **True** for Tile view. 

### TimerInitialDelaySeconds

*Power BI Report Server, Reporting Services 2017 and later only* 

Set for how long you want the initial time to be delayed in seconds. *Default is 60.*

### TrustedFileFormat

*Power BI Report Server, Reporting Services 2017 and later only* 

Set all the external file formats that open within the browser under the Reporting Services portal site. External file formats not listed prompts to download the option in the browser. The default values are jpg, jpeg, jpe, wav, bmp, pdf, img, gif, json, mp4, web, png.

### UseSessionCookies
Indicates whether the report server should use session cookies when communicating with client browsers. The default value is **true**.  

## See Also

[Set Report Server Properties &#40;Management Studio&#41;](../../reporting-services/tools/set-report-server-properties-management-studio.md)   
[Connect to a Report Server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)   
[Reporting Services Properties](../../reporting-services/report-server-web-service/net-framework/reporting-services-properties.md)   
[Report Server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)   
[Report Server System Properties](../../reporting-services/report-server-web-service/net-framework/reporting-services-properties-report-server-system-properties.md)   
[Script Deployment and Administrative Tasks](../../reporting-services/tools/script-deployment-and-administrative-tasks.md)   
[Enable and Disable My Reports](../../reporting-services/report-server/enable-and-disable-my-reports.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
