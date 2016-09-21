---
title: "SET FMTONLY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f0789ada-f091-4fe3-b320-f1ef73779796
caps.latest.revision: 10
author: BarbKess
---
# SET FMTONLY (SQL Server PDW)
Specifies whether or not a SQL Server PDW query will return only the column header information (e.g., the metadata) without returning any rows of data. Use this setting to view the format of the query results without actually running the query.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET FMTONLY { ON | OFF } [;]  
```  
  
## Arguments  
ON specifies to return only the column header information. This stays in effect until FMTONLY is set to OFF. The default is OFF, which specifies to return the full results of the query.  
  
## Permissions  
Requires membership in the public role.  
  
## Examples  
  
### A. View the column header information for a query without actually running the query.  
The following example shows how to return only the column header (metadata) information for a query. The batch begins with FMTONLY set to OFF and changes FMTONLY to ON before the SELECT statement. This causes the SELECT statement to return only the column headers; no rows of data are returned.  
  
```  
USE AdventureWorksPDW2012;  
BEGIN  
    SET FMTONLY OFF;  
    SET DATEFORMAT mdy;  
    SET FMTONLY ON;  
    SELECT * FROM dbo.DimCustomer;  
    SET FMTONLY OFF;  
END  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SET Statements &#40;SQL Server PDW&#41;](../sqlpdw/set-statements-sql-server-pdw.md)  
  
