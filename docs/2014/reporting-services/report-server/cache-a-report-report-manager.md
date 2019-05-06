---
title: "Cache a Report (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "report execution properties [Reporting Services]"
  - "cache [Reporting Services]"
  - "cached reports [Reporting Services]"
  - "schedules [Reporting Services], report expiration"
  - "expiration [Reporting Services]"
ms.assetid: 723d1cb0-c2e7-4763-8690-a6a7a8bbbb90
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Cache a Report (Report Manager)
  One way to improve performance is to configure caching properties for a report. When a report is cached, a copy of the rendered report is saved for a short period of time. The first user who requests the report must wait for all processing to complete before viewing the report. Subsequent users who request the report within the caching period can view it right away because processing has already occurred.  
  
 There are restrictions on the types of reports that you can cache. For example, a report cannot be cached if report output varies based on the user identity or if data is retrieved using the security token of the user who requests the report. For more information, see [Caching Reports &#40;SSRS&#41;](caching-reports-ssrs.md).  
  
### To schedule the expiration of a cached report  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, navigate to the **Contents** page. Navigate to the report for which you want to set caching properties, hover over the item, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**.  
  
4.  In the left frame, click the **Processing Options**.  
  
5.  On the page, select **Always run this report with the most recent data**.  
  
6.  Select one of the following two cache options and configure expiration as follows:  
  
    -   To configure a cached copy to expire after a particular time period, click **Cache a temporary copy of the report. Expire copy of report after a number of minutes**. Type the number of minutes for report expiration.  
  
    -   To configure a cached copy to expire on a schedule, click **Cache a temporary copy of the report. Expire copy of report on the following schedule.** Click **Configure**, or select a shared schedule to control report expiration  
  
7.  Click **Apply**.  
  
## See Also  
 [Set Report Processing Properties](set-report-processing-properties.md)   
 [Caching Reports &#40;SSRS&#41;](caching-reports-ssrs.md)  
  
  
