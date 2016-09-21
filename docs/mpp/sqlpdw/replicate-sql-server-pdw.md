---
title: "REPLICATE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 9408f76a-339f-4f83-9444-e6c42599fd06
caps.latest.revision: 25
author: BarbKess
---
# REPLICATE (SQL Server PDW)
Repeats a string value a specified number of times in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
REPLICATE (string_expression, integer_expression )  
```  
  
## Arguments  
*string_expression*  
An expression of a character string. *string_expression* must be one of the supported character data types.  
  
*integer_expression*  
An expression of any **integer** type. This expression can be a constant or an **int** column. If *integer_expression* is negative, NULL is returned.  
  
## Return Types  
Returns the same type as *string_expression*.  
  
## Examples  
The following example replicates a `0` character four times in front of an `ItemCode` value.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT EnglishProductName AS Name,  
   ProductAlternateKey AS ItemCode,  
   REPLICATE('0', 4) + ProductAlternateKey AS FullItemCode  
FROM dbo.DimProduct  
ORDER BY Name;  
```  
  
Here are the first rows in the result set.  
  
<pre>Name                     ItemCode       FullItemCode  
------------------------ -------------- ---------------  
Adjustable Race          AR-5381        0000AR-5381  
All-Purpose Bike Stand   ST-1401        0000ST-1401  
AWC Logo Cap             CA-1098        0000CA-1098  
AWC Logo Cap             CA-1098        0000CA-1098  
AWC Logo Cap             CA-1098        0000CA-1098  
BB Ball Bearing          BE-2349        0000BE-2349</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CAST and CONVERT &#40;SQL Server PDW&#41;](../sqlpdw/cast-and-convert-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[RTRIM &#40;SQL Server PDW&#41;](../sqlpdw/rtrim-sql-server-pdw.md)  
  
