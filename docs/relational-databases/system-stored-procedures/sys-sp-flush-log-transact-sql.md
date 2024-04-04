---
title: "sys.sp_flush_log (Transact-SQL)"
description: Flushes to disk the transaction log of the current database, hardening all previously committed delayed durable transactions.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/06/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_flush_log_TSQL"
  - "sys.sp_flush_log"
  - "sys.sp_flush_log_TSQL"
  - "sp_flush_log"
helpviewer_keywords:
  - "sys.sp_flush_log"
dev_langs:
  - "TSQL"
---
# sys.sp_flush_log (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Flushes to disk the transaction log of the current database, thereby hardening all previously committed delayed durable transactions.

If you choose to use delayed transaction durability because of the performance benefits, but you also want to have a guaranteed limit on the amount of data that is lost on server crash or failover, then execute `sys.sp_flush_log` on a regular schedule. For example, if you want to make sure you don't lose more than *n* seconds worth of data, you would execute `sp_flush_log` every *n* seconds.

Executing `sys.sp_flush_log` guarantees that all previously committed delayed durable transactions are made durable. For more information, see [Control Transaction Durability](../logs/control-transaction-durability.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_flush_log
[ ; ]
```

## Arguments

None.

## Return code values

A return code of `1` indicates success. Any other value indicates failure.

## Result set

None.

## Sample code

```sql
EXEC sys.sp_flush_log;
```

## Related content

- [SQL Server transaction log architecture and management guide](../sql-server-transaction-log-architecture-and-management-guide.md)
