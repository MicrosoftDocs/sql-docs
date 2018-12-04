---
title: "Specify Article Types (Replication Transact-SQL Programming) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "publishing [SQL Server replication], stored procedure execution"
  - "articles [SQL Server replication], transactional replication options"
  - "articles [SQL Server replication], merge replication options"
  - "stored procedures [SQL Server replication], publishing"
ms.assetid: d7effbac-c45b-423f-97ae-fd426b1050ba
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Specify Article Types (Replication Transact-SQL Programming)
  The default article types for replication are table articles, but you can publish other database objects as articles, including views, stored procedures, user-defined functions, and stored procedure execution. You can use replication stored procedures to specify an article type programmatically when you define an article. The procedures that you use depend on the type of replication and article type.  
  
> [!NOTE]  
>  The schema-only designation when defining table, view, and stored procedure articles indicates that only the object definition is replicated.  
  
### To publish a table article in a transactional or snapshot publication  
  
1.  At the Publisher on the publication database, execute [sp_addarticle](/sql/relational-databases/system-stored-procedures/sp-addarticle-transact-sql). Specify one of the following values for **@type** to define the type of article:  
  
    -   **logbased** - a log-based table article, which is the default for transactional and snapshot replication. Replication automatically generates the stored procedure used for horizontal filtering and the view that defines a vertically filtered article.  
  
    -   **logbased manualfilter** - a log-based, horizontally filtered article where the stored procedure used for horizontal filtering is manually created and defined by the user and specified for **@filter**. For more information, see [Define and Modify a Static Row Filter](define-and-modify-a-static-row-filter.md).  
  
    -   **logbased manualview** - a log-based, vertically filtered article where the view that defines the vertically filtered article is created and defined by the user and specified for **@sync_object**. For more information, see [Define and Modify a Static Row Filter](define-and-modify-a-static-row-filter.md) and [Define and Modify a Column Filter](define-and-modify-a-column-filter.md).  
  
    -   **logbased manualboth** - a log-based, horizontally and vertically filtered article where both the stored procedure used for horizontal filtering and the view that defines the vertically filtered article are created and defined by the user and specified for **@filter** and **@sync_object**, respectively. For more information, see [Define and Modify a Static Row Filter](define-and-modify-a-static-row-filter.md) and [Define and Modify a Column Filter](define-and-modify-a-column-filter.md).  
  
     This defines a new article for the publication. For more information, see [Define an Article](define-an-article.md).  
  
2.  For **logbased manualboth** and **logbased manualfilter** articles, execute [sp_articlefilter](/sql/relational-databases/system-stored-procedures/sp-articlefilter-transact-sql) to generate the filtering stored procedure for a horizontally filtered article. For more information, see [Define and Modify a Static Row Filter](define-and-modify-a-static-row-filter.md).  
  
3.  For **logbased manualboth**, **logbased manualview**, and **logbased manualfilter** articles, execute [sp_articleview](/sql/relational-databases/system-stored-procedures/sp-articleview-transact-sql) to generate the view that defines the vertically filtered article. For more information, see [Define and Modify a Column Filter](define-and-modify-a-column-filter.md).  
  
### To publish a view or indexed view article in a transactional or snapshot publication  
  
1.  At the Publisher on the publication database, execute [sp_addarticle](/sql/relational-databases/system-stored-procedures/sp-addarticle-transact-sql). Specify one of the following values for **@type** to define the type of article:  
  
    -   **indexed view logbased** - a log-based indexed view article. Replication automatically generates the stored procedure used for horizontal filtering and the view that defines a vertically filtered article.  
  
    -   **view schema only** - a schema-only view article. The base table must also be replicated.  
  
    -   **indexed view schema only** - a schema-only indexed view article. The base table must also be replicated.  
  
    -   **indexed view logbased manualfilter** - a log-based, horizontally filtered indexed view article where the stored procedure used for horizontal filtering is manually created and defined by the user and specified for **@filter**. For more information, see [Define and Modify a Static Row Filter](define-and-modify-a-static-row-filter.md).  
  
    -   **indexed view logbased manualview** - a log-based, filtered indexed view article where the view that defines a vertically filtered article is created and defined by the user and specified for **@sync_object**. For more information, see [Define and Modify a Static Row Filter](define-and-modify-a-static-row-filter.md) and [Define and Modify a Column Filter](define-and-modify-a-column-filter.md).  
  
    -   **indexed view logbased manualboth** - a log-based, filtered indexed view article where both the stored procedure used for horizontal filtering and the view that defines a vertically filtered article are created and defined by the user and specified for **@filter** and **@sync_object**, respectively. For more information, see [Define and Modify a Static Row Filter](define-and-modify-a-static-row-filter.md) and [Define and Modify a Column Filter](define-and-modify-a-column-filter.md).  
  
     This defines a new article for the publication. For more information, see [Define an Article](define-an-article.md).  
  
2.  For **logbased manualboth** and **logbased manualfilter** articles, execute [sp_articlefilter](/sql/relational-databases/system-stored-procedures/sp-articlefilter-transact-sql) to generate the filtering stored procedure for a horizontally filtered article. For more information, see [Define and Modify a Static Row Filter](define-and-modify-a-static-row-filter.md).  
  
3.  For **logbased manualboth**, **logbased manualview**, and **logbased manualfilter** articles, execute [sp_articleview](/sql/relational-databases/system-stored-procedures/sp-articleview-transact-sql) to generate the view that defines the vertically filtered article. For more information, see [Define and Modify a Column Filter](define-and-modify-a-column-filter.md).  
  
### To publish a stored procedure, stored procedure execution, or user-defined function article in a transactional or snapshot publication  
  
1.  At the Publisher on the publication database, execute [sp_addarticle](/sql/relational-databases/system-stored-procedures/sp-addarticle-transact-sql). Specify one of the following values for **@type** to define the type of article:  
  
    -   **proc schema only** - a schema-only stored procedure article.  
  
    -   **proc exec** - replicates the execution of the stored procedure to all Subscribers of the article. For more information, see [Publishing Stored Procedure Execution in Transactional Replication](../transactional/publishing-stored-procedure-execution-in-transactional-replication.md).  
  
    -   **serializable proc exec** - replicates the execution of the stored procedure only if it is executed within the context of a serializable transaction. For more information, see [Publishing Stored Procedure Execution in Transactional Replication](../transactional/publishing-stored-procedure-execution-in-transactional-replication.md).  
  
    -   **func schema only** - a schema-only user-defined function article.  
  
     This defines a new article for the publication. For more information, see [Define an Article](define-an-article.md).  
  
### To publish a table or view article in a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle](/sql/relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql). Specify one of the following values for **@type** to define the type of article:  
  
    -   **table** - a table article.  
  
    -   **indexed view schema only** - a schema-only indexed view article.  
  
    -   **view schema only** - a schema-only view article.  
  
     This defines a new article for the publication. For more information, see [Define an Article](define-an-article.md).  
  
### To publish a stored procedure or user-defined function article in a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle](/sql/relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql). Specify one of the following values for **@type** to define the type of article:  
  
    -   **func schema only** - a schema-only user-defined function article.  
  
    -   **proc schema only** - a schema-only stored procedure article.  
  
     This defines a new article for the publication. For more information, see [Define an Article](define-an-article.md).  
  
## See Also  
 [Replication System Stored Procedures Concepts](../concepts/replication-system-stored-procedures-concepts.md)   
 [Publish Data and Database Objects](publish-data-and-database-objects.md)  
  
  
