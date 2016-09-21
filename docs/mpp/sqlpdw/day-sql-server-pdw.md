---
title: "DAY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 46b9ce38-14df-419c-beee-40ad51c1ec73
caps.latest.revision: 13
author: BarbKess
---
# DAY (SQL Server PDW)
Returns an integer representing the day (day of the month) of the specified *date* in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DAY (date )  
```  
  
## Arguments  
*date*  
An expression that can be resolved to a **time**, **date**, **smalldatetime**, **datetime**, **datetime2**, or **datetimeoffset** value. The *date* argument can be an expression, column expression, or string literal.  
  
## Return Type  
**int**  
  
## Return Value  
DAY returns the same value as [DATEPART](../sqlpdw/datepart-sql-server-pdw.md) (**day**, *date*).  
  
If *date* contains only a time part, the return value is 1, the base day.  
  
## Examples  
The following example returns `30`. This is the number of the day.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP 1 DAY('2010-07-30T01:01:01.1234')   
FROM dbo.DimCustomer;  
```  
  
The following example returns `1900, 1, 1`. The argument for *date* is the number `0`. SQL Server interprets `0` as January 1, 1900.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP 1 YEAR(0), MONTH(0), DAY(0) FROM dbo.DimCustomer;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[MONTH &#40;SQL Server PDW&#41;](../sqlpdw/month-sql-server-pdw.md)  
[YEAR &#40;SQL Server PDW&#41;](../sqlpdw/year-sql-server-pdw.md)  
  
