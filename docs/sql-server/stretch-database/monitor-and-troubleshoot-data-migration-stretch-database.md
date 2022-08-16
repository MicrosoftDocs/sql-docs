---
title: Monitor and troubleshoot data migration
description: Monitor and troubleshoot data migration (Stretch Database)
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql-server-stretch-database
ms.topic: conceptual
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "Stretch Database, monitoring"
  - "monitoring Stretch Database"
---
# Monitor and troubleshoot data migration (Stretch Database)

[!INCLUDE [sqlserver2016-windows-only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

## Check the status of data migration in a dynamic management view

Open the dynamic management view `sys.dm_db_rda_migration_status` to see how many batches and rows of data have been migrated. For more info, see [sys.dm_db_rda_migration_status &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/stretch-database-sys-dm-db-rda-migration-status.md).

## Troubleshoot data migration

### Rows from my Stretch-enabled table are not being migrated to Azure. What's the problem?

There are several problems that can affect migration. Check the following things.

- Check network connectivity for the SQL Server computer.

- Check that the Azure firewall isn't blocking your SQL Server from connecting to the remote endpoint.

- Check the dynamic management view `sys.dm_db_rda_migration_status` for the status of the latest batch. If an error has occurred, check the error_number, error_state, and error_severity values for the batch.

  - For more info about the view, see [sys.dm_db_rda_migration_status &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/stretch-database-sys-dm-db-rda-migration-status.md).

  - For more info about the content of a SQL Server error message, see [sys.messages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md).

### The Azure firewall is blocking connections from my local server

You may have to add a rule in the Azure firewall settings of the Azure server to let SQL Server communicate with the remote Azure server.

## See also

- [Manage and troubleshoot Stretch Database](manage-and-troubleshoot-stretch-database.md)
