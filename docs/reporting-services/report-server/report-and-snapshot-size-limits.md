---
title: "Report and snapshot size limits"
description: Learn about report size limits when a report is published in Report Server, rendered at run time, and saved to the file system.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "large reports"
  - "maximum report size"
  - "size [SQL Server], reports"
  - "report size [Reporting Services]"
  - "reports [Reporting Services], size"
  - "denial of service attacks [Reporting Services]"
---
# Report and snapshot size limits
  Administrators who manage a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] deployment can use the information in this article to understand report size limits when the report is published to a report server, rendered at run time, and saved to the file system. This article also provides practical guidance on how to measure the size of a report server database, and describes the effect of snapshot size on server performance.  
  
## Maximum size for published reports  
 On the report server, report and model size are based on the size of the report definition (.rdl) and report model (.smdl) files that you publish to a report server. The report server doesn't limit the size of a report that you publish. However, [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] imposes a maximum size for items that are posted to the server. By default, this limit is 4 megabytes (MB). If you upload or publish a file that exceeds this limit to a report server, you receive an HTTP exception. In this case, you can modify the default by increasing the value of the `maxRequestLength` element in the `Machine.config` file.  
  
 Although a report model might be large, report definitions rarely exceed 4 MB. A more typical report size is in the order of kilobytes (KB). However, if you include embedded images, the encoding of those images can result in large report definitions that exceed the 4-MB default.  
  
 [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] imposes a maximum limit on posted files to reduce the threat of denial-of-service attacks against the server. Increasing the value of the upper limit undermines some of the protection that this limit provides. Increase the value only if you're confident that the benefit of doing so outweighs any other security risk.  
  
 Keep in mind that the value you set for the `maxRequestLength` element must be larger than the actual size limits you want to enforce. To account for the inevitable increase in the HTTP request size, make the value larger. This increase occurs after encapsulating all the parameters in a SOAP envelope and applying Base64 encoding. Base64 encoding increases the size of the original data by approximately 33%. So, the value you specify for the `maxRequestLength` element needs to be approximately 33% larger than the actual usable item size. For example, if you specify a value of 64 MB for `maxRequestLength`, realistically you can expect the maximum size for report files that are posted to the report server to be approximately 48 MB.  
  
## Report Size in Memory  
 When you run a report, report size is equal to the amount of data that is returned in the report plus the size of the output stream. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] doesn't impose a maximum limit on the size of a rendered report. System memory determines the upper limit on size. By default, a report server uses all available configured memory when rendering a report. But, you can specify configuration settings to set memory thresholds and memory management policies. For more information, see [Configure available memory for report server applications](../../reporting-services/report-server/configure-available-memory-for-report-server-applications.md).  
  
 For any report, size can vary considerably depending on how much data is returned and which rendering format you use for the report. A parameterized report might be larger or smaller depending on how parameter values affect the query results. The report output format you choose effects report size in the following ways:  
  
-   HTML processes the report one page at a time. Because the report is processed in smaller units, less memory is required to process specific chunks.  
  
-   PDF, Excel, TIFF, XML, and CSV process the entire report in memory before displaying the report to the user.  
  
 To measure the size of a rendered report, you can view the report execution log. For more information, see [Report server ExecutionLog and the ExecutionLog3 view](../../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md).  
  
 To calculate the size of a rendered report on disk, you can export and then save the report to the file system. The saved file includes data and report formatting information.  
  
 The only hard limit on report size is when rendering to Excel format. Worksheets can't exceed 65,536 rows or 256 columns. Other rendering formats don't have these limits so size is limited only by the amount of resources on your server.  
  
> [!NOTE]  
>  Report processing and rendering occur in memory. If you have large reports or large number of users, be sure to do some kind of capacity planning to make sure your report server deployment performs at a level that is satisfactory for your users. For more information about tools and guidelines, see the following publications on MSDN: [Plan for scalability and performance with Reporting Services](/previous-versions/sql/sql-server-2005/administrator/cc966418(v=technet.10)) and [Use Visual Studio 2005 to perform load testing on a SQL Server 2005 Reporting Services report server](/previous-versions/sql/sql-server-2005/administrator/aa964139(v=sql.90)).  
  
## Measure snapshot storage  
 The size of any given snapshot is directly proportional to the amount of data in the report. Snapshots are typically much larger than other items that are stored on a report server. Snapshot size can typically range from a few megabytes to tens of megabytes. If you have large reports, you can expect to see snapshots that are even larger.  The amount of disk space that the report server database requires can increase rapidly over a short period of time. The amount of disk space increase depends on how frequently you use snapshots and how you configure report history.  
  
 By default, both the **reportserver** and **reportservertempdb** databases are set to autogrow. Although the database size can increase automatically, the size is never decreased automatically. If the **reportserver** database has excess capacity because you deleted snapshots, you must manually reduce it to recover disk space. Similarly, if the **reportservertempdb** grew to accommodate an unusually high volume of interactive reporting, the disk space allocation remains at that setting until you reduce it.  
  
 To measure the size of the report server databases, you can run the following [!INCLUDE[tsql](../../includes/tsql-md.md)] commands. Calculating total database size at regular intervals can help you develop reasonable estimates of how to allocate space for the report server database over time. The following statements measure the amount of space that is currently used. The statements assume you're using default database names:  
  
```  
USE ReportServer  
EXEC sp_spaceused  
```  
  
## Snapshot size and report server performance  
 Snapshot size affects server performance when the report is processed and rendered. Server performance is most affected by rendering operations, so if you have a large snapshot you can expect some delay when users request the report. Depending on the number of users, you can expect to encounter delays when snapshot size is over 100 megabytes.  
  
 To minimize performance delays due to large snapshots, complete the following steps:  
  
-   Deploy the report server and the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] on separate computers.  
  
-   Add more system memory.  
  
-   Review the "Planning for Scalability and Performance with Reporting Services" document on the MSDN web site for best practices on how to configure a report server for the enterprise.  
  
 The quantity of snapshots that are stored in a report server database is, by itself, not a performance factor. You can store a large number of snapshots without affecting server performance. You can keep snapshots indefinitely. However, the  report history is configurable. If a report server administrator lowers the report history limit, you might lose historical reports that you intended to keep. If you delete the report, all report history is deleted with it.  
  
## Related content  
 [Set report processing properties](../../reporting-services/report-server/set-report-processing-properties.md)   
 [Report server database &#40;SSRS native mode&#41;](../../reporting-services/report-server/report-server-database-ssrs-native-mode.md)   
 [Process large reports](../../reporting-services/report-server/process-large-reports.md)  
  
