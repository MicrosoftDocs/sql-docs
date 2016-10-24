---
title: "WHILE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 25d6edbe-4a58-4d44-8a40-bc1088b46266
caps.latest.revision: 16
author: BarbKess
---
# WHILE (SQL Server PDW)
Sets a condition for the repeated execution of a SQL statement or statement block. The statements are run repeatedly as long as the specified condition is true. The execution of statements in the WHILE loop can be controlled from inside the loop with the BREAK keyword.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
WHILE Boolean_expression   
     { sql_statement | statement_block | BREAK }  
```  
  
## Arguments  
*Boolean_expression*  
Is an [expression](https://msdn.microsoft.com/library/ms190286.aspx) that returns **TRUE** or **FALSE**. If the Boolean expression contains a SELECT statement, the SELECT statement must be enclosed in parentheses.  
  
{*sql_statement* | *statement_block*}  
Is any SQL statement or statement grouping as defined with a statement block. To define a statement block, use the control-of-flow keywords BEGIN and END.  
  
BREAK  
Causes an exit from the innermost WHILE loop. Any statements that appear after the END keyword, marking the end of the loop, are run.  
  
## General Remarks  
If two or more WHILE loops are nested, the inner BREAK exits to the next outermost loop. All the statements after the end of the inner loop run first, and then the next outermost loop restarts.  
  
## Examples  
  
### Simple While Loop  
In the following example, if the average list price of a product is less than `$300`, the `WHILE` loop doubles the prices and then selects the maximum price. If the maximum price is less than or equal to `$500`, the `WHILE` loop restarts and doubles the prices again. This loop continues doubling the prices until the maximum price is greater than `$500`, and then exits the `WHILE` loop.  
  
```  
USE AdventureWorksPDW2012;  
WHILE ( SELECT AVG(ListPrice) FROM dbo.DimProduct) < $300  
BEGIN  
    UPDATE dbo.DimProduct  
        SET ListPrice = ListPrice * 2;  
    SELECT MAX ( ListPrice) FROM dbo.DimProduct  
    IF ( SELECT MAX (ListPrice) FROM dbo.DimProduct) > $500  
        BREAK;  
END  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
