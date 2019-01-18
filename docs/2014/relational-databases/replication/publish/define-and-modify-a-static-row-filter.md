---
title: "Define and Modify a Static Row Filter | Microsoft Docs"
ms.custom: ""
ms.date: "06/30/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying filters, static row"
  - "static row filters"
  - "filters [SQL Server replication], static row"
ms.assetid: a6ebb026-026f-4c39-b6a9-b9998c3babab
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Define and Modify a Static Row Filter
  This topic describes how to define and modify a static row filter in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
-   **To define and modify a static row filter, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   If you add, modify, or delete a static row filter after subscriptions to the publication have been initialized, you must generate a new snapshot and reinitialize all subscriptions after making the change. For more information about requirements for property changes, see [Change Publication and Article Properties](change-publication-and-article-properties.md).  
  
-   If the publication is enabled for peer-to-peer transactional replication, tables cannot be filtered.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   Because these filters are static, all subscribers will receive the same subset of the data. If you need to dynamically filter rows in a table article belonging to a merge publication so that each subscriber receives a different partition of the data, see [Define and Modify a Parameterized Row Filter for a Merge Article](define-and-modify-a-parameterized-row-filter-for-a-merge-article.md). Merge replication also enables you to filter related rows based on an existing row filter. For more information, see [Define and Modify a Join Filter Between Merge Articles](define-and-modify-a-join-filter-between-merge-articles.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Define, modify, and delete static row filters on the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>** dialog box. For more information about using the wizard and accessing the dialog box, see [Create a Publication](create-a-publication.md) and [View and Modify Publication Properties](view-and-modify-publication-properties.md).  
  
#### To define a static row filter  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>** dialog box, the action you take depends on the type of publication:  
  
    -   For a snapshot or transactional publication, click **Add**.  
  
    -   For a merge publication, click **Add**, and then click **Add Filter**.  
  
2.  In the **Add Filter** dialog box, select a table to filter from the drop-down list box.  
  
3.  Create a filter statement in the **Filter statement** text area. You can type directly in the text area, and you can also drag and drop columns from the **Columns** list box.  
  
    > [!NOTE]  
    >  The WHERE clause should use two-part naming; three-part naming and four-part naming are not supported. If the publication is from an Oracle Publisher, the WHERE clause must be compliant with Oracle syntax.  
  
    -   The **Filter statement** text area includes the default text, which is in the form of:  
  
        ```tsql  
        SELECT <published_columns> FROM [schema].[tablename] WHERE  
        ```  
  
    -   The default text cannot be changed; type the filter clause after the WHERE keyword using standard SQL syntax. The complete filter clause would appear like:  
  
        ```tsql  
        SELECT <published_columns> FROM [HumanResources].[Employee] WHERE [LoginID] = 'adventure-works\ranjit0'  
        ```  
  
    -   A static row filter can include a user-defined function. The complete filter clause for a static row filter with a user-defined function would appear like:  
  
        ```tsql  
        SELECT <published_columns> FROM [Sales].[SalesOrderHeader] WHERE MyFunction([Freight]) > 100  
        ```  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
5.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  
  
#### To modify a static row filter  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>** dialog box, select a filter in the **Filtered Tables** pane, and then click **Edit**.  
  
2.  In the **Edit Filter** dialog box, modify the filter.  
  
3.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
#### To delete a static row filter  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>** dialog box, select a filter in the **Filtered Tables** pane, and then click **Delete**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 When creating table articles, you can define a WHERE clause to filter rows out of an article. You can also change a row filter after it has been defined. Static row filters can be created and modified programmatically using replication stored procedures.  
  
#### To define a static row filter for a snapshot or transactional publication  
  
1.  Define the article to filter. For more information, see [Define an Article](define-an-article.md).  
  
2.  At the Publisher on the publication database, execute [sp_articlefilter &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-articlefilter-transact-sql). Specify the name of the article for **@article**, the name of the publication for **@publication**, a name for the filter for **@filter_name**, and the filtering clause for **@filter_clause** (not including `WHERE`).  
  
3.  If a column filter must still be defined, see [Define and Modify a Column Filter](define-and-modify-a-column-filter.md). Otherwise, execute [sp_articleview &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-articleview-transact-sql). Specify the publication name for **@publication**, the name of the filtered article for **@article**, and the filter clause specified in step 2 for **@filter_clause**. This creates the synchronization objects for the filtered article.  
  
#### To modify a static row filter for a snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_articlefilter &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-articlefilter-transact-sql). Specify the name of the article for **@article**, the name of the publication for **@publication**, a name for the new filter for **@filter_name**, and the new filtering clause for **@filter_clause** (not including `WHERE`). Because this change will invalidate data in existing subscriptions, specify a value of **1** for **@force_reinit_subscription**.  
  
2.  At the Publisher on the publication database, execute [sp_articleview &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-articleview-transact-sql). Specify the publication name for **@publication**, the name of the filtered article for **@article**, and the filter clause specified in step 1 for **@filter_clause**. This re-creates the view that defines the filtered article.  
  
3.  Rerun the Snapshot Agent job for the publication to generate an updated snapshot. For more information, see [Create and Apply the Initial Snapshot](../create-and-apply-the-initial-snapshot.md).  
  
4.  Reinitialize subscriptions. For more information, see [Reinitialize Subscriptions](../reinitialize-subscriptions.md).  
  
#### To delete a static row filter for a snapshot or transactional publication  
  
1.  At the Publisher on the publication database, execute [sp_articlefilter &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-articlefilter-transact-sql). Specify the name of the article for **@article**, the name of the publication for **@publication**, a value of NULL for **@filter_name**, and a value of NULL for **@filter_clause**. Because this change will invalidate data in existing subscriptions, specify a value of **1** for **@force_reinit_subscription**.  
  
2.  Rerun the Snapshot Agent job for the publication to generate an updated snapshot. For more information, see [Create and Apply the Initial Snapshot](../create-and-apply-the-initial-snapshot.md).  
  
3.  Reinitialize subscriptions. For more information, see [Reinitialize Subscriptions](../reinitialize-subscriptions.md).  
  
#### To define a static row filter for a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_addmergearticle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql). Specify the filtering clause for **@subset_filterclause** (not including `WHERE`). For more information, see [Define an Article](define-an-article.md).  
  
2.  If a column filter must still be defined, see [Define and Modify a Column Filter](define-and-modify-a-column-filter.md).  
  
#### To modify a static row filter for a merge publication  
  
1.  At the Publisher on the publication database, execute [sp_changemergearticle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql). Specify the publication name for **@publication**, the name of the filtered article for **@article**, a value of **subset_filterclause** for **@property**, and the new filtering clause for **@value** (not including `WHERE`). Because this change will invalidate data in existing subscriptions, specify a value of 1 for **@force_reinit_subscription**.  
  
2.  Rerun the Snapshot Agent job for the publication to generate an updated snapshot. For more information, see [Create and Apply the Initial Snapshot](../create-and-apply-the-initial-snapshot.md).  
  
3.  Reinitialize subscriptions. For more information, see [Reinitialize Subscriptions](../reinitialize-subscriptions.md).  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 In this transactional replication example, the article is filtered horizontally to remove all discontinued products.  
  
 [!code-sql[HowTo#sp_AddTranArticle](../../../snippets/tsql/SQL15/replication/howto/tsql/createtranpub.sql#sp_addtranarticle)]  
  
 In this merge replication example, the articles are filtered horizontally to return only rows that belong to the specified salesperson. A join filter is also used. For more information, see [Define and Modify a Join Filter Between Merge Articles](define-and-modify-a-join-filter-between-merge-articles.md).  
  
 [!code-sql[HowTo#sp_AddMergeArticle](../../../snippets/tsql/SQL15/replication/howto/tsql/createmergepub.sql#sp_addmergearticle)]  
  
## See Also  
 [Define and Modify a Parameterized Row Filter for a Merge Article](define-and-modify-a-parameterized-row-filter-for-a-merge-article.md)   
 [Change Publication and Article Properties](change-publication-and-article-properties.md)   
 [Filter Published Data](filter-published-data.md)   
 [Filter Published Data for Merge Replication](../merge/filter-published-data-for-merge-replication.md)  
  
  
