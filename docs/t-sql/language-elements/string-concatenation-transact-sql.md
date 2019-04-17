---
title: "+ (String Concatenation) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/06/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "concatenation"
  - "+"
  - "string"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "concatenation [SQL Server]"
  - "string concatenation operators"
  - "+ (string concatenation)"
ms.assetid: 35cb3d7a-48f5-4b13-926c-a9d369e20ed7
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# + (String Concatenation) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  An operator in a string expression that concatenates two or more character or binary strings, columns, or a combination of strings and column names into one expression (a string operator).  For example `SELECT 'book'+'case';` returns `bookcase`.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
expression + expression  
```  
  
## Arguments  
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any one of the data types in the character and binary data type category, except the **image**, **ntext**, or **text** data types. Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression.  
  
 An explicit conversion to character data must be used when concatenating binary strings and any characters between the binary strings. The following example shows when `CONVERT`, or `CAST`, must be used with binary concatenation and when `CONVERT`, or `CAST`, does not have to be used.  
  
```sql
DECLARE @mybin1 varbinary(5), @mybin2 varbinary(5)  
SET @mybin1 = 0xFF  
SET @mybin2 = 0xA5  
-- No CONVERT or CAST function is required because this example   
-- concatenates two binary strings.  
SELECT @mybin1 + @mybin2  
-- A CONVERT or CAST function is required because this example  
-- concatenates two binary strings plus a space.  
SELECT CONVERT(varchar(5), @mybin1) + ' '   
   + CONVERT(varchar(5), @mybin2)  
-- Here is the same conversion using CAST.  
SELECT CAST(@mybin1 AS varchar(5)) + ' '   
   + CAST(@mybin2 AS varchar(5))  
  
```  
  
## Result Types  
 Returns the data type of the argument with the highest precedence. For more information, see [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
  
## Remarks  
 The + (String Concatenation) operator behaves differently when it works with an empty, zero-length string than when it works with NULL, or unknown values. A zero-length character string can be specified as two single quotation marks without any characters inside the quotation marks. A zero-length binary string can be specified as 0x without any byte values specified in the hexadecimal constant. Concatenating a zero-length string always concatenates the two specified strings. When you work with strings with a null value, the result of the concatenation depends on the session settings. Just like arithmetic operations that are performed on null values, when a null value is added to a known value the result is typically an unknown value, a string concatenation operation that is performed with a null value should also produce a null result. However, you can change this behavior by changing the setting of `CONCAT_NULL_YIELDS_NULL` for the current session. For more information, see [SET CONCAT_NULL_YIELDS_NULL &#40;Transact-SQL&#41;](../../t-sql/statements/set-concat-null-yields-null-transact-sql.md).  
  
 If the result of the concatenation of strings exceeds the limit of 8,000 bytes, the result is truncated. However, if at least one of the strings concatenated is a large value type, truncation does not occur.  
  
## Examples  
  
### A. Using string concatenation  
 The following example creates a single column under the column heading `Name` from multiple character columns, with the last name of the person followed by a comma, a single space, and then the first name of the person. The result set is in ascending, alphabetical order by the last name, and then by the first name.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT (LastName + ', ' + FirstName) AS Name  
FROM Person.Person  
ORDER BY LastName ASC, FirstName ASC;  
```  
  
### B. Combining numeric and date data types  
 The following example uses the `CONVERT` function to concatenate **numeric** and **date** data types.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT 'The order is due on ' + CONVERT(varchar(12), DueDate, 101)  
FROM Sales.SalesOrderHeader  
WHERE SalesOrderID = 50001;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 ------------------------------------------------  
 The order is due on 04/23/2007  
 (1 row(s) affected)
 ```  
  
### C. Using multiple string concatenation  
 The following example concatenates multiple strings to form one long string to display the last name and the first initial of the vice presidents at [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)]. A comma is added after the last name and a period after the first initial.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT (LastName + ',' + SPACE(1) + SUBSTRING(FirstName, 1, 1) + '.') AS Name, e.JobTitle  
FROM Person.Person AS p  
    JOIN HumanResources.Employee AS e  
    ON p.BusinessEntityID = e.BusinessEntityID  
WHERE e.JobTitle LIKE 'Vice%'  
ORDER BY LastName ASC;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 Name               Title  
 -------------      ---------------`  
 Duffy, T.          Vice President of Engineering  
 Hamilton, J.       Vice President of Production  
 Welcker, B.        Vice President of Sales  

 (3 row(s) affected)
 ```  
 
### D. Using large strings in concatenation
The following example concatenates multiple strings to form one long string and then tries to compute the length of the final string. The final length of resultset is 16000, because expression evaluation starts from left that is, @x + @z + @y => (@x + @z) + @y. In this case the result of (@x + @z) is truncated at 8000 bytes and then @y is added to the resultset, which makes the final string length 16000. Since @y is a large value type string, truncation does not occur.

```sql
DECLARE @x varchar(8000) = replicate('x', 8000)
DECLARE @y varchar(max) = replicate('y', 8000)
DECLARE @z varchar(8000) = replicate('z',8000)
SET @y = @x + @z + @y
-- The result of following select is 16000
SELECT len(@y) AS y
GO
```
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 y        
 -------  
 16000  
  
 (1 row(s) affected)
 ```  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### E. Using multiple string concatenation  
 The following example concatenates multiple strings to form one long string to display the last name and the first initial of the vice presidents within a sample database. A comma is added after the last name and a period after the first initial.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT (LastName + ', ' + SUBSTRING(FirstName, 1, 1) + '.') AS Name, Title  
FROM DimEmployee  
WHERE Title LIKE '%Vice Pres%'  
ORDER BY LastName ASC;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Name               Title                                           
-------------      ---------------  
Duffy, T.          Vice President of Engineering  
Hamilton, J.       Vice President of Production  
Welcker, B.        Vice President of Sales  
```  
  
## See Also  
 [+= &#40;String Concatenation Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/string-concatenation-equal-transact-sql.md)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)   
 [Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
  
  



