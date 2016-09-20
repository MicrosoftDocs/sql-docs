---
title: "MONTH (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: bd4cdb0a-165c-4fd1-a8c0-5f61e043ef66
caps.latest.revision: 13
author: BarbKess
---
# MONTH (SQL Server PDW)
Returns an integer that represents the month of the specified date in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
MONTH (date )  
```  
  
## Arguments  
*date*  
An expression that can be resolved to a **time**, **date**, **smalldatetime**, **datetime**, **datetime2**, or **datetimeoffset** value. The *date* argument can be an expression, column expression, or string literal.  
  
## Return Type  
**int**  
  
## Return Value  
MONTH returns the same value as [DATEPART](../../mpp/sqlpdw/datepart-sql-server-pdw.md) (*month*, *date*).  
  
If *date* contains only a time part, the return value is 1, the base month.  
  
## Examples  
The following example returns `4`. This is the number of the month.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP 1 MONTH('2007-04-30T01:01:01.1234')   
FROM dbo.DimCustomer;  
```  
  
The following example returns `1900, 1, 1`. The argument for *date* is the number `0`. SQL Server interprets `0` as January 1, 1900.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP 1 YEAR(0), MONTH(0), DAY(0) FROM dbo.DimCustomer;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[DAY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/day-sql-server-pdw.md)  
[YEAR &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/year-sql-server-pdw.md)  
  
