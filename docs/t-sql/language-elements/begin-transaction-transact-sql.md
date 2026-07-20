---
title: "BEGIN TRANSACTION (Transact-SQL)"
description: Marks the starting point of an explicit, local transaction. Explicit transactions start with the BEGIN TRANSACTION statement and end with the COMMIT or ROLLBACK statement.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 12/17/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "BEGIN_TRANSACTION_TSQL"
  - "TRANSACTION_TSQL"
  - "TRANSACTION"
  - "BEGIN TRANSACTION"
  - "BEGIN TRAN"
  - "BEGIN_TRAN_TSQL"
helpviewer_keywords:
  - "transaction logs [SQL Server], BEGIN TRANSACTION statement"
  - "marked transactions [SQL Server], BEGIN TRANSACTION statement"
  - "BEGIN TRANSACTION statement"
  - "local transactions [SQL Server]"
  - "marking transactions [SQL Server]"
  - "transactions [SQL Server], starting"
  - "transaction names [SQL Server]"
  - "starting point marked for transactions"
  - "starting transactions"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# BEGIN TRANSACTION (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb.md)]

Marks the starting point of an explicit, local transaction. Explicit transactions start with the `BEGIN TRANSACTION` statement and end with the `COMMIT` or `ROLLBACK` statement.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)].

```syntaxsql
BEGIN { TRAN | TRANSACTION }
    [ { transaction_name | @tran_name_variable }
      [ WITH MARK [ 'description' ] ]
    ]
[ ; ]
```

Syntax for Fabric Data Warehouse, Azure Synapse Analytics, and Analytics Platform System (PDW).

```syntaxsql
BEGIN { TRAN | TRANSACTION }
[ ; ]
```

## Arguments

#### *transaction_name*

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)].

The name assigned to the transaction. *transaction_name* must conform to the rules for identifiers, but identifiers longer than 32 characters aren't allowed. Use transaction names only on the outermost pair of `BEGIN...COMMIT` or `BEGIN...ROLLBACK` statements. *transaction_name* is always case sensitive, even when the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] instance isn't case sensitive.

#### *@tran_name_variable*

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)].

The name of a user-defined variable containing a valid transaction name. The variable must be declared with a **char**, **varchar**, **nchar**, or **nvarchar** data type. If more than 32 characters are passed to the variable, only the first 32 characters are used. The remaining characters are truncated.

#### WITH MARK [ '*description*' ]

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)].

Specifies that the transaction is marked in the transaction log. *description* is a string that describes the mark. A *description* longer than 128 characters is truncated to 128 characters before being stored in the `msdb.dbo.logmarkhistory` table.

If `WITH MARK` is used, a transaction name must be specified. `WITH MARK` allows for restoring a transaction log to a point identified by the mark.

## Remarks

`BEGIN TRANSACTION` increments `@@TRANCOUNT` by `1`.

`BEGIN TRANSACTION` represents a point at which the data referenced by a session has a certain state of consistency. All data modifications made after the `BEGIN TRANSACTION` can be rolled back to return the data to this known state of consistency. Each transaction lasts until `COMMIT TRANSACTION` is issued to make the modifications a permanent part of the database, or all modifications are erased with a `ROLLBACK TRANSACTION` statement.

A transaction can be automatically rolled back if a transaction-aborting error occurs, or if any run-time error occurs and the `XACT_ABORT` session option is set to `ON`. For more information, see [SET XACT_ABORT](../statements/set-xact-abort-transact-sql.md).

`BEGIN TRANSACTION` starts a local transaction for the session issuing the statement. Depending on the current transaction isolation level settings, resources acquired to support the [!INCLUDE [tsql](../../includes/tsql-md.md)] statements issued by the session are locked by the transaction until it completes with either a `COMMIT TRANSACTION` or `ROLLBACK TRANSACTION` statement. Transactions left outstanding for long periods of time can prevent other sessions from accessing these locked resources, and can also prevent transaction log truncation and version store cleanup.

Although `BEGIN TRANSACTION` starts a local transaction, it isn't recorded in the transaction log until the application then performs an action that must be recorded in the log, such as executing an `INSERT`, `UPDATE`, or `DELETE` statement. Once a transaction is started, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] can perform actions such as acquiring locks to protect the transaction isolation level of `SELECT` statements, but nothing is recorded in the transaction log until the application performs a modification action.

After issuing `BEGIN TRANSACTION`, you can issue `BEGIN TRANSACTION` again to start one or more inner transactions. Even though you can specify *transaction_name* for an inner transaction, only the first (outermost) transaction name is registered with the system. A rollback to any other name (other than a valid savepoint name) generates an error without rolling back any of the statements. The statements are rolled back only when the outer transaction is rolled back.

The local transaction started by the `BEGIN TRANSACTION` statement is promoted to a distributed transaction if the following actions are performed before the statement is committed or rolled back:

- An `INSERT`, `DELETE`, or `UPDATE` statement that references a remote table on a linked server is executed. The `INSERT`, `UPDATE`, or `DELETE` statement fails if the OLE DB provider used to access the linked server doesn't support the `ITransactionJoin` interface.

- A call is made to a remote stored procedure when the `REMOTE_PROC_TRANSACTIONS` option is set to `ON`.

The local [!INCLUDE [ssde-md](../../includes/ssde-md.md)] instance becomes the transaction controller and uses [!INCLUDE [msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) to manage the distributed transaction.

A transaction can be explicitly executed as a distributed transaction by using `BEGIN DISTRIBUTED TRANSACTION`. For more information, see [BEGIN DISTRIBUTED TRANSACTION](begin-distributed-transaction-transact-sql.md).

When `SET IMPLICIT_TRANSACTIONS` is set to `ON`, a `BEGIN TRANSACTION` statement creates an outer and an inner transaction, setting `@@TRANCOUNT` to 2. For more information, see [SET IMPLICIT_TRANSACTIONS](../statements/set-implicit-transactions-transact-sql.md).

> [!NOTE]  
> The [!INCLUDE [ssde-md](../../includes/ssde-md.md)] doesn't support independently manageable nested transactions. A commit of an inner transaction decrements `@@TRANCOUNT` but has no other effects. A rollback of an inner transaction always rolls back the outer transaction, unless a [savepoint](save-transaction-transact-sql.md) exists and is specified in the `ROLLBACK` statement.

## Marked transactions

The `WITH MARK` option causes the transaction name to be recorded in the transaction log. When you restore a database to an earlier state, the marked transaction can be used to specify the restore point instead of a date and time. For more information, see [Use Marked Transactions to Recover Related Databases Consistently](../../relational-databases/backup-restore/use-marked-transactions-to-recover-related-databases-consistently.md) and [RESTORE Statements](../statements/restore-statements-transact-sql.md).

Additionally, transaction log marks are necessary if you need to recover a set of related databases to a certain shared state of consistency. An application that is aware of the consistency state of every database can place marks in the transaction logs of the related databases using a cross-database or a distributed transaction. Recovering the set of related databases to these marks results in a set of databases that have a known shared state of consistency.

The mark is placed in the transaction log only if the database is updated by the marked transaction. Transactions that don't modify data aren't recorded in the log.

`BEGIN TRANSACTION <new_name> WITH MARK` can be used when starting an inner transaction. In that case, `<new_name>` becomes the mark name for the transaction if the outer transaction isn't marked. In the following conceptual example, `M2` is the name of the mark.

```sql
BEGIN TRAN T1;

UPDATE table1 ...;

BEGIN TRAN M2 WITH MARK;

UPDATE table2 ...;

SELECT column1 FROM table1;

COMMIT TRAN M2;

UPDATE table3 ...;

COMMIT TRAN T1;
```

When you mark an inner transaction, you receive the following warning message if you try to mark a transaction that is already marked:

```output
Server: Msg 3920, Level 16, State 1, Line 3
WITH MARK option only applies to the first BEGIN TRAN WITH MARK.
The option is ignored.
```

## Permissions

Requires membership in the `public` role.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use an explicit transaction

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], Analytics Platform System (PDW)

```sql
BEGIN TRANSACTION;

DELETE FROM HumanResources.JobCandidate
WHERE JobCandidateID = 13;

COMMIT TRANSACTION;
```

### B. Roll back a transaction

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], Analytics Platform System (PDW)

The following example shows the effect of rolling back a transaction. In this example, the `ROLLBACK` statement rolls back the `INSERT` statement, but the created table still exists.

```sql
CREATE TABLE ValueTable
(
    id INT
);

BEGIN TRANSACTION;

INSERT INTO ValueTable VALUES (1);
INSERT INTO ValueTable VALUES (2);

ROLLBACK;
```

### C. Name a transaction

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]

The following example shows how to name a transaction.

```sql
DECLARE @TranName AS VARCHAR (20);
SELECT @TranName = 'MyTransaction';

BEGIN TRANSACTION @TranName;

DELETE FROM HumanResources.JobCandidate
WHERE JobCandidateID = 13;

COMMIT TRANSACTION @TranName;
```

### D. Mark a transaction

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]

The following example shows how to mark a transaction. The transaction `CandidateDelete` is marked.

```sql
BEGIN TRANSACTION CandidateDelete
    WITH MARK N'Deleting a Job Candidate';

DELETE FROM HumanResources.JobCandidate
WHERE JobCandidateID = 13;

COMMIT TRANSACTION CandidateDelete;
```

## Related content

- [BEGIN DISTRIBUTED TRANSACTION (Transact-SQL)](begin-distributed-transaction-transact-sql.md)
- [COMMIT TRANSACTION (Transact-SQL)](commit-transaction-transact-sql.md)
- [COMMIT WORK (Transact-SQL)](commit-work-transact-sql.md)
- [ROLLBACK TRANSACTION (Transact-SQL)](rollback-transaction-transact-sql.md)
- [ROLLBACK WORK (Transact-SQL)](rollback-work-transact-sql.md)
- [SAVE TRANSACTION (Transact-SQL)](save-transaction-transact-sql.md)
- [Transaction locking and row versioning guide](../../relational-databases/sql-server-transaction-locking-and-row-versioning-guide.md)
