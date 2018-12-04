---
title: "sp_replsetoriginator (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_replsetoriginator"
  - "sp_replsetoriginator_TSQL"
helpviewer_keywords: 
  - "sp_replsetoriginator"
ms.assetid: 030e5226-0585-439f-b8cd-36f48367d86d
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_replsetoriginator (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Used to invoke loopback detection and handling in bidirectional transactional replication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_replsetoriginator [ @server_name= ] 'server_name'   
    , [ @database_name= ] 'database_name'  
```  
  
## Arguments  
 [ **@server_name=**] **'***server_name***'**  
 Is the name of the server where the transaction is being applied. *originating_server* is **sysname**, with no default.  
  
 [ **@database_name=**] **'***database_name***'**  
 Is the name of the database where the transaction is being applied. *originating_db* is **sysname**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_replsetoriginator** is executed by the Distribution Agent to record the source of transactions applied by replication. This information is used to invoke loopback detection for bidirectional transactional subscriptions that have the loopback property set.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role at the Publisher, members of the **db_owner** fixed database role on the publication database, or users in the publication access list (PAL) can execute **sp_replsetoriginator**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
