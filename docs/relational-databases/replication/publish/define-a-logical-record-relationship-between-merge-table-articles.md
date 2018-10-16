---
title: "Define a Logical Record Relationship Between Merge Table Articles | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "merge replication logical records [SQL Server replication]"
  - "articles [SQL Server replication], logical records"
  - "logical records [SQL Server replication]"
ms.assetid: ff847b3a-c6b0-4eaf-b225-2ffc899c5558
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Define a Logical Record Relationship Between Merge Table Articles
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to define a logical record relationship between merge table articles in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or Replication Management Objects (RMO).  
  
 Merge replication allows you to define a relationship between related rows in different tables. These rows can then be processed as a transactional unit during synchronization. A logical record can be defined between two articles whether or not they have a join filter relationship. For more information, see [Group Changes to Related Rows with Logical Records](../../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
-   **To define a logical record relationship between merge table articles, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   If you add, modify, or delete a logical record after subscriptions to the publication have been initialized, you must generate a new snapshot and reinitialize all subscriptions after making the change. For more information about requirements for property changes, see [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Define logical records in the **Add Join** dialog box, which is available in the New Publication Wizard and the **Publication Properties - \<Publication>** dialog box. For more information about using the wizard and accessing the dialog box, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md) and [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
 Logical records can be defined in the **Add Join** dialog box only if they are applied to a join filter in a merge publication, and the publication follows the requirements for using precomputed partitions. To define logical records that are not applied to join filters and to set conflict detection and resolution at the logical record level, you must use stored procedures.  
  
#### To define a logical record relationship  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>** dialog box, select a row filter in the **Filtered Tables** pane.  
  
     A logical record relationship is associated with a join filter, which extends a row filter. Therefore you must define a row filter before you can extend the filter with a join and apply a logical record relationship. After one join filter is defined, you can extend this join filter with another join filter. For more information about defining join filters, see [Define and Modify a Join Filter Between Merge Articles](../../../relational-databases/replication/publish/define-and-modify-a-join-filter-between-merge-articles.md).  
  
2.  Click **Add**, and then click **Add Join to Extend the Selected Filter**.  
  
3.  Define a join filter in the **Add Join** dialog box, and then select the check box **Logical Record**.  
  
4.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  
  
#### To delete a logical record relationship  
  
-   Delete only the logical record relationship or delete the logical record relationship and the join filter associated with it.  
  
     To delete only the logical record relationship:  
  
    1.  On the **Filter Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>** dialog box, select the join filter associated with the logical record relationship in the **Filtered Tables** pane, and then click **Edit**.  
  
    2.  In the **Edit Join** dialog box, clear the check box **Logical Record**.  
  
    3.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
     To delete the logical record relationship and join filter associated with it:  
  
    -   On the **Filter Rows** page of the New Publication Wizard or **Publication Properties - \<Publication>** dialog box, select a filter in the **Filtered Tables** pane, and then click **Delete**. If the join filter you delete is itself extended by other joins, those joins will also be deleted.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 You can programmatically specify logical record relationships between articles using replication stored procedures.  
  
#### To define a logical record relationship without an associated join filter  
  
1.  If the publication contains any articles that are filtered, execute [sp_helpmergepublication](../../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md), and note the value of **use_partition_groups** in the result set.  
  
    -   If the value is **1**, then precomputed partitions are already being used.  
  
    -   If the value is **0**, then execute [sp_changemergepublication](../../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md) at the Publisher on the publication database. Specify a value of **use_partition_groups** for **@property** and a value of **true** for **@value**.  
  
        > [!NOTE]  
        >  If the publication does not support precomputed partitions, then logical records cannot be used. For more information, see Requirements for Using Precomputed Partitions in the topic [Optimize Parameterized Filter Performance with Precomputed Partitions](../../../relational-databases/replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md).  
  
    -   If the value is NULL, then the Snapshot Agent needs to be run to generate the initial snapshot for the publication.  
  
2.  If the articles that will comprise the logical record do not exist, execute [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) at the Publisher on the publication database. Specify one of the following conflict detection and resolution options for the logical record:  
  
    -   To detect and resolve conflicts that occur within related rows in the logic record, specify a value of **true** for **@logical_record_level_conflict_detection** and **@logical_record_level_conflict_resolution**.  
  
    -   To use the standard row- or column-level conflict detection and resolution, specify a value of **false** for **@logical_record_level_conflict_detection** and **@logical_record_level_conflict_resolution**, which is the default.  
  
3.  Repeat step 2 for each article that will comprise the logical record. You must use the same conflict detection and resolution option for each article in the logical record. For more information, see [Detecting and Resolving Conflicts in Logical Records](../../../relational-databases/replication/merge/advanced-merge-replication-conflict-resolving-in-logical-record.md).  
  
4.  At the publisher on the publication database, execute [sp_addmergefilter](../../../relational-databases/system-stored-procedures/sp-addmergefilter-transact-sql.md). Specify **@publication**, the name of one article in the relationship for **@article**, the name of the second article for **@join_articlename**, a name for the relationship for **@filtername**, a clause that defines the relationship between the two articles for **@join_filterclause**, the type of join for **@join_unique_key** and one of the following values for **@filter_type**:  
  
    -   **2** - Defines a logical relationship.  
  
    -   **3** - Defines a logical relationship with a join filter.  
  
    > [!NOTE]  
    >  If a join filter is not used, the direction of the relationship between the two articles is not important.  
  
5.  Repeat step 2 for each remaining logical record relationship in the publication.  
  
#### To change conflict detection and resolution for logical records  
  
1.  To detect and resolve conflicts that occur within related rows in the logical record:  
  
    -   At the Publisher on the publication database, execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). Specify a value of **logical_record_level_conflict_detection** for **@property** and a value of **true** for **@value**. Specify a value of **1** for **@force_invalidate_snapshot** and **@force_reinit_subscription**.  
  
    -   At the Publisher on the publication database, execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). Specify a value of **logical_record_level_conflict_resolution** for **@property** and a value of **true** for **@value**. Specify a value of **1** for **@force_invalidate_snapshot** and **@force_reinit_subscription**.  
  
2.  To use the standard row-level or column-level conflict detection and resolution:  
  
    -   At the Publisher on the publication database, execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). Specify a value of **logical_record_level_conflict_detection** for **@property** and a value of **false** for **@value**. Specify a value of **1** for **@force_invalidate_snapshot** and **@force_reinit_subscription**.  
  
    -   At the Publisher on the publication database, execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). Specify a value of **logical_record_level_conflict_resolution** for **@property** and a value of **false** for **@value**. Specify a value of **1** for **@force_invalidate_snapshot** and **@force_reinit_subscription**.  
  
#### To remove a logical record relationship  
  
1.  At the Publisher on the publication database, execute the following query to return information about all logical record relationships defined for the specified publication:  
  
     [!code-sql[HowTo#sp_ReturnMergeLogicalRecords](../../../relational-databases/replication/codesnippet/tsql/define-a-logical-record-_1.sql)]  
  
     Note the name of the logical record relationship being removed in the **filtername** column in the result set.  
  
    > [!NOTE]  
    >  This query returns the same information as [sp_helpmergefilter](../../../relational-databases/system-stored-procedures/sp-helpmergefilter-transact-sql.md); however, this system stored procedure only returns information about logical record relationships that are also join filters.  
  
2.  At the Publisher on the publication database, execute [sp_dropmergefilter](../../../relational-databases/system-stored-procedures/sp-dropmergefilter-transact-sql.md). Specify **@publication**, the name of one of the articles in the relationship for **@article**, and the name of the relationship from step 1 for **@filtername**.  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 This example enables precomputed partitions on an existing publication, and creates a logical record comprising the two new articles for the `SalesOrderHeader` and `SalesOrderDetail` tables.  
  
 [!code-sql[HowTo#sp_AddMergeLogicalRecord](../../../relational-databases/replication/codesnippet/tsql/define-a-logical-record-_2.sql)]  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
  
> [!NOTE]  
>  Merge replication allows you to specify that conflicts be tracked and resolved at the logical record level, but these options cannot be set using RMO.  
  
#### To define a logical record relationship without an associated join filter  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePublication> class, set the <xref:Microsoft.SqlServer.Replication.Publication.Name%2A> and <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A> properties for the publication, and set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the connection created in step 1.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object. If this method returns **false**, either the publication properties in step 2 were defined incorrectly or the publication does not exist.  
  
4.  If the <xref:Microsoft.SqlServer.Replication.MergePublication.PartitionGroupsOption%2A> property is set to <xref:Microsoft.SqlServer.Replication.PartitionGroupsOption.False>, set it to <xref:Microsoft.SqlServer.Replication.PartitionGroupsOption.True>.  
  
5.  If the articles that are to comprise the logical record do not exist, create an instance of the <xref:Microsoft.SqlServer.Replication.MergeArticle> class, and set the following properties:  
  
    -   The name of the article for <xref:Microsoft.SqlServer.Replication.Article.Name%2A>.  
  
    -   The name of the publication for <xref:Microsoft.SqlServer.Replication.Article.PublicationName%2A>.  
  
    -   (Optional) If the article is horizontally filtered, specify the row filter clause for the <xref:Microsoft.SqlServer.Replication.MergeArticle.FilterClause%2A> property. Use this property to specify a static or parameterized row filter. For more information, see [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
     For more information, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).  
  
6.  Call the <xref:Microsoft.SqlServer.Replication.Article.Create%2A> method.  
  
7.  Repeat steps 5 and 6 for each article comprising the logical record.  
  
8.  Create an instance of the <xref:Microsoft.SqlServer.Replication.MergeJoinFilter> class to define the logical record relationship between articles. Then, set the following properties:  
  
    -   The name of the child article in the logical record relationship for the <xref:Microsoft.SqlServer.Replication.MergeJoinFilter.ArticleName%2A> property.  
  
    -   The name of the existing, parent article in the logical record relationship for the <xref:Microsoft.SqlServer.Replication.MergeJoinFilter.JoinArticleName%2A> property.  
  
    -   A name for the logical record relationship for the <xref:Microsoft.SqlServer.Replication.MergeJoinFilter.FilterName%2A> property.  
  
    -   The expression that defines the relationship for the <xref:Microsoft.SqlServer.Replication.MergeJoinFilter.JoinFilterClause%2A> property.  
  
    -   A value of <xref:Microsoft.SqlServer.Replication.FilterTypes.LogicalRecordLink> for the <xref:Microsoft.SqlServer.Replication.MergeJoinFilter.FilterTypes%2A> property. If the logical record relationship is also a join filter, specify a value of <xref:Microsoft.SqlServer.Replication.FilterTypes.JoinFilterAndLogicalRecordLink> for this property. For more information, see [Group Changes to Related Rows with Logical Records](../../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
9. Call the <xref:Microsoft.SqlServer.Replication.MergeArticle.AddMergeJoinFilter%2A> method on the object that represents the child article in the relationship. Pass the <xref:Microsoft.SqlServer.Replication.MergeJoinFilter> object from step 8 to define the relationship.  
  
10. Repeat steps 8 and 9 for each remaining logical record relationship in the publication.  
  
###  <a name="PShellExample"></a> Example (RMO)  
 This example creates a logical record comprising the two new articles for the `SalesOrderHeader` and `SalesOrderDetail` tables.  
  
 [!code-cs[HowTo#rmo_CreateLogicalRecord](../../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_createlogicalrecord)]  
  
 [!code-vb[HowTo#rmo_vb_CreateLogicalRecord](../../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_createlogicalrecord)]  
  
## See Also  
 [Define and Modify a Join Filter Between Merge Articles](../../../relational-databases/replication/publish/define-and-modify-a-join-filter-between-merge-articles.md)   
 [Define and Modify a Parameterized Row Filter for a Merge Article](../../../relational-databases/replication/publish/define-and-modify-a-parameterized-row-filter-for-a-merge-article.md)   
 [Define and Modify a Static Row Filter](../../../relational-databases/replication/publish/define-and-modify-a-static-row-filter.md)   
 [Group Changes to Related Rows with Logical Records](../../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md)   
 [Optimize Parameterized Filter Performance with Precomputed Partitions](../../../relational-databases/replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md)   
 [Group Changes to Related Rows with Logical Records](../../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md)  
  
  
