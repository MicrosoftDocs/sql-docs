---
title: "Server Properties (Logging Page) | Microsoft Docs"
description: Learn how to use the options on the Reporting Services page in SQL Server Management Studio to set limits on the report execution data that is collected by a report server.
ms.date: 06/10/2016
ms.service: reporting-services
ms.subservice: tools


ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.reportserver.serverproperties.logging.f1"
ms.assetid: b338deab-4868-4951-9f22-0605add2fc95
author: maggiesMSFT
ms.author: maggies
---
# Server Properties (Logging Page)
  Use this [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] page in [!INCLUDE[ssManStudioFull_md](../../includes/ssmanstudiofull-md.md)] to set limits on the report execution data that is collected by a report server. Execution data is stored internally in the report server database. You can track report activity for report server that runs in native mode or SharePoint integrated mode. If the report server is part of a scale-out deployment, the report execution log maintains a record of all report activity for the entire deployment in a single log file.  
  
 To open this page:
 1) Start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]
 2) Connect to a report server.
 3) Right-click the report server name and select **Properties**. 
 4) Click **Logging** to open this page.  
  
## Options  
 **Enable report execution logging**  
 Click to create and store information about report activity on the server. If this option is enabled, the report server will track which reports are used, the frequency of report processing, the type of report operation that was performed, the output format, and who ran the report. For more information about additional data points that are captured in the log, see [Report Server ExecutionLog and the ExecutionLog3 View](../../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md).  
  
 **Remove log entries older than this number of days**  
 Specify the number of days after which log entries will be trimmed from the report execution log. The default value is 60 days.  
  
## See Also  
 [Set Report Server Properties &#40;Management Studio&#41;](../../reporting-services/tools/set-report-server-properties-management-studio.md)   
 [Connect to a Report Server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)   
 [Reporting Services Log Files and Sources](../../reporting-services/report-server/reporting-services-log-files-and-sources.md)   
 [Report Server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)   
 [Report Server ExecutionLog and the ExecutionLog3 View](../../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md)  
  
  
