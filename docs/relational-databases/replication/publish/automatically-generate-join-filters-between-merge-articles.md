---
title: "Automatically generate join filters (Merge)"
description: Describes how to automatically generate a set of join filters on the 'Filter Table Rows' page of the 'New Publication Wizard' for a Merge Publicaton in SQL Server Management Studio (SSMS).
ms.custom: seo-lt-2019
ms.date: "03/07/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "automatic join filter generation"
  - "join filters"
ms.assetid: 7ef419f4-c17f-42a5-9068-174a3ec08941
author: "MashaMSFT"
ms.author: "mathoma"
---
# Automatically Generate Join Filters Between Merge Articles
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Automatically generate a set of join filters on the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>** dialog box. For more information about using the wizard and accessing the dialog box, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md) and [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
> [!NOTE]  
>  If you automatically generate a set of join filters in the **Publication Properties - \<Publication>** dialog box after subscriptions to the publication have been initialized, you must generate a new snapshot and reinitialize all subscriptions after making the change. For more information about requirements for property changes, see [Change Publication and Article Properties](../../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
 Join filters can be created manually for a set of tables, or replication can generate the filters automatically based on the foreign key to primary key relationships defined on the tables. For more information about creating join filters manually, see [Define and Modify a Join Filter Between Merge Articles](../../../relational-databases/replication/publish/define-and-modify-a-join-filter-between-merge-articles.md).  
  
### To automatically generate a set of join filters between merge articles  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>**, click **Add**, and then click **Automatically Generate Filters**.  
  
    > [!NOTE]  
    >  Automatically generating filters deletes any existing row filters or join filters in the publication. You can add filters after automatically generating a set of filters.  
  
2.  Follow the process in the **Generate Filters** dialog box to create a row filter. The row filter is then extended to the tables related to the filtered table through primary key and foreign key relationships.  
  
    1.  Select a table to filter from the drop-down list box.  
  
    2.  Create a filter statement in the **Filter statement** text box. You can type directly in the text area, and you can also drag and drop columns from the **Columns** list box.  
  
         The **Filter statement** text area includes the default text, which is in the form of:  
  
        ```  
        SELECT <published_columns> FROM [tableowner].[tablename] WHERE  
        ```  
  
         The default text cannot be changed; type the filter clause for a static row filter or a parameterized row filter after the WHERE keyword using standard SQL syntax. The complete filter clause for a parameterized row filter would look like:  
  
        ```  
        SELECT <published_columns> FROM [HumanResources].[Employee] WHERE LoginID = SUSER_SNAME()  
        ```  
  
         The WHERE clause should use two-part naming; three-part naming and four-part naming are not supported.  
  
    3.  Specify filter options.  
  
         Select the option that matches how data will be shared among Subscribers: **A row from this table will go to multiple subscriptions** or **A row from this table will go to only one subscription**. If you select **A row from this table will go to only one subscription**, merge replication can optimize performance by storing and processing less metadata. However, you must ensure that the data is partitioned in such a way that a row cannot be replicated to more than one Subscriber. For more information, see the section "Setting 'partition options'" in the topic [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
3.  Select **OK**.
  
     The filter you specified is parsed and run against the table in the SELECT clause. If the filter statement contains syntax errors or other problems, you will be notified and will be able to edit the filter statement.  
  
     After the statement is parsed, replication creates the necessary join filters and displays them in the **Filtered Tables** pane on the **Filter Table Rows** or **Filter Rows** page. If you are generating filters from the New Publication Wizard and have not yet configured the Distributor for the Publisher against which this wizard is running, you are prompted to configure it.  
  
4.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  
  
### To modify a filter that was automatically generated  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>**, select a filter in the **Filtered Tables** pane, and then click **Edit**.  
  
2.  In the **Edit Filter** or **Edit Join** dialog box, modify the filter.  
  
3.  Select **OK**.
  
### To delete a filter that was automatically generated  
  
1.  On the **Filter Table Rows** page of the New Publication Wizard or the **Filter Rows** page of the **Publication Properties - \<Publication>**, select a filter in the **Filtered Tables** pane, and then click **Delete**.  
  
## See Also  
 [Join Filters](../../../relational-databases/replication/merge/join-filters.md)   
 [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md)  
  
  
