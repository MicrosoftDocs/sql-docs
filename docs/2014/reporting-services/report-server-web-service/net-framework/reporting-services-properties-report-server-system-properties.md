---
title: "Report Server System Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "report servers [Reporting Services], properties"
  - "system-specific properties [Reporting Services]"
ms.assetid: cd874117-00e5-4ae6-8629-eb9ba9f40478
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Report Server System Properties
  The following system property names are reserved. You cannot create user-defined properties of the same name. You can read or modify many of these properties using the Web service methods.  
  
## Properties  
  
|Property|Description|  
|--------------|-----------------|  
|SiteName|The name of the report server site displayed on the user interface. The default value is `Microsoft Report Server`. This property can be an empty string. The maximum length is 8,000 characters.|  
|SystemSnapshotLimit|The maximum number of snapshots that are stored for a report. Valid values are `-1` through `2`,`147`,`483`,`647`. If the value is `-1`, there is no snapshot limit.|  
|SystemReportTimeout|The default report processing timeout value, in seconds, for all reports managed in the report server namespace. This value can be overridden at the report level. If this property is set, the report server attempts to stop the processing of a report when the specified time has expired. Valid values are `-1` through `2`,`147`,`483`,`647`. If the value is `-1`, reports in the namespace do not time out during processing. The default value is `1800`.|  
|UseSessionCookies|Indicates whether the report server should use session cookies when communicating with client browsers. The default value is `true`.|  
|SessionTimeout|The length of time, in seconds, that a session remains active. The default value is `600`.|  
|EnableMyReports|Indicates whether the My Reports feature is enabled. A value of `true` indicates that the feature is enabled.|  
|MyReportsRole|The name of the role used when creating security policies on user's My Reports folders. The default value is `My Reports Role`.|  
|EnableExecutionLogging|Indicates whether report execution logging is enabled. The default value is `true`.|  
|ExecutionLogDaysKept|The number of days to keep report execution information in the execution log. Valid values for this property include `0` through `2`,`147`,`483`,`647`. If the value is `0` entries are not deleted from the Execution Log table. The default value is `60`.|  
|SnapshotCompression|Defines how snapshots are compressed. The default value is `SQL`. The valid values are as follows:<br /><br /> `SQL` = Snapshots are compressed when stored in the report server database. This is the current behavior.<br /><br /> **None** = Snapshots are not compressed.<br /><br /> `All` = Snapshots are compressed for all storage options, which include the report server database or the file system.|  
|EnableClientPrinting|Determines whether the RSClientPrint ActiveX control is available for download from the report server. The valid values are `true` and `false`. The default value is `true`. For more information about additional settings that are required for this control, see [Enable and Disable Client-Side Printing for Reporting Services](../../report-server/enable-and-disable-client-side-printing-for-reporting-services.md).|  
|EnableIntegratedSecurity|Determines whether integrated security is supported for report data source connections. The default is `True`. The valid values are as follows:<br /><br /> `True` = Integrated security is enabled.<br /><br /> `False` = Integrated security is not enabled. Report data sources that are configured to use integrated security will not run.|  
|EnableRemoteErrors|Includes external error information (for example, error information about report data sources) with the error messages that are returned for users who request reports from remote computers. Valid values are `true` and `false`. The default value is `false`. For more information, see [Enable Remote Errors &#40;Reporting Services&#41;](../../report-server/enable-remote-errors-reporting-services.md).|  
  
## See Also  
 <xref:ReportService2010.ReportingService2010.GetSystemProperties%2A>   
 <xref:ReportService2010.ReportingService2010.SetSystemProperties%2A>   
 [Building Applications Using the Web Service and the .NET Framework](building-applications-using-the-web-service-and-the-net-framework.md)   
 [Report Server Web Service](../report-server-web-service.md)   
 [Technical Reference &#40;SSRS&#41;](../../technical-reference-ssrs.md)  
  
  
