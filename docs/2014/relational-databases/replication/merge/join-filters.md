---
title: "Join Filters | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "filters [SQL Server replication], join"
  - "publications [SQL Server replication], join filters"
  - "merge replication join filters [SQL Server replication]"
  - "join filters"
ms.assetid: dd78fd8f-56e3-4582-9abd-6bc25c91e075
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Join Filters
  A join filter allows a table to be filtered based on how a related table in the publication is filtered. Typically a parent table is filtered using a parameterized filter; then one or more join filters are defined in much the same way that you define a join between tables. The join filters extend the parameterized filter so that the data in the related tables is only replicated if it matches the join filter clause.  
  
 Join filters typically follow the primary key/foreign key relationships defined for the tables to which they are applied, but they are not limited strictly to primary key/foreign key relationships. The join filter can be based on any logic that compares related data in two tables.  
  
 Consider the following tables in the [!INCLUDE[ssSampleDBCoShort](../../../includes/sssampledbcoshort-md.md)] sample database, which are related through primary key to foreign key relationships:  
  
-   **HumanResources.Employee**  
  
-   **Sales.SalesOrderHeader**  
  
-   **Sales.SalesOrderDetail**  
  
 These tables could be used in an application to support a mobile sales force, but they must be filtered so that each sales person in the **HumanResources.Employee** table receives only the data relevant to their customers' orders.  
  
 The first step is to define a parameterized filter on the parent table, which in this example is the **HumanResources.Employee** table. This table includes the column **LoginID**, which contains the login for each employee in the form *domain\login*. To filter this table so that each employee receives only the data related to them, specify a parameterized filter clause of:  
  
```  
LoginID = SUSER_SNAME()  
```  
  
 This filter ensures that each employee's subscription only contains data from the **HumanResources.Employee** table that is relevant to that employee (which in this case is a single row). For more information, see [Parameterized Row Filters](parameterized-filters-parameterized-row-filters.md).  
  
 The next step is to extend this filter to each of the related tables, using syntax similar to that used to specify a join between two tables. The first join filter clause is:  
  
```  
Employee.EmployeeID = SalesOrderHeader.SalesPersonID  
```  
  
 This ensures the subscription contains only the order data relevant to each sales person. The second join filter clause is:  
  
```  
SalesOrderHeader.SalesOrderID = SalesOrderDetail.SalesOrderID  
```  
  
 This ensures the subscription contains only the detail data related to the order data for each sales person. This example shows a single table being joined at each point; it is also possible to join more than one table at each point.  
  
 Join filters can be added one at a time through the New Publication Wizard and the **Publication Properties** dialog box, or they can be added programmatically. They can also be generated automatically through the New Publication Wizard: you specify a row filter for a table and join filters are applied to all related tables. For more information, see [Define and Modify a Join Filter Between Merge Articles](../publish/define-and-modify-a-join-filter-between-merge-articles.md), [Automatically Generate a Set of Join Filters Between Merge Articles &#40;SQL Server Management Studio&#41;](../publish/automatically-generate-join-filters-between-merge-articles.md), and [Define an Article](../publish/define-an-article.md).  
  
## Optimizing Join Filter Performance  
 Join filter performance can be optimized by following these guidelines:  
  
-   Limit the number of tables in the join filter hierarchy.  
  
     Join Filters can involve an unlimited number of tables, but filters with a large number of tables can significantly impact performance during merge processing. If you are generating join filters of five or more tables, consider other solutions: do not filter tables that are small, not subject to change, or are primarily lookup tables. Use join filters only between tables that must be partitioned among subscriptions.  
  
-   Set the **join unique key** option to **True** where appropriate.  
  
     The merge process has special performance optimizations available if the joined column in the parent is unique. If the join condition is based on a unique column, set the **join unique key** option for the join filter. For information about setting this option, see the how-to topics listed in the previous section.  
  
-   Ensure that the columns referenced in join filters are indexed.  
  
     If the columns referenced in the filter are indexed, replication can process the filters more efficiently.  
  
-   Do not create row filters that mimic join filters.  
  
     It is possible to create row filters that mimic join filters by using a subquery in a WHERE clause, such as:  
  
    ```  
    WHERE Customer.SalesPersonID IN (SELECT EmployeeID FROM Employee WHERE LoginID = SUSER_SNAME())   
    ```  
  
     It is strongly recommended that all such logic be expressed in a join filter rather than a subquery. If your application requires a row filter to use a subsquery, ensure that the subquery only references lookup data that does not change.  
  
## See Also  
 [Filter Published Data for Merge Replication](filter-published-data-for-merge-replication.md)   
 [Parameterized Row Filters](parameterized-filters-parameterized-row-filters.md)  
  
  
