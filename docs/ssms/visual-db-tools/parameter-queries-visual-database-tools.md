---
title: "Parameter Queries (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "parameter queries [SQL Server]"
ms.assetid: 4897c41a-324a-47b8-a30b-cbc9e9e19a8b
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Parameter Queries (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
In some cases you want to create a query that you can use many times, but with a different value each time. For example, you might frequently run a query to find all the `title_ids` written by one author. You could run the same query for each request, except that the author's ID or name would be different each time.  
  
To create a query that can have different values at different times, you use parameters in the query. A parameter is a placeholder for a value that is supplied when the query runs. An SQL statement with a parameter might look like the following, where "?" represents the parameter for the author's ID:  
  
```  
SELECT title_id  
FROM titleauthor  
WHERE (au_id = ?)  
```  
  
## Where You Can Use Parameters  
You can use parameters as placeholders for literal values - for either text or numeric values. Most commonly, parameters are used as placeholders in search conditions for individual rows or for groups (that is, in the WHERE or HAVING clauses of an SQL statement).  
  
You can use parameters as placeholders in expressions. For example, you might want to calculate discounted prices by supplying a different discount value each time you run a query. To do so, you could specify the following expression:  
  
```  
(price * ?)  
```  
  
## Specifying Unnamed and Named Parameters  
You can specify two types of parameters: unnamed and named. An unnamed parameter is a question mark (?) that you put anywhere in the query that you want to prompt for or substitute a literal value. For example, if you use an unnamed parameter to search for an author's id in the `titleauthor` table, the resulting statement in the [SQL Pane](../../ssms/visual-db-tools/sql-pane-visual-database-tools.md) might look like this:  
  
```  
SELECT title_id  
FROM titleauthor  
WHERE (au_id = ?)  
```  
  
When you run the query in the [Query and View Designer](../../ssms/visual-db-tools/query-and-view-designer-tools-visual-database-tools.md), the [Query Parameters Dialog Box](../../ssms/visual-db-tools/query-parameters-dialog-box-visual-database-tools.md) appears with "?" as the name of the parameter.  
  
Alternatively, you can assign a name to a parameter. Named parameters are particularly useful if you have multiple parameters in a query. For example, if you use named parameters to search for an author's first and last names in the `authors` table, the resulting statement in the SQL pane might look like this:  
  
```  
SELECT au_id  
FROM authors  
WHERE au_fname = %first name% AND  
      au_lname = %last name%  
```  
  
> [!TIP]  
> You must define prefix and suffix characters before creating a named parameter query.  
  
When you run the query in the Query and View Designer, the [Query Parameters Dialog Box](../../ssms/visual-db-tools/query-parameters-dialog-box-visual-database-tools.md) appears with a list of named parameters.  
  
## See Also  
[Query with Parameters &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-with-parameters-visual-database-tools.md)  
[Supported Query Types &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/supported-query-types-visual-database-tools.md)  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
  
