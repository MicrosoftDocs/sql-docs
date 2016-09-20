---
title: "= (Equals) (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b12283a4-bac2-4415-a2d4-af79902c5cff
caps.latest.revision: 15
author: BarbKess
---
# = (Equals) (SQL Server PDW)
Compares the equality of two expressions (a comparison operator).  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
expression =expression  
```  
  
## Arguments  
*expression*  
Any valid expression. If the expressions are not of the same data type, the data type for one expression must be implicitly convertible to the data type of the other. The conversion is based on the rules of data type precedence. For more information, see Data Type Precedence in [Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md).  
  
## Result Types  
**Boolean**  
  
## Remarks  
When you compare two NULL expressions, the result is NULL.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/operators-sql-server-pdw.md)  
  
