---
title: "sp_addmergefilter (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addmergefilter"
  - "sp_addmergefilter_TSQL"
helpviewer_keywords: 
  - "sp_addmergefilter"
ms.assetid: 4c118cb1-2008-44e2-a797-34b7dc34d6b1
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addmergefilter (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a new merge filter to create a partition based on a join with another table. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addmergefilter [ @publication = ] 'publication'   
        , [ @article = ] 'article'   
        , [ @filtername = ] 'filtername'   
        , [ @join_articlename = ] 'join_articlename'   
        , [ @join_filterclause = ] join_filterclause  
    [ , [ @join_unique_key = ] join_unique_key ]  
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]  
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]  
    [ , [ @filter_type = ] filter_type ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication in which the merge filter is being added. *publication* is **sysname**, with no default.  
  
`[ @article = ] 'article'`
 Is the name of the article on which the merge filter is being added. *article* is **sysname**, with no default.  
  
`[ @filtername = ] 'filtername'`
 Is the name of the filter. *filtername* is a required parameter. *filtername*is **sysname**, with no default.  
  
`[ @join_articlename = ] 'join_articlename'`
 Is the parent article to which the child article, specified by *article*, must be joined using the join clause specified by *join_filterclause*, in order to determine the rows in the child article that meet the filter criterion of the merge filter. *join_articlename* is **sysname**, with no default. The article must be in the publication given by *publication*.  
  
`[ @join_filterclause = ] join_filterclause`
 Is the join clause that must be used to join the child article specified by *article*and parent article specified by *join_article*, in order to determine the rows qualifying the merge filter. *join_filterclause* is **nvarchar(1000)**.  
  
`[ @join_unique_key = ] join_unique_key`
 Specifies if the join between child article *article*and parent article *join_article*is one-to-many, one-to-one, many-to-one, or many-to-many. *join_unique_key* is **int**, with a default of 0. **0** indicates a many-to-one or many-to-many join. **1** indicates a one-to-one or one-to-many join. This value is **1** when the joining columns form a unique key in *join_article*, or if *join_filterclause* is between a foreign key in *article* and a primary key in *join_article*.  
  
> [!CAUTION]  
>  Only set this parameter to **1** if you have a constraint on the joining column in the underlying table for the parent article that guarantees uniqueness. If *join_unique_key* is set to **1** incorrectly, non-convergence of data may occur.  
  
`[ @force_invalidate_snapshot = ] force_invalidate_snapshot`
 Acknowledges that the action taken by this stored procedure may invalidate an existing snapshot. *force_invalidate_snapshot* is a **bit**, with a default **0**.  
  
 **0** specifies that changes to the merge article will not cause the snapshot to be invalid. If the stored procedure detects that the change does require a new snapshot, an error will occur and no changes will be made.  
  
 **1** specifies that changes to the merge article may cause the snapshot to be invalid, and if there are existing subscriptions that would require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and a new snapshot generated.  
  
`[ @force_reinit_subscription = ] force_reinit_subscription`
 Acknowledges that the action taken by this stored procedure may require existing subscriptions to be reinitialized. *force_reinit_subscription* is a **bit**, with a default of 0.  
  
 **0** specifies that changes to the merge article will not cause the subscription to be reinitialized. If the stored procedure detects that the change would require subscriptions to be reinitialized, an error will occur and no changes will be made.  
  
 **1** specifies that changes to the merge article will cause existing subscriptions to be reinitialized, and gives permission for the subscription reinitialization to occur.  
  
`[ @filter_type = ] filter_type`
 Specifies the type of filter being added. *filter_type* is **tinyint**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1**|Join filter only. Required to support [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers.|  
|**2**|Logical record relationship only.|  
|**3**|Both join filter and logical record relationship.|  
  
 For more information, see [Group Changes to Related Rows with Logical Records](../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_addmergefilter** is used in merge replication.  
  
 **sp_addmergefilter** can only be used with table articles. View and indexed view articles are not supported.  
  
 This procedure can also be used to add a logical relationship between two articles that may or may not have a join filter between them. *filter_type* is used to specify if the merge filter being added is a join filter, a logical relation, or both.  
  
 To use logical records, the publication and articles must meet a number of requirements. For more information, see [Group Changes to Related Rows with Logical Records](../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
 Typically, this option is used for an article that has a foreign key reference to a published primary key table, and the primary key table has a filter defined in its article. The subset of primary key rows is used to determine the foreign key rows that are replicated to the Subscriber.  
  
 You cannot add a join filter between two published articles when the source tables for both articles share the same table object name. In such a case, even if both tables are owned by different schemas and have unique article names, creation of the join filter will fail.  
  
 When both a parameterized row filter and a join filter are used on a table article, replication determines whether a row belongs in a Subscriber's partition. It does so by evaluating either the filtering function or the join filter (using the [OR](../../t-sql/language-elements/or-transact-sql.md) operator), rather than evaluating the intersection of the two conditions (using the [AND](../../t-sql/language-elements/and-transact-sql.md) operator).  
  
## Example  
 [!code-sql[HowTo#sp_addmergefilter](../../relational-databases/replication/codesnippet/tsql/sp-addmergefilter-transa_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_addmergefilter**.  
  
## See Also  
 [Define an Article](../../relational-databases/replication/publish/define-an-article.md)   
 [Define and Modify a Join Filter Between Merge Articles](../../relational-databases/replication/publish/define-and-modify-a-join-filter-between-merge-articles.md)   
 [Join Filters](../../relational-databases/replication/merge/join-filters.md)   
 [sp_changemergefilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergefilter-transact-sql.md)   
 [sp_dropmergefilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergefilter-transact-sql.md)   
 [sp_helpmergefilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergefilter-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
