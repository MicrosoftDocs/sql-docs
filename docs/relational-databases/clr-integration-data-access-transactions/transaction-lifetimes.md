---
title: "Transaction Lifetimes | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "lifetimes [SQL Server]"
  - "Transact-SQL vs. managed code"
ms.assetid: cb076fda-6488-4959-a6a4-7adaccf3f25c
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Transaction Lifetimes
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  There is an important difference between transactions started in [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures and those started in managed code: common language runtime (CLR) code cannot unbalance the transaction state on entry or exit of a CLR invocation. Be aware of the following implications of this difference:  
  
-   A transaction started inside a CLR frame must be committed or rolled back, or else [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates an error when the frame is exited.  
  
-   An outer transaction cannot be committed or rolled back inside the CLR code.  
  
-   An attempt to commit a transaction not started in the same procedure causes a run-time error.  
  
-   An attempt to roll back a transaction not started in the same procedure causes the transaction to hang (preventing any other side-effecting operation from happening). The transaction discontinues until the CLR code goes out of scope. Note that this can be useful when you detect an error inside your procedure and want to make sure the whole transaction terminates.  
  
## See Also  
 [CLR Integration and Transactions](../../relational-databases/clr-integration-data-access-transactions/clr-integration-and-transactions.md)  
  
  
