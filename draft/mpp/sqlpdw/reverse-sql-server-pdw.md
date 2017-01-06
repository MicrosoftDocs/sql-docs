---
title: "REVERSE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f9697761-4b20-4a3b-bca0-c3391d4a9de9
caps.latest.revision: 5
author: BarbKess
---
# REVERSE (SQL Server PDW)
Returns the reverse order of a string value.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
REVERSE (string_expression )  
```  
  
## Arguments  
*string_expression*  
*string_expression* is an expression of a string or binary data type. *string_expression* can be a constant, variable, or column of either character or binary data.  
  
## Return Types  
**varchar** or **nvarchar**  
  
## Remarks  
*string_expression* must be of a data type that is implicitly convertible to **varchar**. Otherwise, use [CAST](../sqlpdw/cast-and-convert-sql-server-pdw.md) to explicitly convert *string_expression*.  
  
## Supplementary Characters (Surrogate Pairs)  
When using SC collations, the REVERSE function will not reverse the order of two halves of a surrogate pair.  
  
## Examples  
The following example returns names of all databases, and the names with the characters reversed.  
  
```  
SELECT name, REVERSE(name) FROM sys.databases;  
GO  
```  
  
The following example reverses the characters in a variable.  
  
```  
DECLARE @myvar varchar(10);  
SET @myvar = 'sdrawkcaB';  
SELECT REVERSE(@myvar) AS Reversed ;  
GO  
```  
  
The following example makes an implicit conversion from an **int** data type into **varchar** data type and then reverses the result.  
  
```  
SELECT REVERSE(1234) AS Reversed ;  
GO  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
