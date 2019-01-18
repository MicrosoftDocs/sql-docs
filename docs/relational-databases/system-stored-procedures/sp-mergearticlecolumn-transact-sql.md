---
title: "sp_mergearticlecolumn (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_mergearticlecolumn"
  - "sp_mergearticlecolumn_TSQL"
helpviewer_keywords: 
  - "sp_mergearticlecolumn"
ms.assetid: b4f2b888-e094-4759-a472-d893638995eb
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_mergearticlecolumn (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Partitions a merge publication vertically. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_mergearticlecolumn [ @publication = ] 'publication'  
        , [ @article = ] 'article'  
    [ , [ @column = ] 'column'  
    [ , [ @operation = ] 'operation'   
    [ , [ @schema_replication = ] 'schema_replication' ]  
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]   
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]   
```  
  
## Arguments  
 [ **@publication =**] **'**_publication_**'**  
 Is the name of the publication. *Publication* is **sysname**, with no default.  
  
 [ **@article =**] **'**_article_**'**  
 Is the name of the article in the publication. *article* is **sysname**, with no default.  
  
 [ **@column =**] **'**_column_**'**  
 Identifies the columns on which to create the vertical partition. *column* is **sysname**, with a default of NULL. If NULL and `@operation = N'add'`, all columns in the source table are added to the article by default. *column* cannot be NULL when *operation* is set to **drop**. To exclude columns from an article, execute **sp_mergearticlecolumn** and specify *column* and `@operation = N'drop'` for each column to be removed from the specified *article*.  
  
 [ **@operation =**] **'**_operation_**'**  
 Is the replication status. *operation* is **nvarchar(4)**, with a default of ADD. **add** marks the column for replication. **drop** clears the column.  
  
 [ **@schema_replication=**] **'**_schema_replication_**'**  
 Specifies that a schema change will be propagated when the Merge Agent runs. *schema_replication* is **nvarchar(5)**, with a default of FALSE.  
  
> [!NOTE]  
>  Only **FALSE** is supported for *schema_replication*.  
  
 [ **@force_invalidate_snapshot =** ] *force_invalidate_snapshot*  
 Enables or disables the ability to have a snapshot invalidated. *force_invalidate_snapshot* is a **bit**, with a default of **0**.  
  
 **0** specifies that changes to the merge article will not cause the snapshot to be invalid.  
  
 **1** specifies that changes to the merge article may cause the snapshot to be invalid, and if that is the case, a value of **1** gives permission for the new snapshot to occur.  
  
 [ **@force_reinit_subscription = ]**_force_reinit_subscription_  
 Enables or disables the ability to have the subscription reinitializated. *force_reinit_subscription* is a bit with a default of **0**.  
  
 **0** specifies that changes to the merge article will not cause the subscription to be reinitialized.  
  
 **1** specifies that changes to the merge article may cause the subscription to be reinitialized, and if that is the case, a value of **1** gives permission for the subscription reinitialization to occur.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_mergearticlecolumn** is used in merge replication.  
  
 An identity column cannot be dropped from the article if automatic identity range management is being used. For more information, see [Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md).  
  
 If an application sets a new vertical partition after the initial snapshot is created, a new snapshot must be generated and reapplied to each subscription. Snapshots are applied when the next scheduled snapshot and distribution or merge agent run.  
  
 If row tracking is used for conflict detection (the default), the base table can include a maximum of 1,024 columns, but columns must be filtered from the article so that a maximum of 246 columns is published. If column tracking is used, the base table can include a maximum of 246 columns.  
  
## Example  
 [!code-sql[HowTo#sp_AddMergeArticle](../../relational-databases/replication/codesnippet/tsql/sp-mergearticlecolumn-tr_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_mergearticlecolumn**.  
  
## See Also  
 [Define and Modify a Join Filter Between Merge Articles](../../relational-databases/replication/publish/define-and-modify-a-join-filter-between-merge-articles.md)   
 [Define and Modify a Parameterized Row Filter for a Merge Article](../../relational-databases/replication/publish/define-and-modify-a-parameterized-row-filter-for-a-merge-article.md)   
 [Filter Published Data](../../relational-databases/replication/publish/filter-published-data.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
