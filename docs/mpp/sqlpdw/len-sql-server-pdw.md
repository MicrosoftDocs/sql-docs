---
title: "LEN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 67b9178b-39c5-481c-8375-7e4cbd407f62
caps.latest.revision: 21
author: BarbKess
---
# LEN (SQL Server PDW)
Returns the number of characters in the specified string expression, including leading but not trailing spaces in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
LEN (string_expression )  
```  
  
## Arguments  
*string_expression*  
The string expression to be evaluated. *string_expression* can be a constant or column of character data.  
  
## Return Types  
**int**  
  
## Examples  
The following example returns the number of characters in the column `FirstName` and the first and last names of employees located in `Australia`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT DISTINCT LEN(FirstName) AS FNameLength, FirstName, LastName   
FROM dbo.DimEmployee AS e  
INNER JOIN dbo.DimGeography AS g   
    ON e.SalesTerritoryKey = g.SalesTerritoryKey   
WHERE EnglishCountryRegionName = 'Australia';  
```  
  
Here is the result set.  
  
<pre>FNameLength  FirstName  LastName  
-----------  ---------  ---------------  
4            Lynn       Tsoflias</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md)  
[LEFT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/left-sql-server-pdw.md)  
[RIGHT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/right-sql-server-pdw.md)  
  
