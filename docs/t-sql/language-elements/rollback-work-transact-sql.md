---
title: ROLLBACK WORK (Transact-SQL)
description: "ROLLBACK WORK (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: dfurman, randolphwest
ms.date: 12/17/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "ROLLBACK WORK"
  - "ROLLBACK_WORK_TSQL"
helpviewer_keywords:
  - "transaction rollbacks [SQL Server]"
  - "erasing data modifications [SQL Server]"
  - "ROLLBACK WORK statement"
  - "roll back transactions [SQL Server]"
  - "rolling back transactions, ROLLBACK WORK"
  - "savepoints [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# ROLLBACK WORK (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Rolls back a user-specified transaction to the beginning of the transaction.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ROLLBACK [ WORK ]
[ ; ]
```

## Remarks

This statement functions identically to `ROLLBACK TRANSACTION` except that `ROLLBACK TRANSACTION` accepts a user-defined transaction name. With or without specifying the optional `WORK` keyword, this `ROLLBACK` syntax is ISO-compatible.

When there are inner transactions, `ROLLBACK WORK` always rolls back to the outermost `BEGIN TRANSACTION` statement and decrements the `@@TRANCOUNT` system function to 0.

## Permissions

Requires membership in the `public` role.

## Related content

- [BEGIN DISTRIBUTED TRANSACTION (Transact-SQL)](begin-distributed-transaction-transact-sql.md)
- [BEGIN TRANSACTION (Transact-SQL)](begin-transaction-transact-sql.md)
- [COMMIT TRANSACTION (Transact-SQL)](commit-transaction-transact-sql.md)
- [COMMIT WORK (Transact-SQL)](commit-work-transact-sql.md)
- [ROLLBACK TRANSACTION (Transact-SQL)](rollback-transaction-transact-sql.md)
- [SAVE TRANSACTION (Transact-SQL)](save-transaction-transact-sql.md)
- [Transaction locking and row versioning guide](../../relational-databases/sql-server-transaction-locking-and-row-versioning-guide.md)
