---
title: "sp_repldone (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_repldone"
  - "sp_repldone_TSQL"
helpviewer_keywords: 
  - "sp_repldone"
ms.assetid: 045d3cd1-712b-44b7-a56a-c9438d4077b9
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_repldone (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Updates the record that identifies the last distributed transaction of the server. This stored procedure is executed at the Publisher on the publication database.  
  
> [!CAUTION]  
>  If you execute **sp_repldone** manually, you can invalidate the order and consistency of delivered transactions. **sp_repldone** should only be used for troubleshooting replication as directed by an experienced replication support professional.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_repldone [ @xactid= ] xactid   
        , [ @xact_seqno= ] xact_seqno   
    [ , [ @numtrans= ] numtrans ]   
    [ , [ @time= ] time   
    [ , [ @reset= ] reset ]  
```  
  
## Arguments  
`[ @xactid = ] xactid`
 Is the log sequence number (LSN) of the first record for the last distributed transaction of the server. *xactid* is **binary(10)**, with no default.  
  
`[ @xact_seqno = ] xact_seqno`
 Is the LSN of the last record for the last distributed transaction of the server. *xact_seqno* is **binary(10)**, with no default.  
  
`[ @numtrans = ] numtrans`
 Is the number of transactions distributed. *numtrans* is **int**, with no default.  
  
`[ @time = ] time`
 Is the number of milliseconds, if provided, needed to distribute the last batch of transactions. *time* is **int**, with no default.  
  
`[ @reset = ] reset`
 Is the reset status. *reset* is **int**, with no default. If **1**, all replicated transactions in the log are marked as distributed. If **0**, the transaction log is reset to the first replicated transaction and no replicated transactions are marked as distributed. *reset* is valid only when both *xactid* and *xact_seqno* are NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_repldone** is used in transactional replication.  
  
 **sp_repldone** is used by the log reader process to track which transactions have been distributed.  
  
 With **sp_repldone**, you can manually tell the server that a transaction has been replicated (sent to the Distributor). It also allows you to change the transaction marked as the next one awaiting replication. You can move forward or backward in the list of replicated transactions. (All transactions less than or equal to that transaction are marked as distributed.)  
  
 The required parameters *xactid* and *xact_seqno* can be obtained by using **sp_repltrans** or **sp_replcmds**.  
  
## Permissions  
 Members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_repldone**.  
  
## Examples  
 When *xactid* is NULL, *xact_seqno* is NULL, and *reset* is **1**, all replicated transactions in the log are marked as distributed. This is useful when there are replicated transactions in the transaction log that are no longer valid and you want to truncate the log, for example:  
  
```  
EXEC sp_repldone @xactid = NULL, @xact_segno = NULL, @numtrans = 0,     @time = 0, @reset = 1  
```  
  
> [!CAUTION]  
>  This procedure can be used in emergency situations to allow truncation of the transaction log when transactions pending replication are present.  
  
## See Also  
 [sp_replcmds &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replcmds-transact-sql.md)   
 [sp_replflush &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replflush-transact-sql.md)   
 [sp_repltrans &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-repltrans-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
