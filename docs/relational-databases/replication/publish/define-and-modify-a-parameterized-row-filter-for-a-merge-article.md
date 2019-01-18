---
title: "Define and Modify a Parameterized Row Filter for a Merge Article | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "parameterized filters [SQL Server replication], defining"
  - "parameterized filters [SQL Server replication], modifying"
  - "merge replication [SQL Server replication], dynamic filters"
  - "merge replication parameterized filters [SQL Server replication]"
  - "filters [SQL Server replication], parameterized"
  - "modifying filters, parameterized row"
  - "dynamic filters [SQL Server replication]"
ms.assetid: de0482a2-3cc8-4030-8a4a-14364549ac9f
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Define and Modify a Parameterized Row Filter for a Merge Article
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to define and modify a parameterized row filter in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
 When creating table articles, you can use parameterized row filters. These filters use a [WHERE](../../../t-sql/queries/where-transact-sql.md) clause to select the appropriate data to be published. Rather than specifying a literal value in the clause (as you do with a static row filter), you specify one or both of the following system functions: [SUSER_SNAME](../../../t-sql/functions/suser-sname-transact-sql.md) and [HOST_NAME](../../../t-sql/functions/host-name-transact-sql.md). For more information, see [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
-   **To define and modify a parameterized row filter, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   If you add, modify, or delete a parameterized row filter after subscriptions to the publication have been initialized, you must generate a new snapshot and reinitialize all subscriptions after making the change. For more information about requirements for property changes, see [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   For performance reasons, we recommend that you not apply functions to column names in parameterized row filter clauses, such as `LEFT([MyColumn]) = SUSER_SNAME()`. If you use HOST_NAME in a filter clause and override the HOST_NAME value, it might be necessary to convert data types using CONVERT. For more information about best practices for this case, see the section "Overriding the HOST_NAME() Value" in the topic [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Define, modify, and delete parameterized row filters on the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>** dialog box. For more information about using the wizard and accessing the dialog box, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md) and [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
#### To define a parameterized row filter  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>**, click **Add**, and then click **Add Filter**.  
  
2.  In the **Add Filter** dialog box, select a table to filter from the drop-down list box.  
  
3.  Create a filter statement in the **Filter statement** text box. You can type directly in the text area, and you can also drag and drop columns from the **Columns** list box.  
  
    -   The **Filter statement** text area includes the default text, which is in the form of:  
  
        ```  
        SELECT <published_columns> FROM [tableowner].[tablename] WHERE  
        ```  
  
    -   The default text cannot be changed; type the filter clause after the WHERE keyword using standard SQL syntax. A parameterized filter includes a call to the system function **HOST_NAME()** and/or **SUSER_SNAME()**, or a user-defined function that references one or both of these functions. The following is an example of a complete filter clause for a parameterized row filter:  
  
        ```  
        SELECT <published_columns> FROM [HumanResources].[Employee] WHERE LoginID = SUSER_SNAME()  
        ```  
  
         The WHERE clause should use two-part naming; three-part naming and four-part naming are not supported.  
  
4.  Select the option that matches how data will be shared among Subscribers:  
  
    -   **A row from this table will go to multiple subscriptions**  
  
    -   **A row from this table will go to only one subscription**  
  
     If you select **A row from this table will go to only one subscription**, merge replication can optimize performance by storing and processing less metadata. However, you must ensure that the data is partitioned in such a way that a row cannot be replicated to more than one Subscriber. For more information, see the section "Setting 'partition options'" in the topic [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
5.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
6.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  
  
#### To modify a parameterized row filter  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>**, select a filter in the **Filtered Tables** pane, and then click **Edit**.  
  
2.  In the **Edit Filter** dialog box, modify the filter.  
  
3.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
#### To delete a parameterized row filter  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>**, select a filter in the **Filtered Tables** pane, and then click **Delete**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Parameterized row filters can be created and modified programmatically using replication stored procedures.  
  
#### To define a parameterized row filter for an article in a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md). Specify **@publication**, a name for the article for **@article**, the table being published for **@source_object**, the WHERE clause that defines the parameterized filter for **@subset_filterclause** (not including `WHERE`), and one of the following values for **@partition_options**, which describes the type of partitioning that will result from the parameterized row filter:  
  
    -   **0** - Filtering for the article either is static or does not yield a unique subset of data for each partition (an "overlapping" partition).  
  
    -   **1** - Resulting partitions are overlapping, and updates made at the Subscriber cannot change the partition to which a row belongs.  
  
    -   **2** - Filtering for the article yields nonoverlapping partitions, but multiple Subscribers can receive the same partition.  
  
    -   **3** - Filtering for the article yields nonoverlapping partitions that are unique for each subscription.  
  
#### To change a parameterized row filter for an article in a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md). Specify **@publication**, **@article**, a value of **subset_filterclause** for **@property**, the expression that defines the parameterized filter for **@value** (not including `WHERE`), and a value of **1** for both **@force_invalidate_snapshot** and **@force_reinit_subscription**.  
  
2.  If this change results in different partitioning behavior, then execute [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md) again. Specify **@publication**, **@article**, a value of **partition_options** for **@property**, and the most appropriate partitioning option for **@value**, which can be one of the following:  
  
    -   **0** - Filtering for the article either is static or does not yield a unique subset of data for each partition (an "overlapping" partition).  
  
    -   **1** - Resulting partitions are overlapping, and updates made at the Subscriber cannot change the partition to which a row belongs.  
  
    -   **2** - Filtering for the article yields nonoverlapping partitions, but multiple Subscribers can receive the same partition.  
  
    -   **3** - Filtering for the article yields nonoverlapping partitions that are unique for each subscription.  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 This example defines a group of articles in a merge publication where the articles are filtered with a series of join filters against the `Employee` table that is itself filtered using a parameterized row filter on the **LoginID** column. During synchronization, the value returned by the [HOST_NAME](../../../t-sql/functions/host-name-transact-sql.md) function is overridden. For more information, see Overriding the HOST_NAME() Value in the topic [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
 [!code-sql[HowTo#sp_MergeDynamicPub1](../../../relational-databases/replication/codesnippet/tsql/define-and-modify-a-para_1.sql)]  
  
## See Also  
 [Define and Modify a Join Filter Between Merge Articles](../../../relational-databases/replication/publish/define-and-modify-a-join-filter-between-merge-articles.md)   
 [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md)   
 [Join Filters](../../../relational-databases/replication/merge/join-filters.md)   
 [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md)  
  
  
