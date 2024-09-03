---
title: "DBCC OPENTRAN (Transact-SQL)"
description: DBCC OPENTRAN helps to identify active transactions that may be preventing log truncation.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "DBCC_OPENTRAN_TSQL"
  - "DBCC OPENTRAN"
  - "OPENTRAN_TSQL"
  - "OPENTRAN"
helpviewer_keywords:
  - "status information [SQL Server], transactions"
  - "transactions [SQL Server], status information"
  - "DBCC OPENTRAN statement"
  - "open transactions"
  - "displaying transaction information"
  - "checking open transactions"
  - "oldest transactions [SQL Server]"
dev_langs:
  - "TSQL"
---
# DBCC OPENTRAN (Transact-SQL)

[!INCLUDE [SQL Server SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Helps to identify active transactions that may be preventing log truncation. `DBCC OPENTRAN` displays information about the oldest active transaction and the oldest distributed and nondistributed replicated transactions, if any, within the transaction log of the specified database. Results are displayed only if there is an active transaction that exists in the log or if the database contains replication information. An informational message is displayed if there are no active transactions in the log.

> [!NOTE]  
> `DBCC OPENTRAN` is not supported for non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC OPENTRAN
[
    ( [ database_name | database_id | 0 ] )
    { [ WITH TABLERESULTS ]
      [ , [ NO_INFOMSGS ] ]
    }
]
```

## Arguments

#### *database_name* | *database_id* | 0

The name or ID of the database for which to display the oldest transaction information. If not specified, or if 0 is specified, the current database is used. Database names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).

#### TABLERESULTS

Specifies the results in a tabular format that can be loaded into a table. Use this option to create a table of results that can be inserted into a table for comparisons. When this option isn't specified, results are formatted for readability.

#### NO_INFOMSGS

Suppresses all informational messages.

## Remarks

Use `DBCC OPENTRAN` to determine whether an open transaction exists within the transaction log. When you use the `BACKUP LOG` statement, only the inactive part of the log can be truncated; an open transaction can prevent the log from truncating completely. To identify an open transaction, use `sp_who` to obtain the system process ID.

## Result sets

`DBCC OPENTRAN` returns the following result set when there are no open transactions:

```output
No active open transactions.
DBCC execution completed. If DBCC printed error messages, contact your system administrator.
```

## Permissions

Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.

## Examples

### A. Return the oldest active transaction

The following example obtains transaction information for the current database. Results may vary.

```sql
CREATE TABLE T1(Col1 INT, Col2 CHAR(3));
GO
BEGIN TRAN
INSERT INTO T1 VALUES (101, 'abc');
GO
DBCC OPENTRAN;
ROLLBACK TRAN;
GO
DROP TABLE T1;
GO
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
Transaction information for database 'master'.
Oldest active transaction:
SPID (server process ID) : 52
UID (user ID) : -1
Name          : user_transaction
LSN           : (518:1576:1)
Start time    : Jun  1 2004  3:30:07:197PM
SID           : 0x010500000000000515000000a065cf7e784b9b5fe77c87709e611500
DBCC execution completed. If DBCC printed error messages, contact your system administrator.
```

> [!NOTE]  
> The "UID (user ID)" result is meaningless and will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

### B. Specify the WITH TABLERESULTS option

The following example loads the results of the `DBCC OPENTRAN` command into a temporary table.

```sql
-- Create the temporary table to accept the results.
CREATE TABLE #OpenTranStatus (
   ActiveTransaction VARCHAR(25),
   Details sql_variant
   );
-- Execute the command, putting the results in the table.
INSERT INTO #OpenTranStatus
   EXEC ('DBCC OPENTRAN WITH TABLERESULTS, NO_INFOMSGS');
  
-- Display the results.
SELECT * FROM #OpenTranStatus;
GO
```

## Related content

- [sys.dm_tran_database_transactions (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md)
- [BEGIN TRANSACTION (Transact-SQL)](../../t-sql/language-elements/begin-transaction-transact-sql.md)
- [COMMIT TRANSACTION (Transact-SQL)](../../t-sql/language-elements/commit-transaction-transact-sql.md)
- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
- [DB_ID (Transact-SQL)](../../t-sql/functions/db-id-transact-sql.md)
- [ROLLBACK TRANSACTION (Transact-SQL)](../../t-sql/language-elements/rollback-transaction-transact-sql.md)
