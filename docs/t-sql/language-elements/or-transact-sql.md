---
title: "OR (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "OR_TSQL"
  - "OR"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "conditions [SQL Server], combining"
  - "combining conditions"
  - "OR operator"
ms.assetid: b730a256-4a63-4880-9906-65b05cd9caf2
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# OR (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Combines two conditions. When more than one logical operator is used in a statement, OR operators are evaluated after AND operators. However, you can change the order of evaluation by using parentheses.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
boolean_expression OR boolean_expression  
```  
  
## Arguments  
 *boolean_expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) that returns TRUE, FALSE, or UNKNOWN.  
  
## Result Types  
 **Boolean**  
  
## Result Value  
 OR returns TRUE when either of the conditions is TRUE.  
  
## Remarks  
 The following table shows the result of the OR operator.  
  
||TRUE|FALSE|UNKNOWN|  
|------|----------|-----------|-------------|  
|**TRUE**|TRUE|TRUE|TRUE|  
|**FALSE**|TRUE|FALSE|UNKNOWN|  
|**UNKNOWN**|TRUE|UNKNOWN|UNKNOWN|  
  
## Examples  
 The following example uses the `vEmployeeDepartmentHistory` view to retrieve the names of `Quality Assurance` personnel who work either the evening shift or the night shift. If the parentheses are omitted, the query returns `Quality Assurance` employees who work the evening shift and all employees who work the night shift.  
  
```  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName, Shift   
FROM HumanResources.vEmployeeDepartmentHistory  
WHERE Department = 'Quality Assurance'  
   AND (Shift = 'Evening' OR Shift = 'Night');  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 FirstName    LastName         Shift 
 ------------ ---------------- ------- 
 Andreas      Berglund         Evening 
 Sootha       Charncherngkha   Night
 ```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example retrieves the names of employees who either earn a `BaseRate` less than 20 or have a `HireDate` January 1, 2001 or later.  
  
```  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName, BaseRate, HireDate   
FROM DimEmployee  
WHERE BaseRate < 10 OR HireDate >= '2001-01-01';  
```  
  
## See Also  
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)  
  
  


