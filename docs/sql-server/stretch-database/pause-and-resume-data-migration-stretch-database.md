---
title: Pause and resume data migration
description: Pause and resume data migration (Stretch Database)
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql-server-stretch-database
ms.topic: conceptual
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "Stretch Database, pausing and resuming"
  - "pausing Stretch Database"
  - "resuming Stretch Database"
---
# Pause and resume data migration (Stretch Database)

[!INCLUDE [sqlserver2016-windows-only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

To pause or resume data migration to Azure, select **Stretch** for a table in SQL Server Management Studio, and then select **Pause** to pause data migration or **Resume** to resume data migration. You can also use Transact-SQL to pause or resume data migration.

Pause data migration on individual tables when you want to troubleshoot problems on the local server or to maximize the available network bandwidth.

## Pause data migration

### Use SQL Server Management Studio

1. In SQL Server Management Studio, in Object Explorer, select the Stretch-enabled table for which you want to pause data migration.

1. Right-click and select **Stretch > Pause**.

### Use Transact-SQL

Run the following command.

```sql
USE [<Stretch-enabled database name>];
GO
ALTER TABLE [<Stretch-enabled table name>]
    SET ( REMOTE_DATA_ARCHIVE ( MIGRATION_STATE = PAUSED ) );
GO
```

## Resume data migration

### Use SQL Server Management Studio

1. In SQL Server Management Studio, in Object Explorer, select the Stretch-enabled table for which you want to resume data migration.

1. Right-click and select **Stretch > Resume**.

### Use Transact-SQL

Run the following command.

```sql
USE [<Stretch-enabled database name>];
GO
ALTER TABLE [<Stretch-enabled table name>]
    SET ( REMOTE_DATA_ARCHIVE ( MIGRATION_STATE = OUTBOUND ) );
GO
```

## Check whether migration is active or paused

### Use SQL Server Management Studio

In SQL Server Management Studio, open **Stretch Database Monitor** and check the value of the **Migration State** column. For more info, see [Monitor and troubleshoot data migration](monitor-and-troubleshoot-data-migration-stretch-database.md).

### Use Transact-SQL

Query the catalog view `sys.remote_data_archive_tables` and check the value of the `is_migration_paused` column. For more info, see [sys.remote_data_archive_tables](../../relational-databases/system-catalog-views/stretch-database-catalog-views-sys-remote-data-archive-tables.md).

## See also

- [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)
[Monitor and troubleshoot data migration](monitor-and-troubleshoot-data-migration-stretch-database.md)
