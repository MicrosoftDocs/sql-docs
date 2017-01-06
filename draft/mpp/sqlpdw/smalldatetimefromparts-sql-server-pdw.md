---
title: "SMALLDATETIMEFROMPARTS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7754d134-6be3-4e09-8f27-18d6163a46e2
caps.latest.revision: 4
author: BarbKess
---
# SMALLDATETIMEFROMPARTS (SQL Server PDW)
Returns a **smalldatetime** value for the specified date and time.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SMALLDATETIMEFROMPARTS (year, month, day, hour, minute)  
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
  
## Return Types  
**smalldatetime**  
  
## Remarks  
This functions acts like a constructor for a fully initialized **smalldatetime** value. If the arguments are not valid, then an error is thrown. If required arguments are null, then null is returned.  
  
## Examples  
  
```  
SELECT SMALLDATETIMEFROMPARTS ( 2010, 12, 31, 23, 59 ) AS Result  
```  
  
Here is the result set.  
  
```  
Result  
---------------------------  
2011-01-01 00:00:00  
  
(1 row(s) affected)  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
