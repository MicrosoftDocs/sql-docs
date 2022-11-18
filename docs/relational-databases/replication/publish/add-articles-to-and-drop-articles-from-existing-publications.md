---
title: "Add & drop articles (existing Publication)"
description: Learn how to add articles to and drop articles from existing publications for SQL Server.
ms.custom: seo-lt-2019
ms.date: "03/07/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "articles [SQL Server replication], dropping"
  - "deleting articles"
  - "removing articles"
  - "dropping articles"
  - "adding articles"
  - "administering replication, articles"
  - "publications [SQL Server replication], adding and dropping articles"
  - "articles [SQL Server replication], adding"
ms.assetid: b148e907-e1f2-483b-bdb2-59ea596efceb
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Add Articles to and Drop Articles from Existing Publications
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  After a publication is created, it is possible to add and drop articles. Articles can be added at any time, but the actions required for dropping articles depend on the type of replication and when the article is dropped.  
  
## Adding Articles  
 Adding an article involves: adding the article to the publication; creating a new snapshot for the publication; synchronizing the subscription to apply the schema and data for the new article.  
  
> [!NOTE]
>  If you add an article to a merge publication and an existing article depends on the new article, you must specify a processing order for both articles using the **\@processing_order** parameter of [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) and [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). Consider the following scenario: you publish a table but you do not publish a function that the table references. If you do not publish the function, the table cannot be created at the Subscriber. When you add the function to the publication: specify a value of **1** for the **\@processing_order** parameter of **sp_addmergearticle**; and specify a value of **2** for the **\@processing_order** parameter of **sp_changemergearticle**, specifying the table name for the parameter **\@article**. This processing order ensures that you create the function at the Subscriber before the table that depends on it. You can use different numbers for each article, as long as the number for the function is lower than the number for the table.  
  
1.  Add one or more articles through one of the following methods:  
  
    -   [Add Articles to and Drop Articles from a Publication &#40;SQL Server Management Studio&#41;](../../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-a-publication.md)  
  
    -   [Define an Article](../../../relational-databases/replication/publish/define-an-article.md)  
  
2.  After adding an article to a publication, you must create a new snapshot for the publication (and all partitions if it is a merge publication with parameterized filters). The Distribution Agent or Merge Agent then copies the schema and data for the new article to the Subscriber (it does not reinitialize the entire publication).  
  
    -   To create a new snapshot, see [Create and Apply the Initial Snapshot](../../../relational-databases/replication/create-and-apply-the-initial-snapshot.md).  
  
    -   To create a new snapshot for a merge publication with parameterized filters, see [Create a Snapshot for a Merge Publication with Parameterized Filters](../../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).  
  
3.  After the snapshot is created, synchronize the subscription to copy the schema and data for the new article.  

    -   To synchronize a push subscription, see [Synchronize a Push Subscription](../../../relational-databases/replication/synchronize-a-push-subscription.md).  
  
    -   To synchronize a pull subscription, see [Synchronize a Pull Subscription](../../../relational-databases/replication/synchronize-a-pull-subscription.md).  
  
## Dropping Articles  
 Articles can be dropped from a publication at any time, but you must take into account the following behaviors:  
  
-   Dropping an article from a publication does not remove the object from the publication database or the corresponding object from the subscription database. Use DROP \<Object> to remove these objects if necessary. When you drop an article that is related to other published articles through foreign key constraints, we recommend that you drop the table at the Subscriber manually or by using on-demand script execution: specify a script that includes the appropriate DROP \<Object> statements. For more information, see [Execute Scripts During Synchronization &#40;Replication Transact-SQL Programming&#41;](../../../relational-databases/replication/execute-scripts-during-synchronization-replication-transact-sql-programming.md).  
  
-   For merge publications with a compatibility level of 90RTM or higher, articles can be dropped at any time, but a new snapshot is required. Additionally:  
  
    -   If an article is a parent article in a join filter or logical record relationship, the relationships must be dropped first, which requires reinitialization.  
  
    -   If an article has the last parameterized filter in a publication, subscriptions must be reinitialized.  
  
-   For merge publications with a compatibility level lower than 90RTM, articles can be dropped with no special considerations prior to the initial synchronization of subscriptions. If an article is dropped after one or more subscriptions is synchronized, the subscriptions must be dropped, re-created, and synchronized.  
  
-   For snapshot or transactional publications, articles can be dropped with no special considerations prior to subscriptions being created. If an article is dropped after one or more subscriptions is created, the subscriptions must be dropped, recreated, and synchronized. For more information about dropping subscriptions, see [Subscribe to Publications](../../../relational-databases/replication/subscribe-to-publications.md) and [sp_dropsubscription &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-dropsubscription-transact-sql.md). **sp_dropsubscription** allows you to drop a single article from the subscription rather than the entire subscription.  
  
1.  Dropping an article from a publication involves dropping the article and creating a new snapshot for the publication. Dropping an article invalidates the current snapshot; therefore a new snapshot must be created.  
  
    -   To drop an article from a publication, see [Add Articles to and Drop Articles from a Publication &#40;SQL Server Management Studio&#41;](../../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-a-publication.md) or [Delete an Article](../../../relational-databases/replication/publish/delete-an-article.md).  
  
2.  After dropping an article from a publication, you must create a new snapshot for the publication (and all partitions if it is a merge publication with parameterized filters).  
  
    -   To create a new snapshot, see [Create and Apply the Initial Snapshot](../../../relational-databases/replication/create-and-apply-the-initial-snapshot.md).  
  
    -   To create a new snapshot for a merge publication with parameterized filters, see [Create a Snapshot for a Merge Publication with Parameterized Filters](../../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).  
  
 As noted above, in some cases dropping an article requires subscriptions to be dropped, recreated, and then synchronized. For more information, see [Subscribe to Publications](../../../relational-databases/replication/subscribe-to-publications.md) and [Synchronize Data](../../../relational-databases/replication/synchronize-data.md).  
 
 > [!NOTE]
 > **[!INCLUDE[sssql14-md](../../../includes/sssql14-md.md)] Service Pack 2** or above and **[!INCLUDE[sssql16-md](../../../includes/sssql16-md.md)] Service Pack 1** or above support dropping a table using **DROP TABLE** DDL command for articles participating in Transactional Replication. If a DROP TABLE DDL is supported by the publication(s), then the DROP TABLE operation will drop the table from the publication and the database. The log reader agent will post a cleanup command for the distribution database of the dropped table and do the cleanup of the publisher metadata. If the log reader hasn't processed all the log records that refer to the dropped table, then it will ignore new commands that are associated with the dropped table. Already processed records will be delivered to distribution database. They may be applied on Subscriber database if the Distribution Agent processes them before Log Reader cleans up the obsolete (dropped) article(s). The **default** setting for all transactional replication publications is to not support DROP TABLE DDL. [KB 3170123](https://support.microsoft.com/help/3170123/supports-drop-table-ddl-for-articles-that-are-included-in-transactional-replication-in-sql-server-2014-or-in-sql-server-2016-sp1) has more details about this improvement.

  
## See Also  
 [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md)   
 [Reinitialize Subscriptions](../../../relational-databases/replication/reinitialize-subscriptions.md)   
 [Make Schema Changes on Publication Databases](../../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md)  
  
  
