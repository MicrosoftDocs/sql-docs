---
title: "STR (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STR"
  - "STR_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "converting numbers to characters"
  - "characters [SQL Server], converting"
  - "character data [SQL Server]"
  - "STR function"
ms.assetid: de03531b-d9e7-4c3c-9604-14e582ac20c6
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# STR (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns character data converted from numeric data.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
STR ( float_expression [ , length [ , decimal ] ] )  
```  
  
## Arguments  
 *float_expression*  
 Is an expression of approximate numeric (**float**) data type with a decimal point.  
  
 *length*  
 Is the total length. This includes decimal point, sign, digits, and spaces. The default is 10.  
  
 *decimal*  
 Is the number of places to the right of the decimal point. *decimal* must be less than or equal to 16. If *decimal* is more than 16 then the result is truncated to sixteen places to the right of the decimal point.  
  
## Return Types  
 **varchar**  
  
## Remarks  
 If supplied, the values for *length* and *decimal* parameters to STR should be positive. The number is rounded to an integer by default or if the decimal parameter is 0. The specified length should be greater than or equal to the part of the number before the decimal point plus the number's sign (if any). A short *float_expression* is right-justified in the specified length, and a long *float_expression* is truncated to the specified number of decimal places. For example, STR(12**,**10) yields the result of 12. This is right-justified in the result set. However, STR(1223**,**2) truncates the result set to **. String functions can be nested.  
  
> [!NOTE]  
>  To convert to Unicode data, use STR inside a CONVERT or [CAST](../../t-sql/functions/cast-and-convert-transact-sql.md) conversion function.  
  
## Examples  
 The following example converts an expression that is made up of five digits and a decimal point to a six-position character string. The fractional part of the number is rounded to one decimal place.  
  
```  
SELECT STR(123.45, 6, 1);  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
------  
 123.5  
  
(1 row(s) affected)  
```  
  
 When the expression exceeds the specified length, the string returns `**` for the specified length.  
  
```  
SELECT STR(123.45, 2, 2);  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
--  
**  
  
(1 row(s) affected)  
```  
  
 Even when numeric data is nested within `STR`, the result is character data with the specified format.  
  
```  
SELECT STR (FLOOR (123.45), 8, 3);
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
--------  
 123.000  
  
(1 row(s) affected)  
```  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
 [FORMAT &#40;Transact-SQL&#41;](../../t-sql/functions/format-transact-sql.md)  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
  
  

