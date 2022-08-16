---
title: Disable Stretch Database and bring back remote data
description: Disable Stretch Database and bring back remote data
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql-server-stretch-database
ms.topic: conceptual
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "Stretch Database, disabling"
  - "disabling Stretch Database"
---
# Disable Stretch Database and bring back remote data

[!INCLUDE [sqlserver2016-windows-only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

You can use Transact-SQL to disable Stretch Database for a table or for a database.

After you disable Stretch Database for a table, data migration stops and query results no longer include results from the remote table.

If you want to pause data migration, see [Pause and resume data migration &#40;Stretch Database&#41;](pause-and-resume-data-migration-stretch-database.md).

> [!NOTE]
> Disabling Stretch Database for a table or for a database does not delete the remote object. If you want to delete the remote table or the remote database, you have to drop it by using the Azure management portal. The remote objects continue to incur Azure costs until you delete them. For more info, see [SQL Server Stretch Database Pricing](https://azure.microsoft.com/pricing/details/sql-server-stretch-database/).

## Disable Stretch Database for a table

### Use Transact-SQL

- To disable Stretch for a table and copy the remote data for the table from Azure back to SQL Server, run the following command. After all the remote data has been copied from Azure back to SQL Server, Stretch is disabled for the table.

  This command can't be canceled.

  ```sql
  USE [<Stretch-enabled database name>];
  GO
  ALTER TABLE [<Stretch-enabled table name>]
     SET ( REMOTE_DATA_ARCHIVE ( MIGRATION_STATE = INBOUND ) );
  GO
  ```

  Copying the remote data for the table from Azure back to SQL Server incurs data transfer costs. For more info, see [Data Transfers Pricing Details](https://azure.microsoft.com/pricing/details/data-transfers/).

- To disable Stretch for a table and abandon the remote data, run the following command.

  ```sql
  USE <Stretch-enabled database name>;
  GO
  ALTER TABLE <Stretch-enabled table name>
     SET ( REMOTE_DATA_ARCHIVE = OFF_WITHOUT_DATA_RECOVERY ( MIGRATION_STATE = PAUSED ) );
  GO
  ```

  Disabling Stretch Database for a table does not delete the remote data or the remote table. If you want to delete the remote table, you have to drop it by using the Azure management portal. The remote table continues to incur Azure costs until you delete it. For more info, see [SQL Server Stretch Database Pricing](https://azure.microsoft.com/pricing/details/sql-server-stretch-database/).

## Disable Stretch Database for a database

Before you can disable Stretch Database for a database, you have to disable Stretch Database on the individual Stretch-enabled tables in the database.

### Use Transact-SQL

Run the following command.

```sql
ALTER DATABASE [<Stretch-enabled database name>]
    SET REMOTE_DATA_ARCHIVE = OFF;
GO
```

Disabling Stretch Database for a database doesn't delete the remote database. If you want to delete the remote database, you have to drop it by using the Azure management portal. The remote database continues to incur Azure costs until you delete it. For more info, see [SQL Server Stretch Database Pricing](https://azure.microsoft.com/pricing/details/sql-server-stretch-database/).

## See also

- [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [Pause and resume data migration &#40;Stretch Database&#41;](pause-and-resume-data-migration-stretch-database.md)
