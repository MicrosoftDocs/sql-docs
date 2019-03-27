---
title: "sp_dropmergefilter (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_dropmergefilter_TSQL"
  - "sp_dropmergefilter"
helpviewer_keywords: 
  - "sp_dropmergefilter"
ms.assetid: 798586d7-05f3-4a5e-bea8-a34b7b52d0fd
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_dropmergefilter (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops a merge filter. **sp_dropmergefilter** drops all the merge filter columns defined on the merge filter that is to be dropped. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dropmergefilter [ @publication= ] 'publication', [ @article= ] 'article'     , [ @filtername= ] 'filtername'  
    [ , [ @force_invalidate_snapshot= ] force_invalidate_snapshot ]  
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @article = ] 'article'`
 Is the name of the article. *article* is **sysname**, with no default.  
  
`[ @filtername = ] 'filtername'`
 Is the name of the filter to be dropped. *filtername* is **sysname**, with no default.  
  
`[ @force_invalidate_snapshot = ] force_invalidate_snapshot`
 Enables or disables the ability to have a snapshot invalidated. *force_invalidate_snapshot* is a **bit**, with a default **0**.  
  
 **0** specifies that changes to the merge article do not cause the snapshot to be invalid.  
  
 **1** means that changes to the merge article may cause the snapshot to be invalid. If that is the case, a value of **1** gives permission for the new snapshot to occur.  
  
`[ @force_reinit_subscription = ] force_reinit_subscription`
 Enables or disables the ability to mark a subscription as not valid. *force_reinit_subscription* is a **bit**, with a default **0**.  
  
 **0** specifies that changes to the merge article filter do not cause the subscriptions to be invalid.  
  
 **1** means that changes to the merge article filter causes the subscriptions to be invalid.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_dropmergefilter** is used in merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_dropmergefilter**.  
  
## See Also  
 [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md)   
 [sp_addmergefilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergefilter-transact-sql.md)   
 [sp_changemergefilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergefilter-transact-sql.md)   
 [sp_helpmergefilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergefilter-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
