---
title: "DBCC TRACEOFF (Transact-SQL)"
description: DBCC TRACEOFF disables the specified trace flags.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "TRACEOFF_TSQL"
  - "TRACEOFF"
  - "DBCC TRACEOFF"
  - "DBCC_TRACEOFF_TSQL"
helpviewer_keywords:
  - "trace flags [SQL Server], disabling"
  - "DBCC TRACEOFF statement"
  - "disabling trace flags"
dev_langs:
  - "TSQL"
---
# DBCC TRACEOFF (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Disables the specified trace flags.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC TRACEOFF ( trace# [ , ...n ] [ , -1 ] ) [ WITH NO_INFOMSGS ]
```

## Arguments

#### *trace#*

The number of the trace flag to disable.

#### `-1`

Disables the specified trace flags globally.

#### WITH NO_INFOMSGS

Suppresses all informational messages that have severity levels from 0 through 10.

## Remarks

Trace flags are used to customize certain characteristics controlling how the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] operates.

## Result sets

`DBCC TRACEOFF` returns the following message:

```output
DBCC execution completed. If DBCC printed error messages, contact your system administrator.
```

## Permissions

Requires membership in the **sysadmin** fixed server role.

## Examples

The following example disables trace flag 3205.

```sql
DBCC TRACEOFF (3205);
GO
```

The following example first disables trace flag 3205 globally.

```sql
DBCC TRACEOFF (3205, -1);
GO
```

The following example disables Trace flags 3205 and 260 globally.

```sql
DBCC TRACEOFF (3205, 260, -1);
GO
```

## Related content

- [DBCC (Transact-SQL)](dbcc-transact-sql.md)
- [DBCC TRACEON (Transact-SQL)](dbcc-traceon-transact-sql.md)
- [DBCC TRACESTATUS (Transact-SQL)](dbcc-tracestatus-transact-sql.md)
- [Set trace flags with DBCC TRACEON (Transact-SQL)](dbcc-traceon-trace-flags-transact-sql.md)
