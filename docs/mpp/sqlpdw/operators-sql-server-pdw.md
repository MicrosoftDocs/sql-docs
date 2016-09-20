---
title: "Operators (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 045ae1fd-2ebe-4f92-824d-a57afbab7b1d
caps.latest.revision: 29
author: BarbKess
---
# Operators (SQL Server PDW)
Describes the operators and operator precedence rules used in SQL Server PDWSQL expressions.  
  
## Table of Contents  
  
-   [Arithmetic Operators](#ArithmeticOperators)  
  
-   [Comparison Operators](#ComparisonOperators)  
  
-   [Comparison Operators](#ComparisonOperators)  
  
-   [Comparison Operators](#ComparisonOperators)  
  
-   [Operator Precedence](#OperatorPrecedence)  
  
## <a name="ArithmeticOperators"></a>Arithmetic Operators  
Arithmetic operators perform mathematical operations on two expressions of one or more of the data types of the numeric data type category. For more information about data type categories and rounding and truncation behaviors when performing mathematical operations on numeric data types, see [Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md).  
  
|Operator|Meaning|  
|------------|-----------|  
|[+ (Add)](../../mpp/sqlpdw/add-sql-server-pdw.md)|Addition|  
|[- (Subtract)](../../mpp/sqlpdw/subtract-sql-server-pdw.md)|Subtraction|  
|[* (Multiply)](../../mpp/sqlpdw/multiply-sql-server-pdw.md)|Multiplication|  
|[/ (Divide)](../../mpp/sqlpdw/divide-operator-sql-server-pdw.md)|Division|  
|[% (Modulo)](../../mpp/sqlpdw/modulo-sql-server-pdw.md)|Returns the integer remainder of a division. For example, 12 % 5 = 2 because the remainder of 12 divided by 5 is 2. (For use of % as a wildcard character, see [Wildcard Character&#40;s&#41; to Match &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/wildcard-character-s-to-match-sql-server-pdw.md).)|  
  
The plus (+) and minus (-) operators can also be used to perform arithmetic operations on **datetime** values.  
  
For more information about the precision and scale of the result of an arithmetic operation, see "Precision, Scale, and Length for Data Types" in the [Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md) topic.  
  
### <a name="UnderstandingRounding"></a>Understanding Rounding and Truncation in Mathematical Operations  
  
-   Calculations performed on numeric data types may result in either rounding or truncation.  
  
This section describes the logic applied to the handling of **int**, **decimal**, and **float** values in mathematical calculations:  
  
-   SQL Server PDW truncates (removes the decimal) during integer division, rather than rounding.  
  
-   SQL Server PDW uses the "round odd down" method when determining rounding on values ending with "5". This means that the digit preceding the "5" determines whether the value will be rounded up or down. If the digit to the left of the "5" is even, the value will be rounded up. If the digit to the left of the "5" is odd, the value will be rounded down. This method is used so that results are not biased up when the digit determining the rounding is "5".  
  
#### Rounding and Truncation Examples  
The following table provides examples of rounding and truncation behavior.  
  
|Formula|Result|Explanation|  
|-----------|----------|---------------|  
|5/4<br /><br />7/4<br /><br />3/2<br /><br />5/2<br /><br />7/2<br /><br />9/2|1<br /><br />1<br /><br />1<br /><br />2<br /><br />3<br /><br />4|These are integer evaluations, so the results are truncated to an integer.|  
|5*1.000/4<br /><br />7.000/4<br /><br />3.000/2<br /><br />5.000/2<br /><br />7\*1.000/2<br /><br />9\*1.00/2|1.250000...<br /><br />1.750000...<br /><br />1.500000...<br /><br />2.500000...<br /><br />3.500000...<br /><br />4.500000...|These are decimal evaluations, so the decimal precision is preserved.|  
|ROUND(5*1.000/4)<br /><br />ROUND(7.000/4)<br /><br />ROUND(3.000/2,0)<br /><br />ROUND(5.000/2,0)<br /><br />ROUND(7\*1.000/2,0)<br /><br />ROUND(9\*1.000/2,0)|1.0<br /><br />2.0<br /><br />1.0<br /><br />3.0<br /><br />3.0<br /><br />5.0|Use of the ROUND function (see [ROUND &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/round-sql-server-pdw.md)) causes the decimal to be rounded up or down. Since the resulting type is a decimal type, the decimal place is preserved in the result.|  
  
## <a name="ComparisonOperators"></a>Comparison Operators  
Comparison operators test whether two expressions are the same. Comparison operators can be used on all expression. The following table lists the SQL comparison operators.  
  
|Operator|Meaning|  
|------------|-----------|  
|[= (Equals)](../../mpp/sqlpdw/equals-sql-server-pdw.md)|Equal to|  
|[&gt; (Greater Than)](../../mpp/sqlpdw/greater-than-sql-server-pdw.md)|Greater than|  
|[&lt; (Less Than)](../../mpp/sqlpdw/less-than-sql-server-pdw.md)|Less than|  
|[&gt;= (Greater Than or Equal To)](../../mpp/sqlpdw/greater-than-or-equal-to-sql-server-pdw.md)|Greater than or equal to|  
|[&lt;= (Less Than or Equal To)](../../mpp/sqlpdw/less-than-or-equal-to-sql-server-pdw.md)|Less than or equal to|  
|[&lt;&gt; (Not Equal To)](../../mpp/sqlpdw/not-equal-to-sql-server-pdw.md)|Not equal to|  
|[!= (Not Equal To)](../../mpp/sqlpdw/not-equal-to-sql-server-pdw.md)|Not equal to (not ISO standard)|  
  
### Boolean Data Types with Comparison Operators  
The result of a comparison operator has the **Boolean** data type. This has three values: TRUE, FALSE, and UNKNOWN. Expressions that return a **Boolean** data type are known as Boolean expressions.  
  
Unlike other SQL Server PDW data types, a **Boolean** data type cannot be specified as the data type of a table column or variable, and cannot be returned in a result set.  
  
An operator that has one or two NULL expressions returns UNKNOWN.  
  
Expressions with **Boolean** data types are used in the WHERE clause to filter the rows that qualify for the search conditions.  
  
```  
SELECT id, firstName, lastName FROM Customer WHERE orderTotal > 100;  
```  
  
## <a name="LogicalOperators"></a>Logical Operators  
Logical operators test for the truth of some condition. Logical operators, like comparison operators, return a **Boolean** data type with a value of TRUE, FALSE, or UNKNOWN.  
  
|Operator|Meaning|  
|------------|-----------|  
|[AND](../../mpp/sqlpdw/and-sql-server-pdw.md)|TRUE if both **Boolean** expressions are TRUE.|  
|[BETWEEN](../../mpp/sqlpdw/between-sql-server-pdw.md)|TRUE if the operand is within a range.|  
|[EXISTS](../../mpp/sqlpdw/exists-sql-server-pdw.md)|TRUE if a subquery contains any rows.|  
|[IN](../../mpp/sqlpdw/in-sql-server-pdw.md)|TRUE if the operand is equal to one of a list of expressions.|  
|[IS [NOT] NULL](../Topic/IS%20[NOT]%20NULL%20(SQL%20Server%20PDW).md)|TRUE if the following expression is null.|  
|[LIKE](../../mpp/sqlpdw/like-sql-server-pdw.md)|TRUE if the operand matches a pattern.|  
|[NOT](../../mpp/sqlpdw/not-sql-server-pdw.md)|Reverses the value of any other Boolean operator.|  
|[OR](../../mpp/sqlpdw/or-sql-server-pdw.md)|TRUE if either **Boolean** expression is TRUE.|  
  
## <a name="StringConcatenationOperators"></a>String Concatenation Operator  
The plus sign (+) is the string concatenation operator. All other string manipulation is handled by using string functions such as SUBSTRING.  
  
In concatenating data of the **varchar**, or **char** data types, the empty string is interpreted as an empty string. For example, 'abc' *+* '' *+* 'def' is stored as 'abcdef'.  
  
### Examples  
The following is an example of concatenation.  
  
```  
SELECT ('abc' + 'def') FROM DimEmployee;  
```  
  
Here is the result set.  
  
```  
------  
abcdef  
```  
  
The following query displays names of the first four contacts under the `Moniker` column in last name, first name order, with a comma and space after the last name.  
  
```  
SELECT LastName + ', ' + FirstName AS Moniker   
FROM Person.Contact  
WHERE ContactID < 5;  
```  
  
Here is the result set.  
  
```  
Moniker  
-------------------------  
Achong, Gustavo  
Abel, Catherine  
Abercrombie, Kim  
Acevedo, Humberto  
```  
  
Other data types, such as **datetime** and **date**, must be converted to character strings by using the CAST conversion function before they can be concatenated with a string.  
  
```  
SELECT 'The due date is ' + CAST(DueDate AS varchar(128))  
FROM Sales.SalesOrderHeader  
WHERE SalesOrderID = 43659;  
```  
  
Here is the result set.  
  
```  
---------------------------------------  
The due date is Jul 13 2001 12:00AM  
```  
  
When the input strings both have the same collation, the output string has the same collation as the inputs. When the input strings have different collations, the rules of collation precedence determine the collation of the output string. See Collation Precedence in the [Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md) topic for more information.  
  
## <a name="OperatorPrecedence"></a>Operator Precedence  
When a complex expression has multiple operators, operator precedence determines the sequence in which the operations are performed. The order of execution can significantly affect the resulting value.  
  
Operators have the precedence levels shown in the following table. An operator on higher levels is evaluated before an operator on a lower level.  
  
|Level|Operators|  
|---------|-------------|  
|1|* (Multiply), / (Division), MOD|  
|2|+ (Positive), - (Negative), + (Add), (+ Concatenate / &#124;&#124; (Concatenate)), - (Subtract)|  
|3|=, >, <, >=, <=, <>, != (Comparison operators)|  
|4|NOT|  
|5|AND|  
|6|ALL, BETWEEN, IN, LIKE, OR|  
  
When two operators in an expression have the same operator precedence level, they are evaluated left to right based on their position in the expression. For example, in the following expression, the subtraction operator is evaluated before the addition operator.  
  
```  
SELECT id, first_name, last_name FROM Customer WHERE (RecentSales - LastYearSales + Commission) > 1000;  
```  
  
Use parentheses to override the defined precedence of the operators in an expression. Everything within the parentheses is evaluated first to yield a single value before that value can be used by any operator outside the parentheses.  
  
For example, in the following expression, the multiplication operator has a higher precedence than the addition operator. Therefore, it is evaluated first.  
  
```  
SELECT id, first_name, last_name FROM Customer WHERE (RecentSales - Commission * 10) > 1000;  
```  
  
In the following expression, the parentheses cause the addition to be performed first. The expression result is `18`.  
  
```  
SELECT id, first_name, last_name FROM Customer WHERE ((RecentSales - Commission) * 10) > 1000;  
```  
  
If an expression has nested parentheses, the most deeply nested expression is evaluated first. The following example contains nested parentheses, with the expression `5 - 3` in the most deeply nested set of parentheses. This expression yields a value of `2`. Then, the addition operator (`+`) adds this result to `4`. This yields a value of `6`. Finally, the `6` is multiplied by `2` to yield an expression result of `12`.  
  
```  
SELECT id, first_name, last_name FROM Customer WHERE 2 * (4 + (5 - 3) ) > 100;  
-- Evaluates to 2 * (4 + 2) which then evaluates to 2 * 6, and   
-- yields an expression result of 12.  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
  
