---
title: "DATEPART (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 18bc4cbf-8bdb-4262-a12b-d4cbea5abdc5
caps.latest.revision: 17
author: BarbKess
---
# DATEPART (SQL Server PDW)
Returns part of a date type value, such as the hour, year, or day in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DATEPART (date_part,date_expression )  
```  
  
## Arguments  
*date_part*  
The part of the date or time to be retrieved. For a list of possible values, see the *datepart* parameter for [DATEADD](../sqlpdw/dateadd-sql-server-pdw.md).  
  
*date_expression*  
A datetime expression of any supported format or type. For more information about supported date or time data type, see [Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md).  
  
## Return Types  
**int**  
  
## Remarks  
When the DATEPART function is used in a WHERE clause, it forces table scanning instead of range partition query processing. This can cause suboptimal query performance.  
  
## Examples  
The following example returns the day part of the date `12/20/1974`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP(1) DATEPART (day,'12/20/1974') FROM dbo.DimCustomer;  
```  
  
Here is the result set.  
  
<pre>--------  
20</pre>  
  
The following example returns the year part of the date `12/20/1974`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT TOP(1) DATEPART (year,'12/20/1974') FROM dbo.DimCustomer;  
```  
  
Here is the result set.  
  
<pre>--------  
1974</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
  
