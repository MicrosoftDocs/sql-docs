---
title: "sp_replshowcmds (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_replshowcmds"
  - "sp_replshowcmds_TSQL"
helpviewer_keywords: 
  - "sp_replshowcmds"
ms.assetid: 199f5a74-e08e-4d02-a33c-b8ab0db20f44
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_replshowcmds (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the commands for transactions marked for replication in readable format. **sp_replshowcmds** can be run only when client connections (including the current connection) are not reading replicated transactions from the log. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_replshowcmds [ @maxtrans = ] maxtrans  
```  
  
## Arguments  
`[ @maxtrans = ] maxtrans`
 Is the number of transactions about which to return information. *maxtrans* is **int**, with a default of **1**, which specifies the maximum number of transactions pending replication for which **sp_replshowcmds** returns information.  
  
## Result Sets  
 **sp_replshowcmds** is a diagnostic procedure that returns information about the publication database from which it is executed.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**xact_seqno**|**binary(10)**|Sequence number of the command.|  
|**originator_id**|**int**|ID of the command originator, always **0**.|  
|**publisher_database_id**|**int**|ID of the Publisher database, always **0**.|  
|**article_id**|**int**|ID of the article.|  
|**type**|**int**|Type of command.|  
|**command**|**nvarchar(1024)**|[!INCLUDE[tsql](../../includes/tsql-md.md)] command.|  
  
## Remarks  
 **sp_replshowcmds** is used in transactional replication.  
  
 Using **sp_replshowcmds**, you can view transactions that currently are not distributed (those transactions remaining in the transaction log that have not been sent to the Distributor).  
  
 Clients that run **sp_replshowcmds** and **sp_replcmds** within the same database receive error 18752.  
  
 To avoid this error, the first client must disconnect or the role of the client as log reader must be released by executing **sp_replflush**. After all clients have disconnected from the log reader, **sp_replshowcmds** can be run successfully.  
  
> [!NOTE]  
>  **sp_replshowcmds** should be run only to troubleshoot problems with replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_replshowcmds**.  
  
## See Also  
 [Error Messages](../../relational-databases/native-client-odbc-error-messages/error-messages.md)   
 [sp_replcmds &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replcmds-transact-sql.md)   
 [sp_repldone &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-repldone-transact-sql.md)   
 [sp_replflush &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replflush-transact-sql.md)   
 [sp_repltrans &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-repltrans-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
