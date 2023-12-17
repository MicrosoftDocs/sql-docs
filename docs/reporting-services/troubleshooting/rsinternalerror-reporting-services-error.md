---
title: "rsInternalError - Reporting Services error"
description: "In this error reference page, learn about event ID 'rsInternalError': An internal error occurred on the report server. For more information, see the error log."
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: troubleshooting
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "rsInternalError"
---
# rsInternalError - Reporting Services error
    
## Details  
  
|Category|Value|  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|rsInternalError|  
|Event Source|Microsoft.ReportingServices.Diagnostics.Utilities.ErrorStrings|  
|Component|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|  
|Message Text|An internal error occurred on the report server. For more information, see the error log.|  
  
## Explanation  
 This error is generic and often followed by a more descriptive error that provides more detail.  
  
 Internal errors are uncommon. If you get this error, more information is available in report server trace logs. In addition, if you're running as local administrator on the same computer on which the error occurs, you can view the call stack for more information.  
  
## User action  
 To determine the specific cause for this message, review the report server log files, which are located at \Microsoft SQL Server\MSRS12.\\&lt;instancename&gt;\Reporting Services\LogFiles. For more information, see [Reporting Services log files and sources](../../reporting-services/report-server/reporting-services-log-files-and-sources.md).  
  
 To view the call stack, right-click the page on which the error occurs and point to **View Source**. Viewing the call stack requires administrator permissions on the same computer on which the error occurs.  
  
 If there's no additional information available, you can try your request again.  
  
## Internal-only  
  
## Related content  
 [Start and stop the report server service](../../reporting-services/report-server/start-and-stop-the-report-server-service.md)  
  
  
