---
title: "EOMONTH (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 378741e9-0ea8-439d-a999-74cd1ddb4c8a
caps.latest.revision: 5
author: BarbKess
---
# EOMONTH (SQL Server PDW)
Returns the last day of the month that contains the specified date, with an optional offset.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
EOMONTH (start_date [, month_to_add ] )  
```  
  
## Arguments  
*start_date*  
Date expression specifying the date for which to return the last day of the month.  
  
*month_to_add*  
Optional integer expression specifying the number of months to add to *start_date*.  
  
If this argument is specified, then **EOMONTH** adds the specified number of months to *start_date*, and then returns the last day of the month for the resulting date. If this addition overflows the valid range of dates, then an error is raised.  
  
## Return Type  
**date**  
  
## Remarks  
  
## Examples  
  
### A. EOMONTH with explicit datetime type  
  
```  
DECLARE @date DATETIME = '12/1/2011';  
SELECT EOMONTH ( @date ) AS Result;  
GO  
```  
  
Here is the result set.  
  
```  
Result  
------------  
2011-12-31  
  
(1 row(s) affected)  
```  
  
### B. EOMONTH with string parameter and implicit conversion  
  
```  
DECLARE @date VARCHAR(255) = '12/1/2011';  
SELECT EOMONTH ( @date ) AS Result;  
GO  
```  
  
Here is the result set.  
  
```  
Result  
------------  
2011-12-31  
  
(1 row(s) affected)  
```  
  
### C. EOMONTH with and without the month_to_add parameter  
  
```Transact-SQL  
DECLARE @date DATETIME = GETDATE();  
SELECT EOMONTH ( @date ) AS 'This Month';  
SELECT EOMONTH ( @date, 1 ) AS 'Next Month';  
SELECT EOMONTH ( @date, -1 ) AS 'Last Month';  
GO  
```  
  
Here is the result set.  
  
```  
This Month  
-----------------------  
2011-12-31  
  
(1 row(s) affected)  
  
Next Month  
-----------------------  
2012-01-31  
  
(1 row(s) affected)  
  
Last Month  
-----------------------  
2011-11-30  
  
(1 row(s) affected)  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
