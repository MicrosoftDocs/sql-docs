---
title: "Control Transaction Durability"
description: Learn about delayed durable transactions. SQL Server transaction commits can be fully durable or delayed durable, also known as lazy commit.
author: "MashaMSFT"
ms.author: mathoma
ms.reviewer: wiassaf
ms.date: 11/11/2022
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "delayed durability"
  - "Lazy Commit"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Control Transaction Durability

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction commits can be either fully durable, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] default, or delayed durable (also known as lazy commit).

 Fully durable transaction commits are synchronous and report a commit as successful and return control to the client only after the log records for the transaction are written to disk. Delayed durable transaction commits are asynchronous and report a commit as successful before the log records for the transaction are written to disk. Writing the transaction log entries to disk is required for a transaction to be durable. Delayed durable transactions become durable when the transaction log entries are flushed to disk.

This article details delayed durable transactions.

## Full vs. Delayed Transaction Durability

 Both full and delayed transaction durability have advantages and disadvantages. An application can have a mix of fully and delayed durable transactions. You should carefully consider your business needs and how each fits into those needs.

### Full transaction durability

 Fully durable transactions write the transaction log to disk before returning control to the client. You should use fully durable transactions whenever:

- Your system cannot tolerate any data loss.
    See the section [When can I lose data?](../../relational-databases/logs/control-transaction-durability.md#bkmk_DataLoss) for information on when you can lose some of your data.

- The bottleneck is not due to transaction log write latency.

 Delayed transaction durability reduces the latency due to log I/O by keeping the transaction log records in memory and writing to the transaction log in batches, thus requiring fewer I/O operations. Delayed transaction durability potentially reduces log I/O contention, thus reducing waits in the system.

 **Full Transaction Durability Guarantees**

- Once transaction commit succeeds, the changes made by the transaction are visible to the other transactions in the system. For more information about transaction isolation levels, see [SET TRANSACTION ISOLATION LEVEL (Transact-SQL)](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md) or [Transactions with Memory-Optimized Tables](../../relational-databases/in-memory-oltp/transactions-with-memory-optimized-tables.md).

- Durability is guaranteed on commit. Corresponding log records are persisted to disk before the transaction commit succeeds and returns control to the client.

### Delayed transaction durability

 Delayed transaction durability is accomplished using asynchronous log writes to disk. Transaction log records are kept in a buffer and written to disk when the buffer fills or a buffer flushing event takes place. Delayed transaction durability reduces both latency and contention within the system because:

- The transaction commit processing does not wait for log IO to finish and return control to the client.

- Concurrent transactions are less likely to contend for log IO; instead, the log buffer can be flushed to disk in larger chunks, reducing contention, and increasing throughput.

    > [!NOTE]  
    >  You may still have log I/O contention if there is a high degree of concurrency, particularly if you fill up the log buffer faster than you flush it.

 #### When to use delayed transaction durability

 Some of the cases in which you could benefit from using delayed transaction durability are:

 **You can tolerate some data loss.**  
 If you can tolerate some data loss, for example, where individual records are not critical as long as you have most of the data, then delayed durability may be worth considering. If you cannot tolerate any data loss, do not use delayed transaction durability.

 **You are experiencing a bottleneck on transaction log writes.**  
 If your performance issues are due to latency in transaction log writes, your application will likely benefit from using delayed transaction durability.

 **Your workloads have a high contention rate.**  
 If your system has workloads with a high contention level much time is lost waiting for locks to be released. Delayed transaction durability reduces commit time and thus releases locks faster, which results in higher throughput.

 ### Delayed Transaction Durability Guarantees

- Once transaction commit succeeds, the changes made by the transaction are visible to the other transactions in the system.

- Transaction durability is guaranteed only following a flush of the in-memory transaction log to disk. The in-memory transaction log is flushed to disk when:

    -   A fully durable transaction in the same database makes a change in the database and successfully commits.

    -   The user executes the system stored procedure `sp_flush_log` successfully.

        If a fully durable transaction or sp_flush_log successfully commits, all previously committed delayed durability transactions are guaranteed to have been made durable.

    - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does attempt to flush the log to disk both based on log generation and on timing, even if all the transactions are delayed durable. This usually succeeds if the IO device is keeping up. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not provide any hard durability guarantees other than durable transactions and sp_flush_log.


## How to control transaction durability

### <a id="bkmk_DbControl"></a> Database level control

 You, the DBA, can control whether users can use delayed transaction durability on a database with the following statement. You must set the delayed durability setting with ALTER DATABASE.

```syntaxsql
ALTER DATABASE ... SET DELAYED_DURABILITY = { DISABLED | ALLOWED | FORCED }
```

 **DISABLED**  
 [default] With this setting, all transactions that commit on the database are fully durable, regardless of the commit level setting (DELAYED_DURABILITY=[ON | OFF]). There is no need for stored procedure change and recompilation. This allows you to ensure that no data is ever put at risk by delayed durability.

 **ALLOWED**  
 With this setting, each transaction's durability is determined at the transaction level - DELAYED_DURABILITY = { *OFF* | ON }. See [Atomic block level control - Natively Compiled Stored Procedures](../../relational-databases/logs/control-transaction-durability.md#CompiledProcControl) and [COMMIT level control](../../relational-databases/logs/control-transaction-durability.md#bkmk_T-SQLControl) for more information.

 **FORCED**  
 With this setting, every transaction that commits on the database is delayed durable. Whether the transaction specifies fully durable (DELAYED_DURABILITY = OFF) or makes no specification, the transaction is delayed durable. This setting is useful when delayed transaction durability is useful for a database and you do not want to change any application code.

### <a id="CompiledProcControl"></a> Atomic block level control - Natively Compiled Stored Procedures

 The following code goes inside the atomic block.

```syntaxsql
DELAYED_DURABILITY = { OFF | ON }
```

 **OFF**  
 [default] The transaction is fully durable, unless the database option DELAYED_DURABLITY = FORCED is in effect, in which case the commit is asynchronous and thus delayed durable. For more information, see [Database level control](../../relational-databases/logs/control-transaction-durability.md#bkmk_DbControl).

 **ON**  
 The transaction is delayed durable, unless the database option DELAYED_DURABLITY = DISABLED is in effect, in which case the commit is synchronous and thus fully durable. For more information, see [Database level control](../../relational-databases/logs/control-transaction-durability.md#bkmk_DbControl).

 **Example Code:**

```sql
CREATE PROCEDURE [<procedureName>] /* parameters go here */
WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER
AS BEGIN ATOMIC WITH
(
    DELAYED_DURABILITY = ON,
    TRANSACTION ISOLATION LEVEL = SNAPSHOT,
    LANGUAGE = N'English'
)
/* procedure body goes here */
END
```

### Table 1: Durability in Atomic Blocks

| Atomic block durability option | No existing transaction | Transaction in process (fully or delayed durable) |
| --- | --- | --- |
| **DELAYED_DURABILITY = OFF** | Atomic block starts a new fully durable transaction. | Atomic block creates a save point in the existing transaction, then begins the new transaction. |
| **DELAYED_DURABILITY = ON** | Atomic block starts a new delayed durable transaction. | Atomic block creates a save point in the existing transaction, then begins the new transaction. |

### <a id="bkmk_T-SQLControl"></a> COMMIT level control -[!INCLUDE[tsql](../../includes/tsql-md.md)]

 The COMMIT syntax is extended so you can force delayed transaction durability. If DELAYED_DURABILITY is DISABLED or FORCED at the database level (see above) this COMMIT option is ignored.

```syntaxsql
COMMIT [ { TRAN | TRANSACTION } ] [ transaction_name | @tran_name_variable ] ] [ WITH ( DELAYED_DURABILITY = { OFF | ON } ) ]
```

 **OFF**  
 [default] The transaction COMMIT is fully durable, unless the database option DELAYED_DURABLITY = FORCED is in effect, in which case the COMMIT is asynchronous and thus delayed durable. For more information, see [Database level control](../../relational-databases/logs/control-transaction-durability.md#bkmk_DbControl).

 **ON**  
 The transaction COMMIT is delayed durable, unless the database option DELAYED_DURABLITY = DISABLED is in effect, in which case the COMMIT is synchronous and thus fully durable. For more information, see [Database level control](../../relational-databases/logs/control-transaction-durability.md#bkmk_DbControl).

### Summary of options and their interactions

 This table summarizes the interactions between database level delayed durability settings and commit level settings. Database level settings always take precedence over commit level settings.

| COMMIT setting/Database setting | DELAYED_DURABILITY = DISABLED | DELAYED_DURABILITY = ALLOWED | DELAYED_DURABILITY = FORCED |
| --- | --- | --- | --- |
| **DELAYED_DURABILITY = OFF** Database level transactions. | Transaction is fully durable. | Transaction is fully durable. | Transaction is delayed durable. |
| **DELAYED_DURABILITY = ON** Database level transactions. | Transaction is fully durable. | Transaction is delayed durable. | Transaction is delayed durable. |
| **DELAYED_DURABILITY = OFF** Cross database or distributed transaction. | Transaction is fully durable. | Transaction is fully durable. | Transaction is fully durable. |
| **DELAYED_DURABILITY = ON** Cross database or distributed transaction. | Transaction is fully durable. | Transaction is fully durable. | Transaction is fully durable. |

## How to force a transaction log flush

 There are two means to force flush the transaction log to disk.

- Execute any fully durable transaction that alters the same database. This forces a flush of the log records of all preceding committed delayed durability transactions to disk.

- Execute the system stored procedure `sp_flush_log`. This procedure forces a flush of the log records of all preceding committed delayed durable transactions to disk. For more information, see [sys.sp_flush_log (Transact-SQL)](../../relational-databases/system-stored-procedures/sys-sp-flush-log-transact-sql.md).

## <a id="bkmk_OtherSQLFeatures"></a> Delayed durability and other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features

 **Change tracking and change data capture**  
 All transactions with change tracking are fully durable. A transaction has the change tracking property if it does any write operations to tables that are enabled for change tracking. The use of delayed durability is not supported for databases, which use change data capture (CDC).

 **Crash recovery**  
 Consistency is guaranteed, but some changes from delayed durable transactions that have committed may be lost.

 **Cross-database and DTC**  
 If a transaction is cross-database or distributed, it is fully durable, regardless of any database or transaction commit setting.

 **Always On Availability Groups and Mirroring**  
 Delayed durable transactions do not guarantee any durability on either the primary or any of the secondaries. In addition, they do not guarantee any knowledge about the transaction at the secondary. After commit, control is returned to the client before any acknowledgment is received from any synchronous secondary. Replication to secondary replicas does continue to happen as flush to disk on the primary happens.

 **Failover clustering**  
 Some delayed durable transaction writes might be lost.

 **Transaction Replication**  
 Delayed durable transactions are not supported with Transactional Replication.

 **Azure Synapse Link for SQL**  
 Delayed durable transactions are not supported with Azure Synapse Link for SQL.

 **Log shipping**  
 Only transactions that have been made durable are included in the log that is shipped.

 **Transaction Log Backup**  
 Only transactions that have been made durable are included in the backup.

## <a id="bkmk_DataLoss"></a> When can I lose data?

 If you implement delayed durability on any of your tables, you should understand that certain circumstances can lead to data loss. If you cannot tolerate any data loss, you should not use delayed durability on your tables.

### Catastrophic events

 In the case of a catastrophic event, like a server crash, you will lose the data for all committed transactions that have not been saved to disk. Delayed durable transactions are saved to disk whenever a fully durable transaction is executed against any table (durable memory-optimized or disk-based) in the database, or `sp_flush_log` is called. If you are using delayed durable transactions, you may want to create a small table in the database that you can periodically update or periodically call `sp_flush_log` to save all outstanding committed transactions. The transaction log also flushes whenever it becomes full, but that is hard to predict and impossible to control.

### [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] shutdown and restart

 For delayed durability, there is no difference between an unexpected shutdown and an expected shutdown/restart of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Like catastrophic events, you should plan for data loss. In a planned shutdown/restart, some transactions that have not been written to disk may be saved to disk before shutdown, but you should not plan on it. Plan as though a shutdown/restart, whether planned or unplanned, loses the data the same as a catastrophic event.

## Next steps

- [Transactions with Memory-Optimized Tables](../../relational-databases/in-memory-oltp/transactions-with-memory-optimized-tables.md)
