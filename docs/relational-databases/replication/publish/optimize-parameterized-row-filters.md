---
description: "Optimize Parameterized Row Filters"
title: "Optimize Parameterized Row Filters | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "precomputed partitions [SQL Server replication]"
  - "filters [SQL Server replication], parameterized"
  - "merge replication precomputed partitions [SQL Server replication], SQL Server Management Studio"
  - "parameterized filters [SQL Server replication], optimizing"
ms.assetid: 49349605-ebd0-4757-95be-c0447f30ba13
author: "MashaMSFT"
ms.author: "mathoma"
---
# Optimize Parameterized Row Filters
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to optimize parameterized row filters in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Recommendations](#Recommendations)  
  
-   **To optimize parameterized row filters, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   When you use parameterized filters, you can control how the filters are processed by merge replication by specifying either the **use partition groups** option or the **keep partition changes** option when you create a publication. These options improve the synchronization performance for publications with filtered articles by storing additional metadata in the publication database. You can control how the data is shared among Subscribers by setting **partition options** when you create an article. For more information about these requirements, see [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
     With [!INCLUDE[ssEW](../../../includes/ssew-md.md)]SQL Server Compact subscribers, keep_partition_changes must be set to true to ensure that deletes are correctly propagated. When set to false, the subscriber might have more rows than expected.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 The following settings can be used to optimize parameterized row filters:  
  
 **Partition Options**  
 Set this option on the **Properties** page of the **Article Properties - \<Article>** dialog box, or in the **Add Filter** dialog box. Both dialog boxes are available in the New Publication Wizard and the **Publication Properties - \<Publication>** dialog box. The **Article Properties - \<Article>** dialog box allows you to specify additional values for this option that are not available in the **Add Filter** dialog box.  
  
 **Precompute Partitions**  
 This option is set to **True** by default if the articles in your publication adhere to a set of requirements. For more information about these requirements, see [Optimize Parameterized Filter Performance with Precomputed Partitions](../../../relational-databases/replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md). Modify this option on the **Subscription Options** page of the **Publication Properties - \<Publication>** dialog box.  
  
 **Optimize Synchronization**  
 This option should be set to **True** only if **Precompute Partitions** is set to **False**. Set this option on the **Subscription Options** page of the **Publication Properties - \<Publication>** dialog box.  
  
 For more information about using the New Publication Wizard and accessing the **Publication Properties - \<Publication>** dialog box, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md) and [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
#### To set Partition options in the Add Filter or Edit Filter dialog box  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>** dialog box, click **Add**, and then click **Add Filter**.  
  
2.  Create a parameterized filter. For more information, see [Define and Modify a Parameterized Row Filter for a Merge Article](../../../relational-databases/replication/publish/define-and-modify-a-parameterized-row-filter-for-a-merge-article.md).  
  
3.  Select the option that matches how data will be shared among Subscribers:  
  
    -   **A row from this table will go to multiple subscriptions**  
  
    -   **A row from this table will go to only one subscription**  
  
     If you select **A row from this table will go to only one subscription**, merge replication can optimize performance by storing and processing less metadata. However, you must ensure that the data is partitioned in such a way that a row cannot be replicated to more than one Subscriber. For more information, see the section "Setting 'partition options'" in the topic [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
4.  Select **OK**.
  
5.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  
  
#### To set Partition Options in the Article Properties - \<Article> dialog box  
  
1.  On the **Articles** page of the New Publication Wizard or the **Publication Properties - \<Publication>** dialog box, select a table, and then click **Article Properties**.  
  
2.  Click **Set Properties of Highlighted Table Article** or **Set Properties of All Table Articles**.  
  
3.  In the **Destination Object** section of the **Properties** tab of the **Article Properties - \<Article>** dialog box, specify one of the following values for **Partition Options**:  
  
    -   **Overlapping**  
  
    -   **Overlapping, disallow out-of-partition data changes**  
  
    -   **Nonoverlapping, single subscription**  
  
    -   **Nonoverlapping, shared between subscriptions**  
  
     For more information about these options and how they relate to the options available in the **Add Filter** and **Edit Filter** dialog boxes, see the "Setting 'partition options'" section of [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
4.  Select **OK**.
  
5.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  
  
#### To set Precompute Partitions  
  
1.  On the **Subscription Options** page of the **Publication Properties - \<Publication>** dialog box, select a value for the **Precompute Partitions** option. The property is read-only if:  
  
    -   The publication does not meet the requirements for precomputed partitions.  
  
    -   A snapshot has not yet been generated for the publication. In this case, the option displays a value of **Set automatically when a snapshot is created**.  
  
2.  Select **OK**.
  
#### To set Optimize Synchronization  
  
1.  On the **Subscription Options** page of the **Publication Properties - \<Publication>** dialog box, select a value of `True` for the **Optimize Synchronization** option.  
  
2.  Select **OK**.
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 For definitions of the filtering options for `@keep_partition_changes` and `@use_partition_groups`, see [sp_addmergepublication](../../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md).  
  
#### To specify merge filter optimizations when creating a new publication  
  
1.  At the Publisher on the publication database, execute [sp_addmergepublication](../../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md). Specify `@publication` and a value of `true` for one the following parameters:  
  
    -   `@use_partition_groups`: - the highest performance optimization, provided that the articles conform to the requirements for precomputed partitions. For more information, see [Optimize Parameterized Filter Performance with Precomputed Partitions](../../../relational-databases/replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md).  
  
    -   `@keep_partition_changes` - use this optimization if precomputed partitions cannot be used.  
  
2.  Add a snapshot job for the publication. For more information see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md).  
  
3.  At the Publisher on the publication database, execute [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md), specifying the following parameters:  
  
    -   `@publication` - the name of the publication from step 1.  
  
    -   `@article` - a name for the article  
  
    -   `@source_object` - the database object being published.  
  
    -   `@subset_filterclause` - the optional parameterized filter clause used to horizontally filter the article.  
  
    -   `@partition_options`- the partition options for the filtered article.  
  
4.  Repeat step 3 for each article in the publication.  
  
5.  (Optional) At the Publisher on the publication database, execute [sp_addmergefilter](../../../relational-databases/system-stored-procedures/sp-addmergefilter-transact-sql.md) to define a join filter between two articles. For more information, see [Define and Modify a Join Filter Between Merge Articles](../../../relational-databases/replication/publish/define-and-modify-a-join-filter-between-merge-articles.md).  
  
#### To view and modify merge filter behaviors for an existing publication  
  
1.  (Optional) At the Publisher on the publication database, execute [sp_helpmergepublication](../../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md), specifying `@publication`. Note the value of `keep_partition_changes` and `use_partition_groups` in the result set.  
  
2.  (Optional) At the Publisher on the publication database, execute [sp_changemergepublication](../../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md). Specify a value of `use_partition_groups` for `@property` and either `true` or `false` for `@value`.  
  
3.  (Optional) At the Publisher on the publication database, execute [sp_changemergepublication](../../../relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql.md). Specify a value of `keep_partition_changes` for `@property` and either `true` or `false` for `@value`.  
  
    > [!NOTE]  
    >  When enabling `keep_partition_changes`, you must first disable `use_partition_groups` and specify a value of `1` for `@force_reinit_subscription`.  
  
4.  (Optional) At the Publisher on the publication database, execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). Specify a value of `partition_options` for `@property` and the appropriate value for `@value`. See [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) for definitions of these filtering options.  
  
5.  (Optional) Start the Snapshot Agent to regenerate the snapshot if necessary. For information about which changes require a new snapshot to be generated, see [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
## See Also  
 [Automatically Generate a Set of Join Filters Between Merge Articles &#40;SQL Server Management Studio&#41;](../../../relational-databases/replication/publish/automatically-generate-join-filters-between-merge-articles.md)   
 [Define and Modify a Parameterized Row Filter for a Merge Article](../../../relational-databases/replication/publish/define-and-modify-a-parameterized-row-filter-for-a-merge-article.md)   
 [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md)  
  
  
