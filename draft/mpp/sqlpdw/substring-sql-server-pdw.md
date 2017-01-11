---
title: "SUBSTRING (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: add625ac-62ac-4175-82ab-85d2c1763764
caps.latest.revision: 23
author: BarbKess
---
# SUBSTRING (SQL Server PDW)
Returns part of a character or binary expression in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SUBSTRING ( value_expression , start_expression ,   
        length_expression )  
```  
  
## Arguments  
*value_expression*  
A character expression.  
  
*start_expression*  
An **integer** or **bigint** expression that specifies where the returned characters start. If *start_expression* is less than 1, the returned expression will begin at the first character that is specified in *value_expression*. In this case, the number of characters that are returned is the larger of either the sum of *start_expression* and *length_expression*, or 0. If *start_expression* is greater than the number of characters in the value expression, a zero-length expression is returned.  
  
*length_expression*  
A positive **integer** or **bigint** expression that specifies how many characters of the *value_expression* will be returned. If *length_expression* is negative, an error is generated and the statement is terminated. If the sum of *start_expression* and *length_expression* is greater than the number of characters in *value_expression*, the whole value expression beginning at *start_expression* is returned.  
  
## Return Types  
Returns character data if *expression* is one of the supported character data types. The returned string is the same type as the specified expression with the exceptions shown in the table.  
  
|Specified expression|Return type|  
|------------------------|---------------|  
|**char**/**varchar**|**varchar**|  
|**nchar/nvarchar**|**nvarchar**|  
  
## Remarks  
The values for *start_expression* and *length_expression* must be specified in number of characters for **char**, **varchar**, **nchar**, or **nvarchar** data types.  
  
## Examples  
  
### A. Using SUBSTRING with a character string  
The following example shows how to return only a part of a character string. From the `dbo.DimEmployee` table, this query returns the last name in one column with only the first initial in the second column.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT LastName, SUBSTRING(FirstName, 1, 1) AS Initial  
FROM dbo.DimEmployee  
WHERE LastName LIKE 'Bar%'  
ORDER BY LastName;  
```  
  
Here is the result set.  
  
<pre>LastName             Initial  
-------------------- -------  
Barbariol            A  
Barber               D  
Barreto de Mattos    P</pre>  
  
The following example shows how to return the second, third, and fourth characters of the string constant `abcdef`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP 1 SUBSTRING('abcdef', 2, 3) AS x FROM dbo.DimCustomer;  
```  
  
Here is the result set.  
  
<pre>x  
-----  
bcd</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
  
