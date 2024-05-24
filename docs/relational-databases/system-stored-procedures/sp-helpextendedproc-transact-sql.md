---
title: "sp_helpextendedproc (Transact-SQL)"
description: Reports the currently defined extended stored procedures and the name of the dynamic-link library (DLL) to which the procedure (function) belongs.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpextendedproc"
  - "sp_helpextendedproc_TSQL"
helpviewer_keywords:
  - "sp_helpextendedproc"
dev_langs:
  - "TSQL"
---
# sp_helpextendedproc (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports the currently defined extended stored procedures and the name of the dynamic-link library (DLL) to which the procedure (function) belongs.

> [!NOTE]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [Common Language Runtime Integration](../clr-integration/common-language-runtime-integration-overview.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpextendedproc [ [ @funcname = ] N'funcname' ]
[ ; ]
```

## Arguments

#### [ @funcname = ] N'*funcname*'

The name of the extended stored procedure for which information is reported. *@funcname* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **sysname** | Name of the extended stored procedure. |
| `dll` | **nvarchar(255)** | Name of the DLL. |

## Remarks

When *@funcname* is specified, `sp_helpextendedproc` reports on the specified extended stored procedure. When this parameter isn't supplied, `sp_helpextendedproc` returns all extended stored procedure names and the DLL names to which each extended stored procedure belongs.

## Permissions

Permission to execute `sp_helpextendedproc` is granted to **public**.

## Examples

### A. Reporting help on all extended stored procedures

The following example reports on all extended stored procedures.

```sql
USE master;
GO
EXEC sp_helpextendedproc;
GO
```

### B. Reporting help on a single extended stored procedure

The following example reports on the `xp_cmdshell` extended stored procedure.

```sql
USE master;
GO
EXEC sp_helpextendedproc xp_cmdshell;
GO
```

## Related content

- [sp_addextendedproc (Transact-SQL)](sp-addextendedproc-transact-sql.md)
- [sp_dropextendedproc (Transact-SQL)](sp-dropextendedproc-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
