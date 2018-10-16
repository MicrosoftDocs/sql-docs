---
title: "Server Properties (Execution Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.reportserver.serverproperties.execution.f1"
ms.assetid: 53b77db1-b013-4dac-82dd-30c0de276639
author: markingmyname
ms.author: maghan
manager: craigg
---
# Server Properties (Execution Page)
  Use this page to set a timeout value for report execution. This value applies to all reports that are processed by the current report server instance. You can override this value for individual reports. The value you specify must accommodate all report processing that occurs on the report server, plus query processing performed on the database server when the report server retrieves data that is used in the report.  
  
 To open this page, start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to a report server instance, right-click the report server name, and select **Properties**. Click **Execution** to open this page.  
  
## Options  
 **Do not timeout report execution**  
 Allow a report server unlimited time to complete report processing.  
  
 **Limit report execution to the following number of seconds**  
 Set a time constraint on report execution. The time period starts when the report is requested. If the time period ends before the report is fully processed, the report server cancels the process and any in-process queries to external data sources.  
  
## See Also  
 [Set Report Server Properties &#40;Management Studio&#41;](set-report-server-properties-management-studio.md)   
 [Connect to a Report Server in Management Studio](connect-to-a-report-server-in-management-studio.md)   
 [Set Report Processing Properties](../report-server/set-report-processing-properties.md)   
 [Setting Time-out Values for Report and Shared Dataset Processing &#40;SSRS&#41;](../report-server/setting-time-out-values-for-report-and-shared-dataset-processing-ssrs.md)   
 [Report Server in Management Studio F1 Help](report-server-in-management-studio-f1-help.md)  
  
  
