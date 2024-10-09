---
title: "Server properties (Logging page)"
description: Learn how to use the options on the Reporting Services page in SQL Server Management Studio. You can learn how to set limits on the report execution data that the report server collects.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.swb.reportserver.serverproperties.logging.f1"
---
# Server properties (Logging page)
  Use this [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] page in [!INCLUDE[ssManStudioFull_md](../../includes/ssmanstudiofull-md.md)] to set limits on the report execution data that the report server collects by a report server. Execution data is stored internally in the report server database. You can track report activity for report server that runs in native mode or SharePoint integrated mode. If the report server is part of a scale-out deployment, the report execution log maintains a record of all report activity for the entire deployment in a single log file.  
  
 To open this page:
 1) Start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]
 2) Connect to a report server.
 3) Right-click the report server name and select **Properties**. 
 4) Select **Logging** to open this page.  
  
## Options  
 **Enable report execution logging**  
 Select to create and store information about report activity on the server. If this option is enabled, the report server tracks which reports are used. It also tracks the frequency of report processing, the type of report operation that was performed, the output format, and who ran the report. For more information about other data points that are captured in the log, see [Report Server ExecutionLog and the ExecutionLog3 View](../../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md).  
  
 **Remove log entries older than this number of days**  
 Specify the number of days after which log entries are trimmed from the report execution log. The default value is 60 days.  
  
## Related content

- [Set report server properties &#40;Management Studio&#41;](../../reporting-services/tools/set-report-server-properties-management-studio.md)
- [Connect to a report server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)
- [Reporting Services log files and sources](../../reporting-services/report-server/reporting-services-log-files-and-sources.md)
- [Report server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)
- [Report server ExecutionLog and the ExecutionLog3 view](../../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md)
