---
title: "CHOOSE (Transact-SQL)"
description: "The CHOOSE logical function returns the item at the specified index from a list of values."
author: markingmyname
ms.author: maghan
ms.date: "03/11/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CHOOSE"
  - "CHOOSE_TSQL"
helpviewer_keywords:
  - "CHOOSE function"
dev_langs:
  - "TSQL"
---
# Logical Functions - CHOOSE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the item at the specified index from a list of values in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CHOOSE ( index, val_1, val_2 [, val_n ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *index*  
 Is an integer expression that represents a 1-based index into the list of the items following it.  
  
 If the provided index value has a numeric data type other than **int**, then the value is implicitly converted to an integer. If the index value exceeds the bounds of the array of values, then CHOOSE returns null.  
  
#### *val_1 ... val_n*  
 List of comma separated values of any data type.  
  
## Return Types  
 Returns the data type with the highest precedence from the set of types passed to the function. For more information, see [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
  
## Remarks  
 CHOOSE acts like an index into an array, where the array is composed of the arguments that follow the index argument. The index argument determines which of the following values will be returned.  
  
## Examples  

### A. Simple CHOOSE example

 The following example returns the third item from the list of values that is provided.  
 
```sql 
SELECT CHOOSE ( 3, 'Manager', 'Director', 'Developer', 'Tester' ) AS Result;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Result  
-------------  
Developer  
  
(1 row(s) affected)  
```  

### B. Simple CHOOSE example based on column

 The following example returns a simple character string based on the value in the `ProductCategoryID` column.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT ProductCategoryID, CHOOSE (ProductCategoryID, 'A','B','C','D','E') AS Expression1  
FROM Production.ProductCategory;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
ProductCategoryID Expression1  
----------------- -----------  
3                 C  
1                 A  
2                 B  
4                 D  
  
(4 row(s) affected)  
  
```  

### C. CHOOSE in combination with MONTH
  
 The following example returns the season in which a product model was last modified. The `MONTH` function is used to return the month value from the column `ModifiedDate`. The `CHOOSE` function is used to assign a Northern Hemisphere season. This sample uses the `AdventureWorksLT` database, which can be quickly installed as the sample database for a new Azure SQL Database. For more information, see [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md#deploy-to-azure-sql-database).
  
```sql  
SELECT Name, ModifiedDate, 
CHOOSE(MONTH(ModifiedDate),'Winter','Winter', 'Spring','Spring','Spring','Summer','Summer',   
                          'Summer','Autumn','Autumn','Autumn','Winter') AS Quarter_Modified
FROM SalesLT.ProductModel AS PM
WHERE Name LIKE '%Frame%'
ORDER BY ModifiedDate;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Name                        ModifiedDate            Quarter_Modified
--------------------------- ----------------------- ----------------
HL Road Frame               2002-05-02 00:00:00.000 Spring
HL Mountain Frame           2005-06-01 00:00:00.000 Summer
LL Road Frame               2005-06-01 00:00:00.000 Summer
ML Road Frame               2005-06-01 00:00:00.000 Summer
ML Road Frame-W             2006-06-01 00:00:00.000 Summer
ML Mountain Frame           2006-06-01 00:00:00.000 Summer
ML Mountain Frame-W         2006-06-01 00:00:00.000 Summer
LL Mountain Frame           2006-11-20 09:56:38.273 Autumn
HL Touring Frame            2009-05-16 16:34:28.980 Spring
LL Touring Frame            2009-05-16 16:34:28.980 Spring

(10 rows affected)
```  
  
## Next steps

- [IIF &#40;Transact-SQL&#41;](../../t-sql/functions/logical-functions-iif-transact-sql.md)  
- [CASE &#40;Transact-SQL&#41;](../../t-sql/language-elements/case-transact-sql.md)
