---
title: "SET @local_variable (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 68d2f7d9-73df-4983-9d76-f7ea53bbc26f
caps.latest.revision: 12
author: BarbKess
---
# SET @local_variable (SQL Server PDW)
Sets the value of a local variable in SQL Server PDW, previously created by using the DECLARE @*local_variable* statement.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET   
{ @local_variable  
    { = | += | -= | *= | /= | %= | &= | ^= | |= } expression  
}  
```  
  
## Arguments  
@*local_variable*  
Is the name of a variable of any type. Variable names must start with one at sign (@). Variable names must comply with the rules for identifiers which are explained in [Object Naming Rules &#40;SQL Server PDW&#41;](../sqlpdw/object-naming-rules-sql-server-pdw.md).  
  
{ += | -= | *= | /= | %= | &= | ^= | |= }  
Compound assignment operator:  
  
1.  +=              Add and assign  
  
2.  -=               Subtract and assign  
  
3.  *=              Multiply and assign  
  
4.  /=               Divide and assign  
  
5.  %=             Modulo and assign  
  
6.  &=              Bitwise AND and assign  
  
7.  ^=              Bitwise XOR and assign  
  
8.  |=               Bitwise OR and assign  
  
*expression*  
Is any valid expression. For more information, see [Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md).  
  
## Remarks  
  
## Examples  
  
### A. Printing the value of a variable initialized by using SET  
The following example creates the `@myvar` variable, puts a string value into the variable, and prints the value of the `@myvar` variable.  
  
```  
DECLARE @myvar char(20);  
SET @myvar = 'This is a test';  
SELECT top 1 @myvar FROM sys.databases;  
```  
  
### B. Using a local variable assigned a value by using SET in a SELECT statement  
The following example creates a local variable named `@dept` and uses this local variable in a `SELECT` statement to find the first and last names of all employees who work in the `Marketing` department.  
  
```  
USE AdventureWorksPDW2012;  
  
DECLARE @dept char(25);  
SET @dept = N'Marketing';  
SELECT RTRIM(FirstName) + ' ' + RTRIM(LastName) AS Name  
FROM DimEmployee   
WHERE DepartmentName = @dept;  
```  
  
### C. Using a compound assignment for a local variable  
The following two examples produce the same result. They create a local variable named `@NewBalance`, multiplies it by `10` and displays the new value of the local variable in a `SELECT` statement. The second example uses a compound assignment operator.  
  
```  
/* Example one */  
DECLARE  @NewBalance  int ;  
SET  @NewBalance  =  10;  
SET  @NewBalance  =  @NewBalance  *  10;  
SELECT  TOP 1 @NewBalance FROM sys.tables;  
  
/* Example Two */  
DECLARE @NewBalance int = 10;  
SET @NewBalance *= 10;  
SELECT TOP 1 @NewBalance FROM sys.tables;  
```  
  
### D. Assigning a value from a query  
The following example uses a query to assign a value to a variable.  
  
```  
USE AdventureWorksPDW2012;  
  
DECLARE @rows int;  
SET @rows = (SELECT COUNT(*) FROM dbo.DimCustomer);  
SELECT TOP 1 @rows FROM sys.tables;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
