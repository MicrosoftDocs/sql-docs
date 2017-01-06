---
title: "SPACE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 57ed4564-1bbd-4f95-8189-b694c8d06ad4
caps.latest.revision: 5
author: BarbKess
---
# SPACE (SQL Server PDW)
Returns a string of repeated spaces.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SPACE (integer_expression )  
```  
  
## Arguments  
*integer_expression*  
Is a positive integer that indicates the number of spaces. If *integer_expression* is negative, a null string is returned.  
  
## Return Types  
**varchar**  
  
## Remarks  
To include spaces in Unicode data, or to return more than 8000 character spaces, use REPLICATE instead of SPACE.  
  
## Examples  
The following example trims the last names and concatenates a comma, two spaces, and the first names of people listed in the `DimCustomer` table in `AdventureWorksPDW2012`.  
  
```  
USE AdventureWorksPDW2012;  
GO  
SELECT RTRIM(LastName) + ',' + SPACE(2) +  LTRIM(FirstName)  
FROM dbo.DimCustomer  
ORDER BY LastName, FirstName;  
GO  
```  
  
## See Also  
[REPLICATE &#40;SQL Server PDW&#41;](../sqlpdw/replicate-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
