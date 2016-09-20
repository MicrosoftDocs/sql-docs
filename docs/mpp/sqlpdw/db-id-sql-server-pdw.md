---
title: "DB_ID (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d2dc0bd8-4bac-465f-ab7a-df505715334e
caps.latest.revision: 9
author: BarbKess
---
# DB_ID (SQL Server PDW)
Returns the database identification (ID) number for a SQL Server PDW database.  
  
For more information, see the [DB_ID (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms186274%20(v=SQL.11).aspx) documentation on MSDN.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DB_ID ( [ 'database_name' ] );  
```  
  
## Arguments  
'*database_name*'  
Is the database name for which the database ID will be returned.  *database_name* is of type **sysname**. When *database_name* is omitted, SQL Server PDW returns the ID of the current database.  
  
## Return Types  
int  
  
## Examples  
  
### A. Return the ID of the current database  
The following example returns the database ID of the current database.  
  
```  
SELECT DB_ID();  
```  
  
### B. Return the ID of a named database.  
The following example returns the database ID of the AdventureWorksDW2012 database.  
  
```  
SELECT DB_ID('AdventureWorksPDW2012');  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
  
