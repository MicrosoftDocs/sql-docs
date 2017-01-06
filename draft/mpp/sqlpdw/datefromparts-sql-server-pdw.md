---
title: "DATEFROMPARTS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: abe12acd-7345-405a-946f-f7b9b1f2d7fd
caps.latest.revision: 4
author: BarbKess
---
# DATEFROMPARTS (SQL Server PDW)
Returns a **date** value for the specified year, month, and day.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DATEFROMPARTS (year, month, day)  
```  
  
## Arguments  
*year*  
Integer expression specifying a year.  
  
*month*  
Integer expression specifying a month, from 1 to 12.  
  
*day*  
Integer expression specifying a day.  
  
## Return Types  
**date**  
  
## Remarks  
**DATEFROMPARTS** returns a **date** value with the date portion set to the specified year, month and day, and the time portion set to the default. If the arguments are not valid, then an error is raised. If required arguments are null, then null is returned.  
  
## Examples  
The following example demonstrates the **DATEFROMPARTS** function.  
  
```  
SELECT DATEFROMPARTS ( 2010, 12, 31 ) AS Result;  
```  
  
Here is the result set.  
  
```  
Result  
----------------------------------  
2010-12-31  
  
(1 row(s) affected)  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
