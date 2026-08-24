---
title: "Transactions (Transact-SQL)"
description: In the SQL Server Database Engine, a transaction is a single unit of work.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/30/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "Transactions"
  - "Transactions_TSQL"
helpviewer_keywords:
  - "transactions [SQL Server]"
  - "transactions [SQL Server], about transactions"
  - "UOW [SQL Server]"
  - "unit of work [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Transactions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricdw-fabricsqldb.md)]

A transaction is a single unit of work. If a transaction is successful, all of the data modifications made during the transaction are committed and become a permanent part of the database. If a transaction encounters errors and must be canceled or rolled back, then all of the data modifications are erased.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] operates in the following transaction modes:

| Transaction mode | Description |
| --- | --- |
| **Autocommit transactions** | Each individual statement is a transaction. |
| **Explicit transactions** | Each transaction is explicitly started with the `BEGIN TRANSACTION` statement and explicitly ended with a `COMMIT` or `ROLLBACK` statement. |
| **Implicit transactions** | A new transaction is implicitly started when the prior transaction completes, but each transaction is explicitly completed with a `COMMIT` or `ROLLBACK` statement. |
| **Batch-scoped transactions** | Applicable only to multiple active result sets (MARS), a [!INCLUDE [tsql](../../includes/tsql-md.md)] explicit or implicit transaction that starts under a MARS session becomes a batch-scoped transaction. A batch-scoped transaction that isn't committed or rolled back when a batch completes is automatically rolled back by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |

For special considerations related to data warehouse products, see  [Transactions in Fabric Data Warehouse](/fabric/data-warehouse/transactions) or [Transactions (Azure Synapse Analytics)](transactions-sql-data-warehouse.md).

<a id="in-this-section"></a>

## Transaction control syntax

The SQL Database Engine provides the following transaction statements:

- [BEGIN DISTRIBUTED TRANSACTION](begin-distributed-transaction-transact-sql.md)
- [ROLLBACK TRANSACTION](rollback-transaction-transact-sql.md)
- [BEGIN TRANSACTION](begin-transaction-transact-sql.md)
- [ROLLBACK WORK](rollback-work-transact-sql.md)
- [COMMIT TRANSACTION](commit-transaction-transact-sql.md)
- [SAVE TRANSACTION](save-transaction-transact-sql.md)
- [COMMIT WORK](commit-work-transact-sql.md)

## Related content

- [SET IMPLICIT_TRANSACTIONS (Transact-SQL)](../statements/set-implicit-transactions-transact-sql.md)
- [@@TRANCOUNT (Transact-SQL)](../functions/trancount-transact-sql.md)
