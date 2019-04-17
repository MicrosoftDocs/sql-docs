---
title: "sp_articlecolumn (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_articlecolumn"
  - "sp_articlecolumn_TSQL"
helpviewer_keywords: 
  - "sp_articlecolumn"
ms.assetid: 8abaa8c1-d99e-4788-970f-c4752246c577
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_articlecolumn (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Used to specify columns included in an article to vertically filter data in a published table. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_articlecolumn [ @publication = ] 'publication'  
        , [ @article = ] 'article'  
    [ , [ @column = ] 'column' ]  
    [ , [ @operation = ] 'operation' ]  
    [ , [ @refresh_synctran_procs = ] refresh_synctran_procs ]  
    [ , [ @ignore_distributor = ] ignore_distributor ]  
    [ , [ @change_active = ] change_actve ]  
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]  
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]  
    [ , [ @publisher = ] 'publisher' ]  
    [ , [ @internal = ] 'internal' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication that contains this article. *publication* is **sysname**, with no default.  
  
`[ @article = ] 'article'`
 Is the name of the article. *article* is **sysname**, with no default.  
  
`[ @column = ] 'column'`
 Is the name of the column to be added or dropped. *column* is **sysname**, with a default of NULL. If NULL, all columns are published.  
  
`[ @operation = ] 'operation'`
 Specifies whether to add or drop columns in an article. *operation* is **nvarchar(5)**, with a default of add. **add** marks the column for replication. **drop** unmarks the column.  
  
`[ @refresh_synctran_procs = ] refresh_synctran_procs`
 Specifies whether the stored procedures supporting immediate updating subscriptions are regenerated to match the number of columns replicated. *refresh_synctran_procs* is **bit**, with a default of **1**. If **1**, the stored procedures are regenerated.  
  
`[ @ignore_distributor = ] ignore_distributor`
 Indicates if this stored procedure executes without connecting to the Distributor. *ignore_distributor* is **bit**, with a default of **0**. If **0**, the database must be enabled for publishing, and the article cache should be refreshed to reflect the new columns replicated by the article. If **1**, allows article columns to be dropped for articles that reside in an unpublished database; should be used only in recovery situations.  
  
`[ @change_active = ] change_active`
 Allows modifying the columns in publications that have subscriptions. *change_active* is an **int** with a default of **0**. If **0**, columns are not modified. If **1**, columns can be added or dropped from active articles that have subscriptions.  
  
`[ @force_invalidate_snapshot = ] force_invalidate_snapshot`
 Acknowledges that the action taken by this stored procedure may invalidate an existing snapshot. *force_invalidate_snapshot* is a **bit**, with a default of **0**.  
  
 **0** specifies that changes to the article do not cause the snapshot to be invalid. If the stored procedure detects that the change does require a new snapshot, an error occurs and no changes are made.  
  
 **1** specifies that changes to the article may cause the snapshot to be invalid, and if there are existing subscriptions that would require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and a new snapshot generated.  
  
 [**@force_reinit_subscription =** ] *force_reinit_subscription*  
 Acknowledges that the action taken by this stored procedure may require existing subscriptions to be reinitialized. *force_reinit_subscription* is a **bit**, with a default of **0**.  
  
 **0** specifies that changes to the article do not cause the subscription to be reinitialized. If the stored procedure detects that the change would require subscriptions to be reinitialized, an error occurs and no changes are made. **1** specifies that changes to the article cause existing subscriptions to be reinitialized, and gives permission for the subscription reinitialization to occur.  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used with a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
`[ @internal = ] 'internal'`
 Internal use only.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_articlecolumn** is used in snapshot replication and transactional replication.  
  
 Only an unsubscribed article can be filtered using **sp_articlecolumn**.  
  
## Example  
 [!code-sql[HowTo#sp_AddTranArticle](../../relational-databases/replication/codesnippet/tsql/sp-articlecolumn-transac_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_articlecolumn**.  
  
## See Also  
 [Define an Article](../../relational-databases/replication/publish/define-an-article.md)   
 [Define and Modify a Column Filter](../../relational-databases/replication/publish/define-and-modify-a-column-filter.md)   
 [Filter Published Data](../../relational-databases/replication/publish/filter-published-data.md)   
 [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md)   
 [sp_articleview &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-articleview-transact-sql.md)   
 [sp_changearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md)   
 [sp_droparticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droparticle-transact-sql.md)   
 [sp_helparticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helparticle-transact-sql.md)   
 [sp_helparticlecolumns &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helparticlecolumns-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
