---
description: "sp_requestpeerresponse (Transact-SQL)"
title: "sp_requestpeerresponse (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_requestpeerresponse_TSQL"
  - "sp_requestpeerresponse"
helpviewer_keywords: 
  - "sp_requestpeerresponse"
ms.assetid: cbe13c22-4d7d-4a36-b194-7a13ce68ef27
author: markingmyname
ms.author: maghan
---
# sp_requestpeerresponse (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  When executed from a node in a peer-to-peer topology, this procedure requests a response from every other node in the topology. By executing this procedure and reviewing the corresponding responses, you can guarantee that all previous commands have been delivered to the responding nodes. This stored procedure is executed at the requesting node on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_requestpeerresponse [ @publication = ] 'publication'  
    [ , [ @description = ] 'description'  
    [ , [ @request_id = ] request_id OUTPUT ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication in a peer-to-peer topology for which the status is being verified. *publication* is **sysname**, with no default.  
  
`[ @description = ] 'description'`
 User-defined information that can be used to identify individual status requests. *description* is **nvarchar(4000)**, with a default of NULL.  
  
`[ @request_id = ] request_id`
 Returns the ID of the new request. *request_id* is **int** and is an OUTPUT parameter. This value can be used when executing [sp_helppeerresponses &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helppeerresponses-transact-sql.md) to view all responses to a status request.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_requestpeerresponse** is used in peer-to-peer transactional replication.  
  
 **sp_requestpeerresponse** is used to ensure that all commands have been received by all other nodes before restoring a database published in a peer-to-peer topology. It is also used when replicating data definition language (DDL) changes made while a node was offline to estimate when these changes arrive at the other nodes.  
  
 **sp_requestpeerresponse** cannot be executed within a user-defined transaction.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_requestpeerresponse**.  
  
## See Also  
 [sp_deletepeerrequesthistory &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-deletepeerrequesthistory-transact-sql.md)   
 [sp_helppeerrequests &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helppeerrequests-transact-sql.md)  
  
  
