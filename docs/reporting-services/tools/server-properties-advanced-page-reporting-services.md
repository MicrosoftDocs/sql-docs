---
title: "Server Properties (Advanced Page) - Reporting Services | Microsoft Docs"
author: markingmyname
ms.author: maghan
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: tools
ms.topic: conceptual
ms.date: 01/15/2019
---

# Server Properties (Advanced Page) - Reporting Services

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

Use this page to set system properties on the report server. There are a number of ways to set system properties. This tool provides a graphical user interface so that you can set properties without having to write code.

To open this page, start SQL Server Management Studio, connect to a report server instance, right-click the report server name, and select **Properties**. Select **Advanced** to open this page.

## Options

**EnableMyReports**  
Indicates whether the My Reports feature is enabled. A value of **true** indicates that the feature is enabled.  

**MyReportsRole**  
The name of the role used when creating security policies on user's My Reports folders. The default value is **My Reports Role**.  

**EnableClientPrinting**  
Determines whether the RSClientPrint ActiveX control is available for download from the report server. The valid values are **true** and **false**. The default value is **true**. For more information about additional settings that are required for this control, see [Enable and Disable Client-Side Printing for Reporting Services](../../reporting-services/report-server/enable-and-disable-client-side-printing-for-reporting-services.md).  

**EnableExecutionLogging**  
Indicates whether report execution logging is enabled. The default value is **true**. For more information about the report server execution log, see [Report Server ExecutionLog and the ExecutionLog3 View](../../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md).  

**ExecutionLogDaysKept**  
The number of days to keep report execution information in the execution log. Valid values for this property include **-1** through **2**,**147**,**483**,**647**. If the value is **-1**, entries are not deleted from the Execution Log table. The default value is **60**.  

> [!NOTE]
> Setting a value of **0** *deletes* all entries from the execution log. A value of **-1** keeps the entries of the execution log and doesn't delete them.

**RDLXReportTimetout**
RDLX report *(Power View reports in a SharePoint Server)* processing timeout value, in seconds, for all reports managed in the report server namespace. This value can be overridden at the report level. If this property is set, the report server attempts to stop the processing of a report when the specified time has expired. Valid values are **-1** through **2**,**147**,**483**,**647**. If the value is **-1**, reports in the namespace do not time out during processing. The default value is **1800**.

**SessionTimeout**
The length of time, in seconds, that a session remains active. The default value is **600**.  

**SharePointIntegratedMode**  
This read-only property indicates the server mode. If this value is False, the report server runs in native mode.  

**SiteName**  
The name of the report server site displayed in the page title of the web portal. The default value is [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. This property can be an empty string. The maximum length is 8,000 characters.  

**StoredParametersLifetime**
Specifies the maximum number of days that a stored parameter can be stored. Valid values are **-1**, **+1** through **2,147,483,647**. The default value is **180** days.  

**StoredParametersThreshold**  
Specifies the maximum number of parameter values that can be stored by the report server. Valid values are **-1**, **+1** through **2,147,483,647**. The default value is **1500**.  

**UseSessionCookies**  
Indicates whether the report server should use session cookies when communicating with client browsers. The default value is **true**.  

**ExternalImagesTimeout**  
Determines the length of time within which an external image file must be retrieved before the connection is timed out. The default is **600** seconds.  

**SnapshotCompression**
A snapshot of the report server at that time.

**SnapshotCompression**  
Defines how snapshots are compressed. The default value is **SQL**. The valid values are as follows:

|Values|Description|
|---------|---------|
|**SQL**|Snapshots are compressed when stored in the report server database. This compression is the current behavior.|
|**None**|Snapshots are not compressed.|
|**All**|Snapshots are compressed for all storage options, which include the report server database or the file system.|

**SystemReportTimeout**  
The default report processing timeout value, in seconds, for all reports managed in the report server namespace. This value can be overridden at the report level. If this property is set, the report server attempts to stop the processing of a report when the specified time has expired. Valid values are **-1** through **2**,**147**,**483**,**647**. If the value is **-1**, reports in the namespace do not time out during processing. The default value is **1800**.  

**SystemSnapshotLimit**  
The maximum number of snapshots that are stored for a report. Valid values are **-1** through **2**,**147**,**483**,**647**. If the value is **-1**, there is no snapshot limit.  

**EnableIntegratedSecurity**  
Determines whether Windows-integrated security is supported for report data source connections. The default is **True**. The valid values are as follows:

|Values|Description|
|---------|---------|
|**True**|Windows-integrated security is enabled.|
|**False**|Windows-integrated security is not enabled. Report data sources that are configured to use Windows-integrated security will not run.|

**EnableLoadReportDefinition**  
Select this option to specify whether users can perform an unplanned report execution from a Report Builder report. Setting this option determines the value of the **EnableLoadReportDefinition** property on the report server.  

If you clear this option, the property is set to False. Report server won't generate clickthrough reports for reports that use a report model as a data source. Any calls to the LoadReportDefinition method are blocked.  

Turning off this option mitigates a threat whereby a malicious user launches a denial of service attack by overloading the report server with LoadReportDefinition requests.  

**EnableRemoteErrors**  
Includes external error information (for example, error information about report data sources) with the error messages that are returned for users who request reports from remote computers. Valid values are **true** and **false**. The default value is **false**. For more information, see [Enable Remote Errors &#40;Reporting Services&#41;](../../reporting-services/report-server/enable-remote-errors-reporting-services.md).  

**AccessControlAllowCredentials**  
Indicates whether the response to the client request can be exposed when the 'credentials' flag is set to true. The default value is **false**.

**AccessControlAllowHeaders**
A comma-separated list of headers that the server will allow when a client makes a request. This property can be an empty string, specifying * will allow all headers.

**AccessControlAllowMethods**
A comma-separated list of HTTP methods that the server will allow when a client makes a request. The default values are (GET, PUT, POST, PATCH, DELETE), specifying * will allow all methods.

**AccessControlAllowOrigin**
A comma-separated list of origins that the server will allow when a client makes a request. The default value is blank, which prevents all requests, specifying * will allow all origins when credentials are not set; if credentials are specified an explicit list of origins must be specified.

**AccessControlExposeHeaders**
A comma-separated list of headers that the server will expose to clients. The default value is blank.

**AccessControlMaxAge**
Specifies the number of seconds the results of the preflight request can be cached. The default value is 600 (10 minutes).

**AllowedResourceExtensionsForUpload** ***(Power BI Report Server only)***
Sets extensions that are allowed to be uploaded to the report server to avoid uploads of files like “*.exe” or “*.bin”

**EditSessionCacheLimit**  
Specifies the number of data cache entries that can be active in a report edit session. The default number is 5.  

**EditSessionTimeout**  
Specifies the number of seconds until a report edit session times out. The default value is 7200 seconds (two hours).  

**EnableCustomVisuals** ***(Power BI Report Server only)***
To enable the display of Power BI custom visuals. Values are True/False. *Default is True.*  

**ExecutionLogLevel**
Set the Execution Log Level. *Default is Normal.*

**InterProcessTimeoutMinutes**
Set the process timeout in minutes. *Default is 30.*

**MaxFileSizeMb**
Set the max file size of the report in MB. *Default is 1000.  Max is 2000.*

**ModelCleanupCycleminutes**
Set the model cleanup cycle in minutes. *Default is 15.*

**OfficeAccessTokenExpirationSeconds** ***(Power BI Report Server only)***
Set for how long you want the office access token to expire in seconds. *Default is 60.*

**OfficeOnlineDiscoveryURL** ***(Power BI Report Server only)***
Set the address of your Office Online Server instance for viewing Excel Workbooks.

**RequireIntune**
Requires Intune to access your organization's reports via the Power BI mobile app. *Default is False.*

**ScheduleRefreshTimeoutMinutes** ***(Power BI Report Server only)***
Set for how long you want the schedule refresh to time out. *Default is 120.*

**ShowDownloadMenu** 
Enables the client tools download menu. *Default is true.*

**SupportedHyperlinkSchemes** ***(Power BI Report Server only)***
Sets the scheme of Hyperlink actions that can be set. If someone wants to avoid javascript injection attacks they can remove the “javascript” scheme to prevent a report author from injecting something like javascript:eval(“window.location = ‘https://evilwebsite.com’;”)

**TimeInitialDelaySeconds**
Set for how long you want the initial time to be delayed in seconds. *Default is 60.*

**TrustedFileFormat**
Set all the external file formats that open within the browser under the Reporting Services portal site. External file formats not listed prompts to download the option in the browser. The default values are jpg, jpeg, jpe, wav, bmp, pdf, img, gif, json, mp4, web, png.

**EnablePowerBIReportExportData** ***(Power BI Report Server only)***  
Enable Power BI Report Server data export from Power BI visuals. Values are True, False.  Default is True.  

**ScheduleRefreshTimeoutMinutes** ***(Power BI Report Server only)***  
Data refresh timeout in minutes for scheduled refresh on Power BI reports with embedded AS models. Default is 120 minutes.

**EnableTestConnectionDetailedErrors**  
Indicates whether to send detailed error messages to the client computer when users test data source connections using the report server. The default value is **true**. If the option is set to **false**, only generic error messages are sent.

## See Also

[Set Report Server Properties &#40;Management Studio&#41;](../../reporting-services/tools/set-report-server-properties-management-studio.md)   
[Connect to a Report Server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)   
[Reporting Services Properties](../../reporting-services/report-server-web-service/net-framework/reporting-services-properties.md)   
[Report Server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)   
[Report Server System Properties](../../reporting-services/report-server-web-service/net-framework/reporting-services-properties-report-server-system-properties.md)   
[Script Deployment and Administrative Tasks](../../reporting-services/tools/script-deployment-and-administrative-tasks.md)   
[Enable and Disable My Reports](../../reporting-services/report-server/enable-and-disable-my-reports.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
