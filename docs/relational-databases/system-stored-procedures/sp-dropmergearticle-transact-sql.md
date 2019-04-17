---
title: "sp_dropmergearticle (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/02/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_dropmergearticle"
  - "sp_dropmergearticle_TSQL"
helpviewer_keywords: 
  - "sp_dropmergearticle"
ms.assetid: 5ef1fbf7-c03d-4488-9ab2-64aae296fa4f
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_dropmergearticle (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes an article from a merge publication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dropmergearticle [ @publication= ] 'publication'  
        , [ @article= ] 'article'   
    [ , [ @ignore_distributor= ] ignore_distributor   
    [ , [ @reserved= ] reserved   
    [ , [ @force_invalidate_snapshot= ] force_invalidate_snapshot ]  
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]  
    [ , [ @ignore_merge_metadata = ] ignore_merge_metadata ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication from which to drop an article. *publication*is **sysname**, with no default.  
  
`[ @article = ] 'article'`
 Is the name of the article to drop from the given publication. *article*is **sysname**, with no default. If **all**, all existing articles in the specified merge publication are removed. Even if *article* is **all**, the publication still must be dropped separately from the article.  
  
`[ @ignore_distributor = ] ignore_distributor`
 Indicates whether this stored procedure is executed without connecting to the Distributor. *ignore_distributor* is **bit**, with a default of **0**.  
  
`[ @reserved = ] reserved`
 Is reserved for future use. *reserved* is **nvarchar(20)**, with a default of NULL.  
  
`[ @force_invalidate_snapshot = ] force_invalidate_snapshot`
 Enables or disables the ability to have a snapshot invalidated. *force_invalidate_snapshot* is a **bit**, with a default **0**.  
  
 **0** specifies that changes to the merge article do not cause the snapshot to be invalid.  
  
 **1** means that changes to the merge article may cause the snapshot to be invalid, and if that is the case, a value of **1** gives permission for the new snapshot to occur.  
  
`[ @force_reinit_subscription = ] force_reinit_subscription`
 Acknowledges that dropping the article requires existing subscriptions to be reinitialized. *force_reinit_subscription* is a **bit**, with a default of **0**.  
  
 **0** specifies that dropping the article does not cause the subscription to be reinitialized.  
  
 **1** means that dropping the article causes existing subscriptions to be reinitialized, and gives permission for the subscription reinitialization to occur.  
  
`[ @ignore_merge_metadata = ] ignore_merge_metadata`
 Internal use only.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_dropmergearticle** is used in merge replication. For more information about dropping articles, see [Add Articles to and Drop Articles from Existing Publications](../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md).  
  
 Executing **sp_dropmergearticle** to drop an article from a publication does not remove the object from the publication database or the corresponding object from the subscription database. Use `DROP <object>` to remove these objects manually if necessary.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_dropmergearticle**.  
  
## Example  
  
```sql  
DECLARE @publication AS sysname;  
DECLARE @article1 AS sysname;  
DECLARE @article2 AS sysname;  
SET @publication = N'AdvWorksSalesOrdersMerge';  
SET @article1 = N'SalesOrderDetail';   
SET @article2 = N'SalesOrderHeader';   
  
-- Remove articles from a merge publication.  
USE [AdventureWorks]  
EXEC sp_dropmergearticle   
  @publication = @publication,   
  @article = @article1,  
  @force_invalidate_snapshot = 1;  
EXEC sp_dropmergearticle   
  @publication = @publication,   
  @article = @article2,  
  @force_invalidate_snapshot = 1;  
GO  
```  
  
```sql  
DECLARE @publication AS sysname;  
DECLARE @table1 AS sysname;  
DECLARE @table2 AS sysname;  
DECLARE @table3 AS sysname;  
DECLARE @salesschema AS sysname;  
DECLARE @hrschema AS sysname;  
DECLARE @filterclause AS nvarchar(1000);  
SET @publication = N'AdvWorksSalesOrdersMerge';   
SET @table1 = N'Employee';   
SET @table2 = N'SalesOrderHeader';   
SET @table3 = N'SalesOrderDetail';   
SET @salesschema = N'Sales';  
SET @hrschema = N'HumanResources';  
SET @filterclause = N'Employee.LoginID = HOST_NAME()';  
  
-- Drop the merge join filter between SalesOrderHeader and SalesOrderDetail.  
EXEC sp_dropmergefilter   
  @publication = @publication,   
  @article = @table3,   
  @filtername = N'SalesOrderDetail_SalesOrderHeader',   
  @force_invalidate_snapshot = 1,   
  @force_reinit_subscription = 1;  
  
-- Drops the merge join filter between Employee and SalesOrderHeader.  
EXEC sp_dropmergefilter   
  @publication = @publication,   
  @article = @table2,   
  @filtername = N'SalesOrderHeader_Employee',   
  @force_invalidate_snapshot = 1,   
  @force_reinit_subscription = 1;  
  
-- Drops the article for the SalesOrderDetail table.  
EXEC sp_dropmergearticle   
  @publication = @publication,   
  @article = @table3,  
  @force_invalidate_snapshot = 1,   
  @force_reinit_subscription = 1;  
  
-- Drops the article for the SalesOrderHeader table.  
EXEC sp_dropmergearticle   
  @publication = @publication,   
  @article = @table2,   
  @force_invalidate_snapshot = 1,   
  @force_reinit_subscription = 1;  
  
-- Drops the article for the Employee table.  
EXEC sp_dropmergearticle   
  @publication = @publication,   
  @article = @table1,  
  @force_invalidate_snapshot = 1,   
  @force_reinit_subscription = 1;  
GO  
```  
  
## See Also  
 [Delete an Article](../../relational-databases/replication/publish/delete-an-article.md)   
 [Add Articles to and Drop Articles from Existing Publications](../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md)   
 [sp_addmergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md)   
 [sp_changemergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md)   
 [sp_helpmergearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergearticle-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
