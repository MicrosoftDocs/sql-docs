---
description: "Add or Edit Join"
title: "Add or Edit Join | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.newpubwizard.addeditjoin.f1"
  - "sql13.sql13.swb.agdashboard.arp4joinstate.issues.f1"
ms.assetid: 3b546560-720f-48b8-9d63-cf159290e9d4
author: "MashaMSFT"
ms.author: "mathoma"
---
# Add or Edit Join
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **Add Join** and **Edit Join** dialog boxes allow you to add and edit join filters for merge publications.  
  
> [!NOTE]  
>  Editing a filter in an existing publication requires a new snapshot for the publication. If a publication has subscriptions, the subscriptions must be reinitialized. For more information about property changes, see [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
 A join filter allows a table to be filtered based on how a related table in the publication is filtered. Typically a parent table is filtered using a parameterized row filter; then one or more join filters are defined in much the same way that you define a join between tables. The join filters extend the row filter so that the data in the related tables is replicated only if it matches the join filter clause.  
  
 Join filters typically follow the primary key/foreign key relationships defined for the tables to which they are applied, but they are not limited strictly to primary key/foreign key relationships. The join filter can be based on any logic that compares related data in two article tables.  
  
> [!IMPORTANT]  
>  Join Filters can involve an unlimited number of tables, but filters with a large number of tables can impact performance during merge processing. If you are generating join filters of five or more tables, consider other solutions: do not filter tables that are small, not subject to change, or are primarily lookup tables. Use join filters only between tables that must be partitioned among Subscribers.  
  
## Options  
 This dialog box involves a three-step process to create a join filter between two tables. Creating more than one join filter requires more than one pass through the dialog box.  
  
1.  **Verify filtered table and select the joined table**  
  
    -   If you are adding a new join, verify that the table in the **Filtered table** text box is correct (if it is not correct, click **Cancel**, select the correct table on the **Filter Table Rows** page, and click **Add Join** to return to this dialog box). Then select a table from the **Joined table** drop-down list box.  
  
    -   If you are editing an existing join, the table names will be specified already and cannot be changed. To change the tables involved in the join, you must delete the existing join filter on the **Filter Table Rows** page and create a new join between different tables.  
  
2.  **Create the join statement**  
  
    -   If you are adding a new join, select either **Use the builder to create the statement** or **Write the join statement manually**. If you begin writing the join manually, you cannot use the builder.  
  
         If you select to use the builder, use the columns in the grid (**Conjunction**, **Filtered table column**, **Operator**, and **Joined table column**) to build a join statement. Each column in the grid contains a drop-down list box, allowing you to select two columns and an operator (**=**, **<>**, **<=**, **\<**, **>=**, **>**, **like**). The results are displayed in the **Preview** text area. If the join involves more than one pair of columns, select a conjunction (**AND** or **OR**) from the **Conjunction** column, and then enter two more columns and another operator.  
  
         If you select to write the statement manually, write the join statement in the **Join statement** text area. Use the **Filtered table columns** list box and **Joined table columns** list box to drag and drop columns to the **Join statement** text area.  
  
    -   If you are editing an existing join, you must make edits manually.  
  
3.  **Specify join options**  

    -   If the column on which you join in the filtered table is unique, select **Unique key**. The merge process has special performance optimizations available if the column is unique.  
  
        > [!CAUTION]  
        >  Selecting this option indicates that the relationship between the child and parent tables in a join filter is one to one or one to many. Only select this option if you have a constraint on the joining column in the parent table that guarantees uniqueness. If the option is set incorrectly, non-convergence of data can occur.  
  
    -   [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions only. By default, merge replication processes changes on a row-by-row basis during synchronization. To have related changes processed as a unit, select **Logical record**. This option is available only if the article and publication requirements for using logical records are met. For more information, see the section "Considerations for Using Logical Records" in [Group Changes to Related Rows with Logical Records](../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
 After you have added or edited a filter, click **OK** to save changes and close the dialog box. The filter you specified is parsed and run against the table in the SELECT clause. If the filter statement contains syntax errors or other problems, you will be notified and will be able to edit the filter statement.  
  
## See Also  
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [View and Modify Publication Properties](../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [Filter Published Data](../../relational-databases/replication/publish/filter-published-data.md)   
 [Join Filters](../../relational-databases/replication/merge/join-filters.md)   
 [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md)   
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)  
  
  
