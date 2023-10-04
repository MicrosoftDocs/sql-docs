---
title: "Transactions and concurrency"
description: "Describes how to use Microsoft SqlClient Data Provider for SQL Server with transactions and concurrency."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "11/24/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Transactions and concurrency

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

A transaction consists of a single command or a group of commands that execute as a package. Transactions allow you to combine multiple operations into a single unit of work. If a failure occurs at one point in the transaction, all of the updates can be rolled back to their pre-transaction state.

A transaction must conform to the ACID properties—atomicity, consistency, isolation, and durability—in order to guarantee data consistency. Most relational database systems, such as Microsoft SQL Server, support transactions by providing locking, logging, and transaction management facilities whenever a client application performs an update, insert, or delete operation.

> [!NOTE]
> Transactions that involve multiple resources can lower concurrency if locks are held too long. Therefore, keep transactions as short as possible.  

If a transaction involves multiple tables in the same database or server, then explicit transactions in stored procedures often perform better. You can create transactions in SQL Server stored procedures by using the Transact-SQL `BEGIN TRANSACTION`, `COMMIT TRANSACTION`, and `ROLLBACK TRANSACTION` statements. For more information, see SQL Server Books Online.

Transactions involving different resource managers, such as a transaction between SQL Server and Oracle, require a distributed transaction.

## In this section

[Local transactions](local-transactions.md)  
Demonstrates how to perform transactions against a database.  
  
[Distributed transactions](distributed-transactions.md)  
Describes how to perform distributed transactions in ADO.NET.  
  
[System.Transactions integration with SQL Server](system-transactions-integration-with-sql-server.md)  
Describes <xref:System.Transactions> integration with SQL Server for working with distributed transactions.  
  
[Optimistic concurrency](optimistic-concurrency.md) 
Describes optimistic and pessimistic concurrency, and how you can test for concurrency violations.  

## See also

- [Transaction fundamentals](/dotnet/framework/data/transactions/transaction-fundamentals)
- [Connecting to data source](connecting-to-data-source.md)
- [Commands and parameters](commands-parameters.md)
- [DataAdapters and DataReaders](dataadapters-datareaders.md)
- [DbProviderFactories](dbproviderfactories.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
