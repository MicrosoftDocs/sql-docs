---
description: "Delete an Article"
title: "Delete an Article | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "articles [SQL Server replication], dropping"
  - "sp_droparticle"
  - "sp_dropmergearticle"
  - "deleting articles"
  - "removing articles"
  - "dropping articles"
ms.assetid: 185b58fc-38c0-4abe-822e-6ec20066c863
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Delete an Article
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  This topic describes how to delete an article in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[tsql](../../../includes/tsql-md.md)] or Replication Management Objects (RMO). For information about the conditions under which articles can be dropped and whether dropping an article requires a new snapshot or the reinitialization of subscriptions, see [Add Articles to and Drop Articles from Existing Publications](../../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md).  
  
 **In This Topic**  
  
-   **To delete an article, using:**  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Articles can be deleted programmatically using replication stored procedures. The stored procedures that you use depend on the type of publication to which the article belongs.  
  
#### To delete an article from a snapshot or transactional publication  
  
1.  Execute [sp_droparticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-droparticle-transact-sql.md) to delete an article, specified by **\@article**, from a publication, specified by **\@publication**. Specify a value of **1** for **\@force_invalidate_snapshot**.  
  
2.  (Optional) To remove the published object from the database entirely, execute the `DROP <objectname>` command at the Publisher on the publication database.  

#### To delete an article from a merge publication  
  
1.  Execute [sp_dropmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-dropmergearticle-transact-sql.md) to delete an article, specified by **\@article**, from a publication, specified by **\@publication**. If necessary, specify a value of **1** for **\@force_invalidate_snapshot** and a value of **1** for **\@force_reinit_subscription**.  
  
2.  (Optional) To remove the published object from the database entirely, execute the `DROP <objectname>` command at the Publisher on the publication database.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 The following example deletes an article from a transactional publication. Because this change invalidates the existing snapshot, a value of **1** is specified for the **\@force_invalidate_snapshot** parameter.  
  
```  
DECLARE @publication AS sysname;  
DECLARE @article AS sysname;  
SET @publication = N'AdvWorksProductTran';   
SET @article = N'Product';   
  
-- Drop the transactional article.  
USE [AdventureWorks]  
EXEC sp_droparticle   
  @publication = @publication,   
  @article = @article,  
  @force_invalidate_snapshot = 1;  
GO  
```  
  
 The following example deletes two articles from a merge publication. Because these changes invalidate the existing snapshot, a value of **1** is specified for the **\@force_invalidate_snapshot** parameter.  
  
```  
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
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
 You can delete articles programmatically by using Replication Management Objects (RMO). The RMO classes you use to delete an article depend on the type of publication to which the article belongs.  
  
#### To delete an article that belongs to a snapshot or transactional publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.TransArticle> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.Article.Name%2A>, <xref:Microsoft.SqlServer.Replication.Article.PublicationName%2A>, and <xref:Microsoft.SqlServer.Replication.Article.DatabaseName%2A> properties.  
  
4.  Set the connection from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
5.  Check the <xref:Microsoft.SqlServer.Replication.ReplicationObject.IsExistingObject%2A> property to verify that the article exists. If the value of this property is **false**, either the article properties in step 3 were defined incorrectly or the article does not exist.  
  
6.  Call the <xref:Microsoft.SqlServer.Replication.Article.Remove%2A> method.  
  
7.  Close all connections.  
  
#### To delete an article that belongs to a merge publication  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergeArticle> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.Article.Name%2A>, <xref:Microsoft.SqlServer.Replication.Article.PublicationName%2A>, and <xref:Microsoft.SqlServer.Replication.Article.DatabaseName%2A> properties.  
  
4.  Set the connection from step 1 for the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property.  
  
5.  Check the <xref:Microsoft.SqlServer.Replication.ReplicationObject.IsExistingObject%2A> property to verify that the article exists. If the value of this property is **false**, either the article properties in step 3 were defined incorrectly or the article does not exist.  
  
6.  Call the <xref:Microsoft.SqlServer.Replication.Article.Remove%2A> method.  
  
7.  Close all connections.  
  
## See Also  
 [Add Articles to and Drop Articles from Existing Publications](../../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md)   
 [Replication System Stored Procedures Concepts](../../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md)  
  
  
