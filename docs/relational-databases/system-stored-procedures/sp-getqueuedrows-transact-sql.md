---
title: "sp_getqueuedrows (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_getqueuedrows_TSQL"
  - "sp_getqueuedrows"
helpviewer_keywords: 
  - "sp_getqueuedrows"
ms.assetid: 139e834f-1988-4b4d-ac81-db1f89ea90e8
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_getqueuedrows (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Retrieves rows at the Subscriber that have updates pending in the queue. This stored procedure is executed at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_getqueuedrows [ @tablename = ] 'tablename'  
    [ , [ @owner = ] 'owner'  
    [ , [ @tranid = ] 'transaction_id' ]  
```  
  
## Arguments  
 [ **@tablename =**] **'***tablename***'**  
 Is the name of the table. *tablename* is **sysname**, with no default. The table must be a part of a queued subscription.  
  
 [ **@owner =**] **'***owner***'**  
 Is the subscription owner. *owner* is **sysname**, with a default of NULL.  
  
 [ **@tranid =** ] **'***transaction_id***'**  
 Allows the output to be filtered by the transaction ID. *transaction_id* is **nvarchar(70)**, with a default of NULL. If specified, the transaction ID associated with the queued command is displayed. If NULL, all the commands in the queue are displayed.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 Shows all rows that currently have at least one queued transaction for the subscribed table.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**Action**|**nvarchar(10)**|Type of action to be taken when synchronization occurs.<br /><br /> INS= insert<br /><br /> DEL = delete<br /><br /> UPD = update|  
|**Tranid**|**nvarchar(70)**|Transaction ID that the command was executed under.|  
|**table column1...n**||The value for each column of the table specified in *tablename*.|  
|**msrepl_tran_version**|**uniqueidentifier**|This column is used for tracking changes to replicated data and to perform conflict detection at the Publisher. This column is added to the table automatically.|  
  
## Remarks  
 **sp_getqueuedrows** is used at Subscribers participating in queued updating.  
  
 **sp_getqueuedrows** finds rows of a given table on a subscription database that have participated in a queued update, yet currently have not been resolved by the queue reader agent.  
  
## Permissions  
 **sp_getqueuedrows** requires SELECT permissions on the table specified in *tablename*.  
  
## See Also  
 [Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md)   
 [Queued Updating Conflict Detection and Resolution](../../relational-databases/replication/transactional/updatable-subscriptions-queued-updating-conflict-resolution.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
