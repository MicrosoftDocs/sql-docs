---
title: "ROLLBACK TRANSACTION (Transact-SQL)"
description: This statement rolls back an explicit or implicit transaction to the beginning of the transaction, or to a savepoint inside the transaction.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/15/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ROLLBACK TRANSACTION"
  - "ROLLBACK"
  - "ROLLBACK_TSQL"
  - "ROLLBACK_TRANSACTION_TSQL"
helpviewer_keywords:
  - "transaction rollbacks [SQL Server]"
  - "ROLLBACK TRANSACTION statement"
  - "erasing data modifications [SQL Server]"
  - "rolling back transactions, ROLLBACK TRANSACTION"
  - "roll back transactions [SQL Server]"
  - "savepoints [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# ROLLBACK TRANSACTION (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw.md)]

This statement rolls back an explicit or implicit transaction to the beginning of the transaction, or to a savepoint inside the transaction. You can use `ROLLBACK TRANSACTION` to erase all data modifications made from the start of the transaction or to a savepoint. It also frees resources held by the transaction.

Rolling back a transaction doesn't include changes made to local variables or table variables. These changes aren't erased by this statement.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server and Azure SQL Database.

```syntaxsql
ROLLBACK { TRAN | TRANSACTION }
    [ transaction_name | @tran_name_variable
    | savepoint_name | @savepoint_variable ]
[ ; ]
```

Syntax for Synapse Data Warehouse in Microsoft Fabric, Azure Synapse Analytics, and Parallel Data Warehouse Database.

```syntaxsql
ROLLBACK { TRAN | TRANSACTION }
[ ; ]
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *transaction_name*

The name assigned to the transaction on `BEGIN TRANSACTION`. *transaction_name* must conform to the rules for identifiers, but only the first 32 characters of the transaction name are used. When you nest transactions, *transaction_name* must be the name from the outermost `BEGIN TRANSACTION` statement. *transaction_name* is always case-sensitive, even when the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] isn't case-sensitive.

#### *@tran_name_variable*

The name of a user-defined variable containing a valid transaction name. The variable must be declared with a **char**, **varchar**, **nchar**, or **nvarchar** data type.

#### *savepoint_name*

*savepoint_name* from a `SAVE TRANSACTION` statement. *savepoint_name* must conform to the rules for identifiers. Use *savepoint_name* when a conditional rollback should affect only part of the transaction.

#### *@savepoint_variable*

The name of a user-defined variable containing a valid savepoint name. The variable must be declared with a **char**, **varchar**, **nchar**, or **nvarchar** data type.

## Error handling

A `ROLLBACK TRANSACTION` statement doesn't produce any messages to the user. If warnings are needed in stored procedures or triggers, use the `RAISERROR` or `PRINT` statements. `RAISERROR` is the preferred statement for indicating errors.

## Remarks

`ROLLBACK TRANSACTION` without a *savepoint_name* or *transaction_name* rolls back to the beginning of the transaction. When you nest transactions, this same statement rolls back all inner transactions to the outermost `BEGIN TRANSACTION` statement. In both cases, `ROLLBACK TRANSACTION` decrements the `@@TRANCOUNT` system function to 0. `ROLLBACK TRANSACTION <savepoint_name>` doesn't decrement `@@TRANCOUNT`.

`ROLLBACK TRANSACTION` can't reference a *savepoint_name* in distributed transactions started either explicitly with `BEGIN DISTRIBUTED TRANSACTION` or escalated from a local transaction.

A transaction can't be rolled back after a `COMMIT TRANSACTION` statement is executed, except when the `COMMIT TRANSACTION` is associated with a nested transaction that is contained within the transaction being rolled back. In this instance, the nested transaction is rolled back, even if you issued a `COMMIT TRANSACTION` for it.

Within a transaction, duplicate savepoint names are allowed, but a `ROLLBACK TRANSACTION` using the duplicate savepoint name rolls back only to the most recent `SAVE TRANSACTION` using that savepoint name.

## Interoperability

In stored procedures, `ROLLBACK TRANSACTION` statements without a *savepoint_name* or *transaction_name* roll back all statements to the outermost `BEGIN TRANSACTION`. A `ROLLBACK TRANSACTION` statement in a stored procedure that causes `@@TRANCOUNT` to have a different value when the stored procedure completes than the `@@TRANCOUNT` value when the stored procedure was called produces an informational message. This message doesn't affect subsequent processing.

If a `ROLLBACK TRANSACTION` is issued in a trigger:

- All data modifications made to that point in the current transaction are rolled back, including any made by the trigger.

- The trigger continues executing any remaining statements after the `ROLLBACK` statement. If any of these statements modify data, the modifications aren't rolled back. No nested triggers are fired by the execution of these remaining statements.

- The statements in the batch after the statement that fired the trigger aren't executed.

`@@TRANCOUNT` is incremented by one when entering a trigger, even when in autocommit mode. (The system treats a trigger as an implied nested transaction.)

`ROLLBACK TRANSACTION` statements in stored procedures don't affect subsequent statements in the batch that called the procedure; subsequent statements in the batch are executed. `ROLLBACK TRANSACTION` statements in triggers terminate the batch containing the statement that fired the trigger; subsequent statements in the batch aren't executed.

The effect of a `ROLLBACK` on cursors is defined by these three rules:

- With `CURSOR_CLOSE_ON_COMMIT` set `ON`, `ROLLBACK` closes, but doesn't deallocate all open cursors.

- With `CURSOR_CLOSE_ON_COMMIT` set `OFF`, `ROLLBACK` doesn't affect any open synchronous `STATIC` or `INSENSITIVE` cursors or asynchronous `STATIC` cursors that were fully populated. Open cursors of any other type are closed but not deallocated.

- An error that terminates a batch and generates an internal rollback deallocates all cursors that were declared in the batch containing the error statement. All cursors are deallocated regardless of their type or the setting of `CURSOR_CLOSE_ON_COMMIT`. This includes cursors declared in stored procedures called by the error batch. Cursors declared in a batch before the error batch are subject to the first two rules. A deadlock error is an example of this type of error. A `ROLLBACK` statement issued in a trigger also automatically generates this type of error.

## Locking behavior

A `ROLLBACK TRANSACTION` statement specifying a *savepoint_name* releases any locks that are acquired beyond the savepoint, except for escalations and conversions. These locks aren't released, and they aren't converted back to their previous lock mode.

## Permissions

Requires membership in the **public** role.

## Examples

The following example shows the effect of rolling back a named transaction. After you create a table, the following statements start a named transaction, insert two rows, and then roll back the transaction named in the variable `@TransactionName`. Another statement outside of the named transaction inserts two rows. The query returns the results of the previous statements.

```sql
USE tempdb;
GO

CREATE TABLE ValueTable ([value] INT);
GO

DECLARE @TransactionName VARCHAR(20) = 'Transaction1';

BEGIN TRANSACTION @TransactionName

INSERT INTO ValueTable
VALUES (1), (2);

ROLLBACK TRANSACTION @TransactionName;

INSERT INTO ValueTable
VALUES (3), (4);

SELECT [value]
FROM ValueTable;

DROP TABLE ValueTable;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
value
-----
3
4
```

## Related content

- [BEGIN DISTRIBUTED TRANSACTION (Transact-SQL)](begin-distributed-transaction-transact-sql.md)
- [BEGIN TRANSACTION (Transact-SQL)](begin-transaction-transact-sql.md)
- [COMMIT TRANSACTION (Transact-SQL)](commit-transaction-transact-sql.md)
- [COMMIT WORK (Transact-SQL)](commit-work-transact-sql.md)
- [ROLLBACK WORK (Transact-SQL)](rollback-work-transact-sql.md)
- [SAVE TRANSACTION (Transact-SQL)](save-transaction-transact-sql.md)
