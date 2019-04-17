---
title: "Understanding Transactions on Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 06075248-705e-4563-9371-b64cd609793c
author: stevestein
ms.author: sstein
manager: craigg
---
# Understanding Transactions on Memory-Optimized Tables
  Transactions access memory-optimized tables using a form of optimistic, multi-version concurrency control. This means that there are different versions of the data. Each transaction operates on its own transactionally consistent version of the database, independent from other concurrently running transactions. In addition, transactions operate under the optimistic assumption that there will be no conflicts with other, concurrent, transactions. This avoids the need to use locks, but does require the system to detect conflicts and terminate one of the conflicting transactions. Conflicts can occur only for write-write transactions and for read-write transactions. If there is a write-write conflict, one write transaction is terminated.  
  
 There are similarities between the concurrency control for memory-optimized tables and the concurrency control for disk-based tables for the READ_COMMITTED_SNAPSHOT and SNAPSHOT transaction isolation levels. (For more information about disk-based tables, see [Row Versioning-based Isolation Levels in the Database Engine](https://msdn.microsoft.com/library/ms177404\(v=sql.100\).aspx).)  
  
## Topics in This Section  
 This section on transactions in memory-optimized tables includes the following topics:  
  
-   [Guidelines for Transaction Isolation Levels with Memory-Optimized Tables](../relational-databases/in-memory-oltp/memory-optimized-tables.md)  
  
-   [Guidelines for Retry Logic for Transactions on Memory-Optimized Tables](guidelines-for-retry-logic-for-transactions-on-memory-optimized-tables.md)  
  
-   [Transactions in Memory-Optimized Tables](transactions-in-memory-optimized-tables.md)  
  
-   [Transaction Isolation Levels](transaction-isolation-levels.md)  
  
-   [Cross-Container Transactions](cross-container-transactions.md)  
  
 For more information, see [Control Transaction Durability](../relational-databases/logs/control-transaction-durability.md).  
  
## See Also  
 [Memory-Optimized Tables](../relational-databases/in-memory-oltp/memory-optimized-tables.md)  
  
  
