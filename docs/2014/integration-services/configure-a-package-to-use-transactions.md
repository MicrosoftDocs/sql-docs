---
title: "Configure a Package to Use Transactions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "transactions [Integration Services], configuring packages to use"
ms.assetid: 8bf14957-27b4-456b-81d9-e1a0e0ca94b7
author: janinezhang
ms.author: janinez
manager: craigg
---
# Configure a Package to Use Transactions
  When you configure a package to use transactions, you have two options:  
  
-   Have a single transaction for the package. In this case, it is the package itself that *initiates* this transaction, whereas individual tasks and containers in the package participate in this single transaction.  
  
-   Have multiple transactions in the package. In this case, the package supports transactions, but individual tasks and containers in the package actually initiate the transactions.  
  
 The following procedures describe how to configure both options.  
  
## Configuring a Single Transaction  
 In this option, the package itself initiates a single transaction. You configure the package to initiate this transaction by setting the TransactionOption property of the package to `Required`.  
  
 Next, you enlist specific tasks and containers in this single transaction. To enlist a task or container in a transaction, you set the TransactionOption property of that task or container to `Supported`.  
  
#### To configure a package to use a single transaction  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want to configure to use a transaction.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  Right-click anywhere in the background of the control flow design surface, and then click **Properties**.  
  
5.  In the **Properties** window, set the TransactionOption property to `Required`.  
  
6.  On the design surface of the **ControlFlow** tab, right-click the task or the container that you want to enroll in the transaction, and then click **Properties**.  
  
7.  In the **Properties** window, set the TransactionOption property to `Supported`.  
  
    > [!NOTE]  
    >  To enlist a connection in a transaction, enroll the tasks that use the connection in the transaction. For more information, see [Integration Services &#40;SSIS&#41; Connections](connection-manager/integration-services-ssis-connections.md).  
  
8.  Repeat steps 6 and 7 for each task and container that you want to enroll in the transaction.  
  
## Configuring Multiple Transactions  
 In this option, the package itself supports transactions but does not start a transaction. You configure the package to support transactions by setting the TransactionOption property of the package to `Supported`.  
  
 Next, you configure the desired tasks and containers inside the package to initiate or participate in transactions. To configure a task or container to initiate a transaction, you set the TransactionOption property of that task or container to `Required`.  
  
#### To configure a package to use multiple transactions  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want to configure to use transaction.s  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  Right-click anywhere in the background of the control flow design surface, and then click **Properties**.  
  
5.  In the **Properties** window, set the TransactionOption property to `Supported`.  
  
    > [!NOTE]  
    >  The package supports transactions, but the transactions are started by task or containers in the package.  
  
6.  On the design surface of the **ControlFlow** tab, right-click the task or the container in the package for which you want to start a transaction, and then click **Properties**.  
  
7.  In the **Properties** window, set the TransactionOption property to `Required`.  
  
8.  If a transaction is started by a container, right-click the task or the container that you want to enroll in the transaction, and then click **Properties**.  
  
9. In the **Properties** window, set the TransactionOption property to `Supported`.  
  
    > [!NOTE]  
    >  To enlist a connection in a transaction, enroll the tasks that use the connection in the transaction. For more information, see [Integration Services &#40;SSIS&#41; Connections](connection-manager/integration-services-ssis-connections.md).  
  
10. Repeat steps 6 through 9 for each task and container that starts a transaction.  
  
## See Also  
 [Integration Services Transactions](integration-services-transactions.md)  
  
  
