---
title: "Auto-Statistics"
description: "Describes auto statistics feature introduced in Analytics Platform System AU7."
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: "06/27/2018"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "conceptual"
ms.custom: seo-dt-2019
monikerRange: ">= aps-pdw-2016-au7"
---
# Configure auto statistics

Learn how to configure Parallel Data Warehouse to use auto statistics for creating and updating statistics automatically.  Use this capability to improve query plans, and therefore improve query performance.

**Applies to:** APS (starting with 2016-AU7)

## What are statistics?
Statistics for query optimization are objects that contain statistical information about the distribution of values in one or more columns of a table. The query optimizer uses these statistics to estimate the cardinality, or number of rows, in the query result. These cardinality estimates enable the query optimizer to create a high-quality query plan. As an example, in APS, the MPP query optimizer uses cardinality estimates to choose to shuffle or replicate the smaller of two tables used in a join clause and in doing so improve query performance.  For more information, see [Statistics](../relational-databases/statistics/statistics.md) and [DBCC SHOW_STATISTICS](../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)

## What are auto statistics?
Auto statistics are statistics that the query optimizer creates and updates automatically to improve the query plan. Statistics can become out-of-date after loads, inserts, updates and deletes operations. Without auto statistics, you need to do your own analysis to understand which columns need statistics and when the statistics need to be updated.

Auto statistics includes the following three settings: 

### AUTO_CREATE_STATISTICS
When the automatic create statistics option, AUTO_CREATE_STATISTICS, is ON, the Query Optimizer creates statistics on individual columns in the query predicate, as necessary, to improve cardinality estimates for the query plan. These single-column statistics are created on columns that do not already have a histogram in an existing statistics object.

### AUTO_UPDATE_STATISTICS 
When the automatic update statistics option, AUTO_UPDATE_STATISTICS, is on, the query optimizer determines when statistics might be out-of-date and then updates them when they are used by a query. Statistics become out-of-date after operations insert, update, delete, or merge change the data distribution in the table or indexed view. The query optimizer determines when statistics might be out-of-date by counting the number of data modifications since the last statistics update and comparing the number of modifications to a threshold. The threshold is based on the number of rows in the table or indexed view.

### AUTO_UPDATE_STATISTICS_ASYNC
The asynchronous statistics update option, AUTO_UPDATE_STATISTICS_ASYNC, determines whether the Query Optimizer uses synchronous or asynchronous statistics updates. For APS, the asynchronous statistics update option is ON by default, and the Query Optimizer updates statistics asynchronously. The AUTO_UPDATE_STATISTICS_ASYNC option applies to statistics objects created for indexes, single columns in query predicates, and statistics created with the CREATE STATISTICS statement.

## Configuration settings for System Administrators
After upgrading to APS AU7, auto statistics is enabled by default. The system administrator can enable or disable auto statistics with the [Feature Switch](appliance-feature-switch.md) option in the Appliance Configuration Manager.  Once enabled, users can change the statistics settings per database.
Changing any feature switch values requires a service restart on APS.

## Change auto statistics settings on a database
When auto statistics is enabled by the system administrator, you can use [ALTER DATABASE (Parallel Data Warehouse)](../t-sql/statements/alter-database-transact-sql.md?tabs=sqlpdw) to change the statistics settings on a database. If auto statistics feature switch is enabled by the system administrator, any new databases created after the upgrade to AU7 will have auto statistics enabled. All databases that existed before the upgrade to AU7 have auto statistics disabled. 
The following example enables auto statistics on the existing database myPDW.

```sql
ALTER DATABASE myPDW SET AUTO_CREATE_STATISTICS ON
ALTER DATABASE myPDW SET AUTO_UPDATE_STATISTICS ON 
ALTER DATABASE myPDW SET AUTO_UPDATE_STATISTICS_ASYNC ON
```
 
AUTO_UPDATE STATISTICS_ASYNC option only works if AUTO_UPDATE_STATISTICS is ON.  Therefore, statistics are not updated when AUTO_UPDATE_STATISTICS is OFF and AUTO_UPDATE_STATISTICS_ASYNC is ON. 

### Error messages
You could receive the error message "This option is not supported in PDW".  This error occurs when the system administrator has not enabled auto statistics, and you try to set any of the auto statistics options in ALTER DATABASE. 

### Limitations and Restrictions
Auto statistics does not work on external tables. 

### Check the current values
The following query returns the current values of the auto statistics settings for all databases.

```sql
SELECT NAME
	, IS_AUTO_CREATE_STATS_ON 
	, IS_AUTO_UPDATE_STATS_ON
	, IS_AUTO_UPDATE_STATS_ASYNC_ON
FROM
	sys.databases;
```

A return value of 1 means on the setting is on, and 0 means the setting is off. 

## Next steps
To see how your queries are performing, see [Monitoring Active Queries](monitoring-active-queries.md)
