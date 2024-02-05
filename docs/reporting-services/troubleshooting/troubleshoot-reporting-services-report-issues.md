---
title: "Troubleshoot Reporting Services report issues"
description: In this article, troubleshoot issues with report design, preview, export, and publishing to or viewing on a report server in native or SharePoint mode.
author: maggiesMSFT
ms.author: maggies
ms.date: 02/27/2016
ms.service: reporting-services
ms.subservice: troubleshooting
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Troubleshoot  Reporting Services report issues
This article helps you in troubleshooting problems with  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report design and previewing a report. It also provides guidance on resolving issues when publishing a report to a report server in native mode or SharePoint mode. Additionally, it covers troubleshooting for viewing a report on the report server or exporting a report to a different file format. 
 
## Monitor report servers  
You can use system and database tools to monitor report server activity. You can also view report server trace log files, or query the report server execution log for detailed information about specific reports. If you use Performance Monitor, you can add performance counters for the Report Server Web service and Windows service to identity bottlenecks in on-demand or scheduled processing.  

For more information, see [Monitor report server performance](../report-server/monitoring-report-server-performance.md).  
  
  
## View the report server logs  
[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] records many internal and external events to log files that record data about specific reports, debugging information, HTTP requests and responses, and report server events. You can also create performance logs and select performance counters that specify which data to collect. The default directory for log files for a default installation is `<drive>\Program Files\Microsoft SQL Server\MSRS130.MSSQLSERVER\Reporting Services\LogFiles`.   
  
For more information, see [Reporting Services log files and sources](../report-server/reporting-services-log-files-and-sources.md).  
  
In order to determine specifically whether report waits are due to data retrieval, report processing, or report rendering, use the Execution Log. For more information, see [Report server ExecutionLog and the ExecutionLog3 view](../report-server/report-server-executionlog-and-the-executionlog3-view.md).   
  
## View the call stack for report processing error messages on the report server  
When you view a published report in Report Manager, you might see an error message that represents a general processing or rendering error. To see more information, you can view the call stack.   
  
To view the call stack, sign in to the report server by using the local administrator credentials, right-click the Report Manager page, and then select **View Source**. The call stack provides detailed context for the error message.  
  
## Use [!INCLUDE[ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)] to verify queries and credentials  
You can use [!INCLUDE[ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)] to validate complex queries before you include them in your report.   
  
For more information, see [Database Engine query editor](../../ssms/f1-help/database-engine-query-editor-sql-server-management-studio.md) and [Manage objects by using Object Explorer](../../ssms/object/manage-objects-by-using-object-explorer.md). 
  
## Analyze problem reports with report data cached on the client  
When a report author creates a report in Business Intelligence Development Studio, the authoring client caches data as an .rdl.data file, which is used when you preview a report. Every time the query changes, the cache is updated. To debug report problems, it's sometimes useful to prevent the refresh for report data so that the data doesn't change when you're debugging.   
  
To control whether [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] can only use cached data, add the following section to devenv.exe.config in the [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. The location of the default directory is: `<drive>:Program Files\Microsoft Visual Studio 10.0\Common7\IDE`.   
  
```  
<system.diagnostics>  
      <switches>  
         <add name="Microsoft.ReportDesigner.ReportPreviewStore.ForceCache" value="1" />  
      </switches>  
   </system.diagnostics>  
```  
As long as the value is set to 1, only cached report data is used. Be sure to remove this section when you finish debugging the report.  
  
## Related content
[Errors and events (Reporting Services)](errors-and-events-reference-reporting-services.md)  
  
  

[!INCLUDE[feedback-qa-stackoverflow-md](../../includes/feedback-qa-stackoverflow-md.md)]
