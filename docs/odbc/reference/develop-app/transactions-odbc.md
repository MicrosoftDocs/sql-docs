---
title: "Transactions ODBC | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "transactions [ODBC], about transactions"
  - "transactions [ODBC]"
ms.assetid: b4ca861a-c164-4e87-8672-d5de15e3823c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Transactions ODBC
A *transaction* is a unit of work that is done as a single,atomic operation; that is, the operation succeeds or fails as a whole. For example, consider transferring money from one bank account to another. This involves two steps: withdrawing the money from the first account and depositing it in the second. It is important that both steps succeed; it is not acceptable for one step to succeed and the other to fail. A database that supports transactions is able to guarantee this.  
  
 Transactions can be completed by either being *committed* or being *rolled back*. When a transaction is committed, the changes made in that transaction are made permanent. When a transaction is rolled back, the affected rows are returned to the state they were in before the transaction was started. To extend the account transfer example, an application executes one SQL statement to debit the first account and a different SQL statement to credit the second account. If both statements succeed, the application then commits the transaction. But if either statement fails for any reason, the application rolls back the transaction. In either case, the application guarantees a consistent state at the end of the transaction.  
  
 A single transaction can encompass multiple database operations that occur at different times. If other transactions had complete access to the intermediate results, the transactions might interfere with one another. For example, suppose one transaction inserts a row, a second transaction reads that row, and the first transaction is rolled back. The second transaction now has data for a row that does not exist.  
  
 To solve this problem, there are various schemes to isolate transactions from one another. *Transaction isolation* is generally implemented by locking rows, which precludes more than one transaction from using the same row at the same time. In some databases, locking a row may also lock other rows.  
  
 With increased transaction isolation comes reduced *concurrency,* or the ability of two transactions to use the same data at the same time. For more information, see [Setting the Transaction Isolation Level](../../../odbc/reference/develop-app/setting-the-transaction-isolation-level.md).  
  
 This section contains the following topics.  
  
-   [Transactions in ODBC](../../../odbc/reference/develop-app/transactions-in-odbc-odbc.md)  
  
-   [Transaction Isolation](../../../odbc/reference/develop-app/transaction-isolation.md)  
  
-   [Concurrency Control](../../../odbc/reference/develop-app/concurrency-control.md)
