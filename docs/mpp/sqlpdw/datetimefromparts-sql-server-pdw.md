---
title: "DATETIMEFROMPARTS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 50a5317d-9322-440f-8328-074896b0841e
caps.latest.revision: 3
author: BarbKess
---
# DATETIMEFROMPARTS (SQL Server PDW)
Returns a **datetime** value for the specified date and time.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DATETIMEFROMPARTS (year, month, day, hour, minute, seconds, milliseconds)  
```  
  
## Arguments  
*year*  
Integer expression specifying a year.  
  
*month*  
Integer expression specifying a month.  
  
*day*  
Integer expression specifying a day.  
  
*hour*  
Integer expression specifying hours.  
  
*minute*  
Integer expression specifying minutes.  
  
*seconds*  
Integer expression specifying seconds.  
  
*milliseconds*  
Integer expression specifying milliseconds.  
  
## Return Types  
**datetime**  
  
## Remarks  
**DATETIMEFROMPARTS** returns a fully initialized **datetime** value. If the arguments are not valid, then an error is raised. If required arguments are null, then a null is returned.  
  
## Examples  
  
```  
SELECT DATETIMEFROMPARTS ( 2010, 12, 31, 23, 59, 59, 0 ) AS Result;  
```  
  
Here is the result set.  
  
```  
Result  
---------------------------  
2010-12-31 23:59:59.000  
  
(1 row(s) affected)  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
