---
title: "SET TEXTSIZE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b0926da8-9743-42af-b2b4-abb9389e250e
caps.latest.revision: 7
author: BarbKess
---
# SET TEXTSIZE (SQL Server PDW)
Specifies the size of **varchar(max)**, **nvarchar(max)**, and **varbinary(max)** data returned by a SELECT statement in SQL Server PDW.  
  
These types are not directly supported by the SQL Server PDWSQL language, but could be displayed as the result of querying the SQL Server system views.  
  
For more information, see the [SET TEXTSIZE (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms186238(v=sql11).aspx) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET TEXTSIZE number ;  
```  
  
## Arguments  
*number*  
Is the length of **varchar(max)**, **nvarchar(max)**, or **varbinary(max)** data, in bytes. *number* is an integer. The maximum setting for SET TEXTSIZE is 2 gigabytes (GB), specified in bytes. A setting of 0 resets the size to the default (4 KB).  
  
## Permissions  
Requires membership in the **public** role.  
  
## General Remarks  
The SQL Server Native Client ODBC driver and SQL Server Native Client OLE DB Provider for SQL Server automatically set TEXTSIZE to 2147483647 when connecting.  
  
TEXTSIZE is set at run time and not at parse time.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
