---
title: "Understanding Transactions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: d3e0414c-6809-4bb1-93b1-4960507faecc
author: MightyPen
ms.author: genemi
manager: craigg
---

# Understanding Transactions

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Transactions are groups of operations that are combined into logical units of work. They are used to control and maintain the consistency and integrity of each action in a transaction, despite errors that might occur in the system.

With the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], transactions can be either local or distributed. Transactions can also use isolation levels. For more information about the isolation levels supported by the JDBC driver, see [Understanding Isolation Levels](../../connect/jdbc/understanding-isolation-levels.md).

Applications should control transactions by either using Transact-SQL statements or the methods provided by the JDBC driver, but not both. Using both Transact-SQL statements and JDBC API methods on the same transaction might lead to problems, such as a transaction cannot be committed when expected, a transaction is committed or rolled back and a new one starts unexpectedly, or "Failed to resume the transaction" exceptions.

## Using Local Transactions

A transaction is considered to be local when it is a single-phase transaction, and it is handled by the database directly. The JDBC driver supports local transactions by using various methods of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class, including [setAutoCommit](../../connect/jdbc/reference/setautocommit-method-sqlserverconnection.md), [commit](../../connect/jdbc/reference/commit-method-sqlserverconnection.md), and [rollback](../../connect/jdbc/reference/rollback-method.md). Local transactions are typically managed explicitly by the application or automatically by the Java Platform, Enterprise Edition (Java EE) application server.

The following example performs a local transaction that consists of two separate statements in the `try` block. The statements are run against the Production.ScrapReason table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database, and they are committed if no exceptions are thrown. The code in the `catch` block rolls back the transaction if an exception is thrown.

[!code[JDBC#UnderstandingTransactions1](../../connect/jdbc/codesnippet/Java/understanding-transactions_1.java)]

## Using Distributed Transactions

A distributed transaction updates data on two or more networked databases while retaining the important atomic, consistent, isolated, and durable (ACID) properties of transaction processing. Distributed transaction support was added to the JDBC API in the JDBC 2.0 Optional API specification. The management of distributed transactions is typically performed automatically by the Java Transaction Service (JTS) transaction manager in a Java EE application server environment. However, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] supports distributed transactions under any Java Transaction API (JTA) compliant transaction manager.

The JDBC driver seamlessly integrates with [!INCLUDE[msCoName](../../includes/msconame_md.md)] Distributed Transaction Coordinator (MS DTC) to provide true distributed transaction support with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. MS DTC is a distributed transaction facility provided by [!INCLUDE[msCoName](../../includes/msconame_md.md)] for [!INCLUDE[msCoName](../../includes/msconame_md.md)] Windows systems. MS DTC uses proven transaction processing technology from [!INCLUDE[msCoName](../../includes/msconame_md.md)] to support XA features such as the complete two-phase distributed commit protocol and the recovery of distributed transactions.

For more information about how to use distributed transactions, see [Understanding XA Transactions](../../connect/jdbc/understanding-xa-transactions.md).

## See Also

[Performing Transactions with the JDBC Driver](../../connect/jdbc/performing-transactions-with-the-jdbc-driver.md)
