---
title: "DELETE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e4ca7c57-a0b7-4727-855f-8fa0ee3644b8
caps.latest.revision: 40
author: BarbKess
---
# DELETE (SQL Server PDW)
Removes rows from a table in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DELETE FROM [database_name . [ schema ] . | schema. ] table_name    
    [ WHERE <search_condition> ]   
    [ OPTION ( <query_options> [ ,...n ]  ) ]  
[; ]  
```  
  
## Arguments  
FROM [*database_name* . [*schema* ] . | *schema*. ] *table_name*  
The name of the table that will have rows removed. Unlike SQL Server, only one table is allowed. The <from_clause> described in the [FROM &#40;SQL Server PDW&#41;](../sqlpdw/from-sql-server-pdw.md) topic does not apply to the DELETE statement.  
  
*WHERE <search_condition>*  
Specifies the rows to be deleted. When the WHERE clause is omitted, all rows will be removed from the table. You cannot use a join to delete rows from multiple tables. You can use a join in a subquery of the search condition. For more details about the search conditions, see [Search Condition &#40;SQL Server PDW&#41;](../sqlpdw/search-condition-sql-server-pdw.md).  
  
OPTION ( <query_options> [ ,...*n* ]  )  
Specifies one or more query options, including a label and query join hints. OPTION query hints apply to the entire DELETE statement. For more details, see [OPTION &#40;SQL Server PDW&#41;](../sqlpdw/option-sql-server-pdw.md).  
  
## Error Handling  
When a DELETE statement encounters an arithmetic error (overflow, divide by zero, or a domain error) during expression evaluation, the rest of the batch is canceled and an error message is returned.  
  
## General Remarks  
To delete all the rows in a table, use the DELETE statement without a WHERE clause.  
  
A join hint is allowed, but ignored, if it is specified in a DELETE statement that does not have a join. For example, the following statement runs, but the join hint is ignored.  
  
```  
DELETE FROM AdventureWorksPDW2012.dbo.DatabaseLog  
WHERE 1=2  
OPTION (HASH JOIN)  
;  
```  
  
## Permissions  
Requires **DELETE** permission on the target table, or membership in the **db_datawriter** fixed database role. **SELECT** permissions are also required if the statement contains a **WHERE** clause. **DELETE** permission is denied to members of the **db_denydatawriter** fixed database role.  
  
## Limitations and Restrictions  
[SET ROWCOUNT &#40;SQL Server PDW&#41;](../sqlpdw/set-rowcount-sql-server-pdw.md) has no effect on this statement. To achieve a similar behavior, use [TOP &#40;SQL Server PDW&#41;](../sqlpdw/top-sql-server-pdw.md).  
  
## Locking  
Takes an exclusive lock at some level on the table being changed.  
  
## Examples  
  
### A. Delete all rows from a table  
The following example deletes all rows from the `Table1` table because a WHERE clause is not used to limit the number of rows deleted.  
  
```  
DELETE FROM Table1;  
```  
  
### B. DELETE a set of rows from a table  
The following example deletes all rows from the  `Table1` table that have a value greater than 1000.00 in the  `StandardCost` column.  
  
```  
DELETE FROM Table1  
WHERE StandardCost > 1000.00;  
```  
  
### C. Using LABEL with a DELETE statement  
The following example uses a label with the DELETE statement.  
  
```  
DELETE FROM Table1  
OPTION ( LABEL = N'label1' );  
```  
  
### D. Using a label and a query hint with the DELETE statement  
This query shows the basic syntax for using a query join hint with the DELETE statement. For more information on join hints and how to use the OPTION clause, see [OPTION &#40;SQL Server PDW&#41;](../sqlpdw/option-sql-server-pdw.md).  
  
```  
USE AdventureWorksPDW2012;  
DELETE FROM dbo.FactInternetSales  
WHERE ProductKey IN (   
    SELECT T1.ProductKey FROM dbo.DimProduct T1   
    JOIN dbo.DimProductSubcategory T2  
    ON T1.ProductSubcategoryKey = T2.ProductSubcategoryKey  
    WHERE T2.EnglishProductSubcategoryName = 'Road Bikes' )  
OPTION ( LABEL = N'CustomJoin', HASH JOIN ) ;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
