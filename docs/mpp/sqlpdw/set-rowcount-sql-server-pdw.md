---
title: "SET ROWCOUNT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: fddb3997-7a2e-42f9-b48a-bc244ceca3a9
caps.latest.revision: 12
author: BarbKess
---
# SET ROWCOUNT (SQL Server PDW)
Causes SQL Server PDW to stop processing the query after the specified number of rows are returned.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET ROWCOUNT { number | @number_var }   
[;]  
```  
  
## Arguments  
*number* | @*number_var*  
An integer number of rows to be returned by each query in the session.  
  
## General Remarks  
SET ROWCOUNT 0 turns the option off so that the query will return all rows.  
  
When ROWCOUNT is set and TOP is in the SELECT statement, the query returns the smaller number of rows. For example, the following query will return at most 3 rows.  
  
```  
SET ROWCOUNT 10;  
SELECT TOP 3 * FROM sys.databases;  
```  
  
SET ROWCOUNT is a run time setting, and stays in effect for the session or until it is changed.  
  
## Limitations and Restrictions  
SET ROWCOUNT does not affect these statements:  
  
-   [DELETE &#40;SQL Server PDW&#41;](../sqlpdw/delete-sql-server-pdw.md)  
  
-   [INSERT &#40;SQL Server PDW&#41;](../sqlpdw/insert-sql-server-pdw.md)  
  
-   [UPDATE &#40;SQL Server PDW&#41;](../sqlpdw/update-sql-server-pdw.md)  
  
-   [CREATE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-table-as-select-sql-server-pdw.md)  
  
-   [CREATE EXTERNAL TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-as-select-sql-server-pdw.md)  
  
-   [CREATE REMOTE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-remote-table-as-select-sql-server-pdw.md)  
  
For a similar behavior, use the [TOP &#40;SQL Server PDW&#41;](../sqlpdw/top-sql-server-pdw.md) statement.  
  
## Permissions  
Requires membership in the public role.  
  
## Examples  
SET ROWCOUNT stops processing after the specified number of rows. In the following example, note that more than 20 rows meet the criteria of `AccountType = 'Assets'`. However, after applying SET ROWCOUNT, you can see that not all rows were returned.  
  
```  
USE AdventureWorksPDW2012;  
  
SET ROWCOUNT 5;  
SELECT * FROM [dbo].[DimAccount]  
WHERE AccountType = 'Assets';  
```  
  
To return all rows, set ROWCOUNT to 0.  
  
```  
USE AdventureWorksPDW2012;  
  
SET ROWCOUNT 0;  
SELECT * FROM [dbo].[DimAccount]  
WHERE AccountType = 'Assets';  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
