---
title: "Generate Filters | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.newpubwizard.generatefilters.f1"
ms.assetid: be28515c-5d6d-467b-b933-d7c8d97a45b4
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Generate Filters
  The **Generate Filters** dialog box allows you to define a row filter on one table in a merge publication; replication then automatically extends the filter to other tables that are related through foreign key relationships. For example, if you define a filter on a customer table so that it only contains data on French customers, replication extends that filter so that related orders and order details tables contain only information related to French customers.  
  
## Options  
 This dialog box involves a three-step process to create a row filter on a table. The filter is then extended to the tables related to the filtered table through primary key and foreign key relationships. For example, given the three tables **Customer**, **SalesOrderHeader**, and **SalesOrderDetail**, with a relationship between **Customer** and **SalesOrderHeader**, and a relationship between **SalesOrderHeader** and **SalesOrderDetail**, apply a row filter to **Customer**, and replication extends that filter to **SalesOrderHeader** and **SalesOrderDetail**.  
  
1.  **Select the table to filter.**  
  
     Select a table from the drop-down list box. Tables appear in the list box only if they were selected on the **Articles** page.  
  
2.  **Complete the filter statement to identify which table rows Subscribers will receive.**  
  
     Define a new filter statement. The **Columns** list box lists all the columns that you are publishing from the table you selected in **Select the table to filter**. The **Filter statement** text area includes the default text, which is in the form of:  
  
     `SELECT <published_columns> FROM [tableowner].[tablename] WHERE`  
  
     This text cannot be changed; type the filter clause after the WHERE keyword using standard [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax.  
  
    > [!IMPORTANT]  
    >  For performance reasons, we recommended that you not apply functions to column names in parameterized row filter clauses, such as `LEFT([MyColumn]) = SUSER_SNAME()`. If you use HOST_NAME in a filter clause and override the HOST_NAME value, it might be necessary to convert data types using CONVERT. For more information about best practices for this case, see the section "Overriding the HOST_NAME() Value" in the topic [Parameterized Row Filters](merge/parameterized-filters-parameterized-row-filters.md).  
  
3.  **Specify how many subscriptions will receive data from this table.**  
  
     [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions only. Merge replication allows you to specify the type of partitions that are best suited to your data and application. If you select **A row from this table will go to only one subscription**, merge replication sets the nonoverlapping partitions option. Nonoverlapping partitions work in conjunction with precomputed partitions to improve performance, with nonoverlapping partitions minimizing the upload cost associated with precomputed partitions. The performance benefit of nonoverlapping partitions is more noticeable when the parameterized filters and join filters used are more complex. If you select this option, you must ensure that the data is partitioned in such a way that a row cannot be replicated to more than one Subscriber. For more information, see the section "Setting 'partition options'" in the topic [Parameterized Row Filters](merge/parameterized-filters-parameterized-row-filters.md).  
  
 After you have added a filter, click **OK** to exit and close the dialog box. The filter you specified is parsed and run against the table in the SELECT clause. If the filter statement contains syntax errors or other problems, you will be notified and will be able to edit the filter statement.  
  
 After the statement is parsed, replication creates the necessary join filters. If you have not yet configured the Distributor for the Publisher against which this wizard is running, you are prompted to configure it.  
  
## See Also  
 [Create a Publication](publish/create-a-publication.md)   
 [View and Modify Publication Properties](publish/view-and-modify-publication-properties.md)   
 [Filter Published Data](publish/filter-published-data.md)   
 [Join Filters](merge/join-filters.md)   
 [Parameterized Row Filters](merge/parameterized-filters-parameterized-row-filters.md)   
 [Publish Data and Database Objects](publish/publish-data-and-database-objects.md)  
  
  
