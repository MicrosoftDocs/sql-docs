---
title: "Query Properties (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdtsql.chm:69636"
  - "vdt.ppg.querydesigner.query"
ms.assetid: 07495669-6ed5-4004-904e-aae1230be5e4
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Query Properties (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
These properties appear in the Properties window when you have a query open in Query and View Designer. Unless otherwise noted, you can edit these properties in the Properties window.  
  
> [!NOTE]  
> The properties in this topic are ordered by category, rather than alphabetically.  
  
## Options  
**Identity Category**  
Expand to show the **Name** property.  
  
**Name**  
Shows the name of the current query. Cannot be changed in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
**Database Name**  
Shows the name of the data source for the selected table.  
  
**Server Name**  
Shows the name of the server for the data source.  
  
**Query Designer Category**  
Expands to show the remaining properties.  
  
**Destination table**  
Specify the name of the table into which you are inserting data. This list appears if you are creating an INSERT query or MAKE TABLE query. For an INSERT query, select a table name from the list.  
  
For a MAKE TABLE query, type the name of the new table. To create a destination table in another data source, specify a fully qualified table name, including the name of the target data source, the owner (if required), and the name of the table.  
  
> [!NOTE]  
> Query Designer does not check whether the name is already in use or whether you have permission to create the table.  
  
**Distinct values**  
Specify that the query will filter out duplicates in the result set. This option is useful when you are using only some of the columns from the table or tables and those columns might contain duplicate values, or when the process of joining two or more tables produces duplicate rows in the result set. Choosing this option is equivalent to inserting the word DISTINCT into the statement in the SQL pane.  
  
**GROUP BY Extension**  
Specify that additional options for queries based on aggregate queries are available. (Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].)  
  
**Output All Columns**  
Specify that all columns from all tables in the current query will be in the result set. Choosing this option is equivalent to specifying an asterisk (*) in place of individual column names after the SELECT keyword in the SQL statement.  
  
**Query Parameter List**  
Shows query parameters. To edit the parameters click the property and then click the ellipses **(...)** to the right of the property. (Applies only to generic OLE DB.)  
  
**SQL Comment**  
Shows a description of the SQL statements. To see the entire description or to edit it, click the description and then click the ellipses **(...)** to the right of the property. Your comments can include information such as who uses the query and when they use it. (Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0 databases or later.)  
  
**Top Specification Category**  
Expand to show properties for the **Top**, **Percent**, **Expression**, and **With Ties** properties.  
  
**(Top)**  
Specify hat the query will include a TOP clause, which returns only the first *n* rows or first *n* percentage of rows in the result set. The default is that the query returns the first 10 rows in the result set.  
  
Use this box to change the number of rows to return or to specify a different percentage. (Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or later.)  
  
**Expression**  
Specify the number or percentage of rows that the query will return. If you set **Percent** to Yes, this number is the percentage of rows the query will return; if you set **Percent** to No, it represents the number of rows to return. (Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 7.0 or later.)  
  
**Percent**  
Specify that the query will return only the first *n* percent of rows in the result set. (Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 7.0 or later.)  
  
**With Ties**  
Specify that the view will include a WITH TIES clause. WITH TIES is useful if a view includes an ORDER BY clause and a TOP clause based on percentage. If this option is set, and if the percentage cutoff falls in the middle of a set of rows with identical values in the ORDER BY clause, the view is extended to include all such rows. (Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 7.0 or later.)  
  
## See Also  
[Query with Parameters &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-with-parameters-visual-database-tools.md)  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
  
