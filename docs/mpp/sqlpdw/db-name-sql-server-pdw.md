---
title: "DB_NAME (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: fa081355-c604-44b6-bd0c-158cdbacabd5
caps.latest.revision: 13
author: BarbKess
---
# DB_NAME (SQL Server PDW)
Returns the name of a database in SQL Server PDW.  
  
For more information, see the [DB_NAME (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms189753(v=SQL.11).aspx) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DB_NAME  ( [database_id] )  
```  
  
## Arguments  
database_id  
Is the ID, specified as an integer, for the database. When *database_id* is omitted, SQL Server PDW returns the name of the current database.  
  
## Return Types  
**nvarchar(128)**  
  
## Examples  
  
### A. Return the current database name  
  
```  
SELECT DB_NAME() AS [Current Database];  
```  
  
### B. Return the name of a database by using the database ID  
The following example returns the database name and database_id for each database.  
  
```  
SELECT DB_NAME(database_id) AS [Database], database_id  
FROM sys.databases;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
  
