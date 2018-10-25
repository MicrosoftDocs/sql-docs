---
title: "Report and Snapshot Size Limits | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server


ms.topic: conceptual
helpviewer_keywords: 
  - "large reports"
  - "maximum report size"
  - "size [SQL Server], reports"
  - "report size [Reporting Services]"
  - "reports [Reporting Services], size"
  - "denial of service attacks [Reporting Services]"
ms.assetid: 1e3be259-d453-4802-b2f5-6b81ef607edf
author: markingmyname
ms.author: maghan
---
# Report and Snapshot Size Limits
  Administrators who manage a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] deployment can use the information in this topic to understand report size limits when the report is published to a report server, rendered at run time, and saved to the file system. This topic also provides practical guidance on how to measure the size of a report server database, and describes the effect of snapshot size on server performance.  
  
## Maximum Size for Published Reports  
 On the report server, report and model size is based on the size of the report definition (.rdl) and report model (.smdl) files that you publish to a report server. The report server does not limit the size of a report that you publish. However, [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] imposes a maximum size for items that are posted to the server. By default, this limit is 4 megabytes (MB). If you upload or publish a file that exceeds this limit to a report server, you receive an HTTP exception. In this case, you can modify the default by increasing the value of the **maxRequestLength** element in the Machine.config file.  
  
 Although a report model might be very large, report definitions rarely exceed 4 MB. A more typical report size is in the order of kilobytes (KB). However, if you include embedded images, the encoding of those images can result in large report definitions that exceed the 4 MB default.  
  
 [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] imposes a maximum limit on posted files to reduce the threat of denial-of-service attacks against the server. Increasing the value of the upper limit undermines some of the protection that this limit provides. Increase the value only if you are confident that the benefit of doing so outweighs any additional security risk.  
  
 Keep in mind that the value you set for the **maxRequestLength** element must be larger than the actual size limits you want to enforce. You need to make the value larger to account for the inevitable increase to the HTTP request size that occurs after all the parameters are encapsulated in a SOAP envelop, and the Base64 encoding is applied to certain. Base64 encoding increases the size of the original data by approximately 33%. Consequently, the value you specify for the **maxRequestLength** element needs to be approximately 33% larger than the actual usable item size. For example, if you specify a value of 64 MB for **maxRequestLength**, realistically you can expect the maximum size for report files that are posted to the report server to be approximately 48 MB.  
  
## Report Size in Memory  
 When you run a report, report size is equal to the amount of data that is returned in the report plus the size of the output stream. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] does not impose a maximum limit on the size of a rendered report. System memory determines the upper limit on size (by default, a report server uses all available configured memory when rendering a report), but you can specify configuration settings to set memory thresholds and memory management policies. For more information, see [Configure Available Memory for Report Server Applications](../../reporting-services/report-server/configure-available-memory-for-report-server-applications.md).  
  
 For any report, size can vary considerably depending on how much data is returned and which rendering format you use for the report. A parameterized report might be larger or smaller depending on how parameter values affect the query results. The report output format you choose effects report size in the following ways:  
  
-   HTML processes the report one page at a time. Because the report is processed in smaller units, less memory is required to process specific chunks.  
  
-   PDF, Excel, TIFF, XML, and CSV process the entire report in memory before displaying the report to the user.  
  
 To measure the size of a rendered report, you can view the report execution log. For more information, see [Report Server ExecutionLog and the ExecutionLog3 View](../../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md).  
  
 To calculate the size of a rendered report on disk, you can export and then save the report to the file system (the saved file includes data and report formatting information).  
  
 The only hard limit on report size is when rendering to Excel format. Worksheets cannot exceed 65536 rows or 256 columns. Other rendering formats do not have these limits so size is limited only by the amount of resources on your server.  
  
> [!NOTE]  
>  Report processing and rendering occur in memory. If you have large reports or large number of users, be sure to do some kind of capacity planning to make sure your report server deployment performs at a level that is satisfactory for your users. For more information about tools and guidelines, see the following publications on MSDN: [Planning for Scalability and Performance with Reporting Services](https://go.microsoft.com/fwlink/?LinkID=70650) and [Using Visual Studio 2005 to Perform Load Testing on a SQL Server 2005 Reporting Services Report Server](https://go.microsoft.com/fwlink/?LinkID=77519).  
  
## Measuring Snapshot Storage  
 The size of any given snapshot is directly proportional to the amount of data in the report. Snapshots are typically much larger than other items that are stored on a report server. Snapshot size can typically range from a few megabytes to tens of megabytes. If you have very large reports, you can expect to see snapshots that are even larger. Depending on how frequently you use snapshots and how you configure report history, the amount of disk space that the report server database requires can increase rapidly over a short period of time.  
  
 By default, both the **reportserver** and **reportservertempdb** databases are set to autogrow. Although the database size can increase automatically, it is never decreased automatically. If the **reportserver** database has excess capacity because you deleted snapshots, you must manually reduce it to recover disk space. Similarly, if the **reportservertempdb** grew to accommodate an unusually high volume of interactive reporting, the disk space allocation will remain at that setting until you reduce it.  
  
 To measure the size of the report server databases, you can run the following [!INCLUDE[tsql](../../includes/tsql-md.md)] commands. Calculating total database size at regular intervals can help you develop reasonable estimates of how to allocate space for the report server database over time. The following statements measure the amount of space that is currently used (the statements assume you are using default database names):  
  
```  
USE ReportServer  
EXEC sp_spaceused  
```  
  
## Snapshot Size and Report Server Performance  
 Snapshot size affects server performance when the report is processed and rendered. Server performance is most affected by rendering operations, so if you have a large snapshot you can expect some delay when users request the report. Depending on the number of users, you can expect to encounter delays when snapshot size is over 100 megabytes.  
  
 To minimize performance delays due to large snapshots, you can do the following:  
  
-   Deploy the report server and the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] on separate computers.  
  
-   Add more system memory.  
  
-   Review the "Planning for Scalability and Performance with Reporting Services" document on the MSDN Web site for best practices on how to configure a report server for the enterprise.  
  
 The quantity of snapshots that are stored in a report server database is, by itself, not a performance factor. You can store a large number of snapshots without affecting server performance. You can keep snapshots indefinitely. However, be aware that report history is configurable. If a report server administrator lowers the report history limit, you might lose historical reports that you intended to keep. If you delete the report, all report history is deleted with it.  
  
## See Also  
 [Set Report Processing Properties](../../reporting-services/report-server/set-report-processing-properties.md)   
 [Report Server Database &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/report-server-database-ssrs-native-mode.md)   
 [Process Large Reports](../../reporting-services/report-server/process-large-reports.md)  
  
  
