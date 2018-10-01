---
title: "Transactions Event Category | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server event classes, Transactions event category"
  - "event classes [SQL Server], Transactions event category"
  - "Transactions event category [SQL Server]"
ms.assetid: bfc75c5b-7115-49d8-9148-a0c84ee66a9a
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Transactions Event Category
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The **Transactions** event classes can be used to monitor the status of transactions. The event class names that are prefixed with **TM:** are used to track the transaction-related operations that are sent through the transaction management interface.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[DTCTransaction Event Class](../../relational-databases/event-classes/dtctransaction-event-class.md)|Tracks transactions coordinated by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC). These are transactions distributed between two or more databases or instances of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].|  
|[SQLTransaction Event Class](../../relational-databases/event-classes/sqltransaction-event-class.md)|Tracks [!INCLUDE[tsql](../../includes/tsql-md.md)] BEGIN TRAN, COMMIT TRAN, SAVE TRAN, and ROLLBACK TRAN statements.|  
|[TM: Begin Tran Completed Event Class](../../relational-databases/event-classes/tm-begin-tran-completed-event-class.md)|Indicates that a BEGIN TRANSACTION request has completed.|  
|[TM: Begin Tran Starting Event Class](../../relational-databases/event-classes/tm-begin-tran-starting-event-class.md)|Indicates that a BEGIN TRANSACTION request is starting.|  
|[TM: Commit Tran Completed Event Class](../../relational-databases/event-classes/tm-commit-tran-completed-event-class.md)|Indicates that a COMMIT TRANSACTION request has completed.|  
|[TM: Commit Tran Starting Event Class](../../relational-databases/event-classes/tm-commit-tran-starting-event-class.md)|Indicates that a COMMIT TRANSACTION request is starting.|  
|[TM: Promote Tran Completed Event Class](../../relational-databases/event-classes/tm-promote-tran-completed-event-class.md)|Indicates that a PROMOTE TRANSACTION request has completed.|  
|[TM: Promote Tran Starting Event Class](../../relational-databases/event-classes/tm-promote-tran-starting-event-class.md)|Indicates that a PROMOTE TRANSACTION request is starting.|  
|[TM: Rollback Tran Completed Event Class](../../relational-databases/event-classes/tm-rollback-tran-completed-event-class.md)|Indicates that a ROLLBACK TRANSACTION request has completed.|  
|[TM: Rollback Tran Starting Event Class](../../relational-databases/event-classes/tm-rollback-tran-starting-event-class.md)|Indicates that a ROLLBACK TRANSACTION request is starting.|  
|[TM: Save Tran Completed Event Class](../../relational-databases/event-classes/tm-save-tran-completed-event-class.md)|Indicates that a SAVE TRANSACTION request has completed.|  
|[TM: Save Tran Starting Event Class](../../relational-databases/event-classes/tm-save-tran-starting-event-class.md)|Indicates that a SAVE TRANSACTION request is starting.|  
|[TransactionLog Event Class](../../relational-databases/event-classes/transactionlog-event-class.md)|Tracks when transactions are written to a database transaction log.|  
  
  
