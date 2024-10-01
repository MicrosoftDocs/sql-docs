---
title: "Transaction locking and row versioning guide"
description: "Transaction locking and row versioning guide"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest, wiassaf
ms.date: 10/01/2024
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords:
  - "guide, transaction locking and row versioning"
  - "transaction locking and row versioning guide"
  - "lock compatibility matrix, [SQL Server]"
  - "lock granularity and hierarchies, [SQL Server]"
  - "deadlocks, [SQL Server]"
  - "lock escalation, [SQL Server]"
  - "lock partitioning, [SQL Server]"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Transaction locking and row versioning guide

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

In any database, mismanagement of transactions often leads to contention and performance problems in systems that have many users. As the number of users that access the data increases, it becomes important to have applications that use transactions efficiently. This guide describes locking and row versioning mechanisms the [!INCLUDE [Database Engine](../includes/ssde-md.md)] uses to ensure the integrity of each transaction and provides information on how applications can control transactions efficiently.

> [!NOTE]  
> **Optimized locking** is a [!INCLUDE [Database Engine](../includes/ssde-md.md)] feature introduced in 2023 that drastically reduces lock memory, and the number of locks required for concurrent writes. This article has been updated to describe the [!INCLUDE [Database Engine](../includes/ssde-md.md)] behavior with and without optimized locking.
>  
> - For more information and to learn where optimized locking is available, see [Optimized locking](performance/optimized-locking.md).
> - To determine if optimized locking is enabled on your database, see [Is optimized locking enabled?](performance/optimized-locking.md#is-optimized-locking-enabled)
>  
> Optimized locking has introduced significant changes to some sections of this article, including:
> - [Locking in the Database Engine](#lock_engine)
> - [Delete operation](#delete-operation)
> - [Insert operation](#insert-operation)
> - [Lock escalation](#lock-escalation)
> - [Reduce locking and escalation](#reducing-locking-and-escalation)
> - [Behavior when modifying data](#behavior-when-modifying-data)
> - [Behavior in summary](#behavior-in-summary)
> - [Locking hints](#locking-hints)

## <a id="Basics"></a> Transaction basics

A transaction is a sequence of operations performed as a single logical unit of work. A logical unit of work must exhibit four properties, called the atomicity, consistency, isolation, and durability (ACID) properties, to qualify as a transaction.

**Atomicity**  
A transaction must be an atomic unit of work; either all of its data modifications are performed, or none of them are performed.

**Consistency**  
When completed, a transaction must leave all data in a consistent state. In a relational database, all rules must be applied to the transaction's modifications to maintain all data integrity. All internal data structures, such as B-tree indexes or doubly linked lists, must be correct at the end of the transaction.

[!INCLUDE [sql-b-tree](../includes/sql-b-tree.md)]

**Isolation**  
Modifications made by concurrent transactions must be isolated from the modifications made by any other concurrent transactions. A transaction either recognizes data in the state it was in before another concurrent transaction modified it, or it recognizes the data after the second transaction has completed, but it doesn't recognize an intermediate state. This is referred to as serializability because it results in the ability to reload the starting data and replay a series of transactions to end up with the data in the same state it was in after the original transactions were performed.

**Durability**  
After a fully durable transaction has completed, its effects are permanently in place in the system. The modifications persist even in the event of a system failure. [!INCLUDE [ssSQL14](../includes/sssql14-md.md)] and later enable delayed durable transactions. Delayed durable transactions commit before the transaction log record is persisted to disk. For more information on delayed transaction durability, see the article [Control Transaction Durability](logs/control-transaction-durability.md).

Applications are responsible for starting and ending transactions at points that enforce the logical consistency of the data. The application must define the sequence of data modifications that leave the data in a consistent state relative to the organization's business rules. The application performs these modifications in a single transaction so that the [!INCLUDE [Database Engine](../includes/ssde-md.md)] can enforce the integrity of the transaction.

It is the responsibility of an enterprise database system, such as an instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)], to provide mechanisms ensuring the integrity of each transaction. The [!INCLUDE [Database Engine](../includes/ssde-md.md)] provides:

- Locking facilities that preserve transaction isolation.

- Logging facilities to ensure transaction durability. For fully durable transactions the log record is hardened to disk before the transactions commits. Thus, even if the server hardware, operating system, or the instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] itself fails, the instance uses the transaction logs upon restart to automatically roll back any incomplete transactions to the point of the system failure. Delayed durable transactions commit before the transaction log record is hardened to disk. Such transactions may be lost if there is a system failure before the log record is hardened to disk. For more information on delayed transaction durability, see the article [Control Transaction Durability](logs/control-transaction-durability.md).

- Transaction management features that enforce transaction atomicity and consistency. After a transaction has started, it must be successfully completed (committed), or the [!INCLUDE [Database Engine](../includes/ssde-md.md)] undoes all of the data modifications made by the transaction since the transaction started. This operation is referred to as rolling back a transaction because it returns the data to the state it was prior to those changes.

### <a id="controlling-transactions"></a> Control transactions

Applications control transactions mainly by specifying when a transaction starts and ends. This can be specified by using either [!INCLUDE [tsql](../includes/tsql-md.md)] statements or database application programming interface (API) functions. The system must also be able to correctly handle errors that terminate a transaction before it completes. For more information, see [Transactions](../t-sql/language-elements/transactions-transact-sql.md), [Performing Transactions in ODBC](native-client/odbc/performing-transactions-in-odbc.md), and [Transactions in SQL Server Native Client](native-client-ole-db-transactions/transactions.md).

By default, transactions are managed at the connection level. When a transaction is started on a connection, all [!INCLUDE [tsql](../includes/tsql-md.md)] statements executed on that connection are part of the transaction until the transaction ends. However, under a multiple active result set (MARS) session, a [!INCLUDE [tsql](../includes/tsql-md.md)] explicit or implicit transaction becomes a batch-scoped transaction that is managed at the batch level. When the batch completes, if the batch-scoped transaction isn't committed or rolled back, it is automatically rolled back by the [!INCLUDE [Database Engine](../includes/ssde-md.md)]. For more information, see [Using Multiple Active Result Sets (MARS)](native-client/features/using-multiple-active-result-sets-mars.md).

#### <a id="starting-transactions"></a> Start transactions

Using API functions and [!INCLUDE [tsql](../includes/tsql-md.md)] statements, you can start transactions as explicit, autocommit, or implicit transactions.

**Explicit transactions**
An explicit transaction is one in which you explicitly define both the start and end of the transaction through an API function or by issuing the [!INCLUDE [tsql](../includes/tsql-md.md)] BEGIN TRANSACTION, COMMIT TRANSACTION, COMMIT WORK, ROLLBACK TRANSACTION, or ROLLBACK WORK [!INCLUDE [tsql](../includes/tsql-md.md)] statements. When the transaction ends, the connection returns to the transaction mode it was in before the explicit transaction was started, which might be the implicit or autocommit mode.

You can use all [!INCLUDE [tsql](../includes/tsql-md.md)] statements in an explicit transaction, except for the following statements:

- CREATE DATABASE
- ALTER DATABASE
- DROP DATABASE
- CREATE FULLTEXT CATALOG
- ALTER FULLTEXT CATALOG
- DROP FULLTEXT CATALOG
- DROP FULLTEXT INDEX
- ALTER FULLTEXT INDEX
- CREATE FULLTEXT INDEX
- BACKUP
- RESTORE
- RECONFIGURE
- Full-text system stored procedures
- `sp_dboption` to set database options or any system procedure that modifies the `master` database inside explicit or implicit transactions.

> [!NOTE]  
> UPDATE STATISTICS can be used inside an explicit transaction. However, UPDATE STATISTICS commits independently of the enclosing transaction and cannot be rolled back.

**Autocommit Transactions**
Autocommit mode is the default transaction management mode of the [!INCLUDE [Database Engine](../includes/ssde-md.md)]. Every [!INCLUDE [tsql](../includes/tsql-md.md)] statement is committed or rolled back when it completes. If a statement completes successfully, it is committed; if it encounters any error, it is rolled back. A connection to an instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] operates in autocommit mode whenever this default mode hasn't been overridden by either explicit or implicit transactions. Autocommit mode is also the default mode for SqlClient, ADO, OLE DB, and ODBC.

**Implicit Transactions**
When a connection is operating in implicit transaction mode, the instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] automatically starts a new transaction after the current transaction is committed or rolled back. You do nothing to delineate the start of a transaction; you only commit or roll back each transaction. Implicit transaction mode generates a continuous chain of transactions. Set implicit transaction mode on through either an API function or the [!INCLUDE [tsql](../includes/tsql-md.md)] `SET IMPLICIT_TRANSACTIONS ON` statement. This mode is also known as Autocommit OFF, see [setAutoCommit Method (SQLServerConnection)](../connect/jdbc/reference/setautocommit-method-sqlserverconnection.md).

After implicit transaction mode has been set on for a connection, the instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] automatically starts a transaction when it first executes any of these statements:

- `ALTER TABLE`
- `CREATE`
- `DELETE`
- `DENY`
- `DROP`
- `FETCH`
- `GRANT`
- `INSERT`
- `OPEN`
- `REVOKE`
- `SELECT`
- `TRUNCATE`
- `UPDATE`

**Batch-scoped Transactions**
Applicable only to multiple active result sets (MARS), a [!INCLUDE [tsql](../includes/tsql-md.md)] explicit or implicit transaction that starts under a MARS session becomes a batch-scoped transaction. A batch-scoped transaction that isn't committed or rolled back when a batch completes is automatically rolled back by the [!INCLUDE [Database Engine](../includes/ssde-md.md)].

**Distributed transactions**
Distributed transactions span two or more servers known as resource managers. The management of the transaction must be coordinated between the resource managers by a server component called a transaction manager. Each instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] can operate as a resource manager in distributed transactions coordinated by transaction managers, such as [!INCLUDE [msCoName](../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC), or other transaction managers that support the Open Group XA specification for distributed transaction processing. For more information, see the MS DTC documentation.

A transaction within a single instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] that spans two or more databases is a distributed transaction. The instance manages the distributed transaction internally; to the user, it operates as a local transaction.

In the application, a distributed transaction is managed much the same as a local transaction. At the end of the transaction, the application requests the transaction to be either committed or rolled back. A distributed commit must be managed differently by the transaction manager to minimize the risk that a network failure may result in some resource managers successfully committing while others roll back the transaction. This is achieved by managing the commit process in two phases (the prepare phase and the commit phase), which is known as a two-phase commit.

- **Prepare phase**

  When the transaction manager receives a commit request, it sends a prepare command to all of the resource managers involved in the transaction. Each resource manager then does everything required to make the transaction durable, and all transaction log buffers for the transaction are flushed to disk. As each resource manager completes the prepare phase, it returns success or failure of the prepare to the transaction manager. [!INCLUDE [ssSQL14](../includes/sssql14-md.md)] introduced delayed transaction durability. Delayed durable transactions commit before the transaction log buffers on each resource manager are flushed to disk. For more information on delayed transaction durability, see the article [Control Transaction Durability](logs/control-transaction-durability.md).

- **Commit phase**

  If the transaction manager receives successful prepares from all of the resource managers, it sends commit commands to each resource manager. The resource managers can then complete the commit. If all of the resource managers report a successful commit, the transaction manager then sends a success notification to the application. If any resource manager reported a failure to prepare, the transaction manager sends a rollback command to each resource manager and indicates the failure of the commit to the application.

  [!INCLUDE [Database Engine](../includes/ssde-md.md)] applications can manage distributed transactions either through [!INCLUDE [tsql](../includes/tsql-md.md)] or through the database API. For more information, see [BEGIN DISTRIBUTED TRANSACTION (Transact-SQL)](../t-sql/language-elements/begin-distributed-transaction-transact-sql.md).

#### <a id="ending-transactions"></a> End transactions

You can end transactions with either a COMMIT or ROLLBACK statement, or through a corresponding API function.

- **Commit**

  If a transaction is successful, commit it. A `COMMIT` statement guarantees all of the transaction's modifications are made a permanent part of the database. A commit also frees resources, such as locks, used by the transaction.

- **Roll back**

  If an error occurs in a transaction, or if the user decides to cancel the transaction, roll back the transaction. A `ROLLBACK` statement backs out all modifications made in the transaction by returning the data to the state it was in at the start of the transaction. Roll back also frees resources held by the transaction.

> [!NOTE]  
> On multiple active result sets (MARS) sessions, an explicit transaction started through an API function cannot be committed while there are pending execution requests. Any attempt to commit this type of transaction while there are executing requests will result in an error.

#### Errors during transaction processing

If an error prevents the successful completion of a transaction, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] automatically rolls back the transaction and frees all resources held by the transaction. If the client network connection to an instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] is broken, any outstanding transactions for the connection are rolled back when the network notifies the instance of the connection break. If the client application fails or if the client computer goes down or is restarted, this also breaks the connection, and the instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] rolls back any outstanding transactions when the network notifies it of the connection break. If the client disconnects from the [!INCLUDE [Database Engine](../includes/ssde-md.md)], any outstanding transactions are rolled back.

If a run-time statement error (such as a constraint violation) occurs in a batch, the default behavior in the [!INCLUDE [Database Engine](../includes/ssde-md.md)] is to roll back only the statement that generated the error. You can change this behavior using the `SET XACT_ABORT ON` statement. After `SET XACT_ABORT ON` is executed, any run-time statement error causes an automatic rollback of the current transaction. Compile errors, such as syntax errors, are not affected by `SET XACT_ABORT`. For more information, see [SET XACT_ABORT (Transact-SQL)](../t-sql/statements/set-xact-abort-transact-sql.md).

When errors occur, the appropriate action (`COMMIT` or `ROLLBACK`) should be included in application code. One effective tool for handling errors, including those in transactions, is the [!INCLUDE [tsql](../includes/tsql-md.md)] `TRY...CATCH` construct. For more information with examples that include transactions, see [TRY...CATCH (Transact-SQL)](../t-sql/language-elements/try-catch-transact-sql.md). Beginning with [!INCLUDE [ssSQL11](../includes/sssql11-md.md)], you can use the `THROW` statement to raise an exception and transfers execution to a `CATCH` block of a `TRY...CATCH` construct. For more information, see [THROW (Transact-SQL)](../t-sql/language-elements/throw-transact-sql.md).

##### Compile and run-time errors in autocommit mode

In autocommit mode, it sometimes appears as if an instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] has rolled back an entire batch instead of just one SQL statement. This happens if the error encountered is a compile error, not a run-time error. A compile error prevents the [!INCLUDE [Database Engine](../includes/ssde-md.md)] from building an execution plan, hence nothing in the batch can be executed. Although it appears that all of the statements before the one generating the error were rolled back, the error prevented anything in the batch from being executed. In the following example, none of the `INSERT` statements in the third batch are executed because of a compile error. It appears that the first two `INSERT` statements are rolled back when they are never executed.

```sql
CREATE TABLE TestBatch (ColA INT PRIMARY KEY, ColB CHAR(3));
GO
INSERT INTO TestBatch VALUES (1, 'aaa');
INSERT INTO TestBatch VALUES (2, 'bbb');
INSERT INTO TestBatch VALUSE (3, 'ccc');  -- Syntax error.
GO
SELECT * FROM TestBatch;  -- Returns no rows.
GO
```

In the following example, the third `INSERT` statement generates a run-time duplicate primary key error. The first two `INSERT` statements are successful and committed, so they remain after the run-time error.

```sql
CREATE TABLE TestBatch (Cola INT PRIMARY KEY, Colb CHAR(3));
GO
INSERT INTO TestBatch VALUES (1, 'aaa');
INSERT INTO TestBatch VALUES (2, 'bbb');
INSERT INTO TestBatch VALUES (1, 'ccc');  -- Duplicate key error.
GO
SELECT * FROM TestBatch;  -- Returns rows 1 and 2.
GO
```

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] uses deferred name resolution, where object names are resolved at execution time, not at compilation time. In the following example, the first two `INSERT` statements are executed and committed, and those two rows remain in the `TestBatch` table after the third `INSERT` statement generates a run-time error by referring to a table that doesn't exist.

```sql
CREATE TABLE TestBatch (ColA INT PRIMARY KEY, ColB CHAR(3));
GO
INSERT INTO TestBatch VALUES (1, 'aaa');
INSERT INTO TestBatch VALUES (2, 'bbb');
INSERT INTO TestBch VALUES (3, 'ccc');  -- Table name error.
GO
SELECT * FROM TestBatch;  -- Returns rows 1 and 2.
GO
```

## <a id="Lock_Basics"></a> Locking and row versioning basics

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] uses the following mechanisms to ensure the integrity of transactions and maintain the consistency of databases when multiple users are accessing data at the same time:

- **Locking**

   Each transaction requests locks of different types on the resources, such as rows, pages, or tables, on which the transaction is dependent. The locks block other transactions from modifying the resources in a way that would cause problems for the transaction requesting the lock. Each transaction frees its locks when it no longer has a dependency on the locked resources.

- **Row versioning**

   When a row versioning based isolation level is used, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] maintains versions of each row that is modified. Applications can specify that a transaction use the row versions to view data as it existed at the start of the transaction or statement, instead of protecting all reads with locks. By using row versioning, the chance that a read operation will block other transactions is greatly reduced.

Locking and row versioning prevent users from reading uncommitted data and prevent multiple users from attempting to change the same data at the same time. Without locking or row versioning, queries executed against that data could produce unexpected results by returning data that hasn't yet been committed in the database.

Applications can choose transaction isolation levels, which define the level of protection for the transaction from modifications made by other transactions. Table-level hints can be specified for individual [!INCLUDE [tsql](../includes/tsql-md.md)] statements to further tailor the behavior to fit the requirements of the application.

### <a id="managing-concurrent-data-access"></a> Manage concurrent data access

Users who access a resource at the same time are said to be accessing the resource concurrently. Concurrent data access requires mechanisms to prevent adverse effects when multiple users try to modify resources that other users are actively using.

#### Concurrency effects

Users modifying data can affect other users who are reading or modifying the same data at the same time. These users are said to be accessing the data concurrently. If a database has no concurrency control, users could see the following side effects:

- **Lost updates**

     Lost updates occur when two or more transactions select the same row and then update the row based on the value originally selected. Each transaction is unaware of the other transactions. The last update overwrites updates made by the other transactions, which results in lost data.

     For example, two editors make an electronic copy of the same document. Each editor changes the copy independently and then saves the changed copy thereby overwriting the original document. The editor who saves the changed copy last overwrites the changes made by the other editor. This problem could be avoided if one editor could not access the file until the other editor had finished and committed the transaction.

- **Uncommitted dependency (dirty read)**

     Uncommitted dependency occurs when a second transaction reads a row that is being updated by another transaction. The second transaction is reading data that hasn't been committed yet and may be changed by the transaction updating the row.

     For example, an editor is making changes to an electronic document. During the changes, a second editor takes a copy of the document that includes all the changes made so far, and distributes the document to the intended audience. The first editor then decides the changes made so far are wrong and removes the edits and saves the document. The distributed document contains edits that no longer exist and should be treated as if they never existed. This problem could be avoided if no one could read the changed document until the first editor does the final save of modifications and commits the transaction.

- **Inconsistent analysis (nonrepeatable read)**

     Inconsistent analysis occurs when a second transaction accesses the same row several times and reads different data each time. Inconsistent analysis is similar to uncommitted dependency in that another transaction is changing the data that a second transaction is reading. However, in inconsistent analysis, the data read by the second transaction was committed by the transaction that made the change. Also, inconsistent analysis involves multiple reads (two or more) of the same row, and each time the information is changed by another transaction; thus, the term nonrepeatable read.

     For example, an editor reads the same document twice, but between each reading the writer rewrites the document. When the editor reads the document for the second time, it has changed. The original read was not repeatable. This problem could be avoided if the writer could not change the document until the editor has finished reading it for the last time.

- **Phantom reads**

     A phantom read is a situation that occurs when two identical queries are executed and the set of rows returned by the second query is different. The example below shows how this may occur. Assume the two transactions below are executing at the same time. The two `SELECT` statements in the first transaction may return different results because the `INSERT` statement in the second transaction changes the data used by both.

    ```sql
    --Transaction 1
    BEGIN TRAN;

    SELECT ID
    FROM dbo.employee
    WHERE ID > 5 AND ID < 10;

    --The INSERT statement from the second transaction occurs here.

    SELECT ID
    FROM dbo.employee
    WHERE ID > 5 and ID < 10;
    
    COMMIT;
    ```

    ```sql
    --Transaction 2
    BEGIN TRAN;
    INSERT INTO dbo.employee (Id, Name)
    VALUES(6 ,'New');

    COMMIT;
    ```

- **Missing and double reads caused by row updates**

  - Missing an updated row or seeing an updated row multiple times

    Transactions that are running at the `READ UNCOMMITTED` level (or statements using the `NOLOCK` table hint) do not issue shared locks to prevent other transactions from modifying data read by the current transaction. Transactions that are running at the `READ COMMITTED` level do issue shared locks, but the row or page locks are released after the row is read. In either case, when you are scanning an index, if another user changes the index key column of the row during your read, the row might appear again if the key change moved the row to a position ahead of your scan. Similarly, the row might not be read at all if the key change moved the row to a position in the index that you had already read. To avoid this, use the `SERIALIZABLE` or `HOLDLOCK` hint, or row versioning. For more information, see [Table Hints (Transact-SQL)](../t-sql/queries/hints-transact-sql-table.md).

  - Missing one or more rows that were not the target of update

    When you are using `READ UNCOMMITTED`, if your query reads rows using an allocation order scan (using IAM pages), you might miss rows if another transaction is causing a page split. This does not occur when you are using the `READ COMMITTED` isolation level.

#### Types of concurrency

When multiple transactions attempt to modify data in a database at the same time, a system of controls must be implemented so that modifications made by one transaction do not adversely affect those of another transaction. This is called concurrency control.

Concurrency control theory has two classifications for the methods of instituting concurrency control:

- **Pessimistic** concurrency control

     A system of locks prevents transactions from modifying data in a way that affects other transactions. After a transactions performs an action that causes a lock to be applied, other transactions cannot perform actions that would conflict with the lock until the owner releases it. This is called pessimistic control because it is mainly used in environments where there is high contention for data, where the cost of protecting data with locks is less than the cost of rolling back transactions if concurrency conflicts occur.

- **Optimistic** concurrency control

     In optimistic concurrency control, transactions do not lock data when they read it. However, when a transaction updates data, the system checks to see if another transaction changed the data after it was read. If another transaction updated the data, an error is raised. Typically, the transaction receiving the error rolls back and starts over. This is called optimistic because it is mainly used in environments where there is low contention for data, and where the cost of occasionally rolling back a transaction is lower than the cost of locking data when read.

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] supports both concurrency control methods. Users specify the type of concurrency control by selecting transaction isolation levels for connections or concurrency options on cursors. These attributes can be defined using [!INCLUDE [tsql](../includes/tsql-md.md)] statements, or through the properties and attributes of database application programming interfaces (APIs) such as ADO, ADO.NET, OLE DB, and ODBC.

#### Isolation levels in the Database Engine

Transactions specify an isolation level that defines the degree to which one transaction must be isolated from the resource or data modifications made by other transactions. Isolation levels are described in terms of which concurrency side-effects, such as dirty reads or phantom reads, are allowed.

Transaction isolation levels control:

- Whether locks are acquired when data is read, and what type of locks are requested.
- How long the read locks are held.
- Whether a read operation referencing rows modified by another transaction:
  - Blocks until the exclusive lock on the row is freed.
  - Retrieves the committed version of the row that existed at the time the statement or transaction started.
  - Reads the uncommitted data modification.

> [!IMPORTANT]  
> Choosing a transaction isolation level does not affect the locks acquired to protect data modifications. A transaction always holds an exclusive lock to perform data modification, and holds that lock until the transaction completes, regardless of the isolation level set for that transaction. For read operations, transaction isolation levels primarily define the level of protection from the effects of modifications made by other transactions.

A lower isolation level increases the ability of many transactions to access data at the same time, but also increases the number of concurrency effects (such as dirty reads or lost updates) transactions might encounter. Conversely, a higher isolation level reduces the types of concurrency effects that transactions may encounter, but requires more system resources and increases the chances that one transaction will block another. Choosing the appropriate isolation level depends on balancing the data integrity requirements of the application against the overhead of each isolation level. The highest isolation level, `SERIALIZABLE`, guarantees that a transaction will retrieve exactly the same data every time it repeats a read operation, but it does this by performing a level of locking that is likely to impact other transactions in multi-user systems. The lowest isolation level, `READ UNCOMMITTED`, may retrieve data that has been modified but not committed by other transactions. All of the concurrency side effects can happen in `READ UNCOMMITTED`, but there is no read locking or versioning, so overhead is minimized.

##### [!INCLUDE [Database Engine](../includes/ssde-md.md)] isolation levels

The ISO standard defines the following isolation levels, all of which are supported by the Database Engine:

| Isolation Level | Definition |
| --- | --- |
| **Read uncommitted** | The lowest isolation level where transactions are isolated only enough to ensure that physically inconsistent data isn't read. In this level, dirty reads are allowed, so one transaction may see not-yet-committed changes made by other transactions. |
| **Read committed** | Allows a transaction to read data previously read (not modified) by another transaction without waiting for the first transaction to complete. The [!INCLUDE [Database Engine](../includes/ssde-md.md)] keeps write locks (acquired on selected data) until the end of the transaction, but read locks are released as soon as the read operation is performed. This is the [!INCLUDE [Database Engine](../includes/ssde-md.md)] default level. |
| **Repeatable read** | The [!INCLUDE [Database Engine](../includes/ssde-md.md)] keeps read and write locks that are acquired on selected data until the end of the transaction. However, because range-locks are not managed, phantom reads can occur. |
| **Serializable** | The highest level where transactions are completely isolated from one another. The [!INCLUDE [Database Engine](../includes/ssde-md.md)] keeps read and write locks acquired on selected data until the end of the transaction. Range-locks are acquired when a SELECT operation uses a range WHERE clause to avoid phantom reads.<br /><br />**Note:** DDL operations and transactions on replicated tables may fail when the `SERIALIZABLE` isolation level is requested. This is because replication queries use hints that may be incompatible with the `SERIALIZABLE` isolation level. |

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] also supports two additional transaction isolation levels that use row versioning. One is an implementation of `READ COMMITTED` isolation level, and one is the `SNAPSHOT` transaction isolation level.

| Row Versioning Isolation Level | Definition |
| --- | --- |
| **Read Committed Snapshot (RCSI)** | When the `READ_COMMITTED_SNAPSHOT` database option is set `ON`, which is the default setting in [!INCLUDE [Azure SQL Database](../includes/ssazure-sqldb.md)], the `READ COMMITTED` isolation level uses row versioning to provide statement-level read consistency. Read operations require only the schema stability (`Sch-S`) table level locks and no page or row locks. That is, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] uses row versioning to present each statement with a transactionally consistent snapshot of the data as it existed at the start of the statement. Locks are not used to protect the data from updates by other transactions. A user-defined function can return data that was committed after the time the statement containing the UDF began.<br /><br />When the `READ_COMMITTED_SNAPSHOT` database option is set `OFF`, which is the default setting in [!INCLUDE [SQL Server](../includes/ssnoversion-md.md)] and [!INCLUDE [Azure SQL Managed Instance](../includes/ssazuremi-md.md)], `READ COMMITTED` isolation uses shared locks to prevent other transactions from modifying rows while the current transaction is running a read operation. The shared locks also block the statement from reading rows modified by other transactions until the other transaction is completed. Both implementations meet the ISO definition of `READ COMMITTED` isolation. |
| **Snapshot** | The snapshot isolation level uses row versioning to provide transaction-level read consistency. Read operations acquire no page or row locks; only the schema stability (`Sch-S`) table locks are acquired. When reading rows modified by another transaction, read operations retrieve the version of the row that existed when the transaction started. You can only use `SNAPSHOT` isolation when the `ALLOW_SNAPSHOT_ISOLATION` database option is set to `ON`. By default, this option is set to `OFF` for user databases in [!INCLUDE [SQL Server](../includes/ssnoversion-md.md)] and [!INCLUDE [Azure SQL Managed Instance](../includes/ssazuremi-md.md)], and set to `ON` for databases in [!INCLUDE [Azure SQL Database](../includes/ssazure-sqldb.md)].<br /><br />**Note:** The [!INCLUDE [Database Engine](../includes/ssde-md.md)] doesn't support versioning of metadata. For this reason, there are restrictions on what DDL operations can be performed in an explicit transaction that is running under snapshot isolation. The following DDL statements are not permitted under snapshot isolation after a `BEGIN TRANSACTION` statement: `ALTER TABLE`, `CREATE INDEX`, `CREATE XML INDEX`, `ALTER INDEX`, `DROP INDEX`, `DBCC REINDEX`, `ALTER PARTITION FUNCTION`, `ALTER PARTITION SCHEME`, or any common language runtime (CLR) DDL statement. These statements are permitted when you are using snapshot isolation within implicit transactions. An implicit transaction, by definition, is a single statement that makes it possible to enforce the semantics of snapshot isolation, even with DDL statements. Violations of this principle can cause error 3961: `Snapshot isolation transaction failed in database '%.*ls' because the object accessed by the statement has been modified by a DDL statement in another concurrent transaction since the start of this transaction. It is not allowed because the metadata is not versioned. A concurrent update to metadata could lead to inconsistency if mixed with snapshot isolation.` |

The following table shows the concurrency side effects enabled by the different isolation levels.

| Isolation level | Dirty read | Nonrepeatable read | Phantom |
| --- | --- | --- | --- |
| **Read uncommitted** | Yes | Yes | Yes |
| **Read committed** | No | Yes | Yes |
| **Repeatable read** | No | No | Yes |
| **Snapshot** | No | No | No |
| **Serializable** | No | No | No |

For more information about the specific types of locking or row versioning controlled by each transaction isolation level, see [SET TRANSACTION ISOLATION LEVEL (Transact-SQL)](../t-sql/statements/set-transaction-isolation-level-transact-sql.md).

Transaction isolation levels can be set using [!INCLUDE [tsql](../includes/tsql-md.md)] or through a database API.

**[!INCLUDE [tsql](../includes/tsql-md.md)]**  
[!INCLUDE [tsql](../includes/tsql-md.md)] scripts use the `SET TRANSACTION ISOLATION LEVEL` statement.

**ADO**  
ADO applications set the `IsolationLevel` property of the `Connection` object to `adXactReadUncommitted`, `adXactReadCommitted`, `adXactRepeatableRead`, or `adXactReadSerializable`.

**ADO.NET**  
ADO.NET applications using the `System.Data.SqlClient` managed namespace can call the `SqlConnection.BeginTransaction` method and set the `IsolationLevel` option to `Unspecified`, `Chaos`, `ReadUncommitted`, `ReadCommitted`, `RepeatableRead`, `Serializable`, or `Snapshot`.

**OLE DB**  
When starting a transaction, applications using OLE DB call `ITransactionLocal::StartTransaction` with `isoLevel` set to `ISOLATIONLEVEL_READUNCOMMITTED`, `ISOLATIONLEVEL_READCOMMITTED`, `ISOLATIONLEVEL_REPEATABLEREAD`, `ISOLATIONLEVEL_SNAPSHOT`, or `ISOLATIONLEVEL_SERIALIZABLE`.

When specifying the transaction isolation level in autocommit mode, OLE DB applications can set the `DBPROPSET_SESSION` property `DBPROP_SESS_AUTOCOMMITISOLEVELS` to `DBPROPVAL_TI_CHAOS`, `DBPROPVAL_TI_READUNCOMMITTED`, `DBPROPVAL_TI_BROWSE`, `DBPROPVAL_TI_CURSORSTABILITY`, `DBPROPVAL_TI_READCOMMITTED`, `DBPROPVAL_TI_REPEATABLEREAD`, `DBPROPVAL_TI_SERIALIZABLE`, `DBPROPVAL_TI_ISOLATED`, or `DBPROPVAL_TI_SNAPSHOT`.

**ODBC**  
ODBC applications call `SQLSetConnectAttr` with `Attribute` set to `SQL_ATTR_TXN_ISOLATION` and `ValuePtr` set to `SQL_TXN_READ_UNCOMMITTED`, `SQL_TXN_READ_COMMITTED`, `SQL_TXN_REPEATABLE_READ`, or `SQL_TXN_SERIALIZABLE`.

For snapshot transactions, applications call `SQLSetConnectAttr` with Attribute set to `SQL_COPT_SS_TXN_ISOLATION` and `ValuePtr` set to `SQL_TXN_SS_SNAPSHOT`. A snapshot transaction can be retrieved using either `SQL_COPT_SS_TXN_ISOLATION` or `SQL_ATTR_TXN_ISOLATION`.

## <a id="lock_engine"></a> Locking in the Database Engine

Locking is a mechanism used by the [!INCLUDE [Database Engine](../includes/ssde-md.md)] to synchronize access by multiple users to the same piece of data at the same time.

Before a transaction acquires a dependency on the current state of a piece of data, such as by reading or modifying the data, it must protect itself from the effects of another transaction modifying the same data. The transaction does this by requesting a lock on the piece of data. Locks have different modes, such as shared (`S`) or exclusive (`X`). The lock mode defines the level of dependency the transaction has on the data. No transaction can be granted a lock that would conflict with the mode of a lock already granted on that data to another transaction. If a transaction requests a lock mode that conflicts with a lock that has already been granted on the same data, the the [!INCLUDE [Database Engine](../includes/ssde-md.md)] will pause the requesting transaction until the first lock is released.

When a transaction modifies a piece of data, it holds certain locks protecting the modification until the end of the transaction. How long a transaction holds the locks acquired to protect read operations depends on the transaction isolation level setting and whether or not [optimized locking](performance/optimized-locking.md) is enabled.

- When optimized locking isn't enabled, row and page locks necessary for writes are held until the end of the transaction.

- When optimized locking is enabled, only a Transaction ID (TID) lock is held until the end of the transaction. Under the default `READ COMMITTED` isolation level, transactions will not hold row and page locks necessary for writes until the end of the transaction. This reduces lock memory required and reduces the need for lock escalation. Further, when optimized locking is enabled, the [lock after qualification (LAQ)](./performance/optimized-locking.md#optimized-locking-and-lock-after-qualification-laq) optimization evaluates predicates of a query on the latest committed version of the row without acquiring a lock, improving concurrency.

All locks held by a transaction are released when the transaction completes (either commits or rolls back).

Applications do not typically request locks directly. Locks are managed internally by a part of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] called the lock manager. When an instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] processes a [!INCLUDE [tsql](../includes/tsql-md.md)] statement, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] query processor determines which resources are to be accessed. The query processor determines what types of locks are required to protect each resource based on the type of access and the transaction isolation level setting. The query processor then requests the appropriate locks from the lock manager. The lock manager grants the locks if there are no conflicting locks held by other transactions.

## Lock granularity and hierarchies

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] has multigranular locking that allows different types of resources to be locked by a transaction. To minimize the cost of locking, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] locks resources automatically at a level appropriate to the task. Locking at a smaller granularity, such as rows, increases concurrency but has a higher overhead because more locks must be held if many rows are locked. Locking at a larger granularity, such as tables, are expensive in terms of concurrency because locking an entire table restricts access to any part of the table by other transactions. However, it has a lower overhead because fewer locks are being maintained.

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] often has to acquire locks at multiple levels of granularity to fully protect a resource. This group of locks at multiple levels of granularity is called a lock hierarchy. For example, to fully protect a read of an index, an instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] may have to acquire shared locks on rows and intent shared locks on the pages and table.

The following table shows the resources that the [!INCLUDE [Database Engine](../includes/ssde-md.md)] can lock.

| Resource | Description |
| --- | --- |
| `RID` | A row identifier used to lock a single row within a heap. |
| `KEY` | A row lock to lock a single row in a B-tree index. |
| `PAGE` | An 8 kilobyte (KB) page in a database, such as data or index pages. |
| `EXTENT` | A contiguous group of eight pages, such as data or index pages. |
| `HoBT` <sup>1</sup> | A heap or B-tree. A lock protecting a B-tree (index) or the heap data pages in a table that doesn't have a clustered index. |
| `TABLE` <sup>1</sup> | The entire table, including all data and indexes. |
| `FILE` | A database file. |
| `APPLICATION` | An application-specified resource. |
| `METADATA` | Metadata locks. |
| `ALLOCATION_UNIT` | An allocation unit. |
| `DATABASE` | The entire database. |
| `XACT` <sup>2</sup> | Transaction ID (TID) lock used in [Optimized locking](performance/optimized-locking.md). For more information, see [Transaction ID (TID) locking](performance/optimized-locking.md#optimized-locking-and-transaction-id-tid-locking). |

<sup>1</sup> `HoBT` and `TABLE` locks can be affected by the `LOCK_ESCALATION` option of [ALTER TABLE](../t-sql/statements/alter-table-transact-sql.md).

<sup>2</sup> Additional locking resources are available for `XACT` lock resources, see [Diagnostic additions for optimized locking](performance/optimized-locking.md#diagnostic-additions-for-optimized-locking).

## <a id="lock_modes"></a> Lock modes

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] locks resources using different lock modes that determine how the resources can be accessed by concurrent transactions.

The following table shows the resource lock modes that the [!INCLUDE [Database Engine](../includes/ssde-md.md)] uses.

| Lock mode | Description |
| --- | --- |
| **Shared (S)** | Used for read operations that do not change or update data, such as a `SELECT` statement. |
| **Update (U)** | Used on resources that can be updated. Prevents a common form of deadlock that occurs when multiple sessions are reading, locking, and potentially updating resources later. |
| **Exclusive (X)** | Used for data-modification operations, such as `INSERT`, `UPDATE`, or `DELETE`. Ensures that multiple updates cannot be made to the same resource at the same time. |
| **Intent** | Used to establish a lock hierarchy. The types of intent locks are: intent shared (`IS`), intent exclusive (`IX`), and shared with intent exclusive (`SIX`). |
| **Schema** | Used when an operation dependent on the schema of a table is executing. The types of schema locks are: schema modification (`Sch-M`) and schema stability (`Sch-S`). |
| **Bulk Update (BU)** | Used when bulk copying data into a table with the `TABLOCK` hint. |
| **Key-range** | Protects the range of rows read by a query when using the `SERIALIZABLE` transaction isolation level. Ensures that other transactions cannot insert rows that would qualify for the queries of the `SERIALIZABLE` transaction if the queries were run again. |

### <a id="shared"></a> Shared locks

Shared (`S`) locks allow concurrent transactions to read a resource under pessimistic concurrency control. No other transactions can modify the data while shared (`S`) locks exist on the resource. Shared (`S`) locks on a resource are released as soon as the read operation completes, unless the transaction isolation level is set to `REPEATABLE READ` or higher, or a locking hint is used to retain the shared (`S`) locks for the duration of the transaction.

### <a id="update"></a> Update locks

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] places update (`U`) locks as it prepares to execute an update. `U` locks are compatible with `S` locks, but only one transaction can hold a `U` lock at a time on a given resource. This is key - many concurrent transactions can hold `S` locks, but only one transaction can hold a `U` lock on a resource. Update (`U`) locks are eventually upgraded to exclusive (`X`) locks to update a row.

Update (`U`) locks can also be taken by statements other than `UPDATE`, when the [UPDLOCK table hint](../t-sql/queries/hints-transact-sql-table.md) is specified in the statement.

- Some applications use the "select a row, then update the row" pattern, where the read and write are explicitly separated within the transaction. In this case, if the isolation level is `REPEATABLE READ` or `SERIALIZABLE`, concurrent updates might cause a deadlock, as follows: 

  A transaction reads data, acquiring a shared (`S`) lock on the resource, and then modifies the data, which requires lock conversion to an exclusive (`X`) lock. If two transactions acquire shared (`S`) locks on a resource and then attempt to update data concurrently, one transaction attempts the lock conversion to an exclusive (`X`) lock. The shared-to-exclusive lock conversion must wait because the exclusive (`X`) lock for one transaction isn't compatible with the shared (`S`) lock of the other transaction; a lock wait occurs. The second transaction attempts to acquire an exclusive (`X`) lock for its update. Because both transactions are converting to exclusive (`X`) locks, and they are each waiting for the other transaction to release its shared (`S`) lock, a deadlock occurs.

  In the default `READ COMMITTED` isolation level, `S` locks are short duration, released as soon as they are used. While the deadlock described above is still possible, it is much less likely with  short duration locks.

  To avoid this type of deadlock, applications can follow a "select a row with `UPDLOCK` hint, then update the row" pattern.

- If the `UPDLOCK` hint is used in a write when `SNAPSHOT` isolation is in use, the transaction must have access to the latest version of the row. If the latest version is no longer visible, it is possible to receive `Msg 3960, Level 16, State 2 Snapshot isolation transaction aborted due to update conflict`. For an example, see [Work with snapshot isolation](#a-work-with-snapshot-isolation).

### <a id="exclusive"></a> Exclusive locks

Exclusive (`X`) locks prevent access to a resource by concurrent transactions. With an exclusive (`X`) lock, no other transactions can modify the data protected by the lock; read operations can take place only with the use of the `NOLOCK` hint or the `READ UNCOMMITTED` isolation level.

Data modification statements, such as `INSERT`, `UPDATE`, and `DELETE` combine both read and modification operations. The statement first performs read operations to acquire data before performing the required modification operations. Data modification statements, therefore, typically request both shared locks and exclusive locks. For example, an `UPDATE` statement might modify rows in one table based on a join with another table. In this case, the `UPDATE` statement requests shared locks on the rows read in the join table in addition to requesting exclusive locks on the updated rows.

### <a id="intent"></a> Intent locks

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] uses intent locks to protect placing a shared (`S`) lock or exclusive (`X`) lock on a resource lower in the lock hierarchy. Intent locks are named "intent locks" because they're acquired before a lock at the lower level and, therefore, signal intent to place locks at a lower level.

Intent locks serve two purposes:

- To prevent other transactions from modifying the higher-level resource in a way that would invalidate the lock at the lower level.
- To improve the efficiency of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] in detecting lock conflicts at the higher level of granularity.

For example, a shared intent lock is requested at the table level before shared (`S`) locks are requested on pages or rows within that table. Setting an intent lock at the table level prevents another transaction from subsequently acquiring an exclusive (`X`) lock on the table containing that page. Intent locks improve performance because the [!INCLUDE [Database Engine](../includes/ssde-md.md)] examines intent locks only at the table level to determine if a transaction can safely acquire a lock on that table. This removes the requirement to examine every row or page lock on the table to determine if a transaction can lock the entire table.

<a name="lock_intent_table"></a> Intent locks include intent shared (`IS`), intent exclusive (`IX`), and shared with intent exclusive (`SIX`).

| Lock mode | Description |
| --- | --- |
| **Intent shared (`IS`)** | Protects requested or acquired shared locks on some (but not all) resources lower in the hierarchy. |
| **Intent exclusive (`IX`)** | Protects requested or acquired exclusive locks on some (but not all) resources lower in the hierarchy. `IX` is a superset of `IS`, and it also protects requesting shared locks on lower level resources. |
| **Shared with intent exclusive (`SIX`)** | Protects requested or acquired shared locks on all resources lower in the hierarchy and intent exclusive locks on some (but not all) of the lower level resources. Concurrent `IS` locks at the top-level resource are allowed. For example, acquiring a `SIX` lock on a table also acquires intent exclusive locks on the pages being modified and exclusive locks on the modified rows. There can be only one `SIX` lock per resource at one time, preventing updates to the resource made by other transactions, although other transactions can read resources lower in the hierarchy by obtaining `IS` locks at the table level. |
| **Intent update (`IU`)** | Protects requested or acquired update locks on all resources lower in the hierarchy. `IU` locks are used only on page resources. `IU` locks are converted to `IX` locks if an update operation takes place. |
| **Shared intent update (`SIU`)** | A combination of `S` and `IU` locks, as a result of acquiring these locks separately and simultaneously holding both locks. For example, a transaction executes a query with the `PAGLOCK` hint and then executes an update operation. The query with the `PAGLOCK` hint acquires the `S` lock, and the update operation acquires the `IU` lock. |
| **Update intent exclusive (`UIX`)** | A combination of `U` and `IX` locks, as a result of acquiring these locks separately and simultaneously holding both locks. |

### <a id="schema"></a> Schema locks

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] uses schema modification (`Sch-M`) locks during a table data definition language (DDL) operation, such as adding a column or dropping a table. During the time that it is held, the `Sch-M` lock prevents concurrent access to the table. This means the `Sch-M` lock blocks all outside operations until the lock is released.

Some data manipulation language (DML) operations, such as table truncation, use `Sch-M` locks to prevent access to affected tables by concurrent operations.

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] uses schema stability (`Sch-S`) locks when compiling and executing queries. `Sch-S` locks do not block any transactional locks, including exclusive (`X`) locks. Therefore, other transactions, including those with `X` locks on a table, continue to run while a query is being compiled. However, concurrent DDL operations, and concurrent DML operations that acquire `Sch-M` locks, are blocked by the `Sch-S` locks.

### <a id="bulk_update"></a> Bulk update locks

Bulk update (`BU`) locks allow multiple threads to bulk load data concurrently into the same table while preventing other processes that are not bulk loading data from accessing the table. The [!INCLUDE [Database Engine](../includes/ssde-md.md)] uses bulk update (`BU`) locks when both of the following conditions are true.

- You use the [!INCLUDE [tsql](../includes/tsql-md.md)] `BULK INSERT` statement, or the `OPENROWSET(BULK)` function, or you use one of the Bulk Insert API commands such as .NET `SqlBulkCopy`, OLEDB Fast Load APIs, or the ODBC Bulk Copy APIs to bulk copy data into a table.
- The `TABLOCK` hint is specified or the `table lock on bulk load` table option is set using [sp_tableoption](system-stored-procedures/sp-tableoption-transact-sql.md).

> [!TIP]  
> Unlike the BULK INSERT statement, which holds a less restrictive Bulk Update (`BU`) lock, `INSERT INTO...SELECT` with the `TABLOCK` hint holds an intent exclusive (`IX`) lock on the table. This means that you cannot insert rows using parallel insert operations.

### <a id="key_range"></a> Key-range locks

Key-range locks protect a range of rows implicitly included in a record set being read by a [!INCLUDE [tsql](../includes/tsql-md.md)] statement while using the `SERIALIZABLE` transaction isolation level. Key-range locking prevents phantom reads. By protecting the ranges of keys between rows, it also prevents phantom insertions or deletions into a record set accessed by a transaction.

## <a id="lock_compatibility"></a> Lock compatibility

Lock compatibility controls whether multiple transactions can acquire locks on the same resource at the same time. If a resource is already locked by another transaction, a new lock request can be granted only if the mode of the requested lock is compatible with the mode of the existing lock. If the mode of the requested lock isn't compatible with the existing lock, the transaction requesting the new lock waits for the existing lock to be released or for the lock timeout interval to expire. For example, no lock modes are compatible with exclusive locks. While an exclusive (`X`) lock is held, no other transaction can acquire a lock of any kind (shared, update, or exclusive) on that resource until the exclusive (`X`) lock is released. Conversely, if a shared (`S`) lock has been applied to a resource, other transactions can also acquire a shared lock or an update (`U`) lock on that resource even if the first transaction hasn't completed. However, other transactions cannot acquire an exclusive lock until the shared lock has been released.

<a name="lock_compat_table"></a> The following table shows the compatibility of the most commonly encountered lock modes.

| Existing granted mode | `IS` | `S` | `U` | `IX` | `SIX` | `X` |
| --- | --- | --- | --- | --- | --- | --- |
| **Requested mode** |
| **Intent shared (`IS`)** | Yes | Yes | Yes | Yes | Yes | No |
| **Shared (`S`)** | Yes | Yes | Yes | No | No | No |
| **Update (`U`)** | Yes | Yes | No | No | No | No |
| **Intent exclusive (`IX`)** | Yes | No | No | Yes | No | No |
| **Shared with intent exclusive (`SIX`)** | Yes | No | No | No | No | No |
| **Exclusive (`X`)** | No | No | No | No | No | No |

> [!NOTE]  
> An intent exclusive (`IX`) lock is compatible with an `IX` lock mode because `IX` means the intention is to update only some of the rows rather than all of them. Other transactions that attempt to read or update some of the rows are also permitted as long as they are not the same rows being updated by other transactions. Further, if two transactions attempt to update the same row, both transactions will be granted an `IX` lock at table and page level. However, one transaction will be granted an `X` lock at row level. The other transaction must wait until the row-level lock is removed.

<a name="lock_matrix"></a> Use the following table to determine the compatibility of all the lock modes available in the [!INCLUDE [Database Engine](../includes/ssde-md.md)].

:::image type="content" source="media/sql-server-transaction-locking-and-row-versioning-guide/sql-server-lock-conflict-compatibility.png" alt-text="A table showing a matrix of lock conflicts and compatibility." lightbox="media/sql-server-transaction-locking-and-row-versioning-guide/sql-server-lock-conflict-compatibility.png":::

## Key-range locking

Key-range locks protect a range of rows implicitly included in a record set being read by a [!INCLUDE [tsql](../includes/tsql-md.md)] statement while using the `SERIALIZABLE` transaction isolation level. The `SERIALIZABLE` isolation level requires that any query executed during a transaction must obtain the same set of rows every time it is executed during the transaction. A key range lock satisfies this requirement by preventing other transactions from inserting new rows whose keys would fall in the range of keys read by the `SERIALIZABLE` transaction.

Key-range locking prevents phantom reads. By protecting the ranges of keys between rows, it also prevents phantom insertions into a set of records accessed by a transaction.

A key-range lock is placed on an index, specifying a beginning and ending key value. This lock blocks any attempt to insert, update, or delete any row with a key value that falls in the range because those operations would first have to acquire a lock on the index. For example, a `SERIALIZABLE` transaction could issue a `SELECT` statement that reads all rows whose key values match the condition `BETWEEN 'AAA' AND 'CZZ'`. A key-range lock on the key values in the range from **'**AAA**'** to **'**CZZ**'** prevents other transactions from inserting rows with key values anywhere in that range, such as **'**ADG**'**, **'**BBD**'**, or **'**CAL**'**.

### <a id="key_range_modes"></a> Key-range lock modes

Key-range locks include both a range and a row component specified in range-row format:

- Range represents the lock mode protecting the range between two consecutive index entries.
- Row represents the lock mode protecting the index entry.
- Mode represents the combined lock mode used. Key-range lock modes consist of two parts. The first represents the type of lock used to lock the index range (Range*T*) and the second represents the lock type used to lock a specific key (*K*). The two parts are connected with a hyphen (-), such as Range*T*-*K*.

| Range | Row | Mode | Description |
| --- | --- | --- | --- |
| `RangeS` | `S` | `RangeS-S` | Shared range, shared resource lock; `SERIALIZABLE` range scan. |
| `RangeS` | `U` | `RangeS-U` | Shared range, update resource lock; `SERIALIZABLE` update scan. |
| `RangeI` | `Null` | `RangeI-N` | Insert range, null resource lock; used to test ranges before inserting a new key into an index. |
| `RangeX` | `X` | `RangeX-X` | Exclusive range, exclusive resource lock; used when updating a key in a range. |

> [!NOTE]  
> The internal `Null` lock mode is compatible with all other lock modes.

Key-range lock modes have a compatibility matrix that shows which locks are compatible with other locks obtained on overlapping keys and ranges.

| Existing granted mode | `S` | `U` | `X` | `RangeS-S` | `RangeS-U` | `RangeI-N` | `RangeX-X` |
| --- | --- | --- | --- | --- | --- | --- | --- |
|**Requested mode**  
| **Shared (`S`)** | Yes | Yes | No | Yes | Yes | Yes | No |
| **Update (`U`)** | Yes | No | No | Yes | No | Yes | No |
| **Exclusive (`X`)** | No | No | No | No | No | Yes | No |
| **`RangeS-S`** | Yes | Yes | No | Yes | Yes | No | No |
| **`RangeS-U`** | Yes | No | No | Yes | No | No | No |
| **`RangeI-N`** | Yes | Yes | Yes | No | No | Yes | No |
| **`RangeX-X`** | No | No | No | No | No | No | No |

### <a id="lock_conversion"></a> Conversion locks

Conversion locks are created when a key-range lock overlaps another lock.

| Lock 1 | Lock 2 | Conversion lock |
| --- | --- | --- |
| `S` | `RangeI-N` | `RangeI-S` |
| `U` | `RangeI-N` | `RangeI-U` |
| `X` | `RangeI-N` | `RangeI-X` |
| `RangeI-N` | `RangeS-S` | `RangeX-S` |
| `RangeI-N` | `RangeS-U` | `RangeX-U` |

Conversion locks can be observed for a short period of time under different complex circumstances, sometimes while running concurrent processes.

### Serializable range scan, singleton fetch, delete, and insert

Key-range locking ensures that the following operations are serializable:

- Range scan query
- Singleton fetch of nonexistent row
- Delete operation
- Insert operation

Before key-range locking can occur, the following conditions must be satisfied:

- The transaction-isolation level must be set to `SERIALIZABLE`.
- The query processor must use an index to implement the range filter predicate. For example, the `WHERE` clause in a `SELECT` statement could establish a range condition with this predicate: `ColumnX BETWEEN N'AAA' AND N'CZZ'`. A key-range lock can only be acquired if `ColumnX` is covered by an index key.

### Examples

The following table and index are used as a basis for the key-range locking examples that follow.

:::image type="content" source="media/sql-server-transaction-locking-and-row-versioning-guide/sql-server-btree.png" alt-text="A diagram of a sample of a Btree." lightbox="media/sql-server-transaction-locking-and-row-versioning-guide/sql-server-btree.png" :::

#### Range scan query

To ensure a range scan query is serializable, the same query should return the same results each time it is executed within the same transaction. New rows must not be inserted within the range scan query by other transactions; otherwise, these become phantom inserts. For example, the following query uses the table and index in the previous illustration:

```sql
SELECT name
FROM mytable
WHERE name BETWEEN 'A' AND 'C';
```

Key-range locks are placed on the index entries corresponding to the range of rows where the name is between the values `Adam` and `Dale`, preventing new rows qualifying in the previous query from being added or deleted. Although the first name in this range is `Adam`, the `RangeS-S` mode key-range lock on this index entry ensures that no new names beginning with the letter `A` can be added before `Adam`, such as `Abigail`. Similarly, the `RangeS-S` key-range lock on the index entry for `Dale` ensures that no new names beginning with the letter `C` can be added after `Carlos`, such as `Clive`.

> [!NOTE]  
> The number of `RangeS-S` locks held is *n*+1, where *n* is the number of rows that satisfy the query.

#### Singleton fetch of nonexistent data

If a query within a transaction attempts to select a row that doesn't exist, issuing the query at a later point within the same transaction has to return the same result. No other transaction can be allowed to insert that nonexistent row. For example, given this query:

```sql
SELECT name
FROM mytable
WHERE name = 'Bill';
```

A key-range lock is placed on the index entry corresponding to the name range from `Ben` to `Bing` because the name `Bill` would be inserted between these two adjacent index entries. The `RangeS-S` mode key-range lock is placed on the index entry `Bing`. This prevents any other transaction from inserting values, such as `Bill`, between the index entries `Ben` and `Bing`.

#### <a id="delete-operation"></a> Delete operation without optimized locking

When deleting a row within a transaction, the range the row falls into doesn't have to be locked for the duration of the transaction performing the delete operation. Locking the deleted key value until the end of the transaction is sufficient to maintain serializability. For example, given this `DELETE` statement:

```sql
DELETE mytable
WHERE name = 'Bob';
```

An exclusive (`X`) lock is placed on the index entry corresponding to the name `Bob`. Other transactions can insert or delete values before or after the row with the value `Bob` that is being deleted. However, any transaction that attempts to read, insert, or delete rows matching the value `Bob` will be blocked until the deleting transaction either commits or rolls back. (The `READ_COMMITTED_SNAPSHOT` database option and the `SNAPSHOT` isolation level also allow reads from a row-version of the previously committed state.)

Range delete can be executed using three basic lock modes: row, page, or table lock. The row, page, or table locking strategy is decided by Query Optimizer or can be specified by the user through Query Optimizer hints such as `ROWLOCK`, `PAGLOCK`, or `TABLOCK`. When `PAGLOCK` or `TABLOCK` is used, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] immediately deallocates an index page if all rows are deleted from this page. In contrast, when `ROWLOCK` is used, all deleted rows are marked only as deleted; they are removed from the index page later using a background task.

#### Delete operation with optimized locking

When deleting a row within a transaction, the row and page locks are acquired and released incrementally, and not held for the duration of the transaction. For example, given this DELETE statement:

```sql
DELETE mytable
WHERE name = 'Bob';
```

A TID lock is placed on all the modified rows for the duration of the transaction. A lock is acquired on the TID of the index rows corresponding to the value `Bob`. With optimized locking, page and row locks continue to be acquired for updates, but each page and row lock is released as soon as each row is updated. The TID lock protects the rows from being updated until the transaction is complete. Any transaction that attempts to read, insert, or delete rows with the value `Bob` will be blocked until the deleting transaction either commits or rolls back. (The `READ_COMMITTED_SNAPSHOT` database option and the `SNAPSHOT` isolation level also allow reads from a row-version of the previously committed state.)

Otherwise, the locking mechanics of a delete operation are the same as without optimized locking.

#### <a id="insert-operation"></a> Insert operation without optimized locking

When inserting a row within a transaction, the range the row falls into doesn't have to be locked for the duration of the transaction performing the insert operation. Locking the inserted key value until the end of the transaction is sufficient to maintain serializability. For example, given this INSERT statement:

```sql
INSERT mytable VALUES ('Dan');
```

The `RangeI-N` mode key-range lock is placed on the index row corresponding to the name `David` to test the range. If the lock is granted, a row with the value `Dan` is inserted and an exclusive (`X`) lock is placed on the inserted row. The `RangeI-N` mode key-range lock is necessary only to test the range and isn't held for the duration of the transaction performing the insert operation. Other transactions can insert or delete values before or after the inserted row with the value `Dan`. However, any transaction attempting to read, insert, or delete the row with the value `Dan` will be blocked until the inserting transaction either commits or rolls back.

#### Insert operation with optimized locking

When inserting a row within a transaction, the range the row falls into doesn't have to be locked for the duration of the transaction performing the insert operation. Row and page locks are rarely acquired, only when there is an online index rebuild in progress, or when there are concurrent `SERIALIZABLE` transactions. If row and page locks are acquired, they are released quickly and not held for the duration of the transaction. Placing an exclusive TID lock on the inserted key value until the end of the transaction is sufficient to maintain serializability. For example, given this `INSERT` statement:

```sql
INSERT mytable VALUES ('Dan');
```

With optimized locking, a `RangeI-N` lock is only acquired if there at least one transaction that is using the `SERIALIZABLE` isolation level in the instance. The `RangeI-N` mode key-range lock is placed on the index row corresponding to the name `David` to test the range. If the lock is granted, a row with the value `Dan` is inserted and an exclusive (`X`) lock is placed on the inserted row. The `RangeI-N` mode key-range lock is necessary only to test the range and isn't held for the duration of the transaction performing the insert operation. Other transactions can insert or delete values before or after the inserted row with the value `Dan`. However, any transaction attempting to read, insert, or delete the row with the value `Dan` will be blocked until the inserting transaction either commits or rolls back.

## Lock escalation

Lock escalation is the process of converting many fine-grained locks into fewer coarse-grain locks, reducing system overhead while increasing the probability of concurrency contention.

Lock escalation behaves differently depending on whether [optimized locking](performance/optimized-locking.md) is enabled.

## Lock escalation without optimized locking

As the [!INCLUDE [Database Engine](../includes/ssde-md.md)] acquires low-level locks, it also places intent locks on the objects that contain the lower-level objects:

- When locking rows or index key ranges, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] places an intent lock on the pages that contain the rows or keys.
- When locking pages, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] places an intent lock on the higher level objects that contain the pages. In addition to intent lock on the object, intent page locks are requested on the following objects:
  - Leaf-level pages of nonclustered indexes
  - Data pages of clustered indexes
  - Heap data pages

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] might do both row and page locking for the same statement to minimize the number of locks and reduce the likelihood that lock escalation will be necessary. For example, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] could place page locks on a nonclustered index (if enough contiguous keys in the index node are selected to satisfy the query) and row locks on the clustered index or heap.

To escalate locks, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] attempts to change the intent lock on the table to the corresponding full lock, for example, changing an intent exclusive (`IX`) lock to an exclusive (`X`) lock, or an intent shared (`IS`) lock to a shared (`S`) lock. If the lock escalation attempt succeeds and the full table lock is acquired, then all HoBT, page (`PAGE`), or row-level (`RID`,`KEY`) locks held by the transaction on the heap or index are released. If the full lock cannot be acquired, no lock escalation happens at that time and the [!INCLUDE [Database Engine](../includes/ssde-md.md)] will continue to acquire row, key, or page locks.

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] doesn't escalate row or key-range locks to page locks, but escalates them directly to table locks. Similarly, page locks are always escalated to table locks. Locking of partitioned tables can escalate to the HoBT level for the associated partition instead of to the table lock. A HoBT-level lock doesn't necessarily lock the aligned HoBTs for the partition.

> [!NOTE]  
> HoBT-level locks usually increase concurrency, but introduce the potential for deadlocks when transactions that are locking different partitions each want to expand their exclusive locks to the other partitions. In rare instances, `TABLE` locking granularity might perform better.

If a lock escalation attempt fails because of conflicting locks held by concurrent transactions, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] retries the lock escalation for each additional 1,250 locks acquired by the transaction.

Each escalation event operates primarily at the level of a single [!INCLUDE [tsql](../includes/tsql-md.md)] statement. When the event starts, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] attempts to escalate all the locks owned by the current transaction in any of the tables that have been referenced by the active statement provided it meets the escalation threshold requirements. If the escalation event starts before the statement has accessed a table, no attempt is made to escalate the locks on that table. If lock escalation succeeds, any locks acquired by the transaction in a previous statement and still held at the time the event starts will be escalated if the table is referenced by the current statement and is included in the escalation event.

For example, assume that a session performs these operations:

- Begins a transaction.
- Updates `TableA`. This generates exclusive row locks in `TableA` that are held until the transaction completes.
- Updates `TableB`. This generates exclusive row locks in `TableB` that are held until the transaction completes.
- Performs a `SELECT` that joins `TableA` with `TableC`. The query execution plan calls for the rows to be retrieved from `TableA` before the rows are retrieved from `TableC`.
- The `SELECT` statement triggers lock escalation while it is retrieving rows from `TableA` and before it has accessed `TableC`.

If lock escalation succeeds, only the locks held by the session on `TableA` are escalated. This includes both the shared locks from the `SELECT` statement and the exclusive locks from the previous `UPDATE` statement. While only the locks the session acquired in `TableA` for the `SELECT` statement are counted to determine if lock escalation should be done, once escalation is successful all locks held by the session in `TableA` are escalated to an exclusive lock on the table, and all other lower-granularity locks, including intent locks, on `TableA` are released.

No attempt is made to escalate locks on `TableB` because there was no active reference to `TableB` in the `SELECT` statement. Similarly no attempt is made to escalate the locks on `TableC`, which are not escalated because it had not yet been accessed when the escalation occurred.

## Lock escalation with optimized locking

Optimized locking helps to reduce lock memory as very few locks are held for the duration of the transaction. As the [!INCLUDE [Database Engine](../includes/ssde-md.md)] acquires row and page locks, lock escalation can occur similarly, but far less frequently. Optimized locking typically succeeds in avoiding lock escalations, lowering the number of locks and amount of lock memory necessary.

When optimized locking is enabled, and in the default `READ COMMITTED` isolation level, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] releases row and page locks as soon as the row is modified. No row and page locks are held for the duration of the transaction, except for a single Transaction ID (TID) lock. This reduces the likelihood of lock escalation.

### Lock escalation thresholds

Lock escalation is triggered when lock escalation isn't disabled on the table by using the `ALTER TABLE SET LOCK_ESCALATION` option, and when either of the following conditions exists:

- A single [!INCLUDE [tsql](../includes/tsql-md.md)] statement acquires at least 5,000 locks on a single nonpartitioned table or index.
- A single [!INCLUDE [tsql](../includes/tsql-md.md)] statement acquires at least 5,000 locks on a single partition of a partitioned table and the `ALTER TABLE SET LOCK_ESCALATION` option is set to AUTO.
- The number of locks in an instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] exceeds memory or configuration thresholds.

If locks cannot be escalated because of lock conflicts, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] periodically triggers lock escalation at every 1,250 new locks acquired.

### Escalation threshold for a Transact-SQL statement

When the [!INCLUDE [Database Engine](../includes/ssde-md.md)] checks for possible escalations at every 1,250 newly acquired locks, a lock escalation will occur if and only if a [!INCLUDE [tsql](../includes/tsql-md.md)] statement has acquired at least 5,000 locks on a single reference of a table. Lock escalation is triggered when a [!INCLUDE [tsql](../includes/tsql-md.md)] statement acquires at least 5,000 locks on a single reference of a table. For example, lock escalation isn't triggered if a statement acquires 3,000 locks in one index and 3,000 locks in another index of the same table. Similarly, lock escalation isn't triggered if a statement has a self join on a table, and each reference to the table only acquires 3,000 locks in the table.

Lock escalation only occurs for tables that have been accessed at the time the escalation is triggered. Assume that a single `SELECT` statement is a join that accesses three tables in this sequence: `TableA`, `TableB`, and `TableC`. The statement acquires 3,000 row locks in the clustered index for `TableA` and at least 5,000 row locks in the clustered index for `TableB`, but hasn't yet accessed `TableC`. When the [!INCLUDE [Database Engine](../includes/ssde-md.md)] detects that the statement has acquired at least 5,000 row locks in `TableB`, it attempts to escalate all locks held by the current transaction on `TableB`. It also attempts to escalate all locks held by the current transaction on `TableA`, but since the number of locks on `TableA` is less than 5,000, the escalation will not succeed. No lock escalation is attempted for `TableC` because it had not yet been accessed when the escalation occurred.

### Escalation threshold for an instance of the Database Engine

Whenever the number of locks is greater than the memory threshold for lock escalation, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] triggers lock escalation. The memory threshold depends on the setting of the [locks](../database-engine/configure-windows/configure-the-locks-server-configuration-option.md) configuration option:

- If the `locks` option is set to its default setting of 0, then the lock escalation threshold is reached when the memory used by lock objects is 24 percent of the memory used by the [!INCLUDE [Database Engine](../includes/ssde-md.md)], excluding AWE memory. The data structure used to represent a lock is approximately 100 bytes long. This threshold is dynamic because the [!INCLUDE [Database Engine](../includes/ssde-md.md)] dynamically acquires and frees memory to adjust for varying workloads.

- If the `locks` option is a value other than 0, then the lock escalation threshold is 40 percent (or less if there is a memory pressure) of the value of the locks option.

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] can choose any active statement from any session for escalation, and for every 1,250 new locks it will choose statements for escalation as long as the lock memory used in the instance remains above the threshold.

### <a id="escalating-mixed-lock-types"></a> Escalate mixed lock types

When lock escalation occurs, the lock selected for the heap or index is strong enough to meet the requirements of the most restrictive lower level lock.

For example, assume a session:

- Begins a transaction.
- Updates a table containing a clustered index.
- Issues a `SELECT` statement that references the same table.

The `UPDATE` statement acquires these locks:

- Exclusive (`X`) locks on the updated data rows.
- Intent exclusive (`IX`) locks on the clustered index pages containing those rows.
- An `IX` lock on the clustered index and another on the table.

The `SELECT` statement acquires these locks:

- Shared (`S`) locks on all data rows it reads, unless the row is already protected by an `X` lock from the `UPDATE` statement.
- Intent Shared (`IS`) locks on all clustered index pages containing those rows, unless the page is already protected by an `IX` lock.
- No lock on the clustered index or table because they are already protected by `IX` locks.

If the `SELECT` statement acquires enough locks to trigger lock escalation and the escalation succeeds, the `IX` lock on the table is converted to an `X` lock, and all the row, page, and index locks are released. Both the updates and reads are protected by the `X` lock on the table.

### <a id="reducing-locking-and-escalation"></a> Reduce locking and escalation

In most cases, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] delivers the best performance when operating with its default settings for locking and lock escalation.

- Take advantage of [optimized locking](performance/optimized-locking.md).

  - [Optimized locking](performance/optimized-locking.md) offers an improved transaction locking mechanism that reduces lock memory consumption and blocking for concurrent transactions. Lock escalation is far less likely to ever occur when optimized locking is enabled.
  - Avoid using [table hints with optimized locking](performance/optimized-locking.md#avoid-locking-hints). Table hints may reduce the effectiveness of optimized locking.
  - Enable the [READ_COMMITTED_SNAPSHOT](../t-sql/statements/alter-database-transact-sql-set-options.md#read_committed_snapshot--on--off-) option for the database for the most benefit from optimized locking. This is the default in [!INCLUDE [Azure SQL Database](../includes/ssazure-sqldb.md)].
  - Optimized locking requires [accelerated database recovery (ADR)](/azure/azure-sql/accelerated-database-recovery) to be enabled on the database.

If an instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] generates a lot of locks and is seeing frequent lock escalations, consider reducing the amount of locking with the following strategies:

- Use an isolation level that doesn't generate shared locks for read operations:

  - `READ COMMITTED` isolation level when the `READ_COMMITTED_SNAPSHOT` database option is `ON`.
  - `SNAPSHOT` isolation level.
  - `READ UNCOMMITTED` isolation level. This can only be used for systems that can operate with dirty reads.

- Use the `PAGLOCK` or `TABLOCK` table hints to have the [!INCLUDE [Database Engine](../includes/ssde-md.md)] use page, heap, or index locks instead of low-level locks. Using this option, however, increases the problems of users blocking other users attempting to access the same data and should not be used in systems with more than a few concurrent users.

- If optimized locking isn't available, for partitioned tables, use the `LOCK_ESCALATION` option of [ALTER TABLE](../t-sql/statements/alter-table-transact-sql.md) to escalate locks to the partition instead of the table, or to disable lock escalation for a table.

- Break up large batch operations into several smaller operations. For example, suppose you ran the following query to remove several hundred thousand old rows from an audit table, and then you found that it caused a lock escalation that blocked other users:

    ```sql
    DELETE FROM LogMessages
    WHERE LogDate < '2024-09-26'
    ```

    By removing these rows a few hundred at a time, you can dramatically reduce the number of locks that accumulate per transaction and prevent lock escalation. For example:

    ```sql
    DECLARE @DeletedRows int;

    WHILE @DeletedRows IS NULL OR @DeletedRows > 0
    BEGIN
        DELETE TOP (500)
        FROM LogMessages
        WHERE LogDate < '2024-09-26'

        SELECT @DeletedRows = @@ROWCOUNT;
    END;
    ```

- Reduce a query lock footprint by making the query as efficient as possible. Large scans or large numbers of key lookups may increase the chance of lock escalation; additionally, that increases the chance of deadlocks, and generally adversely affects concurrency and performance. After you find the query that causes lock escalation, look for opportunities to create new indexes or to add columns to an existing index to remove full index or table scans and to maximize the efficiency of index seeks. Consider using the [Database Engine Tuning Advisor](performance/start-and-use-the-database-engine-tuning-advisor.md) to perform an automatic index analysis on the query. For more information, see [Tutorial: [!INCLUDE [Database Engine](../includes/ssde-md.md)] Tuning Advisor](../tools/dta/tutorial-database-engine-tuning-advisor.md).
    One goal of this optimization is to make index seeks return as few rows as possible to minimize the cost of key lookups (maximize the selectivity of the index for the particular query). If the [!INCLUDE [Database Engine](../includes/ssde-md.md)] estimates that a key lookup logical operator may return many rows, it may use a prefetch optimization to perform the lookup. If the [!INCLUDE [Database Engine](../includes/ssde-md.md)] does use prefetch for a lookup, it must increase the transaction isolation level of a portion of the query to `REPEATABLE READ`. This means that what may look similar to a `SELECT` statement at a `READ COMMITTED` isolation level may acquire many thousands of key locks (on both the clustered index and one nonclustered index), which can cause such a query to exceed the lock escalation thresholds. This is especially important if you find that the escalated lock is a shared table lock, which, however, isn't commonly seen at the default `READ COMMITTED` isolation level.

    If a key lookup with the prefetch optimization is causing lock escalation, consider adding additional columns to the nonclustered index that appears in the Index Seek or the Index Scan logical operator below the key lookup logical operator in the query plan. It may be possible to create a covering index (an index that includes all columns in a table that were used in the query), or at least an index that covers the columns that were used for join criteria or in the `WHERE` clause if including everything in the `SELECT` column list is impractical.
    A Nested Loop join may also use the prefetch optimization, and this causes the same locking behavior.

- Lock escalation cannot occur if a different SPID is currently holding an incompatible table lock. Lock escalation always escalates to a table lock, and never to page locks. Additionally, if a lock escalation attempt fails because another SPID holds an incompatible table lock, the query that attempted escalation doesn't block while waiting for a table lock. Instead, it continues to acquire locks at its original, more granular level (row, key, or page), periodically making additional escalation attempts. Therefore, one method to prevent lock escalation on a particular table is to acquire and hold a lock on a different connection that isn't compatible with the escalated lock type. An intent exclusive (`IX`) lock at the table level doesn't lock any rows or pages, but it is still not compatible with an escalated shared (`S`) or exclusive (`X`) table lock. For example, assume that you must run a batch job that modifies a large number of rows in the `mytable` table and that has caused blocking that occurs because of lock escalation. If this job always completes in less than an hour, you might create a [!INCLUDE [tsql](../includes/tsql-md.md)] job that contains the following code, and schedule the new job to start several minutes before the batch job's start time:

    ```sql
    BEGIN TRAN;

    SELECT *
    FROM mytable WITH (UPDLOCK, HOLDLOCK)
    WHERE 1=0;
    
    WAITFOR DELAY '1:00:00';

    COMMIT TRAN;
    ```

    This query acquires and holds an `IX` lock on `mytable` for one hour, which prevents lock escalation on the table during that time. This batch doesn't modify any data or block other queries (unless the other query forces a table lock with the `TABLOCK` hint or if an administrator has disabled page or row locks on an index on `mytable`).

- You can also use trace flags 1211 and 1224 to disable all or some lock escalations. However, these [trace flags](../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) disable all lock escalation globally for the entire [!INCLUDE [Database Engine](../includes/ssde-md.md)] instance. Lock escalation serves a very useful purpose in the [!INCLUDE [Database Engine](../includes/ssde-md.md)] by maximizing the efficiency of queries that are otherwise slowed down by the overhead of acquiring and releasing several thousands of locks. Lock escalation also helps minimize the required memory to keep track of locks. The memory that the [!INCLUDE [Database Engine](../includes/ssde-md.md)] can dynamically allocate for lock structures is finite, so if you disable lock escalation and the lock memory grows large enough, attempts to allocate additional locks for any query may fail and the following error occurs: `Error: 1204, Severity: 19, State: 1 The SQL Server cannot obtain a LOCK resource at this time. Rerun your statement when there are fewer active users or ask the system administrator to check the SQL Server lock and memory configuration.`

    > [!NOTE]  
    > When the [MSSQLSERVER_1204](errors-events/mssqlserver-1204-database-engine-error.md) error occurs, it stops the processing of the current statement and causes a rollback of the active transaction. The rollback itself may block users or lead to a long database recovery time if you restart the database service.

    > [!NOTE]  
    > Using a lock hint such as `ROWLOCK` only alters the initial lock acquisition. Lock hints do not prevent lock escalation.

Starting with [!INCLUDE [sql2008-md](../includes/sql2008-md.md)], the behavior of lock escalation has changed with the introduction of the `LOCK_ESCALATION` table option. For more information, see the `LOCK_ESCALATION` option of [ALTER TABLE](../t-sql/statements/alter-table-transact-sql.md).

### <a id="monitor-for-lock-escalation"></a> Monitor lock escalation

Monitor lock escalation by using the `lock_escalation` extended event, such as in the following example:

```sql
-- Session creates a histogram of the number of lock escalations per database
CREATE EVENT SESSION [Track_lock_escalation] ON SERVER
ADD EVENT sqlserver.lock_escalation
    (
    SET collect_database_name=1,collect_statement=1
    ACTION(sqlserver.database_id,sqlserver.database_name,sqlserver.query_hash_signed,sqlserver.query_plan_hash_signed,sqlserver.sql_text,sqlserver.username)
    )
ADD TARGET package0.histogram
    (
    SET source=N'sqlserver.database_id'
    )
GO
```

## <a id="dynamic_locks"></a> Dynamic locking

Using low-level locks, such as row locks, increases concurrency by decreasing the probability that two transactions will request locks on the same piece of data at the same time. Using low-level locks also increases the number of locks and the resources needed to manage them. Using high-level table or page locks lowers overhead, but at the expense of lowering concurrency.

:::image type="content" source="media/sql-server-transaction-locking-and-row-versioning-guide/sql-server-locking-cost-vs-concurrency-cost.png" alt-text="A graph of locking cost vs. concurrency cost." lightbox="media/sql-server-transaction-locking-and-row-versioning-guide/sql-server-locking-cost-vs-concurrency-cost.png":::

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] uses a dynamic locking strategy to determine the most effective locks. The [!INCLUDE [Database Engine](../includes/ssde-md.md)] automatically determines what locks are most appropriate when the query is executed, based on the characteristics of the schema and query. For example, to reduce the overhead of locking, the optimizer may choose page locks in an index when performing an index scan.

Starting with [!INCLUDE [sql2008-md](../includes/sql2008-md.md)], the behavior of lock escalation has changed with the introduction of the `LOCK_ESCALATION` table option. For more information, see the `LOCK_ESCALATION` option of [ALTER TABLE](../t-sql/statements/alter-table-transact-sql.md).

## <a id="lock_partitioning"></a> Lock partitioning

For large computer systems, locks on frequently referenced objects can become a performance bottleneck as acquiring and releasing locks place contention on internal locking resources. Lock partitioning enhances locking performance by splitting a single lock resource into multiple lock resources. This feature is only available for systems with 16 or more logical CPUs, and is automatically enabled and cannot be disabled. Only object locks can be partitioned. Object locks that have a subtype are not partitioned. For more information, see [sys.dm_tran_locks (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).

### <a id="understanding-lock-partitioning"></a> Understand lock partitioning

Locking tasks access several shared resources, two of which are optimized by lock partitioning:

- **Spinlock**

  This controls access to a lock resource, such as a row or a table.

  Without lock partitioning, one spinlock manages all lock requests for a single lock resource. On systems that experience a large volume of activity, contention can occur as lock requests wait for the spinlock to become available. Under this situation, acquiring locks can become a bottleneck and can negatively impact performance.

  To reduce contention on a single lock resource, lock partitioning splits a single lock resource into multiple lock resources to distribute the load across multiple spinlocks.

- **Memory**

  This is used to store the lock resource structures.

  Once the spinlock is acquired, lock structures are stored in memory and then accessed and possibly modified. Distributing lock access across multiple resources helps to eliminate the need to transfer memory blocks between CPUs, which will help to improve performance.

### <a id="implementing-and-monitoring-lock-partitioning"></a> Implement and monitor lock partitioning

Lock partitioning is turned on by default for systems with 16 or more CPUs. When lock partitioning is enabled, an informational message is recorded in the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] error log.

When acquiring locks on a partitioned resource:

- Only `NL`, `Sch-S`, `IS`, `IU`, and `IX` lock modes are acquired on a single partition.

- Shared (`S`), exclusive (`X`), and other locks in modes other than `NL`, `Sch-S`, `IS`, `IU`, and `IX` must be acquired on all partitions starting with partition ID 0 and following in partition ID order. These locks on a partitioned resource will use more memory than locks in the same mode on a non-partitioned resource since each partition is effectively a separate lock. The memory increase is determined by the number of partitions. The [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] lock performance counters will display information about memory used by partitioned and non-partitioned locks.

A transaction is assigned to a partition when the transaction starts. For the transaction, all lock requests that can be partitioned use the partition assigned to that transaction. By this method, access to lock resources of the same object by different transactions is distributed across different partitions.

The `resource_lock_partition` column in the `sys.dm_tran_locks` Dynamic Management View provides the lock partition ID for a lock partitioned resource. For more information, see [sys.dm_tran_locks (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md).

### <a id="working-with-lock-partitioning"></a> Work with lock partitioning

The following code examples illustrate lock partitioning. In the examples, two transactions are executed in two different sessions in order to show lock partitioning behavior on a computer system with 16 CPUs.

These [!INCLUDE [tsql](../includes/tsql-md.md)] statements create test objects that are used in the examples that follow.

```sql
-- Create a test table.
CREATE TABLE TestTable
(
col1 int
);
GO

-- Create a clustered index on the table.
CREATE CLUSTERED INDEX ci_TestTable ON TestTable (col1);
GO

-- Populate the table.
INSERT INTO TestTable
VALUES (1);
GO
```

#### Example A

Session 1:

A `SELECT` statement is executed under a transaction. Because of the `HOLDLOCK` lock hint, this statement will acquire and retain an Intent shared (`IS`) lock on the table (for this illustration, row and page locks are ignored). The `IS` lock will be acquired only on the partition assigned to the transaction. For this example, it is assumed that the `IS` lock is acquired on partition ID 7.

```sql
-- Start a transaction.
BEGIN TRANSACTION;

-- This SELECT statement will acquire an IS lock on the table.
SELECT col1
FROM TestTable
WITH (HOLDLOCK);
```

Session 2:

A transaction is started, and the `SELECT` statement running under this transaction will acquire and retain a shared (`S`) lock on the table. The `S` lock will be acquired on all partitions, which results in multiple table locks, one for each partition. For example, on a 16-CPU system, 16 `S` locks will be issued across lock partition IDs 0-15. Because the `S` lock is compatible with the `IS` lock being held on partition ID 7 by the transaction in session 1, there is no blocking between transactions.

```sql
BEGIN TRANSACTION;

SELECT col1
FROM TestTable
WITH (TABLOCK, HOLDLOCK);
```

Session 1:

The following `SELECT` statement is executed under the transaction that is still active under session 1. Because of the exclusive (`X`) table lock hint, the transaction will attempt to acquire an `X` lock on the table. However, the `S` lock that is being held by the transaction in session 2 will block the `X` lock at partition ID 0.

```sql
SELECT col1
FROM TestTable
WITH (TABLOCKX);
```

#### Example B

Session 1:

A `SELECT` statement is executed under a transaction. Because of the `HOLDLOCK` lock hint, this statement will acquire and retain an Intent shared (`IS`) lock on the table (for this illustration, row and page locks are ignored). The `IS` lock will be acquired only on the partition assigned to the transaction. For this example, it is assumed that the `IS` lock is acquired on partition ID 6.

```sql
-- Start a transaction.
BEGIN TRANSACTION;

-- This SELECT statement will acquire an IS lock on the table.
SELECT col1
FROM TestTable
WITH (HOLDLOCK);
```

Session 2:

A `SELECT` statement is executed under a transaction. Because of the `TABLOCKX` lock hint, the transaction tries to acquire an exclusive (`X`) lock on the table. Remember that the `X` lock must be acquired on all partitions starting with partition ID 0. The `X` lock will be acquired on all partitions IDs 0-5 but will be blocked by the `IS` lock that is acquired on partition ID 6.

On partition IDs 7-15 that the `X` lock hasn't yet reached, other transactions can continue to acquire locks.

```sql
BEGIN TRANSACTION;

SELECT col1
FROM TestTable
WITH (TABLOCKX, HOLDLOCK);
```

## <a id="Row_versioning"></a> Row versioning-based isolation levels in the Database Engine

Starting with [!INCLUDE [ssVersion2005](../includes/ssversion2005-md.md)], the [!INCLUDE [Database Engine](../includes/ssde-md.md)] offers an implementation of an existing transaction isolation level, `READ COMMITTED`, that provides a statement level snapshot using row versioning. [!INCLUDE [Database Engine](../includes/ssde-md.md)] also offers a transaction isolation level, `SNAPSHOT`, that provides a transaction level snapshot also using row versioning.

Row versioning is a general framework in [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] that invokes a copy-on-write mechanism when a row is modified or deleted. This requires that while the transaction is running, the old version of the row must be available for transactions that require an earlier transactionally consistent state. Row versioning is used to implement the following features:

- Build the `inserted` and `deleted` tables in triggers. Any rows modified by the trigger are versioned. This includes the rows modified by the statement that launched the trigger, as well as any data modifications made by the trigger.
- Support Multiple Active Result Sets (MARS). If a MARS session issues a data modification statement (such as `INSERT`, `UPDATE`, or `DELETE`) at a time there is an active result set, the rows affected by the modification statement are versioned.
- Support index operations that specify the `ONLINE` option.
- Support row versioning-based transaction isolation levels:
  - A new implementation of the `READ COMMITTED` isolation level that uses row versioning to provide statement-level read consistency.
  - A new isolation level, `SNAPSHOT`, to provide transaction-level read consistency.

Row versions are stored in a version store. If [Accelerated Database Recovery](accelerated-database-recovery-concepts.md) is enabled on a database, the version store is created in that database. Otherwise, the version store is created in the `tempdb` database.

The database must have enough space for the version store. When the version store is in `tempdb`, and the `tempdb` database is full, update operations will stop generating versions but will continue to succeed, but read operations might fail because a particular row version that is needed does not exists. This affects operations like triggers, MARS, and online indexing.

When Accelerated Database Recovery is used and the version store is full, read operations continue to succeed but write operations that generate versions, such as `UPDATE` and `DELETE` fail. `INSERT` operations continue to succeed if the database has sufficient space.

Using row versioning for `READ COMMITTED` and `SNAPSHOT` transactions is a two-step process:

1. Set either or both the `READ_COMMITTED_SNAPSHOT` and `ALLOW_SNAPSHOT_ISOLATION` database options to `ON`.
1. Set the appropriate transaction isolation level in an application:

    - When the `READ_COMMITTED_SNAPSHOT` database option is `ON`, transactions setting the `READ COMMITTED` isolation level use row versioning.
    - When the `ALLOW_SNAPSHOT_ISOLATION` database option is `ON`, transactions can set the `SNAPSHOT` isolation level.

When either `READ_COMMITTED_SNAPSHOT` or `ALLOW_SNAPSHOT_ISOLATION` database option is set to `ON`, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] assigns a transaction sequence number (XSN) to each transaction that manipulates data using row versioning. Transactions start at the time a `BEGIN TRANSACTION` statement is executed. However, the transaction sequence number starts with the first read or write operation after the `BEGIN TRANSACTION` statement. The transaction sequence number is incremented by one each time it is assigned.

When either the `READ_COMMITTED_SNAPSHOT` or `ALLOW_SNAPSHOT_ISOLATION` database options are set to `ON`, logical copies (versions) are maintained for all data modifications performed in the database. Every time a row is modified by a specific transaction, the instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] stores a version of the previously committed image of the row in the version store. Each version is marked with the transaction sequence number of the transaction that made the change. The versions of modified rows are chained using a link list. The newest row value is always stored in the current database and chained to the versioned rows in the version store.

> [!NOTE]  
> For modification of large objects (LOBs), only the changed fragment is copied to the version store.

Row versions are held long enough to satisfy the requirements of transactions running under row versioning-based isolation levels. The [!INCLUDE [Database Engine](../includes/ssde-md.md)] tracks the earliest useful transaction sequence number and periodically deletes all row versions stamped with transaction sequence numbers that are lower than the earliest useful sequence number.

When both database options are set to `OFF`, only rows modified by triggers or MARS sessions, or read by online index operations, are versioned. Those row versions are released when no longer needed. A background process removes stale row versions.

> [!NOTE]  
> For short-running transactions, a version of a modified row may get cached in the buffer pool without getting written to the version store. If the need for the versioned row is short-lived, it will simply get dropped from the buffer pool and may not necessarily incur I/O overhead.

### Behavior when reading data

When transactions running under row versioning-based isolation read data, the read operations do not acquire shared (`S`) locks on the data being read, and therefore do not block transactions that are modifying data. Also, the overhead of locking resources is minimized as the number of locks acquired is reduced. `READ COMMITTED` isolation using row versioning and `SNAPSHOT` isolation are designed to provide statement-level or transaction-level read consistency of versioned data.

All queries, including transactions running under row versioning-based isolation levels, acquire schema stability (`Sch-S`) locks during compilation and execution. Because of this, queries are blocked when a concurrent transaction holds a schema modification (`Sch-M`) lock on the table. For example, a data definition language (DDL) operation acquires a `Sch-M` lock before it modifies the schema information of the table. Ttransactions, including those running under a row versioning-based isolation level, are blocked when attempting to acquire a `Sch-S` lock. Conversely, a query holding a `Sch-S` lock blocks a concurrent transaction that attempts to acquire a `Sch-M` lock.

When a transaction using the `SNAPSHOT` isolation level starts, the instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] records all of the currently active transactions. When the `SNAPSHOT` transaction reads a row that has a version chain, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] follows the chain and retrieves the row where the transaction sequence number is:

- Closest to but lower than the sequence number of the snapshot transaction reading the row.

- Not in the list of the transactions active when the snapshot transaction started.

Read operations performed by a `SNAPSHOT` transaction retrieve the last version of each row that had been committed at the time the `SNAPSHOT` transaction started. This provides a transactionally consistent snapshot of the data as it existed at the start of the transaction.

`READ COMMITTED` transactions using row versioning operate in much the same way. The difference is that the `READ COMMITTED` transaction doesn't use its own transaction sequence number when choosing row versions. Each time a statement is started, the `READ COMMITTED` transaction reads the latest transaction sequence number issued for that instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)]. This is the transaction sequence number used to select the row versions for that statement. This allows `READ COMMITTED` transactions to see a snapshot of the data as it exists at the start of each statement.

> [!NOTE]  
> Even though `READ COMMITTED` transactions using row versioning provides a transactionally consistent view of the data at a statement level, row versions generated or accessed by this type of transaction are maintained until the transaction completes.

### Behavior when modifying data

The behavior of data writes is significantly different with and without optimized locking enabled.

#### Modify data without optimized locking

In a `READ COMMITTED` transaction using row versioning, the selection of rows to update is done using a blocking scan where an update (`U`) lock is acquired on the data row as data values are read. This is the same as a `READ COMMITTED` transaction that doesn't use row versioning. If the data row doesn't meet the update criteria, the update lock is released on that row and the next row is locked and scanned.

Transactions running under `SNAPSHOT` isolation take an optimistic approach to data modification by acquiring locks on data before performing the modification only to enforce constraints. Otherwise, locks are not acquired on data until the data is to be modified. When a data row meets the update criteria, the `SNAPSHOT` transaction verifies that the data row hasn't been modified by a concurrent transaction that committed after the `SNAPSHOT` transaction began. If the data row has been modified outside of the `SNAPSHOT` transaction, an update conflict occurs and the `SNAPSHOT` transaction is terminated. The update conflict is handled by the [!INCLUDE [Database Engine](../includes/ssde-md.md)] and there is no way to disable the update conflict detection.

> [!NOTE]  
> Update operations running under `SNAPSHOT` isolation internally execute under `READ COMMITTED` isolation when the `SNAPSHOT` transaction accesses any of the following:
>
> A table with a FOREIGN KEY constraint.
>
> A table that is referenced in the FOREIGN KEY constraint of another table.
>
> An indexed view referencing more than one table.
>
> However, even under these conditions the update operation will continue to verify that the data has not been modified by another transaction. If data has been modified by another transaction, the `SNAPSHOT` transaction encounters an update conflict and is terminated. Update conflicts must be handled and retried by the application.

#### Modify data with optimized locking

With optimized locking enabled and with the `READ_COMMITTED_SNAPSHOT` (RCSI) database option enabled, and using the default `READ COMMITTED` isolation level, readers don't acquire any locks, and writers acquire short duration low-level locks, instead of locks that expire at the end of the transaction.

Enabling RCSI is recommended for most efficiency with optimized locking. When using stricter isolation levels such as `REPEATABLE READ` or `SERIALIZABLE`, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] holds row and page locks until the end of the transaction, for both readers and writers, resulting in increased blocking and lock memory.

With RCSI enabled, and when using the default `READ COMMITTED` isolation level, writers qualify rows per the predicate based on the latest committed version of the row, without acquiring `U` locks. A query will wait only if the row qualifies and there is another active write transaction on that row or page. Qualifying based on the latest committed version and locking only the qualified rows reduces blocking and increases concurrency.

If update conflicts are detected with RCSI and in the default `READ COMMITTED` isolation level, they are handled and retried automatically without any impact to customer workloads.

With optimized locking enabled and when using the `SNAPSHOT` isolation level, the behavior of update conflicts is the same as without optimized locking. Update conflicts must be handled and retried by the application.

> [!NOTE]  
> For more information on behavior changes with the lock after qualifiation (LAQ) feature of optimized locking, see [Query behavior changes with optimized locking and RCSI](performance/optimized-locking.md#behavior).

### Behavior in summary

The following table summarizes the differences between `SNAPSHOT` isolation and `READ COMMITTED` isolation using row versioning.

| Property | `READ COMMITTED` isolation level using row versioning | `SNAPSHOT` isolation level |
| --- | --- | --- |
| The database option that must be set to `ON` to enable the required support. | `READ_COMMITTED_SNAPSHOT` | `ALLOW_SNAPSHOT_ISOLATION` |
| How a session requests the specific type of row versioning. | Use the default `READ COMMITTED` isolation level, or run the `SET TRANSACTION ISOLATION LEVEL` statement to specify the `READ COMMITTED` isolation level. This can be done after the transaction starts. | Requires the execution of `SET TRANSACTION ISOLATION LEVEL` to specify the `SNAPSHOT` isolation level before the start of the transaction. |
| The version of data read by statements. | All data that was committed before the start of each statement. | All data that was committed before the start of each transaction. |
| How updates are handled. | **Without optimized locking:** Reverts from row versions to actual data to select rows to update and uses update locks on the data rows selected. Acquires exclusive locks on actual data rows to be modified. No update conflict detection.<br /><br />**With optimized locking:** Rows are selected based on the last committed version without any locks being acquired. If rows qualify for the update, exclusive row or page locks are acquired. If update conflicts are detected, they are handled and retried automatically. | Uses row versions to select rows to update. Tries to acquire an exclusive lock on the actual data row to be modified, and if the data has been modified by another transaction, an update conflict occurs and the snapshot transaction is terminated. |
| Update conflict detection | **Without optimized locking:** None.<br /><br />**With optimized locking:** If update conflicts are detected, they are handled and retried automatically. | Integrated support. Cannot be disabled. |

### Row versioning resource usage

The row versioning framework supports the following [!INCLUDE [Database Engine](../includes/ssde-md.md)] features:

- Triggers
- Multiple Active Results Sets (MARS)
- Online indexing

The row versioning framework also supports the following row versioning-based transaction isolation levels:

- When the `READ_COMMITTED_SNAPSHOT` database option is set to `ON`, `READ_COMMITTED` transactions provide statement-level read consistency using row versioning.
- When the `ALLOW_SNAPSHOT_ISOLATION` database option is set to `ON`, `SNAPSHOT` transactions provide transaction-level read consistency using row versioning.

Row versioning-based isolation levels reduce the number of locks acquired by transaction by eliminating the use of shared locks on read operations. This increases system performance by reducing the resources used to manage locks. Performance is also increased by reducing the number of times a transaction is blocked by locks acquired by other transactions.

Row versioning-based isolation levels increase the resources needed by data modifications. Enabling these options causes all data modifications for the database to be versioned. A copy of the data before modification is stored in the version store even when there are no active transactions using row versioning-based isolation. The data after modification includes a pointer to the versioned data in the version store. For large objects, only part of the object that changed is stored in the version store.

#### Space used in tempdb

For each instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)], the version store must have enough space to hold the row versions. The database administrator must ensure that `tempdb` and other databases (if Accelerated Database Recovery is enabled) have ample space to support the version store. There are two types of version stores:

- The online index build version store is used for online index builds.
- The common version store is used for all other data modification operations.

Row versions must be stored for as long as an active transaction needs to access them. Periodically, a background thread removes row versions that are no longer needed and frees up space in the version store. A long-running transaction prevents space in the version store from being released if it meets any of the following conditions:

- It uses row versioning-based isolation.
- It uses triggers, MARS, or online index build operations.
- It generates row versions.

> [!NOTE]  
> When a trigger is invoked inside a transaction, the row versions created by the trigger are maintained until the end of the transaction, even though the row versions are no longer needed after the trigger completes. This also applies to `READ COMMITTED` transactions that use row versioning. With this type of transaction, a transactionally consistent view of the database is needed only for each statement in the transaction. This means that the row versions created for a statement in the transaction are no longer needed after the statement completes. However, row versions created by each statement in the transaction are maintained until the transaction completes.

If the version store is in `tempdb`, and `tempdb` runs out of space, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] forces the version stores to shrink. During the shrink process, the longest running transactions that have not yet generated row versions are marked as victims. A message 3967 is generated in the error log for each victim transaction. If a transaction is marked as a victim, it can no longer read the row versions in the version store. When it attempts to read row versions, message 3966 is generated and the transaction is rolled back. If the shrinking process succeeds, space becomes available in `tempdb`. Otherwise, `tempdb` runs out of space and the following occurs:

- Write operations continue to execute but do not generate versions. An information message (3959) appears in the error log, but the transaction that writes data isn't affected.

- Transactions that attempt to access row versions that were not generated because of a `tempdb` full roll back and terminate with an error 3958.

#### Space used in data rows

Each database row may use up to 14 bytes at the end of the row for row versioning information. The row versioning information contains the transaction sequence number of the transaction that committed the version and the pointer to the versioned row. These 14 bytes are added the first time the row is modified, or when a new row is inserted, under any of these conditions:

- `READ_COMMITTED_SNAPSHOT` or `ALLOW_SNAPSHOT_ISOLATION` options are set to `ON`.
- The table has a trigger.
- Multiple Active Results Sets (MARS) is being used.
- Online index build operations are currently running on the table.

If the version store is in `tempdb`, these 14 bytes are removed from the database row the first time the row is modified under all of these conditions:

- `READ_COMMITTED_SNAPSHOT` and `ALLOW_SNAPSHOT_ISOLATION` options are set to `OFF`.
- The trigger no longer exists on the table.
- MARS isn't being used.
- Online index build operations are not currently running.

The 14 bytes are also removed when a row is modified if Accelerated Database Recovery is no longer enabled and the above conditions are satisfied.

If you use any of the row versioning features, you might need to allocate additional disk space for the database to accommodate the 14 bytes per database row. Adding the row versioning information can cause index page splits or the allocation of a new data page if there isn't enough space available on the current page. For example, if the average row length is 100 bytes, the additional 14 bytes cause an existing table to grow up to 14 percent.

Decreasing the [fill factor](indexes/specify-fill-factor-for-an-index.md) might help to prevent or decrease fragmentation of index pages. To view current page density information for the data and indexes of a table or view, you can use [sys.dm_db_index_physical_stats](../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md).

#### Space used in large objects

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] supports several data types that can hold large strings up to 2 gigabytes (GB) in length, such as: `nvarchar(max)`, `varchar(max)`, `varbinary(max)`, `ntext`, `text`, and `image`. Large data stored using these data types are stored in a series of data fragments that are linked to the data row. Row versioning information is stored in each fragment used to store these large strings. Data fragments are stored in a set of pages dedicated to large objects in a table.

As new large values are added to a database, they are allocated using a maximum of 8040 bytes of data per fragment. Earlier versions of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] stored up to 8080 bytes of `ntext`, `text`, or `image` data per fragment.

Existing `ntext`, `text`, and `image` large object (LOB) data isn't updated to make space for the row versioning information when a database is upgraded to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] from an earlier version of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. However, the first time the LOB data is modified, it is dynamically upgraded to enable storage of versioning information. This will happen even if row versions are not generated. After the LOB data is upgraded, the maximum number of bytes stored per fragment is reduced from 8080 bytes to 8040 bytes. The upgrade process is equivalent to deleting the LOB value and reinserting the same value. The LOB data is upgraded even if only 1 byte is modified. This is a one-time operation for each `ntext`, `text`, or `image` column, but each operation may generate a large amount of page allocations and I/O activity depending upon the size of the LOB data. It may also generate a large amount of logging activity if the modification is fully logged. `WRITETEXT` and `UPDATETEXT` operations are minimally logged if the database recovery model isn't set to FULL.

Enough disk space should be allocated to accommodate this requirement.

#### <a id="monitoring-row-versioning-and-the-version-store"></a> Monitor row versioning and the version store

For monitoring row versioning, version store, and snapshot isolation processes for performance and problems, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] provides tools in the form of Dynamic Management Views (DMVs) and performance counters.

##### DMVs

The following DMVs provide information about the current system state of `tempdb` and the version store, as well as transactions using row versioning.

- `sys.dm_db_file_space_usage`. Returns space usage information for each file in the database. For more information, see [sys.dm_db_file_space_usage (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-db-file-space-usage-transact-sql.md).

- `sys.dm_db_session_space_usage`. Returns page allocation and deallocation activity by session for the database. For more information, see [sys.dm_db_session_space_usage (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-db-session-space-usage-transact-sql.md).

- `sys.dm_db_task_space_usage`. Returns page allocation and deallocation activity by task for the database. For more information, see [sys.dm_db_task_space_usage (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-db-task-space-usage-transact-sql.md).

- `sys.dm_tran_top_version_generators`. Returns a virtual table for the objects producing the most versions in the version store. It groups the top 256 aggregated record lengths by database_id and rowset_id. Use this function to find the largest consumers of the version store. Applies to the version store in `tempdb` only. For more information, see [sys.dm_tran_top_version_generators (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-tran-top-version-generators-transact-sql.md).

- `sys.dm_tran_version_store`. Returns a virtual table that displays all version records in the common version store. Applies to the version store in `tempdb` only. For more information, see [sys.dm_tran_version_store (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-tran-version-store-transact-sql.md).

- `sys.dm_tran_version_store_space_usage`. Returns a virtual table that displays the total space in `tempdb` used by version store records for each database. Applies to the version store in `tempdb` only. For more information, see [sys.dm_tran_version_store_space_usage (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-tran-version-store-space-usage.md).

    > [!NOTE]  
    > The system objects `sys.dm_tran_top_version_generators` and `sys.dm_tran_version_store` are potentially very expensive to run, since both query the entire version store, which could be large.
    > `sys.dm_tran_version_store_space_usage` is efficient and not expensive to run because it does not navigate through individual version store records, and instead returns aggregated version store space consumed in `tempdb` per database.

- `sys.dm_tran_active_snapshot_database_transactions`. Returns a virtual table for all active transactions in all databases within the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instance that use row versioning. System transactions do not appear in this DMV. For more information, see [sys.dm_tran_active_snapshot_database_transactions (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-tran-active-snapshot-database-transactions-transact-sql.md).

- `sys.dm_tran_transactions_snapshot`. Returns a virtual table that displays snapshots taken by each transaction. The snapshot contains the sequence number of the active transactions that use row versioning. For more information, see [sys.dm_tran_transactions_snapshot (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-tran-transactions-snapshot-transact-sql.md).

- `sys.dm_tran_current_transaction`. Returns a single row that displays row versioning-related state information of the transaction in the current session. For more information, see [sys.dm_tran_current_transaction (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-tran-current-transaction-transact-sql.md).

- `sys.dm_tran_current_snapshot`. Returns a virtual table that displays all active transactions at the time the current snapshot isolation transaction starts. If the current transaction is using snapshot isolation, this function returns no rows. The DMV `sys.dm_tran_current_snapshot` is similar to `sys.dm_tran_transactions_snapshot`, except that it returns only the active transactions for the current snapshot. For more information, see [sys.dm_tran_current_snapshot (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-tran-current-snapshot-transact-sql.md).

- `sys.dm_tran_persistent_version_store_stats`. Returns statistics for the persistent version store in each database used when Accelerated Database Recovery is enabled. For more information, see [sys.dm_tran_persistent_version_store_stats (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-tran-persistent-version-store-stats.md).

##### Performance counters

The following performance counters monitor the version store in `tempdb`, as well as transactions using row versioning. The performance counters are contained in the `SQLServer:Transactions` performance object.

- **Free Space in tempdb (KB)**. Monitors the amount, in kilobytes (KB), of free space in the `tempdb` database. There must be enough free space in `tempdb` to handle the version store that supports snapshot isolation.

    The following formula provides a rough estimate of the size of the version store. For long-running transactions, it may be useful to monitor the generation and cleanup rate to estimate the maximum size of the version store.

    [size of common version store] = 2 * [version store data generated per minute] \* [longest running time (minutes) of the transaction]

    The longest running time of transactions should not include online index builds. Because these operations may take a long time on very large tables, online index builds use a separate version store. The approximate size of the online index build version store equals the amount of data modified in the table, including all indexes, while the online index build is active.

- **Version Store Size (KB)**. Monitors the size in KB of all version stores in `tempdb`. This information helps determine the amount of space needed in the `tempdb` database for the version store. Monitoring this counter over a period of time provides a useful estimate of additional space needed for `tempdb`.

- **Version Generation rate (KB/s)**. Monitors the version generation rate in KB per second in all version stores in `tempdb`.

- **Version Cleanup rate (KB/s)**. Monitors the version cleanup rate in KB per second in all version stores in `tempdb`.

    > [!NOTE]  
    > Information from Version Generation rate (KB/s) and Version Cleanup rate (KB/s) can be used to predict `tempdb` space requirements.

- **Version Store unit count**. Monitors the count of version store units.

- **Version Store unit creation**. Monitors the total number of version store units created to store row versions since the instance was started.

- **Version Store unit truncation**. Monitors the total number of version store units truncated since the instance was started. A version store unit is truncated when [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] determines that none of the version rows stored in the version store unit are needed to run active transactions.

- **Update conflict ratio**. Monitors the ratio of update snapshot transactions that have update conflicts to the total number of update snapshot transactions.

- **Longest Transaction Running Time**. Monitors the longest running time in seconds of any transaction using row versioning. This can be used to determine if any transaction is running for an unexpected amount of time.

- **Transactions**. Monitors the total number of active transactions. This doesn't include system transactions.

- **Snapshot Transactions**. Monitors the total number of active snapshot transactions.

- **Update Snapshot Transactions**. Monitors the total number of active snapshot transactions that perform update operations.

- **NonSnapshot Version Transactions**. Monitors the total number of active non-snapshot transactions that generate version records.

    > [!NOTE]  
    > The sum of Update Snapshot Transactions and NonSnapshot Version Transactions represents the total number of transactions that participate in version generation. The difference of Snapshot Transactions and Update Snapshot Transactions represents the number of read-only snapshot transactions.

### Row versioning-based isolation level example

The following examples show the differences in behavior between `SNAPSHOT` isolation transactions and `READ COMMITTED` transactions that use row versioning.

#### A. Work with SNAPSHOT isolation

In this example, a transaction running under `SNAPSHOT` isolation reads data that is then modified by another transaction. The `SNAPSHOT` transaction doesn't block the update operation executed by the other transaction, and it continues to read data from the versioned row, ignoring the data modification. However, when the `SNAPSHOT` transaction attempts to modify the data that has already been modified by the other transaction, the `SNAPSHOT` transaction generates an error and is terminated.

On session 1:

```sql
USE AdventureWorks2022;
GO

-- Enable snapshot isolation on the database.
ALTER DATABASE AdventureWorks2022 SET ALLOW_SNAPSHOT_ISOLATION ON;
GO

-- Start a snapshot transaction
SET TRANSACTION ISOLATION LEVEL SNAPSHOT;
GO

BEGIN TRANSACTION;

-- This SELECT statement will return
-- 48 vacation hours for the employee.
SELECT BusinessEntityID, VacationHours
FROM HumanResources.Employee
WHERE BusinessEntityID = 4;
```

On session 2:

```sql
USE AdventureWorks2022;
GO

-- Start a transaction.
BEGIN TRANSACTION;

-- Subtract a vacation day from employee 4.
-- Update is not blocked by session 1 since
-- under snapshot isolation shared locks are
-- not requested.
UPDATE HumanResources.Employee
SET VacationHours = VacationHours - 8
WHERE BusinessEntityID = 4;

-- Verify that the employee now has 40 vacation hours.
SELECT VacationHours
FROM HumanResources.Employee
WHERE BusinessEntityID = 4;
```

On session 1:

```sql
-- Reissue the SELECT statement - this shows
-- the employee having 48 vacation hours. The
-- snapshot transaction is still reading data from
-- the older, versioned row.
SELECT BusinessEntityID, VacationHours
FROM HumanResources.Employee
WHERE BusinessEntityID = 4;
```

On session 2:

```sql
-- Commit the transaction; this commits the data
-- modification.
COMMIT TRANSACTION;
GO
```

On session 1:

```sql
-- Reissue the SELECT statement - this still
-- shows the employee having 48 vacation hours
-- even after the other transaction has committed
-- the data modification.
SELECT BusinessEntityID, VacationHours
FROM HumanResources.Employee
WHERE BusinessEntityID = 4;

-- Because the data has been modified outside of the
-- snapshot transaction, any further data changes to
-- that data by the snapshot transaction will cause
-- the snapshot transaction to fail. This statement
-- will generate a 3960 error and the transaction will
-- terminate.
UPDATE HumanResources.Employee
    SET SickLeaveHours = SickLeaveHours - 8
    WHERE BusinessEntityID = 4;

-- Undo the changes to the database from session 1.
-- This will not undo the change from session 2.
ROLLBACK TRANSACTION;
GO
```

#### B. Work with READ COMMITTED isolation using row versioning

In this example, a `READ COMMITTED` transaction using row versioning runs concurrently with another transaction. The `READ COMMITTED` transaction behaves differently than a `SNAPSHOT` transaction. Like a `SNAPSHOT` transaction, the `READ COMMITTED` transaction will read versioned rows even after the other transaction has modified data. However, unlike a `SNAPSHOT` transaction, the `READ COMMITTED` transaction:

- Reads the modified data after the other transaction commits the data changes.
- Is able to update the data modified by the other transaction where the `SNAPSHOT` transaction could not.

On session 1:

```sql
USE AdventureWorks2022;
GO

-- Enable READ_COMMITTED_SNAPSHOT on the database.
-- For this statement to succeed, this session
-- must be the only connection to the AdventureWorks2022
-- database.
ALTER DATABASE AdventureWorks2022 SET READ_COMMITTED_SNAPSHOT ON;
GO

-- Start a read-committed transaction
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO

BEGIN TRANSACTION;

-- This SELECT statement will return
-- 48 vacation hours for the employee.
SELECT BusinessEntityID, VacationHours
FROM HumanResources.Employee
WHERE BusinessEntityID = 4;
```

On session 2:

```sql
USE AdventureWorks2022;
GO

-- Start a transaction.
BEGIN TRANSACTION;

-- Subtract a vacation day from employee 4.
-- Update is not blocked by session 1 since
-- under read-committed using row versioning shared locks are
-- not requested.
UPDATE HumanResources.Employee
    SET VacationHours = VacationHours - 8
    WHERE BusinessEntityID = 4;

-- Verify that the employee now has 40 vacation hours.
SELECT VacationHours
    FROM HumanResources.Employee
    WHERE BusinessEntityID = 4;
```

On session 1:

```sql
-- Reissue the SELECT statement - this still shows
-- the employee having 48 vacation hours. The
-- read-committed transaction is still reading data
-- from the versioned row and the other transaction
-- has not committed the data changes yet.
SELECT BusinessEntityID, VacationHours
FROM HumanResources.Employee
WHERE BusinessEntityID = 4;
```

On session 2:

```sql
-- Commit the transaction.
COMMIT TRANSACTION;
GO
```

On session 1:

```sql
-- Reissue the SELECT statement which now shows the
-- employee having 40 vacation hours. Being
-- read-committed, this transaction is reading the
-- committed data. This is different from snapshot
-- isolation which reads from the versioned row.
SELECT BusinessEntityID, VacationHours
FROM HumanResources.Employee
WHERE BusinessEntityID = 4;

-- This statement, which caused the snapshot transaction
-- to fail, will succeed with read-committed using row versioning.
UPDATE HumanResources.Employee
SET SickLeaveHours = SickLeaveHours - 8
WHERE BusinessEntityID = 4;

-- Undo the changes to the database from session 1.
-- This will not undo the change from session 2.
ROLLBACK TRANSACTION;
GO
```

### <a id="enabling-row-versioning-based-isolation-levels"></a> Enable row versioning-based isolation levels

Database administrators control the database-level settings for row versioning by using the `READ_COMMITTED_SNAPSHOT` and `ALLOW_SNAPSHOT_ISOLATION` database options in the `ALTER DATABASE` statement.

When the `READ_COMMITTED_SNAPSHOT` database option is set to `ON`, the mechanisms used to support the option are activated immediately. When setting the `READ_COMMITTED_SNAPSHOT` option, only the connection executing the `ALTER DATABASE` command is allowed in the database. There must be no other open connection in the database until `ALTER DATABASE` is complete. The database doesn't have to be in single-user mode.

The following [!INCLUDE [tsql](../includes/tsql-md.md)] statement enables `READ_COMMITTED_SNAPSHOT`:

```sql
ALTER DATABASE AdventureWorks2022 SET READ_COMMITTED_SNAPSHOT ON;
```

When the `ALLOW_SNAPSHOT_ISOLATION` database option is set to `ON`, the instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] doesn't start generating row versions for modified data until all active transactions that have modified data in the database complete. If there are active modification transactions, the [!INCLUDE [Database Engine](../includes/ssde-md.md)] sets the state of the option to `PENDING_ON`. After all of the modification transactions complete, the state of the option is changed to `ON`. Users cannot start a `SNAPSHOT` transaction in the database until the option is `ON`. Similarly, the database passes through a `PENDING_OFF` state when the database administrator sets the `ALLOW_SNAPSHOT_ISOLATION` option to `OFF`.

The following [!INCLUDE [tsql](../includes/tsql-md.md)] statement will enable `ALLOW_SNAPSHOT_ISOLATION`:

```sql
ALTER DATABASE AdventureWorks2022 SET ALLOW_SNAPSHOT_ISOLATION ON;
```

The following table lists and describes the states of the `ALLOW_SNAPSHOT_ISOLATION` option. Using `ALTER DATABASE` with the `ALLOW_SNAPSHOT_ISOLATION` option doesn't block users who are currently accessing the database data.

| State of SNAPSHOT isolation for current database | Description |
| --- | --- |
| `OFF` | The support for `SNAPSHOT` isolation transactions isn't activated. No `SNAPSHOT` isolation transactions are allowed. |
| `PENDING_ON` | The support for `SNAPSHOT` isolation transactions is in transition state (from `OFF` to `ON`). Open transactions must complete.<br /><br />No `SNAPSHOT` isolation transactions are allowed. |
| `ON` | The support for `SNAPSHOT` isolation transactions is activated.<br /><br />`SNAPSHOT` transactions are allowed. |
| `PENDING_OFF` | The support for `SNAPSHOT` isolation transactions is in transition state (from `ON` to `OFF`).<br /><br />`SNAPSHOT` transactions started after this time cannot access this database. Existing `SNAPSHOT` transactions can still access this database. Existing write transactions still use versioning in this database. The state `PENDING_OFF` doesn't become `OFF` until all `SNAPSHOT` transactions that started when the database `SNAPSHOT` isolation state was `ON` finish. |

Use the `sys.databases` catalog view to determine the state of both row versioning database options.

All updates to user tables and some system tables stored in `master` and `msdb` generate row versions.

The `ALLOW_SNAPSHOT_ISOLATION` option is automatically set to `ON` in the `master` and `msdb` databases, and cannot be disabled.

Users cannot set the `READ_COMMITTED_SNAPSHOT` option to `ON` in `master`, `tempdb`, or `msdb`.

### <a id="using-row-versioning-based-isolation-levels"></a> Use row versioning-based isolation levels

The row versioning framework is always enabled and is used by multiple features. Besides providing row versioning-based isolation levels, it is used to support modifications made in triggers and multiple active result sets (MARS) sessions, and to support data reads for online index operations.

Row versioning-based isolation levels are enabled at the database level. Any application accessing objects from enabled databases can run queries using the following isolation levels:

- `READ COMMITTED` that uses row versioning by setting the `READ_COMMITTED_SNAPSHOT` database option to `ON` as shown in the following code example:

    ```sql
    ALTER DATABASE AdventureWorks2022 SET READ_COMMITTED_SNAPSHOT ON;
    ```

     When the database is enabled for `READ_COMMITTED_SNAPSHOT`, all queries running under the `READ COMMITTED` isolation level use row versioning, which means that read operations do not block update operations.

- `SNAPSHOT` isolation by setting the `ALLOW_SNAPSHOT_ISOLATION` database option to `ON` as shown in the following code example:

    ```sql
    ALTER DATABASE AdventureWorks2022 SET ALLOW_SNAPSHOT_ISOLATION ON;
    ```

     When using cross-database queries, a transaction running under `SNAPSHOT` isolation can access tables in the database(s) that have the `ALLOW_SNAPSHOT_ISOLATION` database option set to `ON`. To access tables in databases that don't have the `ALLOW_SNAPSHOT_ISOLATION` database option set to `ON`, the isolation level must be changed. For example, the following code example shows a `SELECT` statement that joins two tables while running under a `SNAPSHOT` transaction. One table belongs to a database in which `SNAPSHOT` isolation isn't enabled. When the `SELECT` statement runs under `SNAPSHOT` isolation, it fails to execute successfully.

    ```sql
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT;

    BEGIN TRANSACTION;

    SELECT t1.col5, t2.col5
    FROM Table1 as t1
    INNER JOIN SecondDB.dbo.Table2 as t2
    ON t1.col1 = t2.col2;
    ```

     The following code example shows the same `SELECT` statement that has been modified to change the transaction isolation level to `READ COMMITTED` when accessing a specific table. Because of this change, the `SELECT` statement executes successfully.

    ```sql
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT;

    BEGIN TRANSACTION;

    SELECT t1.col5, t2.col5
    FROM Table1 as t1 WITH (READCOMMITTED)
    INNER JOIN SecondDB.dbo.Table2 as t2
    ON t1.col1 = t2.col2;
    ```

#### Limitations of transactions using row versioning-based isolation levels

Consider the following limitations when working with row versioning-based isolation levels:

- `READ_COMMITTED_SNAPSHOT` cannot be enabled in `tempdb`, `msdb`, or `master`.
- Global temp tables are stored in `tempdb`. When accessing global temp tables inside a `SNAPSHOT` transaction, one of the following must happen:
  - Set the `ALLOW_SNAPSHOT_ISOLATION` database option to `ON` in `tempdb`.
  - Use an isolation hint to change the isolation level for the statement.
- `SNAPSHOT` transactions fail when:
  - A database is made read-only after the `SNAPSHOT` transaction starts, but before the `SNAPSHOT` transaction accesses the database.
  - If accessing objects from multiple databases, a database state was changed in such a way that database recovery occurred after a `SNAPSHOT` transaction starts, but before the `SNAPSHOT` transaction accesses the database. For example: the database was set to `OFFLINE` and then to `ONLINE`, database was automatically closed and reopened due to the `AUTO_CLOSE` option set to `ON`, or database was detached and reattached.
- Distributed transactions, including queries in distributed partitioned databases, are not supported under `SNAPSHOT` isolation.
- The [!INCLUDE [Database Engine](../includes/ssde-md.md)] doesn't keep multiple versions of system metadata. Data definition language (DDL) statements on tables and other database objects (indexes, views, data types, stored procedures, and common language runtime functions) change metadata. If a DDL statement modifies an object, any concurrent reference to the object under `SNAPSHOT` isolation causes the `SNAPSHOT` transaction to fail. `READ COMMITTED` transactions do not have this limitation when the `READ_COMMITTED_SNAPSHOT` database option is set to `ON`.

     For example, a database administrator executes the following `ALTER INDEX` statement.

    ```sql
    USE AdventureWorks2022;
    GO
    ALTER INDEX AK_Employee_LoginID ON HumanResources.Employee REBUILD;
    GO
    ```

     Any snapshot transaction that is active when the `ALTER INDEX` statement is executed receives an error if it attempts to reference the `HumanResources.Employee` table after the `ALTER INDEX` statement is executed. `READ COMMITTED` transactions using row versioning are not affected.

    > [!NOTE]  
    > `BULK INSERT` operations may cause changes to target table metadata (for example, when disabling constraint checks). When this happens, concurrent `SNAPSHOT` isolation transactions accessing bulk inserted tables fail.

## <a id="customizing-locking-and-row-versioning"></a> Customize locking and row versioning

### <a id="customizing-the-lock-time-out"></a> Customize the lock time-out

When an instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] cannot grant a lock to a transaction because another transaction already owns a conflicting lock on the resource, the first transaction becomes blocked waiting for the existing lock to be released. By default, there is no time-out period for lock waits, therefore a transaction could potentially get blocked indefinitely.

> [!NOTE]  
> Use the `sys.dm_os_waiting_tasks` dynamic management view to determine whether a task is being blocked and what is blocking it. For more information and examples, see [Understand and resolve SQL Server blocking problems](/troubleshoot/sql/database-engine/performance/understand-resolve-blocking).

The `LOCK_TIMEOUT` setting allows an application to set a maximum time that a statement waits on a blocked resource. When a statement has waited longer than the `LOCK_TIMEOUT` setting, the blocked statement is canceled automatically, and error message 1222 (`Lock request time-out period exceeded`) is returned. Any transaction containing the statement, however, isn't rolled back. Therefore, the application must have an error handler that can trap error message 1222. If an application doesn't trap the error, the application can proceed unaware that an individual statement within a transaction has been canceled but the transaction remains active. Errors can occur because statements later in the transaction might depend on the statement that was never executed.

Implementing an error handler that traps error message 1222 allows an application to handle the time-out situation and take remedial action, such as: automatically resubmitting the statement that was blocked or rolling back the entire transaction.

> [!IMPORTANT]
> Applications that use explicit transactions and require the transaction to terminate upon receiving error 1222 must explicitly roll back the transaction as part of error handling. Without this, other statements can unintentionally execute on the same session while the transaction remains active, leading to unbounded transaction log growth and data loss if the transaction is rolled back later.

To determine the current `LOCK_TIMEOUT` setting, execute the `@@LOCK_TIMEOUT` function:

```sql
SELECT @@LOCK_TIMEOUT;
GO
```

### <a id="customizing-transaction-isolation-level"></a> Customize transaction isolation level

`READ COMMITTED` is the default isolation level for the [!INCLUDE [Database Engine](../includes/ssde-md.md)]. If an application must operate at a different isolation level, it can use the following methods to set the isolation level:

- Run the [SET TRANSACTION ISOLATION LEVEL](../t-sql/statements/set-transaction-isolation-level-transact-sql.md) statement.
- ADO.NET applications that use the `System.Data.SqlClient` namespace can specify an `IsolationLevel` option by using the `SqlConnection.BeginTransaction` method.
- Applications that use ADO can set the `Autocommit Isolation Levels` property.
- When starting a transaction, applications using OLE DB can call `ITransactionLocal::StartTransaction` with `isoLevel` set to the desired transaction isolation level. When specifying the isolation level in autocommit mode, applications that use OLE DB can set the `DBPROPSET_SESSION` property `DBPROP_SESS_AUTOCOMMITISOLEVELS` to the desired transaction isolation level.
- Applications that use ODBC can set the `SQL_COPT_SS_TXN_ISOLATION` attribute by using `SQLSetConnectAttr`.

When the isolation level is specified, the locking behavior for all queries and data manipulation language (DML) statements in the session operates at that isolation level. The isolation level remains in effect until the session terminates or until the isolation level is set to another level.

The following example sets the `SERIALIZABLE` isolation level:

```sql
USE AdventureWorks2022;
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
GO
BEGIN TRANSACTION;

SELECT BusinessEntityID
FROM HumanResources.Employee;

COMMIT;
GO
```

The isolation level can be overridden for individual query or DML statements, if necessary, by specifying a table-level hint. Specifying a table-level hint doesn't affect other statements in the session.

To determine the transaction isolation level currently set, use the `DBCC USEROPTIONS` statement as shown in the following example. The result set may vary from the result set on your system.

```sql
USE AdventureWorks2022;
GO
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ;
GO
DBCC USEROPTIONS;
GO
```

[!INCLUDE [ssResult](../includes/ssresult-md.md)]

```
Set Option                   Value
---------------------------- -------------------------------------------
textsize                     2147483647
language                     us_english
dateformat                   mdy
datefirst                    7
...                          ...
Isolation level              repeatable read

(14 row(s) affected)

DBCC execution completed. If DBCC printed error messages, contact your system administrator.
```

### Locking hints

Locking hints can be specified for individual table references in the `SELECT`, `INSERT`, `UPDATE`, `DELETE` and `MERGE` statements. The hints specify the type of locking or row versioning the instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] uses for the table data. Table-level locking hints can be used when a finer control of the types of locks acquired on an object is required. These locking hints override the current transaction isolation level for the session.

> [!NOTE]  
> Locking hints are not recommended for use when optimized locking is enabled. While table and query hints are honored, they reduce the benefit of optimized locking. For more information, see [Avoid locking hints with optimized locking](performance/optimized-locking.md#avoid-locking-hints).

For more information about the specific locking hints and their behaviors, see [Table Hints (Transact-SQL)](../t-sql/queries/hints-transact-sql-table.md).

> [!NOTE]  
> We recommend that table-level locking hints be used to change the default locking behavior only when necessary. Forcing a locking level can adversely affect concurrency.

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] might have to acquire locks when reading metadata, even when processing a statement with a locking hint that prevents requests for shared locks when reading data. For example, a `SELECT` statement running under the `READ UNCOMMITTED` isolation level or using the `NOLOCK` hint doesn't acquire share locks when reading data, but might sometime request locks when reading a system catalog view. This means it is possible for a such a `SELECT` statement to be blocked when a concurrent transaction is modifying the metadata of the table.

As shown in the following example, if the transaction isolation level is set to `SERIALIZABLE`, and the table-level locking hint `NOLOCK` is used with the `SELECT` statement, key-range locks typically used to maintain `SERIALIZABLE` transactions are not acquired.

```sql
USE AdventureWorks2022;
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
GO
BEGIN TRANSACTION;
GO
SELECT JobTitle
FROM HumanResources.Employee WITH (NOLOCK);
GO

-- Get information about the locks held by
-- the transaction.
SELECT resource_type,
       resource_subtype,
       request_mode
FROM sys.dm_tran_locks
WHERE request_session_id = @@SPID;

-- End the transaction.
ROLLBACK;
GO
```

The only lock acquired that references `HumanResources.Employee` is a schema stability (`Sch-S`) lock. In this case, serializability is no longer guaranteed.

The `LOCK_ESCALATION` option of `ALTER TABLE` avoids table locks during lock escalation, and enables HoBT (partition) locks on partitioned tables. This option isn't a locking hint, and can be used to reduce [lock escalation](#lock-escalation). For more information, see [ALTER TABLE (Transact-SQL)](../t-sql/statements/alter-table-transact-sql.md).

### Customize locking for an index

The [!INCLUDE [Database Engine](../includes/ssde-md.md)] uses a dynamic locking strategy that automatically chooses the best locking granularity for queries in most cases. We recommend that you do not override the default locking levels, unless table or index access patterns are well understood and consistent, and there is a resource contention problem to solve. Overriding a locking level can significantly impede concurrent access to a table or index. For example, specifying only table-level locks on a large table that users access heavily can cause bottlenecks because users must wait for the table-level lock to be released before accessing the table.

There are a few cases where disallowing page or row locking can be beneficial, if the access patterns are well understood and consistent. For example, a database application uses a lookup table that is updated weekly in a batch process. Concurrent readers access the table with a shared (`S`) lock and the weekly batch update accesses the table with an exclusive (`X`) lock. Turning off page and row locking on the table reduces the locking overhead throughout the week by allowing readers to concurrently access the table through shared table locks. When the batch job runs, it can complete the update efficiently because it obtains an exclusive table lock.

Turning off page and row locking might or might not be acceptable because the weekly batch update will block the concurrent readers from accessing the table while the update runs. If the batch job only changes a few rows or pages, you can change the locking level to allow row or page level locking, which will enable other sessions to read from the table without blocking. If the batch job has a large number of updates, obtaining an exclusive lock on the table may be the best way to ensure the batch job runs efficiently.

In some workloads, a type of deadlock might occurs when two concurrent operations acquire row locks on the same table and then block each other because they both need to lock the page. Disallowing row locks forces one of the operations to wait, avoiding the deadlock. For more about deadlocks, see the [Deadlocks guide](sql-server-deadlocks-guide.md).

The granularity of locking used on an index can be set using the `CREATE INDEX` and `ALTER INDEX` statements. In addition, the `CREATE TABLE` and `ALTER TABLE` statements can be used to set locking granularity on `PRIMARY KEY` and `UNIQUE` constraints. For backward compatibility, the `sp_indexoption` system stored procedure can also set the granularity. To display the current locking option for a given index, use the `INDEXPROPERTY` function. Page-level locks, row-level locks, or both page-level and row-level locks can be disallowed for a given index.

| Disallowed locks | Index accessed by |
| --- | --- |
| Page level | Row-level and table-level locks |
| Row level | Page-level and table-level locks |
| Page level and row level | Table-level locks |

## <a id="Advanced"></a> Advanced transaction information

### <a id="nesting-transactions"></a> Nest transactions

Explicit transactions can be nested. This is primarily intended to support transactions in stored procedures that can be called either from a process already in a transaction or from processes that have no active transaction.

The following example shows the use of nested transactions. If `TransProc` is called when a transaction is active, the outcome of the nested transaction in `TransProc` is controlled by the outer transaction, and its `INSERT` statements are committed or rolled back based on the commit or roll back of the outer transaction. If `TransProc` is executed by a process that doesn't have an outstanding transaction, the `COMMIT TRANSACTION` at the end of the procedure commits the `INSERT` statements.

```sql
SET QUOTED_IDENTIFIER OFF;
GO
SET NOCOUNT OFF;
GO

CREATE TABLE TestTrans
(
ColA INT PRIMARY KEY,
ColB CHAR(3) NOT NULL
);
GO

CREATE PROCEDURE TransProc
  @PriKey INT,
  @CharCol CHAR(3)
AS

BEGIN TRANSACTION InProc;

INSERT INTO TestTrans VALUES (@PriKey, @CharCol);
INSERT INTO TestTrans VALUES (@PriKey + 1, @CharCol);

COMMIT TRANSACTION InProc;
GO

/* Start a transaction and execute TransProc. */
BEGIN TRANSACTION OutOfProc;
GO
EXEC TransProc 1, 'aaa';
GO

/* Roll back the outer transaction, this will
   roll back TransProc's nested transaction. */
ROLLBACK TRANSACTION OutOfProc;
GO

EXECUTE TransProc 3,'bbb';
GO

/* The following SELECT statement shows only rows 3 and 4 are
   still in the table. This indicates that the commit
   of the inner transaction from the first EXECUTE statement of
   TransProc was overridden by the subsequent roll back of the
   outer transaction. */
SELECT *
FROM TestTrans;
GO
```

Committing inner transactions is ignored by the [!INCLUDE [Database Engine](../includes/ssde-md.md)] when an outer transaction is active. The transaction is either committed or rolled back based on the commit or roll back at the end of the outermost transaction. If the outer transaction is committed, the inner nested transactions are also committed. If the outer transaction is rolled back, then all inner transactions are also rolled back, regardless of whether or not the inner transactions were individually committed.

Each call to `COMMIT TRANSACTION` or `COMMIT WORK` applies to the last executed `BEGIN TRANSACTION`. If the `BEGIN TRANSACTION` statements are nested, then a `COMMIT` statement applies only to the last nested transaction, which is the innermost transaction. Even if a `COMMIT TRANSACTION transaction_name` statement within a nested transaction refers to the transaction name of the outer transaction, the commit applies only to the innermost transaction.

It isn't allowed for the `transaction_name` parameter of a `ROLLBACK TRANSACTION` statement to refer to the inner transaction in a set of named nested transactions. `transaction_name` can refer only to the transaction name of the outermost transaction. If a `ROLLBACK TRANSACTION transaction_name` statement using the name of the outer transaction is executed at any level of a set of nested transactions, all of the nested transactions are rolled back. If a `ROLLBACK WORK` or `ROLLBACK TRANSACTION` statement without a `transaction_name` parameter is executed at any level of a set of nested transaction, it rolls back all of the nested transactions, including the outermost transaction.

The `@@TRANCOUNT` function records the current transaction nesting level. Each `BEGIN TRANSACTION` statement increments `@@TRANCOUNT` by one. Each `COMMIT TRANSACTION` or `COMMIT WORK` statement decrements `@@TRANCOUNT` by one. A `ROLLBACK WORK` or a `ROLLBACK TRANSACTION` statement that doesn't have a transaction name rolls back all nested transactions and decrements `@@TRANCOUNT` to 0. A `ROLLBACK TRANSACTION` that uses the transaction name of the outermost transaction in a set of nested transactions rolls back all of the nested transactions and decrements `@@TRANCOUNT` to 0. To determine if you are already in a transaction, `SELECT @@TRANCOUNT` to see if it is 1 or more. If `@@TRANCOUNT` is 0, you are not in a transaction.

### <a id="using-bound-sessions"></a> Use bound sessions

Bound sessions ease the coordination of actions across multiple sessions on the same server. Bound sessions allow two or more sessions to share the same transaction and locks, and can work on the same data without lock conflicts. Bound sessions can be created from multiple sessions within the same application or from multiple applications with separate sessions.

To participate in a bound session, a session calls [sp_getbindtoken](system-stored-procedures/sp-getbindtoken-transact-sql.md) or [srv_getbindtoken](extended-stored-procedures-reference/srv-getbindtoken-extended-stored-procedure-api.md) (through Open Data Services) to get a bind token. A bind token is a character string that uniquely identifies each bound transaction. The bind token is then sent to the other sessions to be bound with the current session. The other sessions bind to the transaction by calling `sp_bindsession`, using the bind token received from the first session.

> [!NOTE]  
> A session must have an active user transaction in order for `sp_getbindtoken` or `srv_getbindtoken` to succeed.

Bind tokens must be transmitted from the application code that makes the first session to the application code that subsequently binds their sessions to the first session. There is no [!INCLUDE [tsql](../includes/tsql-md.md)] statement or API function that an application can use to get the bind token for a transaction started by another process. Some of the methods that can be used to transmit a bind token include the following:

- If the sessions are all initiated from the same application process, bind tokens can be stored in global memory or passed into functions as a parameter.

- If the sessions are made from separate application processes, bind tokens can be transmitted using interprocess communication (IPC), such as a remote procedure call (RPC) or dynamic data exchange (DDE).

- Bind tokens can be stored in a table in an instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)] that can be read by processes wanting to bind to the first session.

Only one session in a set of bound sessions can be active at any time. If one session is executing a statement on the instance or has results pending from the instance, no other session bound to the same token can access the instance until the current session finishes processing or cancels the current statement. If the instance is busy processing a statement from another of the bound sessions, an error occurs indicating that the transaction space is in use and the session should retry later.

When you bind sessions, each session retains its isolation level setting. Using `SET TRANSACTION ISOLATION LEVEL` to change the isolation level setting of one session doesn't affect the setting of any other session bound to the same token.

#### Types of bound sessions

The two types of bound sessions are local and distributed.

- **Local bound session**
    Allows bound sessions to share the transaction space of a single transaction in a single instance of the [!INCLUDE [Database Engine](../includes/ssde-md.md)].

- **Distributed bound session**
    Allows bound sessions to share the same transaction across two or more instances until the entire transaction is either committed or rolled back by using [!INCLUDE [msCoName](../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC).

Distributed bound sessions are not identified by a character string bind token; they are identified by distributed transaction identification numbers. If a bound session is involved in a local transaction and executes an RPC on a remote server with `SET REMOTE_PROC_TRANSACTIONS ON`, the local bound transaction is automatically promoted to a distributed bound transaction by MS DTC and an MS DTC session is started.

#### When to use bound sessions

In earlier versions of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], bound sessions were primarily used in developing extended stored procedures that must execute [!INCLUDE [tsql](../includes/tsql-md.md)] statements on behalf of the process that calls them. Having the calling process pass in a bind token as one parameter of the extended stored procedure allows the procedure to join the transaction space of the calling process, thereby integrating the extended stored procedure with the calling process.

In the [!INCLUDE [Database Engine](../includes/ssde-md.md)], stored procedures written using CLR are more secure, scalable, and stable than extended stored procedures. CLR-stored procedures use the `SqlContext` object to join the context of the calling session, not `sp_bindsession`.

Bound sessions can be used to develop three-tier applications in which business logic is incorporated into separate programs that work cooperatively on a single business transaction. These programs must be coded to carefully coordinate their access to a database. Because the two sessions share the same locks, the two programs must not try to modify the same data at the same time. At any point in time, only one session can be doing work as part of the transaction; there can be no parallel execution. The transaction can only be switched between sessions at well-defined yield points, such as when all DML statements have completed and their results have been retrieved.

### <a id="coding-efficient-transactions"></a> Code efficient transactions

It is important to keep transactions as short as possible. When a transaction is started, a database management system (DBMS) must hold many resources until the end of the transaction to protect the atomicity, consistency, isolation, and durability (ACID) properties of the transaction. If data is modified, the modified rows must be protected with exclusive locks that prevent any other transaction from reading the rows, and exclusive locks must be held until the transaction is committed or rolled back. Depending on transaction isolation level settings, `SELECT` statements may acquire locks that must be held until the transaction is committed or rolled back. Especially in systems with many users, transactions must be kept as short as possible to reduce locking contention for resources between concurrent connections. Long-running, inefficient transactions might not be a problem with small numbers of users, but they are highly problematic in a system with thousands of users. Beginning with [!INCLUDE [ssSQL14](../includes/sssql14-md.md)], the [!INCLUDE [Database Engine](../includes/ssde-md.md)] supports delayed durable transactions. Delayed durable transactions might improve scalability and performance, but they do not guarantee durability. For more information, see [Control Transaction Durability](logs/control-transaction-durability.md).

#### <a id="guidelines"></a> Code guidelines

These are the guidelines for coding efficient transactions:

- Do not require input from users during a transaction.
    Get all required input from users before a transaction is started. If additional user input is required during a transaction, roll back the current transaction and restart the transaction after the user input is supplied. Even if users respond immediately, human reaction times are vastly slower than computer speeds. All resources held by the transaction are held for an extremely long time, which has the potential to cause blocking problems. If users do not respond, the transaction remains active, locking critical resources until they respond, which may not happen for several minutes or even hours.

- Do not open a transaction while browsing through data, if at all possible.
    Transactions should not be started until all preliminary data analysis has been completed.

- Keep the transaction as short as possible.
    After you know the modifications that have to be made, start a transaction, execute the modification statements, and then immediately commit or roll back. Do not open the transaction before it is required.

- To reduce blocking, consider using a row versioning-based isolation level for read-only queries.

- Make intelligent use of lower transaction isolation levels.
    Many applications can be coded to use the `READ COMMITTED` transaction isolation level. Few transactions require the `SERIALIZABLE` transaction isolation level.

- Make intelligent use of optimistic concurrency options.
    In a system with a low probability of concurrent updates, the overhead of dealing with an occasional "somebody else changed your data after you read it" error can be much lower than the overhead of always locking rows as they are read.

- Access the least amount of data possible while in a transaction.
    This lessens the number of locked rows, thereby reducing contention between transactions.

- Avoid pessimistic locking hints such as `HOLDLOCK` whenever possible.
    Hints like `HOLDLOCK` or `SERIALIZABLE` isolation level can cause processes to wait even on shared locks and reduce concurrency.

- Avoid using implicit transactions when possible.
    Implicit transactions can introduce unpredictable behavior due to their nature. See [Implicit Transactions and concurrency problems](#implicit-transactions-and-avoiding-concurrency-and-resource-problems).

#### Implicit transactions and avoiding concurrency and resource problems

To prevent concurrency and resource problems, manage implicit transactions carefully. When using implicit transactions, the next [!INCLUDE [tsql](../includes/tsql-md.md)] statement after `COMMIT` or `ROLLBACK` automatically starts a new transaction. This can cause a new transaction to be opened while the application browses through data, or even when it requires input from the user. After completing the last transaction required to protect data modifications, turn off implicit transactions until a transaction is once again required to protect data modifications. This process lets the [!INCLUDE [Database Engine](../includes/ssde-md.md)] use autocommit mode while the application is browsing data and getting input from the user.

In addition, when the `SNAPSHOT` isolation level is enabled, although a new transaction will not hold locks, a long-running transaction will prevent the old versions from being removed from the version store.

### <a id="managing-long-running-transactions"></a> Manage long-running transactions

A *long-running transaction* is an active transaction that hasn't been committed or roll backed in a timely manner. For example, if the beginning and end of a transaction is controlled by the user, a typical cause of a long-running transaction is a user starting a transaction and then leaving while the transaction waits for a response from the user.

A long running transaction can cause serious problems for a database, as follows:

- If a server instance is shut down after an active transaction has performed many uncommitted modifications, the recovery phase of the subsequent restart can take much longer than the time specified by the `recovery interval` server configuration option or by the `ALTER DATABASE ... SET TARGET_RECOVERY_TIME` option. These options control active and indirect checkpoints, respectively. For more information about the types of checkpoints, see [Database checkpoints (SQL Server)](logs/database-checkpoints-sql-server.md).

- More importantly, although a waiting transaction might generate very little log, it holds up log truncation indefinitely, causing the transaction log to grow and possibly fill up. If the transaction log fills up, the database cannot perform any more writes. For more information, see [SQL Server transaction log architecture and management guide](sql-server-transaction-log-architecture-and-management-guide.md), [Troubleshoot a full transaction log (SQL Server Error 9002)](logs/troubleshoot-a-full-transaction-log-sql-server-error-9002.md), and [The transaction log](logs/the-transaction-log-sql-server.md).

> [!IMPORTANT]  
> In Azure SQL Database, idle transactions (transactions that haven't written to the transaction log for six hours) are automatically terminated to free up resources.

#### <a id="discovering-long-running-transactions"></a> Discover long-running transactions

To look for long-running transactions, use one of the following:

- `sys.dm_tran_database_transactions`

    This dynamic management view returns information about transactions at the database level. For a long-running transaction, columns of particular interest include the time of the first log record (`database_transaction_begin_time`), the current state of the transaction (`database_transaction_state`), and the log sequence number (LSN) of the *begin* record in the transaction log (`database_transaction_begin_lsn`).

    For more information, see [sys.dm_tran_database_transactions (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md).

- `DBCC OPENTRAN`

    This statement lets you identify the user ID of the owner of the transaction, so you can potentially track down the source of the transaction for the appropriate termination (commit or roll back). For more information, see [DBCC OPENTRAN (Transact-SQL)](../t-sql/database-console-commands/dbcc-opentran-transact-sql.md).

#### <a id="stopping-a-transaction"></a> Terminate a transaction

To terminate a transaction on a specific session, use the `KILL` statement. Use this statement very carefully, however, especially when critical processes are running. For more information, see [KILL (Transact-SQL)](../t-sql/language-elements/kill-transact-sql.md).

## <a id="deadlocks"></a> Deadlocks

Deadlocks are a complex topic related to locking, but different from blocking.

- For more information on deadlocks, including monitoring, diagnosis, and samples, see the [Deadlocks guide](sql-server-deadlocks-guide.md).
- For more information on deadlocks specific to Azure SQL Database, see [Analyze and prevent deadlocks in Azure SQL Database](/azure/azure-sql/database/analyze-prevent-deadlocks).

## Related content

- [Understand and resolve SQL Server blocking problems](/troubleshoot/sql/database-engine/performance/understand-resolve-blocking)
- [Understand and resolve Azure SQL Database blocking problems](/azure/azure-sql/database/understand-resolve-blocking)
- [Transaction Related Dynamic Management Views and Functions (Transact-SQL)](system-dynamic-management-views/transaction-related-dynamic-management-views-and-functions-transact-sql.md)
- [Overhead of Row Versioning](/archive/blogs/sqlserverstorageengine/overhead-of-row-versioning)
- [sys.dm_tran_locks (Transact-SQL)](system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md)
