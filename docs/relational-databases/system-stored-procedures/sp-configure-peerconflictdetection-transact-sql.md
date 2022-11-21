---
description: "sp_configure_peerconflictdetection (Transact-SQL)"
title: "sp_configure_peerconflictdetection (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 10/05/2021
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_configure_peerconflictdetection_TSQL"
  - "sp_configure_peerconflictdetection"
helpviewer_keywords: 
  - "sp_configure_peerconflictdetection"
ms.assetid: 45117cb2-3247-433f-ba3d-7fa19514b1c3
author: markingmyname
ms.author: maghan
---
# sp_configure_peerconflictdetection (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Configures conflict detection for a publication that is involved in a peer-to-peer transactional replication topology. For more information, see [Conflict Detection in Peer-to-Peer Replication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md). This stored procedure is executed at the Publisher on the publication database.  

> [!IMPORTANT]
> You can't use `sp_configure_peerconflictdetection` to enable `lastwriter`. To change the conflict resolution of an existing replication topology, drop the publication and recreate it.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_configure_peerconflictdetection [ @publication = ] 'publication'  
    [ , [ @action = ] 'action']  
    [ , [ @originator_id = ] originator_id ]  
    [ , [ @conflict_retention = ] conflict_retention ]  
    [ , [ @continue_onconflict = ] 'continue_onconflict']  
    [ , [ @local = ] 'local']  
    [ , [ @timeout = ] timeout ]  
  
```  
  
## Arguments  
 [ @publication=] '*publication*'  
 Is the name of the publication for which to configure conflict detection. *publication* is **sysname**, with no default.  
  
 [ @action= ] '*action*'  
 Specifies whether to enable or disable conflict detection for a publication. *action* is **nvarchar(5)**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**enable**|Enables conflict detection for a publication.|  
|**disable**|Disables conflict detection for a publication.|  
|NULL (default)||  
  
 [ @originator_id= ] *originator_id*  
 Specifies an ID for a node in a peer-to-peer topology. *originator_id* is **int**, with a default of NULL. This ID is used for conflict detection if *action* is set to **enable**. Specify a positive, nonzero ID that has never been used in the topology. For a list of IDs that have already been used, query the [Mspeer_originatorid_history](../../relational-databases/system-tables/mspeer-originatorid-history-transact-sql.md) system table.  
  
 [ @conflict_retention= ] *conflict_retention*  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
 [ @continue_onconflict= ] '*continue_onconflict*' ]  
 Determines whether the Distribution Agent continues to process changes after a conflict is detected. *continue_onconflict* is **nvarchar(5)** with a default value of FALSE.  
  
> [!CAUTION]  
>  We recommend that you use the default value of FALSE. When this option is set to TRUE, the Distribution Agent tries to converge data in the topology by applying the conflicting row from the node that has the highest originator ID. This method does not guarantee convergence. You should make sure that the topology is consistent after a conflict is detected. For more information, see "Handling Conflicts" in [Conflict Detection in Peer-to-Peer Replication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md).  
  
 [ @local= ] '*local*'  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
 [ @timeout= ] *timeout*  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 sp_configure_peerconflictdetection is used in peer-to-peer transactional replication. To use conflict detection, all nodes must be running [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or later versions; and detection must be enabled for all nodes.  
  
## Permissions  
 Requires membership in the sysadmin fixed server role or db_owner fixed database role.  
  
## See Also  
 [Conflict Detection in Peer-to-Peer Replication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md)   
 [Peer-to-Peer Transactional Replication](../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
