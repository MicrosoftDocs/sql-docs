---
title: "sp_dropextendedproc (Transact-SQL)"
description: sp_dropextendedproc drops an extended stored procedure.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dropextendedproc"
  - "sp_dropextendedproc_TSQL"
helpviewer_keywords:
  - "sp_dropextendedproc"
dev_langs:
  - "TSQL"
---
# sp_dropextendedproc (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Drops an extended stored procedure.

> [!NOTE]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [Common Language Runtime Integration](../clr-integration/common-language-runtime-integration-overview.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropextendedproc [ @functname = ] N'functname'
[ ; ]
```

## Arguments

#### [ @functname = ] N'*functname*'

The name of the extended stored procedure to drop. *@functname* is **nvarchar(517)**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Executing `sp_dropextendedproc` drops the user-defined extended stored procedure name from the [sys.objects](../system-catalog-views/sys-objects-transact-sql.md) catalog view, and removes the entry from the [sys.extended_procedures](../system-catalog-views/sys-extended-procedures-transact-sql.md) catalog view. This stored procedure can be run only in the `master` database.

`sp_dropextendedproc` doesn't drop system extended stored procedures. Instead, the system administrator should deny `EXECUTE` permission on the extended stored procedure to the **public** role.

`sp_dropextendedproc` can't be executed inside a transaction.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_dropextendedproc`.

## Examples

The following example drops the `xp_hello` extended stored procedure. This extended stored procedure must already exist, or the example returns an error message.

```sql
USE master;
GO
EXEC sp_dropextendedproc 'xp_hello';
```

## Related content

- [sp_addextendedproc (Transact-SQL)](sp-addextendedproc-transact-sql.md)
- [sp_helpextendedproc (Transact-SQL)](sp-helpextendedproc-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
