---
title: "SELECT @local_variable (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "variable_TSQL"
  - "@loca_variable"
  - "@local"
  - "variable"
  - "@loca_variable_TSQL"
  - "@local_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "variables [SQL Server], assigning"
  - "SELECT statement [SQL Server], @local_variable"
  - "@local_variable"
  - "local variables [SQL Server]"
ms.assetid: 8e1a9387-2c5d-4e51-a1fd-a2a95f026d6f
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
MonikerRange: "= azuresqldb-current ||>= sql-server-2016 ||= azure-sqldw-latest||>= sql-server-linux-2017||= sqlallproducts-allversions"
---
# SELECT @local_variable (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-asdw-xxx-md.md)]

  Sets a local variable to the value of an expression.  
  
 For assigning variables, we recommend that you use [SET @local_variable](../../t-sql/language-elements/set-local-variable-transact-sql.md) instead of SELECT @*local_variable*.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
SELECT { @local_variable { = | += | -= | *= | /= | %= | &= | ^= | |= } expression } 
    [ ,...n ] [ ; ]  
```  
  
## Arguments  
@*local_variable*  
 Is a declared variable for which a value is to be assigned.  
  
{= | += | -= | \*= | /= | %= | &= | ^= | |= }   
Assign the value on the right to the variable on the left.  
  
Compound assignment operator:  
  |operator |action |   
  |-----|-----|  
  | = | Assigns the expression that follows, to the variable. |  
  | += | Add and assign |   
  | -= | Subtract and assign |  
  | \*= | Multiply and assign |  
  | /= | Divide and assign |  
  | %= | Modulo and assign |  
  | &= | Bitwise AND and assign |  
  | ^= | Bitwise XOR and assign |  
  | \|= | Bitwise OR and assign |  
  
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md). This includes a scalar subquery.  
  
## Remarks  
 SELECT @*local_variable* is typically used to return a single value into the variable. However, when *expression* is the name of a column, it can return multiple values. If the SELECT statement returns more than one value, the variable is assigned the last value that is returned.  
  
 If the SELECT statement returns no rows, the variable retains its present value. If *expression* is a scalar subquery that returns no value, the variable is set to NULL.  
  
 One SELECT statement can initialize multiple local variables.  
  
> [!NOTE]  
>  A SELECT statement that contains a variable assignment cannot be used to also perform typical result set retrieval operations.  
  
## Examples  
  
### A. Use SELECT @local_variable to return a single value  
 In the following example, the variable `@var1` is assigned `Generic Name` as its value. The query against the `Store` table returns no rows because the value specified for `CustomerID` does not exist in the table. The variable retains the `Generic Name` value.  
  
```sql  
-- Uses AdventureWorks    
  
DECLARE @var1 varchar(30);         
SELECT @var1 = 'Generic Name';         
SELECT @var1 = Name         
FROM Sales.Store         
WHERE CustomerID = 1000 ;        
SELECT @var1 AS 'Company Name';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```  
 Company Name  
 ------------------------------  
 Generic Name  
 ```  
  
### B. Use SELECT @local_variable to return null  
 In the following example, a subquery is used to assign a value to `@var1`. Because the value requested for `CustomerID` does not exist, the subquery returns no value and the variable is set to `NULL`.  
  
```sql  
-- Uses AdventureWorks  
  
DECLARE @var1 varchar(30)   
SELECT @var1 = 'Generic Name'   
SELECT @var1 = (SELECT Name   
FROM Sales.Store   
WHERE CustomerID = 1000)   
SELECT @var1 AS 'Company Name' ;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Company Name  
----------------------------  
NULL  
```  
  
## See Also  
 [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)  
  
  
