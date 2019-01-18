---
title: "Integration Services Transactions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "containers [Integration Services], transactions"
  - "transactions [Integration Services], about transactions in packages"
  - "tasks [Integration Services], transactions"
  - "transactions [Integration Services]"
ms.assetid: 3c78bb26-ddce-4831-a5f8-09d4f4fd53cc
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Integration Services Transactions
  Packages use transactions to bind the database actions that tasks perform into atomic units, and by doing this maintain data integrity. All [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] container types-packages, the For Loop, Foreach Loop, and Sequence containers, and the task hosts that encapsulate each task-can be configured to use transactions. [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides three options for configuring transactions: **NotSupported**, **Supported**, and **Required**.  
  
-   **Required** indicates that the container starts a transaction, unless one is already started by its parent container. If a transaction already exists, the container joins the transaction. For example, if a package that is not configured to support transactions includes a Sequence container that uses the **Required** option, the Sequence container would start its own transaction. If the package were configured to use the **Required** option, the Sequence container would join the package transaction.  
  
-   **Supported** indicates that the container does not start a transaction, but joins any transaction started by its parent container. For example, if a package with four Execute SQL tasks starts a transaction and all four tasks use the **Supported** option, the database updates performed by the Execute SQL tasks are rolled back if any task fails. If the package does not start a transaction, the four Execute SQL tasks are not bound by a transaction, and no database updates except the ones performed by the failed task are rolled back.  
  
-   **NotSupported** indicates that the container does not start a transaction or join an existing transaction. A transaction started by a parent container does not affect child containers that have been configured to not support transactions. For example, if a package is configured to start a transaction and a For Loop container in the package uses the **NotSupported** option, none of the tasks in the For Loop can roll back if they fail.  
  
 You configure transactions by setting the TransactionOption property on the container. You can set this property by using the **Properties** window in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], or you can set the property programmatically.  
  
> [!NOTE]  
>  The `TransactionOption` property influences whether or not the value of the `IsolationLevel` property requested by a container is applied. For more information, see the description of the `IsolationLevel` property in the topic, [Setting Package Properties](set-package-properties.md).  
  
### To configure a package to use transactions  
  
-   [Configure a Package to Use Transactions](../relational-databases/native-client-ole-db-transactions/transactions.md)  
  
## External Resources  
  
-   Blog entry, [How to Use Transactions in SQL Server Integration Services SSIS](https://go.microsoft.com/fwlink/?LinkId=157783), on www.mssqltips.com  
  
## See Also  
 [Inherited Transactions](../../2014/integration-services/inherited-transactions.md)   
 [Multiple Transactions](../../2014/integration-services/multiple-transactions.md)  
  
  
