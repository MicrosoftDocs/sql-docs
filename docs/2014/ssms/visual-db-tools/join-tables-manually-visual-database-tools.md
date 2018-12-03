---
title: "Join Tables Manually (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "manual joins [SQL Server]"
  - "joins [SQL Server], manual"
  - "joins [SQL Server], creating"
ms.assetid: 9c785356-646b-4c87-82d4-25efd6051d9d
author: stevestein
ms.author: sstein
manager: craigg
---
# Join Tables Manually (Visual Database Tools)
  When you add two (or more) tables to a query, the [Query and View Designer](visual-database-tools.md) attempts to join them based on common data or on information stored in the database about how tables are related. For details, see [Join Tables Automatically &#40;Visual Database Tools&#41;](join-tables-automatically-visual-database-tools.md). However, if the Query and View Designer has not joined the tables automatically, or if you want to create additional join conditions between tables, you can join tables manually.  
  
 You can create joins based on comparisons between any two columns, not just columns that contain the same information. For example, if your database contains two tables, `titles` and `roysched`, you can compare values in the `ytd_sales` column of the `titles` table against the `lorange` and `hirange` columns in the `roysched` table. Creating this join would allow you to find titles for which the year-to-date sales falls between the low and high ranges for the royalty payments.  
  
> [!TIP]  
>  Joins work fastest if the columns in the join condition have been indexed. In some cases, joining on unindexed columns can result in a slow query.  
  
### To manually join tables or table-structured objects  
  
1.  Add to the [Diagram pane](diagram-pane-visual-database-tools.md) the objects you want to join.  
  
2.  Drag the name of the join column in the first table or table-structured object and drop it onto the related column in the second table or table-structured object. You cannot base a join on `text`, `ntext`, or i`mage` columns.  
  
    > [!NOTE]  
    >  The join columns must be of the same (or compatible) data types. For example, if the join column in the first table is a date, you must relate it to a date column in the second table. On the other hand, if the first join column is an integer, the related join column must also be of an integer data type, but it can be a different size. The Query and View Designer will not check the data types of the columns you use to create a join, but when you execute the query, the database will display an error if the data types are not compatible.  
  
3.  If necessary, change the join operator; by default, the operator is an equal sign (=). For details, see [Modify Join Operators &#40;Visual Database Tools&#41;](modify-join-operators-visual-database-tools.md).  
  
 The Query and View Designer adds an INNER JOIN clause to the SQL statement in the [SQL pane](sql-pane-visual-database-tools.md). You can change the type to an outer join. For details see [Create Outer Joins &#40;Visual Database Tools&#41;](create-outer-joins-visual-database-tools.md).  
  
## See Also  
 [Query with Joins &#40;Visual Database Tools&#41;](query-with-joins-visual-database-tools.md)  
  
  
