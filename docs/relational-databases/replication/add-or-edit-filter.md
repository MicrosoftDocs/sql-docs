---
description: "Add or Edit Filter"
title: "Add or Edit Filter | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.newpubwizard.addeditfilter.f1"
ms.assetid: bdd7c71d-1c59-4044-bfe8-c85f908345bb
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Add or Edit Filter
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  The **Add Filter** and **Edit Filter** dialog boxes allow you to add and edit static row filters and parameterized row filters.  
  
> [!NOTE]  
>  Editing a filter in an existing publication requires a new snapshot for the publication. If a publication has subscriptions, the subscriptions must be reinitialized. For more information about property changes, see [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
 All publication types can include static filters; merge publications can also include parameterized filters. A static filter is evaluated when the publication is created: all Subscribers to the publication receive the same data. A parameterized filter is evaluated during replication synchronization: different Subscribers can receive different partitions of data based on the login or computer name of each Subscriber. Click the **Example statements** link in the dialog box to see examples of each type of filter. For more information about filtering options, see [Filter Published Data](../../relational-databases/replication/publish/filter-published-data.md).  
  
 Using row filters, you can specify a subset of rows to be published from a table. Row filters can be used to eliminate rows that users do not need to see (such as rows that contain sensitive or confidential information), or to create different partitions of data that are sent to different Subscribers. Publishing different partitions of data to different Subscribers can also help avoid conflicts that would otherwise be caused by multiple Subscribers updating the same data.  
  
## Options  
 This dialog box involves a two-step process for transactional and snapshot publications and a three-step process for merge publications. All publication types require you to select a table to be filtered and one or more columns to be included in the filter; the filter is defined as a standard WHERE clause.  
  
1.  **Select the table to filter**  
  
     If you are editing an existing filter, the table selection cannot be changed. If you are adding a new filter, select a table from the drop-down list box. Tables appear in the list box only if they were selected on the **Articles** page and do not already have a row filter. If a table has a row filter and you want to define a new one:  
  
    1.  Click **Cancel** on the **Add Filter** dialog box.  
  
    2.  Select the table in the filter pane on the **Filter Table Rows** page and click **Edit**.  
  
    3.  Edit an existing filter in the **Edit Filter** dialog box.  
  
2.  **Complete the filter statement to identify which table rows Subscribers will receive**  
  
     Define a new filter statement or edit an existing one. The **Columns** list box lists all the columns that you are publishing from the table you selected in **Select the table to filter**. The **Filter statement** text area includes the default text, which is in the form of:  
  
     `SELECT <published_columns> FROM [schema].[tablename] WHERE`  
  
     This text cannot be changed; type the filter clause after the WHERE keyword using standard [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax. If the Publisher is an Oracle Publisher, the WHERE clause must be compliant with Oracle query syntax. Avoid using complex filters when possible. Both static and parameterized filters increase processing time for publications; therefore you should keep filter statements as simple as possible.  
  
    > [!IMPORTANT]  
    >  For performance reasons, we recommended that you not apply functions to column names in parameterized row filter clauses for merge publications, such as `LEFT([MyColumn]) = SUSER_SNAME()`. If you use HOST_NAME in a filter clause and override the HOST_NAME value, it might be necessary to convert data types using CONVERT. For more information about best practices for this case, see the section "Overriding the HOST_NAME() Value" in the topic [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
3.  **Specify how many subscriptions will receive data from this table**  
  
     [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions only; merge replication only. Merge replication allows you to specify the type of partitions that are best suited to your data and application. If you select **A row from this table will go to only one subscription**, merge replication sets the nonoverlapping partitions option. Nonoverlapping partitions work in conjunction with precomputed partitions to improve performance, with nonoverlapping partitions minimizing the upload cost associated with precomputed partitions. The performance benefit of nonoverlapping partitions is more noticeable when the parameterized filters and join filters used are more complex. If you select this option, you must ensure that the data is partitioned in such a way that a row cannot be replicated to more than one Subscriber. For more information, see the section "Setting 'partition options'" in the topic [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).  
  
 After you have added or edited a filter, click **OK** to save changes and close the dialog box. The filter you specified is parsed and run against the table in the SELECT clause. If the filter statement contains syntax errors or other problems, you are notified and are able to edit the filter statement.  
  
## See Also  
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [Filter Published Data](../../relational-databases/replication/publish/filter-published-data.md)   
 [Join Filters](../../relational-databases/replication/merge/join-filters.md)   
 [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md)   
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)  
  
  
