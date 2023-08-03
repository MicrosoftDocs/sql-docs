---
title: "View a Database Snapshot (SQL Server)"
description: Learn how to view a SQL Server database snapshot using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jopilov
ms.date: 05/04/2023
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "database snapshots [SQL Server], viewing"
  - "displaying database snapshots"
  - "viewing database snapshots"
---
# View a Database Snapshot (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article explains how to view a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database snapshot using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

> [!NOTE]  
> To create, revert to, or delete a database snapshot, you must use [!INCLUDE[tsql](../../includes/tsql-md.md)].

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

**To view a database snapshot**

1. In Object Explorer, connect to the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.

1. Expand **Databases.**

1. Expand **Database Snapshots**, and select the snapshot you want to view.

## <a id="TsqlProcedure"></a> Use Transact-SQL

**To view a database snapshot**

1. Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].
1. From the **Standard** bar, select **New Query**.
1. To list the database snapshots of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], query the `source_database_id` column of the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view for non-NULL values.
1. You can also use this query to get details about the database snapshot and its files

   ```sql
   SELECT
    db_name(db.source_database_id) source_database,
    db.name AS snapshot_db_name,
    db.database_id,
    db.source_database_id,
    db.create_date,
    db.compatibility_level,
    db.is_read_only,
    mf.physical_name
   FROM sys.databases db
   INNER JOIN sys.master_files mf
    ON db.database_id = mf.database_id
   WHERE db.source_database_id is not null
    AND mf.is_sparse =1
   ORDER BY db.name;
   ```

## <a id="RelatedTasks"></a> Related Tasks

- [Create a Database Snapshot (Transact-SQL)](../../relational-databases/databases/create-a-database-snapshot-transact-sql.md)

- [Revert a Database to a Database Snapshot](../../relational-databases/databases/revert-a-database-to-a-database-snapshot.md)

- [Drop a Database Snapshot (Transact-SQL)](../../relational-databases/databases/drop-a-database-snapshot-transact-sql.md)

## Next steps

- [Database Snapshots (SQL Server)](../../relational-databases/databases/database-snapshots-sql-server.md)
