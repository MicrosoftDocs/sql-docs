---
title: "GETDATE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e9770580-2997-4459-9ff2-0cf0bf6e4999
caps.latest.revision: 4
author: BarbKess
---
# GETDATE (SQL Server PDW)
Returns the current database system timestamp as a **datetime** value without the database time zone offset. This value is derived from the operating system of the SQL Server PDW appliance.  
  
> [!NOTE]  
> SYSDATETIME has more fractional seconds precision than GETDATE. SYSDATETIME can be assigned to a variable of any of the date and time types.  
  
For an overview of all standard Transact\-SQL date and time data types and functions, see [Date and Time Data Types and Functions](http://msdn.microsoft.com/en-us/library/ms186724.aspx).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
GETDATE ()  
```  
  
## Return Type  
**datetime**  
  
## Remarks  
Transact\-SQL statements can refer to GETDATE anywhere they can refer to a **datetime** expression.  
  
GETDATE is a nondeterministic function. Views and expressions that reference this function in a column cannot be indexed.  
  
## Examples  
The following examples use the three SQL Server system functions that return current date and time to return the date, time, or both. The values are returned in series; therefore, their fractional seconds might be different.  
  
### A. Getting the current system date and time  
  
```  
SELECT SYSDATETIME()  
    ,CURRENT_TIMESTAMP  
    ,GETDATE();  
```  
  
### B. Getting the current system date  
  
```  
SELECT CONVERT (date, SYSDATETIME())  
    ,CONVERT (date, CURRENT_TIMESTAMP)  
    ,CONVERT (date, GETDATE());  
```  
  
### C. Getting the current system time  
  
```  
SELECT CONVERT (time, SYSDATETIME())  
    ,CONVERT (time, CURRENT_TIMESTAMP)  
    ,CONVERT (time, GETDATE());  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[DATEPART &#40;SQL Server PDW&#41;](../sqlpdw/datepart-sql-server-pdw.md)  
[DATEDIFF &#40;SQL Server PDW&#41;](../sqlpdw/datediff-sql-server-pdw.md)  
  
