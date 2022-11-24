---
title: "sp_droppublication (Transact-SQL) | Microsoft Docs"
description: Drops a publication and its associated Snapshot Agent. This stored procedure runs at the Publisher on the publication database.
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_droppublication_TSQL"
  - "sp_droppublication"
helpviewer_keywords: 
  - "sp_droppublication"
ms.assetid: b52b37e6-4fec-40cf-abba-7dce4ff395fd
author: markingmyname
ms.author: maghan
---
# sp_droppublication (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Drops a publication and its associated Snapshot Agent. All subscriptions must be dropped before dropping a publication. The articles in the publication are dropped automatically. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_droppublication [ @publication= ] 'publication'   
    [ , [ @ignore_distributor = ] ignore_distributor ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication to be dropped. *publication* is **sysname**, with no default. If **all** is specified, all publications are dropped from the publication database, except for those with subscriptions.  
  
`[ @ignore_distributor = ] ignore_distributor`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_droppublication** is used in snapshot replication and transactional replication.  
  
 **sp_droppublication** recursively drops all articles associated with a publication and then drops the publication itself. A publication cannot be removed if it has one or more subscriptions to it. For information about how to remove subscriptions, see [Delete a Push Subscription](../../relational-databases/replication/delete-a-push-subscription.md) and [Delete a Pull Subscription](../../relational-databases/replication/delete-a-pull-subscription.md).  
  
 Executing **sp_droppublication** to drop a publication does not remove published objects from the publication database or the corresponding objects from the subscription database. Use DROP \<object> to remove these objects manually if necessary.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_droppublication**.  
  
## Examples  
 [!code-sql[HowTo#sp_droppublication](../../relational-databases/replication/codesnippet/tsql/sp-droppublication-trans_1.sql)]  
  
## See Also  
 [Delete a Publication](../../relational-databases/replication/publish/delete-a-publication.md)   
 [sp_addpublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md)   
 [sp_changepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md)   
 [sp_helppublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helppublication-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
