---
description: "sp_reinitsubscription (Transact-SQL)"
title: "sp_reinitsubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_reinitsubscription"
  - "sp_reinitsubscription_TSQL"
helpviewer_keywords: 
  - "sp_reinitsubscription"
ms.assetid: d56ae218-6128-4ff9-b06c-749914505c7b
author: markingmyname
ms.author: maghan
---
# sp_reinitsubscription (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Marks the subscription for reinitialization. This stored procedure is executed at the Publisher for push subscriptions.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_reinitsubscription [ [ @publication = ] 'publication' ]  
    [ , [ @article = ] 'article' ]  
        , [ @subscriber = ] 'subscriber'  
    [ , [ @destination_db = ] 'destination_db']  
    [ , [ @for_schema_change = ] 'for_schema_change']  
    [ , [ @publisher = ] 'publisher' ]  
    [ , [ @ignore_distributor_failure = ] ignore_distributor_failure ]   
    [ , [ @invalidate_snapshot = ] invalidate_snapshot ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with a default of all.  
  
`[ @article = ] 'article'`
 Is the name of the article. *article* is **sysname**, with a default of all. For an immediate-updating publication, *article* must be **all**; otherwise, the stored procedure skips the publication and reports an error.  
  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber. *subscriber* is **sysname**, with no default.  
  
`[ @destination_db = ] 'destination_db'`
 Is the name of the destination database. *destination_db* is **sysname**, with a default of all.  
  
`[ @for_schema_change = ] 'for_schema_change'`
 Indicates whether reinitialization occurs as a result of a schema change at the publication database. *for_schema_change* is **bit**, with a default of 0. If **0**, active subscriptions for publications that allow immediate updating are reactivated as long as the whole publication, and not just some of its articles, are reinitialized. This means that the reinitialization is being initiated as a result of schema changes. If **1**, active subscriptions are not reactivated until the Snapshot Agent runs.  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.  
  
`[ @ignore_distributor_failure = ] ignore_distributor_failure`
 Allows reinitialization even if the Distributor does not exist or is offline. *ignore_distributor_failure* is **bit**, with a default of 0. If **0**, reinitialization fails if the Distributor does not exist or is offline.  
  
`[ @invalidate_snapshot = ] invalidate_snapshot`
 Invalidates the existing publication snapshot. *invalidate_snapshot* is **bit**, with a default of 0. If **1**, a new snapshot is generated for the publication.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_reinitsubscription** is used in transactional replication.  
  
 **sp_reinitsubscription** is not supported for peer-to-peer transactional replication.  
  
 For subscriptions where the initial snapshot is applied automatically and where the publication does not allow updatable subscriptions, the Snapshot Agent must be run after this stored procedure is executed so that schema and bulk copy program files are prepared and the Distribution Agents is then able to resynchronize the subscriptions.  
  
 For subscriptions where the initial snapshot is applied automatically and the publication allows updatable subscriptions, the Distribution Agent resynchronizes the subscription using the most recent schema and bulk copy program files previously created by the Snapshot Agent. The Distribution Agent resynchronizes the subscription immediately after the user executes **sp_reinitsubscription**, if the Distribution Agent is not busy; otherwise, synchronization may occur after the message interval (specified by Distribution Agent command-prompt parameter: **MessageInterval**).  
  
 **sp_reinitsubscription** has no effect on subscriptions where the initial snapshot is applied manually.  
  
 To resynchronize anonymous subscriptions to a publication, pass in **all** or NULL as *subscriber*.  
  
 Transactional replication supports subscription reinitialization at the article level. The snapshot of the article is reapplied at the Subscriber during the next synchronization after the article is marked for reinitialization. However, if there are dependent articles that are also subscribed to by the same Subscriber, reapplying the snapshot on the article might fail unless dependent articles in the publication are also automatically reinitialized under certain circumstances:  
  
-   If the pre-creation command on the article is 'drop', articles for schema-bound views and schema-bound stored procedures on the base object of that article is marked for reinitialization as well.  
  
-   If the schema option on the article includes scripting of declared referential integrity on the primary keys, articles that have base tables with foreign key relationships to base tables of the reinitialized article are marked for reinitialization as well.  
  
## Example  
 [!code-sql[HowTo#sp_reinittranpushsub](../../relational-databases/replication/codesnippet/tsql/sp-reinitsubscription-tr_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, members of the **db_owner** fixed database role, or the creator of the subscription can execute **sp_reinitsubscription**.  
  
## See Also  
 [Reinitialize a Subscription](../../relational-databases/replication/reinitialize-a-subscription.md)   
 [Reinitialize Subscriptions](../../relational-databases/replication/reinitialize-subscriptions.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
