---
title: "sp_cycle_agent_errorlog (Transact-SQL)"
description: Closes the current SQL Server Agent error log file, and cycles the SQL Server Agent error log extension numbers.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cycle_agent_errorlog"
  - "sp_cycle_agent_errorlog_TSQL"
helpviewer_keywords:
  - "sp_cycle_agent_errorlog"
dev_langs:
  - "TSQL"
---
# sp_cycle_agent_errorlog (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Closes the current [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent error log file and cycles the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent error log extension numbers just like a server restart. The new [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent error log contains a line indicating that the new log was created.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_cycle_agent_errorlog
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Every time [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent is started, the current [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent error log is renamed to `SQLAgent.1`; `SQLAgent.1` becomes `SQLAgent.2`, `SQLAgent.2` becomes `SQLAgent.3`, and so on. `sp_cycle_agent_errorlog` enables you to cycle the error log files without stopping and starting the server.

This stored procedure must be run from the `msdb` database.

## Permissions

Execute permissions for `sp_cycle_agent_errorlog` are restricted to members of the **sysadmin** fixed server role.

## Examples

The following example cycles the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent error log.

```sql
USE msdb;
GO

EXEC dbo.sp_cycle_agent_errorlog;
GO
```

## Related content

- [sp_cycle_errorlog (Transact-SQL)](sp-cycle-errorlog-transact-sql.md)
