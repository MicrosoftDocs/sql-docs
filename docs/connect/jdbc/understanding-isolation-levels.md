---
title: Understanding isolation levels
description: Learn about how to control transaction isolation levels in the JDBC Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/06/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Understanding isolation levels

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Transactions specify an isolation level that defines how one transaction is isolated from other transactions. Isolation is the separation of resource or data modifications made by different transactions. Isolation levels are described for which concurrency side effects are allowed, such as dirty reads or phantom reads.

Transaction isolation levels control the following effects:

- Whether locks are taken when data is read, and what type of locks are requested.

- How long the read locks are held.

- Whether read operations referencing rows modified by another transaction:

  - Block until the exclusive lock on the row is freed.

  - Retrieve the committed version of the row that existed at the time the statement or transaction started.

  - Read the uncommitted data modification.

## Choosing an isolation level

Choosing a transaction isolation level doesn't affect the locks that are acquired to protect data modifications. A transaction always gets an exclusive lock on any data it modifies. It holds that lock until the transaction completes, whatever the isolation level set for that transaction. For read operations, transaction isolation levels mainly define how the operation is protected from the effects of other transactions.

A lower isolation level increases the ability of many users to access data at the same time. But it increases the number of concurrency effects, such as dirty reads or lost updates, that users might see. Conversely, a higher isolation level reduces the types of concurrency effects that users might see. But it requires more system resources and increases the chances that one transaction will block another. Choosing the appropriate isolation level depends on balancing the data integrity requirements of the application against the overhead of each isolation level.

The highest isolation level, serializable, guarantees that a transaction will retrieve exactly the same data every time it repeats a read operation. But it uses a level of locking that is likely to impact other users in multi-user systems. The lowest isolation level, read uncommitted, can retrieve data that has been modified but not committed by other transactions. All concurrency side effects can happen in read uncommitted, however there's no read locking or versioning, so overhead is minimized.

## Remarks

 The following table shows the concurrency side effects allowed by the different isolation levels.

| Isolation Level  | Dirty Read | Non-Repeatable Read | Phantom |
| ---------------- | ---------- | ------------------- | ------- |
| Read uncommitted | Yes        | Yes                 | Yes     |
| Read committed   | No         | Yes                 | Yes     |
| Repeatable read  | No         | No                  | Yes     |
| Snapshot         | No         | No                  | No      |
| Serializable     | No         | No                  | No      |

Transactions must be run at an isolation level of at least repeatable read to prevent lost updates that can occur when two transactions each retrieve the same row. The transaction then later updates the row based on the originally retrieved values. If the two transactions update rows using a single UPDATE statement and don't base the update on the previously retrieved values, lost updates can't occur at the default isolation level of read committed.

To set the isolation level for a transaction, you can use the [setTransactionIsolation](../../connect/jdbc/reference/settransactionisolation-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class. This method accepts an **int** value as its argument, which is based on one of the connection constants as in the following:

```java
con.setTransactionIsolation(Connection.TRANSACTION_READ_COMMITTED);
```

To use the new snapshot isolation level of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can use one of the `SQLServerConnection` constants:

```java
con.setTransactionIsolation(SQLServerConnection.TRANSACTION_SNAPSHOT);
```

or you can use:

```java
con.setTransactionIsolation(Connection.TRANSACTION_READ_COMMITTED + 4094);
```

For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] isolation levels, see "Isolation Levels in the [!INCLUDE[ssDE](../../includes/ssde_md.md)]" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.

## See also

[Performing transactions with the JDBC driver](../../connect/jdbc/performing-transactions-with-the-jdbc-driver.md)  
[SET TRANSACTION ISOLATION LEVEL (Transact-SQL)](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)  
