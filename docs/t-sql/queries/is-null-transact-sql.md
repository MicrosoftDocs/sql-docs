---
title: "IS NULL (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "NULL_TSQL"
  - "IS_[NOT]_NULL_TSQL"
  - "IS_NULL_TSQL"
  - "NULL"
  - "[NOT]_TSQL"
  - "IS"
  - "IS_TSQL"
  - "IS NULL"
  - "IS [NOT] NULL"
  - "[NOT]"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "verifying nullability"
  - "IS NOT NULL clause"
  - "null values [SQL Server], verifying"
  - "null values [SQL Server], IS [NOT] NULL"
  - "IS [NOT] NULL clause"
  - "testing nullability"
  - "checking nullability"
ms.assetid: cdc45cd8-e9b6-4648-8417-892fbeab15af
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# IS NULL (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Determines whether a specified expression is NULL.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
expression IS [ NOT ] NULL  
```  
  
## Arguments  
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md).  
  
 NOT  
 Specifies that the Boolean result be negated. The predicate reverses its return values, returning TRUE if the value is not NULL, and FALSE if the value is NULL.  
  
## Result Types  
 **Boolean**  
  
## Return Code Values  
 If the value of *expression* is NULL, IS NULL returns TRUE; otherwise, it returns FALSE.  
  
 If the value of *expression* is NULL, IS NOT NULL returns FALSE; otherwise, it returns TRUE.  
  
## Remarks  
 To determine whether an expression is NULL, use IS NULL or IS NOT NULL instead of comparison operators (such as = or !=). Comparison operators return UNKNOWN when either or both arguments are NULL.  
  
## Examples  
 The following example returns the name and the weight for all products for which either the weight is less than `10` pounds or the color is unknown, or `NULL`.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT Name, Weight, Color  
FROM Production.Product  
WHERE Weight < 10.00 OR Color IS NULL  
ORDER BY Name;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example returns the full names of all employees with middle initials.  
  
```  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName, MiddleName  
FROM DIMEmployee  
WHERE MiddleName IS NOT NULL  
ORDER BY LastName DESC;  
```  
  
## See Also  
 [CASE &#40;Transact-SQL&#41;](../../t-sql/language-elements/case-transact-sql.md)   
 [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [Logical Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/logical-operators-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [sp_help &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)   
 [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)  
  
  

