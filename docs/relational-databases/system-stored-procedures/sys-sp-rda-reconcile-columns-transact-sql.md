---
title: "sys.sp_rda_reconcile_columns (Transact-SQL)"
description: Reconciles the columns in the remote Azure table to the columns in the Stretch-enabled SQL Server table.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
f1_keywords:
  - "sys.sp_rda_reconcile_columns"
  - "sys.sp_rda_reconcile_columns_TSQL"
helpviewer_keywords:
  - "sys.sp_rda_reconcile_columns stored procedure"
dev_langs:
  - "TSQL"
---
# sys.sp_rda_reconcile_columns (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Reconciles the columns in the remote Azure table to the columns in the Stretch-enabled SQL Server table.

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

`sp_rda_reconcile_columns` adds columns to the remote table that exist in the Stretch-enabled SQL Server table but not in the remote table. These columns may be columns that you accidentally deleted from the remote table. However, `sp_rda_reconcile_columns` doesn't delete columns from the remote table that exist in the remote table but not in the SQL Server table.

> [!IMPORTANT]  
> When `sp_rda_reconcile_columns` recreates columns that you accidentally deleted from the remote table, it does not restore the data that was previously in the deleted columns.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_rda_reconcile_columns @objname = '@objname'
[ ; ]
```

## Arguments

#### @objname = '*@objname*'

The name of the Stretch-enabled SQL Server table.

## Return code values

`0` (success) or `> 0` (failure).

## Permissions

Requires **db_owner** permissions.

## Remarks

If there are columns in the remote Azure table that no longer exist in the Stretch-enabled SQL Server table, these extra columns don't prevent Stretch Database from operating normally. You can optionally remove the extra columns manually.

## Example

To reconcile the columns in the remote Azure table, run the following statement.

```sql
EXEC sp_rda_reconcile_columns
    @objname = N'StretchEnabledTableName';
```
