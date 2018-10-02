---
title: "Performing Transactions in ADOMD.NET | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "transactions [ADOMD.NET]"
  - "ADOMD.NET, transactions"
  - "AdomdTransaction object"
ms.assetid: 7978c28b-c255-43c0-ad05-f38604d4d8fe
author: minewiskan
ms.author: owend
manager: craigg
---
# Performing Transactions in ADOMD.NET
  In ADOMD.NET, you use the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdTransaction> object to manage transaction context for a given <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object. This functionality allows you to run several commands within the same context. Each command will read the same data without the read data changing between each command execution.  
  
> [!NOTE]  
>  The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdTransaction> class is the implementation of the `System.Data.IDbTransaction` interface, part of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework Class Library and implemented by all .NET Framework data providers that support transactions.  
  
 The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdTransaction> object only supports read-committed transactions, in which shared locks are held while the data is being read to avoid dirty reads.  
  
 The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> is used to start the transaction. To use the transaction, commands are then run against the connection that started the transaction. When you are finished with the transaction, you can either be roll back or commit the transaction.  
  
## Starting a Transaction  
 You create an instance of an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdTransaction> object by calling the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.BeginTransaction%2A> method of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object. The following example shows how to create an instance of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdTransaction> object:  
  
```  
Dim objTransaction As AdomdTransaction = objConnection.BeginTransaction()  
AdomdTransaction objTransaction = objConnection.BeginTransaction();  
```  
  
## Rolling Back a Transaction  
 To roll back an existing, incomplete transaction, you call the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdTransaction.Rollback%2A> method of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdTransaction> object. If you call this method on an existing, complete transaction, an exception is thrown.  
  
## Committing a Transaction  
 After you call the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.BeginTransaction%2A> method to start a transaction, you can complete the transaction by calling the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdTransaction.Commit%2A> method of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdTransaction> object. If this method is called on an existing, complete transaction, an exception is thrown.  
  
## See Also  
 [Establishing Connections in ADOMD.NET](connections-in-adomd-net.md)   
 [ADOMD.NET Client Programming](adomd-net-client-programming.md)  
  
  
