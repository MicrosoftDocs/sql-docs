---
title: "Manage Azure Synapse Link for SQL Server and Azure SQL Database"
description: Learn about managing the Azure Synapse Link change feed with T-SQL.
ms.date: 05/24/2022
ms.prod: sql
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
ms.custom:
---

# Manage Azure Synapse Link for SQL Server and Azure SQL Database

[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

This article provides details on managing Azure Synapse Link for SQL Server and Azure SQL Database with T-SQL.

- [What is Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- For more information, see [Synapse Link for SQL Server](/azure/synapse-analytics/synapse-link/sql-server-2022-synapse-link).
- To get started quickly, see [Get started with Synapse Link for SQL Server 2022 (Preview)](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022).

The Azure Synapse Link for Azure SQL Database is entirely managed, including provisioning of the landing zone, and uses similar change detection processes as described in this article. For more information, see [Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/sql-database-synapse-link). 

Only a member of the sysadmin server role in SQL Server or the db_owner database role can execute this procedure. 

## Enable change feed for database 

To enable the change feed for a database and create internal metadata objects, use the [sys.sp_change_feed_enable_db](../../relational-databases/system-stored-procedures/sp-change-feed-enable-db.md) system stored procedure.

```sql
EXECUTE sys.sp_change_feed_enable_db  
      @maxtrans 
GO   
```

## Disable change feed for database

To disable the change feed at the database level, and subsequently the metadata for all the associated tables, use the [sys.sp_change_feed_disable_db](../../relational-databases/system-stored-procedures/sp-change-feed-disable-db.md) system stored procedure. 

When the change feed is disabled with active table groups, all connections and schedulers will be stopped immediately/forcefully without waiting for the current operations are completed. No new change feed table groups can be created for the database, and all the existing metadata describing the table groups will be deleted. Re-enabling change feed will result in clean initializations of all table groups and reseeding of all the data.  

```sql
EXECUTE sys.sp_change_feed_disable_db 
GO 
```

## Create change feed a table group 

To create the change feed metadata for a table group, use the [sys.sp_change_feed_create_table_group](../../relational-databases/system-stored-procedures/sp-change-feed-create-table-group.md) system stored procedure.

The SQL Server or the Azure SQL Database will maintain metadata specific to each table group. A table group is the container for individual tables.

```sql
EXECUTE sys.sp_change_feed_create_table_group
    @table_group_id uniqueidentifier,
    @table_group_name nvarchar(140), 
    @workspace_id nvarchar(247), 
    @destination_location nvarchar(512) = NULL,
    @destination_credential sysname = NULL
GO
```

The Azure Storage location of the landing zone parameters, @destination_location and @destination_credential, can be `NULL` for Azure SQL Database.


## Drop change feed table group 

To drop the change feed metadata for a table group, use the [sys.sp_change_feed_drop_table_group](../../relational-databases/system-stored-procedures/sp-change-feed-drop-table-group.md) system stored procedure.

If a table group's change feed is dropped on the SQL Server or Azure SQL Database side, all replication activities for the individual change feed tables associated with this table group will stop. All the associated metadata is also deleted.  

```sql
EXECUTE sys.sp_change_feed_drop_table_group
      @table_group_id uniqueidentifier 
GO
```

## Enable change feed table 

To add a new change feed table to an existing change feed table group, use the [sys.sp_change_feed_enable_table](../../relational-databases/system-stored-procedures/sp-change-feed-enable-table.md) system stored procedure.

```sql
EXECUTE sys.sp_change_feed_enable_table 
    @table_group_id uniqueidentifier,
    @table_id uniqueidentifier,
    @source_schema sysname,
    @source_name sysname
GO
```

## Drop change feed table

To remove a change feed table from a change feed table group, use the [sys.sp_change_feed_disable_table](../../relational-databases/system-stored-procedures/sp-change-feed-disable-table.md) system stored procedure.

When `sys.sp_change_feed_disable_table` is called, publishing changes for this table will be immediately stopped. Changes scanned but not published yet will be ignored. The last changes published and synchronized to Azure Synapse cannot be guaranteed. To guarantee synchronization between source and target up to a certain time, verify the "last transaction commit time" on the target and then call this procedure.

```sql
EXECUTE sys.sp_change_feed_disable_table
    @table_group_id uniqueidentifier,
    @table_id uniqueidentifier
GO
```

## Monitor Azure Synapse Link for SQL Server

The following system objects allow for querying the state of the Synapse Link feature from the source database.

### View configuration

To review the current configuration of link, execute the [sys.sp_help_change_feed](../../relational-databases/system-stored-procedures/sp-help-change-feed.md) system stored procedure.

```sql
EXECUTE sys.sp_help_change_feed
```

### Review change feed errors

To review errors in the [Azure Synapse Link change feed](synapse-link-sql-server-change-feed.md), use the dynamic management view [sys.dm_change_feed_errors](../../relational-databases/system-dynamic-management-views/sys-dm-change-feed-errors.md). This DMV will show errors from last 32 sessions. One session might include multiple errors, for example, retry attempts on landing zone failures. This DMV will also show errors faced during snapshot and incremental change publish process.

```sql
SELECT * FROM sys.dm_change_feed_errors;
```

### View current activity

To view the current activity, use the dynamic management view [sys.dm_change_feed_errors](../../relational-databases/system-dynamic-management-views/sys-dm-change-feed-errors.md).

```sql
SELECT * FROM sys.dm_change_feed_log_scan_sessions;
```

## See also

- [What is Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [What's new in SQL Server 2022?](../what-s-new-in-sql-server-2022.md)
- [Synapse Link for SQL Server](/azure/synapse-analytics/synapse-link/sql-server-2022-synapse-link)
- [Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/sql-database-synapse-link)
- [Synapse Link for Azure Cosmos DB](/azure/cosmos-db/synapse-link)
- [Synapse Link for Dataverse](/powerapps/maker/data-platform/export-to-data-lake)

## Next steps

- [Get started with Synapse Link for SQL Server](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022)
