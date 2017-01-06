---
title: "IF...ELSE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6b199a8c-8a72-4256-b4ef-295eb7c5cd64
caps.latest.revision: 13
author: BarbKess
---
# IF...ELSE (SQL Server PDW)
Imposes conditions on the execution of a SQL Server PDW statement or group of statements.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
IF Boolean_expression   
     { sql_statement | statement_block }   
[ ELSE   
     { sql_statement | statement_block } ]  
```  
  
## Arguments  
*Boolean_expression*  
Is an expression that returns TRUE or FALSE. If the Boolean expression contains a SELECT statement, the SELECT statement must be enclosed in parentheses.  
  
{ *sql_statement*| *statement_block* }  
Is any SQL statement or statement grouping as defined by using a statement block. Unless a statement block is used, the IF or ELSE condition can affect the performance of only one SQL statement.  
  
To define a statement block, use the control-of-flow keywords BEGIN and END.  
  
## Return Values  
Returns TRUE if the condition that follows an IF keyword evaluates to TRUE.  
  
Returns FALSE if the condition that follows an IF keyword evaluates to FALSE.  
  
## General Remarks  
The IF…ELSE construct can be used in batches and in ad hoc queries.  
  
IF tests can be nested after another IF or following an ELSE. The limit to the number of nested levels depends on available memory.  
  
## Examples  
The following example uses `IF…ELSE` to determine which of two responses to show the user, based on the weight of an item in the `DimProduct` table.  
  
```  
USE AdventureWorksPDW2012;  
  
DECLARE @maxWeight float, @productKey integer  
SET @maxWeight = 100.00  
SET @productKey = 424  
IF @maxWeight <= (SELECT Weight from DimProduct WHERE ProductKey=@productKey)   
    (SELECT @productKey, EnglishDescription, Weight, 'This product is too heavy to ship and is only available for pickup.' FROM DimProduct WHERE ProductKey=@productKey)  
ELSE  
    (SELECT @productKey, EnglishDescription, Weight, 'This product is available for shipping or pickup.' FROM DimProduct WHERE ProductKey=@productKey)  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
