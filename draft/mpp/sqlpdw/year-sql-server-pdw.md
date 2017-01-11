---
title: "YEAR (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b238d6e0-9ea9-4f82-90f0-d52ab8d10a7c
caps.latest.revision: 13
author: BarbKess
---
# YEAR (SQL Server PDW)
Returns an integer that represents the year of the specified date in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
YEAR (date )  
```  
  
## Arguments  
*date*  
An expression that can be resolved to a **time**, **date**, **smalldatetime**, **datetime**, **datetime2**, or **datetimeoffset** value. The *date* argument can be an expression, column expression, or string literal.  
  
## Return Types  
**int**  
  
## Return Value  
YEAR returns the same value as [DATEPART](../sqlpdw/datepart-sql-server-pdw.md) (**year**, *date*).  
  
If *date* only contains a time part, the return value is 1900, the base year.  
  
## Examples  
The following statement returns `2010`. This is the number of the year.  
  
```  
SELECT YEAR('2010-07-20T01:01:01.1234');  
```  
  
The following statement returns `1900, 1, 1`. The argument for *date* is the number `0`. SQL Server interprets `0` as January 1, 1900.  
  
```  
SELECT TOP 1 YEAR(0), MONTH(0), DAY(0);  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[MONTH &#40;SQL Server PDW&#41;](../sqlpdw/month-sql-server-pdw.md)  
[DAY &#40;SQL Server PDW&#41;](../sqlpdw/day-sql-server-pdw.md)  
  
