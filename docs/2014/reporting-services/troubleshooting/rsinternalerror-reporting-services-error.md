---
title: "rsInternalError - Reporting Services Error | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "rsInternalError"
ms.assetid: 52613d52-fc78-4870-93f0-7d393ab9c335
author: markingmyname
ms.author: maghan
manager: kfile
---
# rsInternalError - Reporting Services Error
    
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|rsInternalError|  
|Event Source|Microsoft.ReportingServices.Diagnostics.Utilities.ErrorStrings|  
|Component|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|  
|Message Text|An internal error occurred on the report server. See the error log for more details.|  
  
## Explanation  
 This is a generic error message that is often followed by a more descriptive error that provides more detail.  
  
 Internal errors are uncommon. If you get this error, more information is available in report server trace logs. In addition, if you are running as local administrator on the same computer on which the error occurs, you can view the call stack for more information.  
  
## User Action  
 To determine the specific cause for this message, review the report server log files, which are located at \Microsoft SQL Server\MSRS12.\<instancename >\Reporting Services\LogFiles. For more information, see [Reporting Services Log Files and Sources](../report-server/reporting-services-log-files-and-sources.md).  
  
 To view the call stack, right-click the page on which the error occurs and point to **View Source**. Viewing the call stack requires administrator permissions on the same computer on which the error occurs.  
  
 If there is no additional information available, you can try your request again.  
  
## Internal-Only  
  
## See Also  
 [Start and Stop the Report Server Service](../report-server/start-and-stop-the-report-server-service.md)  
  
  
