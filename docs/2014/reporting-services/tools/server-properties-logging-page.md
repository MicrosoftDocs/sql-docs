---
title: "Server Properties (Logging Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.reportserver.serverproperties.logging.f1"
ms.assetid: b338deab-4868-4951-9f22-0605add2fc95
author: markingmyname
ms.author: maghan
manager: craigg
---
# Server Properties (Logging Page)
  Use this page to set limits on the report execution data that is collected by a report server. Execution data is stored internally in the report server database. You can track report activity for report server that runs in native mode or SharePoint integrated mode. If the report server is part of a scale-out deployment, the report execution log maintains a record of all report activity for the entire deployment in a single log file.  
  
 To open this page, start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to a report server, right-click the report server name, and select **Properties**. Click **Logging** to open this page.  
  
## Options  
 **Enable report execution logging**  
 Click to create and store information about report activity on the server. If this option is enabled, the report server will track which reports are used, the frequency of report processing, the type of report operation that was performed, the output format, and who ran the report. For more information about additional data points that are captured in the log, see [Report Server Execution Log and the ExecutionLog3 View](../report-server/report-server-executionlog-and-the-executionlog3-view.md).  
  
 **Remove log entries older than this number of days**  
 Specify the number of days after which log entries will be trimmed from the report execution log. The default value is 60 days.  
  
## See Also  
 [Set Report Server Properties &#40;Management Studio&#41;](set-report-server-properties-management-studio.md)   
 [Connect to a Report Server in Management Studio](connect-to-a-report-server-in-management-studio.md)   
 [Reporting Services Log Files and Sources](../report-server/reporting-services-log-files-and-sources.md)   
 [Report Server in Management Studio F1 Help](report-server-in-management-studio-f1-help.md)   
 [Report Server Execution Log and the ExecutionLog3 View](../report-server/report-server-executionlog-and-the-executionlog3-view.md)  
  
  
