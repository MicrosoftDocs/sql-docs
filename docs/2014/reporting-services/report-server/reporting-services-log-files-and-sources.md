---
title: "Reporting Services Log Files and Sources | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "troubleshooting [Reporting Services], log files"
  - "logs [Reporting Services]"
  - "logs [Reporting Services], about log files"
  - "report servers [Reporting Services], log files"
  - "report server log files"
  - "files [Reporting Services], logs"
ms.assetid: 80ef0acc-cbef-49d0-87e7-844e3ce19604
author: markingmyname
ms.author: maghan
manager: kfile
---
# Reporting Services Log Files and Sources
  A [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] report server and report server environment support a variety of log destinations to record information about server operations and status. There are two basic categories of logging, execution logging and trace logging. Execution logging includes information about report execution statistics, auditing, performance diagnosis and optimization. Trace logging is information about error messages and general diagnostics.  
  
 **[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] SharePoint mode | [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] Native mode  
  
 The following table provides links to additional information about each log, including the log location and how to view the log contents.  
  
|Log|Description|  
|---------|-----------------|  
|[Report Server Execution Log and the ExecutionLog3 View](report-server-executionlog-and-the-executionlog3-view.md)|The execution log is a SQL Server view stored in the report server database.<br /><br /> The report server execution log contains data about specific reports, including when a report was run, who ran it, where it was delivered, and which rendering format was used.|  
|SharePoint trace log|For report servers running in SharePoint, the SharePoint trace logs contains [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] information. You can also configure [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] specific information for the SharePoint Unified Logging service. For more information, see [Turn on Reporting Services events for the SharePoint trace log &#40;ULS&#41;](turn-on-reporting-services-events-for-the-sharepoint-trace-log-uls.md)|  
|[Report Server Service Trace Log](report-server-service-trace-log.md)|The service trace log contains very detailed information that is useful if you are debugging an application or investigating an issue or event.<br /><br /> `C:\Program Files\Microsoft SQL Server\MSRS12.MSSQLSERVER\Reporting Services\LogFiles`|  
|[Report Server HTTP Log](report-server-http-log.md)|The HTTP log file contains a record of all HTTP requests and responses handled by the Report Server Web service and Report Manager.|  
|[Windows Application Log](windows-application-log.md)|The Microsoft Windows Application log contains information about report server events.|  
|Windows Performance logs|The Windows Performance logs contain report server performance data. You can create performance logs, and then choose counters that determine which data to collect. For more information, see [Monitoring Report Server Performance](monitoring-report-server-performance.md).|  
|Setup log files|Log files are also created during Setup. If Setup fails or succeeds with warnings or other messages, you can examine the log files to troubleshoot the problem. For more information, see [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).|  
|IIS Logs|Log files created by Microsoft Internet Information Services (IIS). For more information, see [How to enable logging in Internet Information Services (IIS)](https://support.microsoft.com/kb/313437).|  
|Video|View a short video that demonstrates the use of Microsoft Power Query to view [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] log files.<br /><br /> ![view a video about Power Query and SSRS logs](../media/generic-video-thumbnail.png "view a video about Power Query and SSRS logs")|  
  
## See Also  
 [Reporting Services Report Server &#40;Native Mode&#41;](reporting-services-report-server-native-mode.md)   
 [Errors and Events Reference &#40;Reporting Services&#41;](../troubleshooting/errors-and-events-reference-reporting-services.md)  
  
  
