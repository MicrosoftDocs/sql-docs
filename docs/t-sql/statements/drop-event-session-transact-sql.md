---
title: "DROP EVENT SESSION (Transact-SQL)"
description: DROP EVENT SESSION drops an Extended Event session.
author: uc-msft
ms.author: umajay
ms.reviewer: dfurman
ms.date: 09/02/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_EVENT_SESSION_TSQL"
  - "DROP EVENT SESSION"
helpviewer_keywords:
  - "event sessions [SQL Server]"
  - "DROP EVENT SESSION statement"
dev_langs:
  - "TSQL"
---

# DROP EVENT SESSION (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]

Drops an event session.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DROP EVENT SESSION event_session_name
ON { SERVER | DATABASE }
```

## Arguments

#### *event_session_name*

The name of an existing event session.

## Remarks

When you drop an event session, all configuration information, such as targets and session parameters, is completely removed.

## Permissions

SQL Server and Azure SQL Managed Instance require the `DROP ANY EVENT SESSION` (introduced in SQL Server 2022), or `ALTER ANY EVENT SESSION` permission.

Azure SQL Database requires the `DROP ANY DATABASE EVENT SESSION` permission in the database.

> [!TIP]  
> SQL Server 2022 introduced more granular permissions for Extended Events. For more information, see [Blog: New granular permissions for SQL Server 2022 and Azure SQL to improve adherence with PoLP](https://techcommunity.microsoft.com/blog/sqlserver/new-granular-permissions-for-sql-server-2022-and-azure-sql-to-improve-adherence-/3607507).

## Examples

The following example shows how to drop an event session. To use this example with database event sessions, replace `ON SERVER` with `ON DATABASE`.

```sql
DROP EVENT SESSION test_session ON SERVER;
```

## Related content

- [CREATE EVENT SESSION (Transact-SQL)](create-event-session-transact-sql.md)
- [ALTER EVENT SESSION (Transact-SQL)](alter-event-session-transact-sql.md)
- [sys.server_event_sessions (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-event-sessions-transact-sql.md)
