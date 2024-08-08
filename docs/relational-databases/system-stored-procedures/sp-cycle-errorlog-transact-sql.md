---
title: "sp_cycle_errorlog (Transact-SQL)"
description: sp_cycle_errorlog closes the current error log file and cycles the error log extension numbers just like a server restart.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cycle_errorlog_TSQL"
  - "sp_cycle_errorlog"
helpviewer_keywords:
  - "sp_cycle_errorlog"
dev_langs:
  - "TSQL"
---
# sp_cycle_errorlog (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Closes the current error log file and cycles the error log extension numbers just like a server restart. The new error log contains version and copyright information and a line indicating that the new log was created.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_cycle_errorlog
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Every time [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is started, the current error log is renamed to `errorlog.1`; `errorlog.1` becomes `errorlog.2`, `errorlog.2` becomes `errorlog.3`, and so on. `sp_cycle_errorlog` enables you to cycle the error log files without stopping and starting the server.

## Permissions

Execute permissions for `sp_cycle_errorlog` are restricted to members of the **sysadmin** fixed server role.

## Examples

The following example cycles the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log.

```sql
EXEC sp_cycle_errorlog;
GO
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sp_cycle_agent_errorlog (Transact-SQL)](sp-cycle-agent-errorlog-transact-sql.md)
