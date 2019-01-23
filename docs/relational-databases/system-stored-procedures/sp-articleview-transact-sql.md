---
title: "sp_articleview (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_articleview"
  - "sp_articleview_TSQL"
helpviewer_keywords: 
  - "sp_articleview"
ms.assetid: a3d63fd6-f360-4a2f-8a82-a0dc15f650b3
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_articleview (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates the view that defines the published article when a table is filtered vertically or horizontally. This view is used as the filtered source of the schema and data for the destination tables. Only unsubscribed articles can be modified by this stored procedure. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_articleview [ @publication = ] 'publication'  
        , [ @article = ] 'article'  
    [ , [ @view_name = ] 'view_name']  
    [ , [ @filter_clause = ] 'filter_clause']  
    [ , [ @change_active = ] change_active ]  
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]  
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]  
    [ , [ @publisher = ] 'publisher' ]  
    [ , [ @refreshsynctranprocs = ] refreshsynctranprocs ]  
    [ , [ @internal = ] internal ]  
```  
  
## Arguments  
 [ **@publication=**] **'**_publication_**'**  
 Is the name of the publication that contains the article. *publication* is **sysname**, with no default.  
  
 [ **@article=**] **'**_article_**'**  
 Is the name of the article. *article* is **sysname**, with no default.  
  
 [ **@view_name=**] **'**_view_name_**'**  
 Is the name of the view that defines the published article. *view_name* is **nvarchar(386)**, with a default of NULL.  
  
 [ **@filter_clause=**] **'**_filter_clause_**'**  
 Is a restriction (WHERE) clause that defines a horizontal filter. When entering the restriction clause, omit the WHERE keyword. *filter_clause* is **ntext**, with a default of NULL.  
  
 [ **@change_active =** ] *change_active*  
 Allows modifying the columns in publications that have subscriptions. *change_active* is an **int**, with a default of **0**. If **0**, columns are not changed. If **1**, views can be created or re-created on active articles that have subscriptions.  
  
 [ **@force_invalidate_snapshot =** ] *force_invalidate_snapshot*  
 Acknowledges that the action taken by this stored procedure may invalidate an existing snapshot. *force_invalidate_snapshot* is a **bit**, with a default of **0**.  
  
 **0** specifies that changes to the article do not cause the snapshot to be invalid. If the stored procedure detects that the change does require a new snapshot, an error occurs and no changes are made.  
  
 **1** specifies that changes to the article may cause the snapshot to be invalid, and if there are existing subscriptions that would require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and a new snapshot generated.  
  
 [ **@force_reinit_subscription = ]** _force_reinit_subscription_  
 Acknowledges that the action taken by this stored procedure may require existing subscriptions to be reinitialized. *force_reinit_subscription* is a **bit** with a default of **0**.  
  
 **0** specifies that changes to the article do not cause the subscription to be reinitialized. If the stored procedure detects that the change would require subscriptions to be reinitialized, an error occurs and no changes are made.  
  
 **1** specifies that changes to the article causes existing subscription to be reinitialized, and gives permission for the subscription reinitialization to occur.  
  
 [ **@publisher**= ] **'**_publisher_**'**  
 Specifies a non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used when publishing from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
 [ **@refreshsynctranprocs** = ] *refreshsynctranprocs*  
 Is if the stored procedures used to synchronize replication are automatically recreated. *refreshsynctranprocs* is **bit**, with a default of 1.  
  
 **1** means that the stored procedures are re-created.  
  
 **0** means that the stored procedures are not re-created.  
  
 [ **@internal**= ] *internal*  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_articleview** creates the view that defines the published article and inserts the ID of this view in the **sync_objid** column of the [sysarticles &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sysarticles-transact-sql.md) table, and inserts the text of the restriction clause in the **filter_clause** column. If all columns are replicated and there is no **filter_clause**, the **sync_objid** in the [sysarticles &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sysarticles-transact-sql.md) table is set to the ID of the base table, and the use of **sp_articleview** is not required.  
  
 To publish a vertically filtered table (that is, to filter columns) first run **sp_addarticle** with no *sync_object* parameter, run [sp_articlecolumn &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-articlecolumn-transact-sql.md) once for each column to be replicated (defining the vertical filter), and then run **sp_articleview** to create the view that defines the published article.  
  
 To publish a horizontally filtered table (that is, to filter rows), run [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) with no *filter* parameter. Run [sp_articlefilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-articlefilter-transact-sql.md), providing all parameters including *filter_clause*. Then run **sp_articleview**, providing all parameters including the identical *filter_clause*.  
  
 To publish a vertically and horizontally filtered table, run [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) with no *sync_object* or *filter* parameters. Run [sp_articlecolumn &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-articlecolumn-transact-sql.md) once for each column to be replicated, and then run [sp_articlefilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-articlefilter-transact-sql.md) and **sp_articleview**.  
  
 If the article already has a view that defines the published article, **sp_articleview** drops the existing view and creates a new one automatically. If the view was created manually (**type** in [sysarticles &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sysarticles-transact-sql.md) is **5**), the existing view is not dropped.  
  
 If you create a custom filter stored procedure and a view that defines the published article manually, do not run **sp_articleview**. Instead, provide these as the *filter* and *sync_object* parameters to [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md), along with the appropriate *type* value.  
  
## Example  
 [!code-sql[HowTo#sp_AddTranArticle](../../relational-databases/replication/codesnippet/tsql/sp-articleview-transact-_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_articleview**.  
  
## See Also  
 [Define an Article](../../relational-databases/replication/publish/define-an-article.md)   
 [Define and Modify a Static Row Filter](../../relational-databases/replication/publish/define-and-modify-a-static-row-filter.md)   
 [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md)   
 [sp_articlefilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-articlefilter-transact-sql.md)   
 [sp_changearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md)   
 [sp_droparticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droparticle-transact-sql.md)   
 [sp_helparticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helparticle-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
