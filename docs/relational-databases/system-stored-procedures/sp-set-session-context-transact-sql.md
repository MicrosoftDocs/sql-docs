---
title: "sp_set_session_context (Transact-SQL)"
description: sp_set_session_context sets a key-value pair in the session context.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_set_session_context"
  - "sp_set_session_context_TSQL"
  - "sys.sp_set_session_context"
  - "sys.sp_set_session_context_TSQL"
helpviewer_keywords:
  - "sp_set_session_context"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# sp_set_session_context (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw.md)]

Sets a key-value pair in the session context.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_set_session_context
    [ @key = ] N'key'
    , [ @value = ] 'value'
    [ , [ @read_only = ] read_only ]
[ ; ]
```

## Arguments

#### [ @key = ] N'*key*'

The key being set. *@key* is **sysname** with no default. The maximum key size is 128 bytes.

#### [ @value = ] '*value*'

The value for the specified key. *@value* is **sql_variant**, with a default of `NULL`. Setting a value of `NULL` frees the memory. The maximum size is 8,000 bytes.

#### [ @read_only = ] *read_only*

A flag indicating whether the specified key can be changed on the logical connection. *@read_only* is **bit** with a default of `0`.

- If `1`, the value for the specified key can't be changed again on this logical connection.
- If `0`, the value can be changed.

## Permissions

Any user can set a session context for their session.

## Remarks

Like other stored procedures, only literals and variables (not expressions or function calls) can be passed as parameters.

The total size of the session context is limited to 1 MB. If you set a value that causes this limit to be exceeded, the statement fails. You can monitor overall memory usage in [sys.dm_os_memory_objects (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-memory-objects-transact-sql.md).

You can monitor overall memory usage by querying [sys.dm_os_memory_cache_counters (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-memory-cache-counters-transact-sql.md) as follows:

```sql
SELECT * FROM sys.dm_os_memory_cache_counters WHERE type = 'CACHESTORE_SESSION_CONTEXT';
```

## Examples

### A. Set and return a session context

The following example shows how to set and then return a session's context key named `language`, with a value of `English`.

```sql
EXEC sys.sp_set_session_context @key = N'language', @value = 'English';
SELECT SESSION_CONTEXT(N'language');
```

The following example demonstrates the use of the optional read-only flag.

```sql
EXEC sys.sp_set_session_context @key = N'user_id', @value = 4, @read_only = 1;
```

### B. Set and return a client correlation ID

The following example shows how to set and retrieve a session context key named `client_correlation_id`, with a value of `12323ad`.

1. Set the value.

   ```sql
   EXEC sp_set_session_context 'client_correlation_id', '12323ad';
   ```

2. Retrieve the value.

   ```sql
   SELECT SESSION_CONTEXT(N'client_correlation_id');
   ```

## Related content

- [CURRENT_TRANSACTION_ID (Transact-SQL)](../../t-sql/functions/current-transaction-id-transact-sql.md)
- [SESSION_CONTEXT (Transact-SQL)](../../t-sql/functions/session-context-transact-sql.md)
- [Row-level security](../security/row-level-security.md)
- [CONTEXT_INFO (Transact-SQL)](../../t-sql/functions/context-info-transact-sql.md)
- [SET CONTEXT_INFO (Transact-SQL)](../../t-sql/statements/set-context-info-transact-sql.md)
