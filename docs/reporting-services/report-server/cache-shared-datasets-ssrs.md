---
title: "Cache shared datasets"
description: Learn about caching shared datasets in SQL Server Report Manager, which improves response time and provides consistent data for reports that use the dataset.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/14/2019
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Cache shared datasets (SSRS)
  Query results for a shared dataset can be copied to a cache to provide consistent data for multiple reports and to improve response time for the dataset query. Like reports, you can configure a shared dataset to be cached on first use or by specifying a schedule.  
  
 A shared dataset can be included in multiple reports or as part of component definitions. By caching the shared dataset, you provide a consistent set of data for all reports that use it, and also reduce the number of times that the dataset query runs against the external data source.  
  
 The following list provides examples of when to cache a shared dataset:  
  
-   The query takes a substantial amount of time to run.  
  
-   The query takes parameters, but most of the time, the number of parameter combinations is small. Each combination creates cached query results.  
  
-   The query runs at predictable times of the day, week, or month.  
  
-   The query runs as the result of a shared dataset reference in a report. This report is delivered via email, where a large number of people are likely to select the link in a short span of time.
  
 The following list provides examples of when not to cache a shared dataset:  
  
-   The query results must always include the most recent data.  
  
-   The query runs quickly.  
  
-   The query runs infrequently.  
  
-   The query takes parameters, the number of parameter combinations is large, and no combination is more likely than another.  
  
-   The data source that the shared dataset is based on has Prompt or Windows Integrated credentials.  
  
-   The shared dataset filter or the query contains an expression with a reference to the global collection User.  
  
If a user chooses report parameter values that differ from the default values specified for the cached result set, the dataset query runs actively. In such cases, the cached results aren't used for that query.
  
## Cache shared datasets  
 To enable caching for a shared dataset, you must select the cache option on the shared dataset. After caching is enabled, the query results for a shared dataset are copied to the cache on first use. If the shared dataset has parameters, each combination of parameters creates a new entry in the cache.  
  
 While the query results for a specific parameter combination are in the cache, each report that is launched for processing and that includes a reference to the shared dataset with those parameter values use the cached data.  
  
 You can specify how long to keep data in the cache before it expires. For more information, see [Work with shared datasets](../../reporting-services/work-with-shared-datasets-web-portal.md).  
  
## Preload the cache  
 You can preload the cache by creating a cache refresh plan. With a refresh plan, you can specify how often to refresh the cache by using an item-specific schedule or a shared schedule. To avoid multiple cache entries for the same item, the schedule that you specify should allow enough time for query processing on the external data source. For example, if the query takes 20 minutes to run, the refresh schedule should be greater than 20 minutes. For more information, see [Schedules](../../reporting-services/subscriptions/schedules.md).  
  
 To create a cache refresh plan for a shared dataset, the following conditions apply.  
  
-   The shared dataset must be enabled for caching.  
  
-   The shared data source that the shared dataset depends on can't use Prompt or Windows Integrated credentials.  
  
-   If the shared dataset has parameters, you must specify static default values for each parameter that isn't marked read-only. Read-only parameters always use the default value. To cache a shared dataset for multiple combinations of parameters, you must create a separate cache refresh plan for each combination of values. Parameters can't contain references to other datasets.  
  
-   Each cache refresh plan is associated with only one shared dataset or report.  
  
-   You must have `ReadPolicy` and `UpdatePolicy` permissions on the shared dataset.  
  
 Cache refresh plans apply to both shared datasets and reports. For more information, see [Cache reports &#40;SSRS&#41;](../../reporting-services/report-server/caching-reports-ssrs.md).  
  
## Conditions that cause cache expiration  
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
  
 Updates to cache refresh plans for shared datasets don't affect reports that are already being processed. Updating a cache refresh plan affects only future launches of reports that reference the shared dataset.  
  
## Related content
  
 [Manage shared datasets](../../reporting-services/report-data/manage-shared-datasets.md)  
  
