---
title: "Define & modify join filter between Merge articles"
description: Learn how to define and modify the join filter used between Merge articles using SQL Server Management Studio (SSMS) or Transact-SQL (T-SQL).
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "filters [SQL Server replication], join"
  - "merge replication join filters [SQL Server replication]"
  - "modifying filters, join"
  - "join filters"
ms.assetid: f7f23415-43ff-40f5-b3e0-0be1d148ee5b
author: "MashaMSFT"
ms.author: "mathoma"
---
# Define and Modify a Join Filter Between Merge Articles
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to define and modify a join filter between merge articles in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. Merge replication supports join filters, which are typically used in conjunction with parameterized filters to extend table partitioning to other related table articles.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
-   **To define and modify a join filter between merge articles, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   To create a join filter, a publication must contain at least two related tables. A join filter extends a row filter; therefore you must define a row filter on one table before you can extend the filter with a join to another table. After one join filter is defined, you can extend this join filter with another join filter if the publication contains additional related tables.  
  
-   If you add, modify, or delete a join filter after subscriptions to the publication have been initialized, you must generate a new snapshot and reinitialize all subscriptions after making the change. For more information about requirements for property changes, see [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   Join filters can be created manually for a set of tables, or replication can generate the filters automatically based on the relationships between foreign keys and primary keys defined on the tables. For more information on generating a set of join filters automatically, see [Automatically Generate a Set of Join Filters Between Merge Articles &#40;SQL Server Management Studio&#41;](../../../relational-databases/replication/publish/automatically-generate-join-filters-between-merge-articles.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Define, modify, and delete join filters on the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>** dialog box. For more information about using the wizard and accessing the dialog box, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md) and [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
#### To define a join filter  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>**, select an existing row filter or join filter in the **Filtered Tables** pane.  
  
2.  Click **Add**, and then click **Add Join to Extend the Selected Filter**.  
  
3.  Create the join statement: select either **Use the builder to create the statement** or **Write the join the statement manually**.  
  
    -   If you select to use the builder, use the columns in the grid (**Conjunction**, **Filtered table column**, **Operator**, and **Joined table column**) to build a join statement.  
  
         Each column in the grid contains a drop-down combo box, allowing you to select two columns and an operator (**=**, **<>**, **<=**, **\<**, **>=**, **>**, and **like**). The results are displayed in the **Preview** text area. If the join involves more than one pair of columns, select a conjunction (AND or OR) from the **Conjunction** column, and then enter two more columns and an operator.  
  
    -   If you select to write the statement manually, write the join statement in the **Join statement** text area. Use the **Filtered table columns** list box and the **Joined table columns** list box to drag and drop columns to the **Join statement** text area.  
  
    -   The complete join statement would appear like:  
  
        ```  
        SELECT <published_columns> FROM [Sales].[SalesOrderHeader] INNER JOIN [Sales].[SalesOrderDetail] ON [SalesOrderHeader].[SalesOrderID] = [SalesOrderDetail].[SalesOrderID]  
        ```  
  
         The JOIN clause should use two-part naming; three-part naming and four-part naming are not supported.  
  
4.  Specify join options:  
  
    -   If the column on which you join in the filtered table (the parent table) is unique, select **Unique key**.  
  
        > [!CAUTION]  
        >  Selecting this option indicates that the relationship between the child and parent tables in a join filter is one to one or one to many. Only select this option if you have a constraint on the joining column in the child table that guarantees uniqueness. If the option is set incorrectly, non-convergence of data can occur.  
  
    -   By default, merge replication processes changes on a row-by-row basis during synchronization. To have related changes in rows of both the filtered table and the joined table processed as a unit, select **Logical record** ([!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions only). This option is available only if the article and publication requirements for using logical records are met. For more information see the section "Considerations for Using Logical Records" in [Group Changes to Related Rows with Logical Records](../../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
5.  Select **OK**.

6.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  

#### To modify a join filter  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>**, select a filter in the **Filtered Tables** pane, and then click **Edit**.  
  
2.  In the **Edit Join** dialog box, modify the filter.  
  
3.  Select **OK**.
  
#### To delete a join filter  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>**, select a filter in the **Filtered Tables** pane, and then click **Delete**. If the join filter you delete is itself extended by other joins, those joins will also be deleted.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 These procedures show a parameterized filter on a parent article with join filters between this article and related child articles. Join filters can be defined and modified programmatically using replication stored procedures.  
  
#### To define a join filter to extend an article filter to related articles in a merge publication  
  
1.  Define the filtering for the article being joined to, which is also known as the parent article.  
  
    -   For an article filtered using a parameterized row filter, see [Define and Modify a Parameterized Row Filter for a Merge Article](../../../relational-databases/replication/publish/define-and-modify-a-parameterized-row-filter-for-a-merge-article.md).  
  
    -   For an article filtered using a static row filter, see [Define and Modify a Static Row Filter](../../../relational-databases/replication/publish/define-and-modify-a-static-row-filter.md).  
  
2.  At the Publisher on the publication database, execute [sp_addmergearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md) to define one or more related articles, which are also known as child articles, for the publication. For more information, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).  
  
3.  At the Publisher on the publication database, execute [sp_addmergefilter &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergefilter-transact-sql.md). Specify `@publication`, a unique name for this filter for `@filtername`, the name of the child article created in step 2 for `@article`, the name of the parent article being joined to for `@join_articlename`, and one of the following values for `@join_unique_key`:  
  
    -   **0** - indicates a many-to-one or many-to-many join between the parent and child articles.  
  
    -   **1** - indicates a one-to-one or one-to-many join between the parent and child articles.  
  
     This defines a join filter between the two articles.  
  
    > [!CAUTION]  
    >  Only set `@join_unique_key` to **1** if you have a constraint on the joining column in the underlying table for the parent article that guarantees uniqueness. If `@join_unique_key` is set to **1** incorrectly, non-convergence of data may occur.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 This example defines an article for a merge publication, where the `SalesOrderDetail` table article is filtered against the `SalesOrderHeader` table that is itself filtered using a static row filter. For more information, see [Define and Modify a Static Row Filter](../../../relational-databases/replication/publish/define-and-modify-a-static-row-filter.md).  
  
 [!code-sql[HowTo#sp_AddMergeArticle](../../../relational-databases/replication/codesnippet/tsql/define-and-modify-a-join_1.sql)]  
  
 This example defines a group of articles in a merge publication where the articles are filtered with a series of join filters against the `Employee` table that is itself filtered using a parameterized row filter on the value of [HOST_NAME](../../../t-sql/functions/host-name-transact-sql.md) in the **LoginID** column. For more information, see [Define and Modify a Parameterized Row Filter for a Merge Article](../../../relational-databases/replication/publish/define-and-modify-a-parameterized-row-filter-for-a-merge-article.md).  
  
 [!code-sql[HowTo#sp_MergeDynamicPub1](../../../relational-databases/replication/codesnippet/tsql/define-and-modify-a-join_2.sql)]  
  
## See Also  
 [Join Filters](../../../relational-databases/replication/merge/join-filters.md)   
 [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md)   
 [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md)   
 [Filter Published Data for Merge Replication](../../../relational-databases/replication/merge/filter-published-data-for-merge-replication.md)   
 [How to: Define and Modify a Join Filter Between Merge Articles (SQL Server Management Studio)](../../../relational-databases/replication/publish/define-and-modify-a-join-filter-between-merge-articles.md)   
 [Replication System Stored Procedures Concepts](../../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md)   
 [Define a Logical Record Relationship Between Merge Table Articles](../../../relational-databases/replication/publish/define-a-logical-record-relationship-between-merge-table-articles.md)   
 [Define and Modify a Parameterized Row Filter for a Merge Article](../../../relational-databases/replication/publish/define-and-modify-a-parameterized-row-filter-for-a-merge-article.md)  
  
  
