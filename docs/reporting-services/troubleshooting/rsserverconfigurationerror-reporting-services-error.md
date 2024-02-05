---
title: "rsServerConfigurationError - Reporting Services error"
description: "In this error reference page, learn about event ID 'rsServerConfigurationError': The report server encountered a configuration error."
author: maggiesMSFT
ms.author: maggies
ms.date: 03/20/2017
ms.service: reporting-services
ms.subservice: troubleshooting
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "rsServerConfigurationError"
---
# rsServerConfigurationError - Reporting Services error
    
## Details  
  
|Category|Value|  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|rsServerConfiguration|  
|Event Source|Microsoft.ReportingServices.Diagnostics.Utilities.ErrorStrings|  
|Component|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|  
|Message Text|The report server encountered a configuration error.|  
  
## Explanation  
 This error is a general purpose error that occurs when either the report server or a report authoring tool has invalid configuration settings. There's a second message that usually accompanies this error. The second message states the actual cause of the error.  
  
 The following list summarizes possible causes:  
  
-   The RSReportServer.config or RSReportDesigner.config file can't be found or read.  
  
-   XML elements in either configuration file are missing or invalid.  
  
-   Values for one or more XML elements are missing or invalid.  
  
-   Registry settings are invalid.  
  
## User action  
 If this error began to occur after you manually edited a configuration file, remove your changes and enter the previous value, or restore a previous version if you have a backup.  
  
 To review other error message information that accompanies the **rsServerConfiguration** error, review the report server trace log files, which are located at \Microsoft SQL Server\MSRS12.\\&lt;instancename&gt;\Reporting Services\LogFiles. For more information, see [Reporting Services log files and sources](../../reporting-services/report-server/reporting-services-log-files-and-sources.md).  
  
## Internal-only  
  
## Related content
 [Reporting Services configuration files](../../reporting-services/report-server/reporting-services-configuration-files.md)   
 [Modify a Reporting Services configuration file &#40;RSreportserver.config&#41;](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md)  
  
  
