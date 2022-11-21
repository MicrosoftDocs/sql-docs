---
title: "Publish Data and Database Objects | Microsoft Docs"
description: This article summarizes the tables and other database objects you can publish for replication in SQL Server.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "user-defined types [SQL Server replication]"
  - "articles [SQL Server replication], dropping"
  - "objects [SQL Server replication]"
  - "publications [SQL Server replication], creating"
  - "merge replication [SQL Server replication], publications"
  - "schema-only articles [SQL Server replication]"
  - "publishing [SQL Server replication], database objects"
  - "articles [SQL Server replication], defining"
  - "publishing [SQL Server replication], functions"
  - "replication [SQL Server], publications"
  - "publishing [SQL Server replication], views"
  - "tables [SQL Server replication]"
  - "schemas [SQL Server replication]"
  - "publishing [SQL Server replication], data"
  - "schemas [SQL Server replication], publishing"
  - "articles [SQL Server replication], stored procedures and"
  - "publishing [SQL Server replication], tables"
  - "alias data types [SQL Server replication]"
  - "publications [SQL Server replication], deleting"
  - "snapshot replication [SQL Server], publications"
  - "articles [SQL Server replication], modifying"
  - "transactional replication, publications"
  - "publishing [SQL Server replication], stored procedures"
  - "publishing [SQL Server replication]"
  - "views [SQL Server replication]"
  - "stored procedures [SQL Server replication], publishing"
  - "publications [SQL Server replication], schema options"
  - "articles [SQL Server replication], adding"
  - "publications [SQL Server replication], modifying"
  - "user-defined functions [SQL Server replication]"
ms.assetid: d986032c-3387-4de1-a435-3ec5e82185a2
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Publish Data and Database Objects
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  When creating a publication, you choose the tables and other database objects that you want to publish. You can publish the following database objects using replication.  
  
|Database object|Snapshot replication and transactional replication|Merge replication|  
|---------------------|--------------------------------------------------------|-----------------------|  
|Tables|X|X|  
|Partitioned Tables|X|X|  
|Stored Procedures – Definition ([!INCLUDE[tsql](../../../includes/tsql-md.md)] and CLR)|X|X|  
|Stored Procedures – Execution ([!INCLUDE[tsql](../../../includes/tsql-md.md)] and CLR)|X|no|  
|Views|X|X|  
|Indexed Views|X|X|  
|Indexed Views as Tables|X|no|  
|User-Defined Types (CLR)|X|X|  
|User-Defined Functions ([!INCLUDE[tsql](../../../includes/tsql-md.md)] and CLR)|X|X|  
|Alias Data Types|X|X|  
|Full text indexes|X|X|  
|Schema Objects (constraints, indexes, user DML triggers, extended properties, and collation)|X|X|  
  
## Creating Publications  
 To create a publication, you supply the following information:  
  
-   The Distributor.    
-   The location of the snapshot files.    
-   The publication database.    
-   The type of publication to create (snapshot, transactional, transactional with updatable subscriptions, or merge).    
-   The data and database objects (articles) to include in the publication.   
-   Static row filters and column filters for all types of publications, and parameterized row filters and join filters for merge publications.   
-   The Snapshot Agent schedule.    
-   Accounts under which the following agents will run: the Snapshot Agent for all publications; the Log Reader Agent for all transactional publications; the Queue Reader Agent for transactional publications that allow updating subscriptions.    
-   A name and description for the publication.  
  
 For information about how to work with publications, see the following topics:    
-   [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md)    
-   [Define an Article](../../../relational-databases/replication/publish/define-an-article.md)    
-   [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md)    
-   [View and Modify Article Properties](../../../relational-databases/replication/publish/view-and-modify-article-properties.md)    
-   [Delete a Publication](../../../relational-databases/replication/publish/delete-a-publication.md)    
-   [Delete an Article](../../../relational-databases/replication/publish/delete-an-article.md)  
  
> [!NOTE]  
>  Deleting an article or publication does not remove objects from the Subscriber.  
  
## Publishing Tables  
 The most commonly published object is a table. The following links provide additional information about areas related to publishing tables:  
  
-   [Filter Published Data](../../../relational-databases/replication/publish/filter-published-data.md)    
-   [Article Options for Transactional Replication](../../../relational-databases/replication/transactional/article-options-for-transactional-replication.md)
-   [Article Options for Merge Replication](../../../relational-databases/replication/merge/article-options-for-merge-replication.md)    
-   [Replicate Identity Columns](../../../relational-databases/replication/publish/replicate-identity-columns.md)  
  
 When publishing a table for replication, you can specify which schema objects should be copied to the Subscriber, such as declared referential integrity (primary key constraints, reference constraints, unique constraints), indexes, user DML triggers (DDL triggers cannot be replicated), extended properties, and collation. Extended properties are replicated only in the initial synchronization between the Publisher and the Subscriber. If you add or modify an extended property after the initial synchronization, the change is not replicated.  
  
 To specify schema options, see [Specify Schema Options](../../../relational-databases/replication/publish/specify-schema-options.md) or <xref:Microsoft.SqlServer.Replication.Article.SchemaOption%2A>.  
  
### Partitioned Tables and Indexes  
 Replication supports the publishing of partitioned tables and indexes. The level of support depends on the type of replication that is used, and the options that you specify for the publication and the articles associated with partitioned tables. For more information, see [Replicate Partitioned Tables and Indexes](../../../relational-databases/replication/publish/replicate-partitioned-tables-and-indexes.md).  
  
## Publishing Stored Procedures  
 All types of replication allow you to replicate stored procedure definitions: the CREATE PROCEDURE is copied to each Subscriber. In the case of common language runtime (CLR) stored procedures, the associated assembly is also copied. Changes to procedures are replicated to Subscribers; changes to associated assemblies are not.  
  
 In addition to replicating the definition of a stored procedure, transactional replication allows you to replicate the execution of stored procedures. This is useful in replicating the results of maintenance-oriented stored procedures that affect large amounts of data. For more information, see [Publishing Stored Procedure Execution in Transactional Replication](../../../relational-databases/replication/transactional/publishing-stored-procedure-execution-in-transactional-replication.md).  
  
## Publishing Views  
 All types of replication allow you to replicate views. The view (and its accompanying index, if it is an indexed view) can be copied to the Subscriber, but the base table must also be replicated.  
  
 For indexed views, transactional replication also allows you to replicate the indexed view as a table rather than a view, eliminating the need to also replicate the base table. To do this, specify one of the "indexed view logbased" options for the *\@type* parameter of [sp_addarticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md). For more information about using **sp_addarticle**, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).  
  
## Publishing User-Defined Functions  
 The CREATE FUNCTION statements for CLR functions and [!INCLUDE[tsql](../../../includes/tsql-md.md)] functions are copied to each Subscriber. In the case of CLR functions, the associated assembly is also copied. Changes to functions are replicated to Subscribers; changes to associated assemblies are not.  
  
## Publishing User-Defined Types and Alias Data Types  
 Columns that use user-defined types or alias data types are replicated to Subscribers like other columns. The CREATE TYPEstatement for each replicated type is executed at the Subscriber before the table is created. In the case of user-defined types, the associated assembly is also copied to each Subscriber. Changes to user-defined types and alias data types are not replicated to Subscribers.  
  
 If a type is defined in a database, but it is not referenced in any columns when a publication is created, the type is not copied to Subscribers. If you subsequently create a column of that type in the database and want to replicate it, you must first manually copy the type (and the associated assembly for a user-defined type) to each Subscriber.  
  
## Publishing Full Text Indexes  
 The CREATE FULLTEXT INDEX statement is copied to each Subscriber, and the full text index is created at the Subscriber. Changes made to full text indexes using ALTER FULLTEXT INDEX are not replicated.  
  
## Making Schema Changes to Published Objects  
 Replication supports a wide range of schema changes to published objects. When you make any of the following schema changes on the appropriate published object at a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Publisher, that change is propagated by default to all [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers:  
  
-   ALTER TABLE  
  
-   ALTER VIEW  
  
-   ALTER PROCEDURE  
  
-   ALTER FUNCTION  
  
-   ALTER TRIGGER  
  
 For more information, see [Make Schema Changes on Publication Databases](../../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md).  
  
## Considerations for Publishing  
 Keep the following issues in mind when publishing database objects:  
  
-   The database is accessible to users during the creation of the publication and the initial snapshot, but it is advisable to create publications during times of lower activity on the Publisher.  
  
-   A database cannot be renamed after a publication is created in it. To rename it, you must first remove replication from the database.  
  
-   If you are publishing a database object that depends on one or more other database objects, you must publish all referenced objects. For example, if you publish a view that depends on a table, you must publish the table also.  
  
    > [!NOTE]  
    >  If you add an article to a merge publication and an existing article depends on the new article, you must specify a processing order for both articles using the **\@processing_order** parameter of [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) and [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). Consider the following scenario: you publish a table but you do not publish a function that the table references. If you do not publish the function, the table cannot be created at the Subscriber. When you add the function to the publication: specify a value of **1** for the **\@processing_order** parameter of **sp_addmergearticle**; and specify a value of **2** for the **\@processing_order** parameter of **sp_changemergearticle**, specifying the table name for the parameter **\@article**. This processing order ensures that you create the function at the Subscriber before the table that depends on it. You can use different numbers for each article as long as the number for the function is lower than the number for the table.  
  
-   Publication names cannot include the following characters: % * [ ] | : " ? \ / < >.  
  
### Limitations on Publishing Objects  
  
-   The maximum number of articles and columns that can be published differs by publication type. For more information, see the "Replication Objects" section of [Maximum Capacity Specifications for SQL Server](../../../sql-server/maximum-capacity-specifications-for-sql-server.md).  
  
-   Stored procedures, views, triggers, and user-defined functions that are defined as WITH ENCRYPTION cannot be published as part of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication.  
  
-   XML schema collections can be replicated but changes are not replicated after the initial snapshot.  
  
-   Tables published for transactional replication must have a primary key. If a table is in a transactional replication publication, you cannot disable any indexes that are associated with primary key columns. These indexes are required by replication. To disable an index, you must first drop the table from the publication.  
  
-   Bound defaults created with [sp_bindefault &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-bindefault-transact-sql.md) are not replicated (bound defaults are deprecated in favor of defaults created with the DEFAULT keyword of ALTER TABLE or CREATE TABLE).  
  
-   Functions containing the **NOEXPAND** hint on indexed views cannot be published in the same publication as the referenced tables and indexed views, due to the order in which the distribution agent delivers them. To work around this problem, place the table and indexed view creation in a first publication, and add functions containing the **NOEXPAND** hint on the indexed views to a second publication which you publish after the first publication completes. Or, create scripts for these functions and deliver the script by using the *\@post_snapshot_script* parameter of **sp_addpublication**.  
  
### Schemas and Object Ownership  
 Replication has the following default behavior in the New Publication Wizard with respect to schemas and object ownership:  
  
-   For articles in merge publications with a compatibility level of 90 or higher, snapshot publications, and transactional publications: by default, the object owner at the Subscriber is the same as the owner of the corresponding object at the Publisher. If the schemas that own objects do not exist at the Subscriber, they are created automatically.  
  
-   For articles in merge publications with a compatibility level lower than 90: by default, the owner is left blank and is specified as **dbo** during the creation of the object on the Subscriber.  
  
-   For articles in Oracle publications: by default, the owner is specified as **dbo**.  
  
-   For articles in publications that use character mode snapshots (which are used for non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers and [!INCLUDE[ssEW](../../../includes/ssew-md.md)] Subscribers): by default, the owner is left blank. The owner defaults to the owner associated with the account used by the Distribution Agent or Merge Agent to connect to the Subscriber.  
  
 The object owner can be changed through the **Article Properties - \<**_Article_**>** dialog box and through the following stored procedures: **sp_addarticle**, **sp_addmergearticle**, **sp_changearticle**, and **sp_changemergearticle**. For more information, see [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md), [Define an Article](../../../relational-databases/replication/publish/define-an-article.md), and [View and Modify Article Properties](../../../relational-databases/replication/publish/view-and-modify-article-properties.md).  
  
### Publishing Data to Subscribers Running Previous Versions of SQL Server  
  
-   If you are publishing to a Subscriber running a previous version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], you are limited to the functionality of that version, both in terms of replication-specific functionality and the functionality of the product as a whole.  
  
-   Merge publications use a compatibility level, which determines what features can be used in a publication and allows you to support Subscribers running previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
### Publishing Tables in More Than One Publication  
 Replication supports publishing articles in multiple publications (including republishing data) with the following restrictions:  
  
-   If an article is published in a transactional publication and a merge publication, ensure that the *\@published_in_tran_pub* property is set to TRUE for the merge article. For more information about setting properties, see [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md) and [View and Modify Article Properties](../../../relational-databases/replication/publish/view-and-modify-article-properties.md).  
  
     You should also set the *\@published_in_tran_pub* property if an article is part of a transactional subscription and is included in a merge publication. If this is the case, be aware that by default transactional replication expects tables at the Subscriber to be treated as read-only; if merge replication makes data changes to a table in a transactional subscription, non-convergence of data can occur. To avoid this possibility, we recommend that any such table be specified as download-only in the merge publication. This prevents a merge Subscriber from uploading data changes to the table. For more information, see [Optimize Merge Replication Performance with Download-Only Articles](../../../relational-databases/replication/merge/optimize-merge-replication-performance-with-download-only-articles.md).  
  
-   An article cannot be published in both a merge publication and a transactional publication with queued updating subscriptions.  
  
-   Articles included in transactional publications that support updating subscriptions cannot be republished.  
  
-   If an article is published in more than one transactional publication that supports queued updating subscriptions, the following properties must have the same value for the article across all publications:  
  
    |Property|Parameter in sp_addarticle|  
    |--------------|---------------------------------|  
    |Identity range management|**\@auto_identity_range** (deprecated) and **\@identityrangemangementoption**|  
    |Publisher identity range|**\@pub_identity_range**|  
    |Identity range|**\@identity_range**|  
    |Identity range threshold|**\@threshold**|  
  
     For more information about these parameters, see [sp_addarticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md).  
  
-   If an article is published in more than one merge publication, the following properties must have the same value for the article across all publications:  
  
    |Property|Parameter in sp_addmergearticle|  
    |--------------|--------------------------------------|  
    |Column tracking|**\@column_tracking**|  
    |Schema options|**\@schema_option**|  
    |Column filtering|**\@vertical_partition**|  
    |Subscriber upload options|**\@subscriber_upload_options**|  
    |Conditional delete tracking|**\@delete_tracking**|  
    |Error compensation|**\@compensate_for_errors**|  
    |Identity range management|**\@auto_identity_range** (deprecated) and **\@identityrangemangementoption**|  
    |Publisher identity range|**\@pub_identity_range**|  
    |Identity range|**\@identity_range**|  
    |Identity range threshold|**\@threshold**|  
    |Partition options|**\@partition_options**|  
    |Blob column streaming|**\@stream_blob_columns**|  
    |Filter type|**\@filter_type** (parameter in **sp_addmergefilter**)|  
  
     For more information about these parameters, see [sp_addmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) and [sp_addmergefilter &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergefilter-transact-sql.md).  
  
-   Transactional replication and unfiltered merge replication support publishing a table in multiple publications and then subscribing within a single table in the subscription database (commonly referred to as a roll up scenario). Roll up is often used for aggregating subsets of data from multiple locations in one table at a central Subscriber. Filtered merge publications do not support the central Subscriber scenario. For merge replication, roll up is typically implemented through a single publication with parameterized row filters. For more information, see [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
## See Also  
 [Add Articles to and Drop Articles from Existing Publications](../../../relational-databases/replication/publish/add-articles-to-and-drop-articles-from-existing-publications.md)   
 [Configure Distribution](../../../relational-databases/replication/configure-distribution.md)   
 [Initialize a Subscription](../../../relational-databases/replication/initialize-a-subscription.md)   
 [Scripting Replication](../../../relational-databases/replication/scripting-replication.md)   
 [Secure the Publisher](../../../relational-databases/replication/security/secure-the-publisher.md)   
 [Subscribe to Publications](../../../relational-databases/replication/subscribe-to-publications.md)  
  
  
