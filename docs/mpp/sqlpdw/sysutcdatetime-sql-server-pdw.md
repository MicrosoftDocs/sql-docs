---
title: "SYSUTCDATETIME (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5b479331-19fa-4975-ae0b-ebcf1548da20
caps.latest.revision: 5
author: BarbKess
---
# SYSUTCDATETIME (SQL Server PDW)
Returns a **datetime2** value that contains the date and time of the computer on which the instance of SQL Server is running. The date and time is returned as UTC time (Coordinated Universal Time). The fractional second precision specification has a range from 1 to 7 digits. The default precision is 7 digits.  
  
> [!NOTE]  
> SYSDATETIME and SYSUTCDATE have more fractional seconds precision than GETDATE and GETUTCDATE. SYSDATETIMEOFFSET includes the system time zone offset. SYSDATETIME, SYSUTCDATE, and SYSDATETIMEOFFSET can be assigned to a variable of any one of the date and time types.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SYSUTCDATETIME ( )  
```  
  
## Return Type  
**datetime2**  
  
## Remarks  
Transact\-SQL statements can refer to SYSUTCDATETIME anywhere they can refer to a **datetime2** expression.  
  
SYSUTCDATETIME is a nondeterministic function. Views and expressions that reference this function in a column cannot be indexed.  
  
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
  
### C. Converting date and time values to time  
The following example shows you how to convert date and time values to `time`.  
  
<pre>DECLARE @DATETIME DATETIME = GetDate();  
DECLARE @TIME TIME  
SELECT @TIME = CONVERT(time, @DATETIME)  
SELECT @TIME AS 'Time', @DATETIME AS 'Date Time'</pre>  
  
Here is the result set.  
  
<pre>Time             Date Time  
13:49:33.6330000 2009-04-22 13:49:33.633</pre>  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
