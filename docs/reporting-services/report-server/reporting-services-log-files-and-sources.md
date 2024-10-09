---
title: "Reporting Services log files and sources"
description: Learn about the logs that report servers and report server environments use in Reporting Services to record execution and trace information.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "troubleshooting [Reporting Services], log files"
  - "logs [Reporting Services]"
  - "logs [Reporting Services], about log files"
  - "report servers [Reporting Services], log files"
  - "report server log files"
  - "files [Reporting Services], logs"
#customer-intent: As a Reporting Services user, I want to see the logs that report servers and report server environments use in Reporting Services so that I can record execution and trace information.
---
# Reporting Services log files and sources

A report server and report server environment use various log destinations to record information about server operations and status. There are two basic categories of logging, execution logging, and trace logging. Execution logging includes information about report execution statistics, auditing, performance diagnosis, and optimization. Trace logging is information about error messages and general diagnostics.

[!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode | [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode

The following table provides links to additional information about each log, including the log location and how to view the log contents.

|Log|Description|
|---------|-----------------|
|[Use ExecutionLog and the ExecutionLog3 view in Reporting Services](../../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md)|The execution log is a SQL Server view stored in the report server database.<br /><br /> The report server execution log contains data about specific reports. The data includes when a report ran, who ran it, where it was delivered, and which rendering format was used.|
|SharePoint trace log|For report servers running in SharePoint, the SharePoint trace logs contain [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] information. You can also configure [!INCLUDE[ssRS](../../includes/ssrs.md)] specific information for the SharePoint Unified Logging service. For more information, see [Turn on Reporting Services events for the SharePoint trace log (ULS)](../../reporting-services/report-server/turn-on-reporting-services-events-for-the-sharepoint-trace-log-uls.md)|
|[Report server service trace log](../../reporting-services/report-server/report-server-service-trace-log.md)|The service trace log contains detailed information that is useful if you're debugging an application or investigating an issue or event. The trace log files are `ReportServerService_\<timestamp>.log` and are located in the following folder:<br /><br /> In SQL Server Reporting Services 2016 or earlier: `C:\Program Files\Microsoft SQL Server\MSRS13.MSSQLSERVER\Reporting Services\LogFiles`<br /><br /> In SQL Server Reporting Services 2017: `C:\Program Files\Microsoft SQL Server Reporting Services\SSRS\LogFiles`|
|[Report server HTTP log](../../reporting-services/report-server/report-server-http-log.md)|The HTTP log file contains a record of all HTTP requests and responses handled by the Report Server web service.|
|[Windows Application log](../../reporting-services/report-server/windows-application-log.md)|The Microsoft Windows Application log contains information about report server events.|
|Windows Performance logs|The Windows Performance logs contain report server performance data. You can create performance logs, and then choose counters that determine which data to collect. For more information, see [Monitor report server performance](../../reporting-services/report-server/monitoring-report-server-performance.md).|
|SQL Server Setup log files|Log files are also created during Setup. If Setup fails or succeeds with warnings or other messages, you can examine the log files to troubleshoot the problem. For more information, see [View and read SQL Server Setup log files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).|
|IIS Logs|Log files created by Microsoft Internet Information Services (IIS). For more information, see [Configure logging in IIS](/iis/manage/provisioning-and-managing-iis/configure-logging-in-iis).|

## Related content

- [Reporting Services report server (native mode)](../../reporting-services/report-server/reporting-services-report-server-native-mode.md)
- [Errors and events reference (Reporting Services)](../../reporting-services/troubleshooting/errors-and-events-reference-reporting-services.md)
