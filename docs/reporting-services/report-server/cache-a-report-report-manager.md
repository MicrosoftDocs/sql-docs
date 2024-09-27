---
title: "Cache a report (Report Manager)"
description: Learn how to schedule the expiration of a cached report in Report Manager. Caching a report speeds up viewing while it remains cached.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "report execution properties [Reporting Services]"
  - "cache [Reporting Services]"
  - "cached reports [Reporting Services]"
  - "schedules [Reporting Services], report expiration"
  - "expiration [Reporting Services]"
---
# Cache a report (Report Manager)
  One way to improve performance is to configure caching properties for a report. When a report is cached, a copy of the rendered report is saved for a short period of time. The first user who requests the report must wait for all processing to complete before viewing the report. Subsequent users who request the report within the caching period can view it right away because processing already occurred.  
  
 There are restrictions on the types of reports that you can cache. For example, a report can't be cached if report output varies based on the user identity or if data is retrieved using the security token of the user who requests the report. For more information, see [Caching Reports &#40;SSRS&#41;](../../reporting-services/report-server/caching-reports-ssrs.md).  
  
### Schedule the expiration of a cached report  
  
1.  Start [Report Manager  &#40;SSRS native mode&#41;](../web-portal-ssrs-native-mode.md).  
  
1.  In Report Manager, navigate to the **Contents** page. Navigate to the report for which you want to set caching properties, hover over the item, and select the arrow.  
  
1.  In the menu, choose **Manage**.  
  
1.  In the left frame, choose the **Processing Options**.  
  
1.  On the page, select **Always run this report with the most recent data**.  
  
1.  Select one of the following two cache options and configure expiration as follows:  
  
    -   To configure a cached copy to expire after a particular time period, choose **Cache a temporary copy of the report. Expire copy of report after a number of minutes**. Enter the number of minutes for report expiration.  
  
    -   To configure a cached copy to expire on a schedule, choose **Cache a temporary copy of the report. Expire copy of report on the following schedule.** Select **Configure**, or select a shared schedule to control report expiration  
  
1.  Select **Apply**.  
  
## Related content

- [Set report processing properties](../../reporting-services/report-server/set-report-processing-properties.md)
- [Caching reports &#40;SSRS&#41;](../../reporting-services/report-server/caching-reports-ssrs.md)
