---
description: "Article Properties - &lt;Article&gt;"
title: "Article Properties - &lt;Article&gt; | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.newpubwizard.articleproperties.f1"
helpviewer_keywords: 
  - "Article Properties dialog box"
ms.assetid: 6dd601a4-1233-43d9-a9f0-bc8d84e5d188
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Article Properties - &lt;Article&gt;
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  The **Article Properties** dialog box is available from the New Publication Wizard and the **Publication Properties** dialog box. It allows you to view and set properties for all types of articles. Some properties can be set only when the publication is created, and others can be set only if the publication has no active subscriptions. Properties that cannot be set are displayed as read-only.  
  
> [!NOTE]  
>  After a publication is created, some property changes require a new snapshot. If a publication has subscriptions, some changes also require all subscriptions to be reinitialized. For more information, see [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
 Each property in the **Article Properties** dialog box includes a description. Click a property, and its description is displayed at the bottom of the dialog box. This topic provides additional information on a number of properties. The properties are grouped into the following categories:  
  
-   Properties that apply to all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications.  
  
-   Properties that apply to transactional publications from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Properties that apply to merge publications.  
  
-   Properties that apply to transactional and snapshot publications from Oracle Publishers.  
  
## Options for all publications  
 **Copy table partitioning schemes** and **Copy index partitioning schemes**  
 [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] introduced table partitioning and index partitioning, which are unrelated to the partitioning replication offers through row and column filters. The **Copy table partitioning schemes** and **Copy index partitioning schemes** options specify whether partitioning schemes should be copied to the Subscriber. For more information about partitioning, see [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).  
  
 **Convert data types**  
 Determines whether to convert from user-defined data types to base data types when creating objects at the Subscriber. User-defined data types include the user-defined CLR types introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. Specify a value of **True** if you will replicate these data types to previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]; this ensures they can be handled properly at the Subscriber.  
  
 **Create schemas at Subscriber**  
 [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] introduced schemas, which are defined using the CREATE SCHEMA statement. A schema is the owner of an object; it is used in a multi-part name, such as \<Database>.\<Schema>.\<Object>. If you have objects in the database owned by schemas other than DBO, replication can create these schemas at the Subscriber, so that published objects can be created.  
  
 If you will replicate data to versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] prior to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]:  
  
-   Set this option to **False**, because previous versions do not support CREATE SCHEMA.  
  
-   For each schema, add a user to the subscription database with the same name as the schema.  
  
 **Convert XML to NTEXT**, **Convert MAX data types to NTEXT and IMAGE**, **Convert new datetime to NVARCHAR**, **Convert filestream to MAX data types**, **Convert large CLR to MAX data types**, **Convert hierarchyId to MAX data types**, and **Convert spatial to MAX data types**.  
 Determines whether to convert the data types and attributes as described. Specify a value of **True** if you will replicate these data types to earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This ensures they can be handled properly at the Subscriber.  
  
 **Destination object name**  
 The name of the object created in the subscription database. This option cannot be changed for articles in publications that are enabled for peer-to-peer transactional replication.  
  
 **Destination object owner**  
 The schema under which the object is created in the subscription database. The default is the schema to which the object belongs in the publication database, with the following exceptions:  
  
-   For articles in merge publications with a compatibility level lower than 90: by default, the owner is left blank and is specified as **dbo** during the creation of the object on the Subscriber.  
  
-   For articles in Oracle publications: by default, the owner is specified as **dbo**.  
  
-   For articles in publications that use character mode snapshots (which are used for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers and [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers): by default, the owner is left blank. The owner defaults to the owner associated with the account used by the Distribution Agent or Merge Agent to connect to the Subscriber.  
  
 This option cannot be changed for articles in publications that are enabled for peer-to-peer transactional replication.  
  
 **Automatically manage identity ranges**  
 Replication, by default, manages all identity columns at the Publisher and each Subscriber. Each replication node is assigned a range of identity values (specified with the **Publisher range size** and **Subscriber range size** options) to ensure that a given value is used at only one node. For more information, see [Replicate Identity Columns](../../relational-databases/replication/publish/replicate-identity-columns.md).  
  
## Options for transactional publications  
 **Copy INSERT, UPDATE, and DELETE stored procedures**  
 If, in the **Statement Delivery** section of this dialog box, you select to use stored procedures to propagate changes to Subscribers (the default), select whether to copy the procedures to each Subscriber. If you select **False**, you must copy the procedures manually, or the Distribution Agent will fail when attempting to deliver changes.  
  
 **Statement delivery**  
 The options in this section apply to all tables, including indexed views that are replicated as tables. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you use the default options unless your application requires different functionality. By default, transactional replication propagates changes to Subscribers through a set of stored procedures that are installed on each Subscriber. When an insert, update, or delete occurs on a table at the Publisher, the operation is translated into a call to a stored procedure at the Subscriber.  
  
 The **delivery statement** options specify whether to use a stored procedure, and if so, which format should be used for parameters passed to the procedure. The **stored procedure** options allow you to use the procedures that replication automatically creates or substitute custom procedures you have created.  
  
 For more information, see [Specify How Changes Are Propagated for Transactional Articles](../../relational-databases/replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).  
  
 **Replicate**  
 This option applies to store procedures only. It determines whether to replicate the definition of the stored procedure (the CREATE PROCEDURE statement) or its execution. If you replicate the execution of the procedure, the procedure definition is replicated to the Subscriber when the subscription is initialized; when the procedure is executed at the Publisher, replication executes the corresponding procedure at the Subscriber. This can provide significantly better performance for cases where large batch operations are performed. For more information, see [Publishing Stored Procedure Execution in Transactional Replication](../../relational-databases/replication/transactional/publishing-stored-procedure-execution-in-transactional-replication.md).  
  
## Options for merge publications  
 The **Article Properties** dialog box for merge publications has two tabs: **Properties** and **Resolver**.  
  
### Properties tab  
 **Synchronization direction**  
 Determines whether changes can be uploaded from Subscribers that use the client subscription type:  
  
-   **Bidirectional** (the default): changes can be downloaded to the Subscriber and uploaded to the Publisher.  
  
-   **Download-only to Subscriber, prohibit Subscriber changes**: changes can be downloaded to the Subscriber, but they cannot be uploaded to the Publisher. Triggers prevent changes from being made at the Subscriber.  
  
-   **Download-only to Subscriber, allow Subscriber changes**: changes can be downloaded to the Subscriber, but they cannot be uploaded to the Publisher.  
  
 For more information, see [Optimize Merge Replication Performance with Download-Only Articles](../../relational-databases/replication/merge/optimize-merge-replication-performance-with-download-only-articles.md).  
  
 **Partition options**  
 Specifies the type of partitions that a parameterized filter creates. For more information, see the "Setting 'partition options'" section of [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
 **Tracking level**  
 Determines whether to treat changes to the same row or to the same column as a conflict.  
  
 **Verify INSERT permission**, **Verify UPDATE permission**, and **Verify DELETE permission**  
 Determines whether to check during synchronization that the Subscriber login has INSERT, UPDATE, or DELETE permissions on the published tables in the publication database. The default is **False** because merge replication does not require these permissions to be granted; access to the published tables is controlled through the Publication Access List (PAL). For more information about the PAL, see [Secure the Publisher](../../relational-databases/replication/security/secure-the-publisher.md).  
  
 You can require permissions to be checked if you want to allow one or more Subscribers to upload some changes to published data, but not others. For example, you could add a Subscriber to the PAL, but not give the Subscriber any permissions on the tables in the publication database. You could then set Verify DELETE permissions to **True**: the Subscriber would be able to upload inserts and updates, but not deletes.  
  
 **Multicolumn UPDATE**  
 When merge replication performs an update, it updates all changed columns in one UPDATE statement and resets unchanged columns to their original values. The alternative in these cases is to issue multiple UPDATE statements, with one UPDATE statement for each column that has changed. The multicolumn UPDATE statement is typically more efficient, but you should consider setting the option to **False** if triggers on the table are set to respond to updates of certain columns, and they respond inappropriately because those columns are reset when updates occur.  
  
> [!IMPORTANT]  
>  This option is deprecated and will be removed in a future release.  
  
### Resolver tab  
 **Use the default resolver**  
 If you select the default resolver, conflicts are resolved based on the priority assigned to each Subscriber or the first change written to the Publisher, depending on the type of subscriptions used. For more information, see [Detect and Resolve Merge Replication Conflicts](../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md).  
  
 **Use a custom resolver (registered at the Distributor)**  
 If you choose to use an article resolver (either one supplied by [!INCLUDE[msCoName](../../includes/msconame-md.md)] or one you have written), you must select a resolver from the list-box. For more information, see [Advanced Merge Replication Conflict Detection and Resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md).  
  
 If the resolver requires any input, specify it in **Enter information needed by the resolver** text box. For more information about input required by [!INCLUDE[msCoName](../../includes/msconame-md.md)] custom resolvers, see [Microsoft COM-Based Resolvers](../../relational-databases/replication/merge/advanced-merge-replication-conflict-com-based-resolvers.md).  
  
 **Allow Subscriber to resolve conflicts interactively during on-demand synchronization**  
 Select this option if Subscribers will use on-demand synchronization (the default for merge replication) and you want to resolve conflicts interactively. Specify on-demand synchronization on the **Synchronization Schedule** page of the New Subscription Wizard. To resolve conflicts interactively, use the Interactive Resolver user interface. For more information, see [Interactive Conflict Resolution](../../relational-databases/replication/merge/advanced-merge-replication-conflict-interactive-resolution.md).  
  
 **Require verification of a digital signature before merging**  
 All COM-based resolvers supplied by [!INCLUDE[msCoName](../../includes/msconame-md.md)] are signed. Select this option to verify that the resolver is valid when synchronizing.  
  
## Options for Oracle publications  
 The **Article Properties** dialog box for Oracle publications has two tabs: **Properties** and **Data Mapping**. Oracle publications do not support all of the properties that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publications support. For more information, see [Design Considerations and Limitations for Oracle Publishers](../../relational-databases/replication/non-sql/design-considerations-and-limitations-for-oracle-publishers.md).  
  
### Properties tab  
 **Copy INSERT, UPDATE, and DELETE stored procedures**  
 If the article is in a transactional publication and, in the **Statement Delivery** section of this dialog box, you select to use stored procedures to propagate changes to Subscribers (the default), select whether to copy the procedures to each Subscriber. If you select **False**, you must copy the procedures manually, or the Distribution Agent will fail when attempting to deliver changes.  
  
 **Destination object owner**  
 If you enter a value other than **dbo**:  
  
-   For Subscribers running [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later, you must ensure that a schema is created at the Subscriber with the same name as the value you enter. For more information, see [CREATE SCHEMA &#40;Transact-SQL&#41;](../../t-sql/statements/create-schema-transact-sql.md).  
  
-   For Subscribers running versions prior to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], for each schema, add a user to the subscription database with the same name as the schema.  
  
 **Tablespace name**  
 The tablespace in which to create the replication change tracking tables on the Oracle server instance. For more information, see [Manage Oracle Tablespaces](../../relational-databases/replication/non-sql/manage-oracle-tablespaces.md).  
  
 **Statement delivery**  
 The options in this section apply to all tables in transactional publications. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you use the default options unless your application requires different functionality. By default, transactional replication propagates changes to Subscribers through a set of stored procedures that are installed on each Subscriber. When an insert, update, or delete occurs on a table at the Publisher, the operation is translated into a call to a stored procedure at the Subscriber.  
  
 The **delivery statement** options specify whether to use a stored procedure, and if so, which format should be used for parameters passed to the procedure. The **stored procedure** options allow you to use the procedures that replication automatically creates or substitute custom procedures you have created.  
  
 For more information, see [Specify How Changes Are Propagated for Transactional Articles](../../relational-databases/replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).  
  
### Data Mapping tab  
 **Column name**  
 The name of the column at the Publisher (read-only).  
  
 **Publisher data type**  
 The Oracle data type for the column at the Publisher (read-only). The data type can only be changed directly in the Oracle database. For more information, see the Oracle documentation.  
  
 **Subscriber data type**  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type to which the Oracle data type is mapped when data is replicated:  
  
-   For some data types there is only one possible mapping, in which case the column in the property grid is read-only.  
  
-   For some types, there is more than one type that you can select. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you use the default mapping unless your application requires a different mapping. For more information, see [Data Type Mapping for Oracle Publishers](../../relational-databases/replication/non-sql/data-type-mapping-for-oracle-publishers.md).  
  
## See Also  
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [Create and Apply the Initial Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md)   
 [Reinitialize a Subscription](../../relational-databases/replication/reinitialize-a-subscription.md)   
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)  
  
  
