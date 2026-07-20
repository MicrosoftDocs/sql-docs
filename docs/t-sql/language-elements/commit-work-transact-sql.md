---
title: COMMIT WORK (Transact-SQL)
description: "COMMIT WORK (Transact-SQL)"
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
  - "COMMIT_WORK_TSQL"
  - "WORK_TSQL"
  - "WORK"
  - "COMMIT WORK"
helpviewer_keywords:
  - "ending transactions [SQL Server]"
  - "transactions [SQL Server], ending"
  - "marking end of transactions [SQL Server]"
  - "COMMIT WORK statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# COMMIT WORK (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Marks the end of a transaction.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
COMMIT [ WORK ]
[ ; ]
```

## Remarks

This statement functions identically to `COMMIT TRANSACTION`, except `COMMIT TRANSACTION` accepts a user-defined transaction name. This `COMMIT` syntax, with or without specifying the optional keyword `WORK`, is compatible with SQL-92.

## Permissions

Requires membership in the `public` role.

## Related content

- [BEGIN DISTRIBUTED TRANSACTION (Transact-SQL)](begin-distributed-transaction-transact-sql.md)
- [BEGIN TRANSACTION (Transact-SQL)](begin-transaction-transact-sql.md)
- [COMMIT TRANSACTION (Transact-SQL)](commit-transaction-transact-sql.md)
- [ROLLBACK TRANSACTION (Transact-SQL)](rollback-transaction-transact-sql.md)
- [ROLLBACK WORK (Transact-SQL)](rollback-work-transact-sql.md)
- [SAVE TRANSACTION (Transact-SQL)](save-transaction-transact-sql.md)
- [@@TRANCOUNT (Transact-SQL)](../functions/trancount-transact-sql.md)
- [Transaction locking and row versioning guide](../../relational-databases/sql-server-transaction-locking-and-row-versioning-guide.md)
