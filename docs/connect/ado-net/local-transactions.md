---
title: "Local transactions"
description: "Demonstrates how to perform transactions against a database with Microsoft SqlClient Data Provider for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "11/24/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Local transactions

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Transactions in ADO.NET are used when you want to bind multiple tasks together so that they execute as a single unit of work. For example, imagine that an application performs two tasks. First, it updates a table with order information. Second, it updates a table that contains inventory information, debiting the items ordered. If either task fails, then both updates are rolled back.  

## Determining the transaction type

A transaction is considered to be a local transaction when it is a single-phase transaction and is handled by the database directly. A transaction is considered to be a distributed transaction when it is coordinated by a transaction monitor and uses fail-safe mechanisms (such as two-phase commit) for transaction resolution.

The Microsoft SqlClient Data Provider for SQL Server has its own <xref:Microsoft.Data.SqlClient.SqlTransaction> object for performing local transactions in SQL Server databases. Other .NET data providers also provide their own `Transaction` objects. In addition, there is a <xref:System.Data.Common.DbTransaction> class that is available for writing provider-independent code that requires transactions.

> [!NOTE]
> Transactions are most efficient when they are performed on the server. If you are working with a SQL Server database that makes extensive use of explicit transactions, consider writing them as stored procedures using the Transact-SQL BEGIN TRANSACTION statement.

## Performing a transaction using a single connection 

In ADO.NET, you control transactions with the `Connection` object. You can initiate a local transaction with the `BeginTransaction` method. Once you have begun a transaction, you can enlist a command in that transaction with the `Transaction` property of a `Command` object. You can then commit or roll back modifications made at the data source based on the success or failure of the components of the transaction.

> [!NOTE]
> The `EnlistDistributedTransaction` method should not be used for a local transaction.

The scope of the transaction is limited to the connection. The following example performs an explicit transaction that consists of two separate commands in the `try` block. The commands execute INSERT statements against the `Production.ScrapReason` table in the AdventureWorks SQL Server sample database, which are committed if no exceptions are thrown. The code in the `catch` block rolls back the transaction if an exception is thrown. If the transaction is aborted or the connection is closed before the transaction has completed, it is automatically rolled back.

## Example  

 Follow these steps to perform a transaction.

1. Call the <xref:Microsoft.Data.SqlClient.SqlConnection.BeginTransaction%2A> method of the <xref:Microsoft.Data.SqlClient.SqlConnection> object to mark the start of the transaction. The <xref:Microsoft.Data.SqlClient.SqlConnection.BeginTransaction%2A> method returns a reference to the transaction. This reference is assigned to the <xref:Microsoft.Data.SqlClient.SqlCommand> objects that are enlisted in the transaction.

2. Assign the `Transaction` object to the <xref:Microsoft.Data.SqlClient.SqlCommand.Transaction%2A> property of the <xref:Microsoft.Data.SqlClient.SqlCommand> to be executed. If a command is executed on a connection with an active transaction, and the `Transaction` object has not been assigned to the `Transaction` property of the `Command` object, an exception is thrown.

3. Execute the required commands.

4. Call the <xref:Microsoft.Data.SqlClient.SqlTransaction.Commit%2A> method of the <xref:Microsoft.Data.SqlClient.SqlTransaction> object to complete the transaction, or call the <xref:Microsoft.Data.SqlClient.SqlTransaction.Rollback%2A> method to end the transaction. If the connection is closed or disposed before either the <xref:Microsoft.Data.SqlClient.SqlTransaction.Commit%2A> or <xref:Microsoft.Data.SqlClient.SqlTransaction.Rollback%2A> methods have been executed, the transaction is rolled back.

The following code example demonstrates transactional logic using the Microsoft SqlClient Data Provider for SQL Server.  

[!code-csharp[SqlTransactionLocal#1](~/../sqlclient/doc/samples/SqlTransactionLocal.cs#1)]

## See also

- [Transactions and concurrency](transactions-and-concurrency.md)
- [Distributed transactions](distributed-transactions.md)
- [System.Transactions integration with SQL Server](system-transactions-integration-with-sql-server.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
