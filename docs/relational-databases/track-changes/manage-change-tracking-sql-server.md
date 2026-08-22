---
title: "Manage Change Tracking"
description: "Manage Change Tracking (SQL Server)"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: roblescarlos, bspendolini
ms.date: 08/21/2025
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "tracking data changes [SQL Server]"
  - "change tracking [SQL Server], overhead"
  - "change tracking [SQL Server]"
  - "change tracking [SQL Server], managing"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Manage Change Tracking (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  This article describes how to manage change tracking. It also describes how to configure security and determine the effects on storage and performance when change tracking is used.  

<a id="managing-change-tracking"></a>

## Manage Change Tracking

 The following sections list catalog views, permissions, and settings that are relevant for managing change tracking.  

### Catalog Views

 To determine which tables and databases have change tracking enabled, use the following catalog views:  

-   [sys.change_tracking_databases (Transact-SQL)](../system-catalog-views/change-tracking-catalog-views-sys-change-tracking-databases.md)  

-   [sys.change_tracking_tables (Transact-SQL)](../system-catalog-views/change-tracking-catalog-views-sys-change-tracking-tables.md)  

 Also, the [sys.internal_tables](../system-catalog-views/sys-internal-tables-transact-sql.md) catalog view lists the internal tables that are created when change tracking is enabled for a user table.  

### Security

 To access change tracking information by using the [change tracking functions](../system-functions/change-tracking-functions-transact-sql.md), the principal must have the following permissions:  

-   `SELECT` permission on at least the primary key columns on the change-tracked table to the table that is being queried.  

-   `VIEW CHANGE TRACKING` permission on the table for which changes are being obtained. The `VIEW CHANGE TRACKING` permission is required for the following reasons:  

    -   Change tracking records include information about rows that are deleted. The records use the primary key values of the rows that are deleted. A principal might be granted `SELECT` permission for a change tracked table after some sensitive data is deleted. In this case, you don't want that principal to access the deleted information by using change tracking.  

    -   Change tracking information can store information about which columns are changed by update operations. A principal might be denied permission to a column that contains sensitive information. However, because change tracking information is available, a principal can determine that a column value is updated, but the principal can't determine the value of the column.  

<a id="understanding-change-tracking-overhead"></a>

## Understand change tracking overhead

 When you enable change tracking for a table, it affects some administration operations. The following table lists the operations and the effects you should consider.  

| Operation |When change tracking is enabled|  
| --------------- |-------------------------------------|  
| `DROP TABLE` |All change tracking information for the dropped table is removed.|  
| `ALTER TABLE DROP CONSTRAINT` |An attempt to drop the `PRIMARY KEY` constraint fails. You must disable change tracking before you can drop a `PRIMARY KEY` constraint.|  
| `ALTER TABLE DROP COLUMN` |If a column that you're dropping is part of the primary key, dropping the column isn't allowed, regardless of change tracking.<br /><br /> If the column that you're dropping isn't part of the primary key, dropping the column succeeds. However, you should first understand the effect on any application that is synchronizing this data. If column change tracking is enabled for the table, the dropped column might still be returned as part of the change tracking information. It's the responsibility of the application to handle the dropped column.|    
| `ALTER TABLE ADD COLUMN` |If you add a new column to the change tracked table, the addition of the column isn't tracked. Only the updates and changes that are made to the new column are tracked.|  
| `ALTER TABLE ALTER COLUMN` |Data type changes of a non-primary key column aren't tracked.|  
| `ALTER TABLE SWITCH` |Switching a partition fails if one or both tables have change tracking enabled.|  
| `DROP INDEX, or ALTER INDEX DISABLE` |The index that enforces the primary key can't be dropped or disabled.|  
| `TRUNCATE TABLE` |You can truncate a table that has change tracking enabled. However, the rows that the operation deletes aren't tracked, and the minimum valid version is updated. When an application checks its version, the check indicates that the version is too old and a reinitialization is required. This condition is the same as change tracking being disabled, and then reenabled for the table.|  

 Using change tracking adds some overhead to DML operations because the operation stores change tracking information.  

### Effects on DML

 Change tracking is optimized to minimize the performance overhead on DML operations. The incremental performance overhead that comes with using change tracking on a table is similar to the overhead you encounter when you create and maintain an index for a table.

 For each row that a DML operation changes, the system adds a row to the internal change tracking table. The effect of this action, relative to the DML operation, depends on various factors, such as:  

-   The number of primary key columns  

-   The amount of data changed in the user table row  

-   The number of operations performed in a transaction  

 Snapshot isolation, if used, also affects performance for all DML operations, whether change tracking is enabled or not.  

### Effects on storage

 Change tracking data is stored in the following types of internal tables:  

-   Internal change table  

     Each user table that has change tracking enabled gets its own internal change table.  

-   Internal transaction table  

     The database has one internal transaction table.  

 These internal tables affect storage requirements in the following ways:  

-   For each change to each row in the user table, change tracking adds a row to the internal change table. This row has a small fixed overhead plus a variable overhead equal to the size of the primary key columns. The row can contain optional context information set by an application. If you enable column tracking, each changed column requires 4 bytes in the tracking table.    

-   For each committed transaction, change tracking adds a row to an internal transaction table.    

 As with other internal tables, you can determine the space used for the change tracking tables by using the [sp_spaceused](../system-stored-procedures/sp-spaceused-transact-sql.md) stored procedure. You can get the names of the internal tables by using the [sys.internal_tables](../system-catalog-views/sys-internal-tables-transact-sql.md) catalog view, as shown in the following example.  

```sql  
sp_spaceused 'sys.change_tracking_309576141'  
sp_spaceused 'sys.syscommittab'  
```  

## Related content

- [Track data changes (SQL Server)](track-data-changes-sql-server.md)
- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [Database Properties (ChangeTracking Page)](../databases/database-properties-changetracking-page.md)
- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [Change Tracking Catalog Views - sys.change_tracking_databases](../system-catalog-views/change-tracking-catalog-views-sys-change-tracking-databases.md)
- [Change Tracking Catalog Views - sys.change_tracking_tables](../system-catalog-views/change-tracking-catalog-views-sys-change-tracking-tables.md)
- [About Change Tracking (SQL Server)](about-change-tracking-sql-server.md)
- [Work with Change Data](work-with-change-data-sql-server.md)
