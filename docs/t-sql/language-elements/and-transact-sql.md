---
title: "AND (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "AND_TSQL"
  - "AND"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "values [SQL Server], TRUE"
  - "TRUE"
  - "AND, about AND operators"
  - "AND"
  - "combining expressions"
ms.assetid: b61d7f8d-5a51-49b7-91dd-f6190a5a0fb9
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# AND (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Combines two Boolean expressions and returns **TRUE** when both expressions are **TRUE**. When more than one logical operator is used in a statement, the **AND** operators are evaluated first. You can change the order of evaluation by using parentheses.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
boolean_expression AND boolean_expression  
```  
  
## Arguments  
 *boolean_expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) that returns a Boolean value: **TRUE**, **FALSE**, or **UNKNOWN**.  
  
## Result Types  
 **Boolean**  
  
## Result Value  
 Returns TRUE when both expressions are TRUE.  
  
## Remarks  
 The following chart shows the outcomes when you compare TRUE and FALSE values by using the AND operator.  
  
||TRUE|FALSE|UNKNOWN|  
|------|----------|-----------|-------------|  
|**TRUE**|TRUE|FALSE|UNKNOWN|  
|**FALSE**|FALSE|FALSE|FALSE|  
|**UNKNOWN**|UNKNOWN|FALSE|UNKNOWN|  
  
## Examples  
  
### A. Using the AND operator  
 The following example selects information about employees who have both the title of `Marketing Assistant` and more than `41` vacation hours available.  
  
```  
-- Uses AdventureWorks  
  
SELECT  BusinessEntityID, LoginID, JobTitle, VacationHours   
FROM HumanResources.Employee  
WHERE JobTitle = 'Marketing Assistant'  
AND VacationHours > 41 ;  
```  
  
### B. Using the AND operator in an IF statement  
 The following examples show how to use AND in an IF statement. In the first statement, both `1 = 1` and `2 = 2` are true; therefore, the result is true. In the second example, the argument `2 = 17` is false; therefore, the result is false.  
  
```  
IF 1 = 1 AND 2 = 2  
BEGIN  
   PRINT 'First Example is TRUE'  
END  
ELSE PRINT 'First Example is FALSE';  
GO  
  
IF 1 = 1 AND 2 = 17  
BEGIN  
   PRINT 'Second Example is TRUE'  
END  
ELSE PRINT 'Second Example is FALSE' ;  
GO  
```  
  
## See Also  
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)  
  
  
