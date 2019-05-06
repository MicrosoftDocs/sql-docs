---
title: "Cache Shared Datasets (SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server


ms.topic: conceptual
ms.assetid: 4acb1bbe-1c04-4979-b893-dc1b1c5039b6
author: maggiesMSFT
ms.author: maggies
---
# Cache Shared Datasets (SSRS)
  Query results for a shared dataset can be copied to a cache to provide consistent data for multiple reports and to improve response time for the dataset query. Like reports, you can configure a shared dataset to be cached on first use or by specifying a schedule.  
  
 A shared dataset can be included in multiple reports or as part of component definitions. By caching the shared dataset, you provide a consistent set of data for all reports that use it, and also reduce the number of times that the dataset query runs against the external data source.  
  
 The following list provides examples of when to cache a shared dataset:  
  
-   The query takes a substantial amount of time to run.  
  
-   The query takes parameters, but most of the time, the number of parameter combinations is small. Each combination creates cached query results.  
  
-   The query runs at predictable times of the day, week, or month.  
  
-   The query runs as the result of a shared dataset reference in a report that is delivered via e-mail, where a large number of people are likely to click the link in a short span of time.  
  
 The following list provides examples of when not to cache a shared dataset:  
  
-   The query results must always include the most recent data.  
  
-   The query runs quickly.  
  
-   The query runs infrequently.  
  
-   The query takes parameters, the number of parameter combinations is large, and no combination is more likely than another.  
  
-   The data source that the shared dataset is based on has Prompt or Windows Integrated credentials.  
  
-   The shared dataset filter or the query contains an expression with a reference to the global collection User.  
  
 If a user chooses report parameter values that differ from the default values that are specified for the cached result set, the dataset query runs actively and the cached results are not used for that query.  
  
## Caching Shared Datasets  
 To enable caching for a shared dataset, you must select the cache option on the shared dataset. After caching is enabled, the query results for a shared dataset are copied to the cache on first use. If the shared dataset has parameters, each combination of parameters creates a new entry in the cache.  
  
 While the query results for a specific parameter combination are in the cache, each report that is launched for processing and that includes a reference to the shared dataset with those parameter values will use the cached data.  
  
 You can specify how long to keep data in the cache before it expires. For more information, see [Caching Page, Shared Datasets &#40;Report Manager&#41;](https://msdn.microsoft.com/library/eac372e9-d2a1-48a8-bbe5-09d101df16ea).  
  
## Preloading the Cache  
 You can preload the cache by creating a cache refresh plan. With a refresh plan, you can specify how often to refresh the cache by using an item-specific schedule or a shared schedule. To avoid multiple cache entries for the same item, the schedule that you specify should allow enough time for query processing on the external data source. For example, if the query takes 20 minutes to run, the refresh schedule should be greater than 20 minutes. For more information, see [Schedules](../../reporting-services/subscriptions/schedules.md).  
  
 To create a cache refresh plan for a shared dataset, the following conditions apply.  
  
-   The shared dataset must be enabled for caching.  
  
-   The shared data source that the shared dataset depends on cannot use Prompt or Windows Integrated credentials.  
  
-   If the shared dataset has parameters, you must specify static default values for each parameter that is not marked read-only. Read-only parameters will always use the default value. To cache a shared dataset for multiple combinations of parameters, you must create a separate cache refresh plan for each combination of values. Parameters cannot contain references to other datasets.  
  
-   Each cache refresh plan is associated with only one shared dataset or report.  
  
-   You must have ReadPolicy and UpdatePolicy permissions on the shared dataset.  
  
 Cache refresh plans apply to both shared datasets and reports. For more information, see [Cache Refresh Options &#40;Report Manager&#41;](https://msdn.microsoft.com/library/227da40c-6bd2-48ec-aa9c-50ce6c1ca3a6).  
  
## Conditions that Cause Cache Expiration  
 The following conditions can cause a shared dataset cache to become not valid.  
  
-   A schedule condition expires. The cache times out or the expiration time occurs.  
  
-   A shared schedule is deleted.  
  
-   Changes to a shared schedule. Shared schedules can be paused, which also affects when a cache expires.  
  
-   The query definition for the shared dataset changes.  
  
-   The credentials for the shared data source that the shared dataset depends on change.  
  
-   The cache options for the shared dataset change.  
  
-   The default values for read-only parameters for the shared dataset change.  
  
-   The filters that are part of the shared dataset definition change.  
  
-   The shared dataset is deleted from the report server. When a shared dataset is deleted, associated cached copies and cache refresh plans are also deleted.  
  
 Updates to cache refresh plans for shared datasets do not affect reports that are already being processed. Updating a cache refresh plan affects only future launches of reports that reference the shared dataset.  
  
## See Also  
 [Manage Shared Datasets](../../reporting-services/report-data/manage-shared-datasets.md)  
  
  
