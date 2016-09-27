---
title: "DATALENGTH (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f86c0ee7-1dcd-4e8f-b442-59320435a140
caps.latest.revision: 5
author: BarbKess
---
# DATALENGTH (SQL Server PDW)
Returns the number of bytes used to represent any expression.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DATALENGTH (expression )  
```  
  
## Arguments  
*expression*  
Is an expression of any data type.  
  
## Return Types  
**bigint** if *expression* is of the **varchar(max)**, **nvarchar(max)** or **varbinary(max)** data types; otherwise **int**.  
  
## Remarks  
DATALENGTH is especially useful with **varchar**, **varbinary**, **text**, **image**, **nvarchar**, and **ntext** data types because these data types can store variable-length data.  
  
The DATALENGTH of NULL is NULL.  
  
## Examples  
The following example finds the length of the `Name` column in the `Product` table.  
  
```  
USE AdventureWorksPDW2012;  
GO  
SELECT length = DATALENGTH(EnglishProductName), EnglishProductName  
FROM dbo.DimProduct  
ORDER BY EnglishProductName;  
GO  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[LEN &#40;SQL Server PDW&#41;](../sqlpdw/len-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
