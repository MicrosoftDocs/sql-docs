---
title: "Collation Precedence | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "coercible-default collation label"
  - "precedence [SQL Server], collations"
  - "collation sensitive"
  - "collations [SQL Server], precedence"
  - "explicit collation label [SQL Server]"
  - "implicit collation label"
  - "no-collation label"
  - "collation insensitive"
  - "operators [Transact-SQL], collations"
  - "collation labels"
  - "collation coercion rules"
  - "rules [SQL Server], collations"
ms.assetid: 58c4e64b-5634-4c29-aa22-33193282dd27
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Collation Precedence
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Collation precedence, also known as collation coercion rules, determines the following:  
  
-   The collation of the final result of an expression that is evaluated to a character string.  
  
-   The collation that is used by collation-sensitive operators that use character string inputs but do not return a character string, such as LIKE and IN.  
  
The collation precedence rules apply only to the character string data types: **char**, **varchar**, **text**, **nchar**, **nvarchar**, and **ntext**. Objects that have other data types do not participate in collation evaluations.  
  
## Collation Labels  
The following table lists and describes the four categories in which the collations of all objects are identified. The name of each category is called the collation label.  
  
|Collation label|Types of objects|  
|---------------------|----------------------|  
|Coercible-default|Any [!INCLUDE[tsql](../../includes/tsql-md.md)] character string variable, parameter, literal, or the output of a catalog built-in function, or a built-in function that does not take string inputs but produces a string output.<br /><br /> If the object is declared in a user-defined function, stored procedure, or trigger, the object is assigned the default collation of the database in which the function, stored procedure, or trigger is created. If the object is declared in a batch, the object is assigned the default collation of the current database for the connection.|  
|Implicit X|A column reference. The collation of the expression (X) is taken from the collation defined for the column in the table or view.<br /><br /> Even if the column was explicitly assigned a collation by using a COLLATE clause in the CREATE TABLE or CREATE VIEW statement, the column reference is classified as implicit.|  
|Explicit X|An expression that is explicitly cast to a specific collation (X) by using a COLLATE clause in the expression.|  
|No-collation|Indicates that the value of an expression is the result of an operation between two strings that have conflicting collations of the implicit collation label. The expression result is defined as not having a collation.|  
  
## Collation Rules  
The collation label of a simple expression that references only one character string object is the collation label of the referenced object.  
  
The collation label of a complex expression that references two operand expressions with the same collation label is the collation label of the operand expressions.  
  
The collation label of the final result of a complex expression that references two operand expressions with different collations is based on the following rules:  
  
-   Explicit takes precedence over implicit. Implicit takes precedence over Coercible-default:  
  
     Explicit > Implicit > Coercible-default  
  
-   Combining two Explicit expressions that have been assigned different collations generates an error:  
  
     Explicit X + Explicit Y = Error  
  
-   Combining two Implicit expressions that have different collations yields a result of No-collation:  
  
     Implicit X + Implicit Y = No-collation  
  
-   Combining an expression with No-collation with an expression of any label, except Explicit collation (see the following rule), yields a result that has the No-collation label:  
  
     No-collation + anything = No-collation  
  
-   Combining an expression with No-collation with an expression that has an Explicit collation, yields an expression with an Explicit label:  
  
     No-collation + Explicit X = Explicit  
  
The following table summarizes the rules.  
  
|Operand coercion label|Explicit X|Implicit X|Coercible-default|No-collation|  
|----------------------------|----------------|----------------|------------------------|-------------------|  
|**Explicit Y**|Generates Error|Result is Explicit Y|Result is Explicit Y|Result is Explicit Y|  
|**Implicit Y**|Result is Explicit X|Result is No-collation|Result is Implicit Y|Result is No-collation|  
|**Coercible-default**|Result is Explicit X|Result is Implicit X|Result is Coercible-default|Result is No-collation|  
|**No-collation**|Result is Explicit X|Result is No-collation|Result is No-collation|Result is No-collation|  
  
The following additional rules also apply to collation precedence:  
  
-   You cannot have multiple COLLATE clauses on an expression that is already an explicit expression. For example, the following `WHERE` clause is not valid because a `COLLATE` clause is specified for an expression that is already an explicit expression:  
  
     `WHERE ColumnA = ( 'abc' COLLATE French_CI_AS) COLLATE French_CS_AS`  
  
-   Code page conversions for **text** data types are not allowed. You cannot cast a **text** expression from one collation to another if they have the different code pages. The assignment operator cannot assign values when the collation of the right text operand has a different code page than the left text operand.  
  
Collation precedence is determined after data type conversion. The operand from which the resulting collation is taken can be different from the operand that supplies the data type of the final result. For example, consider the following batch:  
  
```sql  
CREATE TABLE TestTab  
   (PrimaryKey int PRIMARY KEY,  
    CharCol char(10) COLLATE French_CI_AS  
   )  
  
SELECT *  
FROM TestTab  
WHERE CharCol LIKE N'abc'  
```  
  
The Unicode data type of the simple expression `N'abc'` has a higher data type precedence. Therefore, the resulting expression has the Unicode data type assigned to `N'abc'`. However, the expression `CharCol` has a collation label of Implicit, and `N'abc'` has a lower coercion label of Coercible-default. Therefore, the collation that is used is the `French_CI_AS` collation of `CharCol`.  
  
### Examples of Collation Rules  
 The following examples show how the collation rules work. To run the examples, create the following test table.  
  
```sql  
USE tempdb;  
GO  
  
CREATE TABLE TestTab (  
   id int,   
   GreekCol nvarchar(10) collate greek_ci_as,   
   LatinCol nvarchar(10) collate latin1_general_cs_as  
   )  
INSERT TestTab VALUES (1, N'A', N'a');  
GO  
```  
  
#### Collation Conflict and Error  
 The predicate in the following query has collation conflict and generates an error.  
  
```sql  
SELECT *   
FROM TestTab   
WHERE GreekCol = LatinCol;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Msg 448, Level 16, State 9, Line 2  
Cannot resolve collation conflict between 'Latin1_General_CS_AS' and 'Greek_CI_AS' in equal to operation.  
```  
  
#### Explicit Label vs. Implicit Label  
 The predicate in the following query is evaluated in collation `greek_ci_as` because the right expression has the Explicit label. This takes precedence over the Implicit label of the left expression.  
  
```sql  
SELECT *   
FROM TestTab   
WHERE GreekCol = LatinCol COLLATE greek_ci_as;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
id          GreekCol             LatinCol  
----------- -------------------- --------------------  
          1 A                    a  
  
(1 row affected)  
```  
  
#### No-Collation Labels  
The `CASE` expressions in the following queries have a No-collation label; therefore, they cannot appear in the select list or be operated on by collation-sensitive operators. However, the expressions can be operated on by collation-insensitive operators.  
  
```sql  
SELECT (CASE WHEN id > 10 THEN GreekCol ELSE LatinCol END)   
FROM TestTab;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Msg 451, Level 16, State 1, Line 1  
Cannot resolve collation conflict for column 1 in SELECT statement.  
```  
  
```sql  
SELECT PATINDEX((CASE WHEN id > 10 THEN GreekCol ELSE LatinCol END), 'a')  
FROM TestTab;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Msg 446, Level 16, State 9, Server LEIH2, Line 1  
Cannot resolve collation conflict for patindex operation.  
```  
  
```sql  
SELECT (CASE WHEN id > 10 THEN GreekCol ELSE LatinCol END) COLLATE Latin1_General_CI_AS   
FROM TestTab;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
--------------------  
a  
  
(1 row affected)  
```  
  
## Collation Sensitive and Collation Insensitive  
Operators and functions are either collation sensitive or insensitive.  
  
Collation sensitive  
This means that specifying a No-collation operand is a compile-time error. The expression result cannot be No-collation.  
  
Collation insensitive  
This means that the operands and result can be No-collation.  
  
### Operators and Collation  
The comparison operators, and the MAX, MIN, BETWEEN, LIKE, and IN operators, are collation sensitive. The string used by the operators is assigned the collation label of the operand that has the higher precedence. The UNION statement is also collation sensitive, and all string operands and the final result is assigned the collation of the operand with the highest precedence. The collation precedence of the UNION operand and result are evaluated column by column.  
  
The assignment operator is collation insensitive and the right expression is cast to the left collation.  
  
The string concatenation operator is collation sensitive, the two string operands and the result are assigned the collation label of the operand with the highest collation precedence. The UNION ALL and CASE statements are collation insensitive, and all string operands and the final results are assigned the collation label of the operand with the highest precedence. The collation precedence of the UNION ALL operands and result are evaluated column by column.  
  
### Functions and Collation  
THE CAST, CONVERT, and COLLATE functions are collation sensitive for **char**, **varchar**, and **text** data types. If the input and output of the CAST and CONVERT functions are character strings, the output string has the collation label of the input string. If the input is not a character string, the output string is Coercible-default and assigned the collation of the current database for the connection, or the database that contains the user-defined function, stored procedure, or trigger in which the CAST or CONVERT is referenced.  
  
 For the built-in functions that return a string but do not take a string input, the result string is Coercible-default and is assigned either the collation of the current database, or the collation of the database that contains the user-defined function, stored procedure, or trigger in which the function is referenced.  
  
 The following functions are collation-sensitive and their output strings have the collation label of the input string:  
  
|||  
|-|-|  
|CHARINDEX|REPLACE|  
|DIFFERENCE|REVERSE|  
|ISNUMERIC|RIGHT|  
|LEFT|SOUNDEX|  
|LEN|STUFF|  
|LOWER|SUBSTRING|  
|PATINDEX|UPPER|  
  
## See Also  
 [COLLATE &#40;Transact-SQL&#41;](~/t-sql/statements/collations.md)   
 [Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
  
  
