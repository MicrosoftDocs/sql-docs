---
title: "Caching Reports | Microsoft Docs"
description: Learn about caching reports in Report Manager, which speeds up viewing for a processed report while it remains cached. 
ms.date: 05/14/2019
ms.service: reporting-services
ms.subservice: report-server


ms.topic: conceptual
helpviewer_keywords: 
  - "report execution properties [Reporting Services]"
  - "cache [Reporting Services]"
  - "report processing [Reporting Services], caching"
  - "cached instances [Reporting Services]"
  - "refreshing cache"
  - "cached reports [Reporting Services]"
  - "preloading cache"
  - "invalid cached reports [Reporting Services]"
  - "performance [Reporting Services]"
  - "expiration [Reporting Services]"
  - "snapshots [Reporting Services], caching"
ms.assetid: 146542c3-8efd-4b89-a8d8-77d22896630e
author: maggiesMSFT
ms.author: maggies
---
# Caching Reports (SSRS)
  A report server can cache a copy of a processed report and return that copy when a user opens the report. To a user, the only evidence available to indicate the report is a cached copy is the date and time that the report ran. If the date or time is not current and the report is not a snapshot, the report was retrieved from cache.  
  
 Caching can shorten the time required to retrieve a report if the report is large or accessed frequently. If the server is rebooted, all cached instances are reinstated when the Report Server Web service comes back online.  
  
 Caching is a performance-enhancement technique. The contents of the cache are volatile and can change as reports are added, replaced, or removed. If you require a more predictable caching strategy, you should create a report snapshot. For more information, see [Set Report Processing Properties](../../reporting-services/report-server/set-report-processing-properties.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] stores temporary files in a database to support user sessions and report processing. These files are cached for internal use and to support a consistent viewing experience during a single browser session. For more information about how internal-use temporary files are cached, see [Report Server Database &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/report-server-database-ssrs-native-mode.md).  
  
## Cached Instances  
 A cached instance of a report is based on the intermediate format of a report. The report server generally caches one instance of a report based on the report name. However, if a report can contain different data based on query parameters, multiple versions of the report may be cached at any given time. For example, suppose you have a parameterized report that takes a region code as a parameter value. If four different users specify four unique region codes, four cached copies are created.  
  
 The first user who runs the report with a unique region code creates a cached report that contains data for that region. Subsequent users who request the report using the same region code get the cached copy.  
  
 Not all reports can be cached. If a report includes user-dependent data, prompts users for credentials, or uses Windows Authentication, it cannot be cached.  
  
## Refreshing the Cache  
 A cached report is replaced with a newer version when a user selects the report after the previously cached copy has expired. Reports that are configured to run as cached instances are removed from the cache at regular intervals based on expiration settings. You can set a report's expiration in minutes or at a scheduled time, as determined by the data's immediacy requirement. You cannot delete reports from the cache directly unless you use the SOAP API.  
  
 To configure cache expiration, you can use a shared schedule or report-specific schedule. If you use a shared schedule and it is subsequently paused, the cache does not expire while the schedule is inoperative. If the shared schedule is subsequently deleted, a copy of the schedule settings is saved as a report-specific schedule.  
  
 If a schedule expires or if the scheduling engine is unavailable at a cache expiration date, the report server runs a live report until scheduled operations can be resumed (by either extending the schedule or starting the scheduling service).  
  
## Preloading the Cache  
 To improve server performance, you can preload the cache. You can preload the cache with a collection of parameterized report instances in two ways:  
  
1.  Create a cache refresh plan. When you create a refresh plan, you can specify a schedule for a single report or specify a shared schedule.  
  
2.  Create a data-driven subscription that uses the Null Delivery Provider. When you specify the Null Delivery Provider as the method of delivery in the subscription, the report server targets the report server database as the delivery destination and uses a specialized rendering extension called the null rendering extension. In contrast with other delivery extensions, the Null Delivery Provider does not have delivery settings that you can configure through a subscription definition.  
  
 Caching a report is especially useful if you want to cache multiple instances of a parameterized report where different parameter values are used to produce different report instances. Note that you can only specify query-based parameters on the report.  
  
 When you specify a schedule or when you create the data-driven subscription, you schedule how often the reports are delivered to the cache. In order for new copies to be delivered to the cache, the old copies must have expired. Therefore, the Execution properties of the report must be configured to include cache expiration settings. The expiration setting must be consistent with the subscription schedule that you define. For example, if you create a subscription that runs every night, the cache should also expire every night prior to the subscription's run time. If the Execution properties do not include expiration times, newer deliveries are disregarded. For more information about cache refresh plans, see [Schedules](../../reporting-services/subscriptions/schedules.md). For more information about setting properties, see [Set Report Processing Properties](../../reporting-services/report-server/set-report-processing-properties.md). For more information about using data-driven subscriptions, see [Data-Driven Subscriptions](../../reporting-services/subscriptions/data-driven-subscriptions.md).  
  
## Conditions That Cause Cache Expiration  
 A cached report is invalidated in response to the following events: the report definition is modified, report parameters are modified, data source credentials change, or report execution options change. If you delete a report that is stored in the cache, the cached version is also deleted.  
  
 If a report cannot be rendered from a cached instance for any reason (for example, if the parameter values that a user specifies are different from those used to produce the cached report), the report server reruns the report.  
  
## See also  
 [Set Processing Options &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../reporting-services/report-server-sharepoint/set-processing-options-reporting-services-in-sharepoint-integrated-mode.md)   
 [Set Report Processing Properties](../../reporting-services/report-server/set-report-processing-properties.md)   
 [Reporting Services Concepts &#40;SSRS&#41;](../../reporting-services/reporting-services-concepts-ssrs.md)   
 [Preload the Cache](../../reporting-services/report-server/preload-the-cache-report-manager.md)   
 [Schedules](../../reporting-services/subscriptions/schedules.md)   
 [Cache Shared Datasets &#40;SSRS&#41;](../../reporting-services/report-server/cache-shared-datasets-ssrs.md)   
  
  
