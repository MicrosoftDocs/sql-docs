---
title: "SET NUMERIC_ROUNDABORT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: bf076336-a329-4b3b-9751-50a42c321b9f
caps.latest.revision: 7
author: BarbKess
---
# SET NUMERIC_ROUNDABORT (SQL Server PDW)
Specifies the level of error reporting in SQL Server PDW when rounding in an expression causes a loss of precision.  
  
In this release, NUMERIC_ROUNDABORT is always ON.  
  
For more information, see the [SET NUMERIC_ROUNDABORT (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188791(v=sql11)) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET NUMERIC_ROUNDABORT ON;  
```  
  
## Permissions  
Requires membership in the **public** role.  
  
## General Remarks  
When NUMERIC_ROUNDABORT is ON, an error is generated after a loss of precision occurs in an expression.  
  
Loss of precision occurs when an attempt is made to store a value with a fixed precision in a column or variable with less precision.  
  
If NUMERIC_ROUNDABORT is ON, the ARITHABORT setting determines the severity of the generated error. Since NUMERIC_ROUNDABORT and ARITHABORT are always ON, SQL Server PDW will generate an error for precision loss and will not return a result set.  
  
The setting of SET NUMERIC_ROUNDABORT is set at execute or run time and not at parse time.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
