---
title: "Expressions (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 2fb5312a-1262-4761-bef8-4ea03f2e8137
caps.latest.revision: 27
author: BarbKess
---
# Expressions (SQL Server PDW)
A combination of symbols and operators that SQL Server PDW evaluates to obtain a single data value. Simple expressions can be a single constant, variable, column, or scalar function. Operators can be used to join two or more simple expressions into a complex expression.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
Expression in a SELECT statement  
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
  
Scalar Expression in a DECLARE, SET, IF…ELSE, or WHILE statement  
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
  
## Arguments  
*constant*  
A symbol that represents a single, specific data value. For more information, see [Constants](../sqlpdw/constants-sql-server-pdw.md).  
  
*scalar_function*  
A unit of SQL syntax that provides a specific service and returns a single value. Examples of scalar functions are SUM and CAST.  
  
*column*  
The name of a column in a table or view, fully qualified if necessary, and encased in the bracket delimiter '[…]' if the column name is a reserved word.  
  
*variable*  
Is the name of a variable.  
  
*expression*  
Any valid expression as defined in this topic. The parentheses are grouping operators that make sure that all the operators in the expression within the parentheses are evaluated before the resulting expression is combined with another.  
  
*scalar_subquery*  
A subquery that returns one value. For example, `SELECT MAX(UnitPrice) FROM Products`.  
  
*unary_operator*  
The [- (Negative)](../sqlpdw/negative-sql-server-pdw.md) unary operator.  
  
Unary operators can be applied only to expressions that evaluate to any one of the numeric data types.  
  
*binary_operator*  
An operator that defines the way two expressions are combined to yield a single result. *binary_operator* can be an arithmetic operator, the assignment operator (=), a comparison operator, a logical operator, the string concatenation operator (+), or a unary operator. For more information about operators, see [Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md).  
  
COLLATE *Windows_collation_name*  
Specifies the collation for the expression. The collation must be one of the Windows collations supported by SQL Server. For a list of Windows collations supported by SQL Server, see [Windows Collation Name (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188046.aspx). For examples using **COLLATE**, see [COLLATE (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms184391.aspx).  
  
The default collation is the appliance-level collation Latin1_General_100_CI_AS_KS_WS. For more information about using collations in SQL Server PDW, see [Collations &#40;SQL Server PDW&#41;](../sqlpdw/collations-sql-server-pdw.md).  
  
## Expression Results  
For a simple expression made up of a single constant, variable, scalar function, or column name: the data type, collation, precision, scale, and value of the expression is the data type, collation, precision, scale, and value of the referenced element.  
  
When two expressions are combined by using comparison or logical operators, the resulting data type is Boolean and the value is one of the following: TRUE, FALSE, or UNKNOWN.  
  
When two expressions are combined by using arithmetic, bitwise, or string operators, the operator determines the resulting data type. For more information, see [Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md).  
  
Complex expressions made up of many symbols and operators evaluate to a single-valued result. The data type, collation, precision, and value of the resulting expression is determined by combining the component expressions, two at a time, until a final result is reached. The sequence in which the expressions are combined is defined by the precedence of the operators in the expression. For more information about operator precedence, see [Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md).  
  
SQL Server PDW uses the SQL Server collation precedence rules to set the collation of each expression that evaluates to a character string. For more information, see [Collation Precedence (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms190286.aspx) on MSDN.  
  
## General Remarks  
**Implicit and Explicit Conversions**  
  
Two expressions can be combined by an operator if they both have data types supported by the operator and at least one of these conditions is true:  
  
-   The expressions have the same data type.  
  
-   The data type with the lower precedence can be implicitly converted to the data type with the higher data type precedence.  
  
If the expressions do not meet these conditions, the CAST function can be used to explicitly convert the data type with the lower precedence to either the data type with the higher precedence or to an intermediate data type that can be implicitly converted to the data type with the higher precedence.  
  
If there is no supported implicit or explicit conversion, the two expressions cannot be combined.  
  
For more information about implicit data type conversions, see [Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md). For more information about explicit data type conversions, see [CAST and CONVERT &#40;SQL Server PDW&#41;](../sqlpdw/cast-and-convert-sql-server-pdw.md)  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
