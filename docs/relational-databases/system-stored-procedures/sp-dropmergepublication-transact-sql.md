---
description: "sp_dropmergepublication (Transact-SQL)"
title: "sp_dropmergepublication (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_dropmergepublication"
  - "sp_dropmergepublication_TSQL"
helpviewer_keywords: 
  - "sp_dropmergepublication"
ms.assetid: 9e1cb96e-5889-4f97-88cd-f60cf313ce68
author: markingmyname
ms.author: maghan
---
# sp_dropmergepublication (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Drops a merge publication and its associated Snapshot Agent. All subscriptions must be dropped before dropping a merge publication. The articles in the publication are dropped automatically. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dropmergepublication [ @publication= ] 'publication'   
    [ , [ @ignore_distributor = ] ignore_distributor ]   
    [ , [ @reserved = ] reserved ]  
    [ , [ @ignore_merge_metadata = ] ignore_merge_metadata ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication to drop. *publication* is **sysname**, with no default. If **all**, all existing merge publications are removed as well as the Snapshot Agent job associated with them. If you specify a particular value for *publication*, only that publication and its associated Snapshot Agent job are dropped.  
  
`[ @ignore_distributor = ] ignore_distributor`
 Used to drop a publication without doing cleanup tasks at the Distributor. *ignore_distributor* is **bit**, with a default of **0**. This parameter is also used when reinstalling the Distributor.  
  
`[ @reserved = ] reserved`
 Is reserved for future use. *reserved* is **bit**, with a default of **0**.  
  
`[ @ignore_merge_metadata = ] ignore_merge_metadata`
 Internal use only.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_dropmergepublication** is used in merge replication.  
  
 **sp_dropmergepublication** recursively drops all articles that are associated with a publication and then drops the publication itself. A publication cannot be removed if it has one or more subscriptions to it. For information about how to remove subscriptions, see [Delete a Push Subscription](../../relational-databases/replication/delete-a-push-subscription.md) and [Delete a Pull Subscription](../../relational-databases/replication/delete-a-pull-subscription.md).  
  
 Executing **sp_dropmergepublication** to drop a publication does not remove published objects from the publication database or the corresponding objects from the subscription database. Use DROP \<object> to remove these objects manually if necessary.  
  
## Example  
 [!code-sql[HowTo#sp_dropmergepublication](../../relational-databases/replication/codesnippet/tsql/sp-dropmergepublication-_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_dropmergepublication**.  
  
## See Also  
 [Delete a Publication](../../relational-databases/replication/publish/delete-a-publication.md)   
 [sp_addmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md)   
 [sp_changemergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md)   
 [sp_helpmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
