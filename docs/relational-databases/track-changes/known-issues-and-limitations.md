---
title: "Known issues and limitations"
description: "Known issues and limitations Change Data Capture"
author: croblesm
ms.author: roblescarlos
ms.reviewer: "mathoma"
ms.date: "09/01/2023"
ms.service: sql
ms.topic: troubleshooting
helpviewer_keywords:
  - "Change data capture"
  - "Known issues"
  - "Limitations"
---

# Known issues and limitations

This article explains Change Data Capture (CDC) known issues and limitations with **SQL Server and Azure SQL Managed Instance**. For Azure SQL Database, see [CDC with Azure SQL Database - Known issues and limitations](/azure/azure-sql/database/change-data-capture-overview#known-issues-and-limitations). 

### Aggressive log truncation

When you enable change data capture (CDC) on SQL Server, the aggressive log truncation feature of Accelerated Database Recovery (ADR) is disabled. This is because the CDC scan accesses the database transaction log. Active transactions continue to hold the transaction log truncation until the transaction commits and CDC scan catches up, or transaction aborts. This might result in the transaction log filling up more than usual and should be monitored so that the transaction log doesn't fill.

When enabling CDC, we recommend using the Resumable index option. Resumable index doesn't require to keep open a long-running transaction to create or rebuild an index, allowing log truncation during this operation and better log space management. For more information, see [Guidelines for online index operations - Resumable Index considerations](../../relational-databases/indexes/guidelines-for-online-index-operations.md#resumable-index-considerations)

### Attempt to enable CDC fails if the custom schema or user named `cdc` pre-exist in database*

When you enable CDC on a database, it creates a new schema and user named `cdc`. So, it's not recommended to manually create custom schema or user named `cdc`, as it's reserved for system use.  

If you've manually defined a custom schema or user named `cdc` in your database that isn't related to CDC, the system stored procedure `sys.sp_cdc_enable_db` fails to enable CDC on the database with below error message.

> The database `<database_name>` cannot be enabled for Change Data Capture because a database user named 'cdc' or a schema named 'cdc' already exists in the current database. These objects are required exclusively by Change Data Capture. Drop or rename the user or schema and retry the operation.

To resolve this issue:

- Manually drop the empty `cdc` schema and `cdc` user. Then, CDC can be enabled successfully on the database.

### CDC fails after ALTER COLUMN to VARCHAR and VARBINARY

When the datatype of a column on a CDC-enabled table is changed from `TEXT` to `VARCHAR` or `IMAGE` to `VARBINARY` and an existing row is updated to an off-row value. After the update, the CDC scan will result in errors.

### Columnstore indexes

Change data capture can't be enabled on tables with a clustered columnstore index. Beginning with SQL Server 2016, it can be enabled on tables with a nonclustered columnstore index.

### Computed columns

CDC doesn't support the values for computed columns even if the computed column is defined as persisted. Computed columns that are included in a capture instance always have a value of `NULL`. This behavior is intended, and not a bug.

### DDL changes to source tables

Changing the size of columns of a CDC-enabled table using DDL statements can cause issues with the subsequent CDC capture process, resulting in **error 2628** or **error 8115**. Remember that data in CDC change tables are retained based on user-configured settings. So, before making any changes to column size, you must assess whether the alteration is compatible with the existing data in CDC change tables.

If the `sys.dm_cdc_errors` indicate that scans are failing due to the **error 2628** or **error 8115** for change tables, you should first consume the change data in the affected change tables. After that, you need to [disable and then reenable CDC](enable-and-disable-change-data-capture-sql-server.md) on the table to resolve the problem effectively.

### Import database using data-tier Import/Export and Extract/Publish operations

For CDC enabled SQL databases, when you use SqlPackage, SSDT, or other SQL tools to Import/Export or Extract/Publish, the `cdc` schema and user get excluded in the new database. Other CDC objects not included in Import/Export and Extract/Deploy operations include the tables marked as `is_ms_shipped=1` in sys.objects.

Even if CDC isn't enabled and you've defined a custom schema or user named `cdc` in your database that will also be excluded in Import/Export and Extract/Deploy operations to import/setup a new database.

### Linux

CDC is supported for SQL Server 2017 on Linux starting with CU18, and SQL Server 2019 on Linux.

### Partition switching with variables

Using variables with partition switching on databases or tables with change data capture (CDC) isn't supported for the `ALTER TABLE ... SWITCH TO ... PARTITION ...` statement. See [partition switching limitations](../replication/publish/replicate-partitioned-tables-and-indexes.md#replication-support-for-partition-switching) to learn more. 

## Next steps

- For an overview of Change Data Capture for SQL Server, see [What is change data capture (CDC)?](/sql/relational-databases/track-changes/about-change-data-capture-sql-server)
- For more information about known issues and limitations for CDC with Azure SQL Database, see [CDC known issues and limitations with Azure SQL Database](/azure/azure-sql/database/change-data-capture-overview#known-issues-and-limitations)
- For more information on how to enable and disable CDC, see [Enable and disable change data capture](/sql/relational-databases/track-changes/enable-and-disable-change-data-capture-sql-server)