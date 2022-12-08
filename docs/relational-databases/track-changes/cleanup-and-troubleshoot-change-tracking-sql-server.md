---
description: "Learn how to administer change tracking on SQL Server, Azure SQL Managed Instance, and Azure SQL Database. "
title: "Cleanup and troubleshoot Change Tracking"
ms.date: "10/20/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "change tracking, cleanup"
  - "change tracking, troubleshooting"
  - "change tracking, troubleshoot"
ms.assetid: 
author: JetterMcTedder
ms.author: bspendolini
---
# Cleanup and troubleshoot change tracking

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how Change Tracking performs cleanup operations and how to troubleshoot common issues.  
  
## Change Tracking cleanup

Change Tracking cleanup is invoked automatically every 30 minutes. When this background process wakes up, it purges expired records (records beyond the retention period) from the change tracking side tables. The default retention period is two days and can be set for the Change Tracking automatic cleanup process as shown below:

```sql
ALTER DATABASE <DBNAME>
SET CHANGE_TRACKING = ON (CHANGE_RETENTION = 2 DAYS, AUTO_CLEANUP = ON)
```

The retention period can also be changed with the following command without having to disable and re-enable Change Tracking:

```sql
ALTER DATABASE <DBNAME> 
SET CHANGE_TRACKING (CHANGE_RETENTION = 90 MINUTES)
```

### Auto cleanup

There are two cleanup version numbers that the auto-cleanup process maintains over the course of the cleanup action; invalid cleanup version and hardened cleanup version. When the Change Tracking background thread wakes up, it determines the invalid cleanup version. The invalid cleanup version is the change tracking version which marks the point until which the auto cleanup task will perform the cleanup for the side tables. The auto-cleanup thread traverses through the tables that are enabled for change tracking and calls an internal stored procedure. This procedure contains a loop, which deletes records in batches of approximately 5000. The loop is terminated only when all the expired records in the side table are removed. This delete query then uses the syscommittab table (an in-memory rowstore) to identify the transaction IDs that have a commit timestamp less than the invalid cleanup version. The auto-cleanup process is repeated until the cleanup is done with all change tracking side tables for that particular database. Once the process is done with the final change tracking side table, it updates the hardened cleanup version number to the invalid cleanup version number.
  
### Manual cleanup

In SQL Server 2014 Service Pack 2 and above, manual cleanup of the side tables can be done with the store procedure [sp_flush_CT_internal_table_on_demand](../../relational-databases/system-stored-procedures/sys-sp-flush-ct-internal-table-on-demand-transact-sql.md). This stored procedure accepts a table name as parameter and will attempt to clean up records from the corresponding change tracking internal table. Manual cleanup uses existing invalid version and won't update either the hardened version or existing invalid version upon completion of the procedure.

#### Syntax

```sql
sp_flush_CT_internal_table_on_demand [ @TableToClean= ] 'TableName'
```

The sp_flush_CT_internal_table_on_demand stored procedure will do the following:

- If the tablename parameter is passed (@TableToClean), it will do the cleanup for the corresponding side-table using the current invalid version as the watermark. This option should be used to clear any backlogs left by the cleanup thread.

- If the tablename parameter isn't passed (@TableToClean), then the procedure will do the following:

    1. Determine the invalid version based on the retention period and persist this value in the sysobjvalues table.

    1. Use the invalid version from step the previous step (2a) to do the cleanup on all side tables. If there are tables for which cleanup failed, it will add that to a separate list and proceed with the other tables. After completing all tables, check if there are any tables in the error list and retry these tables.

    1. If the error list isn't empty even after a retry, return. If the error list is empty, proceed to step d.

    1. Update Hardened cleanup version and persist the value in sysobjvalues.

    1. Clean up the syscommittab table with the hardened version from step 2d as the watermark.

## Creating extended events for Change Tracking

> [!NOTE]
> Extended Events Are Not Available With Azure SQL Database

[Extended Events](../../relational-databases/extended-events/extended-events.md) can be used for monitoring and alerting with Change Tracking and can be created with [SQL Server Management Studio (SSMS)](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md).

Change Tracking has two events you can capture with Extended Events:

- syscommittab_cleanup
- change_tracking_cleanup

## Troubleshooting

The following sections outline solutions for Change Tracking performance issues.

### Slow cleanup

If the database is having performance issues and is slow to clean up the side tables, perform the following steps:

Start with getting which tables are being tracked with the following SQL:

```sql
select * from sys.change_tracking_tables
```

Next, get history of change tracking from the history system table with the following SQL:

```sql
select * from dbo.MSchange_tracking_history
```

Identify any tables that are causing issues as indicated in the history table and manually run a cleanup using the following stored procedure for each table, one at a time. The TABLE_NAME parameter is the table that is having issues.

```sql
EXEC [sys].[sp_flush_CT_internal_table_on_demand] @TableToClean = 'TABLE_NAME'
```

### Error cleaning up side tables

If side table cleanup is failing because the process couldn't get lock on the base table, which results in a blocking related error message and sp_flush_CT_internal_table_on_demand is also failing, there may be an active transaction on the base table creating the lock.

A result of this could be that seeing the side tables need to clean up and could block syscommittab from being cleaned up, syscommittab will grow large and cause performance issues. To remedy this situation, the following two solutions can be attempted to remove the lock:

- Disable auto cleanup then manually clean-up side tables
- Disable auto cleanup and failover to another instance. This will kill the process and you can clean the tables and then fail back

### Auto-cleanup not able to keep up with transactions

If it's discovered that the auto-cleanup job is able to clean up the side tables and syscommittab using the 30-minute interval, run a manual cleanup job with greater frequency to aid in the process. For SQL Server and Azure SQL Managed Instances, [create a background job](../../ssms/agent/create-a-job.md) using sp_flush_CT_internal_table_on_demand with shorter internal than the default 30 minutes. For Azure SQL, [Azure Logic Apps](/azure/connectors/connectors-create-api-sqlazure) can be used to schedule these jobs.

The follow is a sample script that can be used to create a job to help clean up the side tables for Change Tracking:

```sql
-- Loop to invoke manual cleanup procedure for cleaning up change tracking tables in a database

-- Fetch the tables enabled for Change Tracking
select identity(int, 1,1) as TableID, (SCHEMA_NAME(tbl.Schema_ID) +'.'+ object_name(ctt.object_id)) as TableName
into #CT_Tables
from sys.change_tracking_tables  ctt
INNER JOIN sys.tables tbl
ON tbl.object_id = ctt.object_id

-- Set up the variables
declare @start int = 1, @end int = (select count(*) from #CT_Tables), @tablename varchar(255)
while (@start <= @end)
begin 
 -- Fetch the table to be cleaned up
 select @tablename = TableName from #CT_Tables where TableID = @start
 -- Execute the manual cleanup stored procedure
 exec sp_flush_CT_internal_table_on_demand @tablename 
 -- Increment the counter
 set @start = @start + 1
end
drop table #CT_Tables
```

## See also

 [About Change Tracking &#40;Transact-SQL&#41;](../../relational-databases/track-changes/about-change-tracking-sql-server.md)  
 [Change Tracking Cleanup &#40;Transact-SQL&#41;](../../relational-databases/track-changes/cleanup-and-troubleshoot-change-tracking-sql-server.md)  
 [Change Tracking Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-functions-transact-sql.md)  
 [Change Tracking Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/change-tracking-stored-procedures-transact-sql.md)  
 [Change Tracking System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/change-tracking-tables-transact-sql.md)  
