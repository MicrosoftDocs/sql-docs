---
title: "Distributed transactions"
description: "Describes how to perform distributed transactions in ADO.NET"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "11/25/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Distributed transactions

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

A transaction is a set of related tasks that either succeeds (commit) or fails (abort) as a unit, among other things. A *distributed transaction* is a transaction that affects several resources. For a distributed transaction to commit, all participants must guarantee that any change to data will be permanent. Changes must persist despite system crashes or other unforeseen events. If even a single participant fails to make this guarantee, the entire transaction fails, and any changes to data within the scope of the transaction are rolled back. 

> [!NOTE]
> For information on distributed transactions in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)], see [Distributed transactions across cloud databases](/azure/azure-sql/database/elastic-transactions-overview).

## Working with System.Transactions

In .NET, distributed transactions are managed through the API in the <xref:System.Transactions> namespace. The <xref:System.Transactions> API will delegate distributed transaction handling to a transaction monitor such as the Microsoft Distributed Transaction Coordinator (MS DTC) when multiple persistent resource managers are involved. For more information, see [Transaction Fundamentals](/dotnet/framework/data/transactions/transaction-fundamentals).

ADO.NET 2.0 introduced support for enlisting in a distributed transaction using the `EnlistTransaction` method, which enlists a connection in a <xref:System.Transactions.Transaction> instance. In previous versions of ADO.NET, explicit enlistment in distributed transactions was performed using the `EnlistDistributedTransaction` method of a connection to enlist a connection in a <xref:System.EnterpriseServices.ITransaction> instance, which is supported for backwards compatibility. For more information on Enterprise Services transactions, see [Interoperability with Enterprise Services and COM+ Transactions](/dotnet/framework/data/transactions/interoperability-with-enterprise-services-and-com-transactions).

When using a <xref:System.Transactions> transaction with the Microsoft SqlClient Data Provider for SQL Server against a SQL Server database, a lightweight <xref:System.Transactions.Transaction> will automatically be used. The transaction can then be promoted to a full distributed transaction on an as-needed basis. For more information, see [System.Transactions integration with SQL Server](system-transactions-integration-with-sql-server.md).

## Automatically enlisting in a distributed transaction

Automatic enlistment is the default (and preferred) way of integrating ADO.NET connections with `System.Transactions`. A connection object will automatically enlist in an existing distributed transaction if it determines that a transaction is active, which, in `System.Transaction` terms, means that `Transaction.Current` is not null. Automatic transaction enlistment occurs when the connection is opened. It will not happen after that even if a command is executed inside of a transaction scope. You can disable auto-enlistment in existing transactions by specifying `Enlist=false` as a connection string parameter for a <xref:Microsoft.Data.SqlClient.SqlConnection.ConnectionString%2A?displayProperty=nameWithType>.

## Manually enlisting in a distributed transaction

If auto-enlistment is disabled or you need to enlist a transaction that was started after the connection was opened, you can enlist in an existing distributed transaction using the `EnlistTransaction` method of the <xref:Microsoft.Data.SqlClient.SqlConnection> object for the Microsoft SqlClient Data Provider for SQL Server. Enlisting in an existing distributed transaction ensures that, if the transaction is committed or rolled back, modifications made by the code at the data source will be committed or rolled back as well.

> [!NOTE]
> An exception will be thrown if you attempt to commit or roll back a transaction if a `DataReader` is started while the transaction is active.

Enlisting in distributed transactions is particularly applicable when pooling business objects. If a business object is pooled with an open connection, automatic transaction enlistment only occurs when that connection is opened. If multiple transactions are performed using the pooled business object, the open connection for that object will not automatically enlist in newly initiated transactions. In this case, you can disable automatic transaction enlistment for the connection and enlist the connection in transactions using `EnlistTransaction`.

`EnlistTransaction` takes a single argument of type <xref:System.Transactions.Transaction> that is a reference to the existing transaction. After calling the connection's `EnlistTransaction` method, all modifications made at the data source using the connection are included in the transaction. Passing a null value unenlists the connection from its current distributed transaction enlistment. Note that the connection must be opened before calling `EnlistTransaction`.

> [!NOTE]
> Once a connection is explicitly enlisted on a transaction, it cannot be un-enlisted or enlisted in another transaction until the first transaction finishes.

> [!CAUTION]
> `EnlistTransaction` throws an exception if the connection has already begun a transaction using the connection's <xref:Microsoft.Data.SqlClient.SqlConnection.BeginTransaction%2A> method. However, if the transaction is a local transaction started at the data source (for example, executing the BEGIN TRANSACTION statement explicitly using a <xref:Microsoft.Data.SqlClient.SqlCommand>), `EnlistTransaction` will roll back the local transaction and enlist in the existing distributed transaction as requested. You will not receive notice that the local transaction was rolled back, and must manage any local transactions not started using <xref:Microsoft.Data.SqlClient.SqlConnection.BeginTransaction%2A>. If you are using the Microsoft SqlClient Data Provider for SQL Server with SQL Server, an attempt to enlist will throw an exception. All other cases will go undetected.  

## Promotable transactions in SQL Server

SQL Server supports promotable transactions in which a local lightweight transaction can be automatically promoted to a distributed transaction only if it is required. A promotable transaction does not invoke the added overhead of a distributed transaction unless the added overhead is required. For more information and a code sample, see [System.Transactions integration with SQL Server](system-transactions-integration-with-sql-server.md).

## Configuring Distributed Transactions

 You may need to enable the MS DTC over the network in order to use distributed transactions. If the server's local Windows Firewall is enabled, you must allow the MS DTC service to use the network or open the MS DTC port.  
  
## See also

- [Transactions and concurrency](transactions-and-concurrency.md)
- [System.Transactions integration with SQL Server](system-transactions-integration-with-sql-server.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
