---
title: "|= (Bitwise OR EQUALS) (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 26373f4d-1331-4fdb-8326-3451c5481c6e
caps.latest.revision: 6
author: BarbKess
---
# |= (Bitwise OR EQUALS) (SQL Server PDW)
Performs a bitwise logical OR operation between two specified integer values as translated to binary expressions within Transact\-SQL statements, and sets a value to the result of the operation.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
expression |=expression  
```  
  
## Arguments  
*expression*  
Is any valid [expression](../../mpp/sqlpdw/expressions-sql-server-pdw.md) of any one of the data types in the numeric category except the **bit** data type.  
  
## Result Types  
Returns the data type of the argument with the higher precedence. For more information, see [Data Type Precedence (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms190309.aspx).  
  
## Remarks  
For more information, see [&#124; &#40;Bitwise OR&#41; &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/bitwise-or-sql-server-pdw.md).  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/operators-sql-server-pdw.md)  
[Bitwise Operators &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/bitwise-operators-sql-server-pdw.md)  
[^= &#40;Bitwise Exclusive OR EQUALS&#41; &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/bitwise-exclusive-or-equals-sql-server-pdw.md)  
  
