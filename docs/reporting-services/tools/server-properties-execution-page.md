---
title: "Server properties (Execution page)"
description: Learn how to use the options on the Server Properties Execution page to set a timeout value for report execution.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.swb.reportserver.serverproperties.execution.f1"
---
# Server properties (Execution page)
  Use this page to set a timeout value for report execution. This value applies to all reports that the current report server instance processes. You can override this value for individual reports. The value you specify must accommodate all report processing that occurs on the report server. It must also accommodate query processing performed on the database server when the report server retrieves data that is used in the report.  
  
 To open this page, start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to a report server instance, right-click the report server name, and select **Properties**. Select **Execution** to open this page.  
  
## Options  
 **Do not timeout report execution**  
 Allow a report server unlimited time to complete report processing.  
  
 **Limit report execution to the following number of seconds**  
 Set a time constraint on report execution. The time period starts when the report is requested. If the time period ends before the report is fully processed, the report server cancels the process and any in-process queries to external data sources.  
  
## Related content

- [Set report server properties &#40;Management Studio&#41;](../../reporting-services/tools/set-report-server-properties-management-studio.md)
- [Connect to a report server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)
- [Set report processing properties](../../reporting-services/report-server/set-report-processing-properties.md)
- [Setting Time-out Values for Report and Shared Dataset Processing &#40;SSRS&#41;](../../reporting-services/report-server/setting-time-out-values-for-report-and-shared-dataset-processing-ssrs.md)
- [Report server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)
