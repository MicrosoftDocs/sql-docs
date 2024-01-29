---
title: "Extended stored procedures (Transact-SQL)"
titleSuffix: SQL Server Stretch Database
description: Learn about extended stored procedures that you can use when you work with Stretch-enabled databases. See how to reconcile columns and perform other tasks.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "Stretch Database, stored procedures"
dev_langs:
  - "TSQL"
---
# Stretch Database Extended stored procedures (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

This section describes the extended stored procedures that are related to Stretch Database.

> [!IMPORTANT]  
> [!INCLUDE [stretch-database-deprecation](../../includes/stretch-database-deprecation.md)]

## In this section

- [sys.sp_rda_deauthorize_db](sys-sp-rda-deauthorize-db-transact-sql.md)

  Removes the authenticated connection between a local Stretch-enabled database and the remote Azure database.

- [sys.sp_rda_get_rpo_duration](sys-sp-rda-get-rpo-duration-transact-sql.md)

  Gets the number of hours of migrated data that SQL Server retains in a staging table to help ensure a full restore of the remote Azure database, if a restore is necessary.

- [sys.sp_rda_reauthorize_db](sys-sp-rda-reauthorize-db-transact-sql.md)

  Restores the authenticated connection between a local database enabled for Stretch and the remote database.

- [sys.sp_rda_reconcile_batch](sys-sp-rda-reconcile-batch-transact-sql.md)

  Reconciles the batch ID stored in the Stretch-enabled SQL Server table for the most recently migrated data with the batch ID stored in the remote Azure table.

- [sys.sp_rda_reconcile_columns](sys-sp-rda-reconcile-columns-transact-sql.md)

  Reconciles the columns in the remote Azure table to the columns in the Stretch-enabled SQL Server table.

- [sys.sp_rda_reconcile_indexes](sys-sp-rda-reconcile-indexes-transact-sql.md)

  Queues a schema task to reconcile indexes on the remote table.

- [sys.sp_rda_set_query_mode](sys-sp-rda-set-query-mode-transact-sql.md)

  Specifies whether queries against the current Stretch-enabled database and its tables return both local and remote data (the default), or local data only.

- [sys.sp_rda_set_rpo_duration](sys-sp-rda-set-rpo-duration-transact-sql.md)

  Sets the number of hours of migrated data that SQL Server retains in a staging table to help ensure a full restore of the remote Azure database, if a restore is necessary.

- [sys.sp_rda_test_connection](sys-sp-rda-test-connection-transact-sql.md)

  Tests the connection from SQL Server to the remote Azure server and reports problems that may prevent data migration.

## Related content

- [Stretch Database](../../sql-server/stretch-database/stretch-database.md)
