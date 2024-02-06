---
title: "Process large reports"
description: Find out how to run large reports properly in Report Server by following configuration, rendering, deployment, and distribution recommendations.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/10/2019
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "report processing [Reporting Services], large reports"
  - "page breaks [Reporting Services]"
  - "large reports"
  - "size [SQL Server], reports"
  - "distributing reports [Reporting Services], large reports"
---
# Process large reports
  Large reports present certain processing challenges and require certain configurations if they're to run properly. Large reports shouldn't be run on demand unless they're configured to support pagination.  
  
> [!NOTE]  
>  Page breaks are enabled by default. Don't disable page breaks if you think the report contains a large amount of data. The HTML rendering format that's used to initially render a report opens a report in a browser. If the report is not paginated, all of the data is included in a single page, which cannot be accommodated by most browsers. For example, a report that contains 5,000 rows of data almost certainly cannot be viewed in a browser in a single page.  
  
 If you're working with a large report, you should choose report execution, rendering, and delivery options that can accommodate large documents. The row set that comes back from the query and the rendering extension that is used to present the report determines the report size.  
  
 For reports that contain volatile data, report size can change dramatically from one report run to the next. In this case, you should monitor the data source to determine how data volatility affects your report and whether you need to follow the steps prescribed in this article.  
  
 For more information and tips on how to diagnose time-out errors and out-of-memory errors, see the article [How to diagnose issues when running reports in the report server](/archive/blogs/lukaszp/) on `blogs.msdn.microsoft.com`.  
  
## Configuration recommendations  
 Recommendations for report execution, report rendering, and report access include the following items:  
  
-   Design the report to support pagination. The report server sends a report one page at a time. If the report includes pagination, you can control how much data is streamed to the browser. For more information, see  [Preload the cache (SSRS)](../../reporting-services/report-server/preload-the-cache-report-manager.md).  
  
-   Configure the report to run as a scheduled report snapshot to prevent it from being run on demand. Don't set a time-out value for report execution. Run the report during off-peak hours.  
  
-   Configure the report to use a shared data source if you want to control whether the report is processed. One advantage to using a shared data source is that you can disable it. Disabling the data source prevents report processing.  
  
-   Disable report history if you want to conserve disk space. To disable report history, clear all the check boxes on the **History** properties page.  
  
-   Limit access to the report. Configure the report to use item-level security and replace the default role assignments with new ones that allow access to only those users that need it.  
  
     By default, users can open any report that they can view in the folder hierarchy. Even if you configure a report to run as a snapshot, users who can view the report item in a folder can open the report. If the report is large, it might cause the browser to stop responding when a user opens the report in the web portal.  
  
## Rendering recommendations  
 Before you configure report distribution, you should know which rendering clients can accommodate large documents. The recommended format is the default HTML rendering extension with soft page breaks, but you can choose from any format that supports pagination.  
  
 Performance and memory consumption varies for each rendering format. The same report renders at different rates and requires different amounts of memory depending on the format you select. The fastest and least memory intensive formats include CSV, XML, and HTML. PDF and Excel have the slowest performance, but for different reasons. PDF is CPU-intensive, while Excel is RAM-intensive. Image rendering falls in-between the two groups. You can specify the format when you define how the report is distributed.  
  
## Deployment and distribution recommendations  
 If you're using page breaks to control report rendering, you can deploy a large report the same way you would deploy any report. You can provide access to the report through the web portal, a SharePoint Web part, or a URL that you add to a portal or Web site. All of these deployment options support on demand access, and a previously run report snapshot.  
  
 An alternative deployment strategy is to distribute reports to individual users. You can distribute large reports through subscriptions if you're careful about how you configure delivery options. You can use either a standard subscription or a data-driven subscription to deliver the report. Recommendations for subscription and delivery include the following configurations:  
  
-   Configure a subscription to use Web archive (MHTML), PDF, or Excel.  
  
-   Configure a subscription to use file share delivery if you're using PDF or Excel. Once the report is delivered, you can use a desktop application to work with the report. You must set permissions on the file share to determine who can view the report.  
  
     Once the report is on the file share, the report is no longer controlled or secured by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. If you want to be notified when the report is updated, create a second subscription that uses e-mail delivery to send a notification only.  
  
 If you want to use e-mail report delivery, configure the subscription to include a link. Avoid sending the report as an attachment.  
  
## Related content  
 [Subscriptions and delivery &#40;Reporting Services&#41;](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md)   
 [Set report processing properties](../../reporting-services/report-server/set-report-processing-properties.md)   
 [Specify credential and connection information for report data sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md)   
 [Report server content management &#40;SSRS native mode&#41;](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)   
 [Preload the cache (SSRS)](../../reporting-services/report-server/preload-the-cache-report-manager.md)
