---
title: "Transaction Processing | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "transactions [ADO]"
  - "data updates [ADO], transaction processing"
  - "updating data [ADO], transaction processing"
  - "nested transactions [ADO]"
ms.assetid: 74ab6706-e2dc-42cb-af77-dbc58a9cf4ce
author: MightyPen
ms.author: genemi
manager: craigg
---
# Transaction Processing
A *transaction* delimits the beginning and end of a series of data access operations executed across a connection. Subject to the transactional capabilities of your data source, the **Connection** object also allows you to create and manage transactions. For example, using the Microsoft OLE DB Provider for SQL Server to access a database on Microsoft SQL Server, you can create multiple nested transactions for the commands you execute.  
  
 ADO ensures that changes to a data source resulting from operations in a transaction occur successfully together or not at all.  
  
 If you cancel the transaction, or if one of its operations fails, the result will be as if none of the operations in the transaction occurred. The data source will remain as it was before the transaction began.  
  
 ADO provides the following methods for controlling transactions: **BeginTrans**, **CommitTrans**, and **RollbackTrans**. Use these methods with a **Connection** object when you want to save or cancel a series of changes made to the source data as a single unit. For example, to transfer money between accounts, you subtract an amount from one and add the same amount to the other. If either update fails, the accounts no longer balance. Making these changes within an open transaction ensures that either all or none of the changes go through.  
  
> [!NOTE]
>  Not all providers support transactions. Verify that the provider-defined property "**Transaction DDL**" appears in the **Connection** object's [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection, indicating that the provider supports transactions. If the provider does not support transactions, calling one of these methods will return an error.  
  
 After you call the **BeginTrans** method, the provider will no longer instantaneously commit changes you make until you call **CommitTrans** or **RollbackTrans** to end the transaction.  
  
 Calling the **CommitTrans** method saves changes made within an open transaction on the connection and ends the transaction. Calling the **RollbackTrans** method reverses any changes made within an open transaction and ends the transaction. Calling either method when there is no open transaction generates an error.  
  
 Depending on the **Connection** object's [Attributes](../../../ado/reference/ado-api/attributes-property-ado.md) property, calling either the **CommitTrans** or **RollbackTrans** method may automatically start a new transaction. If the **Attributes** property is set to **adXactCommitRetaining**, the provider automatically starts a new transaction after a **CommitTrans** call. If the **Attributes** property is set to **adXactAbortRetaining**, the provider automatically starts a new transaction after a **RollbackTrans** call.  
  
## Transaction Isolation Level  
 Use the **IsolationLevel** property to set the isolation level of a transaction on a **Connection** object. The setting does not take effect until the next time you call the [BeginTrans](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md) method. If the level of isolation you request is unavailable, the provider may return the next greater level of isolation. Refer to the **IsolationLevel** property in the ADO Programmer's Reference for more details on valid values.  
  
## Nested Transactions  
 For providers that support nested transactions, calling the **BeginTrans** method within an open transaction starts a new, nested transaction. The return value indicates the level of nesting: a return value of "1" indicates you have opened a top-level transaction (that is, the transaction is not nested within another transaction), "2" indicates that you have opened a second-level transaction (a transaction nested within a top-level transaction), and so forth. Calling **CommitTrans** or **RollbackTrans** affects only the most recently opened transaction; you must close or roll back the current transaction before you can resolve any higher-level transactions.
