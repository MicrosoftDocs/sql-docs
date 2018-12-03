---
title: "sp_replcmds (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_replcmds_TSQL"
  - "sp_replcmds"
helpviewer_keywords: 
  - "sp_replcmds"
ms.assetid: 7e932f80-cc6e-4109-8db4-2b7c8828df73
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_replcmds (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the commands for transactions marked for replication. This stored procedure is executed at the Publisher on the publication database.  
  
> [!IMPORTANT]  
>  The **sp_replcmds** procedure should be run only to troubleshoot problems with replication.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_replcmds [ @maxtrans = ] maxtrans  
```  
  
## Arguments  
 [ **@maxtrans=**]  *maxtrans*  
 Is the number of transactions to return information about. *maxtrans* is **int**, with a default of **1**, which specifies the next transaction waiting for distribution.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**article id**|**int**|The ID of the article.|  
|**partial_command**|**bit**|Indicates whether this is a partial command or not.|  
|**command**|**varbinary(1024)**|The command value.|  
|**xactid**|**binary(10)**|Transaction ID.|  
|**xact_seqno**|**varbinary(16)**|The transaction sequence number.|  
|**publication_id**|**int**|The ID of the publication.|  
|**command_id**|**int**|ID of the command in [MSrepl_commands](../../relational-databases/system-tables/msrepl-commands-transact-sql.md).|  
|**command_type**|**int**|Type of command.|  
|**originator_srvname**|**sysname**|Server where the transaction originated.|  
|**originator_db**|**sysname**|Database where the transaction originated.|  
|**pkHash**|**int**|Internal use only.|  
|**originator_publication_id**|**int**|ID of the publication where the transaction originated.|  
|**originator_db_version**|**int**|Version of the database where the transaction originated.|  
|**originator_lsn**|**varbinary(16)**|Identifies the log sequence number (LSN) for the command in the originating publication.|  
  
## Remarks  
 **sp_replcmds** is used by the log reader process in transactional replication.  
  
 Replication treats the first client that runs **sp_replcmds** within a given database as the log reader.  
  
 This procedure can generate commands for owner-qualified tables or not qualify the table name (the default). Adding qualified table names allows replication of data from tables owned by a specific user in one database to tables owned by the same user in another database.  
  
> [!NOTE]  
>  Because the table name in the source database is qualified by the owner name, the owner of the table in the target database must be the same owner name.  
  
 Clients who attempt to run **sp_replcmds** within the same database receive error 18752 until the first client disconnects. After the first client disconnects, another client can run **sp_replcmds**, and becomes the new log reader.  
  
 A warning message number 18759 is added to both the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows application log if **sp_replcmds** is unable to replicate a text command because the text pointer was not retrieved in the same transaction.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_replcmds**.  
  
## See Also  
 [Error Messages](../../relational-databases/native-client-odbc-error-messages/error-messages.md)   
 [sp_repldone &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-repldone-transact-sql.md)   
 [sp_replflush &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replflush-transact-sql.md)   
 [sp_repltrans &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-repltrans-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
