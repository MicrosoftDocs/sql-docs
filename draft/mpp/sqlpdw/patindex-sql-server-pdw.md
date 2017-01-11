---
title: "PATINDEX (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6ae17c34-b565-4307-9bd4-b371da0f271a
caps.latest.revision: 22
author: BarbKess
---
# PATINDEX (SQL Server PDW)
Returns the starting position of the first occurrence of a pattern in a specified expression, or zeros if the pattern is not found, on all valid text and character data types in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
PATINDEX (%pattern% ,expression )  
```  
  
## Arguments  
*pattern*  
A literal string or search pattern. Wildcard characters can be used; however, the % character must be included both before and after *pattern,* except when searching for starting or ending characters. *pattern* is an expression of the character string data type category.  
  
For more information on pattern matching, see "Pattern Matching in Search Conditions" in [LIKE &#40;SQL Server PDW&#41;](../sqlpdw/like-sql-server-pdw.md).  
  
*expression*  
An expression, typically a column that is searched for the specified pattern. *expression* is of the character string data type category.  
  
## Return Types  
**int**  
  
## Remarks  
If either *pattern* or *expression* is NULL, PATINDEX returns NULL.  
  
## Examples  
  
### A. Using a pattern with PATINDEX  
The following example finds the position at which the pattern `wheel` starts in the `EnglishDescription` column in the `dbo.DimProduct` table for every row that contains the characters `wheel`. If you do not restrict the rows to be searched by using a WHERE clause, the query returns all rows in the table and reports nonzero values for those rows in which the pattern was found and zero for all rows in which the pattern was not found.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT ProductKey,   
    PATINDEX('%wheel%',EnglishDescription)AS StartingPosition,  
EnglishDescription  
FROM dbo.DimProduct  
WHERE EnglishDescription LIKE '%wheel%'  
ORDER BY ProductKey;  
```  
  
Here is a partial result set.  
  
<pre>ProductKey  StartingPosition  EnglishDescription  
----------  ----------------  ----------------------------  
410         22                Replacement mountain wheel for entry-level rider.  
411         22                Replacement mountain wheel for the casual to serious rider.  
412         39                High-performance mountain replacement wheel.  
413         24                Replacement road front wheel for entry-level cyclist.  
415         8                 Strong wheel with double-walled rim.</pre>  
  
### B. Using wildcard characters with PATINDEX  
The following example uses wildcard characters to find the position at which the pattern `whe_l` starts in the `EnglishDescription` column in the `dbo.DimProduct` table, where the underscore is a wildcard representing any character. If you do not restrict the rows to be searched, the query returns all rows in the table and reports nonzero values for those rows in which the pattern was found.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT ProductKey,   
    PATINDEX('%whe_l%',EnglishDescription)AS StartingPosition,  
EnglishDescription  
FROM dbo.DimProduct  
WHERE EnglishDescription LIKE '%wheel%'  
ORDER BY ProductKey;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[CHARINDEX &#40;SQL Server PDW&#41;](../sqlpdw/charindex-sql-server-pdw.md)  
  
