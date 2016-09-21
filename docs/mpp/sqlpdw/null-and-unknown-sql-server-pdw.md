---
title: "NULL and UNKNOWN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 4ef334f3-8082-4c0b-aee7-00c1888ffadc
caps.latest.revision: 14
author: BarbKess
---
# NULL and UNKNOWN (SQL Server PDW)
NULL indicates that the value is unknown. A null value is different from an empty or zero value. No two null values are equal. Comparisons between two null values, or between a null value and any other value, return unknown because the value of each NULL is unknown.  
  
Null values generally indicate data that is unknown, not applicable, or to be added later. For example, a customer's middle initial may not be known at the time the customer places an order.  
  
Note the following about null values:  
  
-   To test for null values in a query, use IS NULL or IS NOT NULL in the WHERE clause.  
  
-   Null values can be inserted into a column by explicitly stating NULL in an INSERT or UPDATE statement or by leaving a column out of an INSERT statement.  
  
-   Null values cannot be used as information that is required to distinguish one row in a table from another row in a table, such as primary keys, or for information used to distribute rows, such as distribution keys.  
  
When null values are present in data, logical and comparison operators can potentially return a third result of UNKNOWN instead of just TRUE or FALSE. This need for three-valued logic is a source of many application errors. These tables outline the effect of introducing null comparisons.  
  
The following table shows the results of applying an AND operator to two Boolean operands where one operand returns NULL.  
  
|Operand 1|Operand 2|Result|  
|-------------|-------------|----------|  
|TRUE|NULL|FALSE|  
|NULL|NULL|FALSE|  
|FALSE|NULL|FALSE|  
  
The following table shows the results of applying an OR operator to two Boolean operands where one operand returns NULL.  
  
|Operand 1|Operand 2|Result|  
|-------------|-------------|----------|  
|TRUE|NULL|TRUE|  
|NULL|NULL|UNKNOWN|  
|FALSE|NULL|UNKNOWN|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[AND &#40;SQL Server PDW&#41;](../sqlpdw/and-sql-server-pdw.md)  
[OR &#40;SQL Server PDW&#41;](../sqlpdw/or-sql-server-pdw.md)  
[NOT &#40;SQL Server PDW&#41;](../sqlpdw/not-sql-server-pdw.md)  
[IS &#91;NOT&#93; NULL &#40;SQL Server PDW&#41;](../sqlpdw/is-not-null-sql-server-pdw.md)  
  
