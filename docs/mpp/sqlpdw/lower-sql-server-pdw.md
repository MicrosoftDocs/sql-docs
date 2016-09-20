---
title: "LOWER (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 45764981-2f71-474a-bfe7-3e9f5970e9eb
caps.latest.revision: 19
author: BarbKess
---
# LOWER (SQL Server PDW)
Returns a character expression after converting uppercase character data to lowercase in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
LOWER (character_expression )  
```  
  
## Arguments  
*character_expression*  
An expression of character or binary data. *character_expression* can be a constant or column. *character_expression* must be of a data type that is implicitly convertible to **varchar** or **nvarchar**. Otherwise, use [CAST](../../mpp/sqlpdw/cast-and-convert-sql-server-pdw.md) to explicitly convert *character_expression*.  
  
## Return Types  
**varchar** or **nvarchar**  
  
## Examples  
The following example uses the `LOWER` function, the `UPPER` function, and nests the `UPPER` function inside the `LOWER` function in selecting product names that have prices between $11 and $20.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT LOWER(SUBSTRING(EnglishProductName, 1, 20)) AS Lower,   
       UPPER(SUBSTRING(EnglishProductName, 1, 20)) AS Upper,   
       LOWER(UPPER(SUBSTRING(EnglishProductName, 1, 20))) As LowerUpper  
FROM dbo.DimProduct  
WHERE ListPrice between 11.00 and 20.00;  
```  
  
Here is the result set.  
  
<pre>Lower                 Upper                  LowerUpper  
--------------------  ---------------------  --------------------  
minipump              MINIPUMP               minipump  
taillights – battery  TAILLIGHTS – BATTERY   taillights - battery</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[CAST and CONVERT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/cast-and-convert-sql-server-pdw.md)  
[UPPER &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/upper-sql-server-pdw.md)  
  
