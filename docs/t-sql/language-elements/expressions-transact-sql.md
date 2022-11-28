---
title: "Expressions (Transact-SQL)"
description: "Expressions (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
helpviewer_keywords:
  - "Boolean expressions"
  - "expressions [SQL Server], about expressions"
  - "combining expressions"
  - "Transact-SQL expressions"
  - "expressions [SQL Server], combining"
  - "simple expressions [SQL Server]"
  - "complex expressions [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# Expressions (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Is a combination of symbols and operators that the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] evaluates to obtain a single data value. Simple expressions can be a single constant, variable, column, or scalar function. Operators can be used to join two or more simple expressions into a complex expression.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Syntax for SQL Server and Azure SQL Database  
  
{ constant | scalar_function | [ table_name. ] column | variable   
    | ( expression ) | ( scalar_subquery )   
    | { unary_operator } expression   
    | expression { binary_operator } expression   
    | ranking_windowed_function | aggregate_windowed_function  
}  
```  
  
```syntaxsql
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  

-- Expression in a SELECT statement  
<expression> ::=   
{  
    constant   
    | scalar_function   
    | column  
    | variable  
    | ( expression  )  
    | { unary_operator } expression   
    | expression { binary_operator } expression   
}  
[ COLLATE Windows_collation_name ]  
  
-- Scalar Expression in a DECLARE, SET, IF...ELSE, or WHILE statement  
<scalar_expression> ::=  
{  
    constant   
    | scalar_function   
    | variable  
    | ( expression  )  
    | (scalar_subquery )  
    | { unary_operator } expression   
    | expression { binary_operator } expression   
}  
[ COLLATE { Windows_collation_name ]  
  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
  
|Term|Definition|  
|----------|----------------|  
|*constant*|Is a symbol that represents a single, specific data value. For more information, see [Constants &#40;Transact-SQL&#41;](../../t-sql/data-types/constants-transact-sql.md).|  
|*scalar_function*|Is a unit of [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax that provides a specific service and returns a single value. *scalar_function* can be built-in scalar functions, such as the SUM, GETDATE, or CAST functions, or scalar user-defined functions.|  
|[ _table_name_**.** ]|Is the name or alias of a table.|  
|*column*|Is the name of a column. Only the name of the column is allowed in an expression.|  
|*variable*|Is the name of a variable, or parameter. For more information, see [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md).|  
|**(** _expression_  **)**|Is any valid expression as defined in this topic. The parentheses are grouping operators that make sure that all the operators in the expression within the parentheses are evaluated before the resulting expression is combined with another.|  
|**(** _scalar_subquery_ **)**|Is a subquery that returns one value. For example:<br /><br /> `SELECT MAX(UnitPrice)`<br /><br /> `FROM Products`|  
|{ *unary_operator* }|Unary operators can be applied only to expressions that evaluate to any one of the data types of the numeric data type category. Is an operator that has only one numeric operand:<br /><br /> + indicates a positive number.<br /><br /> - indicates a negative number.<br /><br /> ~ indicates the one's complement operator.|  
|{ *binary_operator* }|Is an operator that defines the way two expressions are combined to yield a single result. *binary_operator* can be an arithmetic operator, the assignment operator (=), a bitwise operator, a comparison operator, a logical operator, the string concatenation operator (+), or a unary operator. For more information about operators, see [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md).|  
|*ranking_windowed_function*|Is any [!INCLUDE[tsql](../../includes/tsql-md.md)] ranking function. For more information, see [Ranking Functions &#40;Transact-SQL&#41;](../../t-sql/functions/ranking-functions-transact-sql.md).|  
|*aggregate_windowed_function*|Is any [!INCLUDE[tsql](../../includes/tsql-md.md)] aggregate function with the OVER clause. For more information, see [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md).|  
  
## Expression Results  
 For a simple expression made up of a single constant, variable, scalar function, or column name: the data type, collation, precision, scale, and value of the expression is the data type, collation, precision, scale, and value of the referenced element.  
  
 When two expressions are combined by using comparison or logical operators, the resulting data type is Boolean and the value is one of the following: TRUE, FALSE, or UNKNOWN. For more information about Boolean data types, see [Comparison Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/comparison-operators-transact-sql.md).  
  
 When two expressions are combined by using arithmetic, bitwise, or string operators, the operator determines the resulting data type.  
  
 Complex expressions made up of many symbols and operators evaluate to a single-valued result. The data type, collation, precision, and value of the resulting expression is determined by combining the component expressions, two at a time, until a final result is reached. The sequence in which the expressions are combined is defined by the precedence of the operators in the expression.  
  
## Remarks  
 Two expressions can be combined by an operator if they both have data types supported by the operator and at least one of these conditions is true:  
  
-   The expressions have the same data type.  
  
-   The data type with the lower precedence can be implicitly converted to the data type with the higher data type precedence.  
  
 If the expressions do not meet these conditions, the CAST or CONVERT functions can be used to explicitly convert the data type with the lower precedence to either the data type with the higher precedence or to an intermediate data type that can be implicitly converted to the data type with the higher precedence.  
  
 If there is no supported implicit or explicit conversion, the two expressions cannot be combined.  
  
 The collation of any expression that evaluates to a character string is set by following the rules of collation precedence. For more information, see [Collation Precedence &#40;Transact-SQL&#41;](../../t-sql/statements/collation-precedence-transact-sql.md).  
  
 In a programming language such as C or [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)], an expression always evaluates to a single result. Expressions in a [!INCLUDE[tsql](../../includes/tsql-md.md)] select list follow a variation on this rule: The expression is evaluated individually for each row in the result set. A single expression may have a different value in each row of the result set, but each row has only one value for the expression. For example, in the following `SELECT` statement both the reference to `ProductID` and the term `1+2` in the select list are expressions:  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT ProductID, 1+2  
FROM Production.Product;  
GO  
```  
  
 The expression `1+2` evaluates to `3` in each row in the result set. Although the expression `ProductID` generates a unique value in each result set row, each row only has one value for `ProductID`.  
 
- [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] allocates a fixed maximum amount of memory to each thread so no thread can use up all the memory.  Some of this memory is used for storing queries' expressions.  If a query has too many expressions and its required memory exceeds the internal limit, the engine will not execute it.  To avoid this problem, users can change the query into multiple queries with smaller number of expressions in each. For example, you have a query with a long list of expressions in the WHERE clause: 

```sql
DELETE FROM dbo.MyTable 
WHERE
(c1 = '0000001' AND c2 = 'A000001') or
(c1 = '0000002' AND c2 = 'A000002') or
(c1 = '0000003' AND c2 = 'A000003') 
/* ... additional, similar expressions omitted for simplicity */
```
Change this query to:

```sql
DELETE FROM dbo.MyTable WHERE (c1 = '0000001' AND c2 = 'A000001');
DELETE FROM dbo.MyTable WHERE (c1 = '0000002' AND c2 = 'A000002');
DELETE FROM dbo.MyTable WHERE (c1 = '0000003' AND c2 = 'A000003');
/* ... refactored, individual DELETE statements omitted for simplicity  */
```

## See Also  
 [AT TIME ZONE &#40;Transact-SQL&#41;](../../t-sql/queries/at-time-zone-transact-sql.md)   
 [CASE &#40;Transact-SQL&#41;](../../t-sql/language-elements/case-transact-sql.md)   
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)   
 [COALESCE &#40;Transact-SQL&#41;](../../t-sql/language-elements/coalesce-transact-sql.md)   
 [Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md)   
 [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)   
 [NULLIF &#40;Transact-SQL&#41;](../../t-sql/language-elements/nullif-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)  
  
  
