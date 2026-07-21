---
title: "SET IMPLICIT_TRANSACTIONS (Transact-SQL)"
description: SET IMPLICIT_TRANSACTIONS sets the BEGIN TRANSACTION mode to implicit, for the connection.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 11/26/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "IMPLICIT_TRANSACTIONS"
  - "SET IMPLICIT_TRANSACTIONS"
  - "IMPLICIT_TRANSACTIONS_TSQL"
  - "SET_IMPLICIT_TRANSACTIONS_TSQL"
helpviewer_keywords:
  - "implicit transactions"
  - "transactions [SQL Server], implicit"
  - "connections [SQL Server], implicit transaction mode"
  - "SET IMPLICIT_TRANSACTIONS statement"
  - "IMPLICIT_TRANSACTIONS option"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SET IMPLICIT_TRANSACTIONS (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb.md)]

Sets the `BEGIN TRANSACTION` mode to *implicit*, for the connection.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SET IMPLICIT_TRANSACTIONS { ON | OFF }
```

## Remarks

When `ON`, the system is in *implicit* transaction mode. This means that if `@@TRANCOUNT = 0`, any of the following Transact-SQL statements begins a new transaction. It's equivalent to an unseen `BEGIN TRANSACTION` being executed first:

- `ALTER TABLE`
- `BEGIN TRANSACTION`
- `CREATE`
- `DELETE`
- `DROP`
- `FETCH`
- `GRANT`
- `INSERT`
- `MERGE`
- `OPEN`
- `REVOKE`
- `SELECT` (See [clarifying remark](#clarifying-remark))
- `TRUNCATE TABLE`
- `UPDATE`

When `OFF`, each of the preceding T-SQL statements is bounded by an unseen `BEGIN TRANSACTION` and an unseen `COMMIT TRANSACTION` statement. When `OFF`, we say the transaction mode is *autocommit*. If your T-SQL code visibly issues a `BEGIN TRANSACTION`, we say the transaction mode is *explicit*.

There are several clarifying points to understand:

- When the transaction mode is implicit, no unseen `BEGIN TRANSACTION` is issued if `@@TRANCOUNT > 0` already. However, any explicit `BEGIN TRANSACTION` statements still increment `@@TRANCOUNT`.

- When your `INSERT` statements and anything else in your unit of work is finished, you must issue `COMMIT TRANSACTION` statements until `@@TRANCOUNT` is decremented back down to 0. Or you can issue one `ROLLBACK TRANSACTION`.

<a id="clarifying-remark"></a>

- `SELECT` statements that don't select from a table don't start implicit transactions. For example, `SELECT GETDATE();` or `SELECT 1, 'ABC';` don't require transactions.

- Implicit transactions might unexpectedly be `ON` due to ANSI defaults. For details see [SET ANSI_DEFAULTS](set-ansi-defaults-transact-sql.md).

  Setting `IMPLICIT_TRANSACTIONS` to `ON` isn't popular. In most cases where `IMPLICIT_TRANSACTIONS` is `ON`, it's because `SET ANSI_DEFAULTS ON` was set.

- The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver, automatically set `IMPLICIT_TRANSACTIONS` to `OFF` when connecting. Set `IMPLICIT_TRANSACTIONS` defaults to `OFF` for connections with the SQLClient managed provider, and for SOAP requests received through HTTP endpoints.

To view the current setting for `IMPLICIT_TRANSACTIONS`, run the following query.

```sql
DECLARE @IMPLICIT_TRANSACTIONS AS VARCHAR (3) = 'OFF';
IF ((2 & @@OPTIONS) = 2)
SET @IMPLICIT_TRANSACTIONS = 'ON';
SELECT @IMPLICIT_TRANSACTIONS AS IMPLICIT_TRANSACTIONS;
```

## Examples

The following Transact-SQL script runs a few different test cases. The text output is also provided, which shows the detailed behavior and results from each test case.

```sql
-- Preparations.
SET NOCOUNT ON;
SET IMPLICIT_TRANSACTIONS OFF;
GO
WHILE (@@TRANCOUNT > 0) COMMIT TRANSACTION;
GO
IF (OBJECT_ID(N'dbo.t1',N'U') IS NOT NULL) DROP TABLE dbo.t1;
GO
CREATE table dbo.t1 (a INT);
GO

PRINT N'-------- [Test A] ---- OFF ----';
PRINT N'[A.01] Now, SET IMPLICIT_TRANSACTIONS OFF.';
PRINT N'[A.02] @@TRANCOUNT, at start, == ' + CAST(@@TRANCOUNT AS NVARCHAR(10));
SET IMPLICIT_TRANSACTIONS OFF;
GO
INSERT INTO dbo.t1 VALUES (11);
INSERT INTO dbo.t1 VALUES (12);
PRINT N'[A.03] @@TRANCOUNT, after INSERTs, == ' + CAST(@@TRANCOUNT AS NVARCHAR(10));
GO

PRINT N' ';
PRINT N'-------- [Test B] ---- ON ----';
PRINT N'[B.01] Now, SET IMPLICIT_TRANSACTIONS ON.';
PRINT N'[B.02] @@TRANCOUNT, at start, == ' + CAST(@@TRANCOUNT AS NVARCHAR(10));
SET IMPLICIT_TRANSACTIONS ON;
GO
INSERT INTO dbo.t1 VALUES (21);
INSERT INTO dbo.t1 VALUES (22);
PRINT N'[B.03] @@TRANCOUNT, after INSERTs, == ' + CAST(@@TRANCOUNT AS NVARCHAR(10));
GO
COMMIT TRANSACTION;
PRINT N'[B.04] @@TRANCOUNT, after COMMIT, == ' + CAST(@@TRANCOUNT AS NVARCHAR(10));
GO

PRINT N' ';
PRINT N'-------- [Test C] ---- ON, then BEGIN TRAN ----';
PRINT N'[C.01] Now, SET IMPLICIT_TRANSACTIONS ON.';
PRINT N'[C.02] @@TRANCOUNT, at start, == ' + CAST(@@TRANCOUNT AS NVARCHAR(10));
SET IMPLICIT_TRANSACTIONS ON;
GO
BEGIN TRANSACTION;
INSERT INTO dbo.t1 VALUES (31);
INSERT INTO dbo.t1 VALUES (32);
PRINT N'[C.03] @@TRANCOUNT, after INSERTs, == ' + CAST(@@TRANCOUNT AS NVARCHAR(10));
GO
COMMIT TRANSACTION;
PRINT N'[C.04] @@TRANCOUNT, after a COMMIT, == ' + CAST(@@TRANCOUNT AS NVARCHAR(10));
COMMIT TRANSACTION;
PRINT N'[C.05] @@TRANCOUNT, after another COMMIT, == ' + CAST(@@TRANCOUNT AS NVARCHAR(10));
GO

PRINT N' ';
PRINT N'-------- [Test D] ---- ON, INSERT, BEGIN TRAN, INSERT ----';
PRINT N'[D.01] Now, SET IMPLICIT_TRANSACTIONS ON.';
PRINT N'[D.02] @@TRANCOUNT, at start, == ' + CAST(@@TRANCOUNT AS NVARCHAR(10));
SET IMPLICIT_TRANSACTIONS ON;
GO
INSERT INTO dbo.t1 VALUES (41);
BEGIN TRANSACTION;
INSERT INTO dbo.t1 VALUES (42);
PRINT N'[D.03] @@TRANCOUNT, after INSERTs, == ' + CAST(@@TRANCOUNT AS NVARCHAR(10));
GO
COMMIT TRANSACTION;
PRINT N'[D.04] @@TRANCOUNT, after a COMMIT, == ' + CAST(@@TRANCOUNT AS NVARCHAR(10));
COMMIT TRANSACTION;
PRINT N'[D.05] @@TRANCOUNT, after another COMMIT, == ' + CAST(@@TRANCOUNT AS NVARCHAR(10));
GO

-- Clean up.
SET IMPLICIT_TRANSACTIONS OFF;
GO
WHILE (@@TRANCOUNT > 0) COMMIT TRANSACTION;
GO
DROP TABLE dbo.t1;
GO
```

Next is the text output from the preceding Transact-SQL script.

```output
-------- [Test A] ---- OFF ----
[A.01] Now, SET IMPLICIT_TRANSACTIONS OFF.
[A.02] @@TRANCOUNT, at start, == 0
[A.03] @@TRANCOUNT, after INSERTs, == 0

-------- [Test B] ---- ON ----
[B.01] Now, SET IMPLICIT_TRANSACTIONS ON.
[B.02] @@TRANCOUNT, at start, == 0
[B.03] @@TRANCOUNT, after INSERTs, == 1
[B.04] @@TRANCOUNT, after COMMIT, == 0

-------- [Test C] ---- ON, then BEGIN TRAN ----
[C.01] Now, SET IMPLICIT_TRANSACTIONS ON.
[C.02] @@TRANCOUNT, at start, == 0
[C.03] @@TRANCOUNT, after INSERTs, == 2
[C.04] @@TRANCOUNT, after a COMMIT, == 1
[C.05] @@TRANCOUNT, after another COMMIT, == 0

-------- [Test D] ---- ON, INSERT, BEGIN TRAN, INSERT ----
[D.01] Now, SET IMPLICIT_TRANSACTIONS ON.
[D.02] @@TRANCOUNT, at start, == 0
[D.03] @@TRANCOUNT, after INSERTs, == 2
[D.04] @@TRANCOUNT, after a COMMIT, == 1
[D.05] @@TRANCOUNT, after another COMMIT, == 0
```

## Related content

- [ALTER TABLE (Transact-SQL)](alter-table-transact-sql.md)
- [BEGIN TRANSACTION (Transact-SQL)](../language-elements/begin-transaction-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](create-table-transact-sql.md)
- [DELETE (Transact-SQL)](delete-transact-sql.md)
- [DROP TABLE (Transact-SQL)](drop-table-transact-sql.md)
- [FETCH (Transact-SQL)](../language-elements/fetch-transact-sql.md)
- [GRANT (Transact-SQL)](grant-transact-sql.md)
- [INSERT (Transact-SQL)](insert-transact-sql.md)
- [MERGE (Transact-SQL)](merge-transact-sql.md)
- [OPEN (Transact-SQL)](../language-elements/open-transact-sql.md)
- [REVOKE (Transact-SQL)](revoke-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [SET Statements (Transact-SQL)](set-statements-transact-sql.md)
- [SET ANSI_DEFAULTS (Transact-SQL)](set-ansi-defaults-transact-sql.md)
- [&#x40;&#x40;TRANCOUNT (Transact-SQL)](../functions/TRANCOUNT-transact-sql.md)
- [TRUNCATE TABLE (Transact-SQL)](truncate-table-transact-sql.md)
- [UPDATE (Transact-SQL)](../queries/update-transact-sql.md)
