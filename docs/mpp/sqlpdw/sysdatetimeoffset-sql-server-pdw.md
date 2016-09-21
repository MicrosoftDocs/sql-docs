---
title: "SYSDATETIMEOFFSET (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 0b2f44ee-2965-4ca5-b555-945113a5e034
caps.latest.revision: 5
author: BarbKess
---
# SYSDATETIMEOFFSET (SQL Server PDW)
Returns a **datetimeoffset(7)** value that contains the date and time of the APS. The time zone offset is included.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SYSDATETIMEOFFSET ()  
```  
  
## Return Type  
**datetimeoffset(7)**  
  
## Remarks  
Transact\-SQL statements can refer to SYSDATETIMEOFFSET anywhere they can refer to a **datetimeoffset** expression.  
  
SYSDATETIMEOFFSET is a nondeterministic function. Views and expressions that reference this function in a column cannot be indexed.  
  
> [!NOTE]  
> APS obtains the date and time values by using the GetSystemTimeAsFileTime() Windows API. The accuracy depends on the computer hardware and version of Windows on which the instance of SQL Server is running. The precision of this API is fixed at 100 nanoseconds. The accuracy can be determined by using the GetSystemTimeAdjustment() Windows API.  
  
## Examples  
The following examples use the six SQL Server system functions that return current date and time to return the date, time, or both. The values are returned in series; therefore, their fractional seconds might be different.  
  
### A. Showing the formats that are returned by the date and time functions  
The following example shows the different formats that are returned by the date and time functions.  
  
```  
SELECT SYSDATETIME() AS SYSDATETIME  
    ,SYSDATETIMEOFFSET() AS SYSDATETIMEOFFSET  
    ,SYSUTCDATETIME() AS SYSUTCDATETIME  
    ,CURRENT_TIMESTAMP AS CURRENT_TIMESTAMP  
    ,GETDATE() AS GETDATE  
    ,GETUTCDATE() AS GETUTCDATE;  
```  
  
Here is the result set.  
  
<pre>SYSDATETIME()      2007-04-30 13:10:02.0474381  
SYSDATETIMEOFFSET()2007-04-30 13:10:02.0474381 -07:00  
SYSUTCDATETIME()   2007-04-30 20:10:02.0474381  
CURRENT_TIMESTAMP  2007-04-30 13:10:02.047  
GETDATE()          2007-04-30 13:10:02.047  
GETUTCDATE()       2007-04-30 20:10:02.047</pre>  
  
### B. Converting date and time to date  
The following example shows you how to convert date and time values to `date`.  
  
```  
SELECT CONVERT (date, SYSDATETIME())  
    ,CONVERT (date, SYSDATETIMEOFFSET())  
    ,CONVERT (date, SYSUTCDATETIME())  
    ,CONVERT (date, CURRENT_TIMESTAMP)  
    ,CONVERT (date, GETDATE())  
    ,CONVERT (date, GETUTCDATE());  
```  
  
Here is the result set.  
  
<pre>2007-04-30  
2007-04-30  
2007-04-30  
2007-04-30  
2007-04-30  
2007-04-30</pre>  
  
### C. Converting date and time to times  
The following example shows you how to convert date and time values to `time`.  
  
```  
SELECT CONVERT (time, SYSDATETIME()) AS SYSDATETIME()  
    ,CONVERT (time, SYSDATETIMEOFFSET()) AS SYSDATETIMEOFFSET()  
    ,CONVERT (time, SYSUTCDATETIME()) AS SYSUTCDATETIME()  
    ,CONVERT (time, CURRENT_TIMESTAMP) AS CURRENT_TIMESTAMP  
    ,CONVERT (time, GETDATE()) AS GETDATE()  
    ,CONVERT (time, GETUTCDATE()) AS GETUTCDATE();  
```  
  
Here is the result set.  
  
<pre>SYSDATETIME()      13:18:45.3490361  
SYSDATETIMEOFFSET()13:18:45.3490361  
SYSUTCDATETIME()   20:18:45.3490361  
CURRENT_TIMESTAMP  13:18:45.3470000  
GETDATE()          13:18:45.3470000  
GETUTCDATE()       20:18:45.3470000</pre>  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
