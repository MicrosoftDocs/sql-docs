---
title: "SET TRANSACTION ISOLATION LEVEL (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/22/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "LEVEL"
  - "LEVEL_TSQL"
  - "SET TRANSACTION ISOLATION LEVEL"
  - "ISOLATION"
  - "ISOLATION_TSQL"
  - "SET_TRANSACTION_ISOLATION_LEVEL_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "SET TRANSACTION ISOLATION LEVEL statement"
  - "row versioning [SQL Server], isolation levels"
  - "TRANSACTION ISOLATION LEVEL option"
  - "isolation levels [SQL Server], setting"
  - "locking [SQL Server], isolation levels"
  - "transactions [SQL Server], isolation levels"
ms.assetid: 016fb05e-a702-484b-bd2a-a6eabd0d76fd
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET TRANSACTION ISOLATION LEVEL (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]


Controls the locking and row versioning behavior of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements issued by a connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```
-- Syntax for SQL Server and Azure SQL Database
  
SET TRANSACTION ISOLATION LEVEL
    { READ UNCOMMITTED
    | READ COMMITTED
    | REPEATABLE READ
    | SNAPSHOT
    | SERIALIZABLE
    }
```

```
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse
  
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
```

## Arguments  
 READ UNCOMMITTED  
 Specifies that statements can read rows that have been modified by other transactions but not yet committed.  
  
 Transactions running at the READ UNCOMMITTED level do not issue shared locks to prevent other transactions from modifying data read by the current transaction. READ UNCOMMITTED transactions are also not blocked by exclusive locks that would prevent the current transaction from reading rows that have been modified but not committed by other transactions. When this option is set, it is possible to read uncommitted modifications, which are called dirty reads. Values in the data can be changed and rows can appear or disappear in the data set before the end of the transaction. This option has the same effect as setting NOLOCK on all tables in all SELECT statements in a transaction. This is the least restrictive of the isolation levels.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can also minimize locking contention while protecting transactions from dirty reads of uncommitted data modifications using either:  
  
-   The READ COMMITTED isolation level with the READ_COMMITTED_SNAPSHOT database option set to ON.  
  
-   The SNAPSHOT isolation level. For more information about snapshot isolation, see [Snapshot Isolation in SQL Server](https://docs.microsoft.com/dotnet/framework/data/adonet/sql/snapshot-isolation-in-sql-server). 
  
 READ COMMITTED  
 Specifies that statements cannot read data that has been modified but not committed by other transactions. This prevents dirty reads. Data can be changed by other transactions between individual statements within the current transaction, resulting in nonrepeatable reads or phantom data. This option is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] default.  
  
 The behavior of READ COMMITTED depends on the setting of the READ_COMMITTED_SNAPSHOT database option:  
  
-   If READ_COMMITTED_SNAPSHOT is set to OFF (the default on SQL Server), the [!INCLUDE[ssDE](../../includes/ssde-md.md)] uses shared locks to prevent other transactions from modifying rows while the current transaction is running a read operation. The shared locks also block the statement from reading rows modified by other transactions until the other transaction is completed. The shared lock type determines when it will be released. Row locks are released before the next row is processed. Page locks are released when the next page is read, and table locks are released when the statement finishes.  
  
-   If READ_COMMITTED_SNAPSHOT is set to ON (the default on SQL Azure Database), the [!INCLUDE[ssDE](../../includes/ssde-md.md)] uses row versioning to present each statement with a transactionally consistent snapshot of the data as it existed at the start of the statement. Locks are not used to protect the data from updates by other transactions.

> [!IMPORTANT]  
> Choosing a transaction isolation level does not affect the locks acquired to protect data modifications. A transaction always gets an exclusive lock on any data it modifies, and holds that lock until the transaction completes, regardless of the isolation level set for that transaction. Additionally, an update made at the READ_COMMITTED isolation level uses update locks on the data rows selected, whereas an update made at the SNAPSHOT isolation level uses row versions to select rows to update. For read operations, transaction isolation levels primarily define the level of protection from the effects of modifications made by other transactions. See the [Transaction Locking and Row Versioning Guide](https://docs.microsoft.com/sql/relational-databases/sql-server-transaction-locking-and-row-versioning-guide) for more information.

> [!NOTE]  
>  Snapshot isolation supports FILESTREAM data. Under snapshot isolation mode, FILESTREAM data read by any statement in a transaction will be the transactionally consistent version of the data that existed at the start of the transaction.  
  
 When the READ_COMMITTED_SNAPSHOT database option is ON, you can use the READCOMMITTEDLOCK table hint to request shared locking instead of row versioning for individual statements in transactions running at the READ COMMITTED isolation level.  
  
> [!NOTE]  
>  When you set the READ_COMMITTED_SNAPSHOT option, only the connection executing the ALTER DATABASE command is allowed in the database. There must be no other open connection in the database until ALTER DATABASE is complete. The database does not have to be in single-user mode.  
  
 REPEATABLE READ  
 Specifies that statements cannot read data that has been modified but not yet committed by other transactions and that no other transactions can modify data that has been read by the current transaction until the current transaction completes.  
  
 Shared locks are placed on all data read by each statement in the transaction and are held until the transaction completes. This prevents other transactions from modifying any rows that have been read by the current transaction. Other transactions can insert new rows that match the search conditions of statements issued by the current transaction. If the current transaction then retries the statement it will retrieve the new rows, which results in phantom reads. Because shared locks are held to the end of a transaction instead of being released at the end of each statement, concurrency is lower than the default READ COMMITTED isolation level. Use this option only when necessary.  
  
 SNAPSHOT  
 Specifies that data read by any statement in a transaction will be the transactionally consistent version of the data that existed at the start of the transaction. The transaction can only recognize data modifications that were committed before the start of the transaction. Data modifications made by other transactions after the start of the current transaction are not visible to statements executing in the current transaction. The effect is as if the statements in a transaction get a snapshot of the committed data as it existed at the start of the transaction.  
  
 Except when a database is being recovered, SNAPSHOT transactions do not request locks when reading data. SNAPSHOT transactions reading data do not block other transactions from writing data. Transactions writing data do not block SNAPSHOT transactions from reading data.  
  
 During the roll-back phase of a database recovery, SNAPSHOT transactions will request a lock if an attempt is made to read data that is locked by another transaction that is being rolled back. The SNAPSHOT transaction is blocked until that transaction has been rolled back. The lock is released immediately after it has been granted.  
  
 The ALLOW_SNAPSHOT_ISOLATION database option must be set to ON before you can start a transaction that uses the SNAPSHOT isolation level. If a transaction using the SNAPSHOT isolation level accesses data in multiple databases, ALLOW_SNAPSHOT_ISOLATION must be set to ON in each database.  
  
 A transaction cannot be set to SNAPSHOT isolation level that started with another isolation level; doing so will cause the transaction to abort. If a transaction starts in the SNAPSHOT isolation level, you can change it to another isolation level and then back to SNAPSHOT. A transaction starts the first time it accesses data.  
  
 A transaction running under SNAPSHOT isolation level can view changes made by that transaction. For example, if the transaction performs an UPDATE on a table and then issues a SELECT statement against the same table, the modified data will be included in the result set.  
  
> [!NOTE]  
>  Under snapshot isolation mode, FILESTREAM data read by any statement in a transaction will be the transactionally consistent version of the data that existed at the start of the transaction, not at the start of the statement.  
  
 SERIALIZABLE  
 Specifies the following:  
  
-   Statements cannot read data that has been modified but not yet committed by other transactions.  
  
-   No other transactions can modify data that has been read by the current transaction until the current transaction completes.  
  
-   Other transactions cannot insert new rows with key values that would fall in the range of keys read by any statements in the current transaction until the current transaction completes.  
  
 Range locks are placed in the range of key values that match the search conditions of each statement executed in a transaction. This blocks other transactions from updating or inserting any rows that would qualify for any of the statements executed by the current transaction. This means that if any of the statements in a transaction are executed a second time, they will read the same set of rows. The range locks are held until the transaction completes. This is the most restrictive of the isolation levels because it locks entire ranges of keys and holds the locks until the transaction completes. Because concurrency is lower, use this option only when necessary. This option has the same effect as setting HOLDLOCK on all tables in all SELECT statements in a transaction.  
  
## Remarks  
 Only one of the isolation level options can be set at a time, and it remains set for that connection until it is explicitly changed. All read operations performed within the transaction operate under the rules for the specified isolation level unless a table hint in the FROM clause of a statement specifies different locking or versioning behavior for a table.  
  
 The transaction isolation levels define the type of locks acquired on read operations. Shared locks acquired for READ COMMITTED or REPEATABLE READ are generally row locks, although the row locks can be escalated to page or table locks if a significant number of the rows in a page or table are referenced by the read. If a row is modified by the transaction after it has been read, the transaction acquires an exclusive lock to protect that row, and the exclusive lock is retained until the transaction completes. For example, if a REPEATABLE READ transaction has a shared lock on a row, and the transaction then modifies the row, the shared row lock is converted to an exclusive row lock.  
  
 With one exception, you can switch from one isolation level to another at any time during a transaction. The exception occurs when changing from any isolation level to SNAPSHOT isolation. Doing this causes the transaction to fail and roll back. However, you can change a transaction started in SNAPSHOT isolation to any other isolation level.  
  
 When you change a transaction from one isolation level to another, resources that are read after the change are protected according to the rules of the new level. Resources that are read before the change continue to be protected according to the rules of the previous level. For example, if a transaction changed from READ COMMITTED to SERIALIZABLE, the shared locks acquired after the change are now held until the end of the transaction.  
  
 If you issue SET TRANSACTION ISOLATION LEVEL in a stored procedure or trigger, when the object returns control the isolation level is reset to the level in effect when the object was invoked. For example, if you set REPEATABLE READ in a batch, and the batch then calls a stored procedure that sets the isolation level to SERIALIZABLE, the isolation level setting reverts to REPEATABLE READ when the stored procedure returns control to the batch.  
  
> [!NOTE]  
>  User-defined functions and common language runtime (CLR) user-defined types cannot execute SET TRANSACTION ISOLATION LEVEL. However, you can override the isolation level by using a table hint. For more information, see [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md).  
  
 When you use sp_bindsession to bind two sessions, each session retains its isolation level setting. Using SET TRANSACTION ISOLATION LEVEL to change the isolation level setting of one session does not affect the setting of any other sessions bound to it.  
  
 SET TRANSACTION ISOLATION LEVEL takes effect at execute or run time, and not at parse time.  
  
 Optimized bulk load operations on heaps block queries that are running under the following isolation levels:  
  
-   SNAPSHOT  
  
-   READ UNCOMMITTED  
  
-   READ COMMITTED using row versioning  
  
 Conversely, queries that run under these isolation levels block optimized bulk load operations on heaps. For more information about bulk load operations, see [Bulk Import and Export of Data &#40;SQL Server&#41;](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md).  
  
 FILESTREAM-enabled databases support the following transaction isolation levels.  
  
|Isolation level|Transact SQL access|File system access|  
|---------------------|-------------------------|------------------------|  
|Read uncommitted|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]|Unsupported|  
|Read committed|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]|  
|Repeatable read|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]|Unsupported|  
|Serializable|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]|Unsupported|  
|Read committed snapshot|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]|  
|Snapshot|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]|[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]|  
  
## Examples  
 The following example sets the `TRANSACTION ISOLATION LEVEL` for the session. For each [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that follows, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] holds all of the shared locks until the end of the transaction.  
  
```  
USE AdventureWorks2012;  
GO  
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ;  
GO  
BEGIN TRANSACTION;  
GO  
SELECT *   
    FROM HumanResources.EmployeePayHistory;  
GO  
SELECT *   
    FROM HumanResources.Department;  
GO  
COMMIT TRANSACTION;  
GO  
```  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [DBCC USEROPTIONS &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-useroptions-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md)  
  
  
