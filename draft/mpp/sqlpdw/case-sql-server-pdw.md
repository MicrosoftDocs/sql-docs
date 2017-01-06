---
title: "CASE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f1efbb94-9b5d-4885-8467-688941df2df0
caps.latest.revision: 19
author: BarbKess
---
# CASE (SQL Server PDW)
Evaluates a list of conditions and returns one of multiple possible result expressions in SQL Server PDW.  
  
CASE can be used in any statement or clause that allows a valid expression. For example, you can use CASE in statements such as SELECT, UPDATE, DELETE and SET, and in clauses such as select_list, IN, WHERE, ORDER BY, and HAVING.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CASE  
     WHEN when_expression THEN result_expression [ ...n ]   
     [ ELSE else_result_expression ]   
END  
```  
  
## Arguments  
WHEN *when_expression*  
Any valid Boolean expression.  
  
THEN *result_expression*  
The expression returned when *when_expression* evaluates to TRUE. *result expression* is any valid expression.  
  
ELSE *else_result_expression*  
The expression returned if no comparison operation evaluates to TRUE. If this argument is omitted and no comparison operation evaluates to TRUE, CASE returns NULL. *else_result_expression* is any valid expression. The data types of *else_result_expression* and any *result_expression* must be the same or must be an implicit conversion.  
  
## Return Types  
Returns the highest precedence type from the set of types in *result_expressions* and the optional *else_result_expression*. For more information, see Data Type Precedence in [Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md).  
  
## Limitations and Restrictions  
SQL Server PDW does not allow for nesting in CASE expressions.  
  
## Examples  
  
### A. Using a SELECT statement with a CASE expression  
Within a SELECT statement, the CASE expression allows for values to be replaced in the result set based on comparison values. The following example uses the CASE expression to change the display of product line categories to make them more understandable. When a value does not exist, the text “Not for sale’ is displayed.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT   ProductAlternateKey, Category =  
      CASE ProductLine  
         WHEN 'R' THEN 'Road'  
         WHEN 'M' THEN 'Mountain'  
         WHEN 'T' THEN 'Touring'  
         WHEN 'S' THEN 'Other sale items'  
         ELSE 'Not for sale'  
      END,  
   EnglishProductName  
FROM dbo.DimProduct  
ORDER BY ProductKey;  
```  
  
### B. Using CASE in an UPDATE statement  
The following example uses the CASE expression in an UPDATE statement to determine the value that is set for the column `VacationHours` for employees with `SalariedFlag` set to 0. When subtracting 10 hours from `VacationHours` results in a negative value, `VacationHours` is increased by 40 hours; otherwise, `VacationHours` is increased by 20 hours.  
  
```  
USE AdventureWorksPDW2012;   
  
UPDATE dbo.DimEmployee  
SET VacationHours =   
    ( CASE  
         WHEN ((VacationHours - 10.00) < 0) THEN VacationHours + 40  
         ELSE (VacationHours + 20.00)   
       END  
    )   
WHERE SalariedFlag = 0;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
[UPDATE &#40;SQL Server PDW&#41;](../sqlpdw/update-sql-server-pdw.md)  
[WHERE &#40;SQL Server PDW&#41;](../sqlpdw/where-sql-server-pdw.md)  
  
