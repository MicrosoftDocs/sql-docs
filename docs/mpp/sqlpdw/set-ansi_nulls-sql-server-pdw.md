---
title: "SET ANSI_NULLS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ed33b693-cf44-41e7-a8b9-4e11e73912ca
caps.latest.revision: 7
author: BarbKess
---
# SET ANSI_NULLS (SQL Server PDW)
Specifies ISO compliant behavior of the Equals (=) and Not Equal To (<>) comparison operators when they are used with NULL values in SQL Server PDW.  
  
In this release, ANSI_NULLS is always ON.  
  
For more information, see the [SET ANSI_NULLS (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188048(v=sql11)) documentation on MSDN.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET ANSI_NULLS ON;  
```  
  
## Permissions  
Requires membership in the public role.  
  
## General Remarks  
When ANSI_NULLS is ON:  
  
1.  A SELECT statement that uses WHERE *column_name* = **NULL** returns zero rows even if there are NULL values in *column_name*. A SELECT statement that uses WHERE *column_name* <> **NULL** returns zero rows even if there are nonnull values in *column_name*.  
  
2.  All comparisons against a NULL value evaluate to UNKNOWN.  
  
3.  The setting affects a comparison only if one of the operands of the comparison is either a variable that is NULL or a literal NULL. If both sides of the comparison are columns or compound expressions, the setting does not affect the comparison.  
  
For a script to work as intended, regardless of the ANSI_NULLS setting, use IS NULL and IS NOT NULL in comparisons that might contain NULL values.  
  
The SQL Server Native Client ODBC driver and SQL Server Native Client OLE DB Provider for SQL Server automatically set ANSI_NULLS to ON when connecting. This setting can be configured in ODBC data sources, in ODBC connection attributes, or in OLE DB connection properties that are set in the application before connecting to an instance of SQL Server PDW.  
  
The setting of SET ANSI_NULLS is set at execute or run time and not at parse time.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
