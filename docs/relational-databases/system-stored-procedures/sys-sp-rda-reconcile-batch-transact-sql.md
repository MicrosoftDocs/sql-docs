---
title: "sys.sp_rda_reconcile_batch (Transact-SQL)"
description: Reconciles the batch ID stored in the Stretch-enabled SQL Server table with the batch ID stored in the remote Azure table.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
f1_keywords:
  - "sys.sp_rda_reconcile_batch"
  - "sys.sp_rda_reconcile_batch_TSQL"
helpviewer_keywords:
  - "sys.sp_rda_reconcile_batch stored procedure"
dev_langs:
  - "TSQL"
---
# sys.sp_rda_reconcile_batch (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Reconciles the batch ID stored in the Stretch-enabled SQL Server table with the batch ID stored in the remote Azure table.

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

Typically you only have to run `sp_rda_reconcile_batch` if you have manually deleted the most recently migrated data from the remote table. When you manually delete remote data that includes the most recent batch, the batch IDs are out of sync and migration stops.

To delete data that has already been migrated to Azure, see the Remarks on this page.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_rda_reconcile_batch @objname = '@objname'
[ ; ]
```

## Arguments

#### @objname = '*@objname*'

The name of the Stretch-enabled SQL Server table.

## Permissions

Requires **db_owner** permissions.

## Remarks

If you want to delete data that has already been migrated to Azure, do the following things.

1. Pause data migration. For more info, see [Pause and resume data migration (Stretch Database)](../../sql-server/stretch-database/pause-and-resume-data-migration-stretch-database.md).

1. Delete the data from the SQL Server staging table by running a `DELETE` command with the `STAGE_ONLY` hint. For more info, see [Make administrative updates and deletes](../../sql-server/stretch-database/manage-and-troubleshoot-stretch-database.md#adminHints).

1. Delete the  same data from the remote Azure table by running a `DELETE` command with the `REMOTE_ONLY` hint.

1. Run `sp_rda_reconcile_batch`.

1. Resume data migration. For more info, see [Pause and resume data migration (Stretch Database)](../../sql-server/stretch-database/pause-and-resume-data-migration-stretch-database.md).

## Example

To reconcile the batch IDs, run the following statement.

```sql
EXEC sp_rda_reconcile_batch
    @objname = N'StretchEnabledTableName';
```
