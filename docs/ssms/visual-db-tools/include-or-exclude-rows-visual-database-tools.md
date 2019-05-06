---
title: "Include or Exclude Rows (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "search criteria [SQL Server], excluding rows"
  - "search criteria [SQL Server], WHERE clause"
  - "included rows"
  - "search conditions [SQL Server], HAVING clause"
  - "search criteria [SQL Server], including rows"
  - "row excluded in search [SQL Server]"
  - "search criteria [SQL Server], HAVING clause"
  - "search conditions [SQL Server], WHERE clause"
  - "row included in search [SQL Server]"
  - "excluding rows"
ms.assetid: ba4e1202-31a2-444d-8365-c68a530ef223
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Include or Exclude Rows (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
To restrict the number of rows a SELECT query should return, you create search conditions or filter criteria. In SQL, search conditions appear in the WHERE clause of the statement, or if you are creating an aggregate query, in the HAVING clause.  
  
> [!NOTE]  
> You can also use search conditions to indicate which rows are affected by an Update, Insert Results, Insert Values, Delete, or Make Table query.  
  
When the query runs, the [!INCLUDE[ssDE](../../includes/ssde_md.md)] examines and applies the search condition to each row in the tables you are searching. If the row meets the condition, it is included in the query. For example, a search condition that would find all the employees in a particular region might be:  
  
```  
region = 'UK'  
```  
  
To establish the criteria for including a row in a result, you can use multiple search conditions. For example, the following search criterion consists of two search conditions. The query includes a row in the result set only if that row satisfies both of the conditions.  
  
```  
region = 'UK' AND product_line = 'Housewares'  
```  
  
You can combine these conditions with AND or OR. The previous example uses AND. In contrast, the following criterion uses OR. The result set will include any row that satisfies either or both of the search conditions:  
  
```  
region = 'UK' OR product_line = 'Housewares'  
```  
  
You can even combine search conditions on a single column. For example, the following criterion combines two conditions on the region column:  
  
```  
region = 'UK' OR region = 'US'  
```  
  
For details about combining search conditions, see the following topics:  
  
-   [Conventions for Combining Search Conditions in the Criteria Pane &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/conventions-combine-search-conditions-in-criteria-pane-visual-db-tools.md)  
  
-   [Specify Multiple Search Conditions for One Column &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/specify-multiple-search-conditions-for-one-column-visual-database-tools.md)  
  
-   [Specify Multiple Search Conditions for Multiple Columns &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/specify-multiple-search-conditions-for-multiple-columns-visual-database-tools.md)  
  
-   [Combine Conditions When AND Has Precedence &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/combine-conditions-when-and-has-precedence-visual-database-tools.md)  
  
-   [Combine Conditions When OR Has Precedence &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/combine-conditions-when-or-has-precedence-visual-database-tools.md)  
  
## Examples  
Here are some examples of queries using various operators and row criteria:  
  
-   **Literal** A single text, numeric, date, or logical value. The following example uses a literal to find all rows for employees in the United Kingdom:  
  
    ```  
    WHERE region = 'UK'  
    ```  
  
-   **Column reference** Compares the values in one column with the values in another. The following example searches a `products` table for all rows in which the value of the production cost is lower than the shipping cost:  
  
    ```  
    WHERE prod_cost < ship_cost  
    ```  
  
-   **Function** A reference to a function that the database back-end can resolve to calculate a value for the search. The function can be a function defined by the database server or a user-defined function that returns a scalar value. The following example searches for orders placed today (the GETDATE( ) function returns the current date):  
  
    ```  
    WHERE order_date = GETDATE()  
    ```  
  
-   **NULL** The following example searches an `authors` table for all authors who have a first name on file:  
  
    ```  
    WHERE au_fname IS NOT NULL  
    ```  
  
-   **Calculation** The result of a calculation that can involve literals, column references, or other expressions. The following example searches a `products` table to find all rows in which the retail sales price is more than twice the production cost:  
  
    ```  
    WHERE sales_price > (prod_cost * 2)  
    ```  
  
## See Also  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
[Specify Search Criteria &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/specify-search-criteria-visual-database-tools.md)  
[Query with Parameters &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-with-parameters-visual-database-tools.md)  
  
