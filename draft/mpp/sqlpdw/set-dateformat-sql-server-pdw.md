---
title: "SET DATEFORMAT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 74929732-f968-4cdc-8915-658b8f29dbb4
caps.latest.revision: 9
author: BarbKess
---
# SET DATEFORMAT (SQL Server PDW)
Sets the order of the month, day, and year date parts for interpreting **date**, **smalldatetime**, **datetime**, **datetime2** and **datetimeoffset** character strings. Applies to SQL Server PDW.  
  
In this release, the US default ‘mdy’ is the only valid date format value.  
  
For more information, see the [SET DATEFIRST (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms181598(v=sql11).aspx) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET DATEFORMAT { format | @format_var }  
```  
  
## Arguments  
*format* | **@***format_var*  
Is the order of the date parts. The U.S. English default, **mdy**, is the only valid parameter.  
  
## Permissions  
Requires membership in the **public** role.  
  
## Example  
  
```  
-- Set date format to month/day/year.  
SET DATEFORMAT mdy;  
DECLARE @datevar datetime2 = '12/31/2012 09:01:01.1234567';  
SELECT @datevar;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
