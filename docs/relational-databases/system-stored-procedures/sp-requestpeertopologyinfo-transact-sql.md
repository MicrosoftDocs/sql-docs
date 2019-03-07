---
title: "sp_requestpeertopologyinfo (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_requestpeertopologyinfo"
  - "sp_requestpeertopologyinfo_TSQL"
helpviewer_keywords: 
  - "sp_requestpeertopologyinfo"
ms.assetid: 15cd28bd-5a72-41fb-ae1b-726baaa6fad5
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_requestpeertopologyinfo (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Populates the [MSpeer_topologyresponse](../../relational-databases/system-tables/mspeer-topologyresponse-transact-sql.md) system table with information about a peer-to-peer transactional replication topology. Execute [sp_gettopologyinfo](../../relational-databases/system-stored-procedures/sp-gettopologyinfo-transact-sql.md) to obtain information from the table in XML format.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_requestpeertopologyinfo [ @publication = ] 'publication'  
        [ ,[ @requestid=] request_id OUTPUT  
```  
  
## Arguments  
 [ @publication= ] '*publication*'  
 Is the name of the publication for which to perform a topology-wide status request. *publication* is **sysname**, with no default.  
  
 [ @request_id= ] *request_id*  
 Is the ID number that is assigned to the topology status request. *request_id* is **int**, with a default of NULL. This ID can be used by [sp_gettopologyinfo](../../relational-databases/system-stored-procedures/sp-gettopologyinfo-transact-sql.md).  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 sp_requestpeertopologyinfo is used in peer-to-peer transactional replication. Execute sp_requestpeertopologyinfo before executing [sp_gettopologyinfo](../../relational-databases/system-stored-procedures/sp-gettopologyinfo-transact-sql.md). These procedures are used by the Configure Peer-to-Peer Topology Wizard, but they can also be used directly if you require topology information in an XML format. If you prefer tabular results, query the [MSpeer_topologyresponse](../../relational-databases/system-tables/mspeer-topologyresponse-transact-sql.md) system table.  
  
## Permissions  
 Requires membership in the sysadmin fixed server role or db_owner fixed database role.  
  
## See Also  
 [Peer-to-Peer Transactional Replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
