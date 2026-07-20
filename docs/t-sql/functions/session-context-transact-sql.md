---
title: "SESSION_CONTEXT (Transact-SQL)"
description: SESSION_CONTEXT returns the value of the specified key in the current session context.
author: VanMSFT
ms.author: vanto
ms.reviewer: derekw, randolphwest
ms.date: 07/07/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SESSION_CONTEXT"
  - "sys.SESSION_CONTEXT"
  - "SESSION_CONTEXT_TSQL"
  - "sys.SESSION_CONTEXT_TSQL"
helpviewer_keywords:
  - "SESSION_CONTEXT function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqledge-current || =azure-sqldw-latest || =fabric || =fabric-sqldb"
---
# SESSION_CONTEXT (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb.md)]

Returns the value of the specified key in the current session context. The value is set by using the [sp_set_session_context](../../relational-databases/system-stored-procedures/sp-set-session-context-transact-sql.md) procedure.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SESSION_CONTEXT(N'key')
```

## Arguments

#### '*key*'

The key of the value being retrieved. *key* is **sysname**.

## Return types

**sql_variant**

## Return value

The value associated with the specified key in the session context, or `NULL` if no value is set for that key.

## Permissions

Any user can read the session context for their session.

## Remarks

The MARS behavior for `SESSION_CONTEXT`' is similar to that of `CONTEXT_INFO`. If a MARS batch sets a key-value pair, the new value isn't returned in other MARS batches on the same connection unless they started after the batch that set the new value completed. If multiple MARS batches are active on a connection, values can't be set as `read_only`. This prevents race conditions and nondeterminism about which value *wins*.

## Examples

The following simple example sets the session context value for key `user_id` to 4, and then uses the `SESSION_CONTEXT` function to retrieve the value.

```sql
EXECUTE sp_set_session_context 'user_id', 4;

SELECT SESSION_CONTEXT(N'user_id');
```

## Known issues

| Issue | Date discovered | Status | Date resolved |
| --- | --- | --- | --- |
| An Access Violation (AV) exception might occur with the `SESSION_CONTEXT` function under certain conditions. You might encounter AV exceptions or wrong results when the `SESSION_CONTEXT` function runs within a parallel execution plan when the session is reset for reuse.<br /><br />A fix, which was introduced in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 14 to address a wrong results issue with `SESSION_CONTEXT` within parallel plans, was later found to cause AV exceptions under certain conditions.<br /><br />To mitigate this issue, you can enable trace flag 11042 as a startup, global, or session trace flag. This trace flag forces `SESSION_CONTEXT` to execute serially, preventing it from participating in parallel query plans.<br /><br />**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 14 and later versions. | January 2022 | Has workaround | |

## Related content

- [sp_set_session_context](../../relational-databases/system-stored-procedures/sp-set-session-context-transact-sql.md)
- [sp_set_session_context (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-set-session-context-transact-sql.md)
- [CURRENT_TRANSACTION_ID (Transact-SQL)](current-transaction-id-transact-sql.md)
- [Row-level security](../../relational-databases/security/row-level-security.md)
- [CONTEXT_INFO (Transact-SQL)](context-info-transact-sql.md)
- [SET CONTEXT_INFO (Transact-SQL)](../statements/set-context-info-transact-sql.md)

