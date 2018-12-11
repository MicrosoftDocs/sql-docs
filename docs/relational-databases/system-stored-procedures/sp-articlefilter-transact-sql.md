---
title: "sp_articlefilter (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_articlefilter_TSQL"
  - "sp_articlefilter"
helpviewer_keywords: 
  - "sp_articlefilter"
ms.assetid: 4c3fee32-a43f-4757-a029-30aef4696afb
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_articlefilter (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Filters data that are published based on a table article. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_articlefilter [ @publication = ] 'publication'  
        , [ @article = ] 'article'  
    [ , [ @filter_name = ] 'filter_name' ]  
    [ , [ @filter_clause = ] 'filter_clause' ]  
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]  
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
 [ **@publication=**] **'**_publication_**'**  
 Is the name of the publication that contains the article. *publication* is **sysname**, with no default.  
  
 [ **@article=**] **'**_article_**'**  
 Is the name of the article. *article* is **sysname**, with no default.  
  
 [ **@filter_name=**] **'**_filter_name_**'**  
 Is the name of the filter stored procedure to be created from the *filter_name*. *filter_name* is **nvarchar(386)**, with a default of NULL. You must specify a unique name for the article filter.  
  
 [ **@filter_clause=**] **'**_filter_clause_**'**  
 Is a restriction (WHERE) clause that defines a horizontal filter. When entering the restriction clause, omit the keyword WHERE. *filter_clause* is **ntext**, with a default of NULL.  
  
 [ **@force_invalidate_snapshot =** ] *force_invalidate_snapshot*  
 Acknowledges that the action taken by this stored procedure may invalidate an existing snapshot. *force_invalidate_snapshot* is a **bit**, with a default of **0**.  
  
 **0** specifies that changes to the article do not cause the snapshot to be invalid. If the stored procedure detects that the change does require a new snapshot, an error occurs and no changes are made.  
  
 **1** specifies that changes to the article may cause the snapshot to be invalid, and if there are existing subscriptions that would require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and a new snapshot generated.  
  
 [ **@force_reinit_subscription =** ] *force_reinit_subscription*  
 Acknowledges that the action taken by this stored procedure may require existing subscriptions to be reinitialized. *force_reinit_subscription* is a **bit**, with a default of **0**.  
  
 **0** specifies that changes to the article do not cause a need for subscriptions to be reinitialized. If the stored procedure detects that the change would require subscriptions to be reinitialized, an error occurs and no changes are made.  
  
 **1** specifies that changes to the article causes existing subscriptions to be reinitialized, and gives permission for the subscription reinitialization to occur.  
  
 [ **@publisher=** ] **'**_publisher_**'**  
 Specifies a non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used with a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_articlefilter** is used in snapshot replication and transactional replication.  
  
 Executing **sp_articlefilter** for an article with existing subscriptions requires that those subscriptions to be reinitialized.  
  
 **sp_articlefilter** creates the filter, inserts the ID of the filter stored procedure in the **filter** column of the [sysarticles &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sysarticles-transact-sql.md) table, and then inserts the text of the restriction clause in the **filter_clause** column.  
  
 To create an article with a horizontal filter, execute [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) with no *filter* parameter. Execute **sp_articlefilter**, providing all parameters including *filter_clause*, and then execute [sp_articleview &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-articleview-transact-sql.md), providing all parameters including the identical *filter_clause*. If the filter already exists and if the **type** in **sysarticles** is **1** (log-based article), the previous filter is deleted and a new filter is created.  
  
 If *filter_name* and *filter_clause* are not provided, the previous filter is deleted and the filter ID is set to **0**.  
  
## Example  
 [!code-sql[HowTo#sp_AddTranArticle](../../relational-databases/replication/codesnippet/tsql/sp-articlefilter-transac_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_articlefilter**.  
  
## See Also  
 [Define an Article](../../relational-databases/replication/publish/define-an-article.md)   
 [Define and Modify a Static Row Filter](../../relational-databases/replication/publish/define-and-modify-a-static-row-filter.md)   
 [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md)   
 [sp_articleview &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-articleview-transact-sql.md)   
 [sp_changearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md)   
 [sp_droparticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droparticle-transact-sql.md)   
 [sp_helparticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helparticle-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
