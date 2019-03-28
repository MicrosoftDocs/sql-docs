---
title: "sp_setsubscriptionxactseqno (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_setsubscriptionxactseqno"
  - "sp_setsubscriptionxactseqno_TSQL"
helpviewer_keywords: 
  - "sp_setsubscriptionxactseqno"
ms.assetid: cdb4e0ba-5370-4905-b03f-0b0c6f080ca6
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_setsubscriptionxactseqno (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Used during troubleshooting to specify the log sequence number (LSN) of the next transaction to be applied by the Distribution Agent at the Subscriber, which enables the agent to skip a failed transaction. This stored procedure is executed at the Subscriber on the subscription database. Not supported for non-SQL Server Subscribers.  
  
> [!CAUTION]  
>  Using this stored procedure incorrectly or specifying an incorrect LSN value can cause the Distribution Agent to revert changes that were already applied at the Subscriber or skip over all remaining changes.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_setsubscriptionxactseqno [ @publisher = ] 'publisher'  
        , [ @publisher_db = ] 'publisher_db'  
        , [ @publication = ] 'publication'  
        , [ @xact_seqno = ] xact_seqno   
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the publication database. *publisher_db* is **sysname**, with no default. For a non-SQL Server Publisher, *publisher_db* is the name of the distribution database.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default. When the Distribution Agent is shared by more than one publication, you must specify a value of ALL for *publication*.  
  
`[ @xact_seqno = ] xact_seqno`
 Is the LSN of the next transaction at the Distributor to be applied at the Subscriber. *xact_seqno* is **varbinary(16)**, with no default.  
  
## Result Set  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**ORIGINAL XACT_SEQNO**|**varbinary(16)**|The original LSN of the next transaction to be applied at the Subscriber.|  
|**UPDATED XACT_SEQNO**|**varbinary(16)**|The updated LSN of the next transaction to be applied at the Subscriber.|  
|**SUBSCRIPTION STREAM COUNT**|**int**|The number of subscription streams used during the last synchronization.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_setsubscriptionxactseqno** is used in transactional replication.  
  
 **sp_setsubscriptionxactseqno** cannot be used in a peer-to-peer transactional replication topology.  
  
 **sp_setsubscriptionxactseqno** can be used to skip a specific transaction that is causing an error when applies at the Subscriber. When there is a failure and after the Distribution Agent stops, call [sp_helpsubscriptionerrors &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscriptionerrors-transact-sql.md) at the Distributor to retrieve the xact_seqno value of the failed transaction, and then call **sp_setsubscriptionxactseqno**, passing this value for *xact_seqno*. This will ensure that only the commands after this LSN will be processed.  
  
 Specify a value of **0** for *xact_seqno* to deliver all pending commands in the distribution database to the Subscriber.  
  
 **sp_setsubscriptionxactseqno** may fail if the Distribution Agent uses multi-subscription streams.  
  
 When this error occurs, you must run the Distribution Agent with a single subscription stream. For more information, see [Replication Distribution Agent](../../relational-databases/replication/agents/replication-distribution-agent.md).  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_setsubscriptionxactseqno**.  
  
  
