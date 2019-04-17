---
title: "sys.dm_tran_transactions_snapshot (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_tran_transactions_snapshot"
  - "dm_tran_transactions_snapshot"
  - "sys.dm_tran_transactions_snapshot_TSQL"
  - "dm_tran_transactions_snapshot_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_tran_transactions_snapshot dynamic management view"
ms.assetid: 03f64883-07ad-4092-8be0-31973348c647 
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_tran_transactions_snapshot (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns a virtual table for the **sequence_number** of transactions that are active when each snapshot transaction starts. The information that is returned by this view can you help you do the following:  
  
-   Find the number of currently active snapshot transactions.  
  
-   Identify data modifications that are ignored by a particular snapshot transaction. For a transaction that is active when a snapshot transaction starts, all data modifications by that transaction, even after that transaction commits, are ignored by the snapshot transaction.  
  
 For example, consider the following output from **sys.dm_tran_transactions_snapshot**:  
  
```  
transaction_sequence_num snapshot_id snapshot_sequence_num  
------------------------ ----------- ---------------------  
59                       0           57  
59                       0           58  
60                       0           57  
60                       0           58  
60                       0           59  
60                       3           57  
60                       3           58  
60                       3           59  
60                       3           60  
```  
  
 The `transaction_sequence_num` column identifies the transaction sequence (XSN) number of the current snapshot transactions. The output shows two: `59` and `60`. The `snapshot_sequence_num` column identifies the transaction sequence number of the transactions that are active when each snapshot transaction starts.  
  
 The output shows that snapshot transaction XSN-59 starts while two active transactions, XSN-57 and XSN-58, are running. If XSN-57 or XSN-58 makes data modifications, XSN-59 ignores the changes and uses row versioning to maintain a transactionally consistent view of the database.  
  
 Snapshot transaction XSN-60 ignores data modifications made by XSN-57 and XSN-58 and also XSN 59.  
  
## Syntax  
  
```  
  
dm_tran_transactions_snapshot  
```  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**transaction_sequence_num**|**bigint**|Transaction sequence number (XSN) of a snapshot transaction.|  
|**snapshot_id**|**int**|Snapshot ID for each [!INCLUDE[tsql](../../includes/tsql-md.md)] statement started under read-committed using row versioning. This value is used to generate a transactionally consistent view of the database supporting each query that is being run under read-committed using row versioning.|  
|**snapshot_sequence_num**|**bigint**|Transaction sequence number of a transaction that was active when the snapshot transaction started.|  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   
  
## Remarks  
 When a snapshot transaction starts, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] records all of the transactions that are active at that time. **sys.dm_tran_transactions_snapshot** reports this information for all currently active snapshot transactions.  
  
 Each transaction is identified by a transaction sequence number that is assigned when the transaction begins. Transactions start at the time a BEGIN TRANSACTION or BEGIN WORK statement is executed. However, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] assigns the transaction sequence number with the execution of the first [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that accesses data after the BEGIN TRANSACTION or BEGIN WORK statement. The transaction sequence numbers are incremented by one.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Transaction Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/transaction-related-dynamic-management-views-and-functions-transact-sql.md)  
  
  

