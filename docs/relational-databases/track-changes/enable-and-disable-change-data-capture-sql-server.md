---
title: "Enable and Disable change data capture"
description: "Enable and Disable change data capture"
author: croblesm
ms.author: roblescarlos
ms.reviewer: mathoma
ms.date: 10/19/2023
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "change data capture, enabling tables"
  - "change data capture, enabling databases"
  - "change data capture, disabling databases"
  - "change data capture, disabling tables"
---

# Enable and disable change data capture

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to enable and disable change data capture (CDC) for a database and a table for SQL Server and Azure SQL Managed Instance. For Azure SQL Database, see [CDC with Azure SQL Database](/azure/azure-sql/database/change-data-capture-overview).

## Permissions

The `sysadmin` permissions are required to enable or disable change data capture in SQL Server and Azure SQL Managed Instance.

## Enable for a database

Before you can create a capture instance for individual tables, you must enable change data capture for the database.

To enable change data capture, run the stored procedure [sys.sp_cdc_enable_db (Transact-SQL)](../system-stored-procedures/sys-sp-cdc-enable-db-transact-sql.md) in the database context. To determine if a database already has CDC enabled, query the **is_cdc_enabled** column in the `sys.databases` catalog view.

When a database has change data capture enabled, the **cdc** schema, **cdc** user, metadata tables, and other system objects are created for the database. The **cdc** schema contains the change data capture metadata tables and, after source tables are enabled for change data capture, the individual change tables serve as a repository for change data. The **cdc** schema also contains associated system functions used to query for change data.

Change data capture requires exclusive use of the **cdc** schema and **cdc** user. If either a schema or a database user named *cdc* currently exists in a database, the change data capture can't be enabled for the database until the schema and/or user is dropped or renamed.

```sql
-- ====
-- Enable Database for CDC
-- ====
USE MyDB
GO
EXEC sys.sp_cdc_enable_db
GO
```

> [!NOTE]  
> To find CDC-related templates in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], go to **View**, select **Template Explorer**, and then select **SQL Server Templates**. **Change data capture** is a sub-folder that contains the templates

## Disable for a database

Use [sys.sp_cdc_disable_db (Transact-SQL)](../../relational-databases/system-stored-procedures/sys-sp-cdc-disable-db-transact-sql.md) in the database context to disable change data capture for a database. It's not necessary to disable CDC for individual tables before you disable CDC for the database. Disabling CDC for the database removes all associated change data capture metadata, including the **cdc** user, schema and the change data capture jobs. However, any gating roles created by CDC won't be removed automatically and must be explicitly deleted. To determine if a database has CDC enabled, query the **is_cdc_enabled** column in the `sys.databases` catalog view.

If a CDC-enabled database is dropped, change data capture jobs are automatically removed.

```sql
-- Disable Database for change data capture
USE MyDB
GO
EXEC sys.sp_cdc_disable_db
GO
```

## Enable for a table

After a database has been enabled for change data capture, members of the **db_owner** fixed database role can create a capture instance for individual source tables by using the stored procedure `sys.sp_cdc_enable_table`. To determine whether a source table has already been enabled for change data capture, examine the is_tracked_by_cdc column in the `sys.tables` catalog view.

> [!IMPORTANT]  
> For more information about the `sys.sp_cdc_enable_table` stored procedure arguments, see [sys.sp_cdc_enable_table (Transact-SQL)](../system-stored-procedures/sys-sp-cdc-enable-table-transact-sql.md).

The following options can be specified when creating a capture instance:

**Columns in the source table to be captured**.

By default, all of the columns in the source table are identified as captured columns. If only a subset of columns needs to be tracked, such as for privacy or performance reasons, use the *\@captured_column_list* parameter to specify the subset of columns.

**A filegroup to contain the change table.**

By default, the change table is located in the default filegroup of the database. Database owners who want to control the placement of individual change tables can use the *\@filegroup_name* parameter to specify a particular filegroup for the change table associated with the capture instance. The named filegroup must already exist. Generally, it's recommended that change tables be placed in a filegroup separate from source tables. See the **Enable a Table Specifying Filegroup Option** template for an example showing use of the *\@filegroup_name* parameter.

```sql
-- Enable CDC for a table specifying filegroup
USE MyDB
GO

EXEC sys.sp_cdc_enable_table
    @source_schema = N'dbo',
    @source_name   = N'MyTable',
    @role_name     = N'MyRole',
    @filegroup_name = N'MyDB_CT',
    @supports_net_changes = 1
GO
```

**A role for controlling access to a change table.**

The purpose of the named role is to control access to the change data. The specified role can be an existing fixed server role or a database role. If the specified role doesn't already exist, a database role of that name is created automatically. Users must have SELECT permission on all the captured columns of the source table. In addition, when a role is specified, users who aren't members of either the **sysadmin** or **db_owner** role must also be members of the specified role.

If you don't want to use a gating role, explicitly set the *\@role_name* parameter to NULL. See the **Enable a Table Without Using a Gating Role** template for an example of enabling a table without a gating role.

```sql
-- Enable CDC for a table using a gating role option
USE MyDB
GO
    EXEC sys.sp_cdc_enable_table
    @source_schema = N'dbo',
    @source_name   = N'MyTable',
    @role_name     = NULL,
    @supports_net_changes = 1
GO
```

**A function to query for net changes.**

A capture instance always includes a table valued function (TVF) for returning all change table entries that occurred within a defined interval. This function is named by appending the capture instance name to `cdc.fn_cdc_get_all_changes_``. For more information, see [cdc.fn_cdc_get_all_changes_<capture_instance> (Transact-SQL)](../system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql.md).

If the parameter *\@supports_net_changes* is set to 1, a net changes function is also generated for the capture instance. This function returns only one change for each distinct row changed in the interval specified in the call. For more information, see [cdc.fn_cdc_get_net_changes_<capture_instance> (Transact-SQL)](../system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql.md).

To support net changes queries, the source table must have a primary key or unique index to uniquely identify rows. If a unique index is used, the name of the index must be specified using the *\@index_name* parameter. The columns defined in the primary key or unique index must be included in the list of source columns to be captured.

See the **Enable a Table for All and Net Changes Queries** template for an example demonstrating the creation of a capture instance with both query functions.

```sql
-- Enable CDC for a table for all and net changes queries
USE MyDB
GO
EXEC sys.sp_cdc_enable_table
    @source_schema = N'dbo',
    @source_name   = N'MyTable',
    @role_name     = N'MyRole',
    @supports_net_changes = 1
GO
```

> [!NOTE]  
> If change data capture is enabled on a table with an existing primary key, and the *\@index_name* parameter is not used to identify an alternative unique index, the change data capture feature uses the primary key. Subsequent changes to the primary key aren't allowed without first disabling change data capture for the table. This is true regardless of whether support for net changes queries was requested when change data capture was configured. If there is no primary key on a table at the time it is enabled for change data capture, the subsequent addition of a primary key is ignored by change data capture. Because change data capture will not use a primary key that is created after the table was enabled, the key and key columns can be removed without restrictions.

## Disable for a table

Members of the **db_owner** fixed database role can remove a capture instance for individual source tables by using the stored procedure `sys.sp_cdc_disable_table`. To determine whether a source table is currently enabled for change data capture, examine the `is_tracked_by_cdc` column in the `sys.tables` catalog view. If there are no tables enabled for the database after the disabling takes place, the change data capture jobs are also removed.

If a change data capture-enabled table is dropped, change data capture metadata that is associated with the table is automatically removed.

See the Disable a Capture Instance for a Table template for an example of disabling a table.

```sql
-- Disable a Capture Instance for a table
USE MyDB
GO
    EXEC sys.sp_cdc_disable_table
    @source_schema = N'dbo',
    @source_name   = N'MyTable',
    @capture_instance = N'dbo_MyTable'
GO
```


## See also

- [Track Data Changes (SQL Server)](track-data-changes-sql-server.md)
- [About change data capture (SQL Server)](about-change-data-capture-sql-server.md)
- [Work with Change Data (SQL Server)](work-with-change-data-sql-server.md)
- [Administer and Monitor change data capture (SQL Server)](administer-and-monitor-change-data-capture-sql-server.md)
