---
title: "BEGIN...END (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 76fe25ce-f43c-4b09-8537-c4b7212790e0
caps.latest.revision: 9
author: BarbKess
---
# BEGIN...END (SQL Server PDW)
Encloses a series of SQL statements in SQL Server PDW. BEGIN and END are provided for compatibility with SQL Server. BEGIN and END are control-of-flow language keywords.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
BEGIN  
     {   
    sql_statement | statement_block   
     }   
END  
```  
  
## Arguments  
{ *sql_statement*| *statement_block* }  
Is any valid SQL statement or statement grouping as defined by using a statement block.  
  
## General Remarks  
BEGIN...END blocks can be nested.  
  
Although all SQL statements are valid within a BEGIN...END block, certain SQL statements should not be grouped together within the same batch, or statement block. For more information, see [Batches, Control-of-Flow, and Variables &#40;SQL Server PDW&#41;](../sqlpdw/batches-control-of-flow-and-variables-sql-server-pdw.md) and the individual statements used.  
  
## Examples  
In the following example, `BEGIN` and `END` define a series of SQL statements that run together. If the `BEGIN...END` block are not included, the following example will be in a continuous loop.  
  
```  
USE AdventureWorksDW2008;  
  
DECLARE @Iteration Integer = 0  
WHILE @Iteration <10  
BEGIN  
    SELECT FirstName, MiddleName   
    FROM dbo.DimCustomer WHERE LastName = 'Adams';  
SET @Iteration += 1  
END;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
