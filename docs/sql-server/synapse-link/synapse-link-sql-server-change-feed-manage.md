---
title: "Manage Azure Synapse Link for SQL Server and Azure SQL Database"
description: Learn about managing the Azure Synapse Link change feed with T-SQL.
ms.date: 05/24/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: data-movement
ms.topic: conceptual
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
ms.custom:
- event-tier1-build-2022
---

# Manage Azure Synapse Link for SQL Server and Azure SQL Database

[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

This article provides details on monitoring and managing [Azure Synapse Link for SQL change feed](synapse-link-sql-server-change-feed.md), with T-SQL.

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- For more information, see:
    - [Azure Synapse Link for SQL Server](/azure/synapse-analytics/synapse-link/sql-server-2022-synapse-link).
    - [Azure Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/sql-database-synapse-link).
- To get started quickly, see: 
    - [Get started with Azure Synapse Link for SQL Server 2022](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022) 
    - [Get started with Azure Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-database)

The Azure Synapse Link for Azure SQL Database is entirely managed, including provisioning of the landing zone, and uses similar change detection processes as described in this article. For more information, see [Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/sql-database-synapse-link). 

For SQL Server, the landing zone is customer-managed and visible, but it is not recommended or supported to consume or modify the files in the landing zone. 

Currently, only a member of the sysadmin server role in SQL Server or the db_owner database role can execute these procedures. 

The SQL Server or the Azure SQL Database will maintain metadata specific to each table group.

> [!NOTE]
> Enabling Azure Synapse Link for SQL will create a `changefeed` database user, a `changefeed` schema, and several tables within the `changefeed` schema in your source database. Please do not alter any of these objects - they are system-managed.

## Monitor Azure Synapse Link for SQL Server and Azure SQL Database

The following system objects allow for querying the state of the Azure Synapse Link for SQL feature from the source database.

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

To view the current activity, use the dynamic management view [sys.dm_change_feed_log_scan_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-change-feed-log-scan-sessions.md), which returns activity for the Azure Synapse Link fo SQL change feed.

```sql
SELECT * FROM sys.dm_change_feed_log_scan_sessions;
```

## Enabling the change feed and creating change feed objects

The system stored procedures `sys.sp_change_feed_enable_table`, `sys.sp_change_feed_enable_db`, `sys.sp_change_feed_create_table_group` are undocumented and for internal use only. Always use Synapse Studio in the Azure portal to create and configure the Azure Synapse Link for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and Azure SQL Database. The following drop and disable stored procedures are provided to remove the Azure Synapse Link in the case where the Azure Synapse Studio has been removed or is inaccessible.

## Disable change feed for database

To disable the change feed at the database level, and subsequently the metadata for all the associated tables, use the [sys.sp_change_feed_disable_db](../../relational-databases/system-stored-procedures/sp-change-feed-disable-db.md) system stored procedure. 

When the change feed is disabled with active table groups, all connections and schedulers will be stopped immediately/forcefully without waiting for the current operations are completed. No new change feed table groups can be created for the database, and all the existing metadata describing the table groups will be deleted. Re-enabling change feed will result in clean initializations of all table groups and reseeding of all the data.  

```sql
EXECUTE sys.sp_change_feed_disable_db 
GO 
```

## Drop change feed table group 

It is recommended to use Azure Synapse Studio in the Azure portal to configure and manage the Azure Synapse Link.

To drop the change feed metadata for a table group, use the [sys.sp_change_feed_drop_table_group](../../relational-databases/system-stored-procedures/sp-change-feed-drop-table-group.md) system stored procedure.

If a table group's change feed is dropped on the SQL Server or Azure SQL Database side, all replication activities for the individual change feed tables associated with this table group will stop. All the associated metadata is also deleted.  

```sql
EXECUTE sys.sp_change_feed_drop_table_group
      @table_group_id uniqueidentifier 
GO
```

## Drop change feed table

It is recommended to use Azure Synapse Studio in the Azure portal to configure and manage the Azure Synapse Link.

To remove a change feed table from a change feed table group, use the [sys.sp_change_feed_disable_table](../../relational-databases/system-stored-procedures/sp-change-feed-disable-table.md) system stored procedure.

When `sys.sp_change_feed_disable_table` is called, publishing changes for this table will be immediately stopped. Changes scanned but not published yet will be ignored. The last changes published and synchronized to Azure Synapse cannot be guaranteed. To guarantee synchronization between source and target up to a certain time, verify the "last transaction commit time" on the target and then call this procedure.

```sql
EXECUTE sys.sp_change_feed_disable_table
    @table_group_id uniqueidentifier,
    @table_id uniqueidentifier
GO
```

## See also

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [What's new in SQL Server 2022?](../what-s-new-in-sql-server-2022.md)
- [Azure Synapse Link for SQL Server](/azure/synapse-analytics/synapse-link/sql-server-2022-synapse-link)
- [Azure Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/sql-database-synapse-link)
- [Azure Synapse Link for Azure Cosmos DB](/azure/cosmos-db/synapse-link)
- [Azure Synapse Link for Dataverse](/powerapps/maker/data-platform/export-to-data-lake)

## Next steps

- [Get started with Azure Synapse Link for SQL Server](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022)
